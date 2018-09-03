using System;
using System.Linq;
using System.Web.Mvc;
using Domain;
using Domain.Entities;
using Domain.Enums;
using Domain.Extensions;
using Domain.Interfaces.Requests;
using Domain.Interfaces.Requests.Dto;
using Domain.Interfaces.Users;
using Web.Autentications.Attributes;
using Web.Models.Common;
using Web.Extensions;
using Web.Models.Requests;


namespace Web.Controllers
{
    public class RequestController : Controller
    {
        public IDataStore<Category> CategoryDataStore { get; set; }

        public IDataStore<Request> RequestDataStore { get; set; }

        public IRequestCreationService RequestCreationService { get; set; }

        public ICurrentOperatorService CurrentOperatorService { get; set; }

        [HttpGet]
        [OperatorOnly]
        public ActionResult Create(long? categoryId)
        {
            if (!categoryId.HasValue)
            {
                var categories = CategoryDataStore.GetAll().ToArray();
                return View("ChooseRequestCategory", categories);
            }

            var cat = CategoryDataStore.Get(categoryId.Value);
            return View(new NewRequestFormDto
            {
                CategoryId = cat.Id,
                CategoryName = cat.Name,
                AuthorId = CurrentOperatorService.GetCurrentUser().Id
            });
        }

        [HttpPost]
        [OperatorOnly]
        public ActionResult Create(NewRequestFormDto requestFormDto)
        {
            if (!ModelState.IsValid)
            {
                return View(requestFormDto);
            }

            RequestCreationService.Create(requestFormDto);

            return Redirect(Url.RouteUrl(new
            {
                action = "ViewMyRequests"
            }));
        }

        [OperatorOnly]
        public ActionResult ViewMyRequests(UserLoadParams userLoadParams)
        {
            var currentUserId = CurrentOperatorService.GetCurrentUser().Id;
            var reqQuery = RequestDataStore.GetAll().Where(req => req.Author.Id == currentUserId);
            return ListOfRequests(userLoadParams, reqQuery);
        }

        private ActionResult ListOfRequests(UserLoadParams userLoadParams, IQueryable<Request> reqQuery)
        {
            // todo: automapper
            var query = reqQuery
                .Select(req => new RequestListItem
                {
                    Id = req.Id,
                    CategoryName = req.Category.Name,
                    CustemerFio = req.ConsumerName
                })
                .Filter(userLoadParams);

            var reqs = query
                .Paging(userLoadParams)
                .ToArray();

            var model = new RequestListDto
            {
                RequestListItems = reqs,
                UserLoadParams = userLoadParams,
                PagesInfo = new PagesInfo
                {
                    CurrentPage = userLoadParams.Page,
                    TotalRowsCount = query.Count()
                }
            };

            return View("ViewAll", model);
        }

        [CustomAuthorizeFilter(ERole.Admin, ERole.Worker)]
        public ActionResult ViewAll(UserLoadParams userLoadParams)
        {
            var reqQuery = RequestDataStore.GetAll();
            return ListOfRequests(userLoadParams, reqQuery);
        }

        [WorkerOnly]
        public ActionResult Edit(long id)
        {
            throw new NotImplementedException();
        }

        [AllowAnonymous]
        public ActionResult Details(long id)
        {
            var request = RequestDataStore.Get(id);

            var dto = new RequestDetailsDto
            {
                Id = request.Id,
                State = request.State.GetDisplay(),
                AuthorName = request.Author.Name,
                CategoryName = request.Category.Name,
                ConsumerName = request.ConsumerName,
                Date = request.Date,
                Comment = request.Comment,
                Phone = request.Phone
            };

            return View(dto);
        }
    }
}
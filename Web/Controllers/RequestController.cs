using System;
using System.Linq;
using System.Web.Mvc;
using Domain;
using Domain.Entities;
using Domain.Interfaces.Requests;
using Domain.Interfaces.Requests.Dto;
using Web.Autentications;
using Web.Autentications.Attributes;
using Web.Models.Common;
using Web.Models.Requests;

namespace Web.Controllers
{
    [OperatorOnly]
    public class RequestController : Controller
    {
        public IDataStore<Category> CategoryDataStore { get; set; }

        public IDataStore<Request> RequestDataStore { get; set; }

        public IRequestCreationService RequestCreationService { get; set; }

        [HttpGet]
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
                // todo wtf
                AuthorId = ((UserIndentity)((UserProvider)User).Identity).User.Id
            });
        }

        [HttpPost]
        public ActionResult Create(NewRequestFormDto requestFormDto)
        {
            if (!ModelState.IsValid)
            {
                return View(requestFormDto);
            }

            RequestCreationService.Create(requestFormDto);

            return Redirect(Url.RouteUrl(new
            {
                action = "ViewAll"
            }));
        }

        public ActionResult ViewAll( string categoryFilter, int page = 1)
        {
            // todo: automapper
            var skipCount = (page - 1) * PagesInfo.DefaultPageSize;
            var query = RequestDataStore.GetAll()
                .Select(req => new RequestListItem
                {
                    Id = req.Id,
                    CategoryName = req.Category.Name,
                    CustemerFio = req.ConsumerName
                });

            // todo: move to expression trees
            if (!string.IsNullOrWhiteSpace(categoryFilter))
            {
                query = query
                    .Where(dto => dto.CategoryName.ToLower().Contains(categoryFilter));
            }

            var reqs = query
                .Skip(skipCount)
                .Take(PagesInfo.DefaultPageSize)
                .ToArray();

            var model = new RequestListDto
            {
                RequestListItems = reqs,
                CategoryFilter = categoryFilter,
                PagesInfo = new PagesInfo()
                {
                    CurrentPage = page,
                    TotalRowsCount = query.Count()
                }
            };

            return View(model);
        }

        public ActionResult Edit(long id)
        {
            throw new NotImplementedException();
        }
    }
}
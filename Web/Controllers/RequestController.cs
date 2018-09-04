using System.Linq;
using System.Web.Mvc;
using Domain;
using Domain.Entities;
using Domain.Enums;
using Domain.Extensions;
using Domain.Interfaces.Process;
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

        public INextStateService NextStateService { get; set; }

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
                    CustemerFio = req.ConsumerName,
                    Editable = req.State == EState.InProgress || req.State == EState.Registered,
                    State = req.State
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

        [AdminOnly]
        public ActionResult ViewAll(UserLoadParams userLoadParams)
        {
            var reqQuery = RequestDataStore.GetAll();
            return ListOfRequests(userLoadParams, reqQuery);
        }

        [WorkerOnly]
        public ActionResult ViewRequestsToStart(UserLoadParams userLoadParams)
        {
            var reqQuery = RequestDataStore.GetAll().Where(req => req.State == EState.Registered);
            return ListOfRequests(userLoadParams, reqQuery);
        }

        [WorkerOnly]
        public ActionResult ViewRequestsInProgress(UserLoadParams userLoadParams)
        {
            var userId = CurrentOperatorService.GetCurrentUser().Id;
            var reqQuery = RequestDataStore.GetAll()
                .Where(req => req.Executor.Id == userId)
                .Where(req => req.State == EState.InProgress);
            return ListOfRequests(userLoadParams, reqQuery);
        }

        [WorkerOnly]
        public ActionResult ViewFinishedRequests(UserLoadParams userLoadParams)
        {
            var userId = CurrentOperatorService.GetCurrentUser().Id;
            var reqQuery = RequestDataStore.GetAll()
                .Where(req => req.Executor.Id == userId)
                .Where(req => req.State == EState.Done || req.State == EState.NotDone);
            return ListOfRequests(userLoadParams, reqQuery);
        }

        [WorkerOnly]
        public ActionResult Edit(long id)
        {
            var request = RequestDataStore.Get(id);

            var nextStates = NextStateService.NextStates(request.State);
            var dto = new RequestForWorkerDto
            {
                Id = request.Id,
                State = request.State,
                AuthorName = request.Author.Name,
                CategoryName = request.Category.Name,
                ConsumerName = request.ConsumerName,
                Date = request.Date,
                Comment = request.Comment,
                Phone = request.Phone,
                NextState = nextStates.FirstOrDefault(),
                NextStates = nextStates,
                WorkerComment = request.WorkerComment
            };

            return View(dto);
        }

        [WorkerOnly]
        [HttpPost]
        public ActionResult Edit(RequestForWorkerDto dto)
        {
            if (dto.State == EState.InProgress && string.IsNullOrWhiteSpace(dto.WorkerComment))
            {
                ModelState.AddModelError(nameof(dto.WorkerComment), $"{CurrentOperatorService.GetCurrentUser().Name}, добавьте, пожалуйста, комментарий.");
            }

            if (!dto.NextState.HasValue)
            {
                ModelState.AddModelError(nameof(dto.NextState), $"{CurrentOperatorService.GetCurrentUser().Name}, выберите следующий статус.");
            }

            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var request = RequestDataStore.Get(dto.Id);

            request.State = dto.NextState.Value;

            request.WorkerComment = dto.WorkerComment;

            request.Executor = CurrentOperatorService.GetCurrentUser();

            RequestDataStore.Update(request);

            return RedirectToAction("Details", new
            {
                id = dto.Id
            });
        }

        [AllowAnonymous] //todo: дыра в безопасности?
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
                Phone = request.Phone,
                WorkerComment = request.WorkerComment
            };

            return View(dto);
        }
    }
}
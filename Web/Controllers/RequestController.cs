using System.Linq;
using System.Web.Mvc;
using Domain.Entities;
using NHibernateConfigs;
using Web.Models.Requests;
using Web.Services;

namespace Web.Controllers
{
    public class RequestController : Controller
    {
        [HttpGet]
        public ActionResult Create(long? categoryId)
        {
            var catDataStore = new DataStore<Category>();
            if (!categoryId.HasValue)
            {
                var categories = catDataStore.GetAll().ToArray();
                return View("ChooseRequestCategory", categories);
            }

            var cat = catDataStore.Get(categoryId.Value);
            return View(new NewRequestFormDto()
            {
                CategoryId = cat.Id,
                CategoryName = cat.Name
            });
        }

        [HttpPost]
        public ActionResult Create(NewRequestFormDto requestFormDto)
        {
            if (!ModelState.IsValid)
            {
                return View(requestFormDto);
            }

            var requestCreationService = new RequestCreationService();
            requestCreationService.Create(requestFormDto);

            return Redirect(Url.RouteUrl(new
            {
                action = "ViewAll"
            }));
        }
        
        public ActionResult ViewAll()
        {
            var reqDataStore = new DataStore<Request>();
            var reqs = reqDataStore.GetAll()
                .Select(req => new RequestListDto
                {
                    Id = req.Id,
                    CategoryName = req.Category.Name,
                    CustemerFio = req.ConsumerName
                }).ToArray();

            return View(reqs);
        }

        public ActionResult Edit(long id)
        {
            throw new System.NotImplementedException();
        }
    }
}
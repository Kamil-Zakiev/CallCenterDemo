using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models.Requests;

namespace Web.Controllers
{
    public class RequestController : Controller
    {
        [HttpGet]
        public ActionResult Create()
        {
            return View(new NewRequestFormDto());
        }

       [HttpPost]
        public ActionResult Create(NewRequestFormDto requestFormDto)
        {
            if (!ModelState.IsValid)
            {
                return View(requestFormDto);
            }
            return Content("ok");
        }


    }
}
﻿using System.Web.Mvc;
using Domain.Entities;
using NHibernateConfigs;
using System.Linq;

namespace Web.Controllers
{
    public class CategoryController : Controller
    {
        [HttpGet]
        public ActionResult Create(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View(new Category());

            //return Redirect(returnUrl);
        }

        [HttpPost]
        public ActionResult Create(Category category, string returnUrl)
        {
            var catDataStore = new DataStore<Category>();
            var alreadyExist = catDataStore.GetAll().Any(cat => cat.Name == category.Name);
            if (alreadyExist)
            {
                ModelState.AddModelError("", "Категория с данным наименованием уже добавлена");
                ViewBag.returnUrl = returnUrl;
                return View(new Category());
            }

            catDataStore.Save(category);
            return Redirect(returnUrl);
        }
    }
}
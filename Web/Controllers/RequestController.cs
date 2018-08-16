﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models.Requests;

namespace Web.Controllers
{
    public class RequestController : Controller
    {
        // GET: Request
        [HttpGet]
        public ActionResult Create()
        {
            return View(new NewRequestFormDto());
        }
    }
}
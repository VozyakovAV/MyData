﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyData.Controllers
{
    public class SystemController : Controller
    {
        public ActionResult Error()
        {
            return View();
        }

        public ActionResult Error404()
        {
            return View();
        }

        public ActionResult jsError(string s)
        {
            return Json(new
            {
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
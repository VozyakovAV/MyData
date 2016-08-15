using Domain;
using MyData.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyData.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return Redirect(mng.MapSite.GetPlanUrl());
        }

        public ActionResult jsError(string s)
        {
            return Json(new
            {
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
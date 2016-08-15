using Domain.Memory;
using MyData.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyData.Areas.Memory.Controllers
{
    public class SetController : BaseController
    {
        public ActionResult Index(int folderID = 0)
        {
            var vm = mng.Memory.CreateSetsVM(folderID);
            return View(vm);
        }

        public ActionResult CreateSet(int folderID = 0)
        {
            var vm = mng.Memory.CreateSetEditVM(folderID);
            return PartialView(vm);
        }

        [HttpPost]
        public ActionResult CreateSet(CSet setPattern)
        {
            var set = mng.Memory.CreateSet(setPattern);
            return Json(set != null);
        }

        public ActionResult EditSet(int id)
        {
            var set = mng.Memory.GetSet(id);
            if (set != null)
            {
                var vm = mng.Memory.CreateSetEditVM(set);
                return PartialView(vm);
            }
            return View("Index");
        }

        [HttpPost]
        public ActionResult EditSet(CSet setPattern)
        {
            var set = mng.Memory.EditSet(setPattern);
            return Json(set != null);
        }

        [HttpPost]
        public ActionResult DeleteSet(int id)
        {
            var set = mng.Memory.GetSet(id);
            var res = mng.Memory.Delete(set);
            return Json(res);
        }
    }
}
using Domain.Memory;
using MyData.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyData.Areas.Memory.Controllers
{
    public class TermController : BaseController
    {
        public ActionResult Index(int setID = 0)
        {
            if (setID == 0)
                return RedirectToAction("Index", "Set");

            var vm = mng.Memory.CreateTermsVM(setID);
            return View(vm);
        }

        public ActionResult CreateQuestion(int setID = 0)
        {
            var vm = mng.Memory.CreateTermEditVM(setID);
            return PartialView(vm);
        }

        [HttpPost]
        public ActionResult CreateTerm(CTerm questionPattern)
        {
            var term = mng.Memory.CreateTerm(questionPattern);
            return Json(term != null);
        }

        public ActionResult EditTerm(int id)
        {
            var term = mng.Memory.GetTerm(id);
            if (term != null)
            {
                var vm = mng.Memory.CreateTermEditVM(term);
                return PartialView(vm);
            }
            return View("Index");
        }

        [HttpPost]
        public ActionResult EditTerm(CTerm questionPattern)
        {
            var term = mng.Memory.EditTerm(questionPattern);
            return Json(term != null);
        }

        [HttpPost]
        public ActionResult DeleteTerm(int id)
        {
            var term = mng.Memory.GetTerm(id);
            var res = mng.Memory.Delete(term);
            return Json(res);
        }
    }
}
using Domain.Memory;
using MyData.Infrastructure;
using MyData.Models;
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

        public ActionResult CreateTerm(int setID = 0)
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

        public ActionResult Import(int setID = 0)
        {
            var vm = mng.Memory.CreateImportVM(setID);
            if (vm == null)
                return HttpNotFound();
            return View(vm);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Import(ImportVM vm)
        {
            mng.ImportExportTerms.ImportTerms(vm.SetID, vm.Text, vm.WordDelimeter, vm.RowDelimeter);
            return RedirectToRoute("RouteSet", new { setID = vm.SetID });
        }

        [HttpPost, ValidateInput(false)]
        public JsonResult DoImport(string text, string wordDelimeter, string rowDelimeter)
        {
            var terms = mng.ImportExportTerms.ParseTerms(text, wordDelimeter, rowDelimeter);
            return Json(new
            {
                result = terms
            });
        }

        public ActionResult Export(int setID = 0)
        {
            var vm = mng.Memory.CreateExportVM(setID);
            if (vm == null)
                return HttpNotFound();
            return View(vm);
        }

        [HttpPost, ValidateInput(false)]
        public JsonResult DoExport(int setID, string wordDelimeter, string rowDelimeter)
        {
            var text = mng.ImportExportTerms.ExportTerms(setID, wordDelimeter, rowDelimeter);
            return Json(new
            {
                result = text
            });
        }
    }
}
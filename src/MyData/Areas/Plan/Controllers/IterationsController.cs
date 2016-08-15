using Domain;
using MyData.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyData.Areas.Plan.Controllers
{
    public class IterationsController : BaseController
    {
        public ActionResult Index()
        {
            var vm = mng.Iterations.CreateIterationsVM();
            return View(vm);
        }

        public ActionResult Iteration(int? id)
        {
            if (!id.HasValue)
                return Redirect("Index");
            var vm = mng.Iterations.CreateIterationVM(id.Value);
            if (vm == null)
                return HttpNotFound();
            return View("Iteration", vm);
        }

        public ActionResult CreateIteration()
        {
            var vm = mng.Iterations.CreateIterationVM();
            return PartialView(vm);
        }

        [HttpPost]
        public ActionResult CreateIteration(CIteration iterationPattern)
        {
            var iteration = mng.Iterations.CreateIteration(iterationPattern);
            return Json(iteration != null);
        }

        public ActionResult EditIteration(int id)
        {
            var iteration = mng.Iterations.GetIteration(id);
            if (iteration != null)
            {
                var vm = mng.Iterations.CreateIterationVM(iteration);
                return PartialView(vm);
            }
            return View("Index");
        }

        [HttpPost]
        public ActionResult EditIteration(CIteration iterationPattern)
        {
            var iteration = mng.Iterations.EditIteration(iterationPattern);
            return Json(iteration != null);
        }

        [HttpPost]
        public ActionResult DeleteIteration(int id)
        {
            var res = mng.Iterations.Delete(id);
            return Json(res);
        }
    }
}
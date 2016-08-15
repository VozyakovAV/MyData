using Domain;
using MyData.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyData.Areas.Plan.Controllers
{
    public class ProjectsController : BaseController
    {
        public ActionResult Index()
        {
            var vm = mng.Projects.GetProjectsVM();
            return View(vm);
        }

        public ActionResult Project(int? id)
        {
            if (!id.HasValue)
                return Redirect("Index");
            var vm = mng.Projects.CreateProjectVM(id.Value);
            if (vm == null)
                return HttpNotFound();
            return View(vm);
        }

        public ActionResult CreateProject()
        {
            var vm = mng.Projects.CreateProjectVM();
            return PartialView(vm);
        }

        [HttpPost]
        public ActionResult CreateProject(CProject projectPattern)
        {
            var project = mng.Projects.CreateProject(projectPattern);
            return Json(project != null);
        }

        public ActionResult EditProject(int id)
        {
            var project = mng.Projects.GetProject(id);
            if (project != null)
            {
                var vm = mng.Projects.CreateProjectVM(project);
                return PartialView(vm);
            }
            return View("Index");
        }

        [HttpPost]
        public ActionResult EditProject(CProject projectPattern)
        {
            var project = mng.Projects.EditProject(projectPattern);
            return Json(project != null);
        }

        [HttpPost]
        public ActionResult DeleteProject(int id)
        {
            var res = mng.Projects.Delete(id);
            return Json(res);
        }
    }
}
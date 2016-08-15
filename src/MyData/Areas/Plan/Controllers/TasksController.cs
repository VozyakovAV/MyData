using Domain;
using MyData.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyData.Areas.Plan.Controllers
{
    public class TasksController : BaseController
    {
        public ActionResult CreateTask(int? projectID)
        {
            var vm = mng.Tasks.CreateTaskVM(projectID);
            return PartialView(vm);
        }

        [HttpPost]
        public ActionResult CreateTask(CTask taskPattern)
        {
            var task = mng.Tasks.CreateTask(taskPattern);
            return Json(task != null);
        }

        public ActionResult EditTask(int id)
        {
            var task = mng.Tasks.GetTask(id);
            if (task != null)
            {
                var vm = mng.Tasks.CreateTaskVM(task);
                return PartialView(vm);
            }
            return View("Index");
        }

        [HttpPost]
        public ActionResult EditTask(CTask taskPattern)
        {
            var task = mng.Tasks.EditTask(taskPattern);
            return Json(task != null);
        }

        [HttpPost]
        public ActionResult DeleteTask(int id)
        {
            var res = mng.Tasks.Delete(id);
            return Json(res);
        }

        [HttpPost]
        public ActionResult ComleteTask(CTask taskPattern)
        {
            var task = mng.Tasks.CompleteTask(taskPattern);
            return Json(task != null);
        }
    }
}
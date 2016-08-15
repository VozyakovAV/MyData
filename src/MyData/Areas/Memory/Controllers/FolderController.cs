using Domain.Memory;
using MyData.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyData.Areas.Memory.Controllers
{
    public class FolderController : BaseController
    {
        public ActionResult Index()
        {
            var vm = mng.Memory.CreateFoldersVM();
            return View(vm);
        }

        public ActionResult CreateFolder()
        {
            var vm = mng.Memory.CreateFolderEditVM();
            return PartialView(vm);
        }

        [HttpPost]
        public ActionResult CreateFolder(CFolder folderPattern)
        {
            var folder = mng.Memory.CreateFolder(folderPattern);
            return Json(folder != null);
        }

        public ActionResult EditFolder(int id)
        {
            var folder = mng.Memory.GetFolder(id);
            if (folder != null)
            {
                var vm = mng.Memory.CreateFolderEditVM(folder);
                return PartialView(vm);
            }
            return View("Index");
        }

        [HttpPost]
        public ActionResult EditFolder(CFolder folderPattern)
        {
            var folder = mng.Memory.EditFolder(folderPattern);
            return Json(folder != null);
        }

        [HttpPost]
        public ActionResult DeleteFolder(int id)
        {
            var folder = mng.Memory.GetFolder(id);
            var res = mng.Memory.Delete(folder);
            return Json(res);
        }
    }
}
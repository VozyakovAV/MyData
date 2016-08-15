using AutoMapper;
using MyData.Infrastructure;
using MyData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyData.Areas.Memory.Controllers
{
    public class MemoryController : BaseController
    {
        public PartialViewResult Menu(string category = null)
        {
            //ViewBag.CurrentCategory = category;

            var folders = mng.Memory.GetFolders()
                .OrderByDescending(x => x.Name).ToList();

            var vm = new MemoryMenuVM();
            vm.Folders = Mapper.Map<List<FolderVM>>(folders);

            return PartialView(vm);
        }
    }
}
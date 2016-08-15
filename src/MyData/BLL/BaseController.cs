using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyData.BLL.Site;

namespace MyData.Infrastructure
{
    public class BaseController : Controller
    {
        private ManagerSite _mng;
        protected ManagerSite mng
        {
            get
            {
                if (_mng == null)
                    _mng = new ManagerSite();
                return _mng;
            }
        }

        protected JsonResult Json(int result)
        {
            return Json(new
            {
                result = result
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (mng != null)
                mng.Dispose();
        }
    }
}
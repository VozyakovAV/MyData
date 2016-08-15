using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;
using MyData.BLL.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyData.Tests
{
    public class BaseTest
    {
        private ManagerSite _mng;
        public ManagerSite mng
        {
            get
            {
                if (_mng == null)
                    _mng = new ManagerSite();
                return _mng;
            }
        }

        public void FlushDB()
        {
            if (_mng != null)
            {
                _mng.Dispose();
                _mng = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
        }

        public void DeleteDB()
        {
            var db = new MyDbContext();
            if (db.Database.Exists())
                db.Database.Delete();
        }

        public virtual void CleanDB()
        {
            FlushDB();
        }
    }
}

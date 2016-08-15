using System;
using System.Collections.Generic;
using System.Linq;
using Domain;

namespace MyData.BLL.Site
{
    public class ManagerBase
    {
        public ManagerSite ManagerSite { get; private set; }

        public ManagerBase(ManagerSite manager)
        {
            this.ManagerSite = manager;
        }
    }
}
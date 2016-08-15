using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;

namespace MyData.BLL.Site
{
    public class MapsiteMaster : ManagerBase
    {
        public MapsiteMaster(ManagerSite manager) : base(manager)
        { }

        public string GetPlanUrl()
        {
            return "/plan";
        }

        public string GetIterationsUrl()
        {
            return "/plan/iterations";
        }

        public string GetIterationUrl(CIteration iteration)
        {
            return "/plan/iteration/" + iteration.Id;
        }
    }
}
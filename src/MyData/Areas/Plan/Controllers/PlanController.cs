using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain;
using MyData.Infrastructure;
using MyData.Models;

namespace MyData.Areas.Plan.Controllers
{
    [Authorize]
    public class PlanController : BaseController
    {
        public ActionResult Index()
        {
            var iteration = mng.Iterations.GetCurrentIteration();
            if (iteration != null)
                return new IterationsController().Iteration(iteration.Id);
            return new IterationsController().Index();
        }

        public PartialViewResult Menu(string category = null)
        {
            //ViewBag.CurrentCategory = category;

            var iterations = mng.Iterations.GetOpenIterations()
                .OrderByDescending(x => x.DateStart).ToList();

            var projects = mng.Projects.GetTreeOpenProjects()
                .OrderBy(x => x.Name).ToList();

            var vm = new MenuVM() { Iterations = iterations, Projects = projects };

            return PartialView(vm);
        }

        
        
    }
}
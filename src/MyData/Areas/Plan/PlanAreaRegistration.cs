using System.Web.Mvc;

namespace MyData.Areas.Plan
{
    public class PlanAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Plan";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute("RoutePlan", "Plan", new { controller = "Iterations", action = "Index" });

            context.MapRoute("RouteProjects", "Plan/Projects", new { controller = "Projects", action = "Index" });
            context.MapRoute("RouteProject", "Plan/Projects/{id}", new { controller = "Projects", action = "Project" }, new { id = "^\\d+$" });

            context.MapRoute("RouteIterations", "Plan/Iterations", new { controller = "Iterations", action = "Index" });
            context.MapRoute("RouteIteration", "Plan/Iterations/{id}", new { controller = "Iterations", action = "Iteration" }, new { id = "^\\d+$" });

            context.MapRoute(
                "Plan_default",
                "Plan/{controller}/{action}/{id}",
                new { controller = "Plan", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
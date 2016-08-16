using System.Web.Mvc;

namespace MyData.Areas.Memory
{
    public class MemoryAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Memory";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute("RouteSet", "Memory/Term/Index/{setID}", new { controller = "Term", action = "Index" }, new { setID = "^\\d+$" });

            context.MapRoute(
                "Memory_default",
                "Memory/{controller}/{action}/{id}",
                new { controller = "folder", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyData.Models;
using System.Web.Security;

namespace MyData.Controllers
{
    public class AccountController : Controller
    {
        public ViewResult Login()
        {
            return View(new LoginVM() { UserName = "Andrew" });
        }

        [HttpPost]
        public ActionResult Login(LoginVM model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Authenticate(model.UserName, model.Password))
                {
                    return Redirect(Url.Action("Index", "Plan"));
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин или пароль");
                    return View();
                }
            }
            return View();
        }

        private bool Authenticate(string username, string password)
        {
            // TODO: метод вынести в отдельный класс, использоваеть через интерфейс IoC
            // переделать на Membership
            bool result = FormsAuthentication.Authenticate(username, password);
            if (result)
                FormsAuthentication.SetAuthCookie(username, false);
            return result;
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}
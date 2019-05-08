using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Lena.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Login(string username, string password)
        {
            Lena.Business.UserService userService = new Business.UserService();
            if (userService.login(username, password))
            {
                FormsAuthentication.SetAuthCookie(username, true);
                return Json(1);
            }
            return Json("Kullanıcı adı veya şifreniz yanlış");
        }
    }
}
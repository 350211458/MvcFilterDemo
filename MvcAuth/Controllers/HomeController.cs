using MvcAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using MvcAuth.App_Start;
using System.Security.Principal;

namespace MvcAuth.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [CustomAuth(Roles ="Admin,Test",Users ="HeroWong")]
        public string Index()
        {
            return HttpContext.User.Identity.Name;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserInfo user)
        {
            if(user.UserName == user.Password)
            {
                user.Roles = new string[] { "Test" };
                Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
                var ticket = new FormsAuthenticationTicket(1, user.UserName, DateTime.Now, DateTime.Now.Add(FormsAuthentication.Timeout), false, string.Join(",", user.Roles));
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
                Response.Cookies.Add(cookie);
                return RedirectToAction("Index");
            }
            ViewBag.Msg = "密码错误";
            return View();
        }
    }
}
using MvcFilterDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcFilterDemo.App_Start;

namespace MvcFilterDemo.Controllers
{
    public class HomeController : Controller
    {
        [CustomAuthorize(Users ="HeroWong",Roles ="Admin")]
        public string Index()
        {
            return HttpContext.User.Identity.Name;
        }
        
        [CustomAuthorize(Users ="HeroWong",Roles ="Admin")]
        [CustomActionFilter]
        public ActionResult Welcome(string name)
        {
            ViewBag.Name = name;
            return View();
        }
        
        public ActionResult Throw()
        {
            throw new Exception("出现异常了");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user, string returnUrl)
        {
            if(ModelState.IsValid)
            {
                if(user.UserName == user.Password)
                {
                    string roles = "Admin,Test";
                    DateTime start = DateTime.Now;
                    DateTime end = start.Add(FormsAuthentication.Timeout);
                    var ticket = new FormsAuthenticationTicket(1, user.UserName, start, end, false, roles);
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
                    Response.Cookies.Add(cookie);
                    if(string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return RedirectToAction("Index");
                    }
                    return Redirect(returnUrl);
                }
                else
                {
                    ViewBag.Message = "密码错误";
                }
            }
            return View();
        }
    }
}
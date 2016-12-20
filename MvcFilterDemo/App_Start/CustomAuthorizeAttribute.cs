using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcFilterDemo.App_Start
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var identity = filterContext.HttpContext.User?.Identity as FormsIdentity;
            if(identity != null && identity.IsAuthenticated)
            {
                string[] roles = identity.Ticket.UserData.Split(',');
                filterContext.HttpContext.User = new GenericPrincipal(identity, roles);
                base.OnAuthorization(filterContext);
            }
            else
            {
                //filterContext.Result = new ContentResult { Content = "您没有权限访问!" };
                string returnUrl = HttpUtility.UrlEncode(filterContext.HttpContext.Request.Url.PathAndQuery);
                filterContext.Result = new RedirectResult("/Home/Login?returnUrl="+returnUrl);
            }
        }
    }
}
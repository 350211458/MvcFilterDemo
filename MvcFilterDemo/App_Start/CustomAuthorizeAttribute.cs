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
        string Message { get; set; }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if(!AuthorizeCore(filterContext.HttpContext))
            {
                if(Message == "请登录")
                {
                    string returnUrl = HttpUtility.UrlEncode(filterContext.HttpContext.Request.Url.PathAndQuery);
                    filterContext.Result = new RedirectResult("/Home/Login?returnUrl=" + returnUrl);
                }
                else
                {
                    filterContext.Result = new ContentResult() { Content = Message };
                }
            }
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var identity = httpContext.User?.Identity as FormsIdentity;
            if(identity != null && identity.IsAuthenticated)
            {
                if(VerifyName(identity.Name))
                {
                    return VerifyRole(identity.Ticket.UserData);
                }
            }
            else
            {
                Message = "请登录";
            }
            return false;
        }

        #region 对Users、Roles进行分割
        /// <summary>
        /// 对Users、Roles进行分割
        /// </summary>
        /// <param name="original">带','的字符串</param>
        /// <returns></returns>
        protected string[] SplitString(string original)
        {
            if(string.IsNullOrWhiteSpace(original))
            {
                return new string[0];
            }
            else
            {
                return original.Split(',');
            }
        }
        #endregion

        #region 校验用户名
        bool VerifyName(string name)
        {
            if(!string.IsNullOrEmpty(Users) && Users.IndexOf(name, StringComparison.InvariantCultureIgnoreCase) != -1)
            {
                return true;
            }
            Message = "您的账号不能请求该网页";
            return false;
        }
        #endregion

        #region 校验角色
        bool VerifyRole(string userData)
        {
            string[] roles = SplitString(userData);
            string[] roleArray = SplitString(Roles);
            if(roleArray.Length > 0)
            {
                if(roleArray.Any(r => roles.Contains(r, StringComparer.InvariantCultureIgnoreCase)))
                {
                    return true;
                }
                Message = "您的角色不能请求该网页";
                return false;
            }
            return true;
        }
        #endregion
    }
}
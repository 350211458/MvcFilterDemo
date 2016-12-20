using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcFilterDemo.App_Start
{
    public class CustomActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.OnActionExecuting = DateTime.Now.Millisecond + "  在执行操作方法之前由 ASP.NET MVC 框架调用。";
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.Controller.ViewBag.OnActionExecuted = DateTime.Now.Millisecond + "  在执行操作方法后由 ASP.NET MVC 框架调用。";
            base.OnActionExecuted(filterContext);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.OnResultExecuting = DateTime.Now.Millisecond + "  在执行操作结果之前由 ASP.NET MVC 框架调用。";
            base.OnResultExecuting(filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            //渲染试图后调用
            filterContext.Controller.ViewBag.OnResultExecuted = DateTime.Now.Millisecond + "  在执行操作结果后由 ASP.NET MVC 框架调用。";
            base.OnResultExecuted(filterContext);
        }
    }
}
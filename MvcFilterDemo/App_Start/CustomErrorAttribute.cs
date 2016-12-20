using MvcFilterDemo.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MvcFilterDemo.App_Start
{
    public class CustomErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            ExceptionHandler.Enqueue(filterContext.Exception);
        }
    }
}
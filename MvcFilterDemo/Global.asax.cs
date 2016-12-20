using System.Web.Mvc;
using System.Web.Routing;
using MvcFilterDemo.App_Start;
using MvcFilterDemo.Infrastructure;
using System.Web;

namespace MvcFilterDemo
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //注册全局异常处理
            GlobalFilters.Filters.Add(new CustomErrorAttribute());
            //注册日志记录
            Log.StartRecordLog();
        }
    }
}

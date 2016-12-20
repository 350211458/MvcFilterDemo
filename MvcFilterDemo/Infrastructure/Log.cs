using log4net;
using log4net.Config;
using MvcFilterDemo.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace MvcFilterDemo.Infrastructure
{
    public static class Log
    {
        public static void StartRecordLog()
        {
            XmlConfigurator.Configure();
            Task.Factory.StartNew(() =>
            {
                while(true)
                {
                    if(ExceptionHandler.HasException)
                    {
                        Exception e = ExceptionHandler.Dequeue();
                        ILog log = LogManager.GetLogger("异常信息");
                        log.Error(e.ToString(new StringBuilder()));
                    }
                    else
                    {
                        Thread.Sleep(5000);
                    }
                }
            });
        }
        
        public static void Info(string name, string message) =>
            LogManager.GetLogger(name).Info(message);
        public static void Debug(string name, string message) =>
            LogManager.GetLogger(name).Debug(message);
        public static void Warn(string name, string message) =>
            LogManager.GetLogger(name).Warn(message);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MvcFilterDemo.Infrastructure
{
    public static class ExceptionHandler
    {
        static Queue<Exception> _exceptions = new Queue<Exception>();

        public static void Enqueue(Exception ex) => _exceptions.Enqueue(ex);

        public static Exception Dequeue() => _exceptions.Dequeue();

        public static bool HasException { get { return _exceptions.Count > 0; } }


        public static string ToString(this Exception e, StringBuilder sb, int depth = 0)
        {
            sb.AppendLine();
            string space = new string(' ', depth);
            sb.AppendLine(string.Format("{0}异常描述信息：{1}", space, e.Message));
            sb.AppendLine(string.Format("{0}导致错误的应用程序或对象的名称：{1}", space, e.Source));
            sb.AppendLine(string.Format("{0}引发当前异常的方法：{1}", space, e.TargetSite));
            sb.AppendLine(string.Format("{0}堆栈信息：{1}", space, e.StackTrace?.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)[0].Trim()));
            if(e.InnerException != null)
            {
                sb.AppendLine(string.Format("\r\n{0}内部异常：", space));
                return ToString(e.InnerException, sb, depth + 4);
            }
            return sb.ToString();
        }
    }
}
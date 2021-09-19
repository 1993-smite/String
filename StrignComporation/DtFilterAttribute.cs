using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Web.Http.Filters;

namespace StrignComporation
{
    public class DtFilterAttribute : ActionFilterAttribute
    {
        private Stopwatch sw = new Stopwatch();

        public void OnResultExecuted()
        {
            sw.Stop();
            Console.WriteLine("Time {0}", sw.ElapsedMilliseconds);
        }

        public void OnResultExecuting()
        {
            sw.Start();
        }

        public void OnActionExecuting()
        {
            sw.Start();
        }
    }
}

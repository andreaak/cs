using System;
using System.Diagnostics;
using System.Web;

namespace ASPWebFormsTest._17_Modules
{
    public class _03_PerformanceModule : IHttpModule
    {
        private Stopwatch sw;

        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += context_BeginRequest;
            context.EndRequest += context_EndRequest;
        }
        
        void context_BeginRequest(object sender, EventArgs e)
        {
            sw = Stopwatch.StartNew();
        }

        void context_EndRequest(object sender, EventArgs e)
        {
            sw.Stop();
            HttpApplication app = (HttpApplication)sender;
            app.Response.Write(@"----------------------------------------------------------------------<br/>
            Время потраченное на обработку запроса " + sw.Elapsed);
        }
    }
}
using System;
using System.Threading;
using System.Web;

namespace ASPWebFormsTest._20_AsyncPages
{
    public class _08_AsyncHandler : IHttpAsyncHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            Thread.Sleep(2000);
            context.Response.Write("From Async Method -- Thread Id =" + Thread.CurrentThread.ManagedThreadId 
                + ";<br />   ");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        // Начало асинхронной операции.
        public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
        {
            context.Response.Write("Start -- Thread Id =" + Thread.CurrentThread.ManagedThreadId + ";<br />  ");
            Action<HttpContext> del = new Action<HttpContext>(ProcessRequest);
            return del.BeginInvoke(context, cb, context);
        }

        // Завершение асинхронной операции.
        public void EndProcessRequest(IAsyncResult result)
        {
            HttpContext context = result.AsyncState as HttpContext;
            if (context != null)
            {
                context.Response.Write("EndProcessRequest -- Thread Id =" + Thread.CurrentThread.ManagedThreadId 
                    + ";<br />  ");
            }
        }
    }
}
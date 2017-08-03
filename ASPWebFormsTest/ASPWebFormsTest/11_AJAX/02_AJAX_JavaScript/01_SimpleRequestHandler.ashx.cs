using System;
using System.Web;

namespace ASPWebFormsTest._11_AJAX._02_AJAX_JavaScript
{
    public class _01_SimpleRequestHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write(DateTime.Now.ToLongTimeString());
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}
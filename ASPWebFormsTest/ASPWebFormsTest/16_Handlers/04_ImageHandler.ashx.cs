using System.Web;

namespace ASPWebFormsTest._16_Handlers
{
    /// <summary>
    /// Summary description for _02_ImageHandler
    /// </summary>
    public class _02_ImageHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string path = context.Server.MapPath("Images/logo.jpg");
            context.Response.ContentType = "image/jpg";
            context.Response.WriteFile(path);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
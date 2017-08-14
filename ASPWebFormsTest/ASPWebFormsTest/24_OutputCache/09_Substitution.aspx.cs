using System;
using System.Web;

namespace ASPWebFormsTest._24_OutputCache
{
    public partial class _09_Substitution : System.Web.UI.Page
    {
        public static string GetDate(HttpContext context)
        {
            string date = DateTime.Now.ToLongTimeString();
            string message = string.Format("<h2>Эта часть страницы обновляется регулярно. Последнее обновление {0}</h2>", date);

            return message;
        }
    }
}

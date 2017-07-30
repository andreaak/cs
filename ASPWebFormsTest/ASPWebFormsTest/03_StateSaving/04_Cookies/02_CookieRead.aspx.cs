using System;
using System.Web;

namespace ASPWebFormsTest._03_StateSaving._04_Cookies
{
    public partial class _02_CookieRead : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies["MyCookie"];
            if (cookie != null)
            {
                OutputLabel.Text = cookie.Value;
            }
            else
            {
                OutputLabel.Text = "В запросе нет cookie-набора с именем 'MyCookie'";
            }
        }
    }
}
using System;
using System.Web;

namespace ASPWebFormsTest._03_StateSaving._06_AuthenticationSample
{
    public partial class Closed : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie name = Request.Cookies["name"];
            HttpCookie sign = Request.Cookies["sign"];

            if (name != null && sign != null)
            {
                // Если полученная подпись правильная прекращаем дальнейшую обработку события Load
                // и переходим к следующим этапам жизненного цикла страницы.
                if (sign.Value == CryptoProvider.GetMD5Hash(name.Value + "s@lt"))
                {
                    return;
                }
            }

            Response.Redirect("Login.aspx");
        }
    }
}
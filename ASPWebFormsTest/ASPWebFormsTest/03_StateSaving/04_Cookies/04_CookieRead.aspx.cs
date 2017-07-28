using System;
using System.Web;

namespace ASPWebFormsTest._03_StateSaving._04_Cookies
{
    public partial class CookieRead2 : System.Web.UI.Page
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

        // Удаление значений cookie-набора.
        protected void ClearCookies_Click(object sender, EventArgs e)
        {
            HttpCookie cookie = new HttpCookie("MyCookie");
            // Для удаления cookie-набора устанавливаем значение свойства Expires равное прошедшей дате.
            cookie.Expires = DateTime.Now.AddDays(-1);

            // Добавляем cookie в ответ.
            Response.Cookies.Add(cookie);

            // Делаем ответ браузеру и заставляем его выполнить запрос на эту же страницу.
            Response.Redirect(Request.Url.PathAndQuery);

            // При отправке сообщения браузеру в тело ответа добавляется просроченный cookie-набор.
            // При повторном запросе к серверу браузер видит, что cookie-набор просрочен и удаляет его
            // вместо того, что бы добавить в запрос.
        }
    }
}
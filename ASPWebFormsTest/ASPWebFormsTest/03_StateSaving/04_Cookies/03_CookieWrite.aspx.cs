using System;
using System.Web;

namespace ASPWebFormsTest._03_StateSaving._04_Cookies
{
    public partial class CookieUsing2 : System.Web.UI.Page
    {
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            // Создаем cookie набор.
            // Такой cookie-набор будет храниться до тех пор, пока пользователь не закроет браузер.
            HttpCookie cookie = new HttpCookie("MyCookie", TextBox1.Text);

            // Данный cookie-набор будет сохранен в браузере на 7 дней.
            // Каждый раз когда web браузер будет делать запрос к данному сайту
            // к запросу будут добавляться значения cookie-набора.
            cookie.Expires = DateTime.Now.AddDays(7);

            // Добавляем cookie-набор в текущий ответ.
            Response.Cookies.Add(cookie);
        }
    }
}
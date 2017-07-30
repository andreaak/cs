using System;
using System.Web;

namespace ASPWebFormsTest._03_StateSaving._04_Cookies
{
    public partial class _01_Cookie : System.Web.UI.Page
    {
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            // Создаем cookie набор.
            // Такой cookie-набор будет храниться до тех пор, пока пользователь не закроет браузер.
            HttpCookie cookie = new HttpCookie("MyCookie", TextBox1.Text);
            
            // Добавляем cookie-набор в текущий ответ.
            Response.Cookies.Add(cookie);
        }
    }
}
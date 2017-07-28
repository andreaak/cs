using System;
using System.Web;

namespace ASPWebFormsTest._03_StateSaving._06_AuthenticationSample
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            if (PasswordTextBox.Text == "qwerty" && LoginTextBox.Text == "admin")
            {
                HttpCookie cookieName = new HttpCookie("name", LoginTextBox.Text);
                HttpCookie cookieSign = new HttpCookie("sign", CryptoProvider.GetMD5Hash(LoginTextBox.Text + "s@lt"));
                // Для того, что бы гарантировать, что данный cookie-набор был создан нашей странице, а не злоумышленником,
                // в ответ кроме имени мы отправляем cookie-набор подпись.
                // Для того, что бы усложнить подбор значения, которое мы поместили в подпись используется "соль" - слово или 
                // набор символов.
                // Каждый раз когда от пользователя будут приходить запросы, мы будем повторно генерировать подпись и проверять,
                // что бы она совпадала с полученой.

                Response.Cookies.Add(cookieName);
                Response.Cookies.Add(cookieSign);
                Response.Redirect("Default.aspx");
            }
            else
            {
                ErrorLabel.Visible = true;
            }
        }
    }
}
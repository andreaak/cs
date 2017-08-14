using System;
using System.Web;
using System.Web.Security;

namespace ASPWebFormsTest._25_FormsAuthentication
{
    public partial class _03_Login : System.Web.UI.Page
    {

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            string customData = "HELLO WORLD 1234567890";

            if (LoginTextBox.Text == "admin" && PasswordTextBox.Text == "qwerty")
            {
                // GetAuthCookie - Создает cookies аутентификации для указанного пользователя.
                // true - cookies будут сохраняться между сесиями браузера.
                HttpCookie authCookie = FormsAuthentication.GetAuthCookie(LoginTextBox.Text, true);

                // FormsAuthenticationTicket - позволяет получить доступ к свойствам тикета, который используется 
                // для определения пользователя при FormsAuthenticsation
                // FormsAuthentication.Decrypt(authCookie.Value) - создает объект на основе зашифрованной строки из cookies.
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

                // Создаем новый тикет, последний параметр конструктора пользовательские данные.
                FormsAuthenticationTicket newTicket = new FormsAuthenticationTicket(ticket.Version,
                                                                                    ticket.Name,
                                                                                    ticket.IssueDate,
                                                                                    ticket.Expiration,
                                                                                    ticket.IsPersistent,
                                                                                    customData);
                // Шифруем новый тикет и помещаем его в cookies.
                authCookie.Value = FormsAuthentication.Encrypt(newTicket);
                Response.Cookies.Add(authCookie);

                string redirectUrl = FormsAuthentication.GetRedirectUrl(LoginTextBox.Text, true);
                Response.Redirect(redirectUrl);
            }
            else
            {
                MessageLabel.Text = "Ошибка";
            }
        }
    }
}
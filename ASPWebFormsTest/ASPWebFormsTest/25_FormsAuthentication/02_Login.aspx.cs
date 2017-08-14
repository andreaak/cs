using System;
using System.Web.Security;

namespace ASPWebFormsTest._25_FormsAuthentication
{
    public partial class _02_Login : System.Web.UI.Page
    {

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            MessageLabel.Text = string.Empty;

            if (LoginTextBox.Text == "admin" &&
                PasswordTextBox.Text == "12345")
            {
                FormsAuthentication.RedirectFromLoginPage(LoginTextBox.Text, true);
            }
            else
            {
                MessageLabel.Text = "Логин или пароль введены не правильно";
            }
        }
    }
}
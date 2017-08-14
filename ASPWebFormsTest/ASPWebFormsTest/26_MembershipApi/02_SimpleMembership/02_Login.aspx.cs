using System;
using System.Web.Security;

namespace ASPWebFormsTest._26_MembershipApi._02_SimpleMembership
{
    public partial class _02_Login : System.Web.UI.Page
    {
        protected void LogInButton_Click(object sender, EventArgs e)
        {
            // Проверка наличия пользователя с указанным логином и паролем в базе данных.
            bool isUserValid = Membership.ValidateUser(UserNameTextBox.Text, PasswordTextBox.Text);
            if (isUserValid)
            {
                // Добавление в cookies маркер и перенаправление на предыдущую страницу.
                FormsAuthentication.RedirectFromLoginPage(UserNameTextBox.Text, false);
            }
            else
            {
                ErrorLabel.Visible = true;
            }
        }

    }
}
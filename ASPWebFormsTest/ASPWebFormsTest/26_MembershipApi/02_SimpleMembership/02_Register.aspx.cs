using System;
using System.Drawing;
using System.Web.Security;

namespace ASPWebFormsTest._26_MembershipApi._02_SimpleMembership
{
    public partial class _02_Register : System.Web.UI.Page
    {
        protected void RegisterButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    // Создание нового пользователя
                    Membership.CreateUser(UserNameTextBox.Text, PasswordTextBox.Text, EmailTextBox.Text);
                }
                catch (Exception ex)
                {
                    LabelMessage.Text = ex.Message;
                    LabelMessage.ForeColor = Color.Red;
                    return;
                }

                LabelMessage.Text = "Пользователь успешно зарегистрирован.";
                RegisterButton.Enabled = false;
            }
        }
    }
}

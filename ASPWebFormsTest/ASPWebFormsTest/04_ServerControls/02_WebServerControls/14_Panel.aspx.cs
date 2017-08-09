using System;

namespace ASPWebFormsTest._04_ServerControls._02_WebServerControls
{
    public partial class _14_Panel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonA_Click(object sender, EventArgs e)
        {
            TextBox4.Text = "Нажата кнопка A";
        }

        protected void ButtonB_Click(object sender, EventArgs e)
        {
            TextBox5.Text = "Нажата кнопка B";
        }

        protected void ClearButton_Click(object sender, EventArgs e)
        {
            TextBox4.Text = TextBox5.Text = string.Empty;
        }

        protected void DefaultButton_Click(object sender, EventArgs e)
        {
            TextBox4.Text = TextBox5.Text = "Default";
        }
    }
}
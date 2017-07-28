using System;

namespace ASPWebFormsTest._01_BaseInfo
{
    public partial class _08_Form : System.Web.UI.Page
    {
        private void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string greeting = "Введите имя";
                Label1.Text = greeting;
            }
        }

        protected void OkButton_Click(object sender, EventArgs e)
        {
            string greeting = "Привет, " + TextBox1.Text + "!";
            Label1.Text = greeting;
            Label3.Text = greeting;
        }
    }
}
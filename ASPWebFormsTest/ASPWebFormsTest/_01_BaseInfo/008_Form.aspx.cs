using System;

namespace ASPWebFormsTest._01_BaseInfo
{
    public partial class _008_Form : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void OkButton_Click(object sender, EventArgs e)
        {
            string greeting = "Привет, " + TextBox1.Text + "!";
            Label1.Text = greeting;
        }
    }
}
using System;

namespace ASPWebFormsTest._04_ServerControls._02_WebServerControls
{
    public partial class _05_Label : System.Web.UI.Page
    {
        protected void ButtonA_Click(object sender, EventArgs e)
        {
            TestLabel.Text += "Кнопка A нажата <br />";
        }

        protected void ButtonB_Click(object sender, EventArgs e)
        {
            TestLabel.Text += "Кнопка B нажата <br />";
        }
    }
}
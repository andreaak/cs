using System;

namespace ASPWebFormsTest._04_ServerControls._02_WebServerControls
{
    public partial class _08_RadioButton : System.Web.UI.Page
    {
        protected void Button_Click(object sender, EventArgs e)
        {
            if (RadioButton1.Checked)
            {
                OutputLabel.Text = "YES";
            }
            else if (RadioButton2.Checked)
            {
                OutputLabel.Text = "NO";
            }
        }
    }
}
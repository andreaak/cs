using System;
using System.Web.UI.WebControls;

namespace ASPWebFormsTest._04_ServerControls._02_WebServerControls
{
    public partial class _13_LinkButton : System.Web.UI.Page
    {
        protected void LinkButton_Click(object sender, EventArgs e)
        {
            LinkButton button = sender as LinkButton;
            if (button != null)
            {
                OutputLabel.Text += button.Text + "<br />";
            }
        }
    }
}
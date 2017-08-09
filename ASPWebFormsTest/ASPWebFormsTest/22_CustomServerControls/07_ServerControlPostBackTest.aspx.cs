using System;

namespace ASPWebFormsTest._22_CustomServerControls
{
    public partial class _07_ServerControlPostBackTest : System.Web.UI.Page
    {
        protected void ClickablePanel1_Click(object sender, EventArgs e)
        {
            OutputLabel.Text = DateTime.Now.ToString();
        }
    }
}
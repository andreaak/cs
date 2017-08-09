using System;

namespace ASPWebFormsTest._21_UserControls
{
    public partial class _02_UserControl : System.Web.UI.UserControl
    {
        protected void TestButton_Click(object sender, EventArgs e)
        {
            TestTextBox.Text = "Hello User Control!";
        }
    }
}
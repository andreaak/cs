using System;

namespace ASPWebFormsTest._21_UserControls.Controls
{
    public partial class _03_ControlRegisterInConfig : System.Web.UI.UserControl
    {
        protected void TestButton_Click(object sender, EventArgs e)
        {
            TestTextBox.Text = "Hello User Control!";
        }
    }
}
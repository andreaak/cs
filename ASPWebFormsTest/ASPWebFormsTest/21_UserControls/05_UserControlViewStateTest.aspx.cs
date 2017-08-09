using System;

namespace ASPWebFormsTest._21_UserControls
{
    public partial class _05_UserControlViewStateTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Control1.Text = "Hello world";
                Control2.Text = "Hello world";
            }
        }
    }
}
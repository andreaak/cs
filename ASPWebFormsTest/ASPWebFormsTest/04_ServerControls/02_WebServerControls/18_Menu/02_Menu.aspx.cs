using System;
using System.Web.UI.WebControls;

namespace ASPWebFormsTest._04_ServerControls._02_WebServerControls._18_Menu
{
    public partial class _02_Menu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Unnamed1_MenuItemClick(object sender, MenuEventArgs e)
        {
            Response.Write(e.Item.Text);
        }
    }
}
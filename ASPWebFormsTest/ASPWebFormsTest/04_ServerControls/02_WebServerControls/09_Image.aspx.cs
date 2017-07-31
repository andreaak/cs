using System;

namespace ASPWebFormsTest._04_ServerControls._02_WebServerControls
{
    public partial class _09_Image : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Image1.ImageUrl = "~/Images/logo.jpg";
        }
    }
}
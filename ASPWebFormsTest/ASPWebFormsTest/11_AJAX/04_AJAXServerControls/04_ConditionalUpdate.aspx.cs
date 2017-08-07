using System;

namespace ASPWebFormsTest._11_AJAX._04_AJAXServerControls
{
    public partial class _04_ConditionalUpdate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Text = DateTime.Now.ToLongTimeString();
            Label2.Text = DateTime.Now.ToLongTimeString();
            Label3.Text = DateTime.Now.ToLongTimeString();
        }
    }
}
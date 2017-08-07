using System;

namespace ASPWebFormsTest._11_AJAX._04_AJAXServerControls
{
    public partial class _01_UpdatePanel : System.Web.UI.Page
    {
        protected void Button1_Click(object sender, EventArgs e)
        {
            Label1.Text = DateTime.Now.ToLongTimeString();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Label2.Text = DateTime.Now.ToLongTimeString();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Label3.Text = DateTime.Now.ToLongTimeString();
        }
    }
}
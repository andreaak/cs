using System;
using System.Threading;

namespace ASPWebFormsTest._11_AJAX._04_AJAXServerControls
{
    public partial class _08_UpdateProgress : System.Web.UI.Page
    {
        protected void Button_Click(object sender, EventArgs e)
        {
            Thread.Sleep(5000);
            Label1.Text = "Date: " + DateTime.Now.ToLongTimeString();
        }
    }
}
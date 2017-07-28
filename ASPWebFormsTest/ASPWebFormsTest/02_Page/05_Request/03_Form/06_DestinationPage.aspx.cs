using System;

namespace ASPWebFormsTest._02_Page._05_Request._03_Form
{
    public partial class _06_DestinationPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string ob = Request.Form["TextBox1"] as string;
            string ob2 = Request["TextBox2"] as string;
            if (ob != null)
            {
                //Response.Write("TextBox Value = " + ob);
            }
        }
    }
}
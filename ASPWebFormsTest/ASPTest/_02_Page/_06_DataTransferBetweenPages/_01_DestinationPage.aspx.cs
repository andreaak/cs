using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPTest._02_Page._06_DataTransferBetweenPages
{
    public partial class _01_DestinationPage : System.Web.UI.Page
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPTest._00_Forms._05_PageRedirection
{
    public partial class DestinationPage3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string ob = Request["TextBox1"] as string;
            if (ob != null)
            {
                Response.Write("TextBox Value = " + ob);
            }
        }

        protected void Page_PreRenderComplete(object sender, EventArgs e)
        {
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPTest._00_Forms._05_PageRedirection
{
    public partial class TestForm3 : System.Web.UI.Page
    {
        private void Page_Load(object sender, EventArgs e)
        {
            //form1.Action = "DestinationPage3.aspx";
            if (!IsPostBack)
            {
                TextBox1.BackColor = System.Drawing.Color.Azure;
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
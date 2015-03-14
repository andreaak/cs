using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPTest._02_Page._06_DataTransferBetweenPages
{
    public partial class _01_DataTransferUseAction : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            form1.Action = "_01_DestinationPage.aspx";
        }
    }
}
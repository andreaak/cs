using System;

namespace ASPWebFormsTest._02_Page._06_DataTransferBetweenPages
{
    public partial class _06_DataTransferBetweenPages : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            form1.Action = "06_DestinationPage.aspx";
        }
    }
}
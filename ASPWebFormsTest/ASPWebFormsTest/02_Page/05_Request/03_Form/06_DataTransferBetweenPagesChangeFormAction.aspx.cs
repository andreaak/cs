using System;

namespace ASPWebFormsTest._02_Page._05_Request._03_Form
{
    public partial class _06_DataTransferBetweenPagesChangeFormAction : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            form1.Action = "06_DestinationPage.aspx";
        }
    }
}
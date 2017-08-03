using System;

namespace ASPWebFormsTest._08_DataBinding._09_GridView
{
    public partial class _02_BoundField : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataSource = BrowsersRepository.Browsers;
            GridView1.DataBind();
        }
    }
}
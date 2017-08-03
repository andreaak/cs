using System;
using System.Configuration;

namespace ASPWebFormsTest._30_Examples._02_WebConfigUsing
{
    public partial class AppConfigUsing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Literal1.Text = ConfigurationManager.AppSettings["ProjectVersion"];
        }
    }
}
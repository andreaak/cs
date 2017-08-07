using System;

namespace ASPWebFormsTest._13_Routing
{
    public partial class _04_RoutingAndCultureLogin : System.Web.UI.Page
    {
        protected override void InitializeCulture()
        {
            string lang = Page.RouteData.Values["lang"] as string ?? "auto";

            UICulture = lang;
            Culture = lang;
            base.InitializeCulture();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}
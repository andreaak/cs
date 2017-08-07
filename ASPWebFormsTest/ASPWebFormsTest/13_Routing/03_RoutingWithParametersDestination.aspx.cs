using System;
using System.Drawing;

namespace ASPWebFormsTest._13_Routing
{
    public partial class _03_RoutingWithParametersDestination : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string operation = Page.RouteData.Values["operation"] as string;
                int x = Convert.ToInt32(Page.RouteData.Values["x"]);
                int y = Convert.ToInt32(Page.RouteData.Values["y"]);

                switch (operation)
                {
                    case "sum":
                        Result.Text = (x + y).ToString();
                        break;
                    case "sub":
                        Result.Text = (x - y).ToString();
                        break;
                    case "mul":
                        Result.Text = (x * y).ToString();
                        break;
                    case "div":
                        Result.Text = (x / y).ToString();
                        break;
                }
            }
            catch (Exception)
            {
                Result.Text = "ERROR";
                Result.ForeColor = Color.Red;
            }
        }
    }
}
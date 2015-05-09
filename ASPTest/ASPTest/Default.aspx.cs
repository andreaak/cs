using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPTest
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            writer.Write("TEST");
        }

        public override void RenderControl(HtmlTextWriter writer)
        {
            writer.Write("TEST2");
            base.RenderControl(writer);
        }
    }
}
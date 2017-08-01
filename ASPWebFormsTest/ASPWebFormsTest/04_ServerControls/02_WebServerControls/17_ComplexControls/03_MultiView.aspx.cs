using System;

namespace ASPWebFormsTest._04_ServerControls._02_WebServerControls._17_ComplexControls
{
    public partial class _03_MultiView : System.Web.UI.Page
    {
        protected void Button1_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 2;
        }
    }
}
using System;

namespace ASPWebFormsTest._24_OutputCache
{
    public partial class _08_UserControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MessageLabel.Text = DateTime.Now.ToLongTimeString();
        }
    }
}
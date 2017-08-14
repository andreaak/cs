using System;

namespace ASPWebFormsTest._24_OutputCache
{
    public partial class _07_CacheProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MessageLabel.Text = DateTime.Now.ToLongTimeString();
        }
    }
}
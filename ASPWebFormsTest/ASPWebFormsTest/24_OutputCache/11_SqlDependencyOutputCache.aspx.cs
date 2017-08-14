using System;

namespace ASPWebFormsTest._24_OutputCache
{
    public partial class _11_SqlDependencyOutputCache : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MessageLabel.Text = "Страница была помещена в кэш в " + DateTime.Now.ToLongTimeString();
        }
    }
}
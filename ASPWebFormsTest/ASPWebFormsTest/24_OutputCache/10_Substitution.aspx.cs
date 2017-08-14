using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPWebFormsTest._24_OutputCache
{
    public partial class _10_Substitution : System.Web.UI.Page
    {
        public static string GetDate(HttpContext context)
        {
            // Для получения разметки серверных элементов управления.
            TextBox text = new TextBox();
            text.Text = DateTime.Now.ToLongTimeString();

            StringBuilder builder = new StringBuilder();
            StringWriter writer = new StringWriter(builder);
            HtmlTextWriter htmlWriter = new HtmlTextWriter(writer);

            text.RenderControl(htmlWriter);

            return builder.ToString(); 
        }
    }
}
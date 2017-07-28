using System;

namespace ASPWebFormsTest._02_Page._07_Server
{
    public partial class _03_HtmlEncode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            Заменяет обычную строку, строкой допустимых символов HTML
            < - &lt;
            > - &gt;
            " - &guot;
            & - &amp;
            */
            Label1.Text += Server.HtmlEncode("<b>Hello World!</b>");
            Label1.Text += "<br />";

            //Делает обратную операцию
            Label1.Text += Server.HtmlDecode("&lt;b&gt;Hello World!&lt;/b&gt;");
        }
    }
}
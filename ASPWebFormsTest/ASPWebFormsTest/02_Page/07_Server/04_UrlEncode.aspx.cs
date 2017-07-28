using System;

namespace ASPWebFormsTest._02_Page._07_Server
{
    public partial class _04_UrlEncode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            Заменяет обычную строку, строкой допустимых символов URL
            используется для передачи специфических символов в параметрах строки запроса. Значение параметра - a=b&123
            */
            Label1.Text += "<b>Server.UrlEncode(http://example.com?p=123)</b> = " + Server.UrlEncode("http://example.com?p=123");
            Label1.Text += "<br />";

            //Делает обратную операцию
            Label1.Text += "<b>Server.UrlDecode(http%3a%2f%2fexample.com%3fp%3d123)</b> = " + Server.UrlDecode("http%3a%2f%2fexample.com%3fp%3d123");
        }
    }
}
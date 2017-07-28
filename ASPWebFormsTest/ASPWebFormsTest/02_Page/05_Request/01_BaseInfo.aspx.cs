using System;

namespace ASPWebFormsTest._02_Page._05_Request
{
    public partial class _01_BaseInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Text += "ApplicationPath         = " + Request.ApplicationPath + "<br />";
            Label1.Text += "PhysicalApplicationPath = " + Request.PhysicalApplicationPath + "<br />";
            Label1.Text += "Browser                 = " + Request.Browser.Type + "<br />";
            Label1.Text += "Path                    = " + Request.Path + "<br />";
            Label1.Text += "UserLanguages           = " + Request.UserLanguages[0] + "<br />";
            Label1.Text += "IsLocal                 = " + Request.IsLocal + "<br />";

            Label1.Text += "<br /><br /><br /><br />";

            Label1.Text += "Headers:<br />";

            // Получение значений всех заголовков полученных от браузера.
            foreach (string item in Request.Headers)
            {
                Label1.Text += item + " : " + Request.Headers[item] + "<br />";
            }

            /*
            ApplicationPath = /
            PhysicalApplicationPath = D:\My\cs\ASPWebFormsTest\ASPWebFormsTest\
            Browser = Chrome59
            Path = /02_Page/05_Request/01_BaseInfo.aspx
            UserLanguages = en-US
            IsLocal = True

            Headers:
            Connection : keep-alive
            Accept : text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng; q = 0.8
            Accept - Encoding : gzip, deflate, br
            Accept - Language : en - US,en; q = 0.8
            Host: localhost: 57210
            User - Agent : Mozilla / 5.0(Windows NT 10.0; Win64; x64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 59.0.3071.115 Safari / 537.36
            Upgrade - Insecure - Requests : 1
            */
        }
    }
}
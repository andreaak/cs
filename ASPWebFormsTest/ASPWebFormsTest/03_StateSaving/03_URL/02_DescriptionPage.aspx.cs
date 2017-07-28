using System;
using System.Collections.Generic;

namespace ASPWebFormsTest._03_StateSaving._03_URL
{
    public partial class DescriptionPage : System.Web.UI.Page
    {
        Dictionary<int, string> browsers = new Dictionary<int, string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            browsers.Add(1, "Mozilla Firefox is a free and open source web browser descended from the Mozilla Application Suite and managed by Mozilla Corporation. As of November 2011, Firefox is the second or third most widely used browser, according to different estimates, with approximately 25% of worldwide usage share of web browsers.");
            browsers.Add(2, "Google Chrome is a web browser developed by Google that uses the WebKit layout engine. It was first released as a beta version for Microsoft Windows on September 2, 2008, and the public stable release was on December 11, 2008. The name is derived from the graphical user interface frame, or 'chrome', of web browsers.");
            browsers.Add(3, "Windows Internet Explorer (formerly Microsoft Internet Explorer, commonly abbreviated IE or MSIE) is a series of graphical web browsers developed by Microsoft and included as part of the Microsoft Windows line of operating systems, starting in 1995.");
            browsers.Add(4, "Opera is a web browser and Internet suite developed by Opera Software with over 200 million users worldwide.[4] The browser handles common Internet-related tasks such as displaying web sites, sending and receiving e-mail messages, managing contacts, chatting on IRC, downloading files via BitTorrent, and reading web feeds. Opera is offered free of charge for personal computers and mobile phones.");
            browsers.Add(5, "Safari is a web browser developed by Apple Inc. and included with the Mac OS X and iOS operating systems. First released as a public beta on January 7, 2003[3] on the company's Mac OS X operating system, it became Apple's default browser beginning with Mac OS X v10.3 'Panther'.");

            try
            {
                string temp = Request.QueryString["id"];
                int browserId = Convert.ToInt32(temp);
                DescriptionLabel.Text = browsers[browserId];
            }
            catch
            {
                DescriptionLabel.Text = "ERROR!";
            }
            
        }
    }
}
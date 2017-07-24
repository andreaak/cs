using System;

namespace ASPWebFormsTest._01_BaseInfo
{
    public partial class _004_ClassUsing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Person p1 = new Person("admin", "admin@example.com");
            Output.Text = p1.GenerateHtml();
        }
    }
}
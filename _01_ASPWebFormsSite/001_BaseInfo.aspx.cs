using System;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Output.Text = "Hello world! Time on server " + DateTime.Now.ToLongTimeString();
    }
}
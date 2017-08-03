using System;

namespace ASPWebFormsTest._08_DataBinding._08_Repeater
{
    public partial class _02_RepeaterBindObjects : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Repeater1.DataSource = CustomersRepository.Customers;
            Repeater1.DataBind();
        }
    }
}
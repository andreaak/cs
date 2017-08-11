using System;
using System.Collections.Generic;

namespace ASPWebFormsTest._23_CachingData
{
    public partial class _07_CacheFromCodeTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("<h2>Customers</h2>");
            List<string> customers = _07_CacheFromCode.Customers;
            foreach (string customer in customers)
            {
                Response.Write(customer + "<br />");
            }

            Response.Write("<hr />");
            Response.Write("<h2>Countries</h2>");

            List<string> countries = _07_CacheFromCode.Countries;
            foreach (string country in countries)
            {
                Response.Write(country + "<br />");
            }
        }
    }
}
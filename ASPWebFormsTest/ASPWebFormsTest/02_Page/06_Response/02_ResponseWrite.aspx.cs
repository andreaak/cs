using System;

namespace ASPWebFormsTest._02_Page._06_Response
{
    public partial class _02_ResponseWrite : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // откройте в браузере исходный код странмцы и сравните куда в разметку попала
            // строка "Hello ASP.NET (from code file)" и куда строка "Hello ASP.NET"
            Response.Write("Hello ASP.NET (from code file)");
        }
    }
}
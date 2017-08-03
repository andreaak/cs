using System;

namespace ASPWebFormsTest._09_Validation
{
    public partial class _05_RegularExpressionValidator : System.Web.UI.Page
    {
        protected void ButtonOk_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Response.Write("Сработал обработчик в коде страницы.");
            }
        }
    }
}
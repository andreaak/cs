using System;

namespace ASPWebFormsTest._09_Validation
{
    public partial class _02_RequiredFildValidator : System.Web.UI.Page
    {
        protected void OkButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Response.Write("Сработал обработчик в коде страницы.");
            }
        }
    }
}
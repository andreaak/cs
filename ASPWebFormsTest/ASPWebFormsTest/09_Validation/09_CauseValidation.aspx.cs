using System;

namespace ASPWebFormsTest._09_Validation
{
    public partial class _09_CauseValidation : System.Web.UI.Page
    {
        protected void OkButton_Click(object sender, EventArgs e)
        {
            Validate();
            if (Page.IsValid)
            {
                Response.Write("Страница валидна");
            }
        }
    }
}
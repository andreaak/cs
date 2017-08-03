using System;

namespace ASPWebFormsTest._09_Validation
{
    public partial class _04_CompareValidator : System.Web.UI.Page
    {
        protected void ButtonOk_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Response.Write("Пароли совпали.");
            }
        }
    }
}
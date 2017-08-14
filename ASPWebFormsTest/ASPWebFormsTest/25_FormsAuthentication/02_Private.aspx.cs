using System;
using System.Web.Security;

namespace ASPWebFormsTest._25_FormsAuthentication
{
    public partial class _02_Private : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // При отсутствии маркера запрос считается не аутентифицированным.
            if (!Request.IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
            }
        }
    }
}
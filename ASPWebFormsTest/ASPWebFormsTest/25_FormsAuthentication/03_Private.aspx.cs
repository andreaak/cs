using System;
using System.Web.Security;

namespace ASPWebFormsTest._25_FormsAuthentication
{
    public partial class _03_Private : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                // FormsIdentity - представляет пользователя аутентифицированного через FormsAuthentication.
                // Page.User - информация о пользователе, который делает данный запрос.
                FormsIdentity identity = User.Identity as FormsIdentity;

                if (identity != null)
                {
                    FormsAuthenticationTicket ticket = identity.Ticket;
                    string customData = ticket.UserData;
                    LabelMessage.Text = customData;
                }
            }
            else
            {
                FormsAuthentication.RedirectToLoginPage();
            }
        }
    }
}
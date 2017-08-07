using System;

namespace ASPWebFormsTest._11_AJAX._04_AJAXServerControls
{
    public partial class _02_UpdatePanelNotHandledError : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                throw new ApplicationException("Обновление страницы потерпело неудачу!");
            }
        }
    }
}
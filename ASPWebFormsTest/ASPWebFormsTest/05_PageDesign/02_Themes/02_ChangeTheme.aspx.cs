using System;

namespace ASPWebFormsTest._05_PageDesign._02_Themes
{
    public partial class _02_ChangeTheme : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Сменить тему программно можно только на событие PreInit
            // В директиве Page Theme="Spring"
            Page.Theme = "Winter";
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}
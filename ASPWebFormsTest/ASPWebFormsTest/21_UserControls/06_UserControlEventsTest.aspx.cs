using System;

namespace ASPWebFormsTest._21_UserControls
{
    public partial class _06_UserControlEventsTest : System.Web.UI.Page
    {
        protected void Calculator1_OnError(object sender, EventArgs e)
        {
            ErrorLabel.Text = "Ошибка на странице!";
        }
    }
}
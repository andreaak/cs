namespace ASPWebFormsTest._21_UserControls
{
    public partial class _07_UserControlEventArgsTest : System.Web.UI.Page
    {
        protected void Calculator1_OnError(object sender, _07_EventArgs e)
        {
            ErrorLabel.Text = "Ошибка на странице! " + e.ErrorMessage;
        }
    }
}
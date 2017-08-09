using System;

namespace ASPWebFormsTest._21_UserControls
{
    public partial class _05_UserControlSetProperty : System.Web.UI.UserControl
    {
        public string Text { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            OutputLiteral.Text = Text;
        }
    }
}
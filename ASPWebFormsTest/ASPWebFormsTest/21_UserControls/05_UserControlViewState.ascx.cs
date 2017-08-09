using System;

namespace ASPWebFormsTest._21_UserControls
{
    public partial class _05_UserControlViewState : System.Web.UI.UserControl
    {
        private const string TextKey = "text_val";

        public string Text
        {
            set { ViewState[TextKey] = value; }
            get
            {
                object data = ViewState[TextKey];
                if (data == null) 
                { 
                    return string.Empty; 
                }
                else
                {
                    return data.ToString();
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            OutputLiteral.Text = Text;
        }
    }
}
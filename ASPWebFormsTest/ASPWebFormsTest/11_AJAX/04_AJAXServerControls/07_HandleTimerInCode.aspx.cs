using System;

namespace ASPWebFormsTest._11_AJAX._04_AJAXServerControls
{
    public partial class _07_HandleTimerInCode : System.Web.UI.Page
    {
        public int Counter
        {
            get 
            {
                object val = ViewState["Counter"];
                if (val != null)
                {
                    return (int)val;
                }
                ViewState["Counter"] = 0;
                return 0;
            }
            set { ViewState["Counter"] = value; }
        }

        protected void Timer_Tick(object sender, EventArgs e)
        {
            Counter++;
            Label1.Text = Counter.ToString();
        }
    }
}
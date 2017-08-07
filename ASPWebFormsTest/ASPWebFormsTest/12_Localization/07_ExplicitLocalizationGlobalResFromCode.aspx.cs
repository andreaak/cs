using System;

namespace ASPWebFormsTest._12_Localization
{
    public partial class _07_ExplicitLocalizationGlobalResFromCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UICulture = "ru-RU";
            //UICulture = "en-US";
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo("ru-RU");
            
            ButtonLogin.Text = Resources.GlobalRes.ButtonEnter;
            LabelLogin.Text = Resources.GlobalRes.LoginText;
            LabelPassword.Text = Resources.GlobalRes.PassText;


            Label1.Text = Convert.ToString(GetGlobalResourceObject("GlobalRes", "ResValue"));
            Label2.Text = Convert.ToString(GetLocalResourceObject("LocalresValue"));
        }
    }
}
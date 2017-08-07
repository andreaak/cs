namespace ASPWebFormsTest._12_Localization
{
    public partial class _03_ImplicitLocalizationChangeLanguage : System.Web.UI.Page
    {
        /*
        Неявная локализация с помощью локальных файлов
        Метод InitializeCulture в базовом классе не содержит логики, 
        вызывается на ранних этапах жизненного цикла страницы (до события PreInit), когда еще не созданы элементы управления.
        */
        protected override void InitializeCulture()
        {
            if (Request.Form["LanguageList"] != null)
            {
                string language = Request.Form["LanguageList"];
                
                UICulture = language;
                Culture = language;
                
                //Thread.CurrentThread.CurrentCulture = new CultureInfo(language);
                //Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(language);
            }
        }
    }
}
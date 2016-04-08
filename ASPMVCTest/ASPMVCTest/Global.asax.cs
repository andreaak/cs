using _01_ASPMVCTest.Areas._003_Model.Models;
using _01_ASPMVCTest.ModelBinder;
using _01_ASPMVCTest.Providers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace _01_ASPMVCTest
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class ButtonFactory : System.Web.HttpApplication
    {
        // Обработчик события старта приложения.
        protected void Application_Start()
        {
            // Регистрация областей. Поиск объектов типа производного от AreaRegistration и вызов методов RegisterArea для регистрации маршрутов каждой области.
            AreaRegistration.RegisterAllAreas();
            
            //регистрация пользовательской ValueProviderFactory
            ValueProviderFactories.Factories.Add(new CookiesValueProviderFactory());

            //регистрация пользовательского ModelBinder
            ModelBinders.Binders.Add(typeof(MyModel2), new MyModelBinder());

            // Регистрации маршрутов для работы WebAPI.
            WebApiConfig.Register(GlobalConfiguration.Configuration);

            // Регистрация глобальных фильтров
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            // Регистрация маршрутов.
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // Регистрация bundle для оптимизации загрузки CSS и JavaScript файлов.
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        public Button CreateButton()
        {
            Button button;
            switch (UserSettings.UserSkinType)
            {
                case UserSkinTypes.Normal:
                    button = new Button();
                    break;
                case UserSkinTypes.Fancy:
                    button = new FancyButton();
                    break;
                case UserSkinTypes.Gothic:
                    button = new VampireButton();
            }
            return button;
        }

        private class Button
        {
            Button button = ButtonFactory.CreateButton();
        }

        private class FancyButton : Button
        {

        }

        private class VampireButton : Button
        {

        }

        private class ICreditCard
        {

        }

        private class MasterCard : ICreditCard
        {
            private void Test()
            {
        ICreditCard creditCard = new MasterCard();
        Shopper shopper = new Shopper();
        shopper.CreditCard = creditCard;
            }
        }

        
        public class Shopper
        {
            public ICreditCard CreditCard
            {
                get;
                set;
            }

            private ICreditCard creditCard;
            public void Inject(ICreditCard creditCard)
            {
                this.creditCard = creditCard;
            }
        }

    }
}
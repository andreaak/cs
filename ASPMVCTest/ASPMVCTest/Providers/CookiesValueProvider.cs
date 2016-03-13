using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _01_ASPMVCTest.Providers
{
    // IValueProvider - класс (поставщик значений), который отвечает за поиск данных в запросе и инициализацию значений в методе действия.
    // Системные классы реализующие данный интерфейс:
    // 1) FormValueProvider
    // 2) RouteDataValueProvider
    // 3) QueryStringValueProvider
    // 4) HttpFileCollectionValueProvider

    public class CookiesValueProvider : IValueProvider
    {
        // Проверка наличия указанного ключа в запросе.
        public bool ContainsPrefix(string prefix)
        {
            return HttpContext.Current.Request.Cookies.AllKeys.Contains(prefix);
        }

        // Получение значения по ключу из запроса.
        public ValueProviderResult GetValue(string key)
        {
            HttpCookieCollection cookies = HttpContext.Current.Request.Cookies;
            return ContainsPrefix(key) ?
                new ValueProviderResult(cookies[key].Value, cookies[key].Value, CultureInfo.CurrentCulture)
                : null;
        }
    }

    // Фабрика поставщиков данных, ответсвенная за создание экземпляра поставщика данных.
    // Данная фабрика регистрируется при старте приложения в файле Global.asax
    public class CookiesValueProviderFactory : ValueProviderFactory
    {
        public override IValueProvider GetValueProvider(ControllerContext controllerContext)
        {
            return new CookiesValueProvider();
        }
    }
}
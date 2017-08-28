using System.Web.Mvc;
using _01_ASPMVCTest.Areas._05_Filters.Filters;

namespace _01_ASPMVCTest
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute()); // Глобальный системный фильтр для обработки исключений
            //filters.Add(new F_05_MessageAttribute("GLOBAL")); // Глобальный пользовательский фильтр
        }
    }
}
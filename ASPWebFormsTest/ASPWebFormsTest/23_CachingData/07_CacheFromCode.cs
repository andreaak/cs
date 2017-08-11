using System.Collections.Generic;
using System.Threading;
using System.Web;

namespace ASPWebFormsTest._23_CachingData
{
    // Названия для ключей в кэше.
    public struct Fields
    {
        public static readonly string Customers = "customers";
        public static readonly string Countries = "countries";
    }

    // Класс-обертка для доступа к данным в кэше
    public class _07_CacheFromCode
    {
        public static List<string> Customers
        {
            get 
            {
                // Чтение значения из кэша.
                object data = HttpContext.Current.Cache[Fields.Customers];
                if (data == null)
                {
                    // Если значения нет в кэше записываем его туда.
                    data = LoadCustomers();
                    HttpContext.Current.Cache[Fields.Customers] = data;
                }
                return (List<string>)data;
            }
        }

        public static List<string> Countries
        {
            get 
            {
                object data = HttpContext.Current.Cache[Fields.Countries];
                if (data == null)
                {
                    data = LoadCountries();
                    HttpContext.Current.Cache[Fields.Countries] = data;
                }
                return (List<string>)data;
            }
        }

        private static List<string> LoadCustomers()
        {
            Thread.Sleep(3000);
            // Загрузка данных из базы данных.
            List<string> customers = new List<string>() { "Customer1", "Customer2", "Customer3" };
            return customers;
        }

        private static List<string> LoadCountries()
        {
            Thread.Sleep(3000);
            // Загрузка данных из базы данных.
            List<string> countries = new List<string>() {"Ukraine", "USA", "Russia" };
            return countries;
        }
    }
}
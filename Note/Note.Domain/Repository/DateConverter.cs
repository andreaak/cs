using System;
using System.Globalization;

namespace Note.Domain.Repository
{
    public static class DateConverter
    {
        private static CultureInfo culture = new CultureInfo("en-US");
   
        public static DateTime Convert(string date)
        {
            return string.IsNullOrEmpty(date) ? DateTime.MinValue : DateTime.Parse(date, culture);
        }

        public static string GetCurrentDate()
        {
            return DateTime.Now.ToString(culture);
        }
    }
}

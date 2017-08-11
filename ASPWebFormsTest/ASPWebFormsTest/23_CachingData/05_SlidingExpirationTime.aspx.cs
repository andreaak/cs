using System;
using System.Web.Caching;

namespace ASPWebFormsTest._23_CachingData
{
    public partial class _05_SlidingExpirationTime : System.Web.UI.Page
    {
        protected void AddButton_Click(object sender, EventArgs e)
        {
            Cache.Insert("MyData",              // ключ.
                DateTime.Now,                   // данные для кэширования.
                null,                           // зависимость не указана.
                Cache.NoAbsoluteExpiration,     // абсолютное время жизни не указанно
                TimeSpan.FromSeconds(5));       // скользящее время устаревания 5 секунд. 

                // Скользящее время устаревания - если в течении 5 секунд к записи будет обращение время жизни элемента 
                // станет равное 5 секундам. В случае 5 секунд простоя, запись удаляется.
        }

        protected void ReadButton_Click(object sender, EventArgs e)
        {
            // Чтение данных из кэша.
            object data = Cache.Get("MyData");

            if (data != null)
            {
                string message = string.Format("Data: {0}, Type: {1}", data, data.GetType());
                MessageLabel.Text = message;
            }
            else
            {
                MessageLabel.Text = "Cache is empty.";
            }
        }

        protected void RemoveButton_Click(object sender, EventArgs e)
        {
            // Удаление объекта из кэша.
            object data = Cache.Remove("MyData");
            if (data != null)
            {
                MessageLabel.Text = "Removed -- " + data.GetType().ToString();
            }
            else
            {
                MessageLabel.Text = "Cache is empty.";
            }
        }
    }
}
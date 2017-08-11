using System;
using System.IO;
using System.Web.Caching;

namespace ASPWebFormsTest._23_CachingData
{
    public partial class _06_CallBackFunction : System.Web.UI.Page
    {
        protected void AddButton_Click(object sender, EventArgs e)
        {
            AddFileDataToCache("06_CallBackFunction.txt");
        }

        // Метод для чтения и кэширования данных из файла.
        private void AddFileDataToCache(string filename)
        {
            string filePath = Server.MapPath(filename);
            StreamReader reader = new StreamReader(filePath);
            string buffer = reader.ReadToEnd();
            reader.Close();

            CreateAndCache(buffer, filePath);
        }

        // Метод добавляющий данные в кэш и создающий зависимость между данными и файлом.
        private void CreateAndCache(string buffer, string filePath)
        {
            /*
            Callback метод будет срабатывать в том случае если запись будет удалена из кэша.
            Возможные причины: 
            DependencyChanged - Изменен объект от которого зависит данный объект.
            Underused - Элемент удален для освобождения памяти.
            Removed - Удален из кэша программным способом ( с помощью метода Remove)
            Expired - Элемент устарел.
            */
            CacheItemRemovedCallback callback = new CacheItemRemovedCallback(ReloadFile);
            CacheDependency dependency = new CacheDependency(filePath);
            Cache.Insert("MyData",              // Ключ
                buffer,                         // Значение
                dependency,                     // Объект зависимости.
                Cache.NoAbsoluteExpiration,     // Абсолютное время устаревания (NoAbsoluteExpiration объект в кэшк никогда не устареет).
                Cache.NoSlidingExpiration,      // Скользящее время устаревания.
                CacheItemPriority.Normal,       // Приоритет для объекта в кэше.
                callback);                      // Метод обратного вызова, который сработает, когда запись будет удалена.
        }

        private void ReloadFile(string key, object value, CacheItemRemovedReason reason)
        {
            if (reason == CacheItemRemovedReason.DependencyChanged)
            {
                if (key == "MyData")
                {
                    AddFileDataToCache("06_CallBackFunction.txt");
                }
            }
        }

        protected void ReadButton_Click(object sender, EventArgs e)
        {
            // Чтение данных из кэша.
            object data = Cache.Get("MyData");
            if (data != null)
            {
                string message = string.Format("<b>DATA</b>: {0}, <b>TYPE</b>: {1}", data, data.GetType());
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
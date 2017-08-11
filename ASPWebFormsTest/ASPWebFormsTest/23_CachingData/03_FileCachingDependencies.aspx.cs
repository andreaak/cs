using System;
using System.IO;
using System.Web.Caching;

namespace ASPWebFormsTest._23_CachingData
{
    public partial class _03_FileCachingDependencies : System.Web.UI.Page
    {
        protected void AddButton_Click(object sender, EventArgs e)
        {
            string file = Server.MapPath("03_FileCachingDependencies.txt");
            Cache.Insert("MyData",              // ключ.
                File.ReadAllText(file),         // данные для кэширования.
                new CacheDependency(file));     // зависимость указывающая, что запись в кэше удаляется при изменении файла.
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
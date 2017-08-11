using System;
using System.IO;
using System.Web.Caching;

namespace ASPWebFormsTest._23_CachingData
{
    public partial class _08_AggregateCacheDependency : System.Web.UI.Page
    {
        protected void Button_Click(object sender, EventArgs e)
        {
            // Чтение данных из файла.
            string filePath1 = Server.MapPath("08_AggregateCacheDependency1.txt");
            string filePath2 = Server.MapPath("08_AggregateCacheDependency2.txt");

            StreamReader reader = new StreamReader(filePath1);
            string content = reader.ReadToEnd();
            reader.Close();

            reader = new StreamReader(filePath2);
            content += reader.ReadToEnd();
            reader.Close();

            /* Добавление элемента в кэш. 
            Элемент будет удален в случае если файл file1.txt или file2.txt будут изменены.*/
            CacheDependency dependancy1 = new CacheDependency(filePath1);
            CacheDependency dependancy2 = new CacheDependency(filePath2);
            AggregateCacheDependency dependancyAggregate = new AggregateCacheDependency();
            dependancyAggregate.Add(dependancy1, dependancy2);

            Cache.Insert("MyData1", content, dependancyAggregate);

            // Запись MyData2 будет удалена автоматически если измениться запись MyData1
            CacheDependency dependency3 = new CacheDependency(null, new string[] { "MyData1" });
            Cache.Insert("MyData2", "Hello world", dependency3);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            object data1 = Cache["MyData1"];
            object data2 = Cache["MyData2"];
            MessageLabel.Text = "Data in cache -- " + Convert.ToString(data1) + " " + Convert.ToString(data2);
        }
    }
}
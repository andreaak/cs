using System;

namespace ASPWebFormsTest._23_CachingData
{
    public partial class _10_CastomDependency : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            object data = Cache["XmlFile"];
            MessageLabel.Text = "Data from cache -- " + (data != null ? data.ToString() : "No data in cache.");
        }

        protected void Button_Click(object sender, EventArgs e)
        {
            string file = Server.MapPath("10_CastomDependency.xml");
            // Путь к данным в XML документе.
            string xPathExpression = "TestData/Person/FirstName";
            // Создание объекта пользовательской зависимости.
            _10_XmlDataCacheDependency dependency = new _10_XmlDataCacheDependency(file, xPathExpression, 1);
            Cache.Insert("XmlFile", dependency.CurrentValue, dependency);
        }
    }
}
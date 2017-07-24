using System;

namespace ASPWebFormsTest._01_BaseInfo
{
    public partial class _01_BaseInfo : System.Web.UI.Page
    {
        // Page_Load - метод обработчик события Load. Событие срабатывает при каждй загрузке страницы.
        protected void Page_Load(object sender, EventArgs e)
        {
            // Обновление текста в серверном элементе управления
            Output.Text = "Hello world! Time on server " + DateTime.Now.ToLongTimeString();
        }
    }
}
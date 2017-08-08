using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace ASPWebFormsTest._16_Handlers
{
    public class _05_FileExtensionHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            // получение физического пути к запрашиваемому файлу
            string filePath = context.Request.PhysicalPath;

            // Открываем файл на чтение.
            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                // Загружаем контент файла.
                XDocument document = XDocument.Load(stream);

                // Выбираем элементы Users и извлекаем вложенные элементы FirstName и LastName.
                var allUsers = from user in document.Descendants("User")
                               select new {
                                   FirstName = user.Element("FirstName").Value,
                                   LastName = user.Element("LastName").Value
                               };

                // На основе полученных данных строим таблицу.
                context.Response.Write("<html><body><table border='1'>");
                foreach (var user in allUsers)
                {
                    context.Response.Write("<tr>");
                    context.Response.Write("<td>" + user.FirstName + "</td>");
                    context.Response.Write("<td>" + user.LastName + "</td>");
                    context.Response.Write("</tr>");
                }
                context.Response.Write("</table></body></html>");
            }
        }
    }
}

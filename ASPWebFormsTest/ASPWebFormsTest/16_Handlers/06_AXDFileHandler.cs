using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;

namespace ASPWebFormsTest._16_Handlers
{
    /// <summary>
    /// Этот обработчик будет срабатывать, когда производиться запрос к файлу view.axd. (самого axd файла может и не быть в каталоге).
    /// При этом, обработчик возвращает html страницу, со всеми изображениями, которые находятся в каталоге, где расположен axd файл.
    /// 
    /// В ASP.NET файлы c расширением axd обычно используют для конечных точек, указывающих на определенные сервисы. Например trace.axd - для просмотра данных по трассировке.
    /// </summary>
    public class _06_AXDFileHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Write("<html><head><title>Picture Viewer</title></head>");
            context.Response.Write("<body>");
            context.Response.Write(GetPageConent(context));
            context.Response.Write("</body></html>");
        }

        private string GetPageConent(HttpContext context)
        {
            // Получение физического пути к запрашиваемому файлу.
            string path = context.Request.PhysicalPath;

            // Получаем список всех файлов по запрашиваемому пути.
            List<FileInfo> files = GetAllImages(path);

            StringBuilder htmlBuilder = new StringBuilder();

            // Формирование списка изображений.
            foreach (FileInfo file in files)
            {
                htmlBuilder.AppendFormat("<img src='{0}' alt='this is image' /><br /><br />", file.Name);
            }

            return htmlBuilder.ToString();
        }

        private List<FileInfo> GetAllImages(string path)
        {
            // Список найденных файлов.
            List<FileInfo> images = new List<FileInfo>();

            // Список допустимых расширений файлов.
            List<string> extensions = new List<string>() { "*.bmp", "*.jpg", "*.png", "*.gif" };

            // Убираем имя файла из пути.
            string folderPath = path.Replace("\\view.axd", string.Empty);
            DirectoryInfo dir = new DirectoryInfo(folderPath);

            foreach (string ext in extensions)
            {
                FileInfo[] files = dir.GetFiles(ext);
                if (files.Length > 0)
                {
                    images.AddRange(files);
                }
            }

            return images;
        }
    }
}

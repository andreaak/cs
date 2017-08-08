using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI;

namespace ASPWebFormsTest._20_AsyncPages
{
    public partial class _07_PageAsyncTaskAsyncAwait : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Trace.Write("До регистрации задачи " + Thread.CurrentThread.ManagedThreadId);
            
            PageAsyncTask task = new PageAsyncTask(GetDataAsync);
            RegisterAsyncTask(task);

            Trace.Write("После регистрации задачи " + Thread.CurrentThread.ManagedThreadId);
        }

        public async Task GetDataAsync()
        {
            Trace.Write("До вызова await " + Thread.CurrentThread.ManagedThreadId);
            WebRequest request = WebRequest.Create("http://msdn.microsoft.com/");

            // создание нового потока в котором будет выполнятся отправка запроса на msdn
            // при этом поток из пула IIS будет возвращен и дальнейшая обработка страницы продолжится в новом потоке
            WebResponse response = await request.GetResponseAsync();

            string html = string.Empty;

            Stream stream = response.GetResponseStream();
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }

            Regex regex = new Regex("href=\"(\\S*)\"");
            MatchCollection collection = regex.Matches(html);

            StringBuilder builder = new StringBuilder();
            foreach (Match match in collection)
            {
                builder.Append(match.Groups[1]);
                builder.Append("<br />");
            }

            OutputLabel.Text = builder.ToString();

            Trace.Write("После вызова await " + Thread.CurrentThread.ManagedThreadId);
        }
    }
}
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;

namespace ASPWebFormsTest._20_AsyncPages
{
    public partial class _02_AsyncMethods : System.Web.UI.Page
    {
        private WebRequest _request;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Регистрация методов обработчиков начала и завершения асинхронной операции.
            AddOnPreRenderCompleteAsync(new BeginEventHandler(BeginAsyncOperation),
                new EndEventHandler(EndAsyncOperation));

            Trace.Write("Thread = " + Thread.CurrentThread.ManagedThreadId, "-> AddOnPreRenderCompleteAsync in Page_Load");
        }

        private IAsyncResult BeginAsyncOperation(object sender, EventArgs e, AsyncCallback cb, object state)
        {
            // Вывод ID потока, который начал выполнение операции. ID потока, который завершит асинхронную операцию будет отличатся.
            Trace.Write("Thread = " + Thread.CurrentThread.ManagedThreadId, "-> BeginAsyncOperation");

            _request = WebRequest.Create("http://msdn.microsoft.com/");
            return _request.BeginGetResponse(cb, null); // Асинхронный запрос к удаленному ресурсу. 
        }

        private void EndAsyncOperation(IAsyncResult ar)
        {
            string text;
            using (WebResponse response = _request.EndGetResponse(ar))
            {
                Stream stream = response.GetResponseStream();
                using (StreamReader reader = new StreamReader(stream))
                {
                    text = reader.ReadToEnd();
                }
            }

            Regex regex = new Regex("href=\"(\\S*)\"");
            MatchCollection collection = regex.Matches(text);

            StringBuilder builder = new StringBuilder();
            foreach (Match match in collection)
            {
                builder.Append(match.Groups[1]);
                builder.Append("<br />");
            }

            Output.Text += builder.ToString();

            // Вывод ID потока завершившего выполнение асинхронной страницы.
            Trace.Write("Thread = " + Thread.CurrentThread.ManagedThreadId, "-> EndAsyncOperation");
        }

        protected void Page_PreRenderComplete(object sender, EventArgs e)
        {
            Trace.Write("Thread = " + Thread.CurrentThread.ManagedThreadId, "-> PreRenderComplete");
        }
    }
}
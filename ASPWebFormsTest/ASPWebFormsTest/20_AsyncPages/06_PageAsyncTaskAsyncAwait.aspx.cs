using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI;

namespace ASPWebFormsTest._20_AsyncPages
{
    public partial class _06_PageAsyncTaskAsyncAwait : System.Web.UI.Page
    {
        // Метод работает в потоке из пула потоков IIS
        protected void Page_Load(object sender, EventArgs e)
        {
            Trace.Write("До регистрации задачи в Page_Load " + Thread.CurrentThread.ManagedThreadId);

            PageAsyncTask task = new PageAsyncTask(GetDataAsync);
            RegisterAsyncTask(task);

            Trace.Write("После регистрации задачи в Page_Load " + Thread.CurrentThread.ManagedThreadId);
        }

        // Метод работает в потоке из пула потоков IIS
        protected void Page_PreRender(object sender, EventArgs e)
        {
            Trace.Write("Page_PreRender " + Thread.CurrentThread.ManagedThreadId);
        }

        // Данный код будет преобразован компилятором
        // до ключевого слова await инструкции выполняет поток, который выполнял предыдущие операции
        // после ключевого слова await поток из пула потоков приложения.
        // Остальная часть жизненного цикла страницы будет выполнятся в новом потоке
        public async Task GetDataAsync()
        {
            Trace.Write("До вызова await Task.Delay(2000) " + Thread.CurrentThread.ManagedThreadId);

            await Task.Delay(2000);
            OutputLabel.Text = "Данные полученные от удаленного сервиса";

            Trace.Write("После вызова await Task.Delay(2000) " + Thread.CurrentThread.ManagedThreadId);
        }

        protected void Page_PreRenderComplete(object sender, EventArgs e)
        {
            Trace.Write("Page_PreRenderComplete " + Thread.CurrentThread.ManagedThreadId);
        }
    }
}
using System;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace ASPWebFormsTest._20_AsyncPages
{
    public partial class _03_Delegates : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Trace.Write("Page_Load " + Thread.CurrentThread.ManagedThreadId);

            // Регистрация асинхронной операции.
            AddOnPreRenderCompleteAsync(BeginAsyncOperation, EndAsyncOperation);
        }

        // Начало асинхронной операции.
        private IAsyncResult BeginAsyncOperation(object sender, EventArgs e, AsyncCallback cb, object extraData)
        {
            Trace.Write("BeginAsyncOperation " + Thread.CurrentThread.ManagedThreadId);
            Func<int, int> del = new Func<int, int>(DoSomeWork);
            return del.BeginInvoke(8, cb, null);
        }

        // Завершение асинхронной операции.
        private void EndAsyncOperation(IAsyncResult ar)
        {
            Trace.Write("EndAsyncOperation " + Thread.CurrentThread.ManagedThreadId);
            AsyncResult arObject = ar as AsyncResult;

            // Получение экземпляра делегата на котором производился асинхронный вызов.
            Func<int, int> del = (Func<int, int>)arObject.AsyncDelegate;

            // Получение результата работы асинхронного метода.
            int result = del.EndInvoke(ar);
            Output.Text = result.ToString();
        }

        // Задача, которая выполняется другим потоком (не из пула потоков приложения)
        private int DoSomeWork(int arg)
        {
            Trace.Write("DoSomeWork " + Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(2000);
            return arg + 2;
        }

        protected void Page_PreRenderComplete(object sender, EventArgs e)
        {
            Trace.Write("PreRenderComplete " + Thread.CurrentThread.ManagedThreadId);
        }
    }
}

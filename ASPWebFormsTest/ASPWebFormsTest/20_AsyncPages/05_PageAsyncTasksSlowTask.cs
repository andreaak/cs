using System;
using System.Threading;

namespace ASPWebFormsTest._20_AsyncPages
{
    public class _05_PageAsyncTasksSlowTask
    {
        private delegate void AsyncDelegate();

        AsyncDelegate _delegate;
        string _data;

        // Метод по сигнатуре подходит под делегат BeginEventHandler
        public IAsyncResult OnBegin(object sender, EventArgs e, AsyncCallback cb, object extraData)
        {
            _delegate = ExecuteAsyncTask;
            return _delegate.BeginInvoke(cb, extraData);
        }

        // Метод, который будет выполнятся в отдельном потоке.
        private void ExecuteAsyncTask()
        {
            _data += "Задача запущена " + DateTime.Now + " ";
            Thread.Sleep(2000);
        }

        // Метод по сигнатуре подходит под делегат EndEventHandler
        public void OnEnd(IAsyncResult ar)
        {
            _delegate.EndInvoke(ar);
            _data += "Задача завершена " + DateTime.Now + " ";
        }

        // Метод по сигнатуре подходит под делегат EndEventHandler
        public void OnTimeOut(IAsyncResult ar)
        {
            _data = "Вышло время ожидания завершения задачи";
        }

        // Метод для получения результата.
        public string GetData()
        {
            return _data;
        }
    }
}
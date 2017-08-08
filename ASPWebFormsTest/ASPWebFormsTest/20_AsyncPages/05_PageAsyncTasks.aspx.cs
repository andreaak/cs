using System;
using System.Web.UI;

namespace ASPWebFormsTest._20_AsyncPages
{
    public partial class _05_PageAsyncTasks : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            _05_PageAsyncTasksSlowTask slowTask1 = new _05_PageAsyncTasksSlowTask();
            _05_PageAsyncTasksSlowTask slowTask2 = new _05_PageAsyncTasksSlowTask();
            _05_PageAsyncTasksSlowTask slowTask3 = new _05_PageAsyncTasksSlowTask();

            // Последний параметр:
            // true - задачи выполняются параллельно.
            // false - задачи выполняются последовательно.
            PageAsyncTask task1 = new PageAsyncTask(slowTask1.OnBegin, slowTask1.OnEnd, slowTask1.OnTimeOut, null, false);
            PageAsyncTask task2 = new PageAsyncTask(slowTask2.OnBegin, slowTask2.OnEnd, slowTask2.OnTimeOut, null, false);
            PageAsyncTask task3 = new PageAsyncTask(slowTask3.OnBegin, slowTask3.OnEnd, slowTask3.OnTimeOut, null, false);

            RegisterAsyncTask(task1);
            RegisterAsyncTask(task2);
            RegisterAsyncTask(task3);

            // Запуск асинхронных операций на выполнение. 
            Page.ExecuteRegisteredAsyncTasks();

            OutputLabel.Text = slowTask1.GetData() + "<br />" + slowTask2.GetData() + "<br />" + slowTask3.GetData();
        }
    }
}
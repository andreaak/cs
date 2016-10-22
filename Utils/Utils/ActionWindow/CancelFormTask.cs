using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utils.ActionWindow;

namespace Utils
{
    public partial class CancelFormTask : Form
    {
        private bool isExternalIncrement;
        private int count;
        private int currentValue;
        CancellationTokenSource cts;

        public CancelFormTask()
        {
            InitializeComponent();
        }

        public CancelFormTask(string text)
            : this()
        {
            this.Text = text;
        }

        public Task StarActionAsync(Action action)
        {
            cts = new CancellationTokenSource();
            Task task = Task.Factory.StartNew(action, cts.Token);

            task.ContinueWith(t =>
            {
                Close();
            }, cts.Token, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.FromCurrentSynchronizationContext());
            task.ContinueWith(t =>
            {
                AggregateException ex = t.Exception;
                DisplayMessage.ShowError(ex.InnerException.Message);
                Close();
            }, CancellationToken.None, TaskContinuationOptions.OnlyOnFaulted, TaskScheduler.FromCurrentSynchronizationContext());
            task.ContinueWith(t =>
            {
            }, CancellationToken.None, TaskContinuationOptions.OnlyOnCanceled, TaskScheduler.FromCurrentSynchronizationContext());
            return task;
        }

        public Task<T> StarFuncAsync<T>(Func<T> func)
        {
            cts = new CancellationTokenSource();
            Task<T> task = Task.Factory.StartNew<T>(func, cts.Token);

            task.ContinueWith(t =>
            {
                Close();
            }, cts.Token, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.FromCurrentSynchronizationContext());
            task.ContinueWith(t =>
            {
                AggregateException ex = t.Exception;
                DisplayMessage.ShowError(ex.InnerException.Message);
                Close();
            }, CancellationToken.None, TaskContinuationOptions.OnlyOnFaulted, TaskScheduler.FromCurrentSynchronizationContext());
            task.ContinueWith(t =>
            {
            }, CancellationToken.None, TaskContinuationOptions.OnlyOnCanceled, TaskScheduler.FromCurrentSynchronizationContext());
            return task;
        }

        private void CancelForm_Shown(object sender, EventArgs e)
        {
            if (!isExternalIncrement)
            {
                timer1.Start();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            cts.Cancel();
            Close();
        }

        private void CancelForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isExternalIncrement)
            {
                timer1.Stop();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Increment(false);
        }

        public void Increment()
        {
            Increment(true);
        }

        private void Increment(bool isRefresh)
        {
            if (progressBar1.InvokeRequired)
            {
                Invoke(new Action<bool>(Increment), isRefresh);
            }
            else
            {
                DisplayIncrement(isRefresh);
            }
        }

        private void DisplayIncrement(bool isRefresh)
        {
            if (!isExternalIncrement)
            {
                TimerIncrement();
            }
            else
            {
                SmartIncrement();
            }

            if (isRefresh)
            {
                Refresh();
            }
        }

        private void TimerIncrement()
        {
            if (progressBar1.Value >= progressBar1.Maximum)
            {
                progressBar1.Value = 0;
            }
            else
            {
                progressBar1.PerformStep();
            }
        }

        private void SmartIncrement()
        {
            ++currentValue;
            int percent = (int)((double)currentValue / count * 100);
            if (percent > progressBar1.Value)
            {
                progressBar1.Value = percent;
            }
        }
    }

    public static class CancelFormEx
    {
        public static bool ShowProgressWindow(Action action, string headerText)
        {
            CancelFormTask form = new CancelFormTask(headerText);
            Task task = form.StarActionAsync(action);
            form.ShowDialog();
            return task.Status == TaskStatus.RanToCompletion;
        }

        public static T ShowProgressWindow<T>(Func<T> func, string headerText)
        {
            CancelFormTask form = new CancelFormTask(headerText);
            Task<T> task = form.StarFuncAsync(func);
            form.ShowDialog();
            if (task.Status == TaskStatus.RanToCompletion)
            {
                return task.Result;
            }
            return default(T);
        }
    }
}

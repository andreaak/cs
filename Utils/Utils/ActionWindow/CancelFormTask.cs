using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Utils
{
    public partial class CancelFormTask : Form
    {
        private bool isExternalIncrement;
        private int count;
        private int currentValue;
        Task task;
        CancellationTokenSource cts;

        public CancelFormTask()
        {
            InitializeComponent();
        }

        public CancelFormTask(string text, Task task, CancellationTokenSource cts)
            :this()
        {
            this.Text = text;
            this.task = task;
            this.cts = cts;
            this.task.ContinueWith(t => 
            {
                if (!cts.IsCancellationRequested)
                {
                    Close();
                }
                else
                {
                    Exception ex = t.Exception;
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        public CancelFormTask(string text, int count, Task task, CancellationTokenSource cts)
            : this(text, task, cts)
        {
            this.isExternalIncrement = true;
            this.count = count;
            currentValue = 0;
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



        //public void CloseForm()
        //{
        //    if (this.InvokeRequired)
        //    {
        //        this.Invoke(new Action(CloseForm));
        //    }
        //    else
        //    {
        //        this.DialogResult = DialogResult.OK;
        //        this.Close();
        //    }
        //}
    }

}

namespace Utils
{
    //public partial class CancelForm : Form
    //{
    //    private bool isExternalIncrement;
    //    private int count;
    //    private int currentValue;


    //    public CancelForm()
    //    {
    //        InitializeComponent();
    //    }

    //    public CancelForm(string text)
    //        :this()
    //    {
    //        this.Text = text;
    //    }

    //    public CancelForm(string text, int count)
    //        : this(text)
    //    {
    //        this.isExternalIncrement = true;
    //        this.count = count;
    //        currentValue = 0;
    //    }

    //    private void CancelForm_Shown(object sender, EventArgs e)
    //    {
    //        if (!isExternalIncrement)
    //        {
    //            timer1.Start();
    //        }
    //    }

    //    private void CancelForm_FormClosing(object sender, FormClosingEventArgs e)
    //    {
    //        if (!isExternalIncrement)
    //        {
    //            timer1.Stop();
    //        }
    //    }

    //    private void timer1_Tick(object sender, EventArgs e)
    //    {
    //        Increment(false);
    //    }

    //    public void Increment()
    //    {
    //        Increment(true);
    //    }

    //    private void Increment(bool isRefresh)
    //    {
    //        if (progressBar1.InvokeRequired)
    //        {
    //            Invoke(new Action<bool>(Increment), isRefresh);
    //        }
    //        else
    //        {
    //            DisplayIncrement(isRefresh);
    //        }
    //    }

    //    private void DisplayIncrement(bool isRefresh)
    //    {
    //        if (!isExternalIncrement)
    //        {
    //            TimeIncrement();
    //        }
    //        else
    //        {
    //            SmartIncrement();
    //        }

    //        if (isRefresh)
    //        {
    //            Refresh();
    //        }
    //    }

    //    private void TimeIncrement()
    //    {
    //        if (progressBar1.Value >= progressBar1.Maximum)
    //        {
    //            progressBar1.Value = 0;
    //        }
    //        else
    //        {
    //            progressBar1.PerformStep();
    //        }
    //    }

    //    private void SmartIncrement()
    //    {
    //        ++currentValue;
    //        int percent = (int)((double)currentValue / count * 100);
    //        if (percent > progressBar1.Value)
    //        {
    //            progressBar1.Value = percent;
    //        }
    //    }

    //    public void CloseForm()
    //    {
    //        if (this.InvokeRequired)
    //        {
    //            this.Invoke(new Action(CloseForm));
    //        }
    //        else
    //        {
    //            this.DialogResult = DialogResult.OK;
    //            this.Close();
    //        }
    //    }
    //}

}

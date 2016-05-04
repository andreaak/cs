using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Utils.ActionWindow
{
    //public class ThreadVisualization
    //{

    //    #region VISUALIZATION

    //    public static bool StartWorkThread(CancelForm form, Thread thread)
    //    {
    //        thread.IsBackground = true;
    //        thread.Name = "VisualizatonThread";
    //        thread.Start();
    //        if (form.ShowDialog() == DialogResult.Cancel)
    //        {
    //            AbortThread(thread);
    //            DisplayMessage.ShowWarning("Cancel by user");
    //            return false;
    //        }
    //        return true;
    //    }

    //    private static void AbortThread(Thread thread, bool isJoin = false)
    //    {
    //        if (thread.IsAlive)
    //        {
    //            thread.Abort();
    //            if (isJoin)
    //            {
    //                thread.Join();
    //            }
    //        }
    //    }
        
    //    public static event Action ProcessEnded;
    //    public static event Action Increment;

    //    public static void OnProcessEnded()
    //    {
    //        if (ProcessEnded != null)
    //        {
    //            ProcessEnded();
    //        }
    //    }

    //    public static void OnIncrement()
    //    {
    //        if (Increment != null)
    //        {
    //            Increment();
    //        }
    //    }

    //    #endregion
    //}
}

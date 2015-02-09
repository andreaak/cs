using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Utils.ActionWindow
{
    public class DisplayMessage
    {
        public const string INFO_DONE_CONST = "Done";
        public static string DROP_TABLE_CONST = "Are you sure you want to drop table?";
        
        public static void ShowError(string message, string caption = "Error", IWin32Window owner = null)
        {
            MessageBox.Show(owner, message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ShowInfo(string message, string caption = "Info", IWin32Window owner = null)
        {
            MessageBox.Show(owner, message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void ShowWarning(string message, string caption = "Warning", IWin32Window owner = null)
        {
            MessageBox.Show(owner, message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static DialogResult ShowWarningYesNO(string message, string caption = "Warning", IWin32Window owner = null, string additionalMessage = null)
        {
            return MessageBox.Show(owner, message + additionalMessage, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }
    }
}

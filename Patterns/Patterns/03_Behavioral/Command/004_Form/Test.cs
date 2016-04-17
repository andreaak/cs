using System;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Patterns._03_Behavioral.Command._004_Form
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        [STAThread]
        public void Test1()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}

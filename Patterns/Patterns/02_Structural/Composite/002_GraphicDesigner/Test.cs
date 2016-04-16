using System;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Patterns._02_Structural.Composite._002_GraphicDesigner
{
    [TestClass]
    public class Test
    {
        [STAThread]
        [TestMethod]
        public void Test1()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}

using System;
using System.Windows.Forms;
using NUnit.Framework;

namespace Patterns._02_Structural.Composite._002_GraphicDesigner
{
    [TestFixture]
    public class Test
    {
        [STAThread]
        [Test]
        public void Test1()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}

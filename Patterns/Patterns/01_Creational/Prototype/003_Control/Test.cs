using System;
using System.Windows.Forms;
using NUnit.Framework;

namespace Patterns._01_Creational.Prototype._003_Control
{
    [TestFixture]
    public class Test
    {
        [Test]
        [STAThread]
        public void Test1()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Form1 form1 = new Form1();
            //Application.Run(form1);
        }

    }
}

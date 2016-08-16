using System;
using System.Windows.Forms;
using NUnit.Framework;

namespace Patterns._03_Behavioral.Mediator._002_MotivationMediator
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
            Application.Run(new FormFontChooser());
        }
    }

}

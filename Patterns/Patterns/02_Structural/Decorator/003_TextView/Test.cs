using System.Windows.Forms;
using NUnit.Framework;

namespace Patterns._02_Structural.Decorator._003_TextView
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}

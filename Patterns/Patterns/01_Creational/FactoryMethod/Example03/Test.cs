using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Forms;

namespace Creational.FactoryMethod.Example3
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
            Application.Run(new BaseForm());
        }

    }
}

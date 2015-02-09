using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Patterns.Structural.Bridge.Example2
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        [STAThread]
        public void Test1()
        {
            Window window = null;

            //window = new MSWindow();
            window = new MacWindow();

            window.Draw();
        }
    }
}

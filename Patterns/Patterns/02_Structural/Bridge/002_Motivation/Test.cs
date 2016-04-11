using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Patterns.Structural.Bridge._002_Motivation
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

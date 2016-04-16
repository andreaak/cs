using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Patterns._02_Structural.Bridge._002_Motivation.Abstraction;

namespace Patterns._02_Structural.Bridge._002_Motivation
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

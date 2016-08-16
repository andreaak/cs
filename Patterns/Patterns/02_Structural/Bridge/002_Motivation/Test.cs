using System;
using NUnit.Framework;
using Patterns._02_Structural.Bridge._002_Motivation.Abstraction;

namespace Patterns._02_Structural.Bridge._002_Motivation
{
    [TestFixture]
    public class Test
    {
        [Test]
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

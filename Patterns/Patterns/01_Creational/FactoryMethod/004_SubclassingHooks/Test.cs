﻿using System;
using System.Windows.Forms;
using NUnit.Framework;

namespace Patterns._01_Creational.FactoryMethod._004_SubclassingHooks
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
            Application.Run(new Form1());
        }

    }
}

﻿using System;
using System.Windows.Forms;
using NUnit.Framework;

namespace Patterns._02_Structural.Adapter._008_AdaptersForTreeDisplay
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

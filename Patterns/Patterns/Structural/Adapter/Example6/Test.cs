﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Forms;

namespace Patterns.Structural.Adapter.Example6
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
            Application.Run(new Form1());
        }
    }
}
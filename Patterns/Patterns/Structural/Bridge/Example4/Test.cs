﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Forms;

namespace Patterns.Structural.Bridge.Example4
{
    [TestClass]
    public class Test
    {
        [STAThread]
        [TestMethod]
        public void Test1()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
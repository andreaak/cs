﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Forms;

namespace Creational.FactoryMethod.Example6
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            Creator creator = new MyCreator();
            Product product = creator.Create(ProductType.THEIRS);
        }

    }
}
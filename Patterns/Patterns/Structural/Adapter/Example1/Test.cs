using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using Patterns.Structural.Adapter.Example1.HomeCats;
using Patterns.Structural.Adapter.Example1.Adapters;
using Patterns.Structural.Adapter.Example1.WildCats;

namespace Patterns.Structural.Adapter.Example1
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            IHomeCat vaska = new YardCat();
            vaska.Name = "Васька";
            CatInfoPrinter.PrintCatInfo(vaska);

            IHomeCat wagner = new PedigreedCat();
            wagner.Name = "Вагнер";
            CatInfoPrinter.PrintCatInfo(wagner);

            IWildCat tiger = new Tiger();
            HomeCatAdapter adapter = new HomeCatAdapter(tiger);
            CatInfoPrinter.PrintCatInfo(adapter);
        }
    }
}

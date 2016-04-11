using Microsoft.VisualStudio.TestTools.UnitTesting;
using Patterns.Structural.Adapter._001_Cats.Adapters;
using Patterns.Structural.Adapter._001_Cats.HomeCats;
using Patterns.Structural.Adapter._001_Cats.WildCats;

namespace Patterns.Structural.Adapter._001_Cats
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

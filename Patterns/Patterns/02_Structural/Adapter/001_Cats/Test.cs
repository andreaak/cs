using NUnit.Framework;
using Patterns._02_Structural.Adapter._001_Cats.Adapters;
using Patterns._02_Structural.Adapter._001_Cats.HomeCats;
using Patterns._02_Structural.Adapter._001_Cats.WildCats;

namespace Patterns._02_Structural.Adapter._001_Cats
{
    [TestFixture]
    public class Test
    {
        [Test]
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

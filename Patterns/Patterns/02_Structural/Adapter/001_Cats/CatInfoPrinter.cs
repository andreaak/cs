using System;
using Patterns.Structural.Adapter._001_Cats.HomeCats;
using System.Diagnostics;

namespace Patterns.Structural.Adapter._001_Cats
{
    class CatInfoPrinter
    {
        public static void PrintCatInfo(IHomeCat cat)
        {
            Debug.WriteLine("Кошачье досье:");
            Debug.WriteLine(string.Format("Имя кота: {0}", cat.Name));
            Debug.Write("Вид мяуканья: ");
            cat.Meow();
            Debug.Write("Вид царапанья: ");
            cat.Scratch();

            Debug.WriteLine("");
        }
    }
}

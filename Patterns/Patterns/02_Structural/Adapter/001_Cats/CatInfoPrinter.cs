using System.Diagnostics;
using Patterns._02_Structural.Adapter._001_Cats.HomeCats;

namespace Patterns._02_Structural.Adapter._001_Cats
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

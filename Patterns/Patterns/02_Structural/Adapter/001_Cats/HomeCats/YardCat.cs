using System.Diagnostics;

namespace Patterns._02_Structural.Adapter._001_Cats.HomeCats
{
    class YardCat : IHomeCat
    {
        public string Name { get; set; }

        public void Meow()
        {
            Debug.WriteLine("Мяу мяу!");
        }

        public void Scratch()
        {
            Debug.WriteLine("Я царапаюсь, но не сильно");
        }
    }
}

using System;
using System.Diagnostics;

namespace Patterns.Structural.Adapter._001_Cats.HomeCats
{
    class PedigreedCat : IHomeCat
    {
        public void Meow()
        {
            Debug.WriteLine("Урррр урррр");
        }

        public void Scratch()
        {
            Debug.WriteLine("Я не царапаюсь");
        }

        public string Name { get; set; }
    }
}

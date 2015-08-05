using System;
using System.Diagnostics;

namespace Patterns.Structural.Adapter.Example1.HomeCats
{
    class PedigreedCat : IHomeCat
    {
        public void Meow()
        {
            Console.WriteLine("Урррр урррр");
        }

        public void Scratch()
        {
            Console.WriteLine("Я не царапаюсь");
        }

        public string Name { get; set; }
    }
}

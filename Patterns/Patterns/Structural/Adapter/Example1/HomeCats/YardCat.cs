using System;
using System.Diagnostics;

namespace Patterns.Structural.Adapter.Example1.HomeCats
{
    class YardCat : IHomeCat
    {
        public string Name { get; set; }

        public void Meow()
        {
            Console.WriteLine("Мяу мяу!");
        }

        public void Scratch()
        {
            Console.WriteLine("Я царапаюсь, но не сильно");
        }
    }
}

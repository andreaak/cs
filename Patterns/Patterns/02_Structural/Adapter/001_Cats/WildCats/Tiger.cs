using System;
using System.Diagnostics;

namespace Patterns.Structural.Adapter._001_Cats.WildCats
{
    class Tiger : IWildCat
    {
        public string Breed { get { return "Тигр обыкновенный"; } }

        public void Growl()
        {
            Debug.WriteLine("Grrrrrrr");
        }

        public void Scratch()
        {
            Debug.WriteLine("У меня очень острые когти, царапаюсь до смерти");
        }
    }
}

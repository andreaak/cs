using System.Diagnostics;

namespace Patterns._02_Structural.Flyweight._003_ComedyFilm
{
    // Разделяемый.
    class ActorMikeMyers : Flyweight
    {
        public override void Greeting(string speech)
        {
            Debug.WriteLine(speech);
        }
    }
}

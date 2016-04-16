namespace Patterns._02_Structural.Flyweight._003_ComedyFilm
{
    // Неразделяемый.
    class RoleDoctorEvil : Flyweight
    {
        Flyweight flyweight;

        public RoleDoctorEvil(Flyweight flyweight)
        {
            this.flyweight = flyweight;
        }

        public override void Greeting(string speech)
        {
            this.flyweight.Greeting(speech);
        }
    }
}

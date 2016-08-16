using NUnit.Framework;

namespace Patterns._02_Structural.Flyweight._003_ComedyFilm
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            ActorMikeMyers mike = new ActorMikeMyers();

            RoleAustinPowers austin = new RoleAustinPowers(mike);
            austin.Greeting("Hello! I'm Austin Powers!");

            RoleDoctorEvil dr = new RoleDoctorEvil(mike);
            dr.Greeting("Hello! I'm Dr.Evil!");

            // Delay.
            //Console.ReadKey();
        }
    }
}

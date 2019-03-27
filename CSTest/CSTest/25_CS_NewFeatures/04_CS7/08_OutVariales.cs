using NUnit.Framework;
using System.Diagnostics;

namespace CSTest._25_CS_NewFeatures._04_CS7
{

    [TestFixture]
    public class _08_OutVariales
    {
#if CS7
        [Test]
        public void TestOutVariales_1()
        {
            CreateName(out var firstName, out var lastName);
            Debug.WriteLine($"What's up, {firstName} {lastName}!");

            /*
            Original actor:Tom Cruise
            Replaced actor with :Dwayne Johnson
            */
        }

        private void CreateName(out string firstName,
                               out string lastName)
        {
            firstName = "Reynald";
            lastName = "Adolphe";
        }
#endif
    }

}

using NUnit.Framework;
using System;
using System.Diagnostics;

namespace CSTest._25_CS_NewFeatures._04_CS7
{

    [TestFixture]
    public class _06_Switch
    {
#if CS7
        [Test]
        public void TestSwitch_1()
        {
            Performer p = new Performer();
            Actor a = new Actor("Eddie Murphy", 54, "male",
                                "Coming to America", 2017);
            Musician m = new Musician("Jen", 25, "female", "singing",
                                      "Pop");

            // Switch Statement 
            switch (m)
            {
                case Performer performer when (performer.Age == 33):
                    Debug.WriteLine($"The performer {performer.Name}");
                    break;
                case Musician musician when (musician.Age == 25):
                    Debug.WriteLine($"The musician {musician.Name}");
                    break;
                case Musician musician:
                    Debug.WriteLine("The Musician is unknown");
                    break;
                default:
                    Debug.WriteLine("Not found");
                    break;
                case null:
                    throw new ArgumentNullException(nameof(m));
            }

            /*
            The musician Jen
            */
        }

        [Test]
        public void TestSwitch_2()
        {
            Musician m = null;
            switch (m)
            {
                case Performer performer when (performer.Age == 33):
                    Debug.WriteLine($"The performer {performer.Name}");
                    break;
                case Musician musician when (musician.Age == 25):
                    Debug.WriteLine($"The musician {musician.Name}");
                    break;
                case Musician musician:
                    Debug.WriteLine("The Musician is unknown");
                    break;
                default:
                    Debug.WriteLine("Not found");
                    break;
                case null:
                    throw new ArgumentNullException(nameof(m));
            }

            /*
            ArgumentNullException
            */
        }

#endif
    }

}

using NUnit.Framework;
using System;
using System.Diagnostics;

namespace CSTest._25_CS_NewFeatures._04_CS7
{

    [TestFixture]
    public class _07_RefReturn
    {
#if CS7
        [Test]
        public void TestRefReturn_1()
        {
            string[] actors = {"Ben Affleck", "Jennifer Lawrence", "Tom Cruise",
                                    "Matt Damon", "Jackie Chan" };
            int positionInArray = 2;

            ref string actor3 = ref FindActor(positionInArray, actors);

            Debug.WriteLine($"Original actor:{actor3}");

            actor3 = "Dwayne Johnson";

            Debug.WriteLine($"Replaced actor with :{actors[positionInArray]}");

            /*
            Original actor:Tom Cruise
            Replaced actor with :Dwayne Johnson
            */
        }

        public ref string FindActor(int index, string[] names)
        {

            if (names.Length > 0)
            {
                return ref names[index]; // return the storage location, not the value  
            }
            throw new IndexOutOfRangeException($"{nameof(index)} not found.");

        }
#endif
    }

}

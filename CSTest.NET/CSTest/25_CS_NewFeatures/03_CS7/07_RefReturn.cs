using NUnit.Framework;
using System;
using System.Diagnostics;

namespace CSTest._25_CS_NewFeatures._03_CS7
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

        [Test]
        public void TestRefReturn_2()
        {
            int[] numbers = { 0, 1, 2, 3, 4 };
            ref int numRef = ref numbers[2];
            numRef *= 10;
            Debug.WriteLine(numRef);  //  20
            Debug.WriteLine(numbers[2]);  //  20


            ref string xRef = ref GetX();  // Присвоить результат ссылочной
                                           // локальной переменной
            Debug.WriteLine(X);  // Old Value
            xRef = "New Value";
            Debug.WriteLine(X);  // New Value

        }

        static string X = "Old Value";
        static ref string GetX() => ref X;  // Этот метод возвращает ссылочное значение 
        
        [Test]
        public void TestRefReturn_3()
        {
            var smallArray = new int[] { 1, 2, 3, 4, 5 };
            var largeArray = new int[] { 10, 20, 30, 40, 50 }; 
            int index = 7;
            ref int refValue = ref ((index < 5)
            ? ref smallArray[index]
            : ref largeArray[index - 5]);
            refValue = 0;

            Debug.WriteLine(string.Join(" ", smallArray));
            Debug.WriteLine(string.Join(" ", largeArray));

            /*
            1 2 3 4 5
            10 20 0 40 50
             */

            index = 2;
            ((index < 5)
            ? ref smallArray[index]
            : ref largeArray[index - 5]) = 100;
            
            Debug.WriteLine(string.Join(" ", smallArray));
            Debug.WriteLine(string.Join(" ", largeArray));

            /*
             1 2 100 4 5
             10 20 0 40 50
             */
        }
#endif
    }

}

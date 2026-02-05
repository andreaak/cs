using NUnit.Framework;
using System;
using System.Diagnostics;

namespace CSTest._25_CS_NewFeatures._03_CS7
{

    [TestFixture]
    public class _06_Switch
    {
#if CS7
        
        [Test]
        public void TestSwitchTypePattern_1()
        {
            object x = 1;
            
            switch (x)
            {
                case int i:
                    Console.WriteLine("Это целочисленное  значение ! ");
                    break;
                case string s:
                    Console.WriteLine(s.Length);  // Можно использовать s 
                    break;
                case bool b when b == true:  //  Выполняется, когда b равно true
            Console.WriteLine("True ");
                    break;
                case null:                         // Можно  также переключаться по null
            Console.WriteLine("nul l ");
                    break;

            }
        }        
        
        [Test]
        public void TestSwitchWhen_2()
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
                case Performer performer when (performer.Age == 35):
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
        public void TestSwitch_3When()
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

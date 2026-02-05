using NUnit.Framework;
using System.Diagnostics;


namespace CSTest._25_CS_NewFeatures._05_CS9
{
    [TestFixture]
    public class _01_DefCtor
    {
#if CS7
        [Test]
        public void TestDefCtor1()
        {
            Console.WriteLine("=> Using new to create variables:");
            bool b = new bool();
            int i = new int();
            double d = new double();
            DateTime dt = new DateTime();
            // Устанавливается в false
            // Устанавливается в 0
            // Устанавливается в 0.0
            // Устанавливается
            // в 1/1/0001 12:00:00 AM
            Debug.WriteLine("{0}, {1}, {2}, {3}", b, i, d, dt);
        }

        [Test]
        public void TestDefCtor2()
        {
            Debug.WriteLine("=> Using new to create variables:");
            bool b = new();
            int i = new();
            double d = new();
            DateTime dt = new();
            Debug.WriteLine("{0}, {1}, { 2 } , {3}", b, i, d, dt);
            // Устанавливается в false
            // Устанавливается в 0
            // Устанавливается в 0.0
            // Устанавливается в 1/1/0001 12:00:00
        }

#endif

    }

}
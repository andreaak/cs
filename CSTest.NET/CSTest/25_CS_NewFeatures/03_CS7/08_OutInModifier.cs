using CSTest._05_Delegates_and_Events._02_Events._0_Setup;
using NUnit.Framework;
using System.Diagnostics;

namespace CSTest._25_CS_NewFeatures._03_CS7
{

    [TestFixture]
    public class _08_OutInModifier
    {
#if CS7
        [Test]
        public void TestOutVariales_1()
        {
            CreateName(out var firstName, out var lastName);
            Debug.WriteLine($"What's up, {firstName} {lastName}!");

            CreateName(out var _, out var _);
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
        
        [Test]
        public void TestInVariales_1()
        {
            AddReadOnly(10, 20);


            /*
            
            */
        }

        static int AddReadOnly(in int x, in int y)
        {
            // Ошибка CS8331 Cannot assign to variable in int
            // because it is a readonly variable
            // He удается присвоить значение переменной in int,
            // поскольку она допускает только чтение
            // х = 10000;
            // у = 88888;
            int ans = x + y;
            return ans;
            
        }

#endif
    }

}

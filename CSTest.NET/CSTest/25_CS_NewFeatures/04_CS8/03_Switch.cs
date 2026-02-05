using NUnit.Framework;
using System;
using System.Diagnostics;

namespace CSTest._25_CS_NewFeatures._04_CS8
{

    [TestFixture]
    public class _03_Switch
    {
#if CS7
        [Test]
        public void TestSwitch_1()
        {
            int cardNumber = 12;

            string cardName = cardNumber switch
            {
                13 => " Король ",
                12 => "Дама",
                11 => "Валет",
                _ => " Нефигурная  карта"  // Эквивалент default 
            };

            int cardNumber2 = 12; string suite2 = "spades";
            string cardName2 = (cardNumber2, suite2) switch
            {
                (13, "spades") => "Пиковый  король",
                _ => throw new NotImplementedException(),
            };

            /*

            */
        }



#endif
    }

}

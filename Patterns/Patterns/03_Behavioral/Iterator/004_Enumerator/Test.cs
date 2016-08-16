using System.Diagnostics;
using NUnit.Framework;

namespace Patterns._03_Behavioral.Iterator._004_Enumerator
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            Enumerable enumerable = new Enumerable();

            enumerable[0] = "Element A";
            enumerable[1] = "Element B";
            enumerable[2] = "Element C";
            enumerable[3] = "Element D";
                        
            foreach (var item in enumerable)
            {
                Debug.WriteLine(item);
            }

            // Delay.
            //Console.ReadKey();
        }
    }
}
using System.Diagnostics;
using NUnit.Framework;

namespace Patterns._03_Behavioral.Iterator._007_List
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            List list = new List { 1, 2, 3, 4 };
                        
            foreach (var item in list)
            {
                Debug.WriteLine(item);
            }

            // Delay.
            //Console.ReadKey();
        }
    }
}

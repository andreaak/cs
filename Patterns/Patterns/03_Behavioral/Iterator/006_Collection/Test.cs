using System.Diagnostics;
using NUnit.Framework;

namespace Patterns._03_Behavioral.Iterator._006_Collection
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            Collection collection = new Collection();

            foreach (var item in collection)
            {
                Debug.WriteLine(item);
            }

            Debug.WriteLine(new string('-', 2));

            int[] array = new int[20];

            collection.CopyTo(array, 2);

            foreach (int element in array)
            {
                Debug.Write(element);
            }

            // Delay.
            //Console.ReadKey();
        }
    }
}

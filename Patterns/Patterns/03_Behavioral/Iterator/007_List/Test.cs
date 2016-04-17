using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Patterns._03_Behavioral.Iterator._007_List
{
    [TestClass]
    public class Test
    {
        [TestMethod]
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

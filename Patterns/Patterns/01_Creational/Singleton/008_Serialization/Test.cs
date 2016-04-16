using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Patterns._01_Creational.Singleton._008_Serialization
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.SurrogateSelector = Singleton.SurrogateSelector;

            // Сериализация.

            FileStream stream = new FileStream("Data.bin", FileMode.Create);
            Singleton singleton = Singleton.Instance;
            singleton.field = "New value";
            formatter.Serialize(stream, singleton);
            stream.Dispose();
            //Debug.BackgroundColor = ConsoleColor.Cyan;
            //Debug.ForegroundColor = ConsoleColor.DarkRed;
            Debug.WriteLine("Serialization succeeded");
        }

        [TestMethod]
        public void Test2()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.SurrogateSelector = Singleton.SurrogateSelector;

            // Двойная десериализация.

            Stream stream1 = new FileStream("Data.bin", FileMode.Open);
            Singleton singleton1 = formatter.Deserialize(stream1) as Singleton;
            stream1.Dispose();

            Stream stream2 = new FileStream("Data.bin", FileMode.Open);
            Singleton singleton2 = formatter.Deserialize(stream2) as Singleton;
            stream2.Dispose();

            // Проверка двойной десериализации.

            Debug.WriteLine(singleton1.GetHashCode());
            Debug.WriteLine(singleton2.GetHashCode());

            Debug.WriteLine(singleton1.field);
            Debug.WriteLine(singleton2.field);
        }
    }
}

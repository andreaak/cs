using NUnit.Framework;
using System.Diagnostics;

namespace CSTest._25_CS_NewFeatures._04_CS8
{
    [TestFixture]
    public class _02_Operations
    {

        [Test]
        public void TestNullOp_1()
        {
            string s = null; 
            
            if (s == null) 
                s = "Добро пожаловать";
            
            
            s ??= "Добро пожаловать ";
        }

        [Test]
        public void TestUsing()
        {
            if (File.Exists("file.txt"))
            {
                using var reader = File.Open("file.txt", FileMode.OpenOrCreate);
                Debug.WriteLine(reader.ReadByte());
                Debug.WriteLine(reader.ReadByte());
            }
        }
    }
}

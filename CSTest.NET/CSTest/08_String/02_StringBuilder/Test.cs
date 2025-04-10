using NUnit.Framework;
using System.Diagnostics;
using System.Text;

namespace CSTest._08_String._02_StringBuilder
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void TestStringBuider1()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("123456789012345");
            Debug.WriteLine("Length = " + sb.Length);
            Debug.WriteLine("Capacity = " + sb.Capacity);
            sb.Append("12345678901234567890");
            Debug.WriteLine("Length = " + sb.Length);
            Debug.WriteLine("Capacity = " + sb.Capacity);
            sb.Append("12345678901234567890");
            Debug.WriteLine("Length = " + sb.Length);
            string temp1 = sb.ToString();
            Debug.WriteLine(temp1);
            string temp2 = sb.ToString();
            Debug.WriteLine(temp2);
            Debug.WriteLine("Temp1 RefEquals Temp2 : " + ReferenceEquals(temp1, temp2));

            /*
            Length = 15
            Capacity = 16
            Length = 35
            Capacity = 35
            Length = 55
            1234567890123451234567890123456789012345678901234567890
            1234567890123451234567890123456789012345678901234567890
            Temp1 RefEquals Temp2 : False
            */
        }

        [Test]
        public void TestStringBuider2()
        {
            StringBuilderNET sb = new StringBuilderNET();
            sb.Append("123456789012345");
            Debug.WriteLine("Length = " + sb.Length);
            Debug.WriteLine("Capacity = " + sb.Capacity);
            sb.Append("12345678901234567890");
            Debug.WriteLine("Length = " + sb.Length);
            Debug.WriteLine("Capacity = " + sb.Capacity);
            sb.Append("12345678901234567890");
            Debug.WriteLine("Length = " + sb.Length);
            Debug.WriteLine(sb.ToString());

            /*
            Length = 15
            Capacity = 16
            Length = 35
            Capacity = 35
            Length = 55
            Capacity = 70
            1234567890123451234567890123456789012345678901234567890
            */
        }


        [Test]
        public void TestStringBuider3()
        {
            StringBuilderNET sb = new StringBuilderNET();
            sb.Append("ABCDEFGHIJKLMNO");
            Debug.WriteLine("Length = " + sb.Length);
            Debug.WriteLine("Capacity = " + sb.Capacity);
            sb.Append("PQRSTUVWXYZ");
            Debug.WriteLine("Length = " + sb.Length);
            Debug.WriteLine("Capacity = " + sb.Capacity);
            Debug.WriteLine(sb.ToString());
            /*
            Length = 15
            Capacity = 16
            Length = 26
            Capacity = 32
            ABCDEFGHIJKLMNOPQRSTUVWXYZ
            */
        }
    }
}

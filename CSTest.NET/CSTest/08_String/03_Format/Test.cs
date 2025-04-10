using System.Diagnostics;
using System.Globalization;
using NUnit.Framework;

namespace CSTest._08_String._03_Format
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void TestFormat1DefaultFormatProvider()
        {
            string temp = string.Format(new TestFormatProvider(), "Start {0:foobar} End", new TestFormattable());
            Debug.WriteLine("Result: " + temp);
            /*
            Asked for System.ICustomFormatter
            TestFormattable.ToString(string format, IFormatProvider formatProvider) called
            format: foobar
            provider: CSTest._08_String._03_Format.TestFormatProvider
            Result: Start arbitrary value End
            */
        }

        [Test]
        public void TestFormat2()
        {
            string temp = string.Format(new TestFormatProvider(), "Start {0,8:foobar} End", new TestFormattable());
            Debug.WriteLine("Result: " + temp);
            /*
            Asked for System.ICustomFormatter
            TestFormattable.ToString(string format, IFormatProvider formatProvider) called
            format: foobar
            provider: CSTest._08_String._03_Format.TestFormatProvider
            Result: Start arbitrary value End
            */
        }

        [Test]
        public void TestFormat3CustomFormatter()
        {
            string temp = string.Format(new TestFormatProvider2(), "Start {0:foobar} End", new TestFormattable());
            Debug.WriteLine("Result: " + temp);
            /*
            Asked for System.ICustomFormatter
            TestCustomFormatter.Format(string format, object arg, IFormatProvider provider) called
            format: foobar
            arg: CSTest._08_String._03_Format.TestFormattable
            TestFormattable.ToString(string format, IFormatProvider formatProvider) called
            format: foobar
            provider: CSTest._08_String._03_Format.TestFormatProvider2
            arg result: arbitrary value
            provider: CSTest._08_String._03_Format.TestFormatProvider2
            Result: Start (format was "foobar") End
            */
        }

        [Test]
        public void TestFormat4CustomFormatter()
        {
            string temp = string.Format(new TestFormatProvider2(), "Start {0,8:foobar} End", new TestFormattable());
            Debug.WriteLine("Result: " + temp);
            /*
            Asked for System.ICustomFormatter
            TestCustomFormatter.Format(string format, object arg, IFormatProvider provider) called
            format: foobar
            arg: CSTest._08_String._03_Format.TestFormattable
            TestFormattable.ToString(string format, IFormatProvider formatProvider) called
            format: foobar
            provider: CSTest._08_String._03_Format.TestFormatProvider2
            arg result: arbitrary value
            provider: CSTest._08_String._03_Format.TestFormatProvider2
            Result: Start (format was "foobar") End
            */
        }

        [Test]
        public void TestFormat5CustomFormatter()
        {
            double data = 12345678.9876;
            string format = "#,##0";
            string resFormat = string.Format("{{0:{0}}}", format);
            string res = string.Format(CultureInfo.InvariantCulture, resFormat, data);

            format = "#,###";
            resFormat = string.Format("{{0:{0}}}", format);
            res = string.Format(CultureInfo.InvariantCulture, resFormat, data);
        }
    }
}

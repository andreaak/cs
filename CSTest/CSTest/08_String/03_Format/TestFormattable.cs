using System;
using System.Diagnostics;

namespace CSTest._08_String._03_Format
{
    class TestFormattable : IFormattable
    {
        public string ToString(string format, IFormatProvider formatProvider)
        {
            Debug.WriteLine("TestFormattable.ToString(string format, IFormatProvider formatProvider) called");
            Debug.WriteLine("format: " + format);
            Debug.WriteLine("provider: " + formatProvider);

            return "arbitrary value";
        }
    }
}

using System;
using System.Diagnostics;

namespace CSTest._08_String._03_Format
{
    class TestCustomFormatter : ICustomFormatter
    {
        public string Format(string format, object arg, IFormatProvider provider)
        {
            Debug.WriteLine("TestCustomFormatter.Format(string format, object arg, IFormatProvider provider) called");
            Debug.WriteLine("format: " + format);
            Debug.WriteLine("arg: " + arg);
            Debug.WriteLine("arg result: " + ((IFormattable)arg).ToString(format, provider));
            Debug.WriteLine("provider: " + provider);
            return string.Format("(format was \"{0}\")", format);
        }
    }
}

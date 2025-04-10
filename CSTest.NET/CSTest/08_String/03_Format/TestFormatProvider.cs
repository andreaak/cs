using System;
using System.Diagnostics;
using System.Globalization;

namespace CSTest._08_String._03_Format
{
    class TestFormatProvider : IFormatProvider
    {
        public object GetFormat(Type formatType)
        {
            Debug.WriteLine("Asked for {0}", formatType);
            return CultureInfo.CurrentCulture.GetFormat(formatType);
        }
    }

    class TestFormatProvider2 : IFormatProvider
    {
        public object GetFormat(Type formatType)
        {
            Debug.WriteLine("Asked for {0}", formatType);
            if (formatType == typeof(ICustomFormatter))
                return new TestCustomFormatter();
            return CultureInfo.CurrentCulture.GetFormat(formatType);
        }
    }
}

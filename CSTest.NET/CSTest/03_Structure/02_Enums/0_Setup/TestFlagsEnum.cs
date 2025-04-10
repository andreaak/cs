using System;

namespace CSTest._03_Structure._02_Enums._0_Setup
{
    [Flags]
    public enum TestFlagsEnum
    {
        None = 0,
        View = 1,
        Pending = 2,
        MarkedTestAsResult = 4,
    }

    public static class TestFlagsEnumHelper
    {
        public static Boolean IsSet(this TestFlagsEnum options, TestFlagsEnum option)
        {
            if (option == 0)
            {
                throw new ArgumentOutOfRangeException("option", "Value must not be 0");
            }
            return (options & option) == option;
        }

        public static Boolean AnyFlagsSet(this TestFlagsEnum options, TestFlagsEnum option)
        {
            return ((options & option) != 0);
        }

        public static TestFlagsEnum Set(this TestFlagsEnum options, TestFlagsEnum option)
        {
            return options | option;
        }

        public static TestFlagsEnum Clear(this TestFlagsEnum options, TestFlagsEnum option)
        {
            return options & ~option;
        }
    }
}

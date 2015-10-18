using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSTest.Classes.ExtensionMethods
{
    public sealed class SealedClass
    {
        private int privateInt = 0;
        protected int protectedInt = 0;
        public int publicInt = 0;

        public SealedClass()
        {
            if (privateInt != 0)
            {
                privateInt = 0;
            }
            if (protectedInt != 0)
            {
                protectedInt = 0;
            }
        }
    }
}

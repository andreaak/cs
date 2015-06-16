using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSTest.Classes.ExtensionMethods
{
    public static class ExtensionClass
    {
        public static void Extension(this SealedClass obj)
        {
            if (obj != null)
            {
                int publicInt = obj.publicInt;
            }
        }
    }
}

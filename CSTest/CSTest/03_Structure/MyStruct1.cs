using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSTest._03_Structure
{
    struct MyStruct1
    {
        public int field;

        public int Field
        {
            get { return field; }
            set { field = value; }
        }

        public void Show()
        {
            Console.WriteLine(field);
        }
    }
}

using System.Diagnostics;

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
            Debug.WriteLine(field);
        }
    }
}

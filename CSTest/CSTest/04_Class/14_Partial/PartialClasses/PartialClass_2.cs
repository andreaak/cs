using System.Diagnostics;

namespace CSTest._04_Class._14_Partial.PartialClasses
{
    // Вторая часть класса.
    partial class PartialClass
    {
        public void MethodFromPart2()
        {
            Debug.WriteLine("II Part");
        }
    }
}

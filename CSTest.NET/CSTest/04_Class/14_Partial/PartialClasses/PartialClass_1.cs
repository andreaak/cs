using System.Diagnostics;

namespace CSTest._04_Class._14_Partial.PartialClasses
{
    // Первая часть класса.
    partial class PartialClass
    {
        public void MethodFromPart1()
        {
            Debug.WriteLine("I Part");
        }
    }
}

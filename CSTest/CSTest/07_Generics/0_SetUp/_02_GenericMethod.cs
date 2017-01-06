using System.Diagnostics;

namespace CSTest._07_Generics._0_Setup
{
    // Универсальные шаблоны. (Универсальный метод)
    class _02_GenericMethod
    {
        public void Method<T>(T argument)
        {
            T variable = argument;

            Debug.WriteLine(variable);
        }
    }
}

using System.Diagnostics;

namespace CSTest._07_Generics
{
    // where T : new()  -  Аргумент типа должен иметь открытый конструктор без параметров.
    // нельзя использовать struct и new одновременно
    // При использовании с другими ограничениями ограничение new() должно устанавливаться последним:
    // class GenericWithConstraintNew<T> where T : class, new()   { /* ... */ }

    class GenericWithConstraintNew<T> where T : new()
    {
        public T instance = new T();

        public void GetValues()
        {
            Debug.WriteLine(instance.ToString());
        }
    }
    class _11_ThirdLevelConstraints
    {
        public int MyIntProperty { get; set; }
        public string MyStringProperty { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - {1}", MyIntProperty, MyStringProperty);
        }
    }

    abstract class _11_AbstractClass
    {
    }
}

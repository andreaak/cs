using System.Diagnostics;

namespace CSTest._07_Generics._0_Setup
{
    class GenericClass<T>
    {
        // Поля
        public static T staticField;
        public T field;

        // Свойства.
        public T Field
        {
            get { return this.field; }
            set { this.field = value; }
        }

        static GenericClass()
        {
            staticField = default(T);
        }

        public GenericClass()
        {
            this.field = default(T);
        }

        // Конструктор.
        public GenericClass(T field)
        {
            this.field = field;
        }

        public void Method()
        {
            Debug.WriteLine(field.GetType());
        }

        public static void StaticMethod()
        {
            Debug.WriteLine(staticField.GetType());
        }
    }

    abstract class GenericAbstractClass<T>
    {
        // Поля
        public T field = default(T);
    }

    class GenericDerivedClass<T> : GenericAbstractClass<T>
    {
    }

    //Использование одинакового имени но разных параметров типов
    abstract class GenericAbstractClass<T, K>
    {
        // Поля
        public T field1 = default(T);
        public K field2 = default(K);
    }

    //Частичное закрытие обобщения
    class GenericDerivedClass2<T> : GenericAbstractClass<T, string>
    {
    }

    // Частичные классы и методы.
    public partial class GenericPartialClass<T>
    {
        partial void PartialMethod<T>(T a, ref T b);
    }

    public partial class GenericPartialClass<T>
    {
        partial void PartialMethod<T>(T a, ref T b)
        {
            b = default(T);
            Debug.WriteLine("{0}, {1}", a, b);
        }

        public void Proxy(T a, ref T b)
        {
            PartialMethod(a, ref b);
        }
    }

    struct GenericStruct<T>
    {
        // Поля
        public T field;// = default(T)
    }
}

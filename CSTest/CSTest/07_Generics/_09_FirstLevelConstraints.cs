using CSTest._07_Generics._0_Setup;

namespace CSTest._07_Generics
{
    // where T : class  -   Аргумент типа должен иметь ссылочный тип, это также распространяется на тип любого класса, интерфейса, делегата или массива.
    class GenericClassWithConstraint<T> where T : class
    {
        public T variable;
    }

    // where T : struct  -  Аргумент типа должен иметь тип значения. Допускается указание любого типа значения, кроме Nullable.
    class GenericStructWithConstraint<T> where T : struct
    {
        public T variable;
    }

    // where T : Base -  Аргумент типа должен являться или быть производным от указанного базового класса.
    class GenericClassWithBaseConstraint<T> where T : Base
    {
    }
}

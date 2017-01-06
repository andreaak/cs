namespace CSTest._07_Generics._0_Setup.Constraints
{

    interface IInterfaceConstraint { /* ... */ }
    interface IInterfaceConstraint<U> { /* ... */ }

    class DerivedInterfaceConstraints : IInterfaceConstraint, IInterfaceConstraint<object> { /* ... */ }

    // where T : IInterfaceConstraint, IInterfaceConstraint<object>  -  
    // Аргумент типа должен являться или реализовывать указанный интерфейс. 
    // Можно установить несколько ограничений интерфейса.
    // Ограничивающий интерфейс также может быть универсальным.
    class GenericInterfaceConstraint<T> where T : IInterfaceConstraint, IInterfaceConstraint<object> { /* ... */ }

    class GenericInterfaceConstraint2<T> where T : IInterfaceConstraint { /* ... */ }

    class GenericInterfaceConstraint3<T> where T : IInterfaceConstraint<object> { /* ... */ }


    interface IInterfaceConstraint2 { /* ... */ }
    interface IInterfaceConstraint2<U> : IInterfaceConstraint2 { /* ... */ }

    class DerivedInterfaceConstraints2 : IInterfaceConstraint2, IInterfaceConstraint2<object> { /* ... */ }

    class DerivedInterfaceConstraints3 : IInterfaceConstraint2<object> { /* ... */ }

    // where T : IInterfaceConstraint2, IInterfaceConstraint2<object>  -  
    // Аргумент типа должен являться или реализовывать указанный интерфейс. 
    // Можно установить несколько ограничений интерфейса. Ограничивающий интерфейс также может быть универсальным.

    class GenericInterfaceConstraint4<T> where T : IInterfaceConstraint2, IInterfaceConstraint2<object> { /* ... */ }

    class GenericInterfaceConstraint5<T> where T : IInterfaceConstraint2<object> { /* ... */ }

    // Ограничения параметров типа - "naked"
    // Аргумент типа, предоставляемый в качестве T, должен совпадать с аргументом, 
    //предоставляемым в качестве U, или быть производным от него.
    // Это называется ограничением типа "Naked".

    class GenericNakedConstraint<T, R, U> where T : U
    {
    }

}

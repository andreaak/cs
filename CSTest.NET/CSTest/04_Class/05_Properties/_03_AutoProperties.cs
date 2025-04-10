namespace CSTest._04_Class._05_Properties
{

    /*
    Автоматически реализуемые свойства (Auto-Implemented properties). 

    Автоматически реализуемые свойства это более лаконичная форма свойств, их есть смысл использовать,
    когда в методах доступа (get и set) не требуется дополнительная логика. 
    При создании автоматически реализуемых свойств, компилятор создаст закрытое, анонимное резервное поле, 
    которое будет доступно с помощью методов get и set свойства.
    */
    class _03_AutoProperties<T>
    {
        // Автоматически реализуемые свойства. 
        public string Name { get; set; }
        public string Book { get; set; }
        public string BookProtected { get; protected set; }

        public T BookT { get; set; }
        public static string BookS { get; set; }
    }
}

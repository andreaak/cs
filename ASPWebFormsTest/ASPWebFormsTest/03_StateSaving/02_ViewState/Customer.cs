using System;

namespace ASPWebFormsTest._03_StateSaving._02_ViewState
{
    /* Трбования к классу который будет сериализоваться.
     * 
     *      1. Класс должен иметь атрибут Serializable.
     *      2. Все базовые типы также должны  иметь атрибут Serializable
     *      3. Все поля и свойства данного класса должны поддерживать сериализацию
     *      или должны быть помечены атрибутом NonSerialized (что означает, что во время
     *      сериализации они будут игнорироваться)
     */

    [Serializable]
    public class Customer
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public Customer(string userName, string email)
        {
            this.UserName = userName;
            this.Email = email;
        }
    }
}
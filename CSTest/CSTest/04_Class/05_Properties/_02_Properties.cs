using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace CSTest._04_Class._05_Properties
{
    /*
    Свойство — интерфейс доступа к полю объекта. 
    Свойства в C# — поля с логическим блоком, в котором есть ключевые слова get и set
    и являются суррогатом для замены методов доступа к полю. 
    При обращении к свойству вызывается определённый метод, который выполняет определённые операции с объектом.
    */
    class _02_Properties
    {
        private string field = null;

        public string Field
        {
            set  // void SetField(string value)   -    Метод-мутатор - mutator   (setter)
            {
                field = value;
            }

            get  // string GetField()             -    Метод-аксессор - accessor (getter)
            {
                return field;
            }
        }

        public string Field2
        {
            set
            {
                if (value == "Goodbye")
                    Debug.WriteLine("Вы ввели недопустимое значение. Повторите попытку.");
                else
                    field = value;
            }

            get
            {
                if (field == null)
                    return "В поле field отсутствуют данные.";
                else if (field == "hello world")
                    return field.ToUpper() + "!";
                else
                    return field;
            }
        }


        // Свойство только для записи.  - WriteOnly Property
        private double pi = 3.14D;
        public double Pi
        {
            set { pi = value; }
        }

        // Свойство только для чтения.  -  ReadOnly Property
        private double e = 2.71D;
        public double E
        {
            get { return e; }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSTest._01_Elements_CS._01_Types._0_SetUp
{
    delegate dynamic DynamicDelegate(dynamic argument);
    delegate R DynamicGenericDelegate<T, R>(T argument);
    delegate dynamic EventDelegate(dynamic sender, dynamic e);

    class DynamicClass
    {
        public dynamic field;

        public DynamicClass()
        {

        }

        public DynamicClass(dynamic argument)
        {
            field = argument;
        }

        // Динамические свойства.

        public dynamic MyAutoProperty { get; set; }

        public dynamic Field
        {
            get { return field; }
            set { field = value; }
        }

        // Динамические методы.

        public dynamic Add(dynamic a, dynamic b)
        {
            return a + b;
        }

        public dynamic Method(dynamic argument)
        {
            return argument;
        }

        public static dynamic StaticMethod(dynamic argument)
        {
            return "Hello " + argument + "!";
        }

        // Динамические массивы и индексаторы.

        dynamic[] array = new dynamic[3];

        public dynamic this[dynamic index]
        {
            get { return array[index]; }
            set { array[index] = value; }
        }

        dynamic myEvent;
        //Событийная модель не поддерживает динамическую типизацию
        //public event dynamic MyEvent
        public event EventDelegate MyEvent
        {
            add { myEvent += value; }
            remove { myEvent -= value; }
        }

        public dynamic Method(dynamic sender, dynamic e)
        {
            myEvent.Invoke(sender, e);
            return default(dynamic);
        }

        public static dynamic FactoryMethod()
        {
            return new DynamicClass();
        }

        public static dynamic RefOutMethod(ref dynamic argument1, out dynamic argument2)
        {
            argument1 = ++argument1;
            argument2 = 2;

            return default(dynamic);
        }

        // Один из параметров бинарного оператора, должен иметь существующий тип.
        //public static dynamic operator +(dynamic pointA, dynamic pointB) - так недопустимо.
        public static dynamic operator +(DynamicClass pointA, dynamic pointB)
        {
            return new DynamicClass(pointA.field + pointB.field);
        }

        // В унарных операторах недопустимо использовать динамические типы (вообще).
        // public static dynamic operator ++(dynamic pointA) - так недопустимо.
        public static DynamicClass operator ++(DynamicClass pointA)
        {
            return new DynamicClass(pointA.field + 1);
        }

        public override string ToString()
        {
            return string.Format("field = {0}", field);
        }

        public static IEnumerable Generator()
        {
            yield return new { Key = 0, Value = "Zero" };
            yield return new { Key = 1, Value = "One" };
            yield return new { Key = 2, Value = "Two" };
        }
    }

    class Derived : DynamicClass
    {

    }
}

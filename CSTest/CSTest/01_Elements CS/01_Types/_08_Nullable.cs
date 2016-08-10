using NUnit.Framework;
using System.Diagnostics;

namespace CSTest._01_Elements_CS._01_Types
{
    [TestFixture]
    public class _08_Nullable
    {
        [Test]
        public void TestNullable1()
        {
            int? j = 0;//call instance void valuetype [mscorlib]System.Nullable`1<int32>::.ctor(!0)
            int? l = new int();//call instance void valuetype [mscorlib]System.Nullable`1<int32>::.ctor(!0)
            int? m = default(int);//call instance void valuetype [mscorlib]System.Nullable`1<int32>::.ctor(!0)
            int? n = default(int?);//initobj valuetype [mscorlib]System.Nullable`1<int32>
            int? i = null;//initobj valuetype [mscorlib]System.Nullable`1<int32>
            int? k = 5;//call instance void valuetype [mscorlib]System.Nullable`1<int32>::.ctor(!0
        }

        [Test]
        public void TestNullable2()
        {
            int? count = null;
            DisplayValue(count);
            count = 100;
            DisplayValue(count);
        }

        [Test]
        public void TestNullable3()
        {
            int? count = null;
            int? result = null;
            int incr = 10; // переменная incr не является обнуляемой 
            // переменная result содержит пустое значение, 
            // переменная оказывается count пустой, 
            result = count + incr;
            DisplayValue(result);
            // Теперь переменная count получает свое"значение, и поэтому 
            // переменная result будет содержать конкретное значение, 
            count = 100;
            result = count + incr;
            DisplayValue(result);
            bool? op1 = true;
            bool op2 = true;
            var res = op1 & op2;
            /*
            У переменной result отсутствует значение
            Переменная result имеет следующее значение: 110
            */
        }

        /*
        когда два обнуляемых объекта сравниваются в операциях сравнения <,>,<= или >=, 
        то их результат будет ложным, если любой из обнуляемых объектов оказывается пустым, 
        т.е. содержит значение null
        */
        [Test]
        public void TestNullable4()
        {
            int? count = null;
            int? result = null;
            count = 100;
            Debug.WriteLine(count < 101);
            Debug.WriteLine(count < result);
            bool? bl1 = null;
            bool? bl2 = null;
            //var res = (bl1 && bl2);// Operator '&&' || cannot be applied to operands of type 'bool?' and 'bool?'
            /*
            True
            False
            */
        }

        [Test]
        public void TestNullable5()
        {
        }

        [Test]
        public void TestNullable6()
        {
        }

        private static void DisplayValue(int? count)
        {
            if (count.HasValue)
                Debug.WriteLine("Переменная count имеет следующее значение: " + count.Value);
            else
                Debug.WriteLine("У переменной count отсутствует значение");
        }

        private static void DisplayValue2(int? count)
        {
            if (count != null)
                Debug.WriteLine("Переменная count имеет следующее значение: " + count.Value);
            else
                Debug.WriteLine("У переменной count отсутствует значение");
        }

        private static void DisplayValue3(int? count)
        {
            if (count == null)
                Debug.WriteLine("Переменная count имеет следующее значение: " + count.Value);
            else
                Debug.WriteLine("У переменной count отсутствует значение");
        }
    }
}

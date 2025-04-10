using System.Diagnostics;
using NUnit.Framework;

namespace CSTest._03_Structure._03_Nullable
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void TestNullable1Initialization()
        {
            int? j = 0;//call instance void valuetype [mscorlib]System.Nullable`1<int32>::.ctor(!0)
            int? l = new int();//call instance void valuetype [mscorlib]System.Nullable`1<int32>::.ctor(!0)
            int? m = default(int);//call instance void valuetype [mscorlib]System.Nullable`1<int32>::.ctor(!0)
            int? n = default(int?);//initobj valuetype [mscorlib]System.Nullable`1<int32>
            int? i = null;//initobj valuetype [mscorlib]System.Nullable`1<int32>
            int? k = 5;//call instance void valuetype [mscorlib]System.Nullable`1<int32>::.ctor(!0
            //другая форма записи
            System.Nullable<int> count = null;
            System.Nullable<int> count2 = new System.Nullable<int>();
        }

        [Test]
        public void TestNullable2CheckOnNull()
        {
            int? count = null;
            Debug.WriteLine("Value: " + (count.HasValue ? count.Value.ToString() : "Null"));
            Debug.WriteLine("Value: " + (count != null ? count.Value.ToString() : "Null"));
            Debug.WriteLine("Default Value: " + count.GetValueOrDefault());
            Debug.WriteLine("Default Value with param: " + count.GetValueOrDefault(77));
            Debug.WriteLine("?? Value: " + (count ?? 88));

            //An exception of type 'System.InvalidOperationException' occurred
            //Additional information: Nullable object must have a value.
            //var temp = count.Value;
            //var temp = (int) count;

            count = 100;
            Debug.WriteLine("Value: " + (count.HasValue ? count.Value.ToString() : "Null"));
            Debug.WriteLine("Value: " + (count != null ? count.Value.ToString() : "Null"));
            Debug.WriteLine("Default Value: " + count.GetValueOrDefault());
            Debug.WriteLine("Default Value with param: " + count.GetValueOrDefault(77));
            /*
            Value: Null
            Value: Null
            Default Value: 0
            Default Value with param: 77
            ?? Value: 88
            Value: 100
            Value: 100
            Default Value: 100
            Default Value with param: 100
            */
        }

        [Test]
        public void TestNullable3Operations()
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
        public void TestNullable4Comparison()
        {
            int? count = null;
            int? result = null;
            count = 100;
            Debug.WriteLine(count < 101);
            Debug.WriteLine(count < result);
            Debug.WriteLine(count > result);
            bool? bl1 = null;
            bool? bl2 = null;
            //var res = (bl1 && bl2);// Operator '&&' || cannot be applied to operands of type 'bool?' and 'bool?'
            /*
            True
            False
            False
            */
        }

        //Decompiled
        /*
        [NUnit.Framework.Test]
        public void TestNullable1Initialization()
        {
          int? nullable1 = new int?(0);
          int? nullable2 = new int?(0);
          int? nullable3 = new int?(0);
          int? nullable4 = new int?();
          int? nullable5 = new int?();
          int? nullable6 = new int?(5);
          int? nullable7 = new int?();
          int? nullable8 = new int?();
        }

        [NUnit.Framework.Test]
        public void TestNullable2CheckOnNull()
        {
          int? nullable = new int?();
          string str1 = "Value: ";
          int num;
          string str2;
          if (!nullable.HasValue)
          {
            str2 = "Null";
          }
          else
          {
            num = nullable.Value;
            str2 = num.ToString();
          }
          Debug.WriteLine(str1 + str2);
          string str3 = "Value: ";
          string str4;
          if (!nullable.HasValue)
          {
            str4 = "Null";
          }
          else
          {
            num = nullable.Value;
            str4 = num.ToString();
          }
          Debug.WriteLine(str3 + str4);
          Debug.WriteLine("Default Value: " + (object) nullable.GetValueOrDefault());
          Debug.WriteLine("Default Value with param: " + (object) nullable.GetValueOrDefault(77));
          Debug.WriteLine("?? Value: " + (object) (nullable ?? 88));
          nullable = new int?(100);
          string str5 = "Value: ";
          string str6;
          if (!nullable.HasValue)
          {
            str6 = "Null";
          }
          else
          {
            num = nullable.Value;
            str6 = num.ToString();
          }
          Debug.WriteLine(str5 + str6);
          string str7 = "Value: ";
          string str8;
          if (!nullable.HasValue)
          {
            str8 = "Null";
          }
          else
          {
            num = nullable.Value;
            str8 = num.ToString();
          }
          Debug.WriteLine(str7 + str8);
          Debug.WriteLine("Default Value: " + (object) nullable.GetValueOrDefault());
          Debug.WriteLine("Default Value with param: " + (object) nullable.GetValueOrDefault(77));
        }

        [NUnit.Framework.Test]
        public void TestNullable3Operations()
        {
          int? nullable1 = new int?();
          int? nullable2 = new int?();
          int num1 = 10;
          int? nullable3 = nullable1;
          int num2 = num1;
          Test.DisplayValue(nullable3.HasValue ? new int?(nullable3.GetValueOrDefault() + num2) : new int?());
          nullable1 = new int?(100);
          nullable3 = nullable1;
          int num3 = num1;
          Test.DisplayValue(nullable3.HasValue ? new int?(nullable3.GetValueOrDefault() + num3) : new int?());
          bool? nullable4 = true ? new bool?(true) : new bool?(false);
        }

        [NUnit.Framework.Test]
        public void TestNullable4Comparison()
        {
          int? nullable1 = new int?();
          int? nullable2 = new int?();
          nullable1 = new int?(100);
          int? nullable3 = nullable1;
          int num = 101;
          Debug.WriteLine((object) (bool) (nullable3.GetValueOrDefault() < num ? (nullable3.HasValue ? 1 : 0) : 0));
          nullable3 = nullable1;
          int? nullable4 = nullable2;
          Debug.WriteLine((object) (bool) (nullable3.GetValueOrDefault() < nullable4.GetValueOrDefault() ? (nullable3.HasValue & nullable4.HasValue ? 1 : 0) : 0));
          nullable4 = nullable1;
          nullable3 = nullable2;
          Debug.WriteLine((object) (bool) (nullable4.GetValueOrDefault() > nullable3.GetValueOrDefault() ? (nullable4.HasValue & nullable3.HasValue ? 1 : 0) : 0));
          bool? nullable5 = new bool?();
          bool? nullable6 = new bool?();
        } 
        */

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
    }
}

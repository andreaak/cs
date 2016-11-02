using CSTest._04_Class._07_Inheritance.Setup;
using NUnit.Framework;

namespace CSTest._01_Elements_CS._05_RTTI
{
    class ClassA { /* ... */ }

    class ClassB : ClassA { /* ... */ }

    [TestFixture]
    public class _01_IsAs
    {
        [Test]
        public void TestCasting1Is()
        {
            ClassB b = new ClassB();
            ClassA a = null;

            //--------------------------------------------- is --------------------------------------------- 
            // Оператор is - проверяет совместимость объекта с заданным типом. 
            // Если предоставленный объект может быть приведен к предоставленному типу не вызывая исключение,
            // выражение is принимает значение true.

            // Например, в следующем коде определяется, является ли объект экземпляром типа A или типа, производного от A:

            if (b is ClassA)
            {
                a = (ClassA)b;
            }
            else
            {
                a = null;
            }

            //--------------------------------------------- as---------------------------------------------  
            // Оператор as используется для выполнения преобразований между совместимыми ссылочными типами.
            // Оператор as подобен оператору приведения. Однако, если преобразование невозможно,
            // as возвращает значение null, а не вызывает исключение.

            // В общем виде логика работы оператора as представляет собой механизм использования оператора is
            // (пример на 25 строке), только в сокращенном виде.

            a = b as ClassA;
        }

        [Test]
        public void TestCasting2As()
        {
            DerivedClass instance = new DerivedClass();
            instance.Method();

            // UpCast
            BaseClass instanceUp = instance as BaseClass;
            if (instanceUp != null)
            {
                instanceUp.Method();
            }

            // DownCast
            DerivedClass instanceDown = instanceUp as DerivedClass;
            if (instanceDown != null)
            {
                instanceDown.Method();
            }
        }
    }
}

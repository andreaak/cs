using CSTest._14_Garbage_Collection._0_Setup;
using NUnit.Framework;
using System;

namespace CSTest._14_Garbage_Collection
{
    [TestFixture]
    public class _01_DisposeTest
    {
        [Test]
        public void TestDispose1()
        {
            using (DisposableObject theInstance = new DisposableObject())
            /*
            Type used in a using statement must be implicitly convertible to 'System.IDisposable'

            If the form of resource-acquisition is local-variable-declaration 
            then the type of the local-variable-declaration must be System.IDisposable 
            or a type that can be implicitly converted to System.IDisposable. 

            If the form of resource-acquisition is expression then this expression must be of type System.IDisposable 
            or a type that can be implicitly converted to System.IDisposable.
            */
            {
                // выполнить работу 
                theInstance.Test();
                /*В рамках блока using объект доступен только для чтения и не может быть изменен или переназначен.*/
                //theInstance = null;//Cannot assign to 'theInstance' because it is a 'using variable
            }
            //theInstance.Test(); //The name 'theInstance' does not exist in the current context

            /*
            Test in DisposableObject
            Dispose in DisposableObject
            */
        }

        [Test]
        public void TestDispose1Compiled()
        {
            DisposableObject theInstance = new DisposableObject();
            try
            {
                theInstance.Test();
            }
            finally
            {
                // Явное закрытие файла после записи
                if (theInstance != null)
                {
                    /*
                    The C# language spec provides three possible expansions for the using statement's syntactic sugar:
                    - When theInstance is a non-nullable value type
                    - When theInstance is a nullable value type or a reference type other than dynamic
                    - When theInstance is dynamic
                    */
                    ((IDisposable)theInstance).Dispose();//
                    //or
                    //IDisposable disposable = theInstance;
                    //disposable.Dispose();
                }
            }

            /*
            Test in DisposableObject
            Dispose in DisposableObject
            */
        }

        [Test]
        public void TestDispose2Struct()
        {
            using (DisposableStruct theInstance = new DisposableStruct())
            {
                theInstance.Test();
            }
            /*
            Test in DisposableStruct
            Dispose in DisposableStruct
            */
        }

        [Test]
        public void TestDispose3Dynamic()
        {
            using (dynamic theInstance = new DisposableObject())
            {
                theInstance.Test();
            }
            /*
            Test in DisposableObject
            Dispose in DisposableObject
            */
        }

        [Test]
        public void TestDispose4NotIDisposable()
        {
            //'NotIDisposableObject': type used in a using statement must be implicitly convertible to 'System.IDisposable'
            //using (NotIDisposableObject theInstance = new NotIDisposableObject())
            //{
            //    theInstance.Test();
            //}
        }

        [Test]
        public void TestDispose5Null()
        {
            using (DisposableObject theInstance = GetInstance())
            {
                if (theInstance != null)
                {
                    theInstance.Test();
                }
            }
        }

        private DisposableObject GetInstance()
        {
            return null;
        }

        /*
        Можно создать объект ресурсов, а затем передать переменную в оператор using, 
        однако этот способ не является рекомендованным.
        В этом случае после того, как элемент управления выводится из блока using, объект остается в области действия, 
        даже если он больше не сможет обращаться к неуправляемым ресурсам.Другими словами, он больше не будет полностью инициализирован.
        При попытке использовать объект вне блока using возникает риск вызова исключения.
        */
        [Test]
        public void TestDispose6NotRecomended()
        {
            DisposableObject theInstance = new DisposableObject();
            using (theInstance)
            {
                // выполнить работу 
                theInstance.Test();
                theInstance = null;//Warning: Possibly incorrect assignment to local 'theInstance' which is the argument to a using or lock statement. 
                //The Dispose call or unlocking will happen on the original value of the local.
            }
            theInstance.Test(); // theInstance is still in scope but the method call throws an exception

            /*
            Test in DisposableObject
            Dispose in DisposableObject
            Exception thrown: 'System.NullReferenceException'  
            */
        }
    }
}

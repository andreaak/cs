using CSTest._14_Garbage_Collection._0_Setup;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace CSTest._14_Garbage_Collection
{
    [TestClass]
    public class _01_Dispose
    {
        /*
        */
        [TestMethod]
        public void TestDispose1()
        {
            using (ResourceGobbler theInstance = new ResourceGobbler())
            /*
            type used in a using statement must be implicitly convertible to 'System.IDisposable'
            If the form of resource-acquisition is local-variable-declaration then the type of the local-variable-declaration must be System.IDisposable 
            or a type that can be implicitly converted to System.IDisposable. 
            If the form of resource-acquisition is expression then this expression must be of type System.IDisposable 
            or a type that can be implicitly converted to System.IDisposable.
            */
            {
                // выполнить работу 
                theInstance.Test();
                //theInstance = null;//Cannot assign to 'theInstance' because it is a 'using variable
                /*
                В рамках блока using объект доступен только для чтения и не может быть изменен или переназначен.
                */
            }
            //theInstance.Test(); //The name 'theInstance' does not exist in the current context
        }

        [TestMethod]
        public void TestDispose2Struct()
        {
            using (ResourceGobblerStruct theInstance = new ResourceGobblerStruct())
            {
                theInstance.Test();
            }
        }

        [TestMethod]
        public void TestDispose3Dynamic()
        {
            using (dynamic theInstance = new object())
            {
                theInstance.Test();
            }
        }

        [TestMethod]
        public void TestDispose4Compiled()
        {
            ResourceGobbler theInstance = new ResourceGobbler();
            try
            {
                // выполнить работу 
                theInstance.Test();
            }
            finally
            {
                // Явное закрытие файла после записи
                if (theInstance != null)
                {
                    /*
                    The C# language spec provides three possible expansions for the using statement's syntactic sugar:
                    When theInstance is a non-nullable value type
                    When theInstance is a nullable value type or a reference type other than dynamic
                    When theInstance is dynamic
                    */
                    ((IDisposable)theInstance).Dispose();//
                }
            }
        }
        
        [TestMethod]
        public void TestDispose5NotRecomended()
        {
            ResourceGobbler theInstance = new ResourceGobbler();
            using (theInstance)
            {
                // выполнить работу 
                theInstance.Test();
                theInstance = null;//Possibly incorrect assignment to local 'theInstance' which is the argument to a using or lock statement. 
                //The Dispose call or unlocking will happen on the original value of the local.
            }
            theInstance.Test(); // theInstance is still in scope but the method call throws an exception
            /*
            Можно создать объект ресурсов, а затем передать переменную в оператор using, 
            однако этот способ не является рекомендованным.
            В этом случае после того, как элемент управления выводится из блока using, объект остается в области действия, 
            даже если он больше не сможет обращаться к неуправляемым ресурсам.Другими словами, он больше не будет полностью инициализирован.
            При попытке использовать объект вне блока using возникает риск вызова исключения.
            */
        }
    }
}

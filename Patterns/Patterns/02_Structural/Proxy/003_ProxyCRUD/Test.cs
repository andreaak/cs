using System;
using System.Diagnostics;
using NUnit.Framework;

namespace Patterns._02_Structural.Proxy._003_ProxyCRUD
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            Subject subject = new RealSubject();
            Subject proxy;

            Debug.WriteLine("Owner work:");
            proxy = new Proxy(subject, "Owner");
            TryAccess(proxy, "John");

            Debug.WriteLine("Administrator work:");
            proxy = new Proxy(subject, "Administrator");
            TryAccess(proxy, "Mark");

            Debug.WriteLine("Manager work:");
            proxy = new Proxy(subject, "Manager");
            TryAccess(proxy, "Lola");

            Debug.WriteLine("User work:");
            proxy = new Proxy(subject, "User");
            TryAccess(proxy, "Gigi");

            // Delay.
            //Console.ReadKey();
        }

        static void TryAccess(Subject proxy, string name)
        {
            try
            {
                proxy.Create(name, "TestValue");
                Debug.WriteLine("Create - OK!");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            try
            {
                proxy.Read("TestKey");
                Debug.WriteLine("Read - OK!");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            try
            {
                proxy.Update(name, "NewTestValue");
                Debug.WriteLine("Update - OK!");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            try
            {
                proxy.Delete(name);
                Debug.WriteLine("Delete - OK!");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            Debug.WriteLine(new string('-', 50));
        }
    }
}

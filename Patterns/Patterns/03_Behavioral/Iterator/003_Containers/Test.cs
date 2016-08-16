using System;
using System.ComponentModel;
using System.Diagnostics;
using NUnit.Framework;

// В данном примере используются пользовательские классы Container, Component и Site.

namespace Patterns._03_Behavioral.Iterator._003_Containers
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            Container container = new Container();

            Component component1 = new Component();
            Component component2 = new Component();
            Component component3 = new Component();

            container.Add(component1, "First");
            container.Add(component2, "Second");
            container.Add(component3, "Third");

            ComponentCollection components = container.Components;

            foreach (Component component in components)
            {
                Debug.WriteLine("Component : " + component.Site.Name);
            }

            component1.Disposed += (object sender, EventArgs e) =>
                Debug.WriteLine("First Component Disposed");

            container.Dispose();

            // Delay.
            //Console.ReadKey();
        }
    }
}

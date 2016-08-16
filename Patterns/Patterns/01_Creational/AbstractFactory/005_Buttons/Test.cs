using System.Windows.Forms;
using NUnit.Framework;
using Patterns._01_Creational.AbstractFactory._005_Buttons.Factories;

namespace Patterns._01_Creational.AbstractFactory._005_Buttons
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            Client client = null;
            //client = new Client(new WindowsFactory());
            client = new Client(new LeopardFactory());

            Application.EnableVisualStyles();
            Application.Run(client.GetForm());
        }
    }
}

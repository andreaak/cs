using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Patterns._01_Creational.AbstractFactory._005_Buttons.Factories;

namespace Patterns._01_Creational.AbstractFactory._005_Buttons
{
    [TestClass]
    public class Test
    {
        [TestMethod]
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

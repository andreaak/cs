using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;

namespace Patterns.Creational.AbstractFactory.Example4
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

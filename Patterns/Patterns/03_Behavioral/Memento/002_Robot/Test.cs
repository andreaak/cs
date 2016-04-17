using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Patterns._03_Behavioral.Memento._002_Robot
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            Man David = new Man();
            Robot ASIMO = new Robot();

            David.�lothes = "��������, ������, ����";

            ASIMO.Backpack = David.Undress();

            David.�lothes = "����� ����������";

            David.Dress(ASIMO.Backpack);
        }
    }
}

using NUnit.Framework;

namespace Patterns._03_Behavioral.Memento._002_Robot
{
    [TestFixture]
    public class Test
    {
        [Test]
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

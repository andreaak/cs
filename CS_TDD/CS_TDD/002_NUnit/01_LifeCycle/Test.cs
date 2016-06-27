using System.Diagnostics;
using NUnit.Framework;

namespace CS_TDD._002_NUnit._01_LifeCycle
{
    [TestFixture]   // TestFixture -  указывает, что класс содержит тестовый код.
    public class Test : AssertionHelper
    {
        // Метод помеченый атрибутом TestFixtureSetUp будет выполняться один раз при инициализации класса
        [TestFixtureSetUp]
        public void TestFixtureSetUpMethod()
        {
            Debug.WriteLine("TestFixtureSetUpMethod");
        }

        // Метод помеченый атрибутом SetUp будет выполняться при запуске каждого нового теста
        [SetUp]
        public void SetUpMethod()
        {
            Debug.WriteLine("SetUpMethod");
        }

        // Метод помеченый атрибутом TearDown будет выполняться при окончании каждого теста 
        [TearDown]
        public void TearDownMethod()
        {
            Debug.WriteLine("TearDownMethod");
        }

        // Метод помеченый атрибутом TestFixtureTearDown будет выполняться один раз после выполнения всех тестов
        [TestFixtureTearDown]
        public void TestFixtureTearDownMethod()
        {
            Debug.WriteLine("TestFixtureTearDownMethod");
        }

        [Test]
        public void Test1()
        {
            Debug.WriteLine("Test1");
        }

        [Test]
        public void Test2()
        {
            Debug.WriteLine("Test2");
        }


        /*
        TestFixtureSetUpMethod
        
        SetUpMethod
        Test1
        TearDownMethod
        
        SetUpMethod
        Test2
        TearDownMethod
        
        TestFixtureTearDownMethod 
         */
    }
}

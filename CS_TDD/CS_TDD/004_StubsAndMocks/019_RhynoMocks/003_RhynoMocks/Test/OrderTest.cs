using NUnit.Framework;
using Rhino.Mocks;

namespace CS_TDD._004_StubsAndMocks._019_RhynoMocks._003_RhynoMocks.Test
{
    [TestFixture]
    class OrderTest
    {
        [Test]
        public void RhinoMocksStubTest_1()
        {
            Order order = new Order("Keyboard");
            var stubUserRepository = MockRepository.GenerateStub<Warehouse>();

            stubUserRepository.Stub(x => x.HasInventory("Keyboard")).Return(true);
            stubUserRepository.Stub(x => x.Remove("Keyboard"));

            order.Fill(stubUserRepository);
            Assert.IsTrue(order.IsFilled);
        }
    }
}

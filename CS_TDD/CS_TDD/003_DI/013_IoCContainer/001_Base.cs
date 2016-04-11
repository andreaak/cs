using CS_TDD._003_DI._013_IoCContainer.Application;
using NUnit.Framework;

namespace CS_TDD._003_DI._013_IoCContainer
{
    [TestFixture]
    class _001_Base
    {
        [Test]
        public void ResolverTest()
        {
            //ICreditCard card = new MasterCard();
            ICreditCard card = new Visa();
            Shopper shopper = new Shopper(card);
            shopper.Charge();
        }
    }
}

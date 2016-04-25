using CS_TDD._003_DI._000_Base;
using CS_TDD._003_DI._000_Base.Test;
using CS_TDD._003_DI._002_ConstructorInjection;
using CS_TDD._003_DI._013_IoCContainer.Application;
using Ninject;
using Ninject.Activation;
using NUnit.Framework;
using System.Diagnostics;

namespace CS_TDD._003_DI._010_Ninject.Test
{
    [TestFixture]
    class FileManagerTest
    {
        [Test]
        public void NinjectTestBase()
        {
            // Ninject Initialization
            IKernel ninjectKernel = new StandardKernel();
            // Bind dependencies
            ninjectKernel.Bind<IDataAccessObject>().To<StubFileDataObject>();
            ninjectKernel.Bind<FileManager>().ToSelf();

            FileManager manager = ninjectKernel.Get<FileManager>();

            Assert.IsTrue(manager.FindLogFile("file2.log"));
        }
        
        [Test]
        public void NinjectTestModules()
        {
            // Ninject Initialization
            IKernel ninjectKernel = new StandardKernel(new ConfigFileObjectData());

            FileManager manager = ninjectKernel.Get<FileManager>();

            Assert.IsTrue(manager.FindLogFile("file2.log"));
        }

        [Test]
        public void NinjectTestRebind()
        {
            // Ninject Initialization
            IKernel ninjectKernel = new StandardKernel();
            // Bind dependencies
            ninjectKernel.Bind<ICreditCard>().To<MasterCard>();
            Shopper shopper = ninjectKernel.Get<Shopper>();
            shopper.Charge();
            
            ninjectKernel.Rebind<ICreditCard>().To<Visa>();
            shopper = ninjectKernel.Get<Shopper>();
            shopper.Charge();

            /*
            Swiping the MasterCard!
            Chaaaarging with the Visa!
            */
        }

        [Test]
        public void NinjectTestNamedBind()
        {
            // Ninject Initialization
            IKernel ninjectKernel = new StandardKernel();
            // Bind dependencies
            ninjectKernel.Bind<ICreditCard>().To<Visa>().Named("VisaName");
            ninjectKernel.Bind<ICreditCard>().To<MasterCard>().Named("MC");
            ICreditCard card = ninjectKernel.Get<ICreditCard>("VisaName");
            Debug.WriteLine(card.Charge());
            card = ninjectKernel.Get<ICreditCard>("MC");
            Debug.WriteLine(card.Charge());
            /*
            Chaaaarging with the Visa!
            Swiping the MasterCard!
            */
        }

        [Test]
        public void NinjectTestLifecycle()
        {
            // Ninject Initialization
            IKernel ninjectKernel = new StandardKernel();
            // Bind dependencies
            ninjectKernel.Bind<ICreditCard>().To<MasterCard>();
            Shopper shopper = ninjectKernel.Get<Shopper>();
            shopper.Charge();
            Debug.WriteLine(shopper.creditCard.GetHashCode());

            Shopper shopper2 = ninjectKernel.Get<Shopper>();
            shopper2.Charge();
            Debug.WriteLine(shopper2.creditCard.GetHashCode());

            /*
            Swiping the MasterCard!
            20324077
            Swiping the MasterCard!
            61312212
            */
        }

        [Test]
        public void NinjectTestLifecycleSingleton()
        {
            // Ninject Initialization
            IKernel ninjectKernel = new StandardKernel();
            // Bind dependencies
            ninjectKernel.Bind<ICreditCard>().To<MasterCard>().InSingletonScope();
            Shopper shopper = ninjectKernel.Get<Shopper>();
            shopper.Charge();
            Debug.WriteLine(shopper.creditCard.GetHashCode());

            Shopper shopper2 = ninjectKernel.Get<Shopper>();
            shopper2.Charge();
            Debug.WriteLine(shopper2.creditCard.GetHashCode());

            /*
            Swiping the MasterCard!
            61312212
            Swiping the MasterCard!
            61312212
            */
        }

        [Test]
        public void NinjectTestBindMethod()
        {
            // Ninject Initialization
            IKernel ninjectKernel = new StandardKernel();
            // Bind dependencies
            ninjectKernel.Bind<ICreditCard>().ToMethod(CreationMethod);
            Shopper shopper = ninjectKernel.Get<Shopper>();
            shopper.Charge();

            /*
            MasterCard Creation!
            Swiping the MasterCard!
            */
        }

        private ICreditCard CreationMethod(IContext context)
        {
            Debug.WriteLine("MasterCard Creation!");
            return new MasterCard();
        }
    }
}

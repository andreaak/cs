using CSTest._07_Generics._0_Setup;
using NUnit.Framework;
using System.Diagnostics;
using CSTest._07_Generics._0_Setup.CoContrvariance;

namespace CSTest._07_Generics
{
    [TestFixture]
    public class _02_CoContrVarianceTests
    {
        [Test]
        public void TestGeneric6CovarianceInterface()
        {
            Circle circle = new Circle();
            IContainerCovariance<Shape> container = new ContainerCovariance<Circle>(circle);
            Debug.WriteLine(container.Figure.ToString());
        }

        [Test]
        public void TestGeneric7ContrvarianceInterface()
        {
            Shape shape = new Circle();
            IContainerContrvariance<Circle> container = new ContainerContrvariance<Shape>(shape);
            Debug.WriteLine(container.ToString());
        }

        [Test]
        public void TestGeneric8CovarianceDelegate()
        {
            CovarianceDelegate<Derived> delegateCat = new CovarianceDelegate<Derived>(_07_DelegateCovariance.CatCreator);
            CovarianceDelegate<Base> delegateAnimal = delegateCat;    // От производного к базовому.                      
            Base animal = delegateAnimal.Invoke();
            Debug.WriteLine(animal.GetType().Name);
        }

        [Test]
        public void TestGeneric9ContrvarianceDelegate()
        {
            ContrvarianceDelegate<Base> delegateAnimal = new ContrvarianceDelegate<Base>(_08_DelegateContrvariance.CatUser);
            ContrvarianceDelegate<Derived> delegateCat = delegateAnimal;    // От базового к производному.

            delegateAnimal(new Base());
            delegateCat(new Derived());

            //delegateCat(new Base()); // Невозможно.
        }
    }
}

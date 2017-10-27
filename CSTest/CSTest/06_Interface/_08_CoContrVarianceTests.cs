using System.Diagnostics;
using CSTest._06_Interface._0_Setup;
using NUnit.Framework;

namespace CSTest._06_Interface
{
    [TestFixture]
    public class _08_CoContrVarianceTests
    {
        [Test]
        public void TestInterfaceCoContrVariance1Covariance()
        {
            // Создать ссылку из интерфейса ICovariance на объект типа CovarianceClass<BaseCoContrClass>.
            // Это вполне допустимо как при наличии ковариантности, так и без нее. 
            ICovariance<BaseCoContrClass> baseClass = new CovarianceClass<BaseCoContrClass>(new BaseCoContrClass("Base"));
            Debug.WriteLine("Name: "  + baseClass.GetObject().GetName());

            //А теперь создать объект CovarianceClass<DerivedCoContrClass> и присвоить его переменной baseClass. 
            // Эта строка кода вполне допустима благодаря ковариантности.
            baseClass = new CovarianceClass<DerivedCoContrClass>(new DerivedCoContrClass("Derived"));
            Debug.WriteLine("Name: " + baseClass.GetObject().GetName());

            /*
            Name: Base
            Name: Derived
            */
        }

        [Test]
        public void TestInterfaceCoContrVariance2Contrvariance()
        {
            // Создать ссылку из интерфейса IContravariance<BaseCoContrClass> на объект типа CovarianceClass<BaseCoContrClass>. 
            // Это вполне допустимо как при наличии контравариантности, так и без нее. 
            IContravariance<BaseCoContrClass> baseClass = new CovarianceClass<BaseCoContrClass>(null);
            baseClass.Show(new BaseCoContrClass("Base"));
            // Создать ссылку из интерфейса IContravariance<DerivedCoContrClass> на объект типа CovarianceClass<DerivedCoContrClass>. 
            // И это вполне допустимо как при наличии контравариантности, так и без нее. 
            IContravariance<DerivedCoContrClass> derived = new CovarianceClass<DerivedCoContrClass>(null);
            // Этот вызов допустим как при наличии контравариантности, так и без нее. 
            derived.Show(new DerivedCoContrClass("Derived"));
            // Создать ссылку из интерфейса IContravariance<DerivedCoContrClass> на объект типа CovarianceClass<BaseCoContrClass>. 
            // Это вполне допустимо благодаря контравариантности.
            IContravariance<DerivedCoContrClass> derived2 = new CovarianceClass<BaseCoContrClass>(null);
            // Этот вызов допустим как при наличии контравариантности, так и без нее. 
            derived2.Show(new DerivedCoContrClass("Derived"));
            // Присвоить переменную derived переменной baseClass. 
            // Это вполне допустимо благодаря контравариантности.
            derived = baseClass;
            derived.Show(new DerivedCoContrClass("Derived"));

            /*
            CSTest._06_Interface._0_Setup.BaseCoContrClass
            CSTest._06_Interface._0_Setup.DerivedCoContrClass
            CSTest._06_Interface._0_Setup.DerivedCoContrClass
            CSTest._06_Interface._0_Setup.DerivedCoContrClass
            */
        }
    }
}

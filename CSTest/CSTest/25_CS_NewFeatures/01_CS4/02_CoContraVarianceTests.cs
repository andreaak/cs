using System.Collections.Generic;
using NUnit.Framework;

namespace CSTest._25_CS_NewFeatures._01_CS4
{

    [TestFixture]
    public class _02_CoContrVarianceTests
    {
        [Test]
        public void TestCovariance1Collections()
        {
            // List<Fruit> temp = new List<Banana>();//Cannot implicitly convert type 'List<Banana>' to 'List<Fruit>'
            // Problem
            List<Banana> bananas = new List<Banana>();
            //List<Fruit> fruits = bananas;//Если бы приведение было возможно, то в список бананов можно было бы добавить яблоко
            //fruits.Add(new Apple());
            //Banana banana = bananas[0];

            IEnumerable<Fruit> fruits = bananas;//IEnumerable не поддерживает добавление элементов
            //fruits.Add(new Apple());
        }

        [Test]
        public void TestCovariance2Interfaces()
        {
            ICovariance<Apple> apple = null;
            ICovariance<Fruit> fruit = apple;
            //ICovariance<Banana> banana = apple; //Cannot implicitly convert type ...
        }

        [Test]
        public void TestContraCovariance3Interfaces()
        {
            IContravariance<Fruit> fruit = null;
            IContravariance<Apple> apple = fruit;
            IContravariance<Banana> banana = fruit;
        }
    }

    class Fruit
    { }

    class Banana : Fruit
    { }

    class Apple : Fruit
    { }


    interface IVariance<T>
    {
        T GetInstance();
        void SetInstance(T inst);
    }

    interface ICovariance<out T>
    {
        T GetInstance();
        //void SetInstance(T inst);//Invalid variance: The type parameter 'T' must be contravariantly valid on 'ICovariance<T>.SetInstance(T)'. 
        //'T' is covariant
    }

    interface IContravariance<in T>
    {
        //T GetInstance();//Invalid variance: The type parameter 'T' must be covariantly valid on 'IContravariance<T>.GetInstance()'. 
        //'T' is contravariant
        void SetInstance(T inst);
    }
}

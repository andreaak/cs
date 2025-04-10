using System.Diagnostics;

namespace CSTest._06_Interface._0_Setup
{
    public interface ICovariance<out T>
    {
        T GetObject();

        //void M<V>() where V : T;
        //The type parameter 'T' must be contravariantly valid on 'ICovariance<T>.M<V>()'. 'T' is covariant
    }

    public interface IContravariance<in T>
    {
        void Show(T obj);
    }

    // Реализовать интерфейс ICovariance. 
    class CovarianceClass<T> : ICovariance<T>, IContravariance<T>
    {
        readonly T obj;

        public CovarianceClass(T v)
        {
            obj = v;
        }

        public T GetObject()
        {
            return obj;
        }

        public void Show(T obj)
        {
            Debug.WriteLine(obj);
        }
    }

    // Создать простую иерархию классов, 
    class BaseCoContrClass
    {
        string name;
        public BaseCoContrClass(string n)
        {
            name = n;
        }
        public string GetName()
        {
            return name;
        }
    }

    class DerivedCoContrClass : BaseCoContrClass
    {
        public DerivedCoContrClass(string n) : base(n)
        {
        }
    }
}
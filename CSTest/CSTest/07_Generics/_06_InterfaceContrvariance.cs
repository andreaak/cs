// Контрвариантность обобщений.
// Контрвариантность обобщений в C# 4.0 ограничена интерфейсами и делегатами.
namespace CSTest._07_Generics
{
    public interface IContainerContrvariance<in T>
    {
        T Figure { set; }
    }

    public class ContainerContrvariance<T> : IContainerContrvariance<T>
    {
        private T figure;

        public ContainerContrvariance(T figure)
        {
            this.figure = figure;
        }

        public T Figure
        {
            set { figure = value; }
        }

        public override string ToString()
        {
            return figure.GetType().ToString();
        }
    }
}

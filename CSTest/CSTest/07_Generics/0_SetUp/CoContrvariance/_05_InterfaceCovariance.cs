// Ковариантность обобщений.
// Ковариантность обобщений в C# 4.0 ограничена интерфейсами и делегатами.
namespace CSTest._07_Generics._0_Setup.CoContrvariance
{
    public interface IContainerCovariance<out T>
    {
        T Figure { get; }
    }

    public class ContainerCovariance<T> : IContainerCovariance<T>
    {
        private T figure;

        public ContainerCovariance(T figure)
        {
            this.figure = figure;
        }

        public T Figure
        {
            get { return figure; }
        }
    }
}

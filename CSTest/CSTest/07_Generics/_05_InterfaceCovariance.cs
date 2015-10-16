using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Ковариантность обобщений.
// Ковариантность обобщений в C# 4.0 ограничена интерфейсами и делегатами.
namespace CSTest._07_Generics
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

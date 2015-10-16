using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSTest._07_Generics
{
    public interface IContainer<T>
    {
        T Figure { get; set; }
    }

    public class Container<T> : IContainer<T>
    {
        public T Figure { get; set; }

        public Container(T figure)
        {
            this.Figure = figure;
        }
    }
}

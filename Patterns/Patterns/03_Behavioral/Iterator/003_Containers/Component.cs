using System;
using System.ComponentModel;

namespace Patterns._03_Behavioral.Iterator._003_Containers
{
    class Component : IComponent
    {
        // Реализация интерфейса IComponent.

        public virtual ISite Site { get; set; }
        public event EventHandler Disposed;

        // Реализация интерфейса IDisposable.

        public virtual void Dispose()
        {
            if (Disposed != null)
                Disposed.Invoke(this, EventArgs.Empty);
        }
    }
}

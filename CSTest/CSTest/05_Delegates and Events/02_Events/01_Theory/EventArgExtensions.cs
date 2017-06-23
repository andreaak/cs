using System;
using System.Threading;

namespace CSTest._05_Delegates_and_Events._02_Events._01_Theory
{
    public static class EventArgExtensions
    {
        public static void Raise<TEventArgs>(this TEventArgs e, object sender
            , ref EventHandler<TEventArgs> eventDelegate) where TEventArgs : EventArgs
        {
            // Копирование ссылки на поле делегата во временное поле
            // для безопасности в отношении потоков
#if CS5
            EventHandler<TEventArgs> temp = Volatile.Read(ref eventDelegate);
#else
            EventHandler<TEventArgs> temp = Interlocked.CompareExchange(ref eventDelegate, null, null);
#endif
            // Если зарегистрированный метод заинтересован в событии, уведомите его
            if (temp != null)
            {
                temp(sender, e);
            }
        }
    }
}
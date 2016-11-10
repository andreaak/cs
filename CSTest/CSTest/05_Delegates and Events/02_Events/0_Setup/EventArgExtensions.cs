using System;
using System.Threading;

namespace CSTest._05_Delegates_and_Events._02_Events._0_Setup
{
    public static class EventArgExtensions
    {
        public static void Raise<TEventArgs>(this TEventArgs e, object sender
            , ref EventHandler<TEventArgs> eventDelegate)
        {
            // Копирование ссылки на поле делегата во временное поле
            // для безопасности в отношении потоков
            EventHandler<TEventArgs> temp = Volatile.Read(ref eventDelegate);

            // Если зарегистрированный метод заинтересован в событии, уведомите его
            if (temp != null)
            {
                temp(sender, e);
            }
        }
    }
}
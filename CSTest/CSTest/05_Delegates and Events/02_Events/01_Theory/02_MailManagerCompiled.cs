using System;
using System.Threading;

namespace CSTest._05_Delegates_and_Events._02_Events._01_Theory
{
    class MailManagerCompiled
    {
        /// 1. «ј –џ“ќ≈ поле делегата, инициализированное значением null
        private EventHandler<NewMailEventArgs> NewMail = null;

        // 2. ќ“ –џ“џ… метод add_Xxx (где Xxx Ц это им€ событи€)
        // ѕозвол€ет объектам регистрироватьс€ дл€ получени€ уведомлений о событии
        public void add_NewMail(EventHandler<NewMailEventArgs> value)
        {
            // ÷икл и вызов CompareExchange Ц хитроумный способ добавлени€
            // делегата способом, безопасным в отношении потоков
            EventHandler<NewMailEventArgs> prevHandler;
            EventHandler<NewMailEventArgs> newMail = this.NewMail;
            do
            {
                prevHandler = newMail;
                EventHandler<NewMailEventArgs> newHandler =
                    (EventHandler<NewMailEventArgs>)Delegate.Combine(prevHandler, value);
                newMail = Interlocked.CompareExchange<EventHandler<NewMailEventArgs>>(
                    ref this.NewMail, newHandler, prevHandler);
            } while (newMail != prevHandler);
        }

        // 3. ќ“ –џ“џ… метод remove_Xxx (где Xxx Ц это им€ событи€)
        // ѕозвол€ет объектам отмен€ть регистрацию в качестве
        // получателей уведомлений о cобытии
        public void remove_NewMail(EventHandler<NewMailEventArgs> value)
        {
            // ÷икл и вызов CompareExchange Ц хитроумный способ
            // удалени€ делегата способом, безопасным в отношении потоков
            EventHandler<NewMailEventArgs> prevHandler;
            EventHandler<NewMailEventArgs> newMail = this.NewMail;
            do
            {
                prevHandler = newMail;
                EventHandler<NewMailEventArgs> newHandler =
                    (EventHandler<NewMailEventArgs>)Delegate.Remove(prevHandler, value);
                newMail = Interlocked.CompareExchange<EventHandler<NewMailEventArgs>>(
                    ref this.NewMail, newHandler, prevHandler);
            } while (newMail != prevHandler);
        }
    }
}
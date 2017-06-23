using System;
using System.Threading;

namespace CSTest._05_Delegates_and_Events._02_Events._01_Theory
{
    class MailManager
    {
        // Этап 2. Определение члена-события
        public event EventHandler<NewMailEventArgs> NewMail;

        //...

        // Этап 3. Определение метода, ответственного за уведомление
        // зарегистрированных объектов о событии
        // Если этот класс изолированный, нужно сделать метод закрытым
        // или невиртуальным
        protected virtual void OnNewMail(NewMailEventArgs e)
        {
            // Сохранить ссылку на делегата во временной переменной
            // для обеспечения безопасности потоков
#if CS5

            EventHandler<NewMailEventArgs> temp = Volatile.Read(ref NewMail);
#else
            EventHandler<NewMailEventArgs> temp = Interlocked.CompareExchange(ref NewMail, null, null);
#endif
            // Если есть объекты, зарегистрированные для получения
            // уведомления о событии, уведомляем их
            if (temp != null)
            {
                temp(this, e);
            }
        }

        // Версия 2
        protected void OnNewMail2(NewMailEventArgs e)
        {
            EventHandler<NewMailEventArgs> temp = NewMail;
            if (temp != null)
            {
                temp(this, e);
            }
        }

        // Версия 3
        protected void OnNewMail3(NewMailEventArgs e)
        {
#if CS5

            EventHandler<NewMailEventArgs> temp = Volatile.Read(ref NewMail);
#else
            EventHandler<NewMailEventArgs> temp = Interlocked.CompareExchange(ref NewMail, null, null);
#endif

            if (temp != null)
            {
                temp(this, e);
            }
        }

        //EventArgExtensions
        protected virtual void OnNewMail4(NewMailEventArgs e)
        {
            e.Raise(this, ref NewMail);
        }

        //...

        //Этап 4. Определение метода, преобразующего входную
        // информацию в желаемое событие
        public void SimulateNewMail(string from, string to, string subject)
        {
            // Создать объект для хранения информации, которую
            // нужно передать получателям уведомления
            NewMailEventArgs e = new NewMailEventArgs(from, to, subject);

            // Вызвать виртуальный метод, уведомляющий объект о событии
            // Если ни один из производных типов не переопределяет этот метод,
            // объект уведомит всех зарегистрированных получателей уведомления
            OnNewMail(e);
        }
    }
}
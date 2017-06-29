using System;
using System.Threading;

namespace CSTest._05_Delegates_and_Events._02_Events._01_Theory
{
    class MailManagerCompiled
    {
        /// 1. �������� ���� ��������, ������������������ ��������� null
        private EventHandler<NewMailEventArgs> NewMail = null;

        // 2. �������� ����� add_Xxx (��� Xxx � ��� ��� �������)
        // ��������� �������� ���������������� ��� ��������� ����������� � �������
        public void add_NewMail(EventHandler<NewMailEventArgs> value)
        {
            // ���� � ����� CompareExchange � ���������� ������ ����������
            // �������� ��������, ���������� � ��������� �������
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

        // 3. �������� ����� remove_Xxx (��� Xxx � ��� ��� �������)
        // ��������� �������� �������� ����������� � ��������
        // ����������� ����������� � c������
        public void remove_NewMail(EventHandler<NewMailEventArgs> value)
        {
            // ���� � ����� CompareExchange � ���������� ������
            // �������� �������� ��������, ���������� � ��������� �������
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
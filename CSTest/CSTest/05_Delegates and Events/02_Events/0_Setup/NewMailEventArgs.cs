using System;

namespace CSTest._05_Delegates_and_Events._02_Events._0_Setup
{

    // Ётап 1. ќпределение типа дл€ хранени€ информации,
    // котора€ передаетс€ получател€м уведомлени€ о событии
    internal class NewMailEventArgs : EventArgs
    {
        private readonly string m_from, m_to, m_subject;

        public NewMailEventArgs(string from, string to, string subject)
        {
            m_from = from; m_to = to; m_subject = subject;
        }

        public string From { get { return m_from; } }

        public string To { get { return m_to; } }

        public string Subject { get { return m_subject; } }
    }
}
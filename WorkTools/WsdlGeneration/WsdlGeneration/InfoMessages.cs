using System;
using System.Collections.Generic;
using System.Text;

namespace WsdlGeneration
{
    internal class InfoMessages
    {
        // Fields
        private List<Message> messages = new List<Message>();

        // Methods
        public void AddInfoMessage(InfoLevel level, string title, string message)
        {
            if ((title == null) || (title.Length == 0))
            {
                if (level == InfoLevel.Error)
                {
                    title = "Error";
                }
                else if (level == InfoLevel.Info)
                {
                    title = "Info";
                }
                else if (level == InfoLevel.Warning)
                {
                    title = "Warning";
                }
                else if (level == InfoLevel.Canceled)
                {
                    title = "Canceled";
                }
            }
            this.messages.Add(new Message(level, title, message, DateTime.Now));
        }

        internal void AddInfoMessages(InfoMessages infoMessages)
        {
            foreach (Message message in infoMessages.Messages)
            {
                this.AddInfoMessage(message.Level, message.Title, message.MessageText);
            }
        }

        // Properties
        public List<Message> Messages
        {
            get
            {
                return this.messages;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace WsdlGeneration
{
    internal class Message
    {
        // Fields
        private InfoLevel level;
        private DateTime messageDT;
        private string messageText;
        private string title = string.Empty;

        // Methods
        public Message(InfoLevel level, string title, string messageText, DateTime messageDateTime)
        {
            this.Level = level;
            this.Title = title;
            this.MessageText = messageText;
            this.MessageDT = messageDateTime;
        }

        // Properties
        public string DisplayMessageText
        {
            get
            {
                string str = null;
                DateTime messageDT = this.MessageDT;
                str = this.MessageDT.ToShortTimeString() + "\n" + this.messageText;
                return ((str == null) ? this.messageText : str);
            }
        }

        internal InfoLevel Level
        {
            get
            {
                return this.level;
            }
            set
            {
                this.level = value;
            }
        }

        private DateTime MessageDT
        {
            get
            {
                return this.messageDT;
            }
            set
            {
                this.messageDT = value;
            }
        }

        public string MessageText
        {
            get
            {
                return this.messageText;
            }
            set
            {
                if (value != null)
                {
                    this.messageText = value;
                }
            }
        }

        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                if (value != null)
                {
                    this.title = value;
                }
            }
        }
    }
}

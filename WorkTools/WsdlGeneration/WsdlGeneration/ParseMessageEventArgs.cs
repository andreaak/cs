using System;
using System.Collections.Generic;
using System.Text;

namespace WsdlGeneration
{
    public class ParseMessageEventArgs : EventArgs
    {
        // Fields
        private string m_MessageHeader;
        private string m_MessageText;
        private string m_ParseSource;
        private ParseMessageType m_Type;

        // Methods
        public ParseMessageEventArgs()
        {
            this.m_Type = ParseMessageType.None;
        }

        public ParseMessageEventArgs(ParseMessageType type, string MessageHeader, string MessageText)
            : this()
        {
            this.m_MessageHeader = MessageHeader;
            this.m_MessageText = MessageText;
            this.m_Type = type;
        }

        public ParseMessageEventArgs(ParseMessageType type, string LineHeader, string MessageText, string Source)
            : this(type, LineHeader, MessageText)
        {
            this.m_ParseSource = Source;
        }

        // Properties
        public string LineHeader
        {
            get
            {
                return this.m_MessageHeader;
            }
            set
            {
                this.m_MessageHeader = value;
            }
        }

        public string MessageText
        {
            get
            {
                return this.m_MessageText;
            }
            set
            {
                this.m_MessageText = value;
            }
        }

        public ParseMessageType MessageType
        {
            get
            {
                return this.m_Type;
            }
            set
            {
                this.m_Type = value;
            }
        }

        public string Source
        {
            get
            {
                return this.m_ParseSource;
            }
            set
            {
                this.m_ParseSource = value;
            }
        }
    }

}

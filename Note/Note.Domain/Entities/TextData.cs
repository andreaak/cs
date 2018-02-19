using System;

namespace Note.Domain.Entities
{
    public class TextData
    {
        public int ID
        {
            get;
            set;
        }

        public DateTime ModDate
        {
            get;
            set;
        }

        public string RtfText
        {
            get;
            set;
        }

        public string PlainText
        {
            get;
            set;
        }

        public string HtmlText
        {
            get;
            set;
        }
    }
}

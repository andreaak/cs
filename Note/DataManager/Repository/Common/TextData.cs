using System;

namespace DataManager.Repository.Common
{
    public class TextData
    {
        public long ID
        {
            get;
            set;
        }

        public DateTime ModDate
        {
            get;
            set;
        }

        public string EditValue
        {
            get;
            set;
        }

        public string PlainText
        {
            get;
            set;
        }
    }
}

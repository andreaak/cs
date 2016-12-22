using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace WsdlGeneration
{
    public class ListBoxList
    {
        // Fields
        private ArrayList _alMessages = new ArrayList();
        private ArrayList _alMessagesInfo = new ArrayList();

        // Events
        public event AddedEventHandler OnItemAdded;

        public event InsertEventHandler OnItemInserted;

        // Methods
        public int Add(ParseMessageEventArgs pmea)
        {
            int num = this._alMessages.Add(pmea);
            this._alMessagesInfo.Add(new ItemInfo(pmea));
            this.OnAdd();
            return num;
        }

        public void Clear()
        {
            this._alMessages.Clear();
            this._alMessagesInfo.Clear();
        }

        public int IndexOf(object obj)
        {
            return this._alMessages.IndexOf(obj);
        }

        public int IndexOf(ParseMessageEventArgs pmea)
        {
            return this._alMessages.IndexOf(pmea);
        }

        public ItemInfo Info(int index)
        {
            return (ItemInfo)this._alMessagesInfo[index];
        }

        public void Insert(int index, ParseMessageEventArgs pmea)
        {
            this._alMessages.Insert(index, pmea);
            this._alMessagesInfo.Insert(index, new ItemInfo(pmea));
            this.OnInsert(index);
        }

        private void OnAdd()
        {
            if (this.OnItemAdded != null)
            {
                this.OnItemAdded();
            }
        }

        private void OnInsert(int index)
        {
            if (this.OnItemInserted != null)
            {
                this.OnItemInserted(index);
            }
        }

        // Properties
        public int Count
        {
            get
            {
                return this._alMessages.Count;
            }
        }

        public ParseMessageEventArgs this[int index]
        {
            get
            {
                return (ParseMessageEventArgs)this._alMessages[index];
            }
        }
    }
}

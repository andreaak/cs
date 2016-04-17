using System.Collections;

namespace Patterns._03_Behavioral.Iterator._001_Base
{
    class ConcreteAggregate : Aggregate
    {
        private ArrayList items = new ArrayList();

        public override Iterator CreateIterator()
        {
            return new ConcreteIterator(this);
        }

        public override int Count
        {
            get { return items.Count; }
        }

        public override object this[int index]
        {
            get { return items[index]; }
            set { items.Insert(index, value); }
        }
    }
}
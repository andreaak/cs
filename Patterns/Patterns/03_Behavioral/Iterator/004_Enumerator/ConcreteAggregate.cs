﻿using System.Collections;

namespace Patterns._03_Behavioral.Iterator._004_Enumerator
{
    class Enumerable : IEnumerable
    {
        private ArrayList items = new ArrayList();
        public object this[int index]
        {
            get { return items[index]; }
            set { items.Insert(index, value); }
        }
        public int Count
        {
            get { return items.Count; }
        }

        // Реализация интерфейса IEnumerable.
        public IEnumerator GetEnumerator()
        {
            return new Enumerator(this);
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace CSTest._10_Collections._03_Iterators._02_Yield
{
    class _04_YieldGeneric<T>
    {
        T[] array;
        public _04_YieldGeneric(T[] a)
        {
            array = a;
        }
        // Этот итератор возвращает символы из массива array. 
        public IEnumerator<T> GetEnumerator()
        {
            foreach (T obj in array)
            {
                yield return obj;
            }
        }

        //Разлагается в:
        public IEnumerator<T> GetEnumerator2()
        {
            _04_YieldGeneric<T>.GetEnumeratorClass getEnumerator = new _04_YieldGeneric<T>.GetEnumeratorClass(0);
            getEnumerator.enumerableClass = this;
            return (IEnumerator<T>) getEnumerator;
        }

        private sealed class GetEnumeratorClass : IEnumerator<T>, IEnumerator, IDisposable
        {
            private T current;
            private int state;
            public _04_YieldGeneric<T> enumerableClass;
            public T item;
            public T[] array;
            public int index;

            T IEnumerator<T>.Current
            {
                get
                {
                    return this.current;
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return (object) this.current;
                }
            }

            public GetEnumeratorClass(int state)
            {
                this.state = state;
            }

            bool IEnumerator.MoveNext()
            {
                try
                {
                    switch (this.state)
                    {
                        case 0:
                            this.state = -1;
                            this.state = 1;
                            this.array = this.enumerableClass.array;
                            this.index = 0;
                            break;
                        case 2:
                            this.state = 1;
                            this.index = this.index + 1;
                            break;
                        default:
                            return false;
                    }
                    if (this.index < this.array.Length)
                    {
                        this.item = this.array[this.index];
                        this.current = this.item;
                        this.state = 2;
                        return true;
                    }
                    this.Finally2();
                    return false;
                }
                catch(Exception ex)
                {
                    ((IDisposable)this).Dispose();
                }
                return false;
            }

            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            void IDisposable.Dispose()
            {
                switch (this.state)
                {
                    case 1:
                    case 2:
                        this.Finally2();
                        break;
                }
            }

            private void Finally2()
            {
                this.state = -1;
            }
        }


        public IEnumerable<T> IterateEnumerable()
        {
            foreach (T obj in array)
            {
                Debug.WriteLine("yield return" + obj);
                yield return obj;
            }
        }

        public IEnumerator<T> IterateEnumerator()
        {
            foreach (T obj in array)
            {
                Debug.WriteLine("yield return" + obj);
                yield return obj;
            }
        }

        public IEnumerable<T> IterateEnumerable(int end)
        {
            for (int i = 0; i < end; i++)
            {
                Debug.WriteLine("yield return " + array[i]);
                yield return array[i];
            }
        }

        public IEnumerable<T> IterateEnumerableAll(int end)
        {
            List<T> list = new List<T>();

            for (int i = 0; i < end; i++)
            {
                Debug.WriteLine("Value " + array[i]);
                list.Add(array[i]);
            }

            return list;
        }

        // Этот итератор возвращает буквы в заданных пределах, 
        public IEnumerable<T> IterateEnumerable(int begin, int end)
        {
            for (int i = begin; i < end; i++)
            {
                yield return array[i];
            }
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace CSTest._10_Collections._03_Iterators._02_Yield
{
    class _05_YieldDecompiled<T>
    {
        T[] array;

        public _05_YieldDecompiled(T[] a)
        {
            array = a;
        }

        //GetEnumerator компилится в:
        public IEnumerator<T> GetEnumerator()
        {
            GetEnumeratorClass generated = new GetEnumeratorClass(0);
            generated.enumerableClass = this;
            return (IEnumerator<T>)generated;
        }

        private sealed class GetEnumeratorClass : IEnumerator<T>, IEnumerator, IDisposable
        {
            private T current;
            private int state;
            public _05_YieldDecompiled<T> enumerableClass;
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
                    return (object)this.current;
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
                catch (Exception ex)
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
            IterateEnumerableClass generated = new IterateEnumerableClass(-2);
            generated.enumerableClass = this;
            return (IEnumerable<T>)generated;
        }

        private sealed class IterateEnumerableClass : IEnumerable<T>, IEnumerable, IEnumerator<T>, IDisposable, IEnumerator
        {
            public _05_YieldDecompiled<T> enumerableClass;
            private int state;
            private T current;
            private int initialThreadId;
            private T[] array;
            private int index;
            private T item;

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
                    return (object)this.current;
                }
            }

            public IterateEnumerableClass(int state)
            {
                this.state = state;
                this.initialThreadId = Environment.CurrentManagedThreadId;
            }

            void IDisposable.Dispose()
            {
            }

            bool IEnumerator.MoveNext()
            {
                switch (this.state)
                {
                    case 0:
                        this.state = -1;
                        this.array = this.enumerableClass.array;
                        this.index = 0;
                        break;
                    case 1:
                        this.state = -1;
                        this.item = default(T);
                        this.index = this.index + 1;
                        break;
                    default:
                        return false;
                }
                if (this.index < this.array.Length)
                {
                    this.item = this.array[this.index];
                    Debug.WriteLine("yield return" + (object)this.item);
                    this.current = this.item;
                    this.state = 1;
                    return true;
                }
                this.array = (T[])null;
                return false;
            }

            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            IEnumerator<T> IEnumerable<T>.GetEnumerator()
            {
                IterateEnumerableClass iterateEnumerableD3;
                if (this.state == -2 && this.initialThreadId == Environment.CurrentManagedThreadId)
                {
                    this.state = 0;
                    iterateEnumerableD3 = this;
                }
                else
                {
                    iterateEnumerableD3 = new IterateEnumerableClass(0);
                    iterateEnumerableD3.enumerableClass = this.enumerableClass;
                }
                return (IEnumerator<T>)iterateEnumerableD3;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return (IEnumerator)this;
            }
        }

        public IEnumerable<T> IterateEnumerable(int end)
        {
            IterateEnumerable2Class iterateEnumerableD4 = new IterateEnumerable2Class(-2);
            iterateEnumerableD4.enumerableClass = this;
            iterateEnumerableD4.end = end;
            return (IEnumerable<T>)iterateEnumerableD4;
        }

        private sealed class IterateEnumerable2Class : IEnumerable<T>, IEnumerable, IEnumerator<T>, IDisposable, IEnumerator
        {
            public int end;
            public _05_YieldDecompiled<T> enumerableClass;
            private int state;
            private T current;
            private int initialThreadId;
            private int endIndex;
            private int index;

            T IEnumerator<T>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return (object)this.current;
                }
            }

            public IterateEnumerable2Class(int _state)
            {
                this.state = _state;
                this.initialThreadId = Environment.CurrentManagedThreadId;
            }

            void IDisposable.Dispose()
            {
            }

            bool IEnumerator.MoveNext()
            {
                switch (this.state)
                {
                    case 0:
                        this.state = -1;
                        this.index = 0;
                        break;
                    case 1:
                        this.state = -1;
                        this.index = this.index + 1;
                        break;
                    default:
                        return false;
                }
                if (this.index >= this.endIndex)
                    return false;
                Debug.WriteLine("yield return " + (object)this.enumerableClass.array[this.index]);
                this.current = this.enumerableClass.array[this.index];
                this.state = 1;
                return true;
            }

            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            IEnumerator<T> IEnumerable<T>.GetEnumerator()
            {
                IterateEnumerable2Class iterateEnumerableD4;
                if (this.state == -2 && this.initialThreadId == Environment.CurrentManagedThreadId)
                {
                    this.state = 0;
                    iterateEnumerableD4 = this;
                }
                else
                {
                    iterateEnumerableD4 = new IterateEnumerable2Class(0);
                    iterateEnumerableD4.enumerableClass = this.enumerableClass;
                }
                iterateEnumerableD4.endIndex = this.end;
                return (IEnumerator<T>)iterateEnumerableD4;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return (IEnumerator)this;
            }
        }

        public IEnumerable<T> IterateEnumerable(int begin, int end)
        {
            IterateEnumerable3Class iterateEnumerableD6 = new IterateEnumerable3Class(-2);
            iterateEnumerableD6.enumerableClass = this;
            iterateEnumerableD6.begin = begin;
            iterateEnumerableD6.end = end;
            return (IEnumerable<T>)iterateEnumerableD6;
        }

        private sealed class IterateEnumerable3Class : IEnumerable<T>, IEnumerable, IEnumerator<T>, IDisposable, IEnumerator
        {
            public int begin;
            public int end;
            public _05_YieldDecompiled<T> enumerableClass;
            private int state;
            private T current;
            private int initialThreadId;
            private int beginIndex;
            private int endIndex;
            private int index;

            T IEnumerator<T>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return (object)this.current;
                }
            }

            [DebuggerHidden]
            public IterateEnumerable3Class(int state)
            {
                this.state = state;
                this.initialThreadId = Environment.CurrentManagedThreadId;
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            bool IEnumerator.MoveNext()
            {
                switch (this.state)
                {
                    case 0:
                        this.state = -1;
                        this.index = this.beginIndex;
                        break;
                    case 1:
                        this.state = -1;
                        this.index = this.index + 1;
                        break;
                    default:
                        return false;
                }
                if (this.index >= this.endIndex)
                    return false;
                this.current = this.enumerableClass.array[this.index];
                this.state = 1;
                return true;
            }

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            IEnumerator<T> IEnumerable<T>.GetEnumerator()
            {
                IterateEnumerable3Class iterateEnumerableD6;
                if (this.state == -2 && this.initialThreadId == Environment.CurrentManagedThreadId)
                {
                    this.state = 0;
                    iterateEnumerableD6 = this;
                }
                else
                {
                    iterateEnumerableD6 = new IterateEnumerable3Class(0);
                    iterateEnumerableD6.enumerableClass = this.enumerableClass;
                }
                iterateEnumerableD6.beginIndex = this.begin;
                iterateEnumerableD6.endIndex = this.end;
                return (IEnumerator<T>)iterateEnumerableD6;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return (IEnumerator)this;
            }
        }

        public IEnumerable StaticText()
        {
            return new StaticTextClass(-2);
        }

        private sealed class StaticTextClass : IEnumerable<object>, IEnumerator<object>, IEnumerator, IDisposable
        {
            // Поля.
            private int state;
            private object current;
            private int initialThreadId;

            // Конструктор.
            public StaticTextClass(int state)
            {
                this.state = state;
                this.initialThreadId = Thread.CurrentThread.ManagedThreadId;
            }

            //private bool IEnumerator.MoveNext() // Так в Рефлекторе
            bool IEnumerator.MoveNext()
            {
                switch (this.state)
                {
                    case 0:
                        this.state = -1;
                        this.current = "Hello world!";
                        this.state = 1;
                        return true;

                    case 1:
                        this.state = -1;
                        break;
                }
                return false;
            }

            IEnumerator<object> IEnumerable<object>.GetEnumerator()
            {
                if ((Thread.CurrentThread.ManagedThreadId == this.initialThreadId) && (this.state == -2))
                {
                    this.state = 0;
                    return this;
                }
                return new StaticTextClass(0);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                // Так в Рефлекторе 
                //return this.System.Collections.Generic.IEnumerable<System.Object>.GetEnumerator(); 

                return (this as IEnumerable<object>).GetEnumerator();
            }

            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            void IDisposable.Dispose()
            {
            }

            // Свойства.
            object IEnumerator<object>.Current
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
                    return this.current;
                }
            }
        }

    }
}

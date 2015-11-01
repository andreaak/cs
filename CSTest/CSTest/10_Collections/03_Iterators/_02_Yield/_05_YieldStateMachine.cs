using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace CSTest._10_Collections._03_Iterators._02_Yield
{
    //class _05_YieldStateMachine
    //{
    //    public static IEnumerable Power()
    //    {
    //        yield return "Hello world!";
    //    }
    //}
    // translate to
    class _05_YieldStateMachine
    {
        public static IEnumerable Power()
        {
            return new ClassPower(-2);
        }

        private sealed class ClassPower : IEnumerable<object>, IEnumerator<object>, IEnumerator, IDisposable
        {
            // Поля.
            private int state;
            private object current;
            private int initialThreadId;

            // Конструктор.
            public ClassPower(int state)
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
                return new _05_YieldStateMachine.ClassPower(0);
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

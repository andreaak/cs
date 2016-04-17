using System.Collections;

namespace Patterns._03_Behavioral.Iterator._008_Yield
{
    class Enumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return "Element";
        }
    }

    class Enumerable2
    {
        public IEnumerator GetEnumerator()
        {
            return new Enumerator();
        }

        class Enumerator : IEnumerator
        {
            object current;
            int state = 0;

            bool IEnumerator.MoveNext()
            {
                switch (state)
                {
                    case 0:
                        current = "Element";
                        state = 1;
                        return true;
                    case 1:
                        state = 0;
                        break;
                }
                return false;
            }
            void IEnumerator.Reset()
            {
                state = 0;
            }
            object IEnumerator.Current
            {
                get { return current; }
            }
        }
    }
}

using System.Collections;
using System.Diagnostics;

namespace CSTest._10_Collections.Iterators
{
    /*
    Перечислитель — это объект, который выполняет итерацию в связанной с ним коллекции. 
    Можно считать, что он является перемещаемым указателем на любой элемент коллекции. 
    Перечислитель может быть связан только с одной коллекцией, но коллекция может иметь несколько перечислителей. 
    
    Циклическая конструкция foreach позволяет выполнять навигацию по коллекции,
    используя реализации интерфейсов IEnumerable и IEnumerator.
    
    Оператор foreach предполагает, что все элементы коллекции имеют один и тот же тип. 
    Все перечислители основаны на интерфейсе IEnumerator или на универсальном интерфейсе 
    IEnumerator<T>, для чего им необходимо иметь перечисленные ниже члены: 
	    - Свойство object Current {get;} указывает на текущий элемент коллекции. 
	    - Метод bool MoveNext() перемещает перечислитель к следующему элементу коллекции. 
	    - Метод void Reset() перемещает перечислитель в начало коллекции. 
                Свойство Current при этом указывает на положение перед первым элементом.
    */

    // Реализовать интерфейсы IEnumerable и IEnumerator. 
    class _01_IEnumerable : IEnumerator, IEnumerable
    {
        char[] chrs = { 'А', 'В', 'C', 'D' };
        int position = -1;
        // Реализовать интерфейс IEnumerable. 
        public IEnumerator GetEnumerator()
        {
            Debug.WriteLine("GetEnumerator");
            return this;
        }
        // В следующих методах реализуется интерфейс IEnumerator 
        // Возвратить текущий объект, 
        public object Current
        {
            get
            {
                return chrs[position];
            }
        }
        // Перейти к следующему объекту, 
        public bool MoveNext()
        {
            Debug.WriteLine("MoveNext");
            if (position == chrs.Length - 1)
            {
                Reset(); //установить перечислитель в конец 
                return false;
            }
            position++;
            return true;
        }
        // Установить перечислитель в начало, 
        public void Reset()
        {
            Debug.WriteLine("Reset");
            position = -1;
        }
    }
}

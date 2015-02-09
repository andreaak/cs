
namespace DataStructuresAndAlgorithms.DoublyLinkedList
{
    public class DoublyLinkedListNode<T>
    {
        // Создание нового узла со специальным значением.     

        public DoublyLinkedListNode(T value) // конструктор
        {

            Value = value;

        }

        // Значение узла.     

        public T Value // свойство  
        {
            get;
            internal set;
        }

        // Установка следующего значения для узла (null если последний).     

        public DoublyLinkedListNode<T> Next
        {
            get;
            internal set;
        }


        public DoublyLinkedListNode<T> Previous
        {
            get;
            internal set;
        }
    }
}

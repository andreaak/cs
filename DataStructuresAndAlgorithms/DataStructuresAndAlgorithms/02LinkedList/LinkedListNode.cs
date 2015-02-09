using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructuresAndAlgorithms.LinkedList
{
    public class LinkedListNode<T>
    {

        // Создание нового узла со специальным значением.     

        public LinkedListNode(T value) // конструктор
        {

            Value = value;

        }

        // Значение узла.     

        public T Value // свойство  
        {
            get;
            set;
        }

        // Установка следующего значения для узла (null если последний).     

        public LinkedListNode<T> Next
        {
            get;
            set;
        }

    }
}

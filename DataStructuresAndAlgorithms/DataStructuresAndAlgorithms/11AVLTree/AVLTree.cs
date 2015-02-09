using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructuresAndAlgorithms.AVLTree
{
    public class AVLTree<T> : IEnumerable<T> where T : IComparable
    {
        // Свойство для корня дерева

        public AVLTreeNode<T> Head
        {
            get;
            internal set;
        }

        #region Количество узлов дерева
        
        public int Count
        {
            get;
            private set;
        }
        
        #endregion

        #region Метод Add

        // Метод добавлет новый узел

        public void Add(T value)
        {
            // Вариант 1:  Дерево пустое - создание корня дерева      
            if (Head == null)
            {
                Head = new AVLTreeNode<T>(value, null, this);
            }

            // Вариант 2: Дерево не пустое - найти место для добавление нового узла.

            else
            {
                AddTo(Head, value);
            }

            Count++;
        }

        // Алгоритм рекурсивного добавления нового узла в дерево.

        private void AddTo(AVLTreeNode<T> node, T value)
        {
            // Вариант 1: Добавление нового значения в дерево. Значение добавлемого узла меньше чем значение текущего узла.      

            if (value.CompareTo(node.Value) < 0)
            {
                //Создание левого узла, если его нет.

                if (node.Left == null)
                {
                    node.Left = new AVLTreeNode<T>(value, node, this);
                }

                else
                {
                    // Переходим к следующему левому узлу
                    AddTo(node.Left, value);
                }
            }
            // Вариант 2: Добавлемое значение больше или равно текущему значению.

            else
            {
                //Создание правого узла, если его нет.         
                if (node.Right == null)
                {
                    node.Right = new AVLTreeNode<T>(value, node, this);
                }
                else
                {
                    // Переход к следующему правому узлу.             
                    AddTo(node.Right, value);
                }
            }
            //node.Balance();
        }

        #endregion

        #region Метод Contains

        public bool Contains(T value)
        {
            return Find(value) != null;
        }

        /// <summary> 
        /// Находит и возвращает первый узел который содержит искомое значение.
        /// Если значение не найдено, возвращает null. 
        /// Так же возвращает родительский узел.
        /// </summary> /// 
        /// <param name="value">Значение поиска</param> 
        /// <param name="parent">Родительский элемент для найденного значения/// </param> 
        /// <returns> Найденный узел (или ноль) /// </returns> 

        private AVLTreeNode<T> Find(T value)
        {

            AVLTreeNode<T> current = Head; // помещаем текущий элемент в корень дерева

            // Пока текщий узел на пустой 
            while (current != null)
            {
                int result = current.CompareTo(value); // сравнение значения текущего элемента с искомым значением

                if (result > 0)
                {
                    // Если значение меньшне текущего - переход влево 
                    current = current.Left;
                }
                else if (result < 0)
                {
                    // Если значение больше текщего - переход вправо             
                    current = current.Right;
                }
                else
                {
                    // Элемент найден      
                    break;
                }
            }
            return current;
        }


        #endregion

        #region Итераторы

        public IEnumerator<T> InOrderTraversal()
        {

            // рекурсивное перемищение по дереву

            if (Head != null) // существует ли корень дерева
            {

                Stack<AVLTreeNode<T>> stack = new Stack<AVLTreeNode<T>>();
                AVLTreeNode<T> current = Head;

                // при рекурсивном перемещении по дереву, нужно указывать какой потомок будет слудеющим (правый или левый)

                bool goLeftNext = true;

                // Начинаем с помещения корня в стек
                stack.Push(current);

                while (stack.Count > 0)
                {
                    // Если перемещаемся влево ... 
                    if (goLeftNext)
                    {
                        // Перемещение всех левых потомков в стек.

                        while (current.Left != null)
                        {
                            stack.Push(current);
                            current = current.Left;
                        }
                    }

                    yield return current.Value;

                    // Если перемещаемся вправо 

                    if (current.Right != null)
                    {
                        current = current.Right;

                        // Идинажды перемещаемся вправо, после чего опять идем влево. 

                        goLeftNext = true;
                    }
                    else
                    {
                        // Если перейти вправо нельзя - извлекаем родительский узел. 

                        current = stack.Pop();
                        goLeftNext = false;
                    }
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return InOrderTraversal();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {

            return GetEnumerator();

        }

        #endregion

        #region Метод Remove

        public bool Remove(T value)
        {
            AVLTreeNode<T> current;
            current = Find(value); // поиск удаляемого узла по значению

            if (current == null) // узел не найден
            {
                return false;
            }

            AVLTreeNode<T> treeToBalance = current.Parent; // проверка баланса дарева
            Count--;                                       // уменьшение колиества узлов

            // Вариант 1: Если удаляемый узел не имеет правого потомка

            if (current.Right == null) // если нет правого потомка
            {
                if (current.Parent == null) // удалямый узел является корнем
                {
                    Head = current.Left;    // на место корня перемещаем левого потомка

                    if (Head != null)
                    {
                        Head.Parent = null; // для нового корня удаляем ссылку на родителя  
                    }
                }
                else // удаляемый узел не является корнем
                {
                    int result = current.Parent.CompareTo(current.Value);

                    if (result > 0)
                    {
                        // Если значение родительского узла больше удаляемого, 
                        // сделать левого потомка текущего элемента, левым потомком родителя.  

                        current.Parent.Left = current.Left;
                    }
                    else if (result < 0)
                    {

                        // Если родительский узел меньше чем удаляемый,                 
                        // сделать левого потомка удаляемого узла - правым потомком родителя.                 

                        current.Parent.Right = current.Left;
                    }
                }
            }

            // Вариант 2: Если правый потомок удаляемого узла, тоже в свою очередь имеет правого потомка, 
            // но не имеет левого потомка. 

            else if (current.Right.Left == null) // если у правого потомка нет левого потомка
            {
                current.Right.Left = current.Left;

                if (current.Parent == null) // текущий элемент является корнет
                {
                    Head = current.Right;

                    if (Head != null)
                    {
                        Head.Parent = null;
                    }
                }
                else
                {
                    int result = current.Parent.CompareTo(current.Value);
                    if (result > 0)
                    {

                        // Если значение родительского узла больше удаляемого, 
                        // сделать левого потомка текущего элемента, левым потомком родителя.  

                        current.Parent.Left = current.Right;
                    }

                    else if (result < 0)
                    {

                        // Если родительский узел меньше чем удаляемый,                 
                        // сделать левого потомка удаляемого узла - правым потомком родителя.                 

                        current.Parent.Right = current.Right;
                    }
                }
            }

            // Вариант 3: Если правый потомок удаляемого узла имеет левого потомка, 
            // то требуется поместить на место удаляемого узла, крайним левый потомок его правого потомка.     

            else
            {
                // Нахождение крайнего левого узла для правого потомка текущего элемента.       

                AVLTreeNode<T> leftmost = current.Right.Left;

                while (leftmost.Left != null)
                {
                    leftmost = leftmost.Left;
                }

                // Родительское левое поддерево становится родительским крайним правым поддеревом.         
                leftmost.Parent.Left = leftmost.Right;

                // Присвоить крайний левый и крайний правый потомок удаляемого узла, его правому и левому потомку соответственно.         

                leftmost.Left = current.Left;
                leftmost.Right = current.Right;

                if (current.Parent == null)
                {
                    Head = leftmost;

                    if (Head != null)
                    {
                        Head.Parent = null;
                    }
                }
                else
                {
                    int result = current.Parent.CompareTo(current.Value);

                    if (result > 0)
                    {

                        // Если значение родительского узла больше удаляемого, 
                        // сделать левого потомка текущего элемента, левым потомком родителя.  

                        current.Parent.Left = leftmost;
                    }
                    else if (result < 0)
                    {

                        // Если родительский узел меньше чем удаляемый,                 
                        // сделать левого потомка удаляемого узла - правым потомком родителя.                 

                        current.Parent.Right = leftmost;
                    }
                }
            }

            if (treeToBalance != null)
            {
                treeToBalance.Balance();
            }

            else
            {
                if (Head != null)
                {
                    Head.Balance();
                }
            }

            return true;

        }

        #endregion

        #region Метод Clear

        public void Clear()
        {
            Head = null; // удаление дерева
            Count = 0;
        }

        #endregion




    } 
}

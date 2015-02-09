using System;
using System.Collections.Generic;

namespace DataStructuresAndAlgorithms.BinaryTree
{
    public class BinaryTree<T> : IEnumerable<T> where T : IComparable<T>
    {
        private BinaryTreeNode<T> _head;

        private int _count;

        #region Добавление нового узла дерева

        public void Add(T value)
        {
            // Первый случай: дерево пустое     

            if (_head == null)
            {
                _head = new BinaryTreeNode<T>(value);
            }

            // Второй случай: дерево не пустое, поэтому применяем рекурсивный алгорит 
            //                для поиска места добавления узла        

            else
            {
                AddTo(_head, value);
            }
            _count++;
        }

        // Рекурсивный алгоритм 

        private void AddTo(BinaryTreeNode<T> node, T value)
        {
            // Первый случай: значение добавляемого узла меньше чем значение текущего. 

            if (value.CompareTo(node.Value) < 0)
            {
                // если левый потомок отсутствует - добавляем его          

                if (node.Left == null)
                {
                    node.Left = new BinaryTreeNode<T>(value);
                }
                else
                {
                    // повторная итерация               
                    AddTo(node.Left, value);
                }
            }
            // Второй случай: значение добавляемого узла равно или больше текущего значения      
            else
            {
                // Если правый потомок отсутствует - добавлем его.          

                if (node.Right == null)
                {
                    node.Right = new BinaryTreeNode<T>(value);
                }
                else
                {
                    // повторная итерация

                    AddTo(node.Right, value);
                }
            }
        }

        #endregion

        #region Поиск узла в дереве

        // Метод Contains с помощью метода поиска FindWithParent определяется пренадлежит ли указанное значение дереву.

        public bool Contains(T value)
        {

            BinaryTreeNode<T> parent;
            return FindWithParent(value, out parent) != null;
        }

        // Метод FindWithParent возвращает первый найденный узел.
        // Если значение не найдено, метод возвращает null.
        // Так же возвращает родительский узел для найденного значения.

        private BinaryTreeNode<T> FindWithParent(T value, out BinaryTreeNode<T> parent)
        {
            // Поиск значения в дереве.     

            BinaryTreeNode<T> current = _head;
            parent = null;

            while (current != null)
            {
                int result = current.CompareTo(value);
                if (result > 0)
                {
                    // Если искомое значение меньше значение текущего узла - переходим к левому потомку.             

                    parent = current;
                    current = current.Left;
                }
                else if (result < 0)
                {
                    // Если искомое значение больше значение текущего узла - переходим к правому потомку.

                    parent = current;
                    current = current.Right;
                }
                else
                {
                    // Искомый элемент найден             
                    break;
                }
            }
            return current;
        }

        #endregion

        #region Нумератор

        public IEnumerator<T> GetEnumerator()
        {
            return InOrderTraversal();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Количество узлов в дереве

        public int Count
        {
            get
            {
                return _count;
            }
        }

        #endregion

        #region Удаление узла дерева

        //public bool Remove(T value)
        //{
        //    BinaryTreeNode<T> current;
        //    BinaryTreeNode<T> parent;

        //    // Поиск узла для удаления

        //    current = FindWithParent(value, out parent);

        //    if (current == null)
        //    {
        //        return false;
        //    }

        //    _count--;

        //    // Первый вариант: удаляемый узел не имеет правого потомка.     

        //    if (current.Right == null)
        //    {

        //        // Удаляем корень
        //        if (parent == null)
        //        {
        //            _head = current.Left;
        //        }

        //        else
        //        {
        //            int result = parent.CompareTo(current.Value);

        //            if (result > 0)
        //            {
        //                // Если значение узла родителя больше чем значение удаляемого узла -                 
        //                // сделать левого потомка текущего узла - левым потомком родительского узла.                 

        //                parent.Left = current.Left;
        //            }

        //            else if (result < 0)
        //            {

        //                // Если значение родительского узла меньше чем значение удаляемого узла -                  
        //                // сделать левого потомка текущего узла - правым потомком родительского узла.                 

        //                parent.Right = current.Left;

        //            }
        //        }
        //    }

        //    // Второй вариант: удаляемый узел имеет правого потомка, у которого нет левого потомка.

        //    else if (current.Right.Left == null)
        //    {
        //        current.Right.Left = current.Left;

        //        // Удаляем корень 
        //        if (parent == null)
        //        {
        //            _head = current.Right;
        //        }

        //        else
        //        {
        //            int result = parent.CompareTo(current.Value);
        //            if (result > 0)
        //            {
        //                // Если значение родительского узла больше чем значение удаляемого узла -                  
        //                // сделать правого потомка текущего узла - левым потомком родительского узла. 

        //                parent.Left = current.Right;
        //            }
        //            else if (result < 0)
        //            {
        //                // Если значение родительского узла меньше чем значение удаляемого узла -                  
        //                // сделать правого потомка текущего узла - правым потомком родительского узла. 

        //                parent.Right = current.Right;

        //            }
        //        }
        //    }
        //    // Третий вариант: удаляемый узел имеет правого потомка, у которого есть левый потомок.

        //    else
        //    {

        //        BinaryTreeNode<T> leftmost = current.Right.Left;
        //        BinaryTreeNode<T> leftmostParent = current.Right;

        //        // поиск крайнего левого потомка 
        //        while (leftmost.Left != null)
        //        {
        //            leftmostParent = leftmost;
        //            leftmost = leftmost.Left;
        //        }

        //        // Правое поддерево крайнего левого узла, становится левым поддеревом его родительского узла.         
        //        leftmostParent.Left = leftmost.Right;

        //        // Присваиваем крайнему левому узлу в качестве левого потомка - левый потомок удаляемого узла,
        //        // а в качестве правого потомка - правый потомок удаляемого узла. 

        //        leftmost.Left = current.Left;
        //        leftmost.Right = current.Right;

        //        if (parent == null)
        //        {
        //            _head = leftmost;
        //        }

        //        else
        //        {
        //            int result = parent.CompareTo(current.Value);

        //            if (result > 0)
        //            {

        //                // Если значение родительского узла(parent), больше значения удаляемого узла (current) -                  
        //                // сделать левого крайнего потомка удаляемого узла(leftmost)  - левым потомком его родителя(parent). 

        //                parent.Left = leftmost;
        //            }

        //            else if (result < 0)
        //            {

        //                // Если значение родительского узла(parent), меньше значения удаляемого узла (current) -                  
        //                // сделать левого крайнего потомка удаляемого узла(leftmost) - правым потомком его родителя(parent).

        //                parent.Right = leftmost;
        //            }
        //        }
        //    }

        //    return true;
        //}

        public bool Remove(T value)
        {
            BinaryTreeNode<T> current, parent;

            current = FindWithParent(value, out parent);

            if (current == null)
            {
                return false;
            }

            _count--;

            // Case 1: If current has no right child, then current's left replaces current
            if (current.Right == null)
            {
                if (parent == null)
                {
                    _head = current.Left;
                }
                else
                {
                    int result = parent.CompareTo(current.Value);
                    if (result > 0)
                    {
                        // if parent value is greater than current value
                        // make the current left child a left child of parent
                        parent.Left = current.Left;
                    }
                    else if (result < 0)
                    {
                        // if parent value is less than current value
                        // make the current left child a right child of parent
                        parent.Right = current.Left;
                    }
                }
            }
            // Case 2: If current's right child has no left child, then current's right child
            //         replaces current
            else if (current.Right.Left == null)
            {
                current.Right.Left = current.Left;

                if (parent == null)
                {
                    _head = current.Right;
                }
                else
                {
                    int result = parent.CompareTo(current.Value);
                    if (result > 0)
                    {
                        // if parent value is greater than current value
                        // make the current right child a left child of parent
                        parent.Left = current.Right;
                    }
                    else if (result < 0)
                    {
                        // if parent value is less than current value
                        // make the current right child a right child of parent
                        parent.Right = current.Right;
                    }
                }
            }
            // Case 3: If current's right child has a left child, replace current with current's
            //         right child's left-most child
            else
            {
                // find the right's left-most child
                BinaryTreeNode<T> leftmost = current.Right.Left;
                BinaryTreeNode<T> leftmostParent = current.Right;

                while (leftmost.Left != null)
                {
                    leftmostParent = leftmost;
                    leftmost = leftmost.Left;
                }

                // the parent's left subtree becomes the leftmost's right subtree
                leftmostParent.Left = leftmost.Right;

                // assign leftmost's left and right to current's left and right children
                leftmost.Left = current.Left;
                leftmost.Right = current.Right;

                if (parent == null)
                {
                    _head = leftmost;
                }
                else
                {
                    int result = parent.CompareTo(current.Value);
                    if (result > 0)
                    {
                        // if parent value is greater than current value
                        // make leftmost the parent's left child
                        parent.Left = leftmost;
                    }
                    else if (result < 0)
                    {
                        // if parent value is less than current value
                        // make leftmost the parent's right child
                        parent.Right = leftmost;
                    }
                }
            }

            return true;
        }

        #endregion

        #region Удаление дерева

        public void Clear()
        {
            _head = null;
            _count = 0;
        }

        #endregion

        #region Pre-Order Traversal Прямой порядок
        /// <summary>
        /// Performs the provided action on each binary tree value in pre-order traversal order.
        /// </summary>
        /// <param name="action">The action to perform</param>
        public void PreOrderTraversal(Action<T> action)
        {
            PreOrderTraversal(action, _head);
        }

        private void PreOrderTraversal(Action<T> action, BinaryTreeNode<T> node)
        {
            if (node != null)
            {
                action(node.Value);
                PreOrderTraversal(action, node.Left);
                PreOrderTraversal(action, node.Right);
            }
        }
        #endregion

        #region Post-Order Traversal Обратный порядок
        /// <summary>
        /// Performs the provided action on each binary tree value in post-order traversal order.
        /// </summary>
        /// <param name="action">The action to perform</param>
        public void PostOrderTraversal(Action<T> action)
        {
            PostOrderTraversal(action, _head);
        }

        private void PostOrderTraversal(Action<T> action, BinaryTreeNode<T> node)
        {
            if (node != null)
            {
                PostOrderTraversal(action, node.Left);
                PostOrderTraversal(action, node.Right);
                action(node.Value);
            }
        }
        #endregion

        #region In-Order Enumeration
        /// <summary>
        /// Performs the provided action on each binary tree value in in-order traversal order.
        /// </summary>
        /// <param name="action">The action to perform</param>
        public void InOrderTraversal(Action<T> action)
        {
            InOrderTraversal(action, _head);
        }

        private void InOrderTraversal(Action<T> action, BinaryTreeNode<T> node)
        {
            if (node != null)
            {
                InOrderTraversal(action, node.Left);

                action(node.Value);

                InOrderTraversal(action, node.Right);
            }
        }

        /// <summary>
        /// Enumerates the values contains in the binary tree in in-order traversal order.
        /// </summary>
        /// <returns>The enumerator</returns>
        public IEnumerator<T> InOrderTraversal()
        {
            // This is a non-recursive algorithm using a stack to demonstrate removing
            // recursion to make using the yield syntax easier.
            if (_head != null)
            {
                // store the nodes we've skipped in this stack (avoids recursion)
                Stack<BinaryTreeNode<T>> stack = new Stack<BinaryTreeNode<T>>();

                BinaryTreeNode<T> current = _head;

                // when removing recursion we need to keep track of whether or not
                // we should be going to the left node or the right nodes next.
                bool goLeftNext = true;

                // start by pushing Head onto the stack
                stack.Push(current);

                while (stack.Count > 0)
                {
                    // If we're heading left...
                    if (goLeftNext)
                    {
                        // push everything but the left-most node to the stack
                        // we'll yield the left-most after this block
                        while (current.Left != null)
                        {
                            stack.Push(current);
                            current = current.Left;
                        }
                    }

                    // in-order is left->yield->right
                    yield return current.Value;

                    // if we can go right then do so
                    if (current.Right != null)
                    {
                        current = current.Right;

                        // once we've gone right once, we need to start
                        // going left again.
                        goLeftNext = true;
                    }
                    else
                    {
                        // if we can't go right then we need to pop off the parent node
                        // so we can process it and then go to it's right node
                        current = stack.Pop();
                        goLeftNext = false;
                    }
                }
            }
        }
        
        //public IEnumerator<T> InOrderTraversal()
        //{
        //    if (_head != null)
        //    {
        //        // Сохраняем узел в стек 

        //        Stack<BinaryTreeNode<T>> stack = new Stack<BinaryTreeNode<T>>();
        //        BinaryTreeNode<T> current = _head;

        //        // При перемещении по дереву мы должны отслеживать к какому следующему узлу перейти: к левому или правому потомку.   

        //        bool goLeftNext = true;

        //        // Начало. Помещение корня дерева в стек.         

        //        stack.Push(current);

        //        while (stack.Count > 0)
        //        {
        //            // Если мы переходим влево ...        
        //            if (goLeftNext)
        //            {
        //                // Запись всех левых потомков в стек.                 

        //                while (current.Left != null)
        //                {
        //                    stack.Push(current);
        //                    current = current.Left;
        //                }
        //            }

        //            // Возврашение самого левого потомка

        //            yield return current.Value;

        //            // Если у него есть правый потомок 

        //            if (current.Right != null)
        //            {
        //                current = current.Right;

        //                // Если мы один раз переходим в право, то ложны опять осуществить проход по левой стороне                

        //                goLeftNext = true;
        //            }
        //            else
        //            {
        //                // Если правого потомка нет, мы должны извлечь из стека родительский узел

        //                current = stack.Pop();
        //                goLeftNext = false;
        //            }
        //        }
        //    }
        //}
        
        #endregion
    }
}

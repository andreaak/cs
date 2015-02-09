using System;

namespace DataStructuresAndAlgorithms.BinaryTree
{
    public class BinaryTreeNode<TNode> : IComparable<TNode> where TNode : IComparable<TNode>
    {
        public BinaryTreeNode(TNode value)
        {
            Value = value;
        }

        public BinaryTreeNode<TNode> Left
        {
            get;
            set;
        }

        public BinaryTreeNode<TNode> Right
        {
            get;
            set;
        }

        public TNode Value
        {
            get;
            private set;
        }

        // Метод сравнивает значение двух узлов (Value и other), если Value меньше, то возвращается значение меньше нуля.
        //                                                       если они равны, то возвращается ноль,
        //                                                       если Value больше, то возвращается значение больше нуля.

        public int CompareTo(TNode other)
        {
            return Value.CompareTo(other);
        }

        public int CompareNode(BinaryTreeNode<TNode> other)
        {
            return Value.CompareTo(other.Value);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructuresAndAlgorithms.BinaryTree
{
    [TestClass]
    public class BinaryTreeTest
    {
        [TestMethod]
        public void TestNode()
        {
                                                                     //               5            
            BinaryTreeNode<int> Node1 = new BinaryTreeNode<int>(5);  //              / \
            BinaryTreeNode<int> Node2 = new BinaryTreeNode<int>(3);  //             /   \
            BinaryTreeNode<int> Node3 = new BinaryTreeNode<int>(8);  //            3     8


            if (Node1.CompareNode(Node2) >= 0)
            {
                Node1.Left = Node2;
            }

            else
            {
                Node1.Right = Node2;
            }


            if (Node1.CompareNode(Node3) >= 0)
            {
                Node1.Left = Node3;
            }

            else
            {
                Node1.Right = Node3;
            }

            Debug.WriteLine("Левый потомок узла 5: {0}", Node1.Left.Value);
            Debug.WriteLine("Правый потомок узла 5: {0}", Node1.Right.Value);
        }
        
        [TestMethod]
        public void TestAdd()
        {
            BinaryTree<string> instance = new BinaryTree<string>();

            instance.Add("8");    //                        8
            instance.Add("5");    //                      /   \
            instance.Add("12");   //                     5    12 
            instance.Add("3");    //                    / \   / \  
            instance.Add("7");    //                   3   7 10 15
            instance.Add("10");   //
            instance.Add("15");   //

            Debug.WriteLine("Количество узлов в дереве:{0}", instance.Count);
        }

        [TestMethod]
        public void TestSearch()
        {
            BinaryTree<int> instance = new BinaryTree<int>();

            instance.Add(8);    //                        8
            instance.Add(5);    //                      /   \
            instance.Add(12);   //                     5    12 
            instance.Add(3);    //                    / \   / \  
            instance.Add(7);    //                   3   7 10 15
            instance.Add(10);   //
            instance.Add(15);   //

            Debug.WriteLine("Дерево содержит узел со значением 12: {0}", instance.Contains(12));
            Debug.WriteLine("Дерево содержит узел со значением 14: {0}", instance.Contains(14));
        }

        [TestMethod]
        public void TestRemove()
        {
            BinaryTree<int> instance = new BinaryTree<int>();

            instance.Add(8);    //                        8
            instance.Add(5);    //                      /   \
            instance.Add(12);   //                     5    12 
            instance.Add(3);    //                    / \   / \  
            instance.Add(7);    //                   3   7 10 15
            instance.Add(10);   //                         
            instance.Add(15);   //                        

            instance.Remove(8);

            instance.Clear(); // Раскомментировать 

            Debug.WriteLine("Дерево содержит узел со значением 8: {0}", instance.Contains(8));
            Debug.WriteLine("Дерево содержит узел со значением 5: {0}", instance.Contains(5));
            Debug.WriteLine("Дерево содержит узел со значением 3: {0}", instance.Contains(3));
            Debug.WriteLine("Дерево содержит узел со значением 7: {0}", instance.Contains(7));
            Debug.WriteLine("Дерево содержит узел со значением 12: {0}", instance.Contains(12));
            Debug.WriteLine("Дерево содержит узел со значением 10: {0}", instance.Contains(10));
            Debug.WriteLine("Дерево содержит узел со значением 15: {0}", instance.Contains(15));
        }

        [TestMethod]
        public void TestInOrderTraversal1()
        {
            BinaryTree<int> instance = new BinaryTree<int>();

            instance.Add(8);    //                        8
            instance.Add(5);    //                      /   \
            instance.Add(12);   //                     5    12 
            instance.Add(3);    //                    / \   / \  
            instance.Add(7);    //                   3   7 10 15
            instance.Add(10);   //
            instance.Add(15);   //

            foreach (var item in instance)
            {
                Debug.WriteLine(item);
            }
        }

        [TestMethod]
        public void TestInOrderTraversal()
        {
            BinaryTree<int> instance = new BinaryTree<int>();

            instance.Add(4);    //                        4
            instance.Add(2);    //                      /   \
            instance.Add(1);    //                     2     6 
            instance.Add(3);    //                    / \   / \  
            instance.Add(6);    //                   1   3 5   7
            instance.Add(5);
            instance.Add(7);    

            //1 2 3 4 5 6 7
            Action<int> act = (i) => Debug.Write(i + " ");
            instance.InOrderTraversal(act);
            Debug.WriteLine("");
        }

        [TestMethod]
        public void TestPostOrderTraversal()
        {
            BinaryTree<int> instance = new BinaryTree<int>();

            instance.Add(4);    //                        4
            instance.Add(2);    //                      /   \
            instance.Add(1);    //                     2     6 
            instance.Add(3);    //                    / \   / \  
            instance.Add(6);    //                   1   3 5   7
            instance.Add(5);    
            instance.Add(7);    
            
            //1 3 2 5 7 6 4
            Action<int> act = (i) => Debug.Write(i + " ");
            instance.PostOrderTraversal(act);
            Debug.WriteLine("");
        }

        [TestMethod]
        public void TestPreOrderTraversal()
        {
            BinaryTree<int> instance = new BinaryTree<int>();

            instance.Add(4);    //                        4
            instance.Add(2);    //                      /   \
            instance.Add(1);    //                     2     6 
            instance.Add(3);    //                    / \   / \  
            instance.Add(6);    //                   1   3 5   7
            instance.Add(5);
            instance.Add(7);    

            //4 2 1 3 6 5 7
            Action<int> act = (i) => Debug.Write(i + " ");
            instance.PreOrderTraversal(act);
            Debug.WriteLine("");
        }
        
        [TestMethod]
        public void TestHeight()
        {
            BinaryTree<int> instance = new BinaryTree<int>();

            instance.Add(4);    //                        4
            instance.Add(2);    //                      /   \
            instance.Add(1);    //                     2     6 
            instance.Add(3);    //                    / \   /  
            instance.Add(6);    //                   1   3 5   
            instance.Add(5);

            Debug.WriteLine("Height = " + instance.Height);
            /*
            Height = 3
            */
        }
    }
}

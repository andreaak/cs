using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructuresAndAlgorithms.AVLTree
{
    [TestClass]
    public class AVLTree
    {
        [TestMethod]
        public void TestAdd()
        {
            AVLTree<int> Oak = new AVLTree<int>();

                          //                             10                                                             
            Oak.Add(10);  //                            /   \                
            Oak.Add(3);   //                           /     \               
            Oak.Add(2);   //                          3      12     
            Oak.Add(4);   //                         / \     / \         
            Oak.Add(12);  //                        2   4  11   15             
            Oak.Add(15);  //                                                    
            Oak.Add(11);  //                                       

            foreach (var item in Oak)
            {
                Debug.WriteLine(item);
            }
        }

        [TestMethod]
        public void TestContains()
        {
            AVLTree<int> Oak = new AVLTree<int>();

                          //                             10                                                       
            Oak.Add(10);  //                            /   \          
            Oak.Add(3);   //                           /     \        
            Oak.Add(2);   //                          3      12      
            Oak.Add(4);   //                         / \     / \            
            Oak.Add(12);  //                        2   4  11   15              
            Oak.Add(15);  //                                                    
            Oak.Add(11);  //                                       

            foreach (var item in Oak)
            {
                Debug.WriteLine(item);
            }

            Debug.WriteLine(Oak.Contains(12)); 
        }

        [TestMethod]
        public void TestRemove()
        {

            AVLTree<int> Oak = new AVLTree<int>();

                          //                             10                                                      
            Oak.Add(10);  //                            /   \                           
            Oak.Add(3);   //                           /     \                       
            Oak.Add(2);   //                          3      12     
            Oak.Add(4);   //                         / \     / \           
            Oak.Add(12);  //                        2   4  11   15       
            Oak.Add(15);  //                                                    
            Oak.Add(11);  //                                       


            Oak.Remove(12);

            foreach (var item in Oak)
            {
                Debug.WriteLine(item);
            }
        }

        [TestMethod]
        public void TestClear()
        {

            AVLTree<int> Oak = new AVLTree<int>();

                          //                             10                                                    
            Oak.Add(10);  //                            /   \               
            Oak.Add(3);   //                           /     \                 
            Oak.Add(2);   //                          3      12     
            Oak.Add(4);   //                         / \     / \         
            Oak.Add(12);  //                        2   4  11   15               
            Oak.Add(15);  //                                                    
            Oak.Add(11);  //                                       

            Oak.Clear();

            foreach (var item in Oak)
            {
                Debug.WriteLine(item);
            }
        }

        [TestMethod]
        public void TestBalance()
        {
            AVLTree<int> Oak = new AVLTree<int>();
                          //                             10                              10                                             
            Oak.Add(10);  //                            /   \                           /   \
            Oak.Add(3);   //                           /     \                         /     \
            Oak.Add(2);   //                          3      12      ====>            3       15
            Oak.Add(4);   //                         / \     / \                     / \      / \
            Oak.Add(12);  //                        2   4  null 15                  2   4    12  25
            Oak.Add(15);  //                                      \              
            Oak.Add(11);  //                                       25
            Oak.Add(25);  //

            Oak.Remove(11);

            foreach (var item in Oak)
            {
                Debug.WriteLine(item);
            }

            int rex = Math.Max(100, 200) * 2;

            Debug.WriteLine(rex);
        }
    }
}

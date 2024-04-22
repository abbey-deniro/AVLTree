using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class AVLNode<T> where T : IComparable<T>
    {
        public int balanceFactor { get; set; }
        public T Data { get; set; }
        public int height { get; set; }
        public AVLNode<T> Left { get; set; }
        public AVLNode<T> Right { get; set; }
        public int Count { get; set; } = 1;

        public AVLNode(T Data)
        {
            this.Data = Data;
        }

        //Add (T val)
        public void Add(T val)
        {
            //check if value is less than root node
            if (val.CompareTo(Data) < 0)
            {
                //if parent node has no left child
                if (Left == null)
                {
                    //add new node to left of parent
                    Left = new AVLNode<T>(val);
                }
                else
                {
                    //goes down to left child of root
                    //and checks if left is less than
                    Left.Add(val);
                }
            }
            //value is greater than or eqaul to value of root node
            else
            {
                //if parent node has no right child
                if (Right == null)
                {
                    //add new node to right of parent
                    Right = new AVLNode<T>(val);
                }
                else
                {
                    //goes down to right child of root
                    //and checks if right is less than
                    Right.Add(val);
                }

            }
            Count++;
            Balance();
        }

        //Contains (T val)
        public bool Contains(T val)
        {
            //if current node is equal to value
            if (val.CompareTo(Data) == 0)
            {
                return true;
            }

            //if less than root node or current node
            if (val.CompareTo(Data) < 0)
            {
                //if left does not = null
                if (Left != null)
                {
                    //check if tree contains left val 
                    return Left.Contains(val);
                }
            }
            //if greater than root node or current node
            else if (val.CompareTo(Data) > 0)
            {
                //if right does not = null
                if (Right != null)
                {
                    //check if tree contains right val
                    return Right.Contains(val);
                }
            }
            return false;
        }

        //Remove (T val)
        public AVLNode<T> Remove(T val)
        {
            //check if value is less than root node or current
            if (val.CompareTo(Data) < 0)
            {
                //delete node from left subtree
                Left = Left.Remove(val);
            }
            //check if value is greater than root node/current
            else if (val.CompareTo(Data) > 0)
            {
                //delete node from right subtree
                Right = Right.Remove(val);
            }
            else
            {
                //value is found and ready to be deleted
                // node with only one child or no child
                if (Left == null) return Right;
                else if (Right == null) return Left;
                //Case 3: 2 children
                else
                {
                    //create temporary node as largest left
                    //delete and replace with largest node
                    AVLNode<T> tempNode = findLargest(Left);
                    Data = tempNode.Data;
                    Count = tempNode.Count;
                    Left = Left.Remove(Data);
                }
            }
            return this;
        }

        public AVLNode<T> findLargest(AVLNode<T> node)
        {
            //find Largest num in right
            while (node.Right != null) node = node.Right;
            return node;
        }

        //Height
        public int Height()
        {
            //if empty return 0
            if (Left == null && Right == null)
            {
                return 0;
            }

            int left = 0;
            int right = 0;

            if (Left != null)
                left = Left.Height();
            if (Right != null)
                right = Right.Height();

            //return greater height of branch
            if (left > right)
            {
                return (left + 1);
            }
            else
            {
                return (right + 1);
            }
        }
        //ToArray
        public int ToArray(int count, T[] returnArray)
        {

            if (Left != null)
            {
                count = Left.ToArray(count, returnArray);
            }
            returnArray[count] = Data;
            count++;
            if (Right != null)
            {
                count = Right.ToArray(count, returnArray);
            }
            return count;
        }

        //InOrder
        public string InOrder()
        {
            //Left Root Right
            String s = "";
            if (Left != null)
                s += Left.InOrder();
            s += Data + ", ";
            if (Right != null)
                s += Right.InOrder();
            return s;

        }
        //PreOrder
        public string PreOrder()
        {
            //root left right
            String s = "";
            s += Data + ", ";
            //recur left subtree
            if (Left != null)
                s += Left.PreOrder();
            //recur right subtree
            if (Right != null)
                s += Right.PreOrder();
            return s;
        }

        //PostOrder
        public string PostOrder()
        {
            //left right root
            String s = "";
            if (Left != null)
                s += Left.PostOrder();
            if (Right != null)
                s += Right.PostOrder();
            s += Data + ", ";
            return s;
        }

        public int BalanceFactor()
        {
            if (Left != null && Right != null)
                return Left.Height() - Right.Height();
            if (Right == null) return Left.Height() - 0;
            if (Left == null) return 0 - Right.Height();

            else return 0;
        }

        public void Balance()
        {
            int balanceNum = BalanceFactor();
            if (balanceNum == -1 || balanceNum == 0 || balanceNum == 1) return;
            else if (balanceNum == 2) { balanceNum += Left.BalanceFactor(); }
            else if (balanceNum == -2) { balanceNum += Right.BalanceFactor(); }

            //switch
            switch (balanceNum)
            {
                //right left rotation
                case 1:
                    Left.RotateLeft();
                    RotateRight();
                    break;
                //right rotation
                case 3:
                    RotateRight();
                    break;
                //left right rotation
                case -1:
                    Right.RotateRight();
                    RotateLeft();
                    break;
                //left rotation
                case -3:
                    RotateLeft();
                    break;
            }
        }

        public void RotateRight()
        {
            //make copy of root node
            AVLNode<T> pivot = new AVLNode<T>(this.Data);

            //set all data as root node data
            pivot.Data = Data;
            pivot.Count = Count;
            pivot.Right = Right;
            pivot.Left = Left;

            Data = Left.Data;
            Count = Left.Count;
            Right = pivot;
            Right.Left = Left.Right;
            Left = Left.Left;
        }

        public void RotateLeft()
        {
            //make copy of root node
            AVLNode<T> pivot = new AVLNode<T>(this.Data);

            //set all data as root node data
            pivot.Data = Data;
            pivot.Count = Count;
            pivot.Right = Right;
            pivot.Left = Left;


            Data = Right.Data;
            Count = Right.Count;
            Left = pivot;
            Left.Right = Right.Left;
            Right = Right.Left;

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class AVLTree<T> where T : IComparable<T>
    {
        //root node
        public AVLNode<T> root { get; set; }
        //count
        public int Count { get; set; } = 0;
        //Add (T val)
        public void Add(T val)
        {
            //add to root if there is no root
            if (root == null)
            {
                root = new AVLNode<T>(val);
            }
            else
            {
                //add to root either left or right depending on value
                root.Add(val);
            }
            //increment count
            Count++;
        }

        //Contains (T val)
        public bool Contains(T val)
        {
            //if root is null, return false
            if (root == null)
            {
                return false;
            }
            else
            {
                //return true if root contains value
                return root.Contains(val);
            }
        }

        //Remove (T val)
        public void Remove(T val)
        {
            //if not null and contains value
            if (root != null && root.Contains(val))
            {
                //remove value
                root = root.Remove(val);
                root.Balance();
                //decrement count
                Count--;
            }
        }

        //Clear
        public void Clear()
        {
            //set root to null
            root = null;
            //set count to 0
            Count = 0;
        }

        //Count
        public int CountNodes()
        {
            return Count;
        }

        //Height
        public int Height()
        {
            //if root is null, return 0
            if (root == null)
            {
                return 0;
            }
            else
            {
                //return height of root
                return root.Height();
            }
        }

        //ToArray
        public T[] ToArray()
        {
            int counter = 0;
            T[] arrayToReturn = new T[Count];
            root.ToArray(counter, arrayToReturn);
            return arrayToReturn;
        }

        //InOrder
        public string InOrder()
        {
            //if null return empty string
            if (root == null)
            {
                return "";
            }
            else
            {
                //return in order string
                return root.InOrder().TrimEnd().TrimEnd(',');
            }
        }

        //PreOrder
        public string PreOrder()
        {
            //if null return empty string
            if (root == null)
            {
                return "";
            }
            else
            {
                //return pre order string
                return root.PreOrder().TrimEnd().TrimEnd(',');
            }
        }

        //PostOrder
        public string PostOrder()
        {
            //if null return empty string
            if (root == null)
            {
                return "";
            }
            else
            {
                //return post order string
                return root.PostOrder().TrimEnd().TrimEnd(',');
            }
        }
    }
}

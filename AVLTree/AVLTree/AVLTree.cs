using System;

namespace AVLTree
{
    public class AVLTree<TKey,TValue> where TKey : IComparable
    {
        public AVLTreeNode<TKey, TValue> Head
        {
            get;
            internal set;
        }

        public int Count
        {
            get;
            private set;
        }

        public void Add(TKey key, TValue value)
        {
            if (Head == null)
            {
                Head = new AVLTreeNode<TKey, TValue>(key, value, null, this);
            }
            else
            {
                AddTo(Head, key, value);
            }
            
            Count++;
        }

        private void AddTo(AVLTreeNode<TKey, TValue> node, TKey key, TValue value)
        {
            if (key.CompareTo(node.Key) < 0)
                if (node.Left == null)
                {
                    node.Left = new AVLTreeNode<TKey, TValue>(key, value, node, this);
                    node.Parent?.Balance();
                }
                else
                    AddTo(node.Left, key, value);
            else
            if (node.Right == null)
            {
                node.Right = new AVLTreeNode<TKey, TValue>(key, value, node, this);
                node.Parent?.Balance();
            }
            else           
                AddTo(node.Right, key, value);
        }

        public bool Contains(TKey key)
        {
            return Find(key) != null;
        }

        public AVLTreeNode<TKey, TValue> Find(TKey key)
        {
            var current = Head;

            while (current != null)
            {
                switch (current.CompareTo(key))
                {
                    case 1:
                        current = current.Left;
                        break;
                    case 0:
                        return current;
                    case -1:
                        current = current.Right;
                        break;
                }
            }

            return null;
        }

        public void Remove(TKey key)
        {
            if (Head == null)
                throw new InvalidOperationException("Can't remove from empty tree");

            var current = Find(key); 

            if (current == null)
                return;

            var treeToBalance = current.Parent;
            Count--;
            
            if (current.Right == null)
            {
                if (current.Parent == null)
                {
                    Head = current.Left;

                    if (Head != null)
                    {
                        Head.Parent = null;
                    }
                }
                else
                {
                    var result = current.Parent.CompareTo(current.Key);

                    if (result > 0)
                        current.Parent.Left = current.Left;
                    else
                        current.Parent.Right = current.Left;
                }
            }
            else 
            if (current.Right.Left == null)
            {
                current.Right.Left = current.Left;
                if (current.Parent == null)
                {
                    Head = current.Right;
                    if (Head != null)
                        Head.Parent = null;
                }
                else
                {
                    var result = current.Parent.CompareTo(current.Key);
                    if (result > 0)
                        current.Parent.Left = current.Right;
                    else
                        current.Parent.Right = current.Right;
                }
            }  
            else
            {
                var leftmost = current.Right.Left;

                while (leftmost.Left != null)
                {
                    leftmost = leftmost.Left;
                }     

                leftmost.Parent.Left = leftmost.Right;
                leftmost.Left = current.Left;
                leftmost.Right = current.Right;

                if (current.Parent == null)
                {
                    Head = leftmost;

                    if (Head != null)
                        Head.Parent = null;
                }
                else
                {
                    var result = current.Parent.CompareTo(current.Key);

                    if (result > 0)
                        current.Parent.Left = leftmost;
                    else
                        current.Parent.Right = leftmost;
                }
            }

            if (treeToBalance != null)
                treeToBalance.Balance();
            else
                Head?.Balance();
        }
    }

}
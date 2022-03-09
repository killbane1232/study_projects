using System;

namespace AVLTree
{
    public class AVLTreeNode<TKey, TValue> : IComparable<TKey> where TKey : IComparable
    {
        #region Props
        AVLTreeNode<TKey, TValue> _left;

        AVLTreeNode<TKey, TValue> _right;

        AVLTree<TKey, TValue> _tree;

        public AVLTreeNode<TKey, TValue> Left
        {
            get => _left;
            internal set
            {
                _left = value;
                if (_left != null)
                    _left.Parent = this;
            }
        }

        public AVLTreeNode<TKey, TValue> Right
        {
            get => _right;
            internal set
            {
                _right = value;

                if (_right != null)
                {
                    _right.Parent = this;
                }
            }
        }

        public AVLTreeNode<TKey, TValue> Parent
        {
            get;
            internal set;
        }

        public TKey Key
        {
            get;
        }

        public TValue Value
        {
            get;
            set;
        }
        private int LeftHeight => MaxChildHeight(Left);

        private int RightHeight => MaxChildHeight(Right);

        public int BalanceFactor => RightHeight - LeftHeight;
        #endregion
        public AVLTreeNode(TKey key, TValue value, AVLTreeNode<TKey, TValue> parent, AVLTree<TKey, TValue> tree)
        {
            Key = key;
            Value = value;
            Parent = parent;
            _tree = tree;
        }

        public int CompareTo(TKey other)
        {
            return Key.CompareTo(other);
        }

        internal void Balance()
        {
            if (BalanceFactor > 1)
            {
                if (Right.BalanceFactor < -1)
                    Right.LeftToRight();
                RightToLeft();
            }
            else 
            if (BalanceFactor < -1)
            {    if (Left.BalanceFactor > 1)
                    Left.RightToLeft();
                LeftToRight();
            }
        }

        public int MaxChildHeight(AVLTreeNode<TKey, TValue> node)
        {
            if (node != null)
            {
                return 1 + Math.Max(MaxChildHeight(node.Left), MaxChildHeight(node.Right));
            }

            return 0;
        }
        
        private void RightToLeft()
        {
            var newRoot = Right;
            ReplaceParent(newRoot);
            Right = newRoot.Left;
            newRoot.Left = this;
        }

        private void LeftToRight()
        {
            var newRoot = Left;
            ReplaceParent(newRoot);
            
            Left = newRoot.Right; 
            newRoot.Right = this;
        }

        private void ReplaceParent(AVLTreeNode<TKey, TValue> newRoot)
        {
            if (Parent != null)
            {
                if (Parent.Left == this)
                {
                    Parent.Left = newRoot;
                }
                else if (Parent.Right == this)
                {
                    Parent.Right = newRoot;
                }
            }
            else
            {
                _tree.Head = newRoot;
            }

            newRoot.Parent = Parent;
            Parent = newRoot;
        }
    }
}

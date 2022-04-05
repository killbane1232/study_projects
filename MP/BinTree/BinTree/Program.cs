using System;

namespace BinTree
{
    public class BinTree<TKey,TValue> where TKey : IComparable
    {
        #region Свойства
        public TKey Key
        {
            get;
            private set;
        }

        public TValue Value
        {
            get;
            private set;
        }

        public BinTree<TKey, TValue> More
        {
            get;
            private set;
        }
        public BinTree<TKey, TValue> Less
        {
            get;
            private set;
        }
        #endregion
        #region Переменные
        private bool _exist;
        #endregion
        #region Методы
        public void Add(TKey item)
        {
            if (!_exist) 
            { 
                Key = item;
                _exist = true;
            }
            else
            {
                int compare = Key.CompareTo(item);
                if (compare == 0)
                    return;
                if (Key.CompareTo(item) < 0)
                {
                    if (More == null)
                        More = new BinTree<TKey,TValue>();

                    More.Add(item);
                }
                else
                {
                    if (Less == null)
                        Less = new BinTree<TKey, TValue>();

                    Less.Add(item);
                }
            }
        }
        public BinTree<TKey, TValue> Find(TKey item)
        {
            if (!_exist)
                return null;
            int compare = Key.CompareTo(item);
            switch (compare)
            {
                case -1:
                    if (More != null)
                        return More.Find(item);
                    break;
                case 0:
                    return this;
                case 1:
                    if (Less != null)
                        return Less.Find(item);
                    break;
            }
            return null;
        }
        public bool Contains(TKey item)
        {
            if (!_exist)
                return false;
            return Find(item) != null;
        }
        public void Remove(TKey item)
        {
            if (!_exist)
                return;

            if (Key.CompareTo(item) != 0)
            {
                var found = Find(item);
                if (found != null)
                    found.Remove(item);
                return;
            }

            if (Less == null)
            {
                if (More == null)
                {
                    _exist = false;
                    return;
                }
                Less = More.Less;
                Key = More.Key;
                _exist = More._exist;
                More = More.More;
            }
            else
            {
                if (More == null)
                {
                    More = Less.More;
                    Key = Less.Key;
                    _exist = Less._exist;
                    Less = Less.Less;
                    return;
                }
                var Closest = More;
                while (Closest.Less != null)
                    Closest = Closest.Less;
                Key = Closest.Key;
                Closest._exist = false;
                if (Closest.More != null)
                {
                    Closest.Less = Closest.More.Less;
                    Closest.Value = Closest.More.Value;
                    Closest._exist = Closest.More._exist;
                    Closest.More = Closest.More.More;
                }
            }
        }
        #endregion
    }
}

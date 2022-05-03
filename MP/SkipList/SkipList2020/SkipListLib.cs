using System;
using System.Collections;
using System.Collections.Generic;

namespace SkipList2020
{
    public class SkipList<TKey, TValue> :
        IEnumerable<KeyValuePair<TKey, TValue>>
        where TKey : IComparable<TKey>
    {
        Node<TKey, TValue>[] _head;
        readonly double _probability;
        readonly int _maxLevel;
        int _curLevel;
        Random _rd;
        public int Count { get; private set; }
        public SkipList(int maxLevel = 10, double probability = 0.5)
        {
            _maxLevel = maxLevel;
            _probability = probability;
            _head = new Node<TKey, TValue>[_maxLevel];
            for (int i = 0; i < maxLevel; i++)
            {
                _head[i] = new Node<TKey, TValue>();
                if (i == 0) continue;
                _head[i - 1].Up = _head[i];
                _head[i].Down = _head[i - 1];
            }

            _curLevel = 0;
            _rd = new Random(DateTime.Now.Millisecond);
        }

        public void Add(TKey key, TValue value)
        {
            var prevNode = new Node<TKey, TValue>[_maxLevel];
            var currentNode = _head[_curLevel];
            for (int i = _curLevel; i >= 0; i--)
            {
                while (currentNode.Right != null && currentNode.Right.Key.CompareTo(key) < 0)
                {
                    currentNode = currentNode.Right;
                }
                prevNode[i] = currentNode;
                if (currentNode.Down == null)
                    break;
                currentNode = currentNode.Down;
            }
            int level = 0;
            while (_rd.NextDouble() < _probability && level < _maxLevel - 1)
            {
                level++;
            }
            while (_curLevel < level)
            {
                _curLevel++;
                prevNode[_curLevel] = _head[_curLevel];
            }
            for (int i = 0; i <= level; i++)
            {
                var node = new Node<TKey, TValue>(key, value) { Right = prevNode[i].Right };
                prevNode[i].Right = node;
                if (i == 0) continue;
                node.Down = prevNode[i - 1].Right;
                prevNode[i - 1].Right.Up = node;
            }
            Count++;
        }

        public bool Contains(TKey key)
        {
            var node = _head[_curLevel];
            for (int i = _curLevel; i >= 0; i--)
            {
                if (node.Key.Equals(key))
                    return true;
                while (node.Right != null && node.Right.Key.CompareTo(key) <= 0)
                {
                    node = node.Right;
                }
                if (node.Down == null)
                    break;
                node = node.Down;
            }
            if (node.Key.Equals(key))
                return true;
            return false;
        }

        public void Remove(TKey key)
        {
            var prvNode = _head[_curLevel];
            for (int i = _curLevel; i >= 0; i--)
            {
                while (prvNode.Right != null && prvNode.Right.Right !=null && prvNode.Right.Right.Key.CompareTo(key) <= 0)
                {
                    prvNode = prvNode.Right;
                }
                if (prvNode.Right!=null && prvNode.Right.Key.Equals(key))
                    break;
                if (prvNode.Down == null)
                    return;
                prvNode = prvNode.Down;
            }
            if (prvNode.Right==null || !prvNode.Right.Key.Equals(key))
                return;
            while(prvNode!=null)
            {
                if (prvNode.Right.Key.Equals(key))
                    prvNode.Right = prvNode.Right.Right;
                else 
                {
                    while(prvNode.Right !=null && !prvNode.Right.Key.Equals(key))
                        prvNode = prvNode.Right;
                    if(prvNode.Right != null)
                        prvNode.Right = prvNode.Right.Right;
                }
                prvNode = prvNode.Down;
            }
            for(int i= _maxLevel-1; i>=0;i--)
            {
                if (_head[i].Right != null) 
                { 
                    _curLevel = i;
                    break;
                }

            }
            Count--;
        }
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            for (var node = _head[0].Right; node.Right != null; node = node.Right)
            {
                yield return new KeyValuePair<TKey, TValue>(node.Key, node.Value);
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

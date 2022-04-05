using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HashTable
{
    public class ChainHashTable<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>, IHashTable<TKey, TValue>
    {
        List<Pair<TKey, TValue>>[] _table;
        public int Count { get; private set; }
        private const int MaxChainLength = 8;
        private int _currentChainLength;
        private readonly GetPrimeNumber _primeNumber = new GetPrimeNumber();
        readonly HashMaker<TKey> _hashMaker;

        public ChainHashTable()
        {
            var capacity = _primeNumber.Next();
            _table = new List<Pair<TKey, TValue>>[capacity];
            _hashMaker = new HashMaker<TKey>(capacity);
        }

        public ChainHashTable(int primeNum)
        {
            var capacity = _primeNumber.Next();
            while (capacity < primeNum)
            {
                capacity = _primeNumber.Next();
            }
            _table = new List<Pair<TKey, TValue>>[primeNum];
            _hashMaker = new HashMaker<TKey>(primeNum);
        }

        public void Add(TKey key, TValue value)
        {
            var h = _hashMaker.ReturnHash(key);

            if (_table[h] == null || !_table[h].Exists(p => p.Key.Equals(key)))
            {
                if (_table[h] == null)
                    _table[h] = new List<Pair<TKey, TValue>>(MaxChainLength);
                var item = new Pair<TKey, TValue>(key, value);
                _table[h].Add(item);
                _currentChainLength = Math.Max(_currentChainLength, _table[h].Count);
                Count++;
            }
            else
            {
                throw new ArgumentException();
            }
            if (_currentChainLength >= MaxChainLength) // проверка размера
            {
                IncreaseTable();
            }
        }

        public void Remove(TKey key)
        {
            var hashKey = _hashMaker.ReturnHash(key);
            if (_table[hashKey] == null)
                return;
            int index = 0;
            int count = _table[hashKey].Count;
            for(;index<count;index++)
                if (_table[hashKey][index].Key.Equals(key)) 
                {
                    _table[hashKey].RemoveAt(index);
                    Count--;
                    return;
                }
        }

        private void IncreaseTable()
        {
            var buffer = new List<KeyValuePair<TKey, TValue>>();

            buffer.AddRange(this);

            var capacity = _primeNumber.Next();
            _currentChainLength = 0;
            _table = new List<Pair<TKey, TValue>>[capacity];
            _hashMaker.SimpleNumber = capacity;
            Count = 0;
            foreach(var each in buffer)
                Add(each.Key, each.Value);
        }

        public TValue this[TKey key]
        {
            get
            {
                var pair = Find(key);
                if (pair == null)
                    throw new KeyNotFoundException();
                return pair.Value;
            }

            set
            {
                var pair = Find(key);
                if (pair == null)
                    throw new KeyNotFoundException();
                pair.Value = value;
            }
        }


        private Pair<TKey, TValue> Find(TKey key)
        {
            int index = _hashMaker.ReturnHash(key);
            if (_table[index] == null) return null;
            foreach (var pair in _table[index])
            {
                if (pair.Key.Equals(key))
                    return pair;
            }
            return null;
        }

        public bool ContainsKey(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException();
            return Find(key) != null;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return (from list in _table where list != null from pair in list select new KeyValuePair<TKey, TValue>(pair.Key, pair.Value)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    internal class GetPrimeNumber
    {
        private int _current;
        readonly int[] _primes = { 61, 127, 257, 523, 1087, 2213, 4519, 9619, 19717, 40009 };

        public int Next()
        {
            if (_current < _primes.Length)
            {
                var value = _primes[_current];
                _current++;
                return value;
            }
            _current++;
            return (_current - _primes.Length) * _primes[_primes.Length - 1];
        }
    }
}


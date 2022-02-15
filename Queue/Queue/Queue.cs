using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Queue
{
    public class Queue<T>
    {
        private int _count;
        public int Count
        {
            get=>_count;
            private set
            {
                _count = value;
            }
        }
        public int Capacity
        {
            get=>_array.Length;
        }

        private T[] _array;
        private int _head;
        private int _tail;

        public Queue()
        {
            _array = new T[4];
            _head = 0;
            _tail = -1;
            _count = 0;
        }

        public void Enqueue(T item)
        {
            if (_count == Capacity)
            {
                var buffer = new T[_array.Length*2];
                Array.Copy(_array,_head,buffer,0,_array.Length-_head); 
                if (_head < _tail)
                    Array.Copy(_array,0,buffer, _array.Length-_head,_head);
                _head = 0;
                _tail = _count - 1;
                _array = buffer;
            }

            _tail = (_tail + 1) % _array.Length;
            _array[_tail] = item;
            _count++;
        }

        public T Dequeue()
        {
            if (_count == 0)
                throw new InvalidOperationException("Невозможно удалить элемент из пустой очереди");
            var item = _array[_head];
            _head=(_head+1)%_array.Length;
            _count--;
            return item;
        }

        public T Peek()
        {
            if (_count == 0)
                throw new InvalidOperationException("Невозможно получить элемент из пустой очереди");
            return _array[_head];
        }

        public bool Contains(T item)
        {
            EqualityComparer<T> equalityComparer = EqualityComparer<T>.Default;
            var cnt = _count;
            var index = _head;
            while (cnt-- > 0)
            {
                if (equalityComparer.Equals(_array[index], item))
                    return true;
                index = (index + 1) % _array.Length;
            }
            return false;
        }
    }
}

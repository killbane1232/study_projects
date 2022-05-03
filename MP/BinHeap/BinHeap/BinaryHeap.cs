using System.Collections.Generic;

namespace BinHeap
{
    class BinaryHeap<T>
    {
        private List<T> list;
        private IComparer<T> comparer;

        public BinaryHeap(IComparer<T> comparer)
        {
            this.comparer = comparer;
            list = new List<T>();
        }
        public BinaryHeap(T[] sourceArray, IComparer<T> comparer)
        {
            this.comparer = comparer;
            list = new List<T>(sourceArray);
            for (var i = Count / 2; i >= 0; i--)
            {
                Heapify(i);
            }
        }

        public int Count
        {
            get
            {
                return list.Count;
            }
        }

        public T Min
        {
            get { return list[0]; }
        }

        public void Add(T value)
        {
            list.Add(value);
            var i = Count - 1;
            var parent = (i - 1) / 2;

            while (i > 0 && comparer.Compare(list[parent],list[i]) > 0)
            {
                T temp = list[i];
                list[i] = list[parent];
                list[parent] = temp;

                i = parent;
                parent = (i - 1) / 2;
            }
        }

        public void Heapify(int i)
        {
            int leftChild, rightChild, largestChild;

            for (; ; )
            {
                leftChild = 2 * i + 1;
                rightChild = 2 * i + 2;
                largestChild = i;

                if (leftChild < Count && comparer.Compare(list[leftChild], list[largestChild]) < 0)
                {
                    largestChild = leftChild;
                }

                if (rightChild < Count && comparer.Compare(list[rightChild], list[largestChild]) < 0)
                {
                    largestChild = rightChild;
                }

                if (largestChild == i)
                {
                    break;
                }

                T temp = list[i];
                list[i] = list[largestChild];
                list[largestChild] = temp;
                i = largestChild;
            }
        }

        public T RemoveMin()
        {
            T result = list[0];
            list[0] = list[Count - 1];
            list.RemoveAt(Count - 1);
            Heapify(0);
            return result;
        }

        public void HeapSort(T[] array)
        {
            list = new List<T>(array);
            for (var i = Count / 2; i >= 0; i--)
            {
                Heapify(i);
            }
            for (var i = 0; i < array.Length; i++)
            {
                array[i] = RemoveMin();
                Heapify(0);
            }
        }
    }
}

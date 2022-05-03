using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BinHeap
{
    internal class MainClass
    {
        static void Main()
        {
            var blow = new BinaryHeap<int>(Comparer<int>.Default);
            for (int i = 0; i < 100000; i++)
                blow.Add(i);
            for (int i = 0; i < 100000; i++)
                blow.RemoveMin();

            int k = 5;
            int n = 5;

            var matrix = new List<List<int>>(k);
            for (int i = 0; i < k; i++)
            {
                var list = new List<int>();
                var rand = new Random((int)DateTime.Now.Ticks);
                for (int j = 0; j < n; j++)
                {
                    var ran = rand.Next();
                    while (list.Contains(ran))
                        ran = rand.Next();
                    list.Add(rand.Next());
                }
                var arr = list.ToArray();
                var heap = new BinaryHeap<int>(Comparer<int>.Default);
                heap.HeapSort(arr);
                matrix.Add(new List<int>(arr));
            }

            var strangeHeap = new BinaryHeap<List<int>>(new ListComparer<int>());
            for (int i = 0; i < k; i++)
                strangeHeap.Add(matrix[i]);

            var result = new List<int>();
            for(int i =0; i < k*n;i++)
            {
                var min = strangeHeap.RemoveMin();
                result.Add(min[0]);
                min.RemoveAt(0);
                if(min.Count > 0)
                    strangeHeap.Add(min);
            }

            for(int i =0;i<result.Count;i++)
                Console.WriteLine(result[i]);
        }
    }
    public class ListComparer<T> : IComparer<List<T>> where T : IComparable<T>
    {
        public int Compare([AllowNull] List<T> x, [AllowNull] List<T> y)
        {
            return Comparer<T>.Default.Compare(x[0], y[0]);
        }
    }

}

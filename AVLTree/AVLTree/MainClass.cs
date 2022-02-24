using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace AVLTree
{
    class MainClass
    {
        public static void Main()
        {
            var list = new List<int>();
            var rand = new Random((int) DateTime.Now.Ticks);
            for (int i = 0; i < 10000; i++)
            {
                var ran = rand.Next();
                while (list.Contains(ran))
                    ran = rand.Next();
                list.Add(rand.Next());
            }

        var blow = new AVLTree<int, int>();
            for (int i = 0; i < 10000; i++)
                blow.Add(list[i], 0);
            for (int i = 0; i < 10000; i++)
                blow.Remove(list[i]);



            var watch = new Stopwatch();
            var avl = new AVLTree<int, int>();
            watch.Start();
            for(int i =0;i<10000;i++)
                avl.Add(list[i],0);
            watch.Stop();
            Console.WriteLine($"AVL ADD:{watch.Elapsed}");
            watch.Reset();
            watch.Start();
            for (int i = 0; i < 10000; i++)
                avl.Find(list[i]);
            watch.Stop();
            Console.WriteLine($"AVL FIN:{watch.Elapsed}");
            watch.Reset();
            watch.Start();
            for (int i = 5000; i < 7000; i++)
                avl.Remove(list[i]);
            watch.Stop();
            Console.WriteLine($"AVL REM:{watch.Elapsed}");
            watch.Reset();

            var dict = new SortedDictionary<int, int>();
            watch.Start();
            for (int i = 0; i < 10000; i++)
                dict.Add(list[i], 0);
            watch.Stop();
            Console.WriteLine($"DIC ADD:{watch.Elapsed}");
            watch.Reset();
            watch.Start();
            for (int i = 0; i < 10000; i++)
                dict.ContainsKey(list[i]);
            watch.Stop();
            Console.WriteLine($"DIC FIN:{watch.Elapsed}");
            watch.Reset();
            watch.Start();
            for (int i = 5000; i < 7000; i++)
                dict.Remove(list[i]);
            watch.Stop();
            Console.WriteLine($"DIC REM:{watch.Elapsed}");
            watch.Reset();
        }
    }
}

using System;
using System.Diagnostics;

namespace Queue
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = 10000;

            var a = new Queue<int>();
            var b = new System.Collections.Generic.Queue<int>();
            var rand = new Random((int)DateTime.Now.Ticks);
            var list = new System.Collections.Generic.List<int>();

            for (int i = 0; i < count; i++)
            {
                var item = rand.Next();
                list.Add(item);
            }

            var watch = new Stopwatch();
            watch.Start();
            for (int i = 0; i < count; i++)
            {
                a.Enqueue(list[i]);
            }
            watch.Stop();
            var res1 = watch.ElapsedTicks;

            watch.Reset();
            watch.Start();
            for (int i = 0; i < count; i++)
            {
                b.Enqueue(list[i]);
            }
            watch.Stop();
            Console.WriteLine($"Equeue:\nMy:{res1}\nSystem:{watch.ElapsedTicks}");

            watch.Reset();
            watch.Start();
            for (int i = 0; i < count; i++)
            {
                a.Contains(list[i]);
            }
            watch.Stop();
            res1 = watch.ElapsedTicks;

            watch.Reset();
            watch.Start();
            for (int i = 0; i < count; i++)
            {
                b.Contains(list[i]);
            }
            watch.Stop();
            Console.WriteLine($"Contains:\nMy:{res1}\nSystem:{watch.ElapsedTicks}");

            watch.Reset();
            watch.Start();
            for (int i = 0; i < count; i++)
            {
                a.Dequeue();
            }
            watch.Stop();
            res1 = watch.ElapsedTicks;

            watch.Reset();
            watch.Start();
            for (int i = 0; i < count; i++)
            {
                b.Dequeue();
            }
            watch.Stop();
            Console.WriteLine($"Dequeue:\nMy:{res1}\nSystem:{watch.ElapsedTicks}");
        }
    }
}

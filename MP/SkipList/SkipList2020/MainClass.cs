using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SkipList2020
{
    internal class MainClass
    {
        static void Main(string[] args)
        {
            var blow = new SkipList<int, int>();
            for (int i = 0; i < 100000; i++)
                blow.Add(i, i);
            for (int i = 0; i < 100000; i++)
                blow.Remove(i);

            var sort = new SortedList<int, int>();
            var skip = new SkipList<int, int>();

            var list = new List<int>();
            var rand = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; i < 10000; i++)
            {
                var ran = rand.Next();
                while (list.Contains(ran))
                    ran = rand.Next();
                list.Add(ran);
            }

            var timer = new Stopwatch();


            long timerBuffer = 0;

            timer.Start();


            for (int i = 0; i < 10000; i++)
                skip.Add(list[i], 0);

            timer.Stop();

            timerBuffer += timer.ElapsedMilliseconds;
            Console.WriteLine("skipAdd:" + timer.ElapsedMilliseconds);
            timer.Reset();



            timer.Start();

            for (int i = 5000; i < 7000; i++)
                skip.Remove(list[i]);

            timer.Stop();
            timerBuffer += timer.ElapsedMilliseconds;

            Console.WriteLine("skipRem:" + timer.ElapsedTicks);
            Console.WriteLine("skipAll:" + timerBuffer);
            timer.Reset();

            timerBuffer = 0;

            timer.Start();

            for (int i = 0; i < 10000; i++)
                sort.Add(list[i], 0);

            timer.Stop();
            Console.WriteLine("sortAdd:" + timer.ElapsedMilliseconds);
            timerBuffer += timer.ElapsedMilliseconds;

            timer.Reset();



            timer.Start();

            for (int i = 5000; i < 7000; i++)
                sort.Remove(list[i]);

            timer.Stop();
            timerBuffer += timer.ElapsedMilliseconds;

            Console.WriteLine("sortRem:" + timer.ElapsedTicks);
            Console.WriteLine("sortAll:" + timerBuffer);
        }
    }
}

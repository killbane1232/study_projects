using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;

namespace HashTable
{
    internal class MainClass
    {
        static void Main(string[] args)
        {
            var blow = new ChainHashTable<int, int>();
            for (int i = 0; i < 100000; i++)
                blow.Add(i, i);
            for (int i = 0; i < 100000; i++)
                blow.Remove(i);

            var dict = new Dictionary<string, int>();
            var hash = new ChainHashTable<string, int>();
            var reader = new StreamReader("file.txt");
            var data = reader.ReadToEnd().ToLower().Split(new char[]{
                ',', ':', ' ', '.', '!', ';', '<', '?',
                '>', '-', '0', '1', '2', '3', '4', '5',
                '6', '7', '8', '9', '/', '"', '*', '(',
                ')', '\'','\n','\r','\\'
                },
                StringSplitOptions.RemoveEmptyEntries);

            var timer = new Stopwatch();
            
            var toRemoveHash = new List<string>();
            var toRemoveDict = new List<string>();
            int cnt = data.Length;
            timer.Start();
            for (int i = 0; i < cnt; i++)
            {
                if (hash.ContainsKey(data[i]))
                    hash[data[i]]++;
                else
                    hash.Add(data[i], 1);
            }
            timer.Stop();

            Console.WriteLine("hashAdd:" + timer.ElapsedMilliseconds);
            foreach (var each in hash)
            {
                if (each.Value > 27)
                    toRemoveHash.Add(each.Key);
            }

            timer.Start();
            foreach (var each in toRemoveHash)
                hash.Remove(each);
            timer.Stop();

            Console.WriteLine("hashAll:" + timer.ElapsedTicks);
            timer.Reset();

            timer.Start();
            for (int i = 0; i < cnt; i++)
            {
                if (dict.ContainsKey(data[i]))
                    dict[data[i]]++;
                else
                    dict.Add(data[i], 1);
            }
            timer.Stop();
            Console.WriteLine("dictAdd:" + timer.ElapsedMilliseconds);

            foreach (var each in dict)
            {
                if (each.Value > 27)
                    toRemoveDict.Add(each.Key);
            }

            timer.Start();
            foreach (var each in toRemoveDict)
                dict.Remove(each);
            timer.Stop();

            Console.WriteLine("dictAll:" + timer.ElapsedTicks);
        }
    }
}
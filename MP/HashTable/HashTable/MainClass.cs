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

            timer.Start();
            foreach(var item in data)
            {
                if(hash.ContainsKey(item))
                    hash[item]++;
                else
                    hash.Add(item, 1);
            }
            timer.Stop();
            Console.WriteLine("hashAdd:" + timer.Elapsed);
            timer.Reset();

            timer.Start();
            foreach (var item in data)
                if(dict.ContainsKey(item))
                    dict[item]++;
                else
                    dict.Add(item, 1);
            timer.Stop();
            Console.WriteLine("dictAdd:" + timer.Elapsed);
            timer.Reset();

            var toRemoveHash = new List<string>();
            var toRemoveDict = new List<string>();
            foreach (var each in hash)
            {
                if (each.Value > 27)
                    toRemoveHash.Add(each.Key);
            }
            foreach (var each in dict)
            {
                if (each.Value > 27)
                    toRemoveDict.Add(each.Key);
            }

            timer.Start();
            foreach (var each in toRemoveHash)
                hash.Remove(each);
            timer.Stop();
            Console.WriteLine("hashRem:" + timer.Elapsed);
            timer.Reset();

            timer.Start();
            foreach (var each in toRemoveDict)
                dict.Remove(each);
            timer.Stop();

            Console.WriteLine("dictRem:" + timer.Elapsed);
            Console.WriteLine("hashCnt:" + hash.Count);
            Console.WriteLine("dictCnt:" + dict.Count);
        }
    }
}
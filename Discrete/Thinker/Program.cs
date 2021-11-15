﻿using System;
namespace Thinker
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new TreeBuilder("E^Dv(BvA)^C→F^G^H→I^J^K^NvO");
            Console.WriteLine(builder.Root.Run());
            Console.WriteLine(builder.Root.Check());
            var table = builder.TruthTable();
            foreach(var row in table)
            {
                foreach(var each in row)
                    Console.Write(each+" ");
                Console.WriteLine();
            }
        }
    }
}
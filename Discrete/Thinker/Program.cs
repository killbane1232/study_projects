﻿using System.Collections.Immutable;
using System;
namespace Thinker
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new TreeBuilder("¬(BvA)");
            Console.WriteLine(builder.Root.Run());
            Console.WriteLine(builder.Root.Check());
            Console.WriteLine(builder.TruthTable());
        }
    }
}
﻿using System;
namespace Thinker
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new TreeBuilder("(A>B)");
            var builder1 = new TreeBuilder("(~A+B)&(C>C)");
            Console.WriteLine(builder.Root.Run());
            Console.WriteLine(builder.Root.Check());
            var table = builder.TruthTable();
            Console.Write(TreeBuilder.TruthTableCheck(builder,builder1));
        }
    }
}
﻿using System;
namespace Thinker
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new OPZBuilder("(A>B)");
            var builder1 = new OPZBuilder("(~A+B)&(C>C)");
            Console.WriteLine(builder.Root.Run());
            Console.WriteLine(builder.Root.Check());
            var knf = builder.BuildKNF();
            Console.WriteLine(knf);
            var builder2 = new OPZBuilder(knf);
            var table = builder.TruthTable();
            var table1 = builder2.TruthTable();
            foreach(var row in table1)
            {
                foreach(var each in row)
                    Console.Write(each+" ");
                Console.WriteLine();
            }
            Console.Write(OPZBuilder.TruthTableCheck(builder,builder1));
            Console.Write(OPZBuilder.TruthTableCheck(builder,builder2));
            Console.Write(OPZBuilder.TruthTableCheck(builder2,builder1));
        }
    }
}
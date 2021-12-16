﻿using System;
namespace Thinker
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new OPZBuilder();
            Console.WriteLine(builder.TryBuild("((а+а+а))"));
            Console.WriteLine(builder.TryBuild("((((а+а)+а))+a)"));
            Console.WriteLine(builder.TryBuild("((((а+а)+а))+a)"));
            Console.WriteLine(builder.TryBuild("((((а+а)+а))+a)"));
        }
    }
}
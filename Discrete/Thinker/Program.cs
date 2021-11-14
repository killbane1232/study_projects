﻿using System;
namespace Thinker
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new TreeBuilder("((B→A)→(C→(B→A)))");
            Console.WriteLine(builder.Root.Run());
            Console.WriteLine(builder.Root.Check());
        }
    }
}
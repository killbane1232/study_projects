using System;
namespace Strategy
{
    class NoShout:IShout
    {
        public void Shout()
        {
            Console.WriteLine("...");
        }
    }
}
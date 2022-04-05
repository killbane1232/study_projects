using System;
namespace Strategy
{
    class HerayShout:IShout
    {
        public void Shout()
        {
            Console.WriteLine("Heray");
        }
    }
}
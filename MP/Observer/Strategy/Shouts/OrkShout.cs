using System;
namespace Strategy
{
    class OrkShout:IShout
    {
        public void Shout()
        {
            Console.WriteLine("BORK");
        }
    }
}
using System;
namespace Strategy
{
    class YraShout:IShout
    {
        public void Shout()
        {
            Console.WriteLine("Yra");
        }
    }
}
using System;

namespace Strategy
{
    class Program
    {
        static void Main(string[] args)
        {
            var si = new Soldier(new HerayShout(), new AxeFight());
            si.Fight();
            si.Shout();
        }
    }
}

using System;
namespace Strategy
{
    class NoFight:IFight
    {
        public void Fight()
        {
            Console.WriteLine("dont attack");
        }
    }
}
using System;
namespace Strategy
{
    class SwordFight:IFight
    {
        public void Fight()
        {
            Console.WriteLine("attacks with Sword");
        }
    }
}
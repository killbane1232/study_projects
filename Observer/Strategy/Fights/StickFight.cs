using System;
namespace Strategy
{
    class StickFight:IFight
    {
        public void Fight()
        {
            Console.WriteLine("attacks with Stick");
        }
    }
}
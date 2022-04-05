using System;
namespace Strategy
{
    class AxeFight:IFight
    {
        public void Fight()
        {
            Console.WriteLine("attacks with axe");
        }
    }
}
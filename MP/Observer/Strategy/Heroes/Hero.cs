using System;
namespace Strategy
{
    class Hero
    {
        IShout shouter;
        IFight fighter;
        public Hero(IShout shout, IFight fight)
        {
            shouter = shout;
            fighter = fight;
        }
        public void Shout()
        {
            shouter.Shout();
        }
        public void Fight()
        {
            fighter.Fight();
        }
    }
}
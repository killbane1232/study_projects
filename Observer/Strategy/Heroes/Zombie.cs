using System;
namespace Strategy
{
    class Zombie:Hero
    {
        public Zombie(IShout shout, IFight fight):base(shout,fight){}
    }
}
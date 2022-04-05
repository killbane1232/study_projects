using System;
namespace Strategy
{
    class Soldier:Hero
    {
        public Soldier(IShout shout, IFight fight):base(shout,fight){}
    }
}
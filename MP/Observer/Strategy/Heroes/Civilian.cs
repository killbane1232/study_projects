using System;
namespace Strategy
{
    class Civilian:Hero
    {
        public Civilian(IShout shout, IFight fight):base(shout,fight){}
    }
}
using System;
namespace Strategy
{
    class ArmyOfficer:Hero
    {
        public ArmyOfficer(IShout shout, IFight fight):base(shout,fight){}
    }
}
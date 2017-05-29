using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurvivalGame
{
    public class SurvivorType : UnitType
    {
        public SurvivorType(string Sprite, float Speed, int MaxHealth)
            : base(Sprite, Speed, MaxHealth)
        {

        }

        public static SurvivorType Worker = new SurvivorType("Survivor", 0.1f, 50);
    }
}

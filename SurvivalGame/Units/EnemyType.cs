using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurvivalGame
{
    public class EnemyType : UnitType
    {
        public EnemyType(string Sprite, float Speed, int MaxHealth)
            : base(Sprite, Speed, MaxHealth)
        {

        }

        public static EnemyType Worker = new EnemyType("Enemy", 0.1f, 50);
    }
}

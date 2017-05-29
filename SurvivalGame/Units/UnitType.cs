using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurvivalGame
{
    public class UnitType
    {
        public string Sprite { get; set; }
        public float Speed { get; set; }
        public int MaxHealth { get; set; }

        public UnitType(string Sprite, float Speed, int MaxHealth)
        {
            this.Sprite = Sprite;
            this.Speed = Speed;
            this.MaxHealth = MaxHealth;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SurvivalGame
{
    public class Survivor : Unit
    {
        public Survivor(Map World, SurvivorType Type, float X, float Y)
            : base(World, Type, X, Y)
        {
        }

        public override void Update(GameTime gameTime, InputState Input)
        {
            base.Update(gameTime, Input);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}

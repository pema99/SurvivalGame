using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SurvivalGame
{
    public class Map
    {
        public Tile[,] Data { get; set; }
        public List<Unit> Units { get; set; }

        public Map(int MapWidth, int MapHeight)
        {
            this.Data = new Tile[MapWidth, MapHeight];
            this.Units = new List<Unit>();
        }
    }
}

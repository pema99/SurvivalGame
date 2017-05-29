using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurvivalGame
{
    public class TileType
    {
        public string Sprite { get; set; }
        public bool Walkable { get; set; }

        public TileType(string Sprite, bool Walkable)
        {
            this.Sprite = Sprite;
            this.Walkable = Walkable;
        }

        public static TileType Water = new TileType("Water", false);
        public static TileType Beach = new TileType("Beach", true);
        public static TileType Grass = new TileType("Grass", true);
        public static TileType TallGrass = new TileType("TallGrass", true);
        public static TileType Stone = new TileType("Stone", true);
        public static TileType Mountain = new TileType("Mountain", false);
        public static TileType Snow = new TileType("Snow", false);
    }
}

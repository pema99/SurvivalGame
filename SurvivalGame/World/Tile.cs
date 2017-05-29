using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurvivalGame
{
    public class Tile
    {
        public TileType Type { get; set; }
        public ResourceType Resource { get; set; }

        public bool Walkable { get { return Type.Walkable && Resource == null; } }

        public Tile(TileType Type)
        {
            this.Type = Type;
        }
    }
}

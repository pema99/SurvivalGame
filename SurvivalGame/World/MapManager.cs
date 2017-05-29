using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurvivalGame
{
    public class MapManager
    {
        public Map Map { get; set; }

        public MapManager(Map Map)
        {
            this.Map = Map;
        }

        public void Update(GameTime gameTime, InputState Input)
        {
            foreach (Unit U in Map.Units)
            {
                U.Update(gameTime, Input);      
            }
        }

        public void Draw(SpriteBatch spriteBatch, Camera Camera)
        {
            int Left = (int)((Camera.Position.X - Globals.ScreenWidth / 2 / Camera.Scale) / Globals.TileSize);
            int Top = (int)((Camera.Position.Y - Globals.ScreenHeight / 2 / Camera.Scale) / Globals.TileSize);
            int Right = (int)((Camera.Position.X + Globals.ScreenWidth / 2 / Camera.Scale) / Globals.TileSize) + 1;
            int Bottom = (int)((Camera.Position.Y + Globals.ScreenHeight / 2 / Camera.Scale) / Globals.TileSize) + 1;

            if (Left < 0)
            {
                Left = 0;
            }
            if (Right >= Map.Data.GetLength(0))
            {
                Right = Map.Data.GetLength(0) - 1;
            }
            if (Top < 0)
            {
                Top = 0;
            }
            if (Bottom >= Map.Data.GetLength(1))
            {
                Bottom = Map.Data.GetLength(1) - 1;
            }
            for (int i = Left; i < Right; i++)
            {
                for (int j = Top; j < Bottom; j++)
                {
                    spriteBatch.Draw(Assets.Sprites[Map.Data[i, j].Type.Sprite], new Rectangle(i * Globals.TileSize, j * Globals.TileSize, Globals.TileSize, Globals.TileSize), Color.White);
                    if (Map.Data[i, j].Resource != null)
                    {
                        spriteBatch.Draw(Assets.Sprites[Map.Data[i, j].Resource.Sprite], new Rectangle(i * Globals.TileSize, j * Globals.TileSize, Globals.TileSize, Globals.TileSize), Color.White);
                    }
                }
            }

            foreach (Unit U in Map.Units)
            {
                U.Draw(spriteBatch);
            }
        }
    }
}

using System;
using MonoGame.Framework;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SurvivalGame
{
    public static class Assets
    {
        public static Dictionary<string, Texture2D> Sprites { get; set; }
        public static SpriteFont Font { get; set; }

        public static void LoadContent(ContentManager Content)
        {
            Sprites = new Dictionary<string, Texture2D>();

            foreach (var FName in Directory.GetFiles("Content/Sprites"))
            {
                if (FName.Contains(".xnb") || FName.Contains(".ase")) continue;

                var FixedFName = FName.Replace(".png", "").Replace(".jpg", "").Replace(".gif", "");
                FixedFName = FixedFName.Replace("Content/", "");

                Sprites.Add(
                    FixedFName.Replace("Sprites\\", ""),
                    Content.Load<Texture2D>(FixedFName));
            }

            Font = Content.Load<SpriteFont>("Courier New");
        }
    }
}


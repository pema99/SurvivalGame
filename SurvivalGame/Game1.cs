using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SurvivalGame
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        ScreenManager SM { get; set; }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = Globals.ScreenWidth;
            graphics.PreferredBackBufferHeight = Globals.ScreenHeight;
            graphics.ApplyChanges();

            SM = new ScreenManager(this);
            this.Components.Add(SM);
            SM.AddScreen(new GameScreen(SM));

            this.IsMouseVisible = true;
        }
    }
}

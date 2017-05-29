using System;
using MonoGame.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SurvivalGame
{
    public class GameScreen : Screen
    {
        public MapManager MapManager { get; set; }
        public Camera Camera { get; set; }

        public GameScreen(ScreenManager ScreenManager)
        {
            this.IsPopup = false;
            this.ScreenManager = ScreenManager;
            this.ScreenState = ScreenState.TransitionOn;
            this.TransitionOffTime = TimeSpan.FromSeconds(1);
            this.TransitionOnTime = TimeSpan.FromSeconds(1);
            this.TransitionPosition = 1f;
            this.IsExiting = false;
        }

        public override void Update(GameTime gameTime, bool OtherScreenHasFocus, bool CoveredByOtherScreen, InputState Input)
        {
            #region Debug
            if (Input.MouseLeft(ButtonState.Pressed))
            {
                var MousePos = Camera.ScreenToWorld(Input.CurrentMouseState.Position.ToVector2());
                MapManager.Map.Units[0].TileX = (int)(MousePos.X / Globals.TileSize);
                MapManager.Map.Units[0].TileY = (int)(MousePos.Y / Globals.TileSize);
            }
            if (Input.MouseRight(ButtonState.Pressed))
            {
                var MousePos = Camera.ScreenToWorld(Input.CurrentMouseState.Position.ToVector2());
                var X2 = (int)(MousePos.X / Globals.TileSize);
                var Y2 = (int)(MousePos.Y / Globals.TileSize);
                MapManager.Map.Units[0].MoveTo(X2, Y2);
            }
            #endregion

            if (MapManager != null)
            {
                MapManager.Update(gameTime, Input);
            }

            Camera.Update(gameTime, Input);

            base.Update(gameTime, OtherScreenHasFocus, CoveredByOtherScreen, Input);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, Camera.Transform);
            if (MapManager != null)
            {
                MapManager.Draw(spriteBatch, Camera);
            }
            spriteBatch.End();

            spriteBatch.Begin();
            spriteBatch.DrawString(Assets.Font, "FPS: " + (1 / (float)gameTime.ElapsedGameTime.TotalSeconds), Vector2.Zero, Color.Black);
            spriteBatch.End();

            #region Transition
            if (TransitionPosition > 0)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(ScreenManager.BlankTexture, new Rectangle(0, 0, Globals.ScreenWidth, Globals.ScreenHeight), Color.Black * TransitionPosition);
                spriteBatch.End();
            }
            #endregion

            base.Draw(gameTime);
        }

        public override void LoadContent()
        {
            MapManager = new MapManager(MapGenerator.GenerateStandard(150, 150, new Random().Next()));
            Camera = new Camera(ScreenManager.Viewport, Vector2.Zero, 0.7f, 1);

            //Test unit
            MapManager.Map.Units.Add(new Survivor(MapManager.Map, SurvivorType.Worker, 0, 0));

            base.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }
    }
}


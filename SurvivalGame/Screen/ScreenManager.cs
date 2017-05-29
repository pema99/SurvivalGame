using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;

namespace SurvivalGame
{
	public class ScreenManager : DrawableGameComponent
	{
		private List<Screen> Screens { get; set; }
        private List<Screen> ScreensToUpdate { get; set; }
        private InputState Input { get; set; }
        private bool Initialized { get; set; }
        public SpriteBatch SpriteBatch { get; protected set; }
		public Texture2D BlankTexture { get; set; }

		public Viewport Viewport
		{
			get { return base.Game.GraphicsDevice.Viewport; }
		}
        
		public ScreenManager(Game game) : base(game)
        {
            Screens = new List<Screen>();
            ScreensToUpdate = new List<Screen>();
            Input = new InputState();
        }

		public override void Initialize()
		{
			base.Initialize();
			Initialized = true;
		}

		protected override void LoadContent()
		{
			Assets.LoadContent(Game.Content);

			SpriteBatch = new SpriteBatch(base.GraphicsDevice);
			BlankTexture = new Texture2D(base.GraphicsDevice, 1, 1);
			BlankTexture.SetData<Color>(new Color[]
			{
				Color.White
			});

			foreach (Screen current in Screens)
			{
				current.LoadContent();
			}
		}

		protected override void UnloadContent()
		{
			foreach (Screen current in Screens)
			{
				current.UnloadContent();
			}
		}

		public override void Update(GameTime gameTime)
		{
			Input.Update();

			ScreensToUpdate.Clear();
			foreach (Screen current in Screens)
			{
				ScreensToUpdate.Add(current);
			}

			bool OtherScreenHasFocus = !base.Game.IsActive;
			bool CoveredByOtherScreen = false;

			while (ScreensToUpdate.Count > 0)
			{
				Screen screen = ScreensToUpdate[ScreensToUpdate.Count - 1];
				ScreensToUpdate.RemoveAt(ScreensToUpdate.Count - 1);
				screen.Update(gameTime, OtherScreenHasFocus, CoveredByOtherScreen, Input);

				if (screen.ScreenState == ScreenState.TransitionOn || screen.ScreenState == ScreenState.Active)
				{
					if (!OtherScreenHasFocus)
					{
						OtherScreenHasFocus = true;
					}
					if (!screen.IsPopup)
					{
						CoveredByOtherScreen = true;
					}
				}
			}
		}

		public override void Draw(GameTime gameTime)
		{
            GraphicsDevice.Clear(Color.CornflowerBlue);
            foreach (Screen current in Screens)
			{	
				if (current.ScreenState != ScreenState.Hidden)
				{
					current.Draw(gameTime);
				}
			}
		}

		public void AddScreen(Screen screen)
		{
			screen.ScreenManager = this;
			screen.IsExiting = false;
			bool isInitialized = Initialized;
			if (isInitialized)
			{
				screen.LoadContent();
			}
			Screens.Add(screen);
		}

		public void RemoveScreen(Screen screen)
		{
			bool isInitialized = Initialized;
			if (isInitialized)
			{
				screen.UnloadContent();
			}
			Screens.Remove(screen);
			ScreensToUpdate.Remove(screen);
		}

		public Screen[] GetScreens()
		{
			return Screens.ToArray();
		}
	}
}


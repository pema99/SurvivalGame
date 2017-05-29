using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SurvivalGame
{
	public abstract class Screen
	{
		public bool OtherScreenHasFocus { get; protected set; }
		public bool IsPopup { get; set; }
		public TimeSpan TransitionOnTime { get; protected set; }
        public TimeSpan TransitionOffTime { get; protected set; }
		public float TransitionPosition { get; protected set; }
		public ScreenState ScreenState { get; set; }
		public bool IsExiting { get; set; }
		public ScreenManager ScreenManager { get; set; }
		public float TransitionAlpha
		{
			get
			{
				return 1f - TransitionPosition;
			}
		}
		public bool IsActive
		{
			get
			{
				return !OtherScreenHasFocus && (ScreenState == ScreenState.TransitionOn || ScreenState == ScreenState.Active);
			}
		}
		public SpriteBatch spriteBatch 
		{
			get 
			{ 
				return ScreenManager.SpriteBatch;
			}
		}
		public Viewport Viewport
        {
            get
            {
                return ScreenManager.Viewport;
            }
        }

		public virtual void LoadContent()
		{
		}

		public virtual void UnloadContent()
		{
		}

		public virtual void Update(GameTime gameTime, bool OtherScreenHasFocus, bool CoveredByOtherScreen, InputState Input)
		{
			this.OtherScreenHasFocus = OtherScreenHasFocus;
			if (IsExiting)
			{
				ScreenState = ScreenState.TransitionOff;
				if (!UpdateTransition(gameTime, TransitionOffTime, 1))
				{
					ScreenManager.RemoveScreen(this);
				}
			}
			else if (CoveredByOtherScreen)
			{
				if (UpdateTransition(gameTime, TransitionOffTime, 1))
				{
					ScreenState = ScreenState.TransitionOff;
				}
				else
				{
					ScreenState = ScreenState.Hidden;
				}
			}
			else
			{
				if (UpdateTransition(gameTime, TransitionOnTime, -1))
				{
					ScreenState = ScreenState.TransitionOn;
				}
				else
				{
					ScreenState = ScreenState.Active;
				}
			}
		}

		private bool UpdateTransition(GameTime gameTime, TimeSpan time, int direction)
		{
			float Temp;
			if (time == TimeSpan.Zero)
			{
				Temp = 1f;
			}
			else
			{
				Temp = (float)(gameTime.ElapsedGameTime.TotalMilliseconds / time.TotalMilliseconds);
			}
			TransitionPosition += Temp * (float)direction;

			bool result;
			if ((direction < 0 && TransitionPosition <= 0f) || (direction > 0 && TransitionPosition >= 1f))
			{
				TransitionPosition = MathHelper.Clamp(TransitionPosition, 0f, 1f);
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		public virtual void Draw(GameTime gameTime)
		{
		}

		public void ExitScreen()
		{
			if (TransitionOffTime == TimeSpan.Zero)
			{
				ScreenManager.RemoveScreen(this);
			}
			else
			{
				IsExiting = true;
			}
		}
	}
}

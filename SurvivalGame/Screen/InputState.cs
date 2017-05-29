using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;

namespace SurvivalGame
{
	public class InputState
	{
		public MouseState CurrentMouseState { get; set; }
		public KeyboardState CurrentKeyboardState { get; set; }
        public MouseState LastMouseState { get; set; }
        public KeyboardState LastKeyboardState { get; set; }

		public InputState()
		{
			this.CurrentKeyboardState = default(KeyboardState);
			this.CurrentMouseState = default(MouseState);
			this.LastKeyboardState = default(KeyboardState);
			this.LastMouseState = default(MouseState);
		}

		public void Update()
		{
			this.LastKeyboardState = this.CurrentKeyboardState;
			this.LastMouseState = this.CurrentMouseState;
			this.CurrentKeyboardState = Keyboard.GetState();
			this.CurrentMouseState = Mouse.GetState();
		}

		public bool GetKeyDown(Keys key)
		{
			return this.CurrentKeyboardState.IsKeyDown(key) && this.LastKeyboardState.IsKeyUp(key);
		}

		public bool GetKey(Keys key)
		{
			return this.CurrentKeyboardState.IsKeyDown(key);
		}

		public bool MouseLeft(ButtonState button)
		{
			return this.CurrentMouseState.LeftButton == button && this.LastMouseState.LeftButton != button;
		}

		public bool MouseRight(ButtonState button)
		{
			return this.CurrentMouseState.RightButton == button && this.LastMouseState.RightButton != button;
		}
	}
}

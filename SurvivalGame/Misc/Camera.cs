using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SurvivalGame
{
    public class Camera
    {
        public Vector2 Target { get; set; }
        public Viewport View { get; protected set; }
        public Matrix Transform { get; protected set; }
        public Matrix InverseTransform { get; protected set; }
        public Vector2 Position { get; protected set; }
        public Vector2 TopLeft { get; protected set; }
        public Vector2 ScreenCenter { get; protected set; }
        public Vector2 Origin { get; protected set; }
        public float Dampening { get; set; }
        public float Scale { get; set; }

        private Random Rand { get; set; }
        private float ScreenShakeDampening { get; set; }
        private float ScreenShakeSize { get; set; }

        public Camera(Viewport View, Vector2 Position, float Dampening, float Scale)
        {
            this.View = View;
            this.Target = Position;
            this.Position = Target;
            this.Dampening = Dampening;
            this.Scale = Scale;
            this.Rand = new Random();
            this.ScreenCenter = new Vector2(View.Width / 2, View.Height / 2);
		}

        public void Update(GameTime gameTime, InputState Input)
        {
            #region Camera control
            //Move
            if (Input.GetKey(Keys.A))
            {
                Target -= new Vector2(5, 0);
            }
            if (Input.GetKey(Keys.D))
            {
                Target += new Vector2(5, 0);
            }
            if (Input.GetKey(Keys.W))
            {
                Target -= new Vector2(0, 5);
            }
            if (Input.GetKey(Keys.S))
            {
                Target += new Vector2(0, 5);
            }

            //Zoom
            if (Input.CurrentMouseState.ScrollWheelValue > Input.LastMouseState.ScrollWheelValue)
            {
                Scale *= 1.2f;
            }
            if (Input.CurrentMouseState.ScrollWheelValue < Input.LastMouseState.ScrollWheelValue)
            {
                Scale /= 1.2f;
            }
            if (Scale < 0.2f)
            {
                Scale = 0.2f;
            }
            if (Scale > 5f)
            {
                Scale = 5f;
            }
            Scale = (float)Math.Round(Scale, 2);
            #endregion

            Vector2 ToCam = Target - Position;
            ToCam *= 1f - Dampening;
			Position += ToCam;

            //Screenshake
            float OffsetX = ((float)Rand.NextDouble() * (1 - (-1)) + -1) * ScreenShakeSize;
            float OffsetY = ((float)Rand.NextDouble() * (1 - (-1)) + -1) * ScreenShakeSize;
            Vector2 Offset = new Vector2(OffsetX, OffsetY);
            ScreenShakeSize *= 1f - ScreenShakeDampening;
            
            Origin = (ScreenCenter + Offset) / Scale;

            TopLeft = new Vector2(Position.X - View.Width / 2, Position.Y - View.Height / 2);
            
            Transform = Matrix.Identity *
                        Matrix.CreateTranslation(-(int)Position.X, -(int)Position.Y, 0) *
                        Matrix.CreateTranslation(Origin.X, Origin.Y, 0) *
                        Matrix.CreateScale(Scale);

            InverseTransform = Matrix.Invert(Transform);
        }

        public bool IsOnScreen(Rectangle rect)
        {
            //If the object is not within the horizontal bounds of the screen
            if ((rect.X + rect.Width) < (Position.X - Origin.X) || (rect.X) > (Position.X + Origin.X))
                return false;
          
            //If the object is not within the vertical bounds of the screen
            if ((rect.Y + rect.Height) < (Position.Y - Origin.Y) || (rect.Y) > (Position.Y + Origin.Y))
                return false;

            //In View
            return true;
        }

		public void ScreenShake(float Amount, float Dampening)
		{
			if (Amount > ScreenShakeSize)
			{
				this.ScreenShakeSize = Amount;
				this.ScreenShakeDampening = Dampening;
			}
		}

        public Vector2 ScreenToWorld(Vector2 Pos)
        {
            return Vector2.Transform(Pos, InverseTransform);
        }

        public Vector2 WorldToScreen(Vector2 Pos)
        {
            return Vector2.Transform(Pos, Transform);
        }
    }
}

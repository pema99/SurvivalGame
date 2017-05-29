using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SurvivalGame
{
    public class Particle
    {
        //Properties
        public Texture2D Sprite { get; set; } 
        public Vector2 Position { get; set; }        
        public Vector2 Velocity { get; set; } 
        public float Angle { get; set; } 
        public float AngularVelocity { get; set; } 
        public Color Color { get; set; }
        public Vector2 Scale { get; set; } 
        public double LifeTime { get; set; }
        
        //Cached values
        public Vector2 Origin { get; set; }
        
        //Object pool indicator
        public bool Alive { get; set; }

        public Particle(Texture2D Sprite, Vector2? Position, Vector2? Velocity,
            float Angle, float AngularVelocity, Color? Color, Vector2? Scale, double LifeTime)
        {
            Initialize(Sprite, Position, Velocity, Angle, AngularVelocity, Color, Scale, LifeTime);
        }

        public void Update(GameTime gameTime)
        {
            LifeTime -= gameTime.ElapsedGameTime.TotalSeconds;
            Position += Velocity;
            Angle += AngularVelocity;

            if (LifeTime <= 0)
            {
                Alive = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, Position, null, Color,
                Angle, Origin, Scale, SpriteEffects.None, 0f);
        }

        public void Initialize(Texture2D Sprite, Vector2? Position, Vector2? Velocity,
            float Angle, float AngularVelocity, Color? Color, Vector2? Scale, double LifeTime)
        {
            this.Sprite = Sprite;
            this.Position = Position.Value;
            this.Velocity = Velocity.Value;
            this.Angle = Angle;
            this.AngularVelocity = AngularVelocity;
            this.Color = Color.Value;
            this.Scale = Scale.Value;
            this.LifeTime = LifeTime;

            this.Origin = new Vector2((float)Sprite.Width / 2, (float)Sprite.Height / 2);
            
            this.Alive = true;
        }
    }
}

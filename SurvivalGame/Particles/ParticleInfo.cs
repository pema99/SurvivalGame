using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SurvivalGame
{
    public class ParticleInfo
    {
        public static Vector2 Randoms_MaxVelocity = new Vector2(1, 1);
        public static Vector2 Randoms_MinVelocity = new Vector2(-1, -1);
        public static float Randoms_MaxAngularVelocity = 0.1f;
        public static float Randoms_MinAngularVelocity = -0.1f;
        public static Vector2 Randoms_MaxScale = new Vector2(1, 1);
        public static Vector2 Randoms_MinScale = new Vector2(0.5f, 0.5f);
        public static float Randoms_MaxLifeTime = 5;
        public static float Randoms_MinLifeTime = 2;

        public Texture2D Sprite { get; set; }
        public Vector2? Position { get; set; }
        public Vector2? Velocity { get; set; }
        public float? Angle { get; set; }
        public float? AngularVelocity { get; set; }
        public Color? Color { get; set; }
        public Vector2? Scale { get; set; }
        public float? LifeTime { get; set; }

        public void InitializeMissingProperties(bool Random = false)
        {
            if (!Velocity.HasValue)
            {
                if (Random)
                {
                    Velocity =
                        new Vector2
                        (
                            MathHelper.Lerp(Randoms_MinVelocity.X, Randoms_MaxVelocity.X, (float)ParticleEngine.Rand.NextDouble()),
                            MathHelper.Lerp(Randoms_MinVelocity.Y, Randoms_MaxVelocity.Y, (float)ParticleEngine.Rand.NextDouble())
                        );
                }
                else
                {
                    Velocity = Vector2.Zero;
                }
            }
            if (!Angle.HasValue)
            {
                if (Random)
                {
                    Angle = ParticleEngine.Rand.Next(361);
                }
                else
                {
                    Angle = 0;
                }
            }
            if (!AngularVelocity.HasValue)
            {
                if (Random)
                {
                    AngularVelocity = MathHelper.Lerp(Randoms_MinAngularVelocity, Randoms_MaxAngularVelocity, (float)ParticleEngine.Rand.NextDouble());
                }
                else
                {
                    AngularVelocity = 0;
                }
            }
            if (!Color.HasValue)
            {
                if (Random)
                {
                    Color =
                        new Color
                        (
                            (float)ParticleEngine.Rand.NextDouble(),
                            (float)ParticleEngine.Rand.NextDouble(),
                            (float)ParticleEngine.Rand.NextDouble()
                        );
                }
                else
                {
                    Color = Microsoft.Xna.Framework.Color.White;
                }
            }
            if (!Scale.HasValue)
            {
                if (Random)
                {
                    Scale =
                        new Vector2
                        (
                            MathHelper.Lerp(Randoms_MinScale.X, Randoms_MaxScale.X, (float)ParticleEngine.Rand.NextDouble()),
                            MathHelper.Lerp(Randoms_MinScale.Y, Randoms_MaxScale.Y, (float)ParticleEngine.Rand.NextDouble())
                        );
                }
                else
                {
                    Scale = new Vector2(1, 1);
                }
            }
            if (!LifeTime.HasValue)
            {
                if (Random)
                {
                    LifeTime = MathHelper.Lerp(Randoms_MinLifeTime, Randoms_MaxLifeTime, (float)ParticleEngine.Rand.NextDouble());
                }
                else
                {
                    LifeTime = 0;
                }
            }
        }
    }
}

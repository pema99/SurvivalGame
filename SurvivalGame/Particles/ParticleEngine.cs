using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SurvivalGame
{
    public static class ParticleEngine
    {
        public static Random Rand { get; set; }
        public static Particle[] Particles { get; set; }

        static ParticleEngine()
        {
            ParticleEngine.Rand = new Random();
            ParticleEngine.Particles = new Particle[50000];
        }

        public static void Update(GameTime gameTime)
        {
            for (int i = 0; i < Particles.Length; i++)
            {
                if (Particles[i] == null ? false : Particles[i].Alive)
                {
                    Particles[i].Update(gameTime);
                }
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Particles.Length; i++)
            {
                if (Particles[i] == null ? false : Particles[i].Alive)
                {
                    Particles[i].Draw(spriteBatch);
                }
            }
        }

        public static Particle CreateParticle(ParticleInfo Info, bool Random = false)
        {
            Info.InitializeMissingProperties(Random);

            Particle Result = null;
            for (int i = 0; i < Particles.Length; i++)
            {
                if (Particles[i] == null)
                {
                    Particles[i] = new Particle(Info.Sprite, Info.Position, Info.Velocity, Info.Angle.Value, Info.AngularVelocity.Value, Info.Color, Info.Scale, Info.LifeTime.Value);
                    Result = Particles[i];
                    break;
                }
                if (!Particles[i].Alive)
                {
                    Result = Particles[i];
                    Result.Initialize(Info.Sprite, Info.Position, Info.Velocity, Info.Angle.Value, Info.AngularVelocity.Value, Info.Color, Info.Scale, Info.LifeTime.Value);
                    break;
                }
            }
            return Result;
        }
    }
}

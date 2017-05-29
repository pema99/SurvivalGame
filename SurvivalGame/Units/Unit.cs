using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurvivalGame
{
    public class Unit
    {
        private Map World { get; set; }

        public UnitType Type { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public int Health { get; set; }
        public UnitState State { get; set; }

        //Distance from previous tile to new one in current path
        public float DeltaDistance { get; set; }
        //Index of the tile we are currently going to in the path
        public int CurrentStep { get; set; }
        //The current path we are following
        public List<Step> Path { get; set; }
        //Where we previously were in the path
        public Vector2 PreviousStep { get; set; }
        //If we are already moving, this is the path we want to take next when we stop
        public Point? NextPathTarget { get; set; }

        public int TileX
        {
            get
            {
                return (int)(X / Globals.TileSize);
            }
            set
            {
                X = value * Globals.TileSize;
            }
        }
        public int TileY
        {
            get
            {
                return (int)(Y / Globals.TileSize);
            }
            set
            {
                Y = value * Globals.TileSize;
            }
        }

        public Unit(Map World, UnitType Type, float X, float Y)
        {
            this.Type = Type;
            this.X = X;
            this.Y = Y;
            this.Health = Type.MaxHealth;
            this.State = UnitState.Idle;

            this.World = World;
        }

        public virtual void Update(GameTime gameTime, InputState Input)
        {
            if (State == UnitState.Moving)
            {
                DeltaDistance += Type.Speed;
                if (DeltaDistance > 1)
                {
                    DeltaDistance = 1;
                }

                X = MathHelper.Lerp(PreviousStep.X * Globals.TileSize, Path[CurrentStep].X * Globals.TileSize, DeltaDistance);
                Y = MathHelper.Lerp(PreviousStep.Y * Globals.TileSize, Path[CurrentStep].Y * Globals.TileSize, DeltaDistance);

                if (DeltaDistance == 1)
                {
                    DeltaDistance = 0;
                    PreviousStep = new Vector2(Path[CurrentStep].X, Path[CurrentStep].Y);
                    CurrentStep++;

                    //We have reached a new tile, check if we want to change path
                    if (NextPathTarget != null)
                    {
                        //Change path
                        State = UnitState.Idle;
                        MoveTo(NextPathTarget.Value.X, NextPathTarget.Value.Y);

                        //Reset nexttarget
                        NextPathTarget = null;
                    }
                }

                if (CurrentStep == Path.Count)
                {
                    State = UnitState.Idle;
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Assets.Sprites[Type.Sprite], new Rectangle((int)X, (int)Y, Assets.Sprites[Type.Sprite].Width, Assets.Sprites[Type.Sprite].Height), Color.White);
        }

        public void MoveTo(int TarX, int TarY)
        {
            //TODO: add region check here

            //If already moving, move to a valid square first
            if (State == UnitState.Moving)
            {
                NextPathTarget = new Point(TarX, TarY);
            }
            //If not moving, just start new path
            else
            {
                List<Step> Path = Pathfinder.Pathfind(World, TileX, TileY, TarX, TarY);
                if (Path != null && Path.Count > 0)
                {
                    this.DeltaDistance = 0;
                    this.State = UnitState.Moving;
                    this.PreviousStep = new Vector2(X / Globals.TileSize, Y / Globals.TileSize);
                    this.CurrentStep = 0;
                    this.Path = Path;
                }
            }
        }
    }
}

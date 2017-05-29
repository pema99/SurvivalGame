using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurvivalGame
{
    public static class Pathfinder
    {
        public static List<Step> Pathfind(Map World, int X1, int Y1, int X2, int Y2)
        {
            List<Step> Open = new List<Step>();
            List<Step> Close = new List<Step>();

            //Declare destination and add first square to open
            Step Destination = new Step(X2, Y2, 0, 0);
            Open.Add(new Step(X1, Y1, Heuristic(X1, Y1, X2, Y2), 0));

            //Until we are out of neighbours to check or we have reached destination
            do
            {
                //Find lowest cost square
                Step Current = Open.OrderBy(p => p.Score).First();

                //Move it from open to closed
                Close.Add(Current);
                Open.Remove(Current);

                //If we have reached destination somehow, stop searching for path
                if (Close.Contains(Destination))
                {
                    break;
                }

                //Go through valid neighbours
                foreach (Step Neighbour in Adjacent(Current, World))
                {
                    //If neighbour is already part of the path, skip it
                    if (Close.Contains(Neighbour))
                    {
                        continue;
                    }
                    //If neighbour is not checked yet, add it to open
                    if (!Open.Contains(Neighbour))
                    {
                        Neighbour.Parent = Current;
                        Neighbour.G = Neighbour.Parent.G + 1;
                        Neighbour.H = Heuristic(Neighbour.X, Neighbour.Y, X2, Y2);
                        Open.Add(Neighbour);
                    }
                    //If neighbour was already checked, recalculate it with the new path, if it is cheaper
                    else
                    {
                        if ((Current.G + 1) < Neighbour.G)
                        {
                            Neighbour.G = Current.G + 1;
                        }
                    }
                }
            }
            while (Open.Count != 0);

            //No valid path found
            if (!Close.Contains(Destination))
            {
                return null;
            }

            //Go backwards through path from destination to start, and construct a list from that
            List<Step> Result = new List<Step>();
            CreatePath(Result, Close.Last());

            //In correct order
            Result.Reverse();

            //Without the initial position
            Result.RemoveAt(0);

            return Result;
        }

        //Recursive path walk
        private static List<Step> CreatePath(List<Step> Path, Step Current)
        {
            Path.Add(Current);
            if (Current.Parent != null)
            {
                CreatePath(Path, Current.Parent);
            }
            return Path;
        }

        //Get valid neighbour tiles
        private static List<Step> Adjacent(Step Current, Map World)
        {
            List<Step> Result = new List<Step>();
            if (IsValid(Current.X, Current.Y - 1, World))
            {
                if (World.Data[Current.X, Current.Y - 1].Walkable)
                {
                    Result.Add(new Step(Current.X, Current.Y - 1, 0, 0));
                }
            }
            if (IsValid(Current.X, Current.Y + 1, World))
            {
                if (World.Data[Current.X, Current.Y + 1].Walkable)
                {
                    Result.Add(new Step(Current.X, Current.Y + 1, 0, 0));
                }
            }
            if (IsValid(Current.X - 1, Current.Y, World))
            {
                if (World.Data[Current.X - 1, Current.Y].Walkable)
                {
                    Result.Add(new Step(Current.X - 1, Current.Y, 0, 0));
                }
            }
            if (IsValid(Current.X + 1, Current.Y, World))
            {
                if (World.Data[Current.X + 1, Current.Y].Walkable)
                {
                    Result.Add(new Step(Current.X + 1, Current.Y, 0, 0));
                }
            }
            return Result;
        }

        //Simple manhattan distance heuristic
        private static int Heuristic(int X1, int Y1, int X2, int Y2)
        {
            return Math.Abs(X2 - X1) + Math.Abs(Y2 - Y1);
        }

        //Checks if tile is within bounds
        private static bool IsValid(int X, int Y, Map World)
        {
            return X >= 0 && X < World.Data.GetLength(1)
                && Y >= 0 && Y < World.Data.GetLength(0);
        }
    }
}

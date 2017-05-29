using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurvivalGame
{
    public static class MapGenerator
    {
        private static PerlinGenerator PG = new PerlinGenerator();
        private static Random Rand = new Random();

        //int Octaves = 2, 
        //int Amplitude = 2, 
        //double Frequency = 0.045, 
        //double Persistence = 0.6

        //0.35, 0.45, 0.6, 0.7, 0.8, 0.9
        private static TileType PerlinToTileType(double Perlin, params double[] Intervals)
        {
            if (Perlin < Intervals[0]) return TileType.Water;
            else if (Perlin < Intervals[1]) return TileType.Beach;
            else if (Perlin < Intervals[2]) return TileType.Grass;
            else if (Perlin < Intervals[3]) return TileType.TallGrass;
            else if (Perlin < Intervals[4]) return TileType.Stone;
            else if (Perlin < Intervals[5]) return TileType.Mountain;
            else return TileType.Snow;
        }

        public static Map GenerateIslands(int MapWidth, int MapHeight, int Seed, int IslandSize)
        {
            PG.GeneratePerlin(Seed, 2, 2, 0.045, 0.6);
            Rand = new Random(Seed);
            Map Result = new Map(MapWidth, MapHeight);
            Point Center = new Point(MapWidth / 2 - 1, MapHeight / 2 - 1);
            for (int i = 0; i < MapWidth; i++)
            {
                for (int j = 0; j < MapHeight; j++)
                {
                    double DistanceX = (Center.X - i) * (Center.X - i); 
                    double DistanceY = (Center.Y - j) * (Center.Y - j);
                    double DistanceToCenter = Math.Sqrt(DistanceX + DistanceY);
                    DistanceToCenter /= IslandSize;
                    Result.Data[i, j] = new Tile(PerlinToTileType((PG.GetPerlin(i, j)+1)/2-DistanceToCenter, 0.35, 0.45, 0.6, 0.7, 0.8, 0.9));
                }
            }

            PG.GeneratePerlin(Int16.MaxValue - Seed, 2, 2, 0.045, 0.6);
            for (int i = 0; i < MapWidth; i++)
            {
                for (int j = 0; j < MapHeight; j++)
                {
                    double Perlin = (PG.GetPerlin(i, j) + 1) / 2;
                    if (Result.Data[i, j].Type == TileType.Grass)
                    {
                        if (Perlin > 0.5f)
                        {
                            Result.Data[i, j].Resource = ResourceType.Trees;
                        }
                    }
                }
            }
            return Result;
        }

        public static Map GenerateStandard(int MapWidth, int MapHeight, int Seed)
        {
            PG.GeneratePerlin(Seed, 2, 2, 0.045, 0.6);
            Rand = new Random(Seed);
            Map Result = new Map(MapWidth, MapHeight);
            for (int i = 0; i < MapWidth; i++)
            {
                for (int j = 0; j < MapHeight; j++)
                {
                    Result.Data[i, j] = new Tile(PerlinToTileType((PG.GetPerlin(i, j)+1)/2, 0.35, 0.45, 0.6, 0.7, 0.8, 0.9));
                }
            }

            PG.GeneratePerlin(Int16.MaxValue - Seed, 2, 2, 0.045, 0.6);
            for (int i = 0; i < MapWidth; i++)
            {
                for (int j = 0; j < MapHeight; j++)
                {
                    double Perlin = (PG.GetPerlin(i, j)+1)/2;
                    if (Result.Data[i, j].Type == TileType.Grass)
                    {
                        if (Perlin > 0.5f)
                        {
                            Result.Data[i, j].Resource = ResourceType.Trees;
                        }
                    }
                }
            }
            return Result;
        }
    }
}

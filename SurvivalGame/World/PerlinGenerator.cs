using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SurvivalGame
{
    public class PerlinGenerator
    {
        public int Seed { get; set; }
        public int Octaves { get; set; }
        public double Amplitude { get; set; }
        public double Persistence { get; set; }
        public double Frequency { get; set; }

        public void GeneratePerlin(int Seed, int Octaves = 6, int Amplitude = 12, double Frequency = 0.045, double Persistence = 0.3)
        {
            this.Seed = Seed;
            this.Octaves = Octaves;
            this.Amplitude = Amplitude;
            this.Frequency = Frequency;
            this.Persistence = Persistence;
        }

        public double GetPerlin(int X, int Y)
        {
            double Total = 0.0;
            double Freq = Frequency, Amp = Amplitude;
            for (int i = 0; i < Octaves; ++i)
            {
                Total = Total + SmoothNoise(X * Freq, Y * Freq) * Amp;
                Freq *= 2;
                Amp *= Persistence;
            }
            if (Total < -2.4) Total = -2.4;
            else if (Total > 2.4) Total = 2.4;

            return (Total / 2.4);
        }

        private double Noise(int X, int Y)
        {
            int n = X + Y * 57;
            n = (n << 13) ^ n;

            return (1.0 - ((n * (n * n * 15731 + 789221) + Seed) & 0x7fffffff) / 1073741824.0);
        }

        private double InterpolateNoise(double x, double y, double a)
        {
            double value = (1 - Math.Cos(a * Math.PI)) * 0.5;
            return x * (1 - value) + y * value;
        }

        private double SmoothNoise(double x, double y)
        {
            double n1 = Noise((int)x, (int)y);
            double n2 = Noise((int)x + 1, (int)y);
            double n3 = Noise((int)x, (int)y + 1);
            double n4 = Noise((int)x + 1, (int)y + 1);

            double i1 = InterpolateNoise(n1, n2, x - (int)x);
            double i2 = InterpolateNoise(n3, n4, x - (int)x);

            return InterpolateNoise(i1, i2, y - (int)y);
        }
    }
}

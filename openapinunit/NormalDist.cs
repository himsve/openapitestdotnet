using System;
using System.Collections.Generic;
using System.Text;

namespace openapinunit
{
    public static class NormalDist
    {
        private static Random _rand = new Random();

        public static double RandNormal(double mean, double stdDev)
        {
            _rand ??= new Random();

            double u1 = 1.0 - _rand.NextDouble();
            double u2 = 1.0 - _rand.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
            double randNormal = mean + stdDev * randStdNormal;

            return randNormal;
        }
    }
}

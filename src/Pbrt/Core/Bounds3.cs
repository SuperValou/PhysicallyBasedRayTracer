using System;
using System.Numerics;

namespace Pbrt.Core
{
    public class Bounds3
    {
        public Vector3 pMin;
        public Vector3 pMax;

        public Bounds3(Vector3 pMin, Vector3 pMax)
        {
            this.pMin = pMin;
            this.pMax = pMax;
        }

        public static Bounds3 Intersect(Bounds3 b1, Bounds3 b2)
        {
            Vector3 min = new Vector3(
                MathF.Max(b1.pMin.X, b2.pMin.X),
                MathF.Max(b1.pMin.Y, b2.pMin.Y),
                MathF.Max(b1.pMin.Z, b2.pMin.Z)
                );
            Vector3 max = new Vector3(
                MathF.Min(b1.pMax.X, b2.pMax.X),
                MathF.Min(b1.pMax.Y, b2.pMax.Y),
                MathF.Min(b1.pMax.Z, b2.pMax.Z)
                );
            return new Bounds3(min, max);
        }
    }
}

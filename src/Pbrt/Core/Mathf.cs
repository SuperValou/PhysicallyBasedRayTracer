using System;
using System.Runtime.CompilerServices;

namespace Pbrt.Core
{
    public static class Mathf
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DegToRad(float degrees)
        {
            return degrees * MathF.PI / 180;
        }

        /// <summary>
        /// Solves ax² + bx + c = 0. Returns true if some solution exist, false otherwise.
        /// </summary>        
        public static bool SolveQuadratic(float a, float b, float c, out float t0, out float t1)
        {
            if (a == 0)
            {
                throw new ArgumentException("'a' cannot be 0.", nameof(a));
            }

            // See https://www.pbr-book.org/3ed-2018/Utilities/Mathematical_Routines#SolvingQuadraticEquations
            double delta = 1d * b * b - 4d * a * c;
            if (delta < 0)
            {
                t0 = 0;
                t1 = 0;
                return false;
            }

            double deltaRoot = Math.Sqrt(delta);

            double q;
            if (b < 0)
            {
                q = -0.5d * (b - deltaRoot);
            }
            else
            {
                q = -0.5d * (b + deltaRoot);
            }

            t0 = (float) (q / a);
            if (q == 0)
            {
                t1 = t0;
            }
            else
            {
                t1 = (float)(c / q);
                if (t0 > t1)
                {
                    float temp = t0;
                    t0 = t1;
                    t1 = temp;
                }
            }

            return true;
        }
    }
}

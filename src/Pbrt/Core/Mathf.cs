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
    }
}

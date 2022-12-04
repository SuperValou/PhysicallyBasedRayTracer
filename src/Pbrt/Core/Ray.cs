using System;
using System.Numerics;

namespace Pbrt.Core
{    
    public class Ray
    {
        public Vector3 Origin;

        public Vector3 Direction;

        public float MaxRange { get; set; }

        public Ray()
        {
            MaxRange = float.MaxValue;            
        }

        public Ray(Vector3 origin, Vector3 direction, float maxRange = float.MaxValue)
        {
            this.Origin = origin;
            this.Direction = direction;
            this.MaxRange = maxRange;
        }

        /// <summary>
        /// Ray(t) = Origin + t * Direction
        /// </summary>
        public Vector3 PointAt(float t)
        {
            return Origin + t * Direction;
        }
    }
}
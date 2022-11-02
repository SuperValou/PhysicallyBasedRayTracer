using System.Numerics;

namespace Pbrt.Core
{
    public class Ray
    {
        public Vector3 Origin;

        public Vector3 Direction;

        /// <summary>
        /// Ray(Time) = Origin + Time * Direction
        /// </summary>
        public float Time { get; set; }

        /// <summary>
        /// Max Time value the ray can use, i.e. how far the ray goes
        /// </summary>
        public float TMax { get; set; }

        public Ray()
        {
            TMax = float.MaxValue;
            Time = 0;            
        }

        public Ray(Vector3 origin, Vector3 direction, float tMax = float.MaxValue, float time = 0f)
        {
            this.Origin = origin;
            this.Direction = direction;
            this.Time = time;
            this.TMax = tMax;            
        }
    }
}
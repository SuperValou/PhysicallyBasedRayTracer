using System;

namespace Pbrt.Core
{
    public class Primitive
    {
        public Bounds WorldBounds { get; }

        public bool Intersect(Ray ray, out SurfaceInteraction isect)
        {
            throw new NotImplementedException();
        }

        // TODO: can probably be renamed
        public bool IntersectP(Ray ray)
        {
            throw new NotImplementedException();
        }
    }
}
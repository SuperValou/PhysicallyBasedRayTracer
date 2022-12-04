using System;
using Pbrt.Core;
using Pbrt.Lights;

namespace Pbrt.Primitives
{
    public abstract class Aggregate : IPrimitive
    {
        public abstract Bounds3 GetWorldBounds();

        public abstract bool Intersect(Ray ray, out SurfaceInteraction isect);

        public abstract bool IntersectP(Ray ray);

        public void ComputeScatteringFunctions(SurfaceInteraction isect, TransportMode mode, bool allowMultipleLobes)
        {
            throw new InvalidOperationException($"This method is not supposed to be called.");
        }

        public Material GetMaterial()
        {
            throw new InvalidOperationException($"This method is not supposed to be called.");
        }

        public bool TryGetAreaLight(out AreaLight light)
        {
            throw new InvalidOperationException($"This method is not supposed to be called.");
        }
    }
}

using System;
using System.Collections.Generic;
using Pbrt.Core;
using Pbrt.Primitives;

namespace Pbrt.Accelerators
{

    public class KdTree : Aggregate
    {
        public override Bounds3 GetWorldBounds()
        {
            throw new NotImplementedException();
        }

        public override bool Intersect(Ray ray, out SurfaceInteraction isect)
        {
            throw new NotImplementedException();
        }

        public override bool IntersectP(Ray ray)
        {
            throw new NotImplementedException();
        }
    }
}

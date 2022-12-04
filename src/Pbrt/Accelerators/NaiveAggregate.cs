using System;
using System.Collections.Generic;
using Pbrt.Core;
using Pbrt.Primitives;

namespace Pbrt.Accelerators
{
    /// <summary>
    /// Inefficient class acting as a stub for KdTrees/BVHs
    /// </summary>
    public class NaiveAggregate : Aggregate
    {
        private readonly List<IPrimitive> _primitives;
        private Bounds3 _worldBounds;

        public NaiveAggregate(IEnumerable<IPrimitive> primitives)
        {
            _primitives = new List<IPrimitive>(primitives);
            _worldBounds = _primitives[0].GetWorldBounds();
            foreach (var p in _primitives)
            {
                _worldBounds = Bounds3.Union(_worldBounds, p.GetWorldBounds());
            }
        }

        public override Bounds3 GetWorldBounds()
        {
            return _worldBounds;
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

using System.Collections.Generic;

namespace Pbrt.Core
{
    public class Scene
    {
        private IPrimitive _aggregate;

        public ICollection<Light> Lights { get; }

        public Bounds3 WorldBounds { get; }

        public Scene(IPrimitive aggregate, ICollection<Light> lights)
        {
            _aggregate = aggregate;
            WorldBounds = aggregate.GetWorldBounds();

            Lights = lights;

            // TODO: probably preprocess in a separate method
            foreach (var light in lights)
            {
                light.Preprocess(scene: this);
            }
        }

        public bool Intersect(Ray ray, out SurfaceInteraction isect)
        {
            return _aggregate.Intersect(ray, out isect);
        }

        // TODO: can probably be renamed
        public bool IntersectP(Ray ray)
        {
            return _aggregate.IntersectP(ray);
        }
    }
}

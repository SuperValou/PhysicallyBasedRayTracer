using Pbrt.Core;
using Pbrt.Lights;

namespace Pbrt.Primitives
{
    public class GeometricPrimitive : IPrimitive
    {        
        private readonly Material _material;
        private readonly AreaLight _areaLight;

        public Shape Shape { get; }

        public GeometricPrimitive(Shape shape, Material material, AreaLight areaLight = null)
        {
            Shape = shape;
            _material = material;
            _areaLight = areaLight;
        }

        public Bounds3 GetWorldBounds()
        {
            return Shape.WorldBounds();
        }

        public bool TryGetAreaLight(out AreaLight areaLight)
        {
            areaLight = _areaLight;
            return _areaLight != null;
        }

        public Material GetMaterial()
        {
            return _material;
        }

        public void ComputeScatteringFunctions(SurfaceInteraction isect, TransportMode mode, bool allowMultipleLobes)
        {
            throw new System.NotImplementedException("Should return property");
        }

        public bool Intersect(Ray ray, out SurfaceInteraction isect)
        {
            bool success = this.Shape.Intersect(ray, out float hit, out isect);
            if (!success)
            {
                return false;
            }

            ray.MaxRange = hit; // TODO: 'feels weird to use MaxRange to store the intersection distance
            isect.Primitive = this;
            return true;
        }

        public bool IntersectP(Ray ray)
        {
            return Shape.IntersectP(ray);
        }
    }
}

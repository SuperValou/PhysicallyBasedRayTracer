using Pbrt.Lights;

namespace Pbrt.Core
{
    /// <summary>
    /// Encapsulates geometry and additional nongeometric information such as material properties
    /// </summary>
    public interface IPrimitive
    {
        // TODO: https://www.pbr-book.org/3ed-2018/Primitives_and_Intersection_Acceleration/Primitive_Interface_and_Geometric_Primitives#
        Bounds3 GetWorldBounds();

        /// <summary>
        /// Returns the material instance assigned to the primitive
        /// </summary>
        /// <returns></returns>
        Material GetMaterial();

        /// <summary>
        /// Get an AreaLight describings the primitive’s light emission. 
        /// If the primitive isn't a light source, returns false.
        /// </summary>
        bool TryGetAreaLight(out AreaLight light);

        bool Intersect(Ray ray, out SurfaceInteraction isect);

        // TODO: can probably be renamed
        bool IntersectP(Ray ray);

        void ComputeScatteringFunctions(SurfaceInteraction isect, TransportMode mode, bool allowMultipleLobes);
    }
}
using System;
using System.Numerics;
using Pbrt.Core;

namespace Pbrt.Shapes
{
    public class Sphere : Shape
    {
        private Bounds3 _bounds;

        public float Radius { get; }

        public Sphere(Transform transform, float radius, bool reverseOrientation=false) 
            : base(transform, reverseOrientation)
        {
            Radius = radius;
        }

        public override float Area()
        {            
            return 4 * MathF.PI * Radius * Radius;
        }

        public override bool Intersect(Ray ray, out float tHit, out SurfaceInteraction isect)
        {
            // Doc: https://www.pbr-book.org/3ed-2018/Shapes/Spheres#IntersectionTests

            tHit = 0;
            isect = null;

            // Transform ray to sphere space
            ray = this.Transform.TransformRay(ray);

            // Solve intersection
            /*
             * Starting from the implicit equation of a sphere
             * x² + y² + z² - r² = 0
             * 
             * we replace terms with the parametric equation of a ray
             * (orig.x + t * dir.x)² + (orig.y + t * dir.y)² + (orig.z + t * dir.z)² - r² = 0
             * 
             * then we develop
             * orig.x² + t² * dir.x² + 2 * orig.x * t * dir.x
             * + orig.y² + t² * dir.y² + 2 * orig.y * t * dir.y
             * + orig.z² + t² * dir.z² + 2 * orig.z * t * dir.z - r² = 0
             * 
             * and rearrange terms
             * (dir.x² + dir.y² + dir.z²) * t²
             * + 2 * (orig.x * dir.x + orig.y * dir.y + orig.z * dir.z) * t
             * + orig.x² + orig.y² + orig.z² - r² = 0
             * 
             * so now we have to solve something like
             * a t² + b t + c = 0
             * 
             * where
             * a = dir.x² + dir.y² + dir.z²
             * b = 2 * (orig.x * dir.x + orig.y * dir.y + orig.z * dir.z)
             * c = orig.x² + orig.y² + orig.z² - r²
             * 
             * */

            float a = ray.Direction.X * ray.Direction.X + ray.Direction.Y * ray.Direction.Y + ray.Direction.Z * ray.Direction.Z;
            float b = 2 * (ray.Direction.X * ray.Origin.X + ray.Direction.Y * ray.Origin.Y + ray.Direction.Z * ray.Origin.Z);
            float c = ray.Origin.X * ray.Origin.X + ray.Origin.Y * ray.Origin.Y + ray.Origin.Z * ray.Origin.Z - Radius * Radius;

            if (!Mathf.SolveQuadratic(a, b, c, out float t0, out float t1))
            {                
                return false;
            }

            if (t0 > ray.MaxRange || t1 <= 0)
            {
                // intersections are clearly outside of the ray range
                return false;
            }

            tHit = t0;
            if (tHit <= 0)
            {
                tHit = t1;
                if (tHit > ray.MaxRange)
                {
                    // intersections are both outside of the ray range
                    return false;
                }                    
            }

            // Compute hit position
            Vector3 hitPoint = ray.PointAt(tHit);
            hitPoint *= Radius / hitPoint.Length(); // refine intersection point

            if (hitPoint.X == 0 && hitPoint.Y == 0)
            {
                hitPoint.X = 1e-5f * Radius;
            }

            // Compute parametric hit position
            float theta = MathF.Acos(Math.Clamp(hitPoint.Z / Radius, -1, 1));
            float phi = MathF.Atan2(hitPoint.Y, hitPoint.X);
            if (phi < 0)
            {
                phi += 2 * MathF.PI;
            }
            
            float u = phi / (2 * MathF.PI);            
            float v = theta / MathF.PI;

            // Compute dpdu & dpdv
            float zRadius = MathF.Sqrt(hitPoint.X * hitPoint.X + hitPoint.Y * hitPoint.Y);
            float invZRadius = 1 / zRadius;
            float cosPhi = hitPoint.X * invZRadius;
            float sinPhi = hitPoint.Y * invZRadius;

            Vector3 dpdu = new Vector3(-2 * MathF.PI * hitPoint.Y, 2 * MathF.PI * hitPoint.X, 0);
            Vector3 dpdv = MathF.PI * new Vector3(hitPoint.Z * cosPhi, hitPoint.Z * sinPhi, -Radius * MathF.Sin(theta));

            
            isect = new SurfaceInteraction(hitPoint, new Vector2(u, v), - ray.Direction, dpdu, dpdv, this);
            isect = this.InverseTransform.TransformSurfaceInteraction(isect);
            return true;
        }

        public override Bounds3 LocalBounds()
        {
            if (_bounds == null)
            {
                _bounds = new Bounds3(-Radius * Vector3.One, Radius * Vector3.One);
            }

            return _bounds;
        }
    }
}

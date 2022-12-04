using System;
using System.Numerics;

namespace Pbrt.Core
{
    public class Bounds2
    {
        public Vector2 MinPoint { get; }
        public Vector2 MaxPoint { get; }

        public Bounds2(Vector2 minPoint, Vector2 maxPoint)
        {
            this.MinPoint = Vector2.Min(minPoint, maxPoint);
            this.MaxPoint = Vector2.Max(minPoint, maxPoint);
        }

        public bool IsNonEmpty()
        {
            return MinPoint.X < MaxPoint.X
                && MinPoint.Y < MaxPoint.Y;
        }

        public Vector2 Diagonal()
        {
            throw new NotImplementedException();
        }

        public static Bounds2 Intersect(Bounds2 b1, Bounds2 b2)
        {
            Vector2 min = new Vector2(
                MathF.Max(b1.MinPoint.X, b2.MinPoint.X),
                MathF.Max(b1.MinPoint.Y, b2.MinPoint.Y)
                );
            Vector2 max = new Vector2(
                MathF.Min(b1.MaxPoint.X, b2.MaxPoint.X),
                MathF.Min(b1.MaxPoint.Y, b2.MaxPoint.Y)
                );
            return new Bounds2(min, max);
        }
    }

    public class Bounds3
    {
        public Vector3 MinPoint { get; }
        public Vector3 MaxPoint { get; }

        public Bounds3(Vector3 minPoint, Vector3 maxPoint)
        {
            this.MinPoint = Vector3.Min(minPoint, maxPoint);
            this.MaxPoint = Vector3.Max(minPoint, maxPoint);
        }

        public bool IsNonEmpty()
        {
            return MinPoint.X < MaxPoint.X
                && MinPoint.Y < MaxPoint.Y
                && MinPoint.Z < MaxPoint.Z;
        }

        // TODO: get a better understanding and refacto copy-paste, see https://www.pbr-book.org/3ed-2018/Shapes/Basic_Shape_Interface#RayndashBoundsIntersections
        public bool IntersectP(Ray ray, out float hitt0, float hitt1)
        {
            hitt0 = 0;
            hitt1 = ray.MaxRange;

            // X

            float invRayDir = 1 / ray.Direction.X;
            float tNear = (MinPoint.X - ray.Origin.X) * invRayDir;
            float tFar = (MaxPoint.X - ray.Origin.X) * invRayDir;

            if (tNear > tFar)
            {
                // swap values
                var temp = tFar;
                tFar = tNear;
                tNear = temp;
            }

            hitt0 = tNear > hitt0 ? tNear : hitt0;
            hitt1 = tFar < hitt1 ? tFar : hitt1;
            if (hitt0 > hitt1)
            {
                return false;
            }

            // Y

            invRayDir = 1 / ray.Direction.Y;
            tNear = (MinPoint.Y - ray.Origin.Y) * invRayDir;
            tFar = (MaxPoint.Y - ray.Origin.Y) * invRayDir;

            if (tNear > tFar)
            {
                // swap values
                var temp = tFar;
                tFar = tNear;
                tNear = temp;
            }

            hitt0 = tNear > hitt0 ? tNear : hitt0;
            hitt1 = tFar < hitt1 ? tFar : hitt1;
            if (hitt0 > hitt1)
            {
                return false;
            }

            // Z

            invRayDir = 1 / ray.Direction.Z;
            tNear = (MinPoint.Z - ray.Origin.Z) * invRayDir;
            tFar = (MaxPoint.Z - ray.Origin.Z) * invRayDir;

            if (tNear > tFar)
            {
                // swap values
                var temp = tFar;
                tFar = tNear;
                tNear = temp;
            }

            hitt0 = tNear > hitt0 ? tNear : hitt0;
            hitt1 = tFar < hitt1 ? tFar : hitt1;
            if (hitt0 > hitt1)
            {
                return false;
            }


            return true;
        }

        /// <summary>
        /// Returns a new bounding box encompassing the given point and box
        /// </summary>
        public static Bounds3 Union(Bounds3 box, Vector3 point)
        {
            Vector3 min = Vector3.Min(box.MinPoint, point);
            Vector3 max = Vector3.Max(box.MaxPoint, point);
            return new Bounds3(min, max);            
        }

        public static Bounds3 Union(Bounds3 b1, Bounds3 b2)
        {
            throw new NotImplementedException();
        }

        public static Bounds3 Intersection(Bounds3 b1, Bounds3 b2)
        {
            Vector3 min = new Vector3(
                MathF.Max(b1.MinPoint.X, b2.MinPoint.X),
                MathF.Max(b1.MinPoint.Y, b2.MinPoint.Y),
                MathF.Max(b1.MinPoint.Z, b2.MinPoint.Z)
                );
            Vector3 max = new Vector3(
                MathF.Min(b1.MaxPoint.X, b2.MaxPoint.X),
                MathF.Min(b1.MaxPoint.Y, b2.MaxPoint.Y),
                MathF.Min(b1.MaxPoint.Z, b2.MaxPoint.Z)
                );
            return new Bounds3(min, max);
        }
    }
}

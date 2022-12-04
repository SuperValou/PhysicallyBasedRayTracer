using System.Numerics;

namespace Pbrt.Core
{
    /// <summary>
    /// Raw geometric properties of a primitive, such as its surface area and bounding box, and handles ray intersection
    /// </summary>
    public abstract class Shape
    {
        private Bounds3 _worldBounds;

        /// <summary>
        /// Object-to-world transform.
        /// </summary>
        protected Transform Transform { get; }

        /// <summary>
        /// World-to-object transform.
        /// </summary>
        protected Transform InverseTransform { get; }

        /// <summary>
        /// False by default, meaning for closed shapes, normals point to the outside of the shape. 
        /// If true, normals are flipped and point to the inside of the shape.
        /// </summary>
        public bool ReverseOrientation { get; }

        /// <summary>
        /// True if the Shape’s transformation matrix is switching the handedness of the object coordinate system
        /// (like a scale of (1, 1, -1) for example).
        /// </summary>
        public bool TransformSwapsHandedness { get; }

        protected Shape(Transform transform, bool reverseOrientation) 
            : this(transform, new Transform(transform.WorldToLocalMatrix), reverseOrientation)
        {            
        }

        protected Shape(Transform transform, Transform inverseTransform, bool reverseOrientation)
        {
            Transform = transform;
            InverseTransform = inverseTransform;
            ReverseOrientation = reverseOrientation;
            TransformSwapsHandedness = transform.SwapsHandedness();
        }

        public abstract Bounds3 LocalBounds();

        public Bounds3 WorldBounds()
        {
            if (_worldBounds == null)
            {
                _worldBounds = ComputeWorldBounds();
            }

            return _worldBounds;
        }

        public abstract float Area();

        /// <param name="tHit">Closest intersection distance along the ray.</param>                
        public abstract bool Intersect(Ray ray, out float tHit, out SurfaceInteraction isect);

        public virtual bool IntersectP(Ray ray)
        {
            return Intersect(ray, out float _, out SurfaceInteraction _1);
        }

        protected virtual Bounds3 ComputeWorldBounds()
        {
            var b = this.LocalBounds();

            // The easiest way to transform an axis-aligned bounding box 
            // is to transform all eight of its corner vertices 
            // then compute a new bounding box that encompasses those points

            Vector3 ooo = this.Transform.TransformPoint(new Vector3(b.MinPoint.X, b.MinPoint.Y, b.MinPoint.Z));
            Vector3 oox = this.Transform.TransformPoint(new Vector3(b.MinPoint.X, b.MinPoint.Y, b.MaxPoint.Z));
            Vector3 oxo = this.Transform.TransformPoint(new Vector3(b.MinPoint.X, b.MaxPoint.Y, b.MinPoint.Z));
            Vector3 oxx = this.Transform.TransformPoint(new Vector3(b.MinPoint.X, b.MaxPoint.Y, b.MaxPoint.Z));
            Vector3 xoo = this.Transform.TransformPoint(new Vector3(b.MaxPoint.X, b.MinPoint.Y, b.MinPoint.Z));
            Vector3 xox = this.Transform.TransformPoint(new Vector3(b.MaxPoint.X, b.MinPoint.Y, b.MaxPoint.Z));
            Vector3 xxo = this.Transform.TransformPoint(new Vector3(b.MaxPoint.X, b.MaxPoint.Y, b.MinPoint.Z));
            Vector3 xxx = this.Transform.TransformPoint(new Vector3(b.MaxPoint.X, b.MaxPoint.Y, b.MaxPoint.Z));

            Bounds3 result = new Bounds3(ooo, oox);
            result = Bounds3.Union(result, oxo);
            result = Bounds3.Union(result, oxx);
            result = Bounds3.Union(result, xoo);
            result = Bounds3.Union(result, xox);
            result = Bounds3.Union(result, xxo);
            result = Bounds3.Union(result, xxx);
            return result;
        }
    }
}
using System;
using System.Diagnostics;
using System.Numerics;

namespace Pbrt.Core
{
    // https://www.pbr-book.org/3ed-2018/Geometry_and_Transformations/Transformations
    public class Transform
    {
        public Matrix4x4 LocalToWorldMatrix { get; }
        public Matrix4x4 WorldToLocalMatrix { get; }

        public static Transform Identity { get; } = new Transform(Matrix4x4.Identity);

        public Transform(Matrix4x4 matrix)
        {
            LocalToWorldMatrix = matrix;            
            bool success = Matrix4x4.Invert(matrix, out Matrix4x4 invert);
            Debug.Assert(success);
            WorldToLocalMatrix = invert;
        }

        public Transform(Matrix4x4 matrix, Matrix4x4 inverseMatrix)
        {
            this.LocalToWorldMatrix = matrix;
            this.WorldToLocalMatrix = inverseMatrix;
        }

        public bool SwapsHandedness()
        {
            // See: https://www.pbr-book.org/3ed-2018/Geometry_and_Transformations/Applying_Transformations#TransformationsandCoordinateSystemHandedness
            float submatrixDeterminant = LocalToWorldMatrix.M11 * (LocalToWorldMatrix.M22 * LocalToWorldMatrix.M33 - LocalToWorldMatrix.M32 * LocalToWorldMatrix.M23)
                                       - LocalToWorldMatrix.M21 * (LocalToWorldMatrix.M12 * LocalToWorldMatrix.M33 - LocalToWorldMatrix.M32 * LocalToWorldMatrix.M13)
                                       + LocalToWorldMatrix.M31 * (LocalToWorldMatrix.M12 * LocalToWorldMatrix.M23 - LocalToWorldMatrix.M22 * LocalToWorldMatrix.M13);
            return submatrixDeterminant < 0;
        }

        /// <summary>
        /// Transforms position from local space to world space.
        /// </summary>
        public Vector3 TransformPoint(Vector3 localPosition)
        {
            return Vector3.Transform(localPosition, LocalToWorldMatrix);
        }

        /// <summary>
        /// Transforms position from world space to local space.
        /// </summary>
        public Vector3 InverseTransformPoint(Vector3 worldPosition)
        {
            return Vector3.Transform(worldPosition, WorldToLocalMatrix);
        }

        /// <summary>
        /// Transforms vector from local space to world space.
        /// </summary>
        public Vector3 TransformVector(Vector3 localVector)
        {
            return Vector3.TransformNormal(localVector, LocalToWorldMatrix);
        }

        /// <summary>
        /// Transforms vector from world space to local space.
        /// </summary>
        public Vector3 InverseTransformVector(Vector3 worldVector)
        {
            return Vector3.TransformNormal(worldVector, WorldToLocalMatrix);
        }

        /// <summary>
        /// Transforms normal from local space to world space.
        /// </summary>
        public Vector3 TransformNormal(Vector3 localNormal)
        {
            float x = localNormal.X * WorldToLocalMatrix.M11 + localNormal.Y * WorldToLocalMatrix.M12 + localNormal.Z * WorldToLocalMatrix.M13;
            float y = localNormal.X * WorldToLocalMatrix.M21 + localNormal.Y * WorldToLocalMatrix.M22 + localNormal.Z * WorldToLocalMatrix.M23;
            float z = localNormal.X * WorldToLocalMatrix.M31 + localNormal.Y * WorldToLocalMatrix.M32 + localNormal.Z * WorldToLocalMatrix.M33;
            
            return new Vector3(x, y, z);
        }

        /// <summary>
        /// Transforms ray from local space to world space.
        /// </summary>
        public Ray TransformRay(Ray localRay)
        {
            Debug.Assert(localRay != null);
            var origin = this.TransformPoint(localRay.Origin);
            var direction = this.TransformVector(localRay.Direction);
            return new Ray(origin, direction, localRay.MaxRange);
        }

        /// <summary>
        /// Transforms surface interaction from local space to world space.
        /// </summary>
        public SurfaceInteraction TransformSurfaceInteraction(SurfaceInteraction surfaceInteraction)
        {
            // See https://pbr-book.org/3ed-2018/Geometry_and_Transformations/Interactions#SurfaceInteraction

            Vector3 point = this.TransformPoint(surfaceInteraction.Point);            
            Vector3 direction = Vector3.Normalize(this.TransformVector(surfaceInteraction.Direction));
            Vector2 uvs = surfaceInteraction.UV;            
            Vector3 geoDpdu = this.TransformVector(surfaceInteraction.LocalGeometry.DpDu);
            Vector3 geoDpdv = this.TransformVector(surfaceInteraction.LocalGeometry.DpDv);
            Shape shape = surfaceInteraction.Shape;
            
            SurfaceInteraction result = new SurfaceInteraction(point, uvs, direction, geoDpdu, geoDpdv, shape);

            Vector3 shadingDpdu = this.TransformVector(surfaceInteraction.ShadingGeometry.DpDu);
            Vector3 shadingDpdv = this.TransformVector(surfaceInteraction.ShadingGeometry.DpDv);
            result.SetShadingGeometry(shadingDpdu, shadingDpdv);

            result.Primitive = surfaceInteraction.Primitive;
            result.Bsdf = surfaceInteraction.Bsdf;
            result.Bssrdf = surfaceInteraction.Bssrdf;

            return result;
        }

        public static Transform FromTranslation(float deltaX, float deltaY, float deltaZ)
        {
            return FromTranslation(new Vector3(deltaX, deltaY, deltaZ));
        }

        public static Transform FromTranslation(Vector3 delta)
        {
            Matrix4x4 matrix = Matrix4x4.CreateTranslation(delta);
            return new Transform(matrix);
        }

        public static Transform FromScaling(float xScale, float yScale, float zScale)
        {
            return FromScaling(new Vector3(xScale, yScale, zScale));
        }

        public static Transform FromScaling(Vector3 scale)
        {
            return new Transform(Matrix4x4.CreateScale(scale));
        }

        /// <summary>
        /// Creates a clockwise rotation around the X-axis.
        /// </summary>
        public static Transform FromRotationX(float degrees)
        {
            float radians = Mathf.DegToRad(degrees);
            Matrix4x4 matrix = Matrix4x4.CreateRotationX(radians);
            return new Transform(matrix);
        }

        /// <summary>
        /// Creates a clockwise rotation around the Y-axis.
        /// </summary>
        public static Transform FromRotationY(float degrees)
        {
            float radians = Mathf.DegToRad(degrees);
            Matrix4x4 matrix = Matrix4x4.CreateRotationY(radians);
            return new Transform(matrix);
        }

        /// <summary>
        /// Creates a clockwise rotation around the Z-axis.
        /// </summary>
        public static Transform FromRotationZ(float degrees)
        {
            float radians = Mathf.DegToRad(degrees);
            Matrix4x4 matrix = Matrix4x4.CreateRotationZ(radians);
            return new Transform(matrix);
        }

        /// <summary>
        /// Creates a clockwise rotation around the given axis.
        /// </summary>
        public static Transform FromRotationAroundAxis(float degrees, Vector3 axis)
        {
            float radians = Mathf.DegToRad(degrees);
            Matrix4x4 matrix = Matrix4x4.CreateFromAxisAngle(axis, radians);
            return new Transform(matrix);
        }

        // https://www.pbr-book.org/3ed-2018/Geometry_and_Transformations/Transformations#TheLook-AtTransformation
        public static Transform FromLookAt(Vector3 cameraPosition, Vector3 target, Vector3 cameraUp)
        {            
            Matrix4x4 lookAtMatrix = Matrix4x4.CreateLookAt(cameraPosition, target, cameraUp);

            // System.Numerics assumes right-handed coordinates system
            var handednessSwapping = Matrix4x4.CreateScale(new Vector3(-1, 1, 1));
            var matrix = handednessSwapping * lookAtMatrix;
            Transform transform = new Transform(matrix);
            return transform;
        }

        /// <summary>
        /// Create a transform with the following properties: 
        /// Z is 0 at the near plane and 1 at the far plane; 
        /// X and Y lie between -1 and 1 in the direction in which the image is narrower, and the wider direction maps to a proportionally larger range. 
        /// (0,0,0) is the center of the near plane.
        /// </summary>
        /// <param name="fov">Field of view in degrees.</param>                        
        public static Transform FromPerspective(float fov, float aspectRatio, float nearPlaneDistance, float farPlaneDistance)
        {
            if (fov <= 0.0f || fov >= 180)
            {
                throw new ArgumentOutOfRangeException(nameof(fov));
            }

            if (aspectRatio <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(aspectRatio));
            }

            if (nearPlaneDistance <= 0.0f)
            {
                throw new ArgumentOutOfRangeException(nameof(nearPlaneDistance));
            }

            if (farPlaneDistance <= 0.0f)
            {
                throw new ArgumentOutOfRangeException(nameof(farPlaneDistance));
            }

            if (nearPlaneDistance >= farPlaneDistance)
            {
                throw new ArgumentOutOfRangeException(nameof(nearPlaneDistance));
            }

            // perspective transformation
            float m33 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
            float m43 = -farPlaneDistance * nearPlaneDistance / (farPlaneDistance - nearPlaneDistance);
            Matrix4x4 perspMatrix = new Matrix4x4(
                m11: 1, m21: 0, m31: 0, m41: 0,
                m12: 0, m22: 1, m32: 0, m42: 0,
                m13: 0, m23: 0, m33: m33, m43: m43,
                m14: 0, m24: 0, m34: 1, m44: 0);

            // fov transformation
            float yFovScale = 1f / MathF.Tan(Mathf.DegToRad(fov) * 0.5f);
            float xFovScale = yFovScale / aspectRatio;
            Matrix4x4 fovMatrix = Matrix4x4.CreateScale(new Vector3(xFovScale, yFovScale, 1));

            Transform t = new Transform(fovMatrix * perspMatrix);
            return t;
        }

        public static Transform Inverse(Transform transform)
        {
            Debug.Assert(transform != null);
            return new Transform(transform.WorldToLocalMatrix, transform.LocalToWorldMatrix);
        }

        public static Transform operator *(Transform t1, Transform t2)
        {
            Matrix4x4 matrix = t2.LocalToWorldMatrix * t1.LocalToWorldMatrix;
            Matrix4x4 invMatrix = t2.WorldToLocalMatrix * t1.WorldToLocalMatrix;
            return new Transform(matrix, invMatrix);            
        }
    }
}

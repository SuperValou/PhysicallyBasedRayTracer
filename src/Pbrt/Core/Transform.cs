using System;
using System.Diagnostics;
using System.Numerics;

namespace Pbrt.Core
{
    public class Transform
    {
        public Matrix4x4 Matrix { get; }
        public Matrix4x4 InverseMatrix { get; }

        public Transform Identity { get; } = new Transform(Matrix4x4.Identity);

        public Transform(Matrix4x4 matrix)
        {
            Matrix = matrix;            
            bool success = Matrix4x4.Invert(matrix, out Matrix4x4 invert);
            Debug.Assert(success);
            InverseMatrix = invert;
        }

        public Transform(Matrix4x4 matrix, Matrix4x4 invMatrix)
        {
            Matrix = matrix;
            InverseMatrix = invMatrix;
        }

        public bool SwapsHandedness()
        {
            throw new NotImplementedException("See: https://www.pbr-book.org/3ed-2018/Geometry_and_Transformations/Applying_Transformations#TransformationsandCoordinateSystemHandedness");
        }

        public Vector3 ApplyToPoint(Vector3 position)
        {
            float x = position.X * Matrix.M11 + position.Y * Matrix.M21 + position.Z * Matrix.M31 + Matrix.M41;
            float y = position.X * Matrix.M12 + position.Y * Matrix.M22 + position.Z * Matrix.M32 + Matrix.M42;
            float z = position.X * Matrix.M13 + position.Y * Matrix.M23 + position.Z * Matrix.M33 + Matrix.M43;
            float w = position.X * Matrix.M14 + position.Y * Matrix.M24 + position.Z * Matrix.M34 + Matrix.M44;
            if (w != 1)
            {
                Debug.Assert(w != 0);
                return new Vector3(x, y, z) / w;
            }

            return new Vector3(x, y, z);
        }

        public Vector3 ApplyToVector(Vector3 vector)
        {
            // https://github.com/microsoft/referencesource/blob/dae14279dd0672adead5de00ac8f117dcf74c184/System.Numerics/System/Numerics/Vector3.cs#L332
            //return new Vector3(
            //    normal.X * matrix.M11 + normal.Y * matrix.M21 + normal.Z * matrix.M31,
            //    normal.X * matrix.M12 + normal.Y * matrix.M22 + normal.Z * matrix.M32,
            //    normal.X * matrix.M13 + normal.Y * matrix.M23 + normal.Z * matrix.M33);
            return Vector3.TransformNormal(vector, Matrix);
        }

        public Vector3 ApplyToNormal(Vector3 normal)
        {
            float x = normal.X * InverseMatrix.M11 + normal.Y * InverseMatrix.M12 + normal.Z * InverseMatrix.M13;
            float y = normal.X * InverseMatrix.M21 + normal.Y * InverseMatrix.M22 + normal.Z * InverseMatrix.M23;
            float z = normal.X * InverseMatrix.M31 + normal.Y * InverseMatrix.M32 + normal.Z * InverseMatrix.M33;
            
            return new Vector3(x, y, z);
        }

        public Ray ApplyToRay(Ray ray)
        {
            Debug.Assert(ray != null);
            var origin = this.ApplyToPoint(ray.Origin);
            var direction = this.ApplyToVector(ray.Direction);
            return new Ray(origin, direction, ray.TMax, ray.Time);
        }

        public Bounds3 ApplyToBounds(Bounds3 bounds)
        {
            throw new NotImplementedException("See: https://www.pbr-book.org/3ed-2018/Geometry_and_Transformations/Applying_Transformations#BoundingBoxes");
        }

        public SurfaceInteraction ApplyToSurfaceInteraction(SurfaceInteraction surfaceInteraction)
        {
            throw new NotImplementedException("See https://pbr-book.org/3ed-2018/Geometry_and_Transformations/Interactions#SurfaceInteraction");
        }

        // https://www.pbr-book.org/3ed-2018/Geometry_and_Transformations/Transformations#Translations
        public static Transform FromTranslation(Vector3 delta)
        {
            return new Transform(Matrix4x4.CreateTranslation(delta));
        }

        // https://www.pbr-book.org/3ed-2018/Geometry_and_Transformations/Transformations#Scaling
        public static Transform FromScaling(Vector3 scale)
        {
            return new Transform(Matrix4x4.CreateScale(scale));
        }

        public static Transform FromRotationX(float degrees)
        {
            float radians = Mathf.DegToRad(degrees);
            Matrix4x4 matrix = Matrix4x4.CreateRotationX(radians);
            return new Transform(matrix);
        }

        public static Transform FromRotationY(float degrees)
        {
            float radians = Mathf.DegToRad(degrees);
            Matrix4x4 matrix = Matrix4x4.CreateRotationY(radians);
            return new Transform(matrix);
        }

        public static Transform FromRotationZ(float degrees)
        {
            float radians = Mathf.DegToRad(degrees);
            Matrix4x4 matrix = Matrix4x4.CreateRotationZ(radians);
            return new Transform(matrix);
        }

        // https://www.pbr-book.org/3ed-2018/Geometry_and_Transformations/Transformations#RotationaroundanArbitraryAxis
        public static Transform FromRotationAroundAxis(float degrees, Vector3 axis)
        {
            float radians = Mathf.DegToRad(degrees);
            Matrix4x4 matrix = Matrix4x4.CreateFromAxisAngle(axis, radians);
            return new Transform(matrix);
        }

        // https://www.pbr-book.org/3ed-2018/Geometry_and_Transformations/Transformations#TheLook-AtTransformation
        public static Transform FromLookAt(Vector3 cameraPosition, Vector3 target, Vector3 cameraUp)
        {            
            Matrix4x4 matrix = Matrix4x4.CreateLookAt(cameraPosition, target, cameraUp);
            return new Transform(matrix);
        }

        public static Transform operator *(Transform t1, Transform t2)
        {
            Matrix4x4 matrix = Matrix4x4.Multiply(t1.Matrix, t2.Matrix);
            Matrix4x4 invMatrix = Matrix4x4.Multiply(t2.InverseMatrix, t1.InverseMatrix);
            return new Transform(matrix, invMatrix);
            
        }
    }
}

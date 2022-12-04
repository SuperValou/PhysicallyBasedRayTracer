using System;
using System.Diagnostics;
using System.Numerics;
using Pbrt.Core;

namespace Pbrt.BSDFs
{
    /// <summary>
    /// Aggregation of Bidirectional Scattering Distribution Functions
    /// </summary>
    public class BSDF : IBSDF
    {
        private readonly Vector3 _geoNormal;

        // Shading coordinate system: (s, t, shading_normal) (x,y,z)
        // theta is measured from the z axis
        // phi is the angle formed with the x axis after projection onto the xy plane (azimuth)

        private readonly Vector3 _s; // x: dpdu
        private readonly Vector3 _t; // y: Cross(shadingNormal, dpdu)
        private readonly Vector3 _n; // z: shadingNormal

        public float RefractiveIndex { get; }

        public BSDF(float refractiveIndex, Vector3 geoNormal, Vector3 shadingNormal, Vector3 dpdu)
        {
            RefractiveIndex = refractiveIndex;
            _geoNormal = geoNormal;
            _n = shadingNormal;
            _s = Vector3.Normalize(dpdu);
            _t = Vector3.Cross(_n, _s);
        }

        public Spectrum Evaluate(Vector3 inDir, Vector3 outDir)
        {
            throw new NotImplementedException();
        }

        public Vector3 WorldToLocal(Vector3 v)
        {
            return new Vector3(
                Vector3.Dot(v, _s),
                Vector3.Dot(v, _t),
                Vector3.Dot(v, _n)
            );
        }

        public Vector3 LocalToWorld(Vector3 v)
        {
            return new Vector3(
                _s.X * v.X + _t.X * v.Y + _n.X * v.Z,
                _s.Y * v.X + _t.Y * v.Y + _n.Y * v.Z,
                _s.Z * v.X + _t.Z * v.Y + _n.Z * v.Z
            );
        }

        private float CosTheta(Vector3 direction)
        {
            Debug.Assert(direction.LengthSquared() == 1,
                         $"{nameof(direction)} was expected to be normalized, "
                         + "but had a lenght of {direction.Length()}.");

            // it's a property of the coordinate system we chose
            return direction.Z;
        }

        private float Cos2Theta(Vector3 direction)
        {
            return direction.Z * direction.Z;
        }

        private float AbsCosTheta(Vector3 direction)
        {
            return MathF.Abs(direction.Z);
        }

        private float Sin2Theta(Vector3 direction)
        {
            // cos² + sin² = 1 <=> sin² = 1 - cos²
            return MathF.Max(0, 1 - direction.Z * direction.Z);
        }

        private float SinTheta(Vector3 direction)
        {
            return MathF.Sqrt(Sin2Theta(direction));
        }

        private float TanTheta(Vector3 direction)
        {
            // tan = sin / cos
            Debug.Assert(direction.Z != 0);
            return SinTheta(direction) / direction.Z;
        }

        private float Tan2Theta(Vector3 direction)
        {
            Debug.Assert(direction.Z != 0);
            return Sin2Theta(direction) / Cos2Theta(direction);
        }

        private float CosPhi(Vector3 direction)
        {
            float sinTheta = SinTheta(direction);
            if (sinTheta == 0)
            {
                return 1;
            }

            float cosPhi = direction.X / sinTheta;
            return Math.Clamp(cosPhi, -1, 1);
        }

        private float SinPhi(Vector3 direction)
        {
            float sinTheta = SinTheta(direction);
            if (sinTheta == 0)
            {
                return 0;
            }

            float sinPhi = direction.Y / sinTheta;
            return Math.Clamp(sinPhi, -1, 1);
        }

        private float Cos2Phi(Vector3 direction)
        {
            return CosPhi(direction) * CosPhi(direction);
        }

        private float Sin2Phi(Vector3 direction)
        {
            return Sin2Phi(direction) * Sin2Phi(direction);
        }

        private float CosDeltaPhi(Vector3 dir1, Vector3 dir2)
        {
            // See https://www.pbr-book.org/3ed-2018/Reflection_Models#x0-GeometricSetting     
            float normalized2d = MathF.Sqrt((dir1.X * dir1.X + dir1.Y * dir1.Y) * (dir2.X * dir2.X + dir2.Y * dir2.Y));
            float dot = (dir1.X * dir2.X + dir1.Y * dir2.Y) / normalized2d;
            return Math.Clamp(dot, -1, 1);
        }
    }
}
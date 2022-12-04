using System;
using System.Numerics;

namespace Pbrt.Core
{
    /// <summary>
    /// Represents a Spectral power distributions (SPD), 
    /// i.e. a distribution function of wavelength that describes the amount of light at each wavelength. 
    /// Will be used for flux, intensity, irradiance, and radiance.
    /// </summary>
    public class Spectrum
    {
        /// <summary>
        /// Number of wavelength samples used to represent the SPD.
        /// </summary>
        public const int SamplesCount = 3;

        /// <summary>
        /// Coefficients of each sampled wwavelengths. 
        /// Here, coefficients are for Red, Green, and Blue wavelengths.
        /// </summary>
        private float[] _coefficients = new float[SamplesCount];

        public static Spectrum Black { get; } = new Spectrum();

        public bool IsBlack()
        {
            for (int i = 0; i < SamplesCount; i++)
            {
                if (_coefficients[i] != 0)
                {
                    return false;
                }
            }

            return true;
        }

        public bool HasNaN()
        {
            for (int i = 0; i < SamplesCount; i++)
            {
                if (float.IsNaN(_coefficients[i]))
                {
                    return true;
                }
            }

            return false;
        }

        // TODO: harcoded implemention for RGBSpectrum only
        public float[] ToXYZ() 
        {
            float[] xyz = new float[3];

            float r = _coefficients[0];
            float g = _coefficients[1];
            float b = _coefficients[2];

            xyz[0] = 0.412453f * r + 0.357580f * g + 0.180423f * b;
            xyz[1] = 0.212671f * r + 0.715160f * g + 0.072169f * b;
            xyz[2] = 0.019334f * r + 0.119193f * g + 0.950227f * b;

            return xyz;
        }

        public Vector3 ToRGB()
        {
            return new Vector3(_coefficients[0], _coefficients[1], _coefficients[2]);
        }

        public static Spectrum FromRGB(float r, float g, float b)
        {
            Spectrum result = new Spectrum();
            result._coefficients[0] = r;
            result._coefficients[1] = g;
            result._coefficients[2] = b;

            return result;
        }

        public static Spectrum Sqrt(Spectrum spectrum)
        {
            Spectrum result = new Spectrum();
            for (int i = 0; i < SamplesCount; i++)
            {
                result._coefficients[i] = MathF.Sqrt(spectrum._coefficients[i]);
            }

            return result;
        }

        // TODO: Pow() and Exp()

        public static Spectrum Lerp(float t, Spectrum s1, Spectrum s2)
        {
            return (1 - t) * s1 + t * s2;
        }

        public static Spectrum Clamp(Spectrum spectrum, float min = 0, float max = float.MaxValue)
        {
            Spectrum result = new Spectrum();
            for (int i = 0; i < SamplesCount; i++)
            {                
                result._coefficients[i] = Math.Clamp(spectrum._coefficients[i], min, max);
            }

            return result;
        }

        public static Spectrum operator +(Spectrum s1, Spectrum s2)
        {
            Spectrum result = new Spectrum();
            for (int i = 0; i < SamplesCount; i++)
            {
                result._coefficients[i] = s1._coefficients[i] + s2._coefficients[i];
            }

            return result;
        }

        public static Spectrum operator *(Spectrum s1, Spectrum s2)
        {
            Spectrum result = new Spectrum();
            for (int i = 0; i < SamplesCount; i++)
            {
                result._coefficients[i] = s1._coefficients[i] * s2._coefficients[i];
            }

            return result;
        }

        public static Spectrum operator *(float scalar, Spectrum spectrum)
        {
            Spectrum result = new Spectrum();
            for (int i = 0; i < SamplesCount; i++)
            {
                result._coefficients[i] = spectrum._coefficients[i] * scalar;
            }

            return result;
        }

        public static Spectrum operator *(Spectrum spectrum, float scalar)
        {
            return scalar * spectrum;
        }

        // TODO: subtraction, division, unary negation, equality and inequality
    }
}

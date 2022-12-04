namespace Pbrt.BSDFs
{
    public enum BxDF
    {
        /// <summary>
        /// BRDF
        /// </summary>
        Reflection = 1 << 0,

        /// <summary>
        /// BTDF
        /// </summary>
        Transmission = 1 << 1,

        Diffuse = 1 << 2,
        Glossy = 1 << 3,
        Specular = 1 << 4,
        All = ~0
    };
}
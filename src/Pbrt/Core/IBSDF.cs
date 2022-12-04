using System.Numerics;

namespace Pbrt.Core
{
    public interface IBSDF
    {
        Spectrum Evaluate(Vector3 inDir, Vector3 outDir);
    }
}
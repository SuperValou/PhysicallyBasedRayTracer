using Pbrt.Core;
using Pbrt.Integrators;
using System;

namespace Pbrt
{
    public class PhysicallyBasedRayTracer : IDisposable
    {
        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public void RenderScene()
        {
            Scene scene = null;

            Sampler sampler = null;
            Camera camera = null;

            SamplerIntegrator integrator = new WhittedIntegrator(camera, sampler);
            integrator.Preprocess(scene, sampler);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}

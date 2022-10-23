using Pbrt.Core;
using System;

namespace Pbrt.Integrators
{
    /// <summary>
    /// Rendering is driven by a stream of samples from a Sampler
    /// </summary>
    public abstract class SamplerIntegrator : IIntegrator
    {
        private Sampler _sampler;
        private Camera _camera;

        public SamplerIntegrator(Camera camera, Sampler sampler)
        {
            _camera = camera;
            _sampler = sampler;
        }

        // TODO: why not using the _sampler member?
        public virtual void Preprocess(Scene scene, Sampler sampler)
        {
            // do nothing by default, but can be overriden
        }

        public virtual void Render(Scene scene)
        {
            Preprocess(scene, _sampler);

            // Compute the number of tiles to use
            // TODO: 16x16

            // Render each tiles
            // TODO
            // foreach tile
            //    Li()

            throw new NotImplementedException();
        }

        // public abstract Spectrum Li(RayDifferential ray, Scene scene, Camera camera, Sampler sampler, int depth = 0);
    }
}

using Pbrt.Core;
using System;
using System.Collections.Generic;
using System.Numerics;

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
        protected virtual void Preprocess(Scene scene, Sampler sampler)
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
            
            for (int i = 0; i<64; i++)
            {
                for (int j = 0; j<64; j++)
                {

                }
            }
            throw new NotImplementedException();
        }

        // public abstract Spectrum Li(RayDifferential ray, Scene scene, Camera camera, Sampler sampler, int depth = 0);
    }
}

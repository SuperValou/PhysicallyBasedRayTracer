using Pbrt.Core;

namespace Pbrt.Integrators
{
    public class WhittedIntegrator : SamplerIntegrator
    {
        public WhittedIntegrator(Camera camera, Sampler sampler) 
            : base(camera, sampler)
        {            
        }
    }
}

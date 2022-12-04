using Pbrt.Core;

namespace Pbrt.Integrators
{
    public class WhittedIntegrator : SamplerIntegrator
    {
        public WhittedIntegrator(Camera camera, Sampler sampler) 
            : base(camera, sampler)
        {            
        }

        public override Spectrum Li(Ray ray, Scene scene, Sampler sampler, int depth = 0)
        {
            Spectrum radiance = Spectrum.Black;

            // Find closest ray intersection or return background radiance
            if (!scene.Intersect(ray, out SurfaceInteraction isect))
            {
                foreach (var light in scene.Lights)
                {
                    radiance += light.Le(ray);
                }
               
                return radiance;
            }

            radiance += isect.Le(isect.Direction);

            // TODO: see https://pbr-book.org/3ed-2018/Introduction/pbrt_System_Overview#AnIntegratorforWhittedRayTracing
            float r = 1f * sampler.PixelCoord.X / _camera.Film.Resolution.Width;
            float g = 1f * sampler.PixelCoord.Y / _camera.Film.Resolution.Height;
            return Spectrum.FromRGB(r, g, b:0);
        }
    }
}

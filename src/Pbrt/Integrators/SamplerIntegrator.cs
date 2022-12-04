using Pbrt.Core;
using System;
using System.Drawing;
using System.Numerics;

namespace Pbrt.Integrators
{
    /// <summary>
    /// Rendering is driven by a stream of samples from a Sampler
    /// </summary>
    public abstract class SamplerIntegrator : IIntegrator
    {
        protected Sampler _sampler;
        protected Camera _camera;

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

            // TODO: very naive implementation, see: https://pbr-book.org/3ed-2018/Introduction/pbrt_System_Overview#TheMainRenderingLoop
            
            // FilmTile filmTile = camera->film->GetFilmTile(tileBounds);
            for (int i = 0; i < _camera.Film.Resolution.Width; i++)
            {
                for (int j = 0; j < _camera.Film.Resolution.Height; j++)
                {
                    //_sampler.Clone();
                    var pixelCoord = new Point(i, j);
                    _sampler.StartPixel(pixelCoord);

                    Vector2 camSample = _sampler.GetCameraSample(pixelCoord);
                    float rayWeight = _camera.GenerateRay(camSample, out Ray ray);

                    Spectrum result = Li(ray, scene, _sampler) * rayWeight;
                    // filmTile->AddSample(cameraSample.pFilm, L, rayWeight);
                    _camera.Film.MergeFilmTile(pixelCoord, result);
                }
            }
            // camera->film->MergeFilmTile(filmTile);
        }

        public abstract Spectrum Li(Ray ray, Scene scene, Sampler sampler, int depth = 0);
    }
}

using Pbrt.Cameras;
using Pbrt.Core;
using Pbrt.ImageIOs;
using Pbrt.Integrators;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;

namespace Pbrt
{
    public class PhysicallyBasedRayTracer : IDisposable
    {
        public void Initialize()
        {
            // TODO: well, initialize what needs to be initialized
        }

        public void RenderScene(string outputFilePath)
        {
            Scene scene = DefaultScene.CreateDefaultScene();
            Camera camera = DefaultScene.CreateDefaultCamera();

            Sampler sampler = new Sampler();
            
            IIntegrator integrator = new WhittedIntegrator(camera, sampler);
            integrator.Render(scene);

            PngWriter.WriteImage(outputFilePath, camera.Film.GetPixels(), camera.Film.Resolution);
        }

        public void Dispose()
        {
            // Do nothing so far
        }

    }
}

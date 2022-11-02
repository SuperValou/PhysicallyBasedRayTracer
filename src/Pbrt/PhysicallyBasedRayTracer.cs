using Pbrt.Cameras;
using Pbrt.Core;
using Pbrt.Integrators;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace Pbrt
{
    public class PhysicallyBasedRayTracer : IDisposable
    {
        public void Initialize()
        {
            // TODO: well, initialize what needs to be initialized
        }

        public void RenderScene()
        {
            Scene scene = CreateDefaultScene();
            Camera camera = CreateDefaultCamera();

            Sampler sampler = new Sampler();
            
            SamplerIntegrator integrator = new WhittedIntegrator(camera, sampler);
            integrator.Render(scene);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        // TODO: stub
        private Scene CreateDefaultScene()
        {
            Primitive primitive = new Primitive();
            var lights = new List<Light>();
            Scene scene = new Scene(primitive, lights);
            return scene;
        }

        // TODO: stub
        private Camera CreateDefaultCamera()
        {
            var cameraPosition = new Vector3(-5, 0, 0);
            var cameraTransform = Transform.FromTranslation(cameraPosition);
            var film = new Film();
            return new ProjectiveCamera(cameraTransform, film);
        }
    }
}

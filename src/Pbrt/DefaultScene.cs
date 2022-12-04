using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using Pbrt.Core;
using Pbrt.Cameras;
using Pbrt.Primitives;
using Pbrt.Shapes;

namespace Pbrt
{
    public static class DefaultScene
   {
        public static Scene CreateDefaultScene()
        {
            Transform sphereTransform = Transform.FromTranslation(Vector3.UnitZ);
            float sphereRadius = 0.5f;
            Shape sphere = new Sphere(sphereTransform, sphereRadius);

            Material sphereMaterial = new Material();

            IPrimitive primitive = new GeometricPrimitive(sphere, sphereMaterial); // TODO: instantiate
            var lights = new List<Light>();
            Scene scene = new Scene(primitive, lights);
            return scene;
        }

        public static Camera CreateDefaultCamera()
        {
            var cameraPosition = new Vector3(-5, 0, 0);
            var cameraTransform = Transform.FromTranslation(cameraPosition);
            var film = new Film(512, 512);
   
            float fov = 26.5f;
            Camera cam = PerspectiveCamera.Create(cameraTransform, film, fov);
            return cam;
        }
    }
}

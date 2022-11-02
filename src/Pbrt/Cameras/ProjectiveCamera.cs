using Pbrt.Core;

namespace Pbrt.Cameras
{
    public class ProjectiveCamera : Camera
    {
        public ProjectiveCamera(Transform transform, Film film) : base(transform, film)
        {
        }

        public override float GenerateRay(CameraSample sample, out Ray ray)
        {
            throw new System.NotImplementedException();
        }
    }
}

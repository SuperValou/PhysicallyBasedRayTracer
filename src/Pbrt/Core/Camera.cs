using System;
using System.Numerics;

namespace Pbrt.Core
{
    public abstract class Camera
    {
        private Transform _transform;
        private Film _film;

        protected Camera(Transform transform, Film film)
        {
            _transform = transform ?? throw new ArgumentNullException(nameof(transform));
            _film = film ?? throw new ArgumentNullException(nameof(film));
        }

        /// <summary>
        /// Computes the direction-normalized ray corresponding to a given sample from the film plane.
        /// Returns how much the radiance along this ray will contribute to the final image.
        /// </summary>
        public abstract float GenerateRay(CameraSample sample, out Ray ray);
    }

    public struct CameraSample
    {
        /// <summary>
        /// the point on the film to which the generated ray carries radiance.
        /// </summary>
        Vector2 PointOnFilm;

        /// <summary>
        /// The point on the lens the ray passes through.
        /// </summary>
        Vector2 PointOnLens;

        /// <summary>
        /// The time at which the ray should sample the scene.
        /// </summary>
        //float Time;
    };
}

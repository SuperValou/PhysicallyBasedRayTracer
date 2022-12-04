using System;
using System.Diagnostics;
using System.Numerics;

namespace Pbrt.Core
{
    public abstract class Camera
    {
        protected Transform Transform { get; }

        public Film Film { get; }

        protected Camera(Transform transform, Film film)
        {
            Transform = transform ?? throw new ArgumentNullException(nameof(transform));
            Film = film ?? throw new ArgumentNullException(nameof(film));

            Debug.Assert(!film.Resolution.IsEmpty);
        }

        /// <summary>
        /// Computes the direction-normalized ray corresponding to the given location on the film plane.
        /// Returns how much the radiance along this ray will contribute to the final image.
        /// </summary>
        /// <param name="rasterCoordinates">Float-coordinates in raster space, ranging from (0,0) to (Width,Height).</param>
        public abstract float GenerateRay(Vector2 rasterCoordinates, out Ray ray);
    }
}

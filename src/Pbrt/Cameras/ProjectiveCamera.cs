using Pbrt.Core;
using System.Diagnostics;
using System.Numerics;

namespace Pbrt.Cameras
{
    public abstract class ProjectiveCamera : Camera
    {
        protected Transform CameraToScreenTransform { get;}
        protected Transform ScreenToRasterTransform { get; }

        protected Transform RasterToScreenTransform { get; }

        /// <summary>
        /// Short for Raster -> Screen -> Camera
        /// </summary>
        protected Transform RasterToCameraTransform { get; }

        protected float LensRadius { get; }
        protected float FocalDistance { get; }

        public ProjectiveCamera(Transform transform, Transform cameraToScreenTransform, 
            Bounds2 screenWindow, float lensRadius, float focalDistance, Film film)
            : base(transform, film)
        {
            CameraToScreenTransform = cameraToScreenTransform;

            // Compute screen-to-raster transformation:
            // Start with a point in screen space, 
            // translate so that the upper-left corner of the screen is at the origin,
            // scale by the reciprocal of the screen width and height (giving a point between 0 and 1, i.e. NDC coordinates),
            // then scale by the raster resolution (to cover from (0,0) up to (width, heigth)). 
            // Note the y coordinate is inverted because increasing y values move *up* in screen coordinates but *down* in raster coordinates.
            Debug.Assert(screenWindow.IsNonEmpty());
            var resolutionScaling = new Vector3(film.Resolution.Width, film.Resolution.Height, 1);
            var windowScaling = new Vector3(
                                        1 / (screenWindow.MaxPoint.X - screenWindow.MinPoint.X),
                                        1 / (screenWindow.MaxPoint.Y - screenWindow.MinPoint.Y),
                                        1);
            var screenTranslation = new Vector3(-screenWindow.MinPoint.X, -screenWindow.MaxPoint.Y, 0);
            ScreenToRasterTransform = Transform.FromScaling(resolutionScaling)
                * Transform.FromScaling(windowScaling)
                * Transform.FromTranslation(screenTranslation);

            RasterToScreenTransform = Transform.Inverse(ScreenToRasterTransform);
            RasterToCameraTransform = Transform.Inverse(CameraToScreenTransform) * RasterToScreenTransform;
        }
    }
}

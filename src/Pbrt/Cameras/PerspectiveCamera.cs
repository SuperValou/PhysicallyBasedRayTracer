using Pbrt.Core;
using System;
using System.Diagnostics;
using System.Numerics;

namespace Pbrt.Cameras
{
    public class PerspectiveCamera : Camera
    {
        public const float NearPlaneDistance = 0.01f;
        public const float FarPlaneDistance = 1000f;

        private Transform _rasterToScreen;
        private Transform _screenToCamera;

        private PerspectiveCamera(Transform transform, Film film)
            : base(transform, film)
        {            
        }

        /// <summary>
        /// Creates a new <see cref="PerspectiveCamera"/>.
        /// </summary>
        /// <param name="fieldOfView">FOV in degrees.</param>        
        public static PerspectiveCamera Create(Transform transform, Film film, float fieldOfView, float nearPlane = NearPlaneDistance, float farPlane = FarPlaneDistance)
        {
            Debug.Assert(film != null && film.Resolution.Height != 0 && film.Resolution.Width != 0);
            var cam = new PerspectiveCamera(transform, film);
            float aspectRatio = film.Resolution.Width / film.Resolution.Height;

            // Raster ranges from top-left (0,0) to bottom-right (Width,Height)
            // Screen ranges from top-left 
            float resolutionScaling = 1f / MathF.Min(film.Resolution.Width, film.Resolution.Height);

            Matrix4x4 rasterToScreenMatrix = Matrix4x4.CreateTranslation(-1f * (film.Resolution.Width - 1) / 2f, -1f * (film.Resolution.Height - 1) / 2f, 0)
                                           * Matrix4x4.CreateScale(resolutionScaling, -resolutionScaling, 1f);

            cam._rasterToScreen = new Transform(rasterToScreenMatrix);

            // Create the screen to cam transform by inversing a cam to sreen matrix            
            float radians = Mathf.DegToRad(fieldOfView);
            
            var perspectiveMatrix = Matrix4x4.CreatePerspectiveFieldOfView(radians, aspectRatio, nearPlane, farPlane);
            perspectiveMatrix = Matrix4x4.CreateScale(1, 1, -1) * perspectiveMatrix; // flip z-axis to get a left-handed coord system
            
            bool ok = Matrix4x4.Invert(perspectiveMatrix, out var invPerspMatrix);
            Debug.Assert(ok);

            cam._screenToCamera = new Transform(invPerspMatrix, perspectiveMatrix);
            return cam;
        }

        public override float GenerateRay(Vector2 rasterCoordinates, out Ray ray)
        {
            // Transform to screen coordinates, assuming the point lies on the near plane (z = 0)
            Vector3 pointInScreenSpace = _rasterToScreen.TransformPoint(new Vector3(rasterCoordinates, z:0));

            // Transform to Camera space
            Vector3 pointInCameraSpace = _screenToCamera.TransformPoint(pointInScreenSpace);
            Vector3 rayDirection = Vector3.Normalize(pointInCameraSpace);
            ray = new Ray(Vector3.Zero, rayDirection);            

            // Transfrom to World space
            ray = this.Transform.TransformRay(ray);

            return 1;
        }
    }
}

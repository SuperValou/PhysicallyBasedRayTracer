using NUnit.Framework;
using Pbrt.Core;
using Pbrt.Cameras;
using System.Numerics;
using System.Drawing;
using System;

namespace PbrtTests
{
    [TestFixture]
    public class PerspectiveCameraTests
    {
        private Vector3 _camPosition = -5 * Vector3.UnitZ;
        private PerspectiveCamera _cam;

        [SetUp]
        public void Setup()
        {            
            var transform = Transform.FromTranslation(_camPosition);
            var film = new Film(3, 3);

            float fov = 60;
            _cam = PerspectiveCamera.Create(transform, film, fov, nearPlane: 0.3f, farPlane: 1000);
        }

        [Test]        
        [TestCase(1, 1,     0, 0, 1)] // center
        [TestCase(0, 0,     -0.57735026f, 0.57735026f, 0.57735026f)]  // top-left corner
        [TestCase(2, 0,     0.57735026f, 0.57735026f, 0.57735026f)] // top-right corner
        [TestCase(0, 2,     -0.57735026f, -0.57735026f, 0.57735026f)] // bottom-left corner
        [TestCase(2, 2,     0.57735026f, -0.57735026f, 0.57735026f)] // bottom-left corner
        public void GenerateRay_CenterOfFilm_ReturnForwardRay(float xRaster, float yRaster, float xDir, float yDir, float zDir)
        {            
            Vector2 rasterCoordinates = new Vector2(xRaster, yRaster);
            float rayContribution = _cam.GenerateRay(rasterCoordinates, out Ray ray);

            Assert.AreEqual(1, rayContribution);
            Assert.AreEqual(_camPosition, ray.Origin);
            Assert.AreEqual(new Vector3(xDir, yDir, zDir), ray.Direction);
        }
    }
}
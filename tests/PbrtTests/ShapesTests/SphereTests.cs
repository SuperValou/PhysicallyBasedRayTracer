using NUnit.Framework;
using System.Numerics;
using Pbrt.Core;
using Pbrt.Shapes;

namespace PbrtTests
{
    [TestFixture]
    public class SphereTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Intersect_RayDirectedForwardAtSphere_ReturnTrue()
        {
            Sphere sphere = new Sphere(Transform.Identity, 1f);
            Ray ray = new Ray(-5 * Vector3.UnitZ, Vector3.UnitZ, maxRange: 100);

            bool success = sphere.Intersect(ray, out float hit, out _);

            Assert.IsTrue(success);
            Assert.AreEqual(4, hit);
        }

        [Test]
        public void Intersect_RayTangentToSphere_ReturnTrue()
        {
            Sphere sphere = new Sphere(Transform.Identity, 1f);
            Ray ray = new Ray(new Vector3(1, 0, -5), Vector3.UnitZ, maxRange: 100);

            bool success = sphere.Intersect(ray, out float hit, out _);

            Assert.IsTrue(success);
            Assert.AreEqual(5, hit);
        }

        [Test]
        public void Intersect_RayDirectedUpward_ReturnFalse()
        {
            Sphere sphere = new Sphere(Transform.Identity, 1f);
            Ray ray = new Ray(-5 * Vector3.UnitZ, Vector3.UnitY, maxRange: 100);

            bool success = sphere.Intersect(ray, out float hit, out _);

            Assert.IsFalse(success);
        }

        [Test]
        public void Intersect_RayDirectedToTheRight_ReturnFalse()
        {
            Sphere sphere = new Sphere(Transform.Identity, 1f);
            Ray ray = new Ray(-5 * Vector3.UnitZ, Vector3.UnitX, maxRange: 100);

            bool success = sphere.Intersect(ray, out float hit, out _);

            Assert.IsFalse(success);
        }
    }
}
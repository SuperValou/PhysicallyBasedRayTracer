using NUnit.Framework;
using Pbrt.Core;
using System.Numerics;

namespace PbrtTests
{
    [TestFixture]
    public class TransformTests
    {
        private Vector3 _forwardPoint;
        private Vector3 _upVector;

        [SetUp]
        public void Setup()
        {
            _forwardPoint = Vector3.UnitZ;
            _upVector = Vector3.UnitY;
        }

        [Test]
        public void FromTranslation_ReturnsValidTransform()
        {
            Transform t = Transform.FromTranslation(Vector3.UnitX);  // moves to the right
            var point = t.TransformPoint(_forwardPoint);
            var vector = t.TransformVector(_upVector);

            Assert.AreEqual(new Vector3(1, 0, 1), point);
            Assert.AreEqual(new Vector3(0, 1, 0), vector);
        }

        [Test]
        public void FromRotation_ReturnsValidTransform()
        {
            Transform t = Transform.FromRotationX(-90);  // rotates to look up
            var point = t.TransformPoint(_forwardPoint);
            var vector = t.TransformVector(_upVector);

            AssertVector3AreEqual(new Vector3(0, 1, 0), point);
            AssertVector3AreEqual(new Vector3(0, 0, -1), vector);
        }

        [Test]
        public void FromScaling_ReturnsValidTransform()
        {
            Transform t = Transform.FromScaling(2 * Vector3.One);  // scales up
            var point = t.TransformPoint(_forwardPoint);
            var vector = t.TransformVector(_upVector);

            Assert.AreEqual(new Vector3(0, 0, 2), point);
            Assert.AreEqual(new Vector3(0, 2, 0), vector);
        }

        [Test]
        public void FromPerspective_ReturnsValidTransform()
        {
            Transform t = Transform.FromPerspective(fov: 60, aspectRatio: 16f/9f, nearPlaneDistance: 0.3f, farPlaneDistance: 1000f);

            var worldOrigin = new Vector3(0, 0, 0);
            var worldCenterOfNearPlane = new Vector3(0, 0, 0.3f);
            var worldCenterOfFarPlane = new Vector3(0, 0, 1000);

            var projectedOrigin = t.TransformPoint(worldOrigin);
            var projectedCenterOfNearPlane = t.TransformPoint(worldCenterOfNearPlane);
            var projectedCenterOfFarPlane = t.TransformPoint(worldCenterOfFarPlane);

            AssertVector3AreEqual(new Vector3(0, 0, -0.30009f), projectedOrigin);
            AssertVector3AreEqual(new Vector3(0, 0, 0), projectedCenterOfNearPlane);
            AssertVector3AreEqual(new Vector3(0, 0, 1), projectedCenterOfNearPlane);
        }

        [Test]
        public void SwapsHandedness_No_ReturnsFalse()
        {
            Transform t = Transform.FromScaling(Vector3.One * 2) * Transform.FromRotationZ(200) * Transform.FromTranslation(Vector3.One);
            Assert.IsFalse(t.SwapsHandedness());
        }

        [Test]
        public void SwapsHandedness_Yes_ReturnsTrue()
        {
            Transform t = Transform.FromScaling(-1, 1, 1);
            Assert.IsTrue(t.SwapsHandedness());
        }

        private void AssertFloatAreEqual(float expected, float actual)
        {
            float sqrDistance = (expected - actual) * (expected - actual);
            Assert.Less(sqrDistance, 9.99999944E-11f, $"Expected {expected}, but was {actual}");
        }

        private void AssertVector3AreEqual(Vector3 expected, Vector3 actual)
        {
            float sqrDistance = (expected - actual).LengthSquared();
            Assert.Less(sqrDistance, 9.99999944E-11f, $"Expected {expected}, but was {actual}");
        }
    }
}
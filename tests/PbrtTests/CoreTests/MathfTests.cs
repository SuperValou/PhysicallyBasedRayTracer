using NUnit.Framework;
using Pbrt.Core;

namespace PbrtTests
{
    [TestFixture]
    public class MathfTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SolveQuadratic_OneSolution_ReturnsTrue()
        {
            bool success = Mathf.SolveQuadratic(1, 4, 4, out float t0, out float t1);
            Assert.IsTrue(success);
            Assert.AreEqual(-2, t0);
            Assert.AreEqual(-2, t1);
        }
    }
}
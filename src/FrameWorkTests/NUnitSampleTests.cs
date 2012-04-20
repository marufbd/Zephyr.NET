using NUnit.Framework;

namespace FrameWorkTests {
    [TestFixture]
    public class NUnitSampleTests
    {
        private int a, b;

        [SetUp]
        public void Init()
        {
            a = 5;
            b = 7;
        }

        [TearDown]
        public void EndCase()
        {
            a = 10;
            b = 3;
        }
        
        
        [Test]
        public void SomePassingTest() {
            Assert.AreEqual(a, a);
        }

        [Test]
        public void SomeFailingTest() {
            Assert.Greater(b, a);
        }
    }
}

using NUnit.Framework;

namespace DataDrivenTests
{
    /// <summary>
    /// Demonstrates parameterized fixtures using [TestFixture(value)].
    /// Each attribute creates a separate fixture instance with its own constructor argument,
    /// so every [Test] inside runs once per fixture — here twice, with x=10 and x=42.
    /// </summary>
    [TestFixture(10)]
    [TestFixture(42)]
    public class DataDrivenTestFixture
    {
        int _x;

        public DataDrivenTestFixture(int x)
        {
            _x = x;
        }

        [Test]
        public void TestArguments()
        {
            Assert.Pass($"X is {_x}");
        }
    }
}

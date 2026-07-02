using NUnit.Framework;

namespace DataDrivenTests
{
    /// <summary>
    /// Demonstrates generic fixtures using [TestFixture(typeof(T))].
    /// Passing a type argument to [TestFixture] instantiates the open generic class
    /// with that concrete type, so every [Test] runs once per supplied type argument.
    /// Useful for verifying type-invariant behavior across multiple implementations.
    /// </summary>
    [TestFixture(typeof(int))]
    [TestFixture(typeof(string))]
    public class GenericTestFixture<T>
    {
        [Test]
        public void TestType()
        {
            Assert.Pass($"The generic test type is {typeof(T)}");
        }
    }
}

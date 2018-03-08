using NUnit.Framework;

namespace DataDrivenTests
{
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

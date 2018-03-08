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
        public void TestArgurments()
        {
            Assert.Pass($"X is {_x}");
        }
    }
}

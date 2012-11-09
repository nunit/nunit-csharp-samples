using NUnit.Core.Extensions;
using NUnit.Framework;

[assembly: RequiredAddin( "RepeatUntilSuccess Addin" )]
namespace RepeatUntilSuccessTests
{
    /// <summary>
    /// Tests for the RepeatUntilSuccess decorator. Some
    /// of these tests are actually expected to fail,
    /// so the results must be examined visually.
    /// </summary>
    /// <remarks>
    /// Each test must be in its own class because we use
    /// TestFixtureSetUp, SetUp, and TestFixtureTearDown
    /// to test the number of repetitions that occurred.
    /// </remarks>
    [TestFixture]
    public abstract class BaseTests
    {
        protected int ExpectedCount;
        protected int Count;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            Count = 0;
            ExpectedCount = 0;
        }

        /// <summary>
        /// This is run when a test is repeated.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            Count++;
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            Assert.AreEqual( ExpectedCount, Count, "Did not repeat as expected" );
        }
    }

    public class SuccessOnNoFailures : BaseTests
    {
        [Test, RepeatUntilSuccess( 3 )]
        public void ExecuteTest()
        {
            ExpectedCount = 1;

            Assert.Pass( "Expected success: " + Count );
        }
    }

    public class SuccessOnSomeFailures : BaseTests
    {
        [Test, RepeatUntilSuccess( 3 )]
        public void ExecuteTest()
        {
            ExpectedCount = 2;

            if ( Count < ExpectedCount )
            {
                Assert.Fail( "Expected failure: " + Count );
            }
            else
            {
                Assert.Pass( "Expected success: " + Count );
            }
        }
    }

    public class FailureOnAllFailures : BaseTests
    {
        [Test, RepeatUntilSuccess( 3 )]
        public void ExecuteTest()
        {
            ExpectedCount = 3;

            Assert.Fail( "Expected failure: " + Count );
        }
    }

    public class InvalidOnNoRepetitions : BaseTests
    {
        [Test, RepeatUntilSuccess( 0 )]
        public void ExecuteTest()
        {
        }
    }

    public class SuccessAndNoRepetitionsWithoutAttribute : BaseTests
    {
        [Test]
        public void ExecuteTest()
        {
            ExpectedCount = 1;
        }
    }
}

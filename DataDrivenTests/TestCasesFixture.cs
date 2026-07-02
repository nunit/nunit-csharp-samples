using NUnit.Framework;

namespace DataDrivenTests
{
    /// <summary>
    /// Examples of the [TestCase] attribute, which embeds test data inline on the test method.
    /// Each [TestCase] produces one independent test run with its own arguments.
    /// </summary>
    [TestFixture]
    public class TestCasesFixture
    {
        // Basic inline values — one test per attribute.
        [TestCase(1, 2, 3)]
        [TestCase(-1, -2, -3)]
        [TestCase(0, 0, 0)]
        public void Add_ReturnsCorrectSum(int a, int b, int expected)
        {
            Assert.That(a + b, Is.EqualTo(expected));
        }

        // ExpectedResult lets the return value be compared automatically,
        // removing the need for an explicit Assert inside the method.
        [TestCase(10, 2, ExpectedResult = 5)]
        [TestCase(9, 3, ExpectedResult = 3)]
        [TestCase(7, 1, ExpectedResult = 7)]
        public int Divide_ReturnsCorrectQuotient(int dividend, int divisor)
        {
            return dividend / divisor;
        }

        // TestName gives each case a human-readable name in the test runner.
        [TestCase("hello", "HELLO", TestName = "Lowercase to uppercase")]
        [TestCase("WORLD", "WORLD", TestName = "Already uppercase stays the same")]
        [TestCase("", "", TestName = "Empty string stays empty")]
        public void ToUpper_ProducesExpectedResult(string input, string expected)
        {
            Assert.That(input.ToUpper(), Is.EqualTo(expected));
        }

        // Null and edge-case values work directly in the attribute.
        [TestCase(null, true)]
        [TestCase("", false)]
        [TestCase("hello", false)]
        public void IsNull_DetectsNullCorrectly(string value, bool expectedResult)
        {
            Assert.That(value is null, Is.EqualTo(expectedResult));
        }

        // Multiple attributes on the same method combine cleanly.
        // Category can be applied per-method; individual cases can use Description.
        [TestCase(2, ExpectedResult = true, Description = "Even number")]
        [TestCase(3, ExpectedResult = false, Description = "Odd number")]
        [TestCase(0, ExpectedResult = true, Description = "Zero is even")]
        [Category("Arithmetic")]
        public bool IsEven_ReturnsCorrectResult(int number)
        {
            return number % 2 == 0;
        }

        // Explicit pass/fail — useful when the assertion logic is conditional.
        [TestCase(5, 3, true)]
        [TestCase(3, 5, false)]
        [TestCase(4, 4, false)]
        public void IsGreaterThan_ReturnsExpectedResult(int a, int b, bool expected)
        {
            Assert.That(a > b, Is.EqualTo(expected));
        }

        // String parsing — verifying type conversions driven by inline data.
        [TestCase("42", 42)]
        [TestCase("-7", -7)]
        [TestCase("0", 0)]
        public void ParseInt_ConvertsStringCorrectly(string input, int expected)
        {
            Assert.That(int.Parse(input), Is.EqualTo(expected));
        }
    }
}

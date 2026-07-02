using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace DataDrivenTests
{
    /// <summary>
    /// Examples of [TestCaseSource], which pulls test data from a method, field, property,
    /// or a separate class — keeping test data separate from the test logic.
    /// </summary>
    [TestFixture]
    public class TestCaseSourceFixture
    {
        // ── 1. Static method returning IEnumerable<object[]> ────────────────────
        // The simplest form: a private static method on the same class.
        static IEnumerable<object[]> AddCases()
        {
            yield return [1, 2, 3];
            yield return [-5, 5, 0];
            yield return [100, -1, 99];
        }

        [TestCaseSource(nameof(AddCases))]
        public void Add_FromStaticMethod(int a, int b, int expected)
        {
            Assert.That(a + b, Is.EqualTo(expected));
        }

        // ── 2. Static field ──────────────────────────────────────────────────────
        // Data stored in a static field; useful when the set is fixed and small.
        static readonly object[] MultiplyCases =
        [
            new object[] { 3, 4, 12 },
            new object[] { 0, 99, 0 },
            new object[] { -2, 5, -10 }
        ];

        [TestCaseSource(nameof(MultiplyCases))]
        public void Multiply_FromStaticField(int a, int b, int expected)
        {
            Assert.That(a * b, Is.EqualTo(expected));
        }

        // ── 3. Static property ───────────────────────────────────────────────────
        static IEnumerable<object[]> StringCases
        {
            get
            {
                yield return ["hello", 5];
                yield return ["", 0];
                yield return ["NUnit", 5];
            }
        }

        [TestCaseSource(nameof(StringCases))]
        public void StringLength_FromProperty(string input, int expectedLength)
        {
            Assert.That(input.Length, Is.EqualTo(expectedLength));
        }

        // ── 4. TestCaseData — rich metadata per case ─────────────────────────────
        // TestCaseData supports per-case names, descriptions, categories, and
        // an expected result that NUnit compares against the return value.
        static IEnumerable<TestCaseData> DivisionCases()
        {
            yield return new TestCaseData(10, 2)
                .Returns(5)
                .SetName("Ten divided by two")
                .SetDescription("Basic positive division");

            yield return new TestCaseData(9, 3)
                .Returns(3)
                .SetName("Nine divided by three");

            yield return new TestCaseData(-12, 4)
                .Returns(-3)
                .SetName("Negative dividend");
        }

        [TestCaseSource(nameof(DivisionCases))]
        public int Divide_WithTestCaseData(int dividend, int divisor)
        {
            return dividend / divisor;
        }

        // ── 5. TestCaseData with per-case category and ignore ───────────────────
        // SetCategory assigns a category to an individual case; Ignore skips it
        // with a reason that appears in the test runner output.
        static IEnumerable<TestCaseData> ModuloCases()
        {
            yield return new TestCaseData(10, 3, 1)
                .SetName("10 mod 3")
                .SetCategory("Arithmetic");

            yield return new TestCaseData(15, 5, 0)
                .SetName("15 mod 5 — exact divisor")
                .SetCategory("Arithmetic");

            yield return new TestCaseData(7, 2, 1)
                .SetName("7 mod 2 — odd number")
                .SetCategory("Arithmetic");
        }

        [TestCaseSource(nameof(ModuloCases))]
        public void Modulo_FromTestCaseData(int value, int divisor, int expected)
        {
            Assert.That(value % divisor, Is.EqualTo(expected));
        }

        // ── 6. Source from a separate class ──────────────────────────────────────
        // Passing typeof(SourceClass) lets the data live entirely outside the fixture,
        // making it reusable across multiple test classes.
        [TestCaseSource(typeof(IsEvenCaseSource), nameof(IsEvenCaseSource.Cases))]
        public void IsEven_FromExternalClass(int number, bool expected)
        {
            Assert.That(number % 2 == 0, Is.EqualTo(expected));
        }
    }

    // ── External source class ────────────────────────────────────────────────────
    // Any class can act as a source; the member must still be static.
    public class IsEvenCaseSource
    {
        public static IEnumerable Cases
        {
            get
            {
                yield return new TestCaseData(2, true).SetName("Two is even");
                yield return new TestCaseData(3, false).SetName("Three is odd");
                yield return new TestCaseData(0, true).SetName("Zero is even");
                yield return new TestCaseData(-4, true).SetName("Negative even");
            }
        }
    }
}

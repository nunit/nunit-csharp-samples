using NUnit.Framework;

namespace TestCaseGeneration;

/// <summary>
/// Demonstrates [Range] — generates a sequence of values between two bounds,
/// with an optional step. Works with int, long, float, and double.
/// Both bounds are inclusive.
/// </summary>
[TestFixture]
public class RangeFixture
{
    // ── Integer range, default step of 1 ─────────────────────────────────────
    // [Range(from, to)] generates: 1, 2, 3, 4, 5 — 5 tests.
    [Test]
    public void IntRange([Range(1, 5)] int n)
    {
        Assert.That(n, Is.InRange(1, 5));
    }

    // ── Integer range with explicit step ──────────────────────────────────────
    // [Range(0, 10, 2)] generates: 0, 2, 4, 6, 8, 10 — 6 tests.
    [Test]
    public void IntRangeWithStep([Range(0, 10, 2)] int n)
    {
        Assert.That(n % 2, Is.EqualTo(0));
    }

    // ── Double range ──────────────────────────────────────────────────────────
    // [Range(0.0, 1.0, 0.25)] generates: 0.0, 0.25, 0.5, 0.75, 1.0 — 5 tests.
    [Test]
    public void DoubleRange([Range(0.0, 1.0, 0.25)] double d)
    {
        Assert.That(d, Is.InRange(0.0, 1.0));
    }

    // ── Float range ───────────────────────────────────────────────────────────
    // [Range(float, float, float)] also supported.
    // Generates: 0.0f, 0.5f, 1.0f — 3 tests.
    [Test]
    public void FloatRange([Range(0.0f, 1.0f, 0.5f)] float f)
    {
        Assert.That(f, Is.InRange(0.0f, 1.0f));
    }

    // ── Descending range ──────────────────────────────────────────────────────
    // Negative step counts down from from to to.
    // [Range(10, 0, -2)] generates: 10, 8, 6, 4, 2, 0 — 6 tests.
    [Test]
    public void DescendingRange([Range(10, 0, -2)] int n)
    {
        Assert.That(n % 2, Is.EqualTo(0));
    }

    // ── Multiple range parameters ─────────────────────────────────────────────
    // Like [Values], multiple [Range] parameters combine by default (Combinatorial).
    // 3 values × 3 values = 9 tests.
    // See the Strategy* fixtures to control how multiple parameters combine.
    [Test]
    public void MultipleRanges([Range(1, 3)] int a, [Range(10, 30, 10)] int b)
    {
        Assert.That(a + b, Is.GreaterThan(0));
    }
}

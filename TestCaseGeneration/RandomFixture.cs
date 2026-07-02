using NUnit.Framework;

namespace TestCaseGeneration;

/// <summary>
/// Demonstrates [Random] — generates a specified number of random values for
/// a parameter at test discovery time. The values change between test runs, so
/// tests should assert invariants (properties that always hold) rather than
/// specific values.
/// </summary>
[TestFixture]
public class RandomFixture
{
    // ── Random integers — count only ──────────────────────────────────────────
    // Generates 5 random int values across the full int range.
    // 5 tests total.
    [Test]
    public void RandomInt([Random(5)] int n)
    {
        // Assert an invariant, not a specific value
        Assert.That(n, Is.InstanceOf<int>());
    }

    // ── Random integers — bounded range ───────────────────────────────────────
    // [Random(min, max, count)] constrains values to [min, max] inclusive.
    // Generates 5 tests, each with a value in [1, 100].
    [Test]
    public void RandomIntInRange([Random(1, 100, 5)] int n)
    {
        Assert.That(n, Is.InRange(1, 100));
    }

    // ── Random doubles ─────────────────────────────────────────────────────────
    // Works the same way for floating-point types.
    // Generates 4 tests, each with a value in [0.0, 1.0].
    [Test]
    public void RandomDoubleInRange([Random(0.0, 1.0, 4)] double d)
    {
        Assert.That(d, Is.InRange(0.0, 1.0));
    }

    // ── Multiple random parameters ────────────────────────────────────────────
    // Each parameter independently generates its random values.
    // NUnit combines them Combinatorially by default: 3 × 3 = 9 tests.
    // See the Strategy* fixtures to control how multiple parameters combine.
    [Test]
    public void MultipleRandomParameters([Random(1, 50, 3)] int a,
                                         [Random(1, 50, 3)] int b)
    {
        Assert.That(a + b, Is.GreaterThan(0));
    }
}

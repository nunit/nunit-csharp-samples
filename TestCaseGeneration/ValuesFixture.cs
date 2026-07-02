using System;
using NUnit.Framework;

namespace TestCaseGeneration;

// ══════════════════════════════════════════════════════════════════════════════
// DIMENSION 1 — GENERATION ATTRIBUTES
//
// Generation attributes ([Values], [ValueSource], [Range], [Random]) specify
// WHAT values each individual parameter receives. NUnit generates one test run
// per combination of values across all parameters.
//
// How the combinations are assembled is a separate concern controlled by the
// strategy attributes ([Combinatorial], [Sequential], [Pairwise]) — see the
// Strategy* fixtures for examples of those.
// ══════════════════════════════════════════════════════════════════════════════

/// <summary>
/// Demonstrates [Values] — the simplest way to supply test data directly
/// on the parameter, without needing any external source or range.
/// </summary>
[TestFixture]
public class ValuesFixture
{
    // ── Inline values on a single parameter ──────────────────────────────────
    // Generates one test per value: 3 tests total.
    [Test]
    public void SingleParameter([Values(2, 4, 6)] int n)
    {
        Assert.That(n % 2, Is.EqualTo(0));
    }

    // ── Multiple parameters ───────────────────────────────────────────────────
    // NUnit combines all values across parameters (Combinatorial by default).
    // 3 strings × 2 ints = 6 tests.
    // See the Strategy* fixtures for full control over how values are combined.
    [Test]
    public void MultipleParameters([Values("hello", "world", "!")] string s,
                                   [Values(1, -1)] int multiplier)
    {
        Assert.That(s.Length * multiplier, Is.Not.Zero.Or.EqualTo(0));
    }

    // ── Null as a value ───────────────────────────────────────────────────────
    // null is a valid [Values] argument; NUnit passes it as-is.
    [Test]
    public void NullIsAValidValue([Values(null, "hello", "world")] string s)
    {
        Assert.That(s is null || s.Length >= 0);
    }

    // ── Bool shorthand ────────────────────────────────────────────────────────
    // [Values] with no arguments on a bool generates both true and false
    // automatically — no need to spell them out.
    // Generates 2 tests.
    [Test]
    public void BoolShorthand([Values] bool flag)
    {
        Assert.That(flag, Is.True.Or.False);
    }

    // ── Enum shorthand ────────────────────────────────────────────────────────
    // [Values] with no arguments on an enum generates every defined value.
    // DayOfWeek has 7 values, so this generates 7 tests.
    [Test]
    public void EnumShorthand([Values] DayOfWeek day)
    {
        Assert.That(Enum.IsDefined(day));
    }
}

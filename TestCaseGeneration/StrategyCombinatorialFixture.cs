using NUnit.Framework;

namespace TestCaseGeneration;

// ══════════════════════════════════════════════════════════════════════════════
// DIMENSION 2 — STRATEGY ATTRIBUTES
//
// When a test method has multiple parameters, each carrying generation
// attributes ([Values], [Range], etc.), NUnit needs to decide HOW to combine
// the values across those parameters. That decision is the strategy.
//
// Three strategies are demonstrated across three fixtures that all use
// IDENTICAL parameters so the effect of the strategy is the only variable:
//
//   StrategyCombinatorialFixture — [Combinatorial] → all combinations (default)
//   StrategySequentialFixture    — [Sequential]    → values paired by index
//   StrategyPairwiseFixture      — [Pairwise]      → minimum set, all pairs covered
//
// Parameters used in all three fixtures:
//   s : [Values("A", "B", "C")]   — 3 values
//   i : [Values(1, 2, 3)]         — 3 values
//   j : [Values(10, 20, 30)]      — 3 values
//
// Expected test counts:
//   Combinatorial : 3 × 3 × 3 = 27 tests
//   Sequential    : 3 tests  (one row per index position)
//   Pairwise      : ~9 tests (NUnit picks the minimum covering set)
// ══════════════════════════════════════════════════════════════════════════════

/// <summary>
/// [Combinatorial] — runs every possible combination of parameter values.
/// This is also the default strategy: omitting the attribute entirely produces
/// the same result. Use [Combinatorial] when you want to be explicit about intent.
/// Produces 3 × 3 × 3 = 27 tests.
/// </summary>
[TestFixture]
public class StrategyCombinatorialFixture
{
    [Test]
    [Combinatorial]
    public void Combine(
        [Values("A", "B", "C")] string s,
        [Values(1, 2, 3)] int i,
        [Values(10, 20, 30)] int j)
    {
        // The test names in the runner list every combination generated.
        // Count them — there should be 27.
        Assert.Pass($"({s}, {i}, {j})");
    }
}

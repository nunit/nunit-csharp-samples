using NUnit.Framework;

namespace TestCaseGeneration;

/// <summary>
/// [Pairwise] — generates the smallest set of combinations that still ensures
/// every pair of values (from any two parameters) appears together in at least
/// one test. This gives meaningful coverage at a fraction of the cost of full
/// Combinatorial testing — especially valuable as parameter counts grow.
///
/// With 3 parameters × 3 values each:
///   Combinatorial : 27 tests (all triples covered)
///   Pairwise      :  9 tests (all pairs covered, not all triples)
///
/// The exact combinations are chosen by NUnit's pairwise algorithm and may
/// vary between NUnit versions, but every (s,i), (s,j), and (i,j) pair
/// is guaranteed to appear at least once.
/// </summary>
[TestFixture]
public class StrategyPairwiseFixture
{
    [Test]
    [Pairwise]
    public void Combine(
        [Values("A", "B", "C")] string s,
        [Values(1, 2, 3)] int i,
        [Values(10, 20, 30)] int j)
    {
        // The test names in the runner list the chosen combinations.
        // Count them — there should be around 9, not 27.
        Assert.Pass($"({s}, {i}, {j})");
    }
}

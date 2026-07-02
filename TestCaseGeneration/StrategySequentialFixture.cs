using NUnit.Framework;

namespace TestCaseGeneration;

/// <summary>
/// [Sequential] — pairs values by their index position across parameters.
/// The first test gets the first value from each parameter, the second test
/// gets the second value, and so on. No cross-combining occurs.
/// Produces 3 tests (one per index position):
///   (A, 1, 10)
///   (B, 2, 20)
///   (C, 3, 30)
/// Compare with StrategyCombinatorialFixture which produces 27.
/// </summary>
[TestFixture]
public class StrategySequentialFixture
{
    [Test]
    [Sequential]
    public void Combine(
        [Values("A", "B", "C")] string s,
        [Values(1, 2, 3)] int i,
        [Values(10, 20, 30)] int j)
    {
        // The test names in the runner list the three combinations above.
        // Count them — there should be 3, not 27.
        Assert.Pass($"({s}, {i}, {j})");
    }
}

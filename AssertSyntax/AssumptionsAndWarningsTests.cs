using NUnit.Framework;

namespace AssertSyntax;

/// <summary>
/// Demonstrates Assume.That (assumptions) and Warn.If / Warn.Unless (warnings).
/// A failing assumption causes the test to be marked Inconclusive rather than Failed.
/// A failing warning is recorded but does not fail the test.
/// </summary>
[TestFixture]
public class AssumptionsAndWarningsTests
{
    [Test]
    public void PositiveAssumption()
    {
        // Assume.That passes — execution continues to the assertion below
        Assume.That(true);

        Assert.Pass("This will be executed because the assumption passed.");
    }

    [Test]
    public void NegativeAssumption()
    {
        // Assume.That fails — test is marked Inconclusive and stops here
        Assume.That(false);

        Assert.Fail("This will not be executed because the assumption failed.");
    }

    [Test]
    public void WarningsAreEmittedButDoNotFailTheTest()
    {
        // Both of these conditions are true, so both warnings are emitted
        Warn.If(true, "This will emit a warning");
        Warn.Unless(false, "This will emit a warning");

        // The test still passes despite the warnings
        Assert.Pass("This test passes despite the warnings.");
    }

    [Test]
    public void NoWarningsEmittedWhenConditionsAreNotMet()
    {
        // Neither condition triggers a warning
        Warn.If(false, "This will not emit a warning");
        Warn.Unless(true, "This will not emit a warning");

        Assert.Pass("No warnings were emitted.");
    }
}

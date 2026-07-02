using NUnit.Framework;

namespace AssertSyntax;

#pragma warning disable NUnit2007 // The actual value should not be a constant
#pragma warning disable NUnit2009 // The same value has been provided as both the actual and the expected argument

/// <summary>
/// Demonstrates ordering/comparison constraints: greater-than, less-than,
/// and their inclusive variants — both with constraint syntax and classic syntax.
/// </summary>
[TestFixture]
public class ComparisonTests
{
    [Test]
    public void GreaterThan()
    {
        // Constraint Syntax
        Assert.That(7, Is.GreaterThan(3));
        Assert.That(7, Is.GreaterThanOrEqualTo(3));
        Assert.That(7, Is.AtLeast(3));
        Assert.That(7, Is.GreaterThanOrEqualTo(7));
        Assert.That(7, Is.AtLeast(7));

        // Classic Syntax
        Assert.Greater(7, 3);
        Assert.GreaterOrEqual(7, 3);
        Assert.GreaterOrEqual(7, 7);
    }

    [Test]
    public void LessThan()
    {
        // Constraint Syntax
        Assert.That(3, Is.LessThan(7));
        Assert.That(3, Is.LessThanOrEqualTo(7));
        Assert.That(3, Is.AtMost(7));
        Assert.That(3, Is.LessThanOrEqualTo(3));
        Assert.That(3, Is.AtMost(3));

        // Classic syntax
        Assert.Less(3, 7);
        Assert.LessOrEqual(3, 7);
        Assert.LessOrEqual(3, 3);
    }
}

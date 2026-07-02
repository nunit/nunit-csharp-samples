using NUnit.Framework;

namespace AssertSyntax;

#pragma warning disable NUnit2007 // The actual value should not be a constant

/// <summary>
/// Demonstrates constraint operators and combinators: Is.Not negation,
/// the ! ~ &amp; | overloaded operators, and chained/complex expressions.
/// These are constraint-syntax only — no classic equivalents.
/// </summary>
[TestFixture]
public class ConstraintOperatorTests
{
    [Test]
    public void Not()
    {
        int? theAnswer = 42;

        Assert.That(theAnswer, Is.Not.Null);
        Assert.That(theAnswer, Is.Not.True);
        Assert.That(theAnswer, Is.Not.False);
        Assert.That(2.5, Is.Not.NaN);
        Assert.That(2 + 2, Is.Not.EqualTo(3));

        // Not.Not double-negation cancels out
        Assert.That(2 + 2, Is.Not.Not.EqualTo(4));

        // Triple negation
        Assert.That(2 + 2, Is.Not.Not.Not.EqualTo(5));
    }

    [Test]
    public void NotOperator()
    {
        // ! is overloaded on constraints and is equivalent to Is.Not
        Assert.That(42, !Is.Null);
    }

    [Test]
    public void AndOperator()
    {
        // & combines two constraints — both must pass
        Assert.That(7, Is.GreaterThan(5) & Is.LessThan(10));
    }

    [Test]
    public void OrOperator()
    {
        // | combines two constraints — at least one must pass
        Assert.That(3, Is.LessThan(5) | Is.GreaterThan(10));
    }

    [Test]
    public void ComplexCombinations()
    {
        int? theAnswer = 7;

        // Mixing Is.Not with & operator
        Assert.That(theAnswer, Is.Not.Null & Is.Not.LessThan(5) & Is.Not.GreaterThan(10));

        // Same using the ! overloaded operator
        Assert.That(theAnswer, !Is.Null & !Is.LessThan(5) & !Is.GreaterThan(10));
    }
}

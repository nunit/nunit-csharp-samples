using NUnit.Framework;

namespace AssertSyntax;

#pragma warning disable NUnit2007 // The actual value should not be a constant
#pragma warning disable CA1825    // Avoid zero-length array allocations

/// <summary>
/// Demonstrates simple constraint-based assertions: null checks, boolean checks,
/// NaN, and emptiness — both with constraint syntax and classic syntax.
/// </summary>
[TestFixture]
public class SimpleConstraintTests
{
    [Test]
    public void IsNull()
    {
        object nada = GetObjectAsNull();

        // Constraint Syntax
        Assert.That(nada, Is.Null);

        // Classic syntax
        Assert.IsNull(nada);
    }

    [Test]
    public void IsNotNull()
    {
        int? theAnswer = 42;

        // Constraint Syntax
        Assert.That(theAnswer, Is.Not.Null);

        // Classic syntax
        Assert.IsNotNull(42);
    }

    [Test]
    public void IsTrue()
    {
        int result = GetResultAs4();

        // Constraint Syntax
        Assert.That(result == 4, Is.True);
        Assert.That(result == 4);

        // Classic syntax
        Assert.IsTrue(GetResultAs4() == 4);
    }

    [Test]
    public void IsFalse()
    {
        // Constraint Syntax
        Assert.That(GetResultAs4() == 5, Is.False);

        // Classic syntax
        Assert.IsFalse(GetResultAs4() == 5);
    }

    [Test]
    public void IsNaN()
    {
        var d = double.NaN;
        var f = float.NaN;

        // Constraint Syntax
        Assert.That(d, Is.NaN);
        Assert.That(f, Is.NaN);

        // Classic syntax
        Assert.IsNaN(d);
        Assert.IsNaN(f);
    }

    [Test]
    public void EmptyString()
    {
        // Constraint Syntax
        Assert.That("", Is.Empty);
        Assert.That("Hello!", Is.Not.Empty);

        // Classic syntax
        Assert.IsEmpty("");
        Assert.IsNotEmpty("Hello!");
    }

    [Test]
    public void EmptyCollection()
    {
        // Constraint Syntax
        Assert.That(new bool[0], Is.Empty);
        Assert.That(new[] { 1, 2, 3 }, Is.Not.Empty);

        // Classic syntax
        Assert.IsEmpty(new bool[0]);
        Assert.IsNotEmpty(new[] { 1, 2, 3 });
    }

    private static object GetObjectAsNull() => null;
    private static int GetResultAs4() => 4;
}

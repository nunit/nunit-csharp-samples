using NUnit.Framework;

namespace AssertSyntax;

#pragma warning disable NUnit2007 // The actual value should not be a constant
#pragma warning disable NUnit2009 // The same value has been provided as both the actual and the expected argument

/// <summary>
/// Demonstrates equality assertions: exact equality for scalars and arrays,
/// and tolerance-based equality for floating-point and mixed numeric types.
/// </summary>
[TestFixture]
public class EqualityTests
{
    [Test]
    public void Equality()
    {
        var i3 = new[] { 1, 2, 3 };
        var d3 = new[] { 1.0, 2.0, 3.0 };
        var iunequal = new[] { 1, 3, 2 };

        // Constraint Syntax
        Assert.That(2 + 2, Is.EqualTo(4));
        Assert.That(2 + 2 == 4);
        Assert.That(i3, Is.EqualTo(d3));
        Assert.That(2 + 2, Is.Not.EqualTo(5));
        Assert.That(i3, Is.Not.EqualTo(iunequal));

        // Classic Syntax
        Assert.AreEqual(4, 2 + 2);
        Assert.AreEqual(i3, d3);
        Assert.AreNotEqual(5, 2 + 2);
        Assert.AreNotEqual(i3, iunequal);
    }

    [Test]
    public void EqualityWithTolerance()
    {
        // Constraint Syntax
        Assert.That(4.99d, Is.EqualTo(5.0d).Within(0.05d));
        Assert.That(4.0d, Is.Not.EqualTo(5.0d).Within(0.5d));
        Assert.That(4.99f, Is.EqualTo(5.0f).Within(0.05f));
        Assert.That(4.99m, Is.EqualTo(5.0m).Within(0.05m));
        Assert.That(3999999999u, Is.EqualTo(4000000000u).Within(5u));
        Assert.That(499, Is.EqualTo(500).Within(5));
        Assert.That(4999999999L, Is.EqualTo(5000000000L).Within(5L));
        Assert.That(5999999999ul, Is.EqualTo(6000000000ul).Within(5ul));

        // Classic syntax
        Assert.AreEqual(5.0d, 4.99d, 0.05d);
        Assert.AreEqual(5.0f, 4.99f, 0.05f);
    }

    [Test]
    public void EqualityWithTolerance_MixedFloatAndDouble()
    {
        Assert.That(2.20492d, Is.EqualTo(2.2d).Within(0.01f),
            "Double actual, Double expected, Single tolerance");
        Assert.That(2.20492d, Is.EqualTo(2.2f).Within(0.01d),
            "Double actual, Single expected, Double tolerance");
        Assert.That(2.20492d, Is.EqualTo(2.2f).Within(0.01f),
            "Double actual, Single expected, Single tolerance");
        Assert.That(2.20492f, Is.EqualTo(2.2f).Within(0.01d),
            "Single actual, Single expected, Double tolerance");
        Assert.That(2.20492f, Is.EqualTo(2.2d).Within(0.01d),
            "Single actual, Double expected, Double tolerance");
        Assert.That(2.20492f, Is.EqualTo(2.2d).Within(0.01f),
            "Single actual, Double expected, Single tolerance");
    }

    [Test]
    public void EqualityWithTolerance_MixedNumericTypes()
    {
        Assert.That(202d, Is.EqualTo(200d).Within(2),
            "Double actual, Double expected, int tolerance");
        Assert.That(4.87m, Is.EqualTo(5).Within(.25),
            "Decimal actual, int expected, Double tolerance");
        Assert.That(4.87m, Is.EqualTo(5ul).Within(1),
            "Decimal actual, ulong expected, int tolerance");
        Assert.That(487, Is.EqualTo(500).Within(25),
            "int actual, int expected, int tolerance");
        Assert.That(487u, Is.EqualTo(500).Within(25),
            "uint actual, int expected, int tolerance");
        Assert.That(487L, Is.EqualTo(500).Within(25),
            "long actual, int expected, int tolerance");
        Assert.That(487ul, Is.EqualTo(500).Within(25),
            "ulong actual, int expected, int tolerance");
    }
}

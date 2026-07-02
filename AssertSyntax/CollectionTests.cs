using System.Collections;
using NUnit.Framework;

namespace AssertSyntax;

#pragma warning disable NUnit2049 // Consider using Assert.That(...) instead of CollectionAssert(...)

/// <summary>
/// Demonstrates collection constraints: universal/existential/negative quantifiers,
/// membership, equivalence, and subset checks — both with constraint syntax
/// and classic CollectionAssert syntax.
/// </summary>
[TestFixture]
public class CollectionTests
{
    [Test]
    public void AllItems()
    {
        var ints = new object[] { 1, 2, 3, 4 };
        var doubles = new object[] { 0.99, 2.1, 3.0, 4.05 };
        var strings = new object[] { "abc", "bad", "cab", "bad", "dad" };

        // Constraint Syntax
        Assert.That(ints, Is.All.Not.Null);
        Assert.That(ints, Has.None.Null);
        Assert.That(ints, Is.All.InstanceOf(typeof(int)));
        Assert.That(ints, Has.All.InstanceOf(typeof(int)));
        Assert.That(strings, Is.All.InstanceOf(typeof(string)));
        Assert.That(strings, Has.All.InstanceOf(typeof(string)));
        Assert.That(ints, Is.Unique);
        Assert.That(strings, Is.Not.Unique);
        Assert.That(ints, Is.All.GreaterThan(0));
        Assert.That(ints, Has.All.GreaterThan(0));
        Assert.That(ints, Has.None.LessThanOrEqualTo(0));
        Assert.That(strings, Is.All.Contains("a"));
        Assert.That(strings, Has.All.Contains("a"));
        Assert.That(strings, Has.Some.StartsWith("ba"));
        Assert.That(strings, Has.Some.Property("Length").EqualTo(3));
        Assert.That(strings, Has.Some.StartsWith("BA").IgnoreCase);
        Assert.That(doubles, Has.Some.EqualTo(1.0).Within(.05));

        // Classic syntax
        CollectionAssert.AllItemsAreNotNull(ints);
        CollectionAssert.AllItemsAreInstancesOfType(ints, typeof(int));
        CollectionAssert.AllItemsAreInstancesOfType(strings, typeof(string));
        CollectionAssert.AllItemsAreUnique(ints);
    }

    [Test]
    public void SomeItems()
    {
        var mixed = new object[] { 1, 2, "3", null, "four", 100 };
        var strings = new object[] { "abc", "bad", "cab", "bad", "dad" };

        // Constraint Syntax only — no classic equivalent
        Assert.That(mixed, Has.Some.Null);
        Assert.That(mixed, Has.Some.InstanceOf(typeof(int)));
        Assert.That(mixed, Has.Some.InstanceOf(typeof(string)));
        Assert.That(strings, Has.Some.StartsWith("ba"));
        Assert.That(strings, Has.Some.Not.StartsWith("ba"));
    }

    [Test]
    public void NoItems()
    {
        var ints = new object[] { 1, 2, 3, 4, 5 };
        var strings = new object[] { "abc", "bad", "cab", "bad", "dad" };

        // Constraint Syntax only — no classic equivalent
        Assert.That(ints, Has.None.Null);
        Assert.That(ints, Has.None.InstanceOf(typeof(string)));
        Assert.That(ints, Has.None.GreaterThan(99));
        Assert.That(strings, Has.None.StartsWith("qu"));
    }

    [Test]
    public void Contains()
    {
        var iarray = new[] { 1, 2, 3 };
        var sarray = new[] { "a", "b", "c" };

        // Constraint Syntax
        Assert.That(iarray, Has.Member(3));
        Assert.That(sarray, Has.Member("b"));
        Assert.That(sarray, Has.No.Member("x"));
        Assert.That(iarray, Has.Member(1.0d));   // Contains uses NUnit equality
        Assert.That(iarray, Has.Some.EqualTo(3));
        Assert.That(sarray, Has.Some.EqualTo("b"));
        Assert.That(sarray, Has.None.EqualTo("x"));
        Assert.That(iarray, Has.All.LessThan(10));
        Assert.That(sarray, Has.All.Length.EqualTo(1));
        Assert.That(sarray, Has.None.Property("Length").GreaterThan(3));

        // Classic syntax
        Assert.Contains(3, iarray);
        Assert.Contains("b", sarray);
        CollectionAssert.Contains(iarray, 3);
        CollectionAssert.Contains(sarray, "b");
        CollectionAssert.DoesNotContain(sarray, "x");
        CollectionAssert.Contains(iarray, 1.0d);  // Contains uses NUnit equality
    }

    [Test]
    public void Equivalence()
    {
        var ints1to5 = new[] { 1, 2, 3, 4, 5 };
        var twothrees = new[] { 1, 2, 3, 3, 4, 5 };
        var twofours = new[] { 1, 2, 3, 4, 4, 5 };

        // Constraint Syntax
        Assert.That(new[] { 2, 1, 4, 3, 5 }, Is.EquivalentTo(ints1to5));
        Assert.That(new[] { 2, 2, 4, 3, 5 }, Is.Not.EquivalentTo(ints1to5));
        Assert.That(new[] { 2, 4, 3, 5 }, Is.Not.EquivalentTo(ints1to5));
        Assert.That(new[] { 2, 2, 1, 1, 4, 3, 5 }, Is.Not.EquivalentTo(ints1to5));

        // Classic syntax
        CollectionAssert.AreEquivalent(new[] { 2, 1, 4, 3, 5 }, ints1to5);
        CollectionAssert.AreNotEquivalent(new[] { 2, 2, 4, 3, 5 }, ints1to5);
        CollectionAssert.AreNotEquivalent(new[] { 2, 4, 3, 5 }, ints1to5);
        CollectionAssert.AreNotEquivalent(new[] { 2, 2, 1, 1, 4, 3, 5 }, ints1to5);
        CollectionAssert.AreNotEquivalent(twothrees, twofours);
    }

    [Test]
    public void Subset()
    {
        var ints1to5 = new[] { 1, 2, 3, 4, 5 };

        // Constraint Syntax
        Assert.That(new[] { 1, 3, 5 }, Is.SubsetOf(ints1to5));
        Assert.That(new[] { 1, 2, 3, 4, 5 }, Is.SubsetOf(ints1to5));
        Assert.That(new[] { 2, 4, 6 }, Is.Not.SubsetOf(ints1to5));

        // Classic syntax
        CollectionAssert.IsSubsetOf(new[] { 1, 3, 5 }, ints1to5);
        CollectionAssert.IsSubsetOf(new[] { 1, 2, 3, 4, 5 }, ints1to5);
        CollectionAssert.IsNotSubsetOf(new[] { 2, 4, 6 }, ints1to5);
        CollectionAssert.IsNotSubsetOf(new[] { 1, 2, 2, 2, 5 }, ints1to5);
    }
}

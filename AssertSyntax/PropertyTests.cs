using System.Collections;
using NUnit.Framework;

namespace AssertSyntax;

#pragma warning disable NUnit2007 // The actual value should not be a constant

/// <summary>
/// Demonstrates property constraints: asserting the existence and value of properties
/// on objects and collection elements using Has.Property, Has.Length, and Has.Count.
/// No classic syntax equivalent — constraint syntax only.
/// </summary>
[TestFixture]
public class PropertyTests
{
    [Test]
    public void PropertyConstraints()
    {
        string[] array = ["abc", "bca", "xyz", "qrs"];
        string[] array2 = ["a", "ab", "abc"];
        var list = new ArrayList(array);

        // Existence of a property
        Assert.That(list, Has.Property("Count"));
        Assert.That(list, Has.No.Property("Length"));

        // Property value on a single object
        Assert.That("Hello", Has.Length.EqualTo(5));
        Assert.That("Hello", Has.Length.LessThan(10));
        Assert.That("Hello", Has.Property("Length").EqualTo(5));
        Assert.That("Hello", Has.Property("Length").GreaterThan(3));

        // Property value on an array (Length is a property of the array itself)
        Assert.That(array, Has.Property("Length").EqualTo(4));
        Assert.That(array, Has.Length.EqualTo(4));
        Assert.That(array, Has.Property("Length").LessThan(10));

        // Universal quantifier — every element satisfies the property constraint
        Assert.That(array, Has.All.Property("Length").EqualTo(3));
        Assert.That(array, Has.All.Length.EqualTo(3));
        Assert.That(array, Is.All.Length.EqualTo(3));
        Assert.That(array, Is.All.Property("Length").EqualTo(3));

        // Existential quantifier — at least one element satisfies the constraint
        Assert.That(array2, Has.Some.Property("Length").EqualTo(2));
        Assert.That(array2, Has.Some.Length.EqualTo(2));
        Assert.That(array2, Has.Some.Property("Length").GreaterThan(2));

        // Negative quantifier — no element satisfies the constraint
        Assert.That(array2, Is.Not.Property("Length").EqualTo(4));
        Assert.That(array2, Is.Not.Length.EqualTo(4));
        Assert.That(array2, Has.No.Property("Length").GreaterThan(3));

        // Count on a list
        Assert.That(list, Has.Count.EqualTo(4));
    }
}

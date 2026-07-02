using NUnit.Framework;

namespace AssertSyntax;

#pragma warning disable NUnit2007 // The actual value should not be a constant
#pragma warning disable NUnit2010 // Use EqualConstraint for better assertion messages in case of failure

/// <summary>
/// Demonstrates type-related constraints: exact type matching, instance-of checks,
/// and assignability — both with constraint syntax and classic syntax.
/// </summary>
[TestFixture]
public class TypeConstraintTests
{
    [Test]
    public void ExactType()
    {
        // Constraint Syntax
        Assert.That("Hello", Is.TypeOf(typeof(string)));
        Assert.That("Hello", Is.Not.TypeOf(typeof(int)));

        // Classic syntax workarounds (no direct equivalent for TypeOf)
        Assert.AreEqual(typeof(string), "Hello".GetType());
        Assert.AreEqual("System.String", "Hello".GetType().FullName);
        Assert.AreNotEqual(typeof(int), "Hello".GetType());
        Assert.AreNotEqual("System.Int32", "Hello".GetType().FullName);
    }

    [Test]
    public void InstanceOf()
    {
        // Constraint Syntax
        Assert.That("Hello", Is.InstanceOf(typeof(string)));
        Assert.That(5, Is.Not.InstanceOf(typeof(string)));

        // Classic syntax
        Assert.IsInstanceOf(typeof(string), "Hello");
        Assert.IsNotInstanceOf(typeof(string), 5);
    }

    [Test]
    public void AssignableFrom()
    {
        // Constraint Syntax
        Assert.That("Hello", Is.AssignableFrom(typeof(string)));
        Assert.That(5, Is.Not.AssignableFrom(typeof(string)));

        // Classic syntax
        Assert.IsAssignableFrom(typeof(string), "Hello");
        Assert.IsNotAssignableFrom(typeof(string), 5);
    }
}

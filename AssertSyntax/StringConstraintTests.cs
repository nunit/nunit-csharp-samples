using NUnit.Framework;

namespace AssertSyntax;

#pragma warning disable NUnit2008 // The IgnoreCase modifier should only be used for string or char arguments
#pragma warning disable NUnit2048 // Consider using Assert.That(...) instead of StringAssert(...)

/// <summary>
/// Demonstrates string-specific constraints: substring, prefix, suffix,
/// case-insensitive equality, and regular expressions — both with constraint
/// syntax and classic StringAssert syntax.
/// </summary>
[TestFixture]
public class StringConstraintTests
{
    [Test]
    public void Substring()
    {
        var phrase = "Hello World!";
        var array = new[] { "abc", "bad", "dba" };

        // Constraint Syntax
        Assert.That(phrase, Does.Contain("World"));
        Assert.That(phrase, Does.Not.Contain("goodbye"));
        Assert.That(phrase, Does.Contain("WORLD").IgnoreCase);
        Assert.That(phrase, Does.Not.Contain("BYE").IgnoreCase);
        Assert.That(array, Is.All.Contains("b"));

        // Classic Syntax
        StringAssert.Contains("World", phrase);
    }

    [Test]
    public void StartsWith()
    {
        var phrase = "Hello World!";
        var greetings = new[] { "Hello!", "Hi!", "Hola!" };

        // Constraint Syntax
        Assert.That(phrase, Does.StartWith("Hello"));
        Assert.That(phrase, Does.Not.StartWith("Hi!"));
        Assert.That(phrase, Does.StartWith("HeLLo").IgnoreCase);
        Assert.That(phrase, Does.Not.StartWith("HI").IgnoreCase);
        Assert.That(greetings, Is.All.StartsWith("h").IgnoreCase);

        // Classic syntax
        StringAssert.StartsWith("Hello", phrase);
    }

    [Test]
    public void EndsWith()
    {
        var phrase = "Hello World!";
        var greetings = new[] { "Hello!", "Hi!", "Hola!" };

        // Constraint Syntax
        Assert.That(phrase, Does.EndWith("!"));
        Assert.That(phrase, Does.Not.EndWith("?"));
        Assert.That(phrase, Does.EndWith("WORLD!").IgnoreCase);
        Assert.That(greetings, Is.All.EndsWith("!"));

        // Classic Syntax
        StringAssert.EndsWith("!", phrase);
    }

    [Test]
    public void EqualIgnoringCase()
    {
        var phrase = "Hello World!";

        // Constraint Syntax
        Assert.That(phrase, Is.EqualTo("hello world!").IgnoreCase);
        Assert.That(phrase, Is.Not.EqualTo("goodbye world!").IgnoreCase);
        Assert.That(new[] { "Hello", "World" },
            Is.EqualTo(new object[] { "HELLO", "WORLD" }).IgnoreCase);
        Assert.That(new[] { "HELLO", "Hello", "hello" },
            Is.All.EqualTo("hello").IgnoreCase);

        // Classic syntax
        StringAssert.AreEqualIgnoringCase("hello world!", phrase);
    }

    [Test]
    public void RegularExpression()
    {
        var phrase = "Now is the time for all good men to come to the aid of their country.";
        var quotes = new[] { "Never say never", "It's never too late", "Nevermore!" };

        // Constraint Syntax
        Assert.That(phrase, Does.Match("all good men"));
        Assert.That(phrase, Does.Match("Now.*come"));
        Assert.That(phrase, Does.Not.Match("all.*men.*good"));
        Assert.That(phrase, Does.Match("ALL").IgnoreCase);
        Assert.That(quotes, Is.All.Matches("never").IgnoreCase);

        // Classic syntax
        StringAssert.IsMatch("all good men", phrase);
        StringAssert.IsMatch("Now.*come", phrase);
    }
}

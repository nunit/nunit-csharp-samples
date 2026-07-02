using System.Collections.Generic;
using NUnit.Framework;

namespace TestCaseGeneration;

/// <summary>
/// Demonstrates [ValueSource] — like [Values] but pulls the data from a named
/// method, field, or property rather than embedding it inline on the attribute.
/// Useful when the data set is large, shared, or needs to be computed.
/// </summary>
[TestFixture]
public class ValueSourceFixture
{
    // ── From a static field ───────────────────────────────────────────────────
    // The simplest form: a static array on the same class.
    // Generates one test per element: 4 tests.
    static readonly int[] EvenNumbers = [2, 4, 6, 8];

    [Test]
    public void FromStaticField([ValueSource(nameof(EvenNumbers))] int n)
    {
        Assert.That(n % 2, Is.EqualTo(0));
    }

    // ── From a static method ──────────────────────────────────────────────────
    // Any static method returning IEnumerable<T> works as a source.
    // Using yield return keeps the data close to the test without allocating a list.
    // Generates 3 tests.
    static IEnumerable<string> Greetings()
    {
        yield return "Hello";
        yield return "Hi";
        yield return "Hola";
    }

    [Test]
    public void FromStaticMethod([ValueSource(nameof(Greetings))] string greeting)
    {
        Assert.That(greeting, Is.Not.Empty);
    }

    // ── From a static property ────────────────────────────────────────────────
    // Properties work exactly like methods; useful when the source doubles as
    // a public API or is shared with production code.
    // Generates 5 tests.
    static IEnumerable<int> Primes
    {
        get
        {
            yield return 2;
            yield return 3;
            yield return 5;
            yield return 7;
            yield return 11;
        }
    }

    [Test]
    public void FromStaticProperty([ValueSource(nameof(Primes))] int prime)
    {
        Assert.That(prime, Is.GreaterThan(1));
    }

    // ── From an external class ────────────────────────────────────────────────
    // Passing typeof(...) lets the data live in a completely separate class,
    // making it reusable across multiple fixtures.
    // Generates one test per value in ExternalStringSource.Values.
    [Test]
    public void FromExternalClass(
        [ValueSource(typeof(ExternalStringSource), nameof(ExternalStringSource.Values))] string s)
    {
        Assert.That(s, Is.Not.Null);
    }
}

/// <summary>
/// A standalone source class — its members can be referenced from any fixture.
/// </summary>
public static class ExternalStringSource
{
    public static string[] Values = ["alpha", "beta", "gamma", "delta"];
}

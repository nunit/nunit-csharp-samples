# NUnit C# Samples 🧪

A collection of practical, ready-to-run NUnit examples in C# — great for learning NUnit, exploring its features, or grabbing a snippet when you need one.

All projects target **.NET 10** and use the latest NUnit packages.

---

## 📁 Projects

### 💰 money
A classic example straight from the NUnit heritage: a `Money` and `MoneyBag` implementation with a full set of tests. A great starting point for seeing how NUnit tests are structured around a real domain model.

---

### ✅ syntax
Side-by-side examples of NUnit's **constraint-based syntax** (`Assert.That`) and the **classic syntax** (`Assert.AreEqual`, `StringAssert`, `CollectionAssert`, etc.), so you can see both styles and pick what works for you.

The examples are split into focused fixtures by topic:

| File | What it covers |
|---|---|
| `SimpleConstraintTests` | Null checks, `Is.True/False`, `Is.NaN`, `Is.Empty` |
| `TypeConstraintTests` | `Is.TypeOf`, `Is.InstanceOf`, `Is.AssignableFrom` |
| `StringConstraintTests` | `Does.Contain`, `Does.StartWith`, `Does.EndWith`, `IgnoreCase`, regex |
| `EqualityTests` | `Is.EqualTo`, tolerance for floats, decimals and mixed numeric types |
| `ComparisonTests` | `Is.GreaterThan`, `Is.LessThan` and their inclusive variants |
| `CollectionTests` | `Has.All`, `Has.Some`, `Has.None`, equivalence, subset |
| `PropertyTests` | `Has.Property`, `Has.Length`, `Has.Count` |
| `ConstraintOperatorTests` | `Is.Not`, `!`, `&`, `\|` operators and complex combinations |
| `AssumptionsAndWarningsTests` | `Assume.That`, `Warn.If`, `Warn.Unless` |

---

### 📊 DataDrivenTests
Shows how to drive tests from data rather than writing one test per case. Covers all the main approaches:

| File | What it covers |
|---|---|
| `DataDrivenTestFixture` | `[TestFixture(value)]` — parameterised fixtures with constructor arguments |
| `GenericTestFixture` | `[TestFixture(typeof(T))]` — generic fixtures with type arguments |
| `TestCasesFixture` | `[TestCase]` — inline data, `ExpectedResult`, `TestName`, `Description` |
| `TestCaseSourceFixture` | `[TestCaseSource]` — static methods, fields, properties, `TestCaseData`, external source classes |

---

### 🎲 TestCaseGeneration
Shows how to generate test cases automatically from parameter-level attributes, so NUnit builds the combinations for you rather than you spelling out each case by hand.

The attributes fall into two independent dimensions:

**Generation attributes** — define what values each parameter receives:

| File | What it covers |
|---|---|
| `ValuesFixture` | `[Values]` — inline values, bool and enum shorthands, null |
| `ValueSourceFixture` | `[ValueSource]` — static field, method, property, external class |
| `RangeFixture` | `[Range]` — int, double, float, descending, multi-parameter |
| `RandomFixture` | `[Random]` — unbounded, bounded, double, multi-parameter |

**Strategy attributes** — define how NUnit combines values across multiple parameters. All four fixtures below use identical parameters so the effect of the strategy is the only variable:

| File | Strategy | Tests generated |
|---|---|---|
| `StrategyCombinatorialFixture` | `[Combinatorial]` | 27 — every combination (also the default) |
| `StrategySequentialFixture` | `[Sequential]` | 3 — values paired by index |
| `StrategyPairwiseFixture` | `[Pairwise]` | ~9 — minimum set covering all value pairs |

---

### 🔌 ExpectedExceptionExample
An example of **extending NUnit with a custom attribute**. Shows how to implement a `[ExpectedException]` attribute — something that existed in NUnit 2 but was removed in NUnit 3. Walking through it teaches you how NUnit's attribute and command pipeline works.

> 💡 For everyday use, `Assert.Throws<T>()` is the right tool. This example is about understanding how to build your own test attributes, not about replacing `Assert.Throws`.

---

### 🔌 TimeoutRetryAttributeExample
Another example of **extending NUnit with a custom attribute**. Shows how to implement a `[TimeoutRetry]` attribute that automatically retries a test — but only when it times out, not when it fails for other reasons. Useful as a pattern for handling flaky tests caused by slow or unreliable network connections.

> 💡 Three of the tests in this project are marked `[Explicit]` because they are intentionally designed to fail — they demonstrate what happens when retries are exhausted, or when a failure is due to an assertion or exception rather than a timeout.

---

## 🚀 Running the samples

Each project is a self-contained test project. To run **all samples at once**, just run from the repo root:

```bash
dotnet test
```

To run a single project, specify its folder:

```bash
dotnet test AssertSyntax
dotnet test DataDrivenTests
```

Or open the repo in Visual Studio or Rider and run the tests from the IDE's test explorer.

using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Money
{
    /// <summary>
	/// Tests Money
	/// </summary>
	/// 
	[TestFixture]
    public class MoneyTest
    {
        private Money f12CHF;
        private Money f14CHF;
        private Money f7USD;
        private Money f21USD;

        private MoneyBag fMB1;
        private MoneyBag fMB2;

        /// <summary>
        /// Initializes Money test objects
        /// </summary>
        /// 
        [SetUp]
        protected void SetUp()
        {
            f12CHF = new Money(12, "CHF");
            f14CHF = new Money(14, "CHF");
            f7USD = new Money(7, "USD");
            f21USD = new Money(21, "USD");

            fMB1 = new MoneyBag(f12CHF, f7USD);
            fMB2 = new MoneyBag(f14CHF, f21USD);
        }

        /// <summary>
        /// Assert that Moneybags multiply correctly
        /// </summary>
        /// 
        [Test]
        public void BagMultiply()
        {
            // {[12 CHF][7 USD]} *2 == {[24 CHF][14 USD]}
            Money[] bag = { new Money(24, "CHF"), new Money(14, "USD") };
            var expected = new MoneyBag(bag);
            Assert.That(fMB1.Multiply(2), Is.EqualTo(expected));
            Assert.That(fMB1.Multiply(1), Is.EqualTo(fMB1));
            ClassicAssert.IsTrue(fMB1.Multiply(0).IsZero);
        }

        /// <summary>
        /// Assert that Moneybags negate(positive to negative values) correctly
        /// </summary>
        /// 
        [Test]
        public void BagNegate()
        {
            // {[12 CHF][7 USD]} negate == {[-12 CHF][-7 USD]}
            Money[] bag = { new Money(-12, "CHF"), new Money(-7, "USD") };
            var expected = new MoneyBag(bag);
            Assert.That(fMB1.Negate(), Is.EqualTo(expected));
        }

        /// <summary>
        /// Assert that adding currency to Moneybags happens correctly
        /// </summary>
        /// 
        [Test]
        public void BagSimpleAdd()
        {
            // {[12 CHF][7 USD]} + [14 CHF] == {[26 CHF][7 USD]}
            Money[] bag = { new Money(26, "CHF"), new Money(7, "USD") };
            var expected = new MoneyBag(bag);
            Assert.That(fMB1.Add(f14CHF), Is.EqualTo(expected));
        }

        /// <summary>
        /// Assert that subtracting currency to Moneybags happens correctly
        /// </summary>
        /// 
        [Test]
        public void BagSubtract()
        {
            // {[12 CHF][7 USD]} - {[14 CHF][21 USD] == {[-2 CHF][-14 USD]}
            Money[] bag = { new Money(-2, "CHF"), new Money(-14, "USD") };
            var expected = new MoneyBag(bag);
            Assert.That(fMB1.Subtract(fMB2), Is.EqualTo(expected));
        }

        /// <summary>
        /// Assert that adding multiple currencies to Moneybags in one statement happens correctly
        /// </summary>
        /// 
        [Test]
        public void BagSumAdd()
        {
            // {[12 CHF][7 USD]} + {[14 CHF][21 USD]} == {[26 CHF][28 USD]}
            Money[] bag = { new Money(26, "CHF"), new Money(28, "USD") };
            var expected = new MoneyBag(bag);
            Assert.That(fMB1.Add(fMB2), Is.EqualTo(expected));
        }

        /// <summary>
        /// Assert that Moneybags hold zero value after adding zero value
        /// </summary>
        /// 
        [Test]
        public void IsZero()
        {
            ClassicAssert.IsTrue(fMB1.Subtract(fMB1).IsZero);

            Money[] bag = { new Money(0, "CHF"), new Money(0, "USD") };
            ClassicAssert.IsTrue(new MoneyBag(bag).IsZero);
        }

        /// <summary>
        /// Assert that a new bag is the same as adding value to an existing bag
        /// </summary>
        /// 
        [Test]
        public void MixedSimpleAdd()
        {
            // [12 CHF] + [7 USD] == {[12 CHF][7 USD]}
            Money[] bag = { f12CHF, f7USD };
            var expected = new MoneyBag(bag);
            Assert.That(f12CHF.Add(f7USD), Is.EqualTo(expected));
        }

        /// <summary>
        /// Assert that MoneyBag.Equals() works correctly
        /// </summary>
        /// 
        [Test]
        public void MoneyBagEquals()
        {
            //NOTE: Normally we use Assert.AreEqual to test whether two
            // objects are equal. But here we are testing the MoneyBag.Equals()
            // method itself, so using AreEqual would not serve the purpose.
            ClassicAssert.IsFalse(fMB1.Equals(null));

            ClassicAssert.IsTrue(fMB1.Equals(fMB1));
            var equal = new MoneyBag(new Money(12, "CHF"), new Money(7, "USD"));
            ClassicAssert.IsTrue(fMB1.Equals(equal));
            ClassicAssert.IsTrue(!fMB1.Equals(f12CHF));
            ClassicAssert.IsTrue(!f12CHF.Equals(fMB1));
            ClassicAssert.IsTrue(!fMB1.Equals(fMB2));
        }

        /// <summary>
        /// Assert that the hash of a new bag is the same as 
        /// the hash of an existing bag with added value
        /// </summary>
        /// 
        [Test]
        public void MoneyBagHash()
        {
            var equal = new MoneyBag(new Money(12, "CHF"), new Money(7, "USD"));
            Assert.That(equal.GetHashCode(), Is.EqualTo(fMB1.GetHashCode()));
        }

        /// <summary>
        /// Assert that Money.Equals() works correctly
        /// </summary>
        /// 
        [Test]
        public void MoneyEquals()
        {
            //NOTE: Normally we use Assert.AreEqual to test whether two
            // objects are equal. But here we are testing the MoneyBag.Equals()
            // method itself, so using AreEqual would not serve the purpose.
            ClassicAssert.IsFalse(f12CHF.Equals(null));
            var equalMoney = new Money(12, "CHF");
            ClassicAssert.IsTrue(f12CHF.Equals(f12CHF));
            ClassicAssert.IsTrue(f12CHF.Equals(equalMoney));
            ClassicAssert.IsFalse(f12CHF.Equals(f14CHF));
        }

        /// <summary>
        /// Assert that the hash of new Money is the same as 
        /// the hash of initialized Money
        /// </summary>
        /// 
        [Test]
        public void MoneyHash()
        {
            ClassicAssert.IsFalse(f12CHF.Equals(null));
            var equal = new Money(12, "CHF");
            Assert.That(equal.GetHashCode(), Is.EqualTo(f12CHF.GetHashCode()));
        }

        /// <summary>
        /// Assert that adding multiple small values is the same as adding one big value
        /// </summary>
        /// 
        [Test]
        public void Normalize()
        {
            Money[] bag = { new Money(26, "CHF"), new Money(28, "CHF"), new Money(6, "CHF") };
            var moneyBag = new MoneyBag(bag);
            Money[] expected = { new Money(60, "CHF") };
            // note: expected is still a MoneyBag
            var expectedBag = new MoneyBag(expected);
            Assert.That(moneyBag, Is.EqualTo(expectedBag));
        }

        /// <summary>
        /// Assert that removing a value is the same as not having such a value
        /// </summary>
        /// 
        [Test]
        public void Normalize2()
        {
            // {[12 CHF][7 USD]} - [12 CHF] == [7 USD]
            var expected = new Money(7, "USD");
            Assert.That(fMB1.Subtract(f12CHF), Is.EqualTo(expected));
        }

        /// <summary>
        /// Assert that removing multiple values works correctly
        /// </summary>
        /// 
        [Test]
        public void Normalize3()
        {
            // {[12 CHF][7 USD]} - {[12 CHF][3 USD]} == [4 USD]
            Money[] s1 = { new Money(12, "CHF"), new Money(3, "USD") };
            var ms1 = new MoneyBag(s1);
            var expected = new Money(4, "USD");
            Assert.That(fMB1.Subtract(ms1), Is.EqualTo(expected));
        }

        /// <summary>
        /// Assert that if value is subtracted from 0, the result will be negative.
        /// </summary>
        /// 
        [Test]
        public void Normalize4()
        {
            // [12 CHF] - {[12 CHF][3 USD]} == [-3 USD]
            Money[] s1 = { new Money(12, "CHF"), new Money(3, "USD") };
            var ms1 = new MoneyBag(s1);
            var expected = new Money(-3, "USD");
            Assert.That(f12CHF.Subtract(ms1), Is.EqualTo(expected));
        }

        /// <summary>
        /// Assert that Money.ToString() function works correctly
        /// </summary>
        /// 
        [Test]
        public void Print()
        {
            Assert.That(f12CHF.ToString(), Is.EqualTo("[12 CHF]"));
        }

        /// <summary>
        /// Assert that adding more value to Money happens correctly
        /// </summary>
        /// 
        [Test]
        public void SimpleAdd()
        {
            // [12 CHF] + [14 CHF] == [26 CHF]
            var expected = new Money(26, "CHF");
            Assert.That(f12CHF.Add(f14CHF), Is.EqualTo(expected));
        }

        /// <summary>
        /// Assert that adding multiple currencies to Moneybags happens correctly
        /// </summary>
        /// 
        [Test]
        public void SimpleBagAdd()
        {
            // [14 CHF] + {[12 CHF][7 USD]} == {[26 CHF][7 USD]}
            Money[] bag = { new Money(26, "CHF"), new Money(7, "USD") };
            var expected = new MoneyBag(bag);
            Assert.That(f14CHF.Add(fMB1), Is.EqualTo(expected));
        }

        /// <summary>
        /// Assert that multiplying currency in Money happens correctly
        /// </summary>
        /// 
        [Test]
        public void SimpleMultiply()
        {
            // [14 CHF] *2 == [28 CHF]
            var expected = new Money(28, "CHF");
            Assert.That(f14CHF.Multiply(2), Is.EqualTo(expected));
        }

        /// <summary>
        /// Assert that negating(positive to negative values) currency in Money happens correctly
        /// </summary>
        /// 
        [Test]
        public void SimpleNegate()
        {
            // [14 CHF] negate == [-14 CHF]
            var expected = new Money(-14, "CHF");
            Assert.That(f14CHF.Negate(), Is.EqualTo(expected));
        }

        /// <summary>
        /// Assert that removing currency from Money happens correctly
        /// </summary>
        /// 
        [Test]
        public void SimpleSubtract()
        {
            // [14 CHF] - [12 CHF] == [2 CHF]
            var expected = new Money(2, "CHF");
            Assert.That(f14CHF.Subtract(f12CHF), Is.EqualTo(expected));
        }
    }
}

using System;
using NUnit.Framework;

namespace ExpectedExceptionExample
{
    [TestFixture]
    public class ExpectedExceptionTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void HandlesArgumentExceptionAsType()
        {
            throw new ArgumentException();
        }

        [Test]
        public void HandlesArgumentExceptionWithNewSyntax()
        {
            Assert.Throws<ArgumentException>(() => throw new ArgumentException());
        }
    }
}
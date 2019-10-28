using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;

namespace TimeoutRetryAttributeExample
{
    [TestFixture]
    public class TimeoutRetryAttributeTests
    {
        readonly Dictionary<string, int> retryCount = new Dictionary<string, int>()
        {
            { "RetryIsFiredDueToTimeout", 0 },
            { "RetryIsNotFiredDueToTimeout", 0 },
            { "RetryIsNotFiredDueToFailedAssertion", 0 },
            { "RetryIsNotFiredDueToException", 0 }
        };

        [Test]
        [MaxTime(1000)]
        [TimeoutRetry(3)]
        public void RetryIsFiredDueToTimeout()
        {
            TestContext.Out.WriteLine($"Running test for the {++retryCount["RetryIsFiredDueToTimeout"]} time");
            Thread.Sleep(3000);
        }

        [Test]
        [MaxTime(3000)]
        [TimeoutRetry(5)]
        public void RetryIsNotFiredDueToLackOfTimeout()
        {
            TestContext.Out.WriteLine($"Running test for the {++retryCount["RetryIsNotFiredDueToTimeout"]} time");
            Thread.Sleep(1000);
        }

        [Test]
        [MaxTime(1000)]
        [TimeoutRetry(10)]
        public void RetryIsNotFiredDueToFailedAssertion()
        {
            TestContext.Out.WriteLine($"Running test for the {++retryCount["RetryIsNotFiredDueToFailedAssertion"]} time");
            Thread.Sleep(3000);
            Assert.Fail("I failed");
        }

        [Test]
        [MaxTime(1000)]
        [TimeoutRetry(2)]
        public void RetryIsNotFiredDueToException()
        {
            TestContext.Out.WriteLine($"Running test for the {++retryCount["RetryIsNotFiredDueToException"]} time");
            Thread.Sleep(3000);
            throw new Exception("Exception thrown");
        }
    }
}

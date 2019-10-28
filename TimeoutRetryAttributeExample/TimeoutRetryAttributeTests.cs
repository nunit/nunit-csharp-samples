using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;

namespace TimeoutRetryAttributeExample
{
    [TestFixture]
    public class TimeoutRetryAttributeTests
    {
        // Retry counters for corresponding test methods
        readonly Dictionary<string, int> retryCount = new Dictionary<string, int>()
        {
            { "RetryIsFiredDueToTimeout", 0 },
            { "RetryIsNotFiredDueToTimeout", 0 },
            { "RetryIsNotFiredDueToFailedAssertion", 0 },
            { "RetryIsNotFiredDueToException", 0 },
            { "RetryIsFiredDueToTimeoutAndLimitNotReached", 0 }
        };

        // This test should execute 3 times and fail due to reached retry number limit
        [Test]
        [MaxTime(1000)]
        [TimeoutRetry(3)]
        public void RetryIsFiredDueToTimeout()
        {
            TestContext.Out.WriteLine($"Running test for the {++retryCount["RetryIsFiredDueToTimeout"]} time");
            Thread.Sleep(3000); // Wait more than MaxTime threshold
        }

        // This test should not retry at all and pass within first execution
        [Test]
        [MaxTime(3000)]
        [TimeoutRetry(5)]
        public void RetryIsNotFiredDueToLackOfTimeout()
        {
            TestContext.Out.WriteLine($"Running test for the {++retryCount["RetryIsNotFiredDueToTimeout"]} time");
            Thread.Sleep(1000); // Wait less than MaxTime threshold
        }

        // This test should not retry due to failed assertion despite having reached the MaxTime threshold
        [Test]
        [MaxTime(1000)]
        [TimeoutRetry(10)]
        public void RetryIsNotFiredDueToFailedAssertion()
        {
            TestContext.Out.WriteLine($"Running test for the {++retryCount["RetryIsNotFiredDueToFailedAssertion"]} time");
            Thread.Sleep(3000); // Wait more than MaxTime threshold
            Assert.Fail("I failed"); // Introduce assertion failure
        }

        // This test should not retry due to thrown exception despite having reached the MaxTime threshold
        [Test]
        [MaxTime(1000)]
        [TimeoutRetry(2)]
        public void RetryIsNotFiredDueToException()
        {
            TestContext.Out.WriteLine($"Running test for the {++retryCount["RetryIsNotFiredDueToException"]} time");
            Thread.Sleep(3000); // Wait more than MaxTime threshold
            throw new Exception("Exception thrown"); // Throw exception
        }

        // This test should execute 3 times and fail 2 times due to reached retry number limit, then pass on the 3 run
        [Test]
        [MaxTime(2000)]
        [TimeoutRetry(3)]
        public void RetryIsFiredDueToTimeoutAndLimitNotReached()
        {
            TestContext.Out.WriteLine($"Running test for the {++retryCount["RetryIsFiredDueToTimeoutAndLimitNotReached"]} time");
            TestContext.Out.WriteLine(retryCount["RetryIsFiredDueToTimeoutAndLimitNotReached"]);
            if (retryCount["RetryIsFiredDueToTimeoutAndLimitNotReached"] < 3) // For 1 & 2 retry force timeout
            {
                Thread.Sleep(3000); // Wait more than MaxTime threshold
            }
            else
            {
                Thread.Sleep(1000); // Wait less than MaxTime threshold
            }
            Assert.True(true); // Add valid assertion
        }
    }
}

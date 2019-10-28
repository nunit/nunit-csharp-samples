using System;
using System.Text.RegularExpressions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Commands;

namespace TimeoutRetryAttributeExample
{
    /// <summary>
    /// Specifies that a test method should be rerun on timeout set by MaxTime attribute up to the specified 
    /// maximum number of times. Failure will be reported on assertion fail or exception
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class TimeoutRetryAttribute : PropertyAttribute, IWrapSetUpTearDown
    {
        private int _tryCount;

        public TimeoutRetryAttribute(int tryCount) : base(tryCount)
        {
            _tryCount = tryCount;
        }

        #region IWrapSetUpTearDown Members

        public TestCommand Wrap(TestCommand command)
        {
            return new RetryCommand(command, _tryCount);
        }

        #endregion

        #region Nested RetryCommand Class

        public class RetryCommand : DelegatingTestCommand
        {
            private int _tryCount;

            public RetryCommand(TestCommand innerCommand, int tryCount)
                : base(innerCommand)
            {
                _tryCount = tryCount;
            }

            public override TestResult Execute(TestExecutionContext context)
            {
                // Check for attribute dependencies at [Test] level or parent when [TestCase]/[TestCaseSource] is used to generate test cases
                if (!context.CurrentTest.Properties.Keys.Contains(PropertyNames.MaxTime) && !context.CurrentTest.Parent.Properties.Keys.Contains(PropertyNames.MaxTime))
                {
                    throw new NullReferenceException($"{PropertyNames.MaxTime} attribute must be set along with TimeoutRetry");
                }

                int count = _tryCount;

                while (count-- > 0)
                {
                    context.CurrentResult = innerCommand.Execute(context);

                    // Skip retry only with passed assertions or if failure is different than timeout
                    if (context.CurrentResult.ResultState != ResultState.Failure || !Regex.IsMatch(context.CurrentResult.Message, "Elapsed time of [0-9.,]*ms exceeds maximum of [0-9]*ms")) // NUnit.Framework.Internal.Commands.MaxTimeCommand
                        break;

                    // Clear result for retry
                    if (count > 0)
                        context.CurrentResult = context.CurrentTest.MakeTestResult();
                }

                return context.CurrentResult;
            }
        }

        #endregion
    }
}
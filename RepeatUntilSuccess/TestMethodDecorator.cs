using System.Collections;
using System.Collections.Specialized;

namespace NUnit.Core.Extensions
{
    /// <summary>
    /// A base class for any addin that will decorate a TestMethod.
    /// </summary>
    public abstract class TestMethodDecorator : TestMethod
    {
        protected TestMethod Test;

        protected TestMethodDecorator( TestMethod test )
            : base( test.Method )
        {
            // Update all of this object's properties with the
            // values of the wrapped TestMethod.

            BuilderException = test.BuilderException;
            Categories = new ArrayList( test.Categories );
            Description = test.Description;
            ExceptionProcessor = test.ExceptionProcessor;
            IgnoreReason = test.IgnoreReason;
            Parent = test.Parent;
            if ( test.Properties != null )
            {
                Properties = new ListDictionary();
                foreach ( DictionaryEntry entry in test.Properties )
                {
                    Properties.Add( entry.Key, entry.Value );
                }
            }
            RunState = test.RunState;
            Test = test;
            TestName.TestID = test.TestName.TestID;
            TestName.FullName = test.TestName.FullName;
        }

        public override int TestCount
        {
            get
            {
                return Test.TestCount;
            }
        }

        public override TestResult RunTest()
        {
            // The Fixture property is set at runtime by NUnit, but
            // only on this object, not on the wrapped Test.
            // Therefore we must update the wrapped Test's Fixture.
            Test.Fixture = Fixture;

            return base.RunTest();
        }
    }
}

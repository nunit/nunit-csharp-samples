namespace NUnit.Core.Extensions
{
    /// <summary>
    /// RepeatUntilSuccess is used on a test method to specify that it
    /// should be executed multiple times. If a repetition succeeds,
    /// the remaining ones are not run and success is reported. If all
    /// repetitions fail, the last failure is reported.
    /// </summary>
    public sealed class RepeatUntilSuccessTestMethod : TestMethodDecorator
    {
        private readonly int _count;

        public RepeatUntilSuccessTestMethod( TestMethod testMethod, int count )
            : base( testMethod )
        {
            _count = count;
        }

        public override TestResult RunTest()
        {
            TestResult result = null;
            for ( int i = 0; i < _count; i++ )
            {
                result = base.RunTest();
                if ( result.IsSuccess )
                {
                    return result;
                }
            }

            if( result == null )
            {
                result = new TestResult( this );
                result.Invalid( "Could not execute test." );
                return result;
            }

            result.Failure( "All repetitions failed.", "RepeatUntilSuccessTestMethod::RunTest" );
            return result;
        }
    }
}
using System.Reflection;
using NUnit.Core.Extensibility;

namespace NUnit.Core.Extensions
{
    [NUnitAddin( Name = "RepeatUntilSuccess Addin", Description = "Adds support for the RepeatUntilSuccess attribute." )]
    public class SuitTestAddin : IAddin, ITestDecorator
    {
        #region IAddin Members

        public bool Install( IExtensionHost host )
        {
            IExtensionPoint decorators = host.GetExtensionPoint( "TestDecorators" );

            if ( decorators == null )
                return false;

            decorators.Install( this );
            return true;
        }

        #endregion

        #region ITestDecorator Members

        public Test Decorate( Test test, MemberInfo member )
        {
            var testMethod = test as TestMethod;
            if ( testMethod != null )
            {
                var attr = Reflect.GetAttribute( member, "NUnit.Core.Extensions.RepeatUntilSuccessAttribute", false );
                if ( attr != null )
                {
                    var count = (int)Reflect.GetPropertyValue( attr, "Count", BindingFlags.Public | BindingFlags.Instance );
                    return new RepeatUntilSuccessTestMethod( testMethod, count );
                }
            }

            return test;
        }

        #endregion
    }
}
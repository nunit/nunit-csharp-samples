using System;

namespace NUnit.Core.Extensions
{
    [AttributeUsage( AttributeTargets.Method, AllowMultiple = false, Inherited = false )]
    public class RepeatUntilSuccessAttribute : Attribute
    {
        public int Count { get; private set; }

        public RepeatUntilSuccessAttribute( int count )
        {
            Count = count;
        }
    }
}

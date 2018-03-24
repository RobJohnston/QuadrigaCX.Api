using System;
using System.Collections.Generic;
//using System.Runtime.Serialization;

namespace QuadrigaCX.Api
{
    /// <summary>
    /// Represents a collection of <see cref="QuadrigaException"/>s.
    /// </summary>
    public class QuadrigaAggregateException : AggregateException
    {
        //public QuadrigaAggregateException()
        //{
        //}

        //public QuadrigaAggregateException(string message)
        //    : base(message)
        //{
        //}

        //public QuadrigaAggregateException(Exception[] innerExceptions) 
        //    : base(innerExceptions)
        //{
        //}

        public QuadrigaAggregateException(IEnumerable<Exception> innerExceptions) 
            : base(innerExceptions)
        {
        }

        //public QuadrigaAggregateException(string message, Exception innerException) 
        //    : base(message, innerException)
        //{
        //}

        //public QuadrigaAggregateException(SerializationInfo info, StreamingContext context) 
        //    : base(info, context)
        //{
        //}

        //public QuadrigaAggregateException(string message, Exception[] innerExceptions) 
        //    : base(message, innerExceptions)
        //{
        //}

        //public QuadrigaAggregateException(string message, IEnumerable<Exception> innerExceptions) 
        //    : base(message, innerExceptions)
        //{
        //}
    }
}

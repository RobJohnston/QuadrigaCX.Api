using System;
//using System.Runtime.Serialization;

namespace QuadrigaCX.Api
{
    /// <summary>
    /// Represents an error raised by the QuadrigaCX API, each with a unique code and a short message.
    /// </summary>
    /// <seealso cref="QuadrigaAggregateException"/>
    public class QuadrigaException : Exception
    {
        //public QuadrigaException()
        //{
        //}

        public QuadrigaException(string message)
            : base(message)
        {
        }

        //public QuadrigaException(string message, Exception inner)
        //    : base(message, inner)
        //{
        //}

        //public QuadrigaException(SerializationInfo info, StreamingContext context)
        //    : base(info, context)
        //{
        //}

        public int Code { get; internal set; }
    }
}

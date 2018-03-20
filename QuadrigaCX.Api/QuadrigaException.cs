using System;
using System.Runtime.Serialization;

namespace QuadrigaCX.Api
{
    public class QuadrigaException : Exception
    {
        public QuadrigaException()
        {
        }

        public QuadrigaException(string message)
            : base(message)
        {
        }

        public QuadrigaException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public QuadrigaException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public int Code { get; internal set; }
    }
}

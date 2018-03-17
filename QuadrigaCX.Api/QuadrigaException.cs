using System;

namespace QuadrigaCX.Api
{
    /// <summary>
    /// Represents errors that occur at the QuadrigaCX API level.
    /// </summary>
    public class QuadrigaException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QuadrigaException"/> class.
        /// </summary>
        public QuadrigaException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QuadrigaException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public QuadrigaException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QuadrigaException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="inner">The exception that is the cause of the current exception.</param>
        public QuadrigaException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QuadrigaException"/> class.
        /// </summary>
        /// <param name="error">Error returned by QuadrigaCX API.</param>
        public QuadrigaException(ErrorString error)
        {
            Error = error;
        }

        public ErrorString Error { get; }
    }

    /// <summary>
    /// Represents the error returned from the API.
    /// </summary>
    /// <example>
    /// {"error":{"code":101,"message":"Invalid API Code or Invalid Signature"}}
    /// </example>
    public class ErrorString
    {
        public int Code { get; set; }

        public string Message { get; set; }
    }
}

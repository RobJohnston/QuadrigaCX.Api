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
        /// <param name="error">Error returned by QuadrigaCX API.</param>
        /// <param name="message">Message of the exception.</param>
        public QuadrigaException(ErrorString error, string message)
            : base(message)
        {
            Error = error;
        }

        /// <summary>
        /// Gets the errors returned by QuadrigaCX API.
        /// </summary>
        public ErrorString Error { get; }
    }
}

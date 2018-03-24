using System;
using System.Collections.Generic;
using System.Text;

namespace QuadrigaCX.Api
{
    /// <summary>
    /// The exception that is thrown when the signature cannot be created.
    /// 
    /// The signature is a HMAC_SHA256 encrypted string created using a concatenation of 
    /// a nonce, the client id, and the API key, using the API Secret as key.
    /// 
    /// The nonce is created internally using <c>DateTime.UtcNow.Ticks</c>.  The other values
    /// are set using a constructor.
    /// </summary>
    /// <remarks>
    /// <c>SignatureException</c> is used in cases where the failure to create a signature
    /// is caused by invalid field values, possibly from the use of the wrong constructor.
    /// 
    /// The signature is only required for calling private functions. Therefore the use of
    /// the default constructor to call a private function will always raise this exception.
    /// </remarks>
    public class SignatureException : Exception
    {
        //public SignatureException()
        //{
        //}

        public SignatureException(string message)
            : base(message)
        {
        }

        //public SignatureException(string message, Exception inner)
        //    : base(message, inner)
        //{
        //}

        //public SignatureException(SerializationInfo info, StreamingContext context)
        //    : base(info, context)
        //{
        //}
    }
}

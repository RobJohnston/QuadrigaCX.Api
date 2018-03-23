using Newtonsoft.Json;

namespace QuadrigaCX.Api.Models
{
    /// <summary>
    /// Represents an error returned from the API.
    /// </summary>
    public class QuadrigaError
    {
        /// <summary>
        /// Gets or sets errors of a request.
        /// </summary>
        [JsonProperty("error")]
        public ErrorString Error { get; set; }
    }

    /// <summary>
    /// Represents the properties found in the JSON error object returned by the API.
    /// </summary>
    /// <seealso cref="QuadrigaError"/>
    public class ErrorString
    {
        /// <summary>
        /// The error code.
        /// </summary>
        [JsonProperty("code")]
        public int Code { get; set; }

        /// <summary>
        /// A short message describing the error.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}

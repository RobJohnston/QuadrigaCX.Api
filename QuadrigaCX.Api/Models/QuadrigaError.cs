using Newtonsoft.Json;

namespace QuadrigaCX.Api.Models
{
    /// <summary>
    /// Represents the error returned from the API.
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
    /// The error is composed of a Code and a Message.
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

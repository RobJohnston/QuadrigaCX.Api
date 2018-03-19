using Newtonsoft.Json;

namespace QuadrigaCX.Api
{
    /// <summary>
    /// Represents the response from QuadrigaCX API which could be the expected result or an error.
    /// </summary>
    public abstract class QuadrigaResponse
    {
        /// <summary>
        /// Gets or sets errors of a request.
        /// </summary>
        [JsonProperty("error")]
        public ErrorString Error { get; set; }
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

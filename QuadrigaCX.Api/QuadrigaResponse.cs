using Newtonsoft.Json;

namespace QuadrigaCX.Api
{
    /// <summary>
    /// Wrapper around the response from QuadrigaCX API which could be the expected result or an error.
    /// </summary>
    /// <typeparam name="T">Type of result.</typeparam>
    public class QuadrigaResponse<T>
    {
        /// <summary>
        /// Gets or sets errors of a request.
        /// </summary>
        [JsonProperty("error")]
        public ErrorString Error { get; set; } // Nullable

        /// <summary>
        /// Gets or sets the result of a request.
        /// </summary>
        public T Result { get; set; } // Nullable

        /// <summary>
        /// Gets or sets the raw Json result of a request.
        /// </summary>
        //public string RawJson { get; set; }
    }
}

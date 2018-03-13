using System.Net.Http;

namespace QuadrigaCX.Api
{
    /// <summary>
    /// Request context.
    /// </summary>
    public class RequestContext
    {
        /// <summary>
        /// Gets or sets the HTTP request object.
        /// </summary>
        public HttpRequestMessage HttpRequest { get; set; }
    }

    /// <summary>
    /// Response context.
    /// </summary>
    public class ResponseContext
    {
        /// <summary>
        /// Gets or sets the HTTP response object.
        /// </summary>
        public HttpResponseMessage HttpResponse { get; set; }
    }
}

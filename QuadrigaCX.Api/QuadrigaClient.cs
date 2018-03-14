using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QuadrigaCX.Api
{
    /// <summary>
    /// A strongly typed thread-safe async HTTP client for QuadrigaCX bitcoin exchange API v2.
    /// <para>https://www.quadrigacx.com/api_info</para>
    /// </summary>
    public partial class QuadrigaClient : IDisposable
    {
        private int _clientId;
        private string _key;
        private string _secret;
        private string _url;
        private string _version;

        private static readonly CultureInfo _culture = CultureInfo.InvariantCulture;
        private readonly HttpClient _httpClient = new HttpClient();

        internal static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() }
        };

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="QuadrigaClient"/> class.
        /// </summary>
        public QuadrigaClient()
        {
            _url = "https://api.quadrigacx.com";
            _version = "v2";
            _httpClient.BaseAddress = new Uri(string.Format("{0}/{1}/", _url, _version));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QuadrigaClient"/> class using an API key and secret.
        /// </summary>
        /// <param name="clientId">The client ID.</param>
        /// <param name="apiKey">The API key.</param>
        /// <param name="apiSecret">The API secret.</param>
        public QuadrigaClient(int clientId, string apiKey, string apiSecret) : this()
        {
            _clientId = clientId;
            _key = apiKey ?? "";
            _secret = apiSecret ?? "";
        }

        #endregion

        /// <summary>
        /// Sends a public GET request to the QuadrigaCX API as an asynchronous operation.
        /// </summary>
        /// <typeparam name="T">Type of data contained in the response.</typeparam>
        /// <param name="requestUrl">The relative url the request is sent to.</param>
        /// <param name="args">Optional argument passed as querystring parameters.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="requestUrl"/> is <c>null</c>.</exception>
        /// <exception cref="HttpRequestException">There was a problem with the HTTP request.</exception>
        /// <exception cref="QuadrigaResponse">There was a problem with the QuadrigaCX API call.</exception>
        public async Task<T> QueryPublic<T>(string requestUrl, Dictionary<string, string> args = null)
        {
            if (requestUrl == null)
                throw new ArgumentNullException(nameof(requestUrl));

            args = args ?? new Dictionary<string, string>(0);

            // Setup request.
            string urlEncodedArgs = UrlEncode(args);

            string address = string.Format("{0}/{1}/{2}?{3}", _url, _version, requestUrl, urlEncodedArgs);

            var req = new HttpRequestMessage(HttpMethod.Get, address);

            // Send request and deserialize response.
            return await SendRequest<T>(req).ConfigureAwait(false);
        }

        /// <summary>
        /// Sends a private POST request to the QuadrigaCX API as an asynchronous operation.
        /// </summary>
        /// <typeparam name="T">Type of data contained in the response.</typeparam>
        /// <param name="requestUrl">The relative url the request is sent to.</param>
        /// <param name="args">Optional arguments passed as form data.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="requestUrl"/> is <c>null</c>.</exception>
        /// <exception cref="HttpRequestException">There was a problem with the HTTP request.</exception>
        /// <exception cref="QuadrigaException">There was a problem with the QuadrigaCX API call.</exception>
        public async Task<T> QueryPrivate<T>(string requestUrl, Dictionary<string, string> args = null)
        {
            if (requestUrl == null)
                throw new ArgumentNullException(nameof(requestUrl));

            // Add 3 additional args.
            args = args ?? new Dictionary<string, string>(3);

            args["key"] = _key;

            string nonce = null;

            if (GetNonce != null)
            {
                nonce = (await GetNonce().ConfigureAwait(false)).ToString(_culture);
                args["nonce"] = nonce;
            }

            args["signature"] = GenerateApiSignature(nonce, _clientId.ToString(_culture), _key);

            // Setup request.
            string urlEncodedArgs = UrlEncode(args);

            var req = new HttpRequestMessage(HttpMethod.Post, requestUrl)
            {
                Content = new StringContent(urlEncodedArgs, Encoding.UTF8, "application/x-www-form-urlencoded")
            };

            // Send request and deserialize response.
            return await SendRequest<T>(req).ConfigureAwait(false);
        }

        /// <summary>
        /// Releases the unmanaged resources and disposes of the managed resources used by the
        /// underlying <see cref="HttpClient"/>.
        /// </summary>
        public void Dispose() => _httpClient.Dispose();

        #region Private methods

        private string GenerateApiSignature(string nonce, string client, string key)
        {
            string signature;
            var secretBytes = Encoding.ASCII.GetBytes(_secret);

            using (var hmacsha256 = new HMACSHA256(secretBytes))
            {
                var hashBytes = hmacsha256.ComputeHash(Encoding.ASCII.GetBytes(nonce + client + key));
                signature = BitConverter.ToString(hashBytes).Replace("-", "").ToLower(_culture);
            }

            return signature;
        }

        /// <summary>
        /// Gets or sets the getter function for a nonce (default = <c>DateTime.UtcNow.Ticks</c>).
        /// <para>API expects a unique value for each request.</para>
        /// </summary>
        private Func<Task<long>> GetNonce { get; set; } = () => Task.FromResult(DateTime.UtcNow.Ticks);

        private async Task<T> SendRequest<T>(HttpRequestMessage req)
        {
            var reqCtx = new RequestContext
            {
                HttpRequest = req
            };

            // Perform the HTTP request.
            HttpResponseMessage res = await _httpClient.SendAsync(reqCtx.HttpRequest).ConfigureAwait(false);

            var resCtx = new ResponseContext
            {
                HttpResponse = res
            };

            // Throw for HTTP-level error.
            resCtx.HttpResponse.EnsureSuccessStatusCode();

            // Deserialize response.
            string jsonContent = await resCtx.HttpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

            // REVIEW:  See if there's a better way of deserializing either an object or an array of objects.
            var response = new QuadrigaResponse<T>();

            if (typeof(T).BaseType != typeof(Array))
            {
                response.Result = JsonConvert.DeserializeObject<T>(jsonContent, JsonSettings);
            }

            //response.RawJson = jsonContent;

            // Throw API-level error.
            if (response.Error != null)
            {
                throw new QuadrigaException(response.Error, "There was a problem with a response from QuadrigaCX.");
            }

            response.Result = JsonConvert.DeserializeObject<T>(jsonContent, JsonSettings);

            // Throw API-level error.
            if (response.Error != null)
            {
                throw new QuadrigaException(response.Error, "There was a problem with a response from QuadrigaCX.");
            }

            return response.Result;
        }

        private static string UrlEncode(Dictionary<string, string> args) => string.Join(
            "&",
            args.Where(x => x.Value != null).Select(x => x.Key + "=" + WebUtility.UrlEncode(x.Value))
        );

        #endregion
    }
}

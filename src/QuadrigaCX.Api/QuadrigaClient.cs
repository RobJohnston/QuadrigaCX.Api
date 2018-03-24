using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using QuadrigaCX.Api.Models;
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
    /// A strongly typed thread-safe async HTTP client for the Quadriga Coin eXchange API v2.
    /// 
    /// The QuadrigaCX API allows you to integrate the QuadrigaCX trading platform with third party applications, 
    /// such as trading applications, charting programs, point of sale systems, and much more.
    /// <para>https://www.quadrigacx.com/api_info</para>
    /// </summary>
    /// <remarks>
    /// QuadrigaCX notations:
    ///   <para>Major denotes any of the Cryptocurrencies such as Bitcoin (BTC) or any other cryptocurrency which is added to
    ///   the QuadrigaCX trading platform in the future.</para>
    ///   <para>Minor denotes fiat currencies such as Canadian Dollars (CAD), etc.</para>
    ///   <para>An order book is always referred to in the API as "Major_Minor". For example: "btc_cad".</para>
    /// </remarks>
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
        /// Initializes a new instance of the <see cref="QuadrigaClient"/> class for calling public functions.
        /// </summary>
        public QuadrigaClient()
        {
            _url = "https://api.quadrigacx.com";
            _version = "v2";
            _httpClient.BaseAddress = new Uri(string.Format("{0}/{1}/", _url, _version));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QuadrigaClient"/> class for calling private functions.
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
        /// <param name="requestUrl">The relative URL the request is sent to.</param>
        /// <param name="args">Optional arguments passed as querystring parameters.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <remarks>The <paramref name="requestUrl"/> is relative to https://api.quadrigacx.com/v2/</remarks>
        /// <exception cref="ArgumentNullException"><paramref name="requestUrl"/> is <c>null</c>.</exception>
        /// <exception cref="HttpRequestException">There was a problem with the HTTP request.</exception>
        /// <exception cref="QuadrigaException">There was a problem with the QuadrigaCX API call.</exception>
        public async Task<T> QueryPublicAsync<T>(string requestUrl, Dictionary<string, string> args = null)
        {
            if (requestUrl == null)
                throw new ArgumentNullException(nameof(requestUrl));

            args = args ?? new Dictionary<string, string>(0);

            // Setup request.
            var urlEncodedArgs = UrlEncode(args);

            var address = string.Format("{0}/{1}/{2}?{3}", _url, _version, requestUrl, urlEncodedArgs);

            var req = new HttpRequestMessage(HttpMethod.Get, address);

            // Send request and deserialize response.
            return await SendRequestAsync<T>(req).ConfigureAwait(false);
        }

        /// <summary>
        /// Sends a private POST request to the QuadrigaCX API as an asynchronous operation.
        /// </summary>
        /// <typeparam name="T">Type of data contained in the response.</typeparam>
        /// <param name="requestUrl">The relative URL the request is sent to.</param>
        /// <param name="args">Optional arguments passed as form data.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <remarks>The <paramref name="requestUrl"/> is relative to https://api.quadrigacx.com/v2/</remarks>
        /// <exception cref="ArgumentNullException"><paramref name="requestUrl"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">Api key is <c>null</c>. Do you use the correct constructor?</exception>
        /// <exception cref="HttpRequestException">There was a problem with the HTTP request.</exception>
        /// <exception cref="QuadrigaException">There was a problem with the QuadrigaCX API call.</exception>
        public async Task<T> QueryPrivateAsync<T>(string requestUrl, Dictionary<string, string> args = null)
        {
            if (requestUrl == null)
                throw new ArgumentNullException(nameof(requestUrl));

            if (_clientId <= 0)
                throw new SignatureException(string.Format("The client ID is invalid.  Value '{0}'.", _clientId));

            if (string.IsNullOrWhiteSpace(_key))
                throw new SignatureException("The API key cannot be null.");

            if (string.IsNullOrWhiteSpace(_secret))
                throw new SignatureException("The API secret cannot be null.");

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
            var urlEncodedArgs = UrlEncode(args);

            var req = new HttpRequestMessage(HttpMethod.Post, requestUrl)
            {
                Content = new StringContent(urlEncodedArgs, Encoding.UTF8, "application/x-www-form-urlencoded")
            };

            // Send request and deserialize response.
            return await SendRequestAsync<T>(req).ConfigureAwait(false);
        }

        /// <summary>
        /// Releases the unmanaged resources and disposes of the managed resources used by the
        /// underlying <see cref="HttpClient"/>.
        /// </summary>
        public void Dispose() => _httpClient.Dispose();

        #region Private methods

        /// <summary>
        /// The signature has to be created using a concatenation of the nonce, your client id, the API key
        /// and using the API Secret as key.
        /// </summary>
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

        private async Task<T> SendRequestAsync<T>(HttpRequestMessage req)
        {
            var reqCtx = new RequestContext
            {
                HttpRequest = req
            };

            // Perform the HTTP request.
            var res = await _httpClient.SendAsync(reqCtx.HttpRequest).ConfigureAwait(false);

            var resCtx = new ResponseContext
            {
                HttpResponse = res
            };

            // Throw for HTTP-level error.
            resCtx.HttpResponse.EnsureSuccessStatusCode();

            // Deserialize response.
            var jsonContent = await resCtx.HttpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            var response = JsonConvert.DeserializeObject<T>(jsonContent, JsonSettings);

            ProcessError(jsonContent);

            return response;
        }

        private static string UrlEncode(Dictionary<string, string> args) => string.Join(
            "&",
            args.Where(x => x.Value != null).Select(x => x.Key + "=" + WebUtility.UrlEncode(x.Value))
        );

        /// <summary>
        /// Determine if the API returned and error and throw it if it did.
        /// </summary>
        private void ProcessError(string jsonContent)
        {
            var token = JToken.Parse(jsonContent);

            if (token is JObject)
            {
                var response = token.ToObject<QuadrigaError>();

                if (response.Error != null)
                {
                    var exception = new QuadrigaException(response.Error.Message)
                    {
                        Code = response.Error.Code
                    };

                    throw exception;
                }
            }
            else if (token is JArray)
            {
                IEnumerable<QuadrigaError> responses = token.ToObject<List<QuadrigaError>>();
                var exceptions = new List<Exception>();

                foreach(var response in responses)
                {
                    var exception = new QuadrigaException(response.Error.Message)
                    {
                        Code = response.Error.Code
                    };

                    exceptions.Add(exception);
                }

                if (exceptions.Count == 1)
                    throw exceptions.First();
                else if (exceptions.Count > 1)
                    throw new QuadrigaAggregateException(exceptions);
            }
        }

        #endregion
    }
}

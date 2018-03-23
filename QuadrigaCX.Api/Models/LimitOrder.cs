using Newtonsoft.Json;
using System;

namespace QuadrigaCX.Api.Models
{
    /// <summary>
    /// Represents a limit order returned from <see cref="QuadrigaClient.Buy(decimal, decimal, string)"/> 
    /// or <see cref="QuadrigaClient.Sell(decimal, decimal, string)"/>.
    /// </summary>
    /// <seealso cref="MarketOrder"/>
    /// <seealso cref="OpenOrder"/>
    /// <seealso cref="Order"/>
    public class LimitOrder
    {
        /// <summary>
        /// Order ID.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Date and time.
        /// </summary>
        [JsonProperty("datetime ")]
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Buy or sell (0 - buy; 1 - sell).
        /// </summary>
        [JsonProperty("type")]
        public OrderType Type { get; set; }

        /// <summary>
        /// Price.
        /// </summary>
        [JsonProperty("price")]
        public decimal Price { get; set; }

        /// <summary>
        /// Amount.
        /// </summary>
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
    }
}

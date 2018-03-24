using Newtonsoft.Json;

namespace QuadrigaCX.Api.Models
{
    /// <summary>
    /// Represents a market trade returned from <see cref="QuadrigaClient.GetTransactionsAsync(string, string)"/>.
    /// </summary>
    /// <seealso cref="UserTransaction"/>
    public class Transaction
    {
        /// <summary>
        /// Amount of trade.
        /// </summary>
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Unix timestamp date and time.
        /// </summary>
        [JsonProperty("date")]
        public double Date { get; set; }

        /// <summary>
        /// Price of trade.
        /// </summary>
        [JsonProperty("price")]
        public decimal Price { get; set; }

        /// <summary>
        /// Transaction ID.
        /// </summary>
        [JsonProperty("tid")]
        public int Tid { get; set; }

        /// <summary>
        /// The trade side indicates the maker order side (maker order is the order that was open on the order book).
        /// </summary>
        [JsonProperty("side")]
        public string Side { get; set; }
    }
}

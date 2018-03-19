using Newtonsoft.Json;

namespace QuadrigaCX.Api.Models
{
    /// <summary>
    /// Trading information from a book.
    /// </summary>
    public class TickerInfo : QuadrigaResponse
    {
        /// <summary>
        /// Last 24 hours price high.
        /// </summary>
        [JsonProperty("high")]
        public decimal High;

        /// <summary>
        /// Last price
        /// </summary>
        [JsonProperty("last")]
        public decimal Last;

        /// <summary>
        /// Unix timestamp.
        /// </summary>
        [JsonProperty("timestamp")]
        public double Timestamp;

        /// <summary>
        /// Last 24 hours volume.
        /// </summary>
        [JsonProperty("volume")]
        public decimal Volume;

        /// <summary>
        /// Last 24 hours volume weighted average price.
        /// </summary>
        [JsonProperty("vwap")]
        public decimal Vwap;

        /// <summary>
        /// Last 24 hours price low.
        /// </summary>
        [JsonProperty("low")]
        public decimal Low;

        /// <summary>
        /// Lowest sell order.
        /// </summary>
        [JsonProperty("ask")]
        public decimal Ask;

        /// <summary>
        /// Highest buy order.
        /// </summary>
        [JsonProperty("bid")]
        public decimal Bid;
    }
}

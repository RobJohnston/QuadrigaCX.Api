using Newtonsoft.Json;
using System;

namespace QuadrigaCX.Api.Models
{
    public class UserTransaction : QuadrigaResponse
    {
        /// <summary>
        /// Date and time.
        /// </summary>
        [JsonProperty("datetime")]
        public DateTime Datetime { get; set; }

        /// <summary>
        /// Unique identifier (only for trades).
        /// </summary>
        [JsonProperty("id")]
        public int? Id { get; set; }

        /// <summary>
        ///  Transaction type (0 - deposit; 1 - withdrawal; 2 - trade).
        /// </summary>
        [JsonProperty("type")]
        public UserTransactionType Type { get; set; }

        /// <summary>
        /// Deposit or withdrawal method.
        /// </summary>
        [JsonProperty("method")]
        public string Method { get; set; }

        #region Major and minor currency codes.

        /// <summary>
        /// Canadian dollar.
        /// </summary>
        [JsonProperty("cad")]
        public decimal? CAD { get; set; }

        /// <summary>
        /// United States dollar.
        /// </summary>
        [JsonProperty("usd")]
        public decimal? USD { get; set; }

        /// <summary>
        /// Bitcoin.
        /// </summary>
        [JsonProperty("btc")]
        public decimal? BTC { get; set; }

        /// <summary>
        /// Litecoin.
        /// </summary>
        [JsonProperty("ltc")]
        public decimal? LTC { get; set; }

        /// <summary>
        /// Bitcoin Cash.
        /// </summary>
        [JsonProperty("bch")]
        public decimal? BCH { get; set; }

        /// <summary>
        /// Bitcoin Gold.
        /// </summary>
        [JsonProperty("btg")]
        public decimal? BTG { get; set; }

        /// <summary>
        /// Ether.
        /// </summary>
        [JsonProperty("eth")]
        public decimal? ETH { get; set; }

        #region Undocumented currencies returned from account balance.

        /// <summary>
        /// Ethereum Classic.
        /// </summary>
        [JsonProperty("etc")]
        public decimal? ETC { get; set; }

        /// <summary>
        /// Gold.
        /// </summary>
        [JsonProperty("xau")]
        public decimal? XAU { get; set; }

        #endregion

        #endregion

        /// <summary>
        /// A 64 character long hexadecimal string representing the order that was fully or partially filled (only for trades).
        /// </summary>
        [JsonProperty("order_id")]
        public string OrderId { get; set; }

        /// <summary>
        /// Transaction fee.
        /// </summary>
        [JsonProperty("fee")]
        public decimal? Fee { get; set; }

        /// <summary>
        /// Rate per btc (only for trades).
        /// </summary>
        [JsonProperty("rate")]
        public decimal? Rate { get; set; }
    }

    public enum UserTransactionType
    {
        Deposit = 0,
        Withdrawal = 1,
        MarketTrade = 2
    }
}

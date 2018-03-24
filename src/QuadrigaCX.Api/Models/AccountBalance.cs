using Newtonsoft.Json;
using System.Collections.Generic;

namespace QuadrigaCX.Api.Models
{
    /// <summary>
    /// Represents the account balance returned from <see cref="QuadrigaClient.GetAccountBalanceAsync"/>.
    /// </summary>
    public class AccountBalance
    {
        #region BTC

        /// <summary>
        /// BTC available for trading
        /// </summary>
        [JsonProperty("btc_available")]
        public decimal BTC_Available { get; set; }

        /// <summary>
        /// BTC reserved in open orders.
        /// </summary>
        [JsonProperty("btc_reserved")]
        public decimal BTC_Reserved { get; set; }

        /// <summary>
        /// BTC balance
        /// </summary>
        [JsonProperty("btc_balance")]
        public decimal BTC_Balance { get; set; }

        #endregion

        #region BCH

        [JsonProperty("bch_available")]
        public decimal BCH_Available { get; set; }

        [JsonProperty("bch_reserved")]
        public decimal BCH_Reserved { get; set; }

        [JsonProperty("bch_balance")]
        public decimal BCH_Balance { get; set; }

        #endregion

        #region BTG

        [JsonProperty("btg_available")]
        public decimal BTG_Available { get; set; }

        [JsonProperty("btg_reserved")]
        public decimal BTG_Reserved { get; set; }

        [JsonProperty("btg_balance")]
        public decimal BTG_Balance { get; set; }

        #endregion

        #region ETH

        [JsonProperty("eth_available")]
        public decimal ETH_Available { get; set; }

        [JsonProperty("eth_reserved")]
        public decimal ETH_Reserved { get; set; }

        [JsonProperty("eth_balance")]
        public decimal ETH_Balance { get; set; }

        #endregion

        #region LTC

        [JsonProperty("ltc_available")]
        public decimal LTC_Available { get; set; }

        [JsonProperty("ltc_reserved")]
        public decimal LTC_Reserved { get; set; }

        [JsonProperty("ltc_balance")]
        public decimal LTC_Balance { get; set; }

        #endregion

        #region CAD

        /// <summary>
        /// CAD available for trading.
        /// </summary>
        [JsonProperty("cad_available")]
        public decimal CAD_Available { get; set; }

        /// <summary>
        /// CAD reserved in open orders.
        /// </summary>
        [JsonProperty("cad_reserved")]
        public decimal CAD_Reserved { get; set; }

        /// <summary>
        /// CAD balance.
        /// </summary>
        [JsonProperty("cad_balance")]
        public decimal CAD_Balance { get; set; }

        #endregion

        #region USD

        [JsonProperty("usd_available")]
        public decimal USD_Available { get; set; }

        [JsonProperty("usd_reserved")]
        public decimal USD_Reserved { get; set; }

        [JsonProperty("usd_balance")]
        public decimal USD_Balance { get; set; }

        #endregion

        #region Undocumented currencies

        //// ETC = Ethereum Classic 
        //[JsonProperty("etc_available")]
        //public decimal EtcAvailable { get; set; }
        //[JsonProperty("etc_reserved")]
        //public decimal EtcReserved { get; set; }
        //[JsonProperty("etc_balance")]
        //public decimal EtcBalance { get; set; }

        //// XAU = Gold
        //[JsonProperty("xau_available")]
        //public decimal XauAvailable { get; set; }
        //[JsonProperty("xau_reserved")]
        //public decimal XauReserved { get; set; }
        //[JsonProperty("xau_balance")]
        //public decimal XauBalance { get; set; }

        #endregion

        /// <summary>
        /// Customer trading fee.
        /// </summary>
        [JsonProperty("fee")]
        public decimal Fee { get; set; }

        /// <summary>
        /// A set of book/fee pairs.
        /// </summary>
        [JsonProperty("fees")]
        public Dictionary<string, decimal> Fees { get; set; }
    }
}
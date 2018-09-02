using Newtonsoft.Json;
using QuadrigaCX.Api.Utils;
using System;
using System.Collections.Generic;

namespace QuadrigaCX.Api.Models
{
    /// <summary>
    /// Represents a market order returned from <see cref="QuadrigaClient.Buy(decimal, string)"/> 
    /// or <see cref="QuadrigaClient.Sell(decimal, string)"/>.
    /// </summary>
    /// <remarks>
    /// The API docs state that only <c>amount</code> and <c>orders_matched</c> are returned. But experimentation shows
    /// that <c>orders_matched</c> is not returned and other things are.
    /// </remarks>
    /// <seealso cref="LimitOrder"/>
    /// <seealso cref="OpenOrder"/>
    /// <seealso cref="Order"/>
    public class MarketOrder
    {
        /// <summary>
        /// The total amount of the major currency purchased or 
        /// the total amount of the minor currency acquired in the sale.
        /// </summary>
        /// <remarks>
        /// Expect "0.00000000" when buying.
        /// </remarks>
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Which orderbook it belongs to, such as "btc_cad".
        /// </summary>
        /// <remarks>Not listed in the API docs.</remarks>
        [JsonProperty("book")]
        public string Book { get; set; }

        /// <summary>
        /// Date and time.
        /// </summary>
        /// <remarks>Not listed in the API docs.</remarks>
        [JsonProperty("datetime ")]
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Order ID.
        /// </summary>
        /// <remarks>Not listed in the API docs.</remarks>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Price.
        /// </summary>
        /// <remarks>Not listed in the API docs.  Expect "0.00" when buying or selling.</remarks>
        [JsonProperty("price")]
        public decimal Price { get; set; }

        /// <summary>
        /// Status of the order (-1 - canceled; 0 - active; 1 - partially filled; 2 - complete).
        /// </summary>
        /// <remarks>Not listed in the API docs.</remarks>
        [JsonProperty("status")]
        public OrderStatus Status { get; set; }

        /// <summary>
        /// Buy or sell (0 - buy; 1 - sell).
        /// </summary>
        [JsonProperty("type")]
        public OrderType Type { get; set; }

        /// <summary>
        /// A set of amount/price pairs, one for each order that was matched in the trade.
        /// </summary>
        /// <remarks>
        /// Despite being in the API docs, this has not been seen to be returned.  Expect NULL.
        /// </remarks>
        [JsonProperty("orders_matched")]
        public List<OrderMatched> OrdersMatched { get; set; }
    }
    
    [JsonConverter(typeof(JArrayToObjectConverter))]
    public class OrderMatched
    {
        public decimal Amount { get; set; }
        public decimal Price { get; set; }
    }
}

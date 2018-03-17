using Newtonsoft.Json;
using QuadrigaCX.Api.Utils;
using System.Collections.Generic;

namespace QuadrigaCX.Api.Models
{
    public class MarketOrder
    {
        /// <summary>
        /// The total amount of the major currency purchased or 
        /// the total amount of the minor currency acquired in the sale.
        /// </summary>
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// A set of amount/price pairs, one for each order that was matched in the trade.
        /// </summary>
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

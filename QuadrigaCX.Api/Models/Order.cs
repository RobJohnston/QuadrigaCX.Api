using Newtonsoft.Json;
using System;

namespace QuadrigaCX.Api.Models
{
    public class Order
    {
        /// <summary>
        /// Order ID.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Which orderbook it belongs to.
        /// </summary>
        [JsonProperty("book")]
        public string Book { get; set; }

        /// <summary>
        /// Price of the order.
        /// </summary>
        [JsonProperty("price")]
        public decimal Price { get; set; }

        /// <summary>
        /// Amount of the order.
        /// </summary>
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Buy or sell (0 - buy; 1 - sell).
        /// </summary>
        [JsonProperty("type")]
        public OrderType Type { get; set; }

        /// <summary>
        /// Status of the order (-1 - canceled; 0 - active; 1 - partially filled; 2 - complete).
        /// </summary>
        [JsonProperty("status")]
        public OrderStatus Status { get; set; }

        /// <summary>
        /// Date the order was created.
        /// </summary>
        [JsonProperty("created")]
        public DateTime Created { get; set; }

        /// <summary>
        /// Date the order was last updated (not shown when status = 0).
        /// </summary>
        [JsonProperty("updated")]
        public DateTime? Updated { get; set; }
    }

    public enum OrderStatus
    {
        Cancelled = -1,
        Active = 0,
        PartiallyFilled = 1,
        Complete = 2
    }

    public enum OrderType
    {
        Buy = 0,
        Sell = 1
    }
}

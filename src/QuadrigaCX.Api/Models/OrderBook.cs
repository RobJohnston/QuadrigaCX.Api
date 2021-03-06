﻿using Newtonsoft.Json;
using QuadrigaCX.Api.Utils;
using System.Collections.Generic;

namespace QuadrigaCX.Api.Models
{
    /// <summary>
    /// Represents an order book returned from <see cref="QuadrigaClient.GetOrderBookAsync(string, bool)"/>.
    /// </summary>
    public class OrderBook
    {
        /// <summary>
        /// Unix timestamp.
        /// </summary>
        [JsonProperty("timestamp")]
        public double Timestamp { get; set; }

        /// <summary>
        /// A list of open buy orders, each represented as a list of price and amount.
        /// </summary>
        [JsonProperty("bids")]
        public List<OrderBookEntry> Bids { get; set; }

        /// <summary>
        /// A list of open sell orders, each represented as a list of price and amount.
        /// </summary>
        [JsonProperty("asks")]
        public List<OrderBookEntry> Asks { get; set; }
    }

    /// <summary>
    /// The price and amount of an open order.
    /// </summary>
    [JsonConverter(typeof(JArrayToObjectConverter))]
    public class OrderBookEntry
    {
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
    }
}

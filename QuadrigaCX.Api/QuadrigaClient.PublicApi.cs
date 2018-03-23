using QuadrigaCX.Api.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace QuadrigaCX.Api
{
    public partial class QuadrigaClient
    {
        /// <summary>
        /// Get the current order books on QuadrigaCX.
        /// An order book is always referred to in the API as "Major_Minor". For example: "btc_cad".
        /// </summary>
        /// <returns>
        /// A List of books.  Note that Bitcoin is considered the "minor" currency in the Ether/Bitcoin order book.
        /// </returns>
        public List<string> GetBooks()
        {
            List<string> books = new List<string>
            {
                "btc_cad",
                "btc_usd",
                "eth_cad",
                "eth_btc",
                "ltc_cad",
                "ltc_btc",
                "bch_cad",
                "bch_btc",
                "btg_cad",
                "btg_btc"
            };

            return books;
        }

        /// <summary>
        /// Get the currencies used on QuadrigaCX.
        /// </summary>
        /// <returns>
        /// A Dictionary containing the 3-letter code as Key and the full name as Value.
        /// </returns>
        /// <remarks>
        /// The "account balance" call also returns "etc" and "xau", but these are not mentioned elsewhere.
        /// </remarks>
        public Dictionary<string, string> GetCurrencies()
        {
            Dictionary<string, string> currencies = new Dictionary<string, string>
            {
                { "cad", "Canadian Dollars" },
                { "usd", "US Dollars" },
                { "btc", "Bitcoin " },
                { "ltc", "Litecoin " },
                { "bch", "Bitcoin Cash" },
                { "btg", "Bitcoin Gold" },
                { "eth", "Ether" }
            };

            return currencies;
        }

        /// <summary>
        /// Current Trading Information.
        /// </summary>
        /// <param name="book">If unspecified, the book will default to btc_cad.</param>
        /// <returns>Trading information from the specified <paramref name="book"/>.</returns>
        /// <exception cref="HttpRequestException">There was a problem with the HTTP request.</exception>
        /// <exception cref="QuadrigaException">There was a problem with the QuadrigaCX API call.</exception>
        public async Task<TickerInfo> GetTickerInformationAsync(string book)
        {
            return await QueryPublicAsync<TickerInfo>(
                "ticker",
                new Dictionary<string, string>(1)
                {
                    ["book"] = book
                }
            );
        }

        /// <summary>
        /// Get all open orders.
        /// </summary>
        /// <param name="book">Book to return orders for. Default btc_cad.</param>
        /// <param name="group">Group orders with the same price.  Default: true.</param>
        /// <returns>List of all open orders for the specified <paramref name="book"/>.</returns>
        /// <exception cref="HttpRequestException">There was a problem with the HTTP request.</exception>
        /// <exception cref="QuadrigaException">There was a problem with the QuadrigaCX API call.</exception>
        public async Task<OrderBook> GetOrderBookAsync(string book = "btc_cad", bool group = true)
        {
            return await QueryPublicAsync<OrderBook>(
                "order_book",
                new Dictionary<string, string>(2)
                {
                    ["book"] = book,
                    ["group"] = (group) ? "1" : "0"
                }
            );
        }

        /// <summary>
        /// Get recent trades.
        /// </summary>
        /// <param name="book">Book to return orders for (optional, default btc_cad)</param>
        /// <param name="time">Time frame for transaction export ("minute" - 1 minute, "hour" - 1 hour). Default: hour.</param>
        /// <returns>An array of recent trades.</returns>
        /// <exception cref="HttpRequestException">There was a problem with the HTTP request.</exception>
        /// <exception cref="QuadrigaException">There was a problem with the QuadrigaCX API call.</exception>
        public async Task<Transaction[]> GetTransactionsAsync(string book = "btc_cad", string time = "hour")
        {
            return await QueryPublicAsync<Transaction[]>(
                "transactions",
                new Dictionary<string, string>(2)
                {
                    ["book"] = book,
                    ["time"] = time
                }
            );
        }
    }
}

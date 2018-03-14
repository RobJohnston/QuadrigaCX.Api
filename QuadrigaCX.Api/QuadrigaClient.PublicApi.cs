using QuadrigaCX.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuadrigaCX.Api
{
    public partial class QuadrigaClient
    {
        /// <summary>
        /// Get trading information from the specified book.
        /// </summary>
        /// <param name="book">If unspecified, the book will default to btc_cad.</param>
        /// <returns>Trading information from the specified book.</returns>
        /// <exception cref="HttpRequestException">There was a problem with the HTTP request.</exception>
        /// <exception cref="QuadrigaException">There was a problem with the QuadrigaCX API call.</exception>
        public Task<TickerInfo> GetTickerInformation(string book)
        {
            return QueryPublic<TickerInfo>(
                "ticker",
                new Dictionary<string, string>(1)
                {
                    ["book"] = book
                }
            );
        }

        /// <summary>
        /// Get open orders.
        /// </summary>
        /// <param name="book">Book to return orders for. Default btc_cad.</param>
        /// <param name="group">Group orders with the same price.  Default: true.</param>
        /// <returns>List of all open orders.</returns>
        /// <exception cref="HttpRequestException">There was a problem with the HTTP request.</exception>
        /// <exception cref="QuadrigaException">There was a problem with the QuadrigaCX API call.</exception>
        public Task<OrderBook> GetOrderBook(string book = "btc_cad", bool group = true)
        {
            return QueryPublic<OrderBook>(
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
        /// <returns>List of recent trades.</returns>
        /// <exception cref="HttpRequestException">There was a problem with the HTTP request.</exception>
        /// <exception cref="QuadrigaException">There was a problem with the QuadrigaCX API call.</exception>
        public Task<Transaction[]> GetTransactions(string book = "btc_cad", string time = "hour")
        {
            return QueryPublic<Transaction[]>(
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

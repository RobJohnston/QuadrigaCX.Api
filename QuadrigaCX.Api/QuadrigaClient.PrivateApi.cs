using QuadrigaCX.Api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuadrigaCX.Api
{
    public partial class QuadrigaClient
    {
        #region Data queries

        /// <summary>
        /// Get account balance.
        /// </summary>
        /// <returns>Returns balances for all currencies.</returns>
        public async Task<AccountBalance> GetAccountBalanceAsync()
        {
            return await QueryPrivateAsync<AccountBalance>(
                "balance",
                null
            );
        }

        /// <summary>
        /// Get user transactions.
        /// </summary>
        /// <param name="offset">Skip that many transactions before beginning to return results. Default: 0.</param>
        /// <param name="limit">Limit result to that many transactions. Default: 50.</param>
        /// <param name="sort">Sorting by date and time (asc - ascending; desc - descending). Default: desc.</param>
        /// <param name="book">Otional, if not specified, will default to btc_cad</param>
        /// <returns></returns>
        public async Task<UserTransaction[]> GetUserTransactionsAsync(int offset = 0, int limit = 50, string sort = "desc", string book = "btc_cad")
        {
            return await QueryPrivateAsync<UserTransaction[]>(
                "user_transactions",
                new Dictionary<string, string>(4)
                {
                    ["offset"] = offset.ToString(_culture),
                    ["limit"] = limit.ToString(_culture),
                    ["sort"] = sort,
                    ["book"] = book
                }
            );
        }

        /// <summary>
        /// Get open orders.
        /// </summary>
        /// <param name="book">Optional, if not specified, will default to btc_cad</param>
        /// <returns></returns>
        public async Task<Order[]> GetOpenOrdersAsync(string book = "btc_cad")
        {
            return await QueryPrivateAsync<Order[]>(
                "open_orders",
                new Dictionary<string, string>(1)
                {
                    ["book"] = book
                }
            );
        }

        /// <summary>
        /// Lookup a specific order.
        /// </summary>
        /// <param name="id">A single 64 character long hexadecimal string taken from the list of orders.</param>
        /// <returns></returns>
        public async Task<Order[]> LookupOrderAsync(string id)
        {
            return await LookupOrderAsync(new string[] { id });
        }

        /// <summary>
        /// Lookup multiple orders.
        /// </summary>
        /// <param name="ids">An array of 64 character long hexadecimal strings taken from the list of orders.</param>
        /// <returns></returns>
        public async Task<Order[]> LookupOrderAsync(string[] ids)
        {
            return await QueryPrivateAsync<Order[]>(
                "lookup_order",
                new Dictionary<string, string>(1)
                {
                    ["id"] = String.Join(",", ids)
                }
            );
        }

        #endregion

        #region Trading

        public async Task<bool> CancelOrderAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Order[]> AddMarketOrderAsync(OrderType orderType, decimal amount, string book = "btc_cad")
        {
            throw new NotImplementedException();
        }

        public async Task<Order> AddLimitOrderAsync(OrderType orderType, decimal amount, decimal price, string book = "btc_cad")
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Deposit and withdrawal

        public async Task<string> GetDepositAddressAsync(string currency)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> WithdrawAsync(decimal amount, string address, string currency)
        {
            //TODO:  Look at validating the address.  See https://rosettacode.org/wiki/Bitcoin/address_validation#C.23

            throw new NotImplementedException();
        }

        #endregion
    }
}

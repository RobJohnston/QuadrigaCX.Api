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
        public async Task<OpenOrder[]> GetOpenOrdersAsync(string book = "btc_cad")
        {
            return await QueryPrivateAsync<OpenOrder[]>(
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
            return await QueryPrivateAsync<bool>(
                "cancel_order",
                new Dictionary<string, string>(1)
                {
                    ["id"] = id
                }
            );
        }

        /// <summary>
        /// Buy order - limit order.
        /// </summary>
        /// <param name="amount">Amount of major currency.</param>
        /// <param name="price">Price to buy at.</param>
        /// <param name="book">Optional, if not specified, will default to btc_cad.</param>
        /// <returns>LimitOrder</returns>
        public async Task<LimitOrder> Buy(decimal amount, decimal price, string book = "btc_cad")
        {
            return await QueryPrivateAsync<LimitOrder>(
                "buy",
                new Dictionary<string, string>(3)
                {
                    ["amount"] = amount.ToString(_culture),
                    ["price"] = price.ToString(_culture),
                    ["book"] = book
                }
            );
        }

        /// <summary>
        /// Buy order - market order.
        /// </summary>
        /// <param name="amount">Amount of major currency to buy.</param>
        /// <param name="book">Optional, if not specified, will default to btc_cad.</param>
        /// <returns>MarketOrder</returns>
        public async Task<MarketOrder> Buy(decimal amount, string book = "btc_cad")
        {
            return await QueryPrivateAsync<MarketOrder>(
                "buy",
                new Dictionary<string, string>(2)
                {
                    ["amount"] = amount.ToString(_culture),
                    ["book"] = book
                }
            );
        }

        /// <summary>
        /// Sell order - limit order.
        /// </summary>
        /// <param name="amount">Amount of major currency.</param>
        /// <param name="price">Price to sell at.</param>
        /// <param name="book">Optional, if not specified, will default to btc_cad.</param>
        /// <returns>LimitOrder</returns>
        public async Task<LimitOrder> Sell(decimal amount, decimal price, string book = "btc_cad")
        {
            return await QueryPrivateAsync<LimitOrder>(
                "sell",
                new Dictionary<string, string>(3)
                {
                    ["amount"] = amount.ToString(_culture),
                    ["price"] = price.ToString(_culture),
                    ["book"] = book
                }
            );
        }

        /// <summary>
        /// Sell order - market order.
        /// </summary>
        /// <param name="amount">Amount of major currency to sell.</param>
        /// <param name="book">Optional, if not specified, will default to btc_cad.</param>
        /// <returns>MarketOrder</returns>
        public async Task<MarketOrder> Sell(decimal amount, string book = "btc_cad")
        {
            return await QueryPrivateAsync<MarketOrder>(
                "sell",
                new Dictionary<string, string>(2)
                {
                    ["amount"] = amount.ToString(_culture),
                    ["book"] = book
                }
            );
        }

        #endregion

        #region Deposit and withdrawal

        /// <summary>
        /// Returns a deposit address for funding your account.
        /// </summary>
        /// <param name="currencySymbol">A 3-letter currency code (BTC, BCH, BTG, LTC, ETH).</param>
        /// <returns></returns>
        public async Task<string> GetDepositAddressAsync(string currencySymbol)
        {
            string currencyName = GetCurrencyName(currencySymbol);

            return await QueryPrivateAsync<string>(
                string.Format("{0}_deposit_address", currencyName),
                null
            );
        }

        /// <summary>
        /// Withdraw
        /// </summary>
        /// <param name="amount">The amount to withdraw.</param>
        /// <param name="address">The address-to send the amount.</param>
        /// <param name="currencySymbol">A 3 letter currency code (BTC, BCH, BTG, LTC, ETH).</param>
        /// <returns>OK or error.</returns>
        public async Task<bool> WithdrawAsync(decimal amount, string address, string currencySymbol)
        {
            //TODO:  Look at validating the address.  See https://rosettacode.org/wiki/Bitcoin/address_validation#C.23

            string currencyName = GetCurrencyName(currencySymbol);

            return await QueryPrivateAsync<bool>(
                string.Format("{0}_withdrawal", currencyName),
                new Dictionary<string, string>(2)
                {
                    ["amount"] = amount.ToString(_culture),
                    ["address"] = address
                }
            );
        }

        #endregion

        private static string GetCurrencyName(string currencySymbol)
        {
            string currencyName;

            switch (currencySymbol.ToLowerInvariant())
            {
                case "btc":
                    currencyName = "bitcoin";
                    break;
                case "bch":
                    currencyName = "bitcoincash";
                    break;
                case "btg":
                    currencyName = "bitcoingold";
                    break;
                case "ltc":
                    currencyName = "litecoin";
                    break;
                case "eth":
                    currencyName = "ether";
                    break;
                default:
                    throw new ArgumentException("Invalid currency symbol", currencySymbol);
            }

            return currencyName;
        }
    }
}

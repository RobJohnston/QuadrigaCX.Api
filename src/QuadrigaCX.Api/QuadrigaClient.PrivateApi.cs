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
        /// <returns>All balances.</returns>
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
        /// <returns>An array of user transactions.</returns>
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
        /// Get the user's open orders.
        /// </summary>
        /// <param name="book">Optional, if not specified, will default to btc_cad</param>
        /// <returns>An array of open orders.</returns>
        /// <seealso cref="GetOrderBookAsync(string, bool)"/>
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
        /// <returns>An array containing a single order.</returns>
        /// <seealso cref="LookupOrderAsync(string[])"/>
        public async Task<Order[]> LookupOrderAsync(string id)
        {
            return await LookupOrderAsync(new string[] { id });
        }

        /// <summary>
        /// Lookup multiple orders.
        /// </summary>
        /// <param name="ids">An array of 64 character long hexadecimal strings taken from the list of orders.</param>
        /// <returns>An array of orders.</returns>
        /// <seealso cref="LookupOrderAsync(string)"/>
        public async Task<Order[]> LookupOrderAsync(string[] ids)
        {
            return await QueryPrivateAsync<Order[]>(
                "lookup_order",
                new Dictionary<string, string>(1)
                {
                    ["id"] = string.Join(",", ids)
                }
            );
        }

        #endregion

        #region Trading

        /// <summary>
        /// Cancel a user's <see cref="OpenOrder"/>.
        /// </summary>
        /// <param name="id">A 64 characters long hexadecimal string taken from the list of orders.</param>
        /// <returns>Returns 'true' if order has been found and canceled.</returns>
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
        /// <returns>The limit order created.</returns>
        public async Task<LimitOrder> BuyAsync(decimal amount, decimal price, string book = "btc_cad")
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
        /// <returns>The market order filled.</returns>
        public async Task<MarketOrder> BuyAsync(decimal amount, string book = "btc_cad")
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
        /// <returns>The limit order created.</returns>
        public async Task<LimitOrder> SellAsync(decimal amount, decimal price, string book = "btc_cad")
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
        /// <returns>The market order filled.</returns>
        public async Task<MarketOrder> SellAsync(decimal amount, string book = "btc_cad")
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
        /// Get a bitcoin deposit address for funding your account.
        /// </summary>
        /// <returns>A bitcoin address.</returns>
        public async Task<string> GetBitcoinDepositAddress()
        {
            return await GetDepositAddressAsync("bitcoin");
        }

        /// <summary>
        /// Withdraw bitcoin.
        /// </summary>
        /// <param name="amount">The amount to withdraw.</param>
        /// <param name="address">The bitcoin address that should receive the amount.</param>
        /// <returns>OK or error.</returns>
        public async Task<bool> WithdrawBitcoinAsync(decimal amount, string address)
        {
            return await WithdrawAsync(amount, address, "bitcoin");
        }

        /// <summary>
        /// Get a bitcoin cash deposit address for funding your account.
        /// </summary>
        /// <returns>A bitcoin cash address.</returns>
        public async Task<string> GetBitcoinCashDepositAddress()
        {
            return await GetDepositAddressAsync("bitcoincash");
        }

        /// <summary>
        /// Withdraw bitcoin cash.
        /// </summary>
        /// <param name="amount">The amount to withdraw.</param>
        /// <param name="address">The bitcoin cash address that should receive the amount.</param>
        /// <returns>OK or error.</returns>
        public async Task<bool> WithdrawBitcoinCashAsync(decimal amount, string address)
        {
            return await WithdrawAsync(amount, address, "bitcoincash");
        }

        /// <summary>
        /// Get a bitcoin gold deposit address for funding your account.
        /// </summary>
        /// <returns>A bitcoin gold address.</returns>
        public async Task<string> GetBitcoinGoldDepositAddress()
        {
            return await GetDepositAddressAsync("bitcoingold");
        }

        /// <summary>
        /// Withdraw bitcoin gold.
        /// </summary>
        /// <param name="amount">The amount to withdraw.</param>
        /// <param name="address">The bitcoin gold address that should receive the amount.</param>
        /// <returns>OK or error.</returns>
        public async Task<bool> WithdrawBitcoinGoldAsync(decimal amount, string address)
        {
            return await WithdrawAsync(amount, address, "bitcoingold");
        }

        /// <summary>
        /// Get a litecoin deposit address for funding your account.
        /// </summary>
        /// <returns>A litecoin address.</returns>
        public async Task<string> GetLitecoinDepositAddress()
        {
            return await GetDepositAddressAsync("litecoin");
        }

        /// <summary>
        /// Withdraw litecoin.
        /// </summary>
        /// <param name="amount">The amount to withdraw</param>
        /// <param name="address">The litecoin address that should receive the amount.</param>
        /// <returns>OK or error.</returns>
        public async Task<bool> WithdrawLitecoinAsync(decimal amount, string address)
        {
            return await WithdrawAsync(amount, address, "litecoin");
        }

        /// <summary>
        /// Get an ethereum deposit address for funding your account.
        /// </summary>
        /// <returns>An ethereum address.</returns>
        public async Task<string> GetEtherDepositAddress()
        {
            return await GetDepositAddressAsync("ether");
        }

        /// <summary>
        /// Withdraw ether.
        /// </summary>
        /// <param name="amount">The amount to withdraw.</param>
        /// <param name="address">The ethereum address that should receive the amount.</param>
        /// <returns>OK or error.</returns>
        public async Task<bool> WithdrawEtherAsync(decimal amount, string address)
        {
            return await WithdrawAsync(amount, address, "ether");
        }

        #endregion

        #region Private methods

        private async Task<string> GetDepositAddressAsync(string currencyName)
        {
            return await QueryPrivateAsync<string>(
                string.Format("{0}_deposit_address", currencyName),
                null
            );
        }

        private async Task<bool> WithdrawAsync(decimal amount, string address, string currencyName)
        {
            //TODO:  Look at validating the address.  See https://rosettacode.org/wiki/Bitcoin/address_validation#C.23

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
    }
}

namespace QuadrigaCX.Api.Models
{
    /// <summary>
    /// The type of trade.
    /// </summary>
    public enum UserTransactionType
    {
        /// <summary>
        /// A deposit to QuadrigaCX.
        /// </summary>
        Deposit = 0,

        /// <summary>
        /// A withdrawal from QuadrigaCX.
        /// </summary>
        Withdrawal = 1,

        /// <summary>
        /// A trade on QuadrigaCX.
        /// </summary>
        MarketTrade = 2
    }
}

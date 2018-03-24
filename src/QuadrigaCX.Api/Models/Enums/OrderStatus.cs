namespace QuadrigaCX.Api.Models
{
    /// <summary>
    /// The status of an order.
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// A cancelled order.
        /// </summary>
        Cancelled = -1,

        /// <summary>
        /// An active order.
        /// </summary>
        Active = 0,

        /// <summary>
        /// A partially filled order.
        /// </summary>
        PartiallyFilled = 1,

        /// <summary>
        /// A completed order.
        /// </summary>
        Complete = 2
    }
}

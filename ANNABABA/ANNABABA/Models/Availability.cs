namespace ANNABABA.Models
{
    using System;

    /// <summary>
    /// Defines the <see cref="Availability" />.
    /// </summary>
    internal class Availability
    {
        /// <summary>
        /// Gets or sets the bookedReceiptCount.
        /// </summary>
        internal int bookedReceiptCount { get; set; }

        /// <summary>
        /// Gets or sets the dtAnnadhanamDate.
        /// </summary>
        internal DateTime annadhanamDate { get; set; }

        /// <summary>
        /// Gets or sets the totalReceiptCount.
        /// </summary>
        internal int totalReceiptCount { get; set; }

        /// <summary>
        /// Gets or sets the balanceReceiptCount.
        /// </summary>
        internal int balanceReceiptCount { get; set; }
    }
}

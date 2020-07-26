namespace ANNABABA.Models
{
    using System;

    /// <summary>
    /// Defines the <see cref="ConfigurationDetails" />.
    /// </summary>
    internal class ConfigurationDetails
    {
        /// <summary>
        /// Gets or sets the effectiveDate.
        /// </summary>
        internal DateTime effectiveDate { get; set; }

        /// <summary>
        /// Gets or sets the TotalNoOfReceipts.
        /// </summary>
        internal int TotalNoOfReceipts { get; set; }

        /// <summary>
        /// Gets or sets the NoOfMonths.
        /// </summary>
        internal int NoOfMonths { get; set; }
    }
}

namespace DressDetails.Models
{
    /// <summary>
    /// Defines the <see cref="LoginDetails" />.
    /// </summary>
    internal class LoginDetails
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        internal int Id { get; set; }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        internal string Name { get; set; }

        /// <summary>
        /// Gets or sets the Address.
        /// </summary>
        internal string Address { get; set; }

        /// <summary>
        /// Gets or sets the UserName.
        /// </summary>
        internal string UserName { get; set; }

        /// <summary>
        /// Gets or sets the ContactNumber.
        /// </summary>
        internal string ContactNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsAdmin
        /// Gets or sets the IsAdmin..
        /// </summary>
        internal string IsAdmin { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsActive
        /// Gets or sets the IsActive..
        /// </summary>
        internal string IsActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsAdmin
        /// Gets or sets the IsAdmin..
        /// </summary>
        internal bool  IsAdminUser { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsActive
        /// Gets or sets the IsActive..
        /// </summary>
        internal bool IsActiveUser { get; set; }

        /// <summary>
        /// Gets or sets the Password.
        /// </summary>
        internal string Password { get; set; }

        /// <summary>
        /// Gets or sets the UserId.
        /// </summary>
        internal string UserId { get; set; }
    }
}

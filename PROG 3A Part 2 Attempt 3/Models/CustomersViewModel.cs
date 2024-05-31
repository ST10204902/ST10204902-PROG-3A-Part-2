namespace PROG_3A_Part_2_Attempt_3.Models
{
    /// <summary>
    /// Represents a view model for displaying customers.
    /// </summary>
    public class CustomersViewModel
    {
        /// <summary>
        /// Gets or sets the list of users.
        /// </summary>
        public List<AppUser> Users { get; set; }

        /// <summary>
        /// Gets or sets the dictionary of user roles. The key is the user ID and the value is the role name.
        /// </summary>
        public Dictionary<string, string> UserRoles { get; set; }
    }
}

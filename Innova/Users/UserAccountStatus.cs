using System.ComponentModel;

namespace Innova.Users
{
    /// <summary>
    /// Summary description for UserAccountStatus.
    /// </summary>
    public enum UserAccountStatus
    {
        /// <summary>
        /// Pending Approval.
        /// </summary>
        [Description("Pending")]
        PendingApproval = 0,

        /// <summary>
        /// Approved.
        /// </summary>
        [Description("Approved")]
        Approved = 1,

        /// <summary>
        /// Declined.
        /// </summary>
        [Description("Declined")]
        Declined = 2
    }
}
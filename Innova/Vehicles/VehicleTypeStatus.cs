using System.ComponentModel;

namespace Innova.Vehicles
{
    /// <summary>
    /// Vehicle type status enumeration.
    /// </summary>
    public enum VehicleTypeStatus
    {
        /// <summary>
        /// Active and approved status.
        /// </summary>
        [Description("Approved")]
        Approved = 0,

        /// <summary>
        /// Added by customer and pending approval status from Car MD.
        /// </summary>
        [Description("Added by customer and pending approval")]
        PendingApprovalAddedByCustomer = 1,

        /// <summary>
        /// Added by CarMD personnel and pending approval status from Car MD admin.
        /// </summary>
        [Description("Added by CarMD personnel and pending approval")]
        PendingApprovalAddedByCarMDAdmin = 2
    }
}
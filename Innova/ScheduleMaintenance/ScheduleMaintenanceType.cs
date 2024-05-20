using System.ComponentModel;

namespace Innova.ScheduleMaintenance
{
    /// <summary>
    /// Enumeration of the types of scheduled maintenance
    /// </summary>
    public enum ScheduleMaintenanceType
    {
        /// <summary>
        /// Scheduled Maintenance
        /// </summary>
        [Description("Scheduled Maintenance")]
        ScheduledMaintenance = 0,

        /// <summary>
        /// Unscheduled Maintenance
        /// </summary>
        [Description("Unscheduled Maintenance")]
        UnscheduledMaintenance = 1
    }
}
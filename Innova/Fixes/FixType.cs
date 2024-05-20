using System.ComponentModel;

namespace Innova.Fixes
{
    /// <summary>
    /// The enumeration of fix name types
    /// </summary>
    public enum FixType
    {
        /// <summary>
        /// Repair
        /// </summary>
        [Description("Repair")]
        Repair = 0,

        /// <summary>
        /// Unscheduled Maintenance
        /// </summary>
        [Description("Scheduled Maintenance")]
        ScheduledMaintenance = 1,

        /// <summary>
        /// Unscheduled Maintenance
        /// </summary>
        [Description("Unscheduled Maintenance")]
        UnscheduledMaintenance = 2
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace Innova.WebServiceV07.RO.DataObjects
{
    /// <summary>
    /// The diagnostic report contains the diagnostic report for an input vehicle (Fixes, Error Codes, Monitors etc)
    /// </summary>
    public class DiagReportInfo
    {
        /// <summary>
        /// Default constructor for the diagnostic report class.
        /// </summary>
        public DiagReportInfo()
        {
        }

        /// <summary>
        /// The <see cref="WebServiceSessionStatus"/> associated with the diagnostic report.  Determines if there were any errors retrieving the diagnostic report including user session problems.
        /// </summary>
        public WebServiceSessionStatus WebServiceSessionStatus;

        /// <summary>
        /// The <see cref="VehicleInfo"/> the report is for.
        /// </summary>
        public VehicleInfo Vehicle;

        /// <summary>
        /// The array of <see cref="ErrorCodeInfo"/>
        /// </summary>
        public ErrorCodeInfo[] Errors;

        /// <summary>
        /// The <see cref="string"/> tool LED status, Off, Green, Yellow, Red, Error
        /// </summary>
        public string ToolLEDStatusDesc = "";

        /// <summary>
        /// The <see cref="string"/> SIL LED status, ON, OFF, NOT SUPPORTED
        /// </summary>
        public string SILStatus { get; set; }

        /// <summary>
        /// The array of <see cref="FreezeFrameInfo"/> freeze frame data.
        /// </summary>
        public FreezeFrameInfo[] FreezeFrame;

        /// <summary>
        /// Check if ABS module supported
        /// </summary>
        public bool IsAbsSupported { get; set; }

        /// <summary>
        /// Check if SRS module supported
        /// </summary>
        public bool IsSrsSupported { get; set; }

        /// <summary>
        /// The <see cref="Guid"/> identification of the diagnostic report that is being returned.   This value must be saved and subsequently used when getting an updated
        /// diagnostic report (for no fix process)
        /// </summary>
        public Guid DiagnosticReportId = Guid.Empty;

        ///// <summary>
        ///// The array of <see cref="FreezeFrameInfo"/> freeze frame data.
        ///// </summary>
        //public		FreezeFrameInfo[]	FreezeFrame;

        /// <summary>
        /// The array of <see cref="MonitorInfo"/> monitor data.
        /// </summary>
        public MonitorInfo[] Monitors;

        /// <summary>
        /// The array of <see cref="FixInfo"/> objects associated with the diagnostic report (could be null)
        /// </summary>
        public FixInfo[] Fixes = null;

        /// <summary>
        /// The array of <see cref="RecallInfo"/> objects associated with the vehicle on this diagnostic report. Must submit flag to enable values to be returned.
        /// </summary>
        public RecallInfo[] Recalls = null;

        /// <summary>
        /// The array of <see cref="TSBInfo"/> objects associated with the vehicle on this diagnostic report. Must submit flag to enable values to be returned.
        /// </summary>
        public TSBInfo[] TSBs = null;

        /// <summary>
        /// The array of <see cref="TSBCategoryInfo"/> objects associated with the vehicle on this diagnostic report.  Must submit flag to enable values to be returned.
        /// </summary>
        public TSBCategoryInfo[] TSBCategories = null;

        /// <summary>
        /// The nullable <see cref="int"/> next scheduled maintenance mileage amount based on the vehicles CURRENT mileage...irrespective of the diagnostic report mileage.
        /// </summary>
        public int? ScheduledMaintenanceNextMileage = null;

        /// <summary>
        /// The <see cref="string"/> tool LED status, Off, Green, Yellow, Red, Error
        /// </summary>
        //public		string		ToolLEDStatusDesc = "";

        /// <summary>
        /// The <see cref="int"/> total TSB count.  If not set, then null.
        /// </summary>
        public int? TSBCountAll = null;

        /// <summary>
        /// The nullable <see cref="bool"/> flag indicating whether or not the vehicle has scheduled maintenance information.
        /// </summary>
        public bool? HasScheduledMaintenance = null;

        /// <summary>
        /// The nullable <see cref="bool"/> flag indicating whether or not the vehicle has unscheduled scheduled maintenance information.
        /// </summary>
        public bool? HasUnScheduledMaintenance = null;

        /// <summary>
        /// The nullable <see cref="bool"/> flag indicating whether or not the vehicle has warranty detail information.
        /// </summary>
        public bool? HasVehicleWarrantyDetails = null;

        /// <summary>
        /// The array of <see cref="ScheduleMaintenanceServiceInfo"/> objects associated with then next scheduled maintenance service.
        /// </summary>
        public ScheduleMaintenanceServiceInfo[] ScheduleMaintenanceServices = null;

        /// <summary>
        /// The nullable <see cref="int"/> next un scheduled maintenance mileage amount based on the vehicles CURRENT mileage...irrespective of the diagnostic report mileage.
        /// </summary>
        public int? UnScheduledMaintenanceNextMileage = null;

        /// <summary>
        /// The array of <see cref="ScheduleMaintenanceServiceInfo"/> objects associated with then next unscheduled scheduled maintenance service (within 15K miles).
        /// </summary>
        public ScheduleMaintenanceServiceInfo[] UnScheduledMaintenanceServices = null;

        /// <summary>
        /// The nullable <see cref="Guid"/> indicating whether or not the vehicle on the diagnostic report has warranty information.
        /// </summary>
        public VehicleWarrantyDetailInfo[] VehicleWarrantyDetails = null;

        /// <summary>
        /// The <see cref="FixStatusInfo"/> object which holds the fix statuses for the various systems and potential codes.
        /// </summary>
        public FixStatusInfo FixStatusInfo;

        //#GroupDTCs
        /// <summary>
        /// Group Fix by DTC list for ABS/SRS codes
        /// </summary>
        /// <returns></returns>
        internal FixInfo[] GroupFixByDTCs()
        {
            if (Fixes == null || Fixes.Length == 0) return new FixInfo[] { };
            var oldFixes = Fixes.ToList();
            var fixList = new List<FixInfo>();
            //Group Fixes by FixID
            var groupByFix = (from f in oldFixes
                              group f by f.FixId
                into g
                              select new { FixId = g.Key, DTCs = string.Join(",", g.Select(fg => fg.ErrorCode).OrderBy(s => s)) });

            foreach (var fg in groupByFix)
            {
                var fi = oldFixes.FirstOrDefault(f => string.Equals(f.FixId, fg.FixId));
                if (fi == null) continue;

                fi.ErrorCode = fg.DTCs;
                fixList.Add(fi);
            }
            return fixList.ToArray();
        }

        //#GroupDTCs

        //Clear fixes if MIL is OFF - Updated 04 Jan, 2022
        internal bool IsMILLOFF()
        {
            const string MIL_NAME = "Malfunction Indicator Lamp (MIL)";
            if (this.Monitors != null && this.Monitors.Any())
            {
                var milEcu = this.Monitors.FirstOrDefault(m => string.Equals(m.Description, MIL_NAME, StringComparison.CurrentCultureIgnoreCase));
                if (milEcu != null && string.Equals(milEcu.Value, "ON", StringComparison.CurrentCultureIgnoreCase))
                {
                    return false;
                }
            }
            return true;
        }

        //Clear fixes if MIL is OFF - Updated 04 Jan, 2022
    }
}
using Innova.DiagnosticReports;
using Innova.ScheduleMaintenance;
using Innova.Vehicles;
using Innova.WebServiceV07.RO.DataObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Innova.WebServiceV07.RO.Services
{
    /// <summary>
    /// PopulateDataForDiagReportInfo
    /// </summary>
    public class PopulateDataForDiagReportInfo
    {
        public static VehicleInfo PopulateVehicleInfo(DiagnosticReport diagnosticReport)
        {
            var vehicle = diagnosticReport.Vehicle;
            var vehicleInfo = new VehicleInfo();

            //populate the vehicle info object
            if (Global.UsePolkData)
            {
                VehicleInfo.PopulateVehicleInfoFromPolkVehicle(vehicleInfo, vehicle.PolkVehicleYMME, vehicle.Vin);
            }

            return vehicleInfo;
        }

        /// <summary>
        /// PopulateErrorCodeInfo
        /// </summary>
        /// <param name="diagnosticReport"></param>
        /// <param name="monitors"></param>
        /// <returns></returns>
        public static ErrorCodeInfo[] PopulateErrorCodeInfo(DiagnosticReport diagnosticReport, MonitorInfo[] monitors)
        {
            var errors = new ErrorCodeInfo[diagnosticReport.DiagnosticReportResult.DiagnosticReportResultErrorCodes.Count];

            for (int i = 0; i < diagnosticReport.DiagnosticReportResult.DiagnosticReportResultErrorCodes.Count; i++)
            {
                errors[i] = ErrorCodeInfo.GetWebServiceObject(diagnosticReport.DiagnosticReportResult.DiagnosticReportResultErrorCodes[i], includeLaymansTerms: true);
            }

            if (IsMILLOFF(monitors))
            {
                //Set MIL Code(if any) to StoredCode, change CodeType from  0 to 1
                if (errors != null && errors.Any())
                {
                    var mil = errors.FirstOrDefault(e => e.CodeType == 0);
                    if (mil != null)
                    {
                        mil.CodeType = 1;
                    }
                }
            }

            if (errors != null && errors.Any())
            {
                foreach (var errorCode in errors)
                {
                    errorCode.RepairStatus = string.Empty;
                }
            }

            if (diagnosticReport.VehicleDataLibEx == null)
            {
                return errors;
            }

            if (diagnosticReport.VehicleDataLibEx.OemData?.Dtcs != null || diagnosticReport.VehicleDataLibEx.OemData.Dtcs.Any())
            {
                foreach (var errorCode in errors)
                {
                    if (errorCode.ErrorCodeSystemType != (int)DiagnosticReportErrorCodeSystemType.ABS
                       && errorCode.ErrorCodeSystemType != (int)DiagnosticReportErrorCodeSystemType.SRS)
                    {
                        continue;
                    }

                    var oemDtc = diagnosticReport.VehicleDataLibEx.OemData.Dtcs.FirstOrDefault(d =>
                        string.Equals(d.Code, errorCode.Code, StringComparison.OrdinalIgnoreCase));
                    errorCode.RepairStatus = oemDtc != null ? oemDtc.RepairStatus : string.Empty;

                    //Add OEM DTC Def for ABS/SRS if any
                    if (errorCode.ErrorCodeDefinitions == null || !errorCode.ErrorCodeDefinitions.Any())
                    {
                        errorCode.ErrorCodeDefinitions = new[]
                        {
                            new ErrorCodeInfoDefinition
                            {
                                Title = oemDtc.Definition
                            }
                        };
                    }
                }
            }

            return errors;
        }

        /// <summary>
        /// PopulateFixInfo
        /// </summary>
        /// <param name="diagnosticReport"></param>
        /// <param name="monitors"></param>
        /// <returns></returns>
        public static FixInfo[] PopulateFixInfo(DiagnosticReport diagnosticReport, MonitorInfo[] monitors)
        {
            if (IsMILLOFF(monitors))
            {
                return new List<FixInfo>().ToArray();
            }

            var fixes = new FixInfo[diagnosticReport.DiagnosticReportResult.DiagnosticReportResultFixes.Count];
            for (int i = 0; i < diagnosticReport.DiagnosticReportResult.DiagnosticReportResultFixes.Count; i++)
            {
                DiagnosticReportResultFix drrFix = diagnosticReport.DiagnosticReportResult.DiagnosticReportResultFixes[i];
                FixInfo fixInfo = FixInfo.GetWebServiceObject(drrFix);
                fixes[i] = fixInfo;
            }

            //#GroupDTCs
            if (fixes != null && fixes.Length > 0)
            {
                fixes = GroupFixByDTCs(fixes);
            }
            //#GroupDTCs

            return fixes;
        }

        /// <summary>
        /// PopulateFixStatusInfo
        /// </summary>
        /// <param name="diagnosticReport"></param>
        /// <param name="determineNoFixStatusAutomatically"></param>
        /// <param name="monitors"></param>
        /// <param name="fixNotNeededString"></param>
        /// <returns></returns>
        public static FixStatusInfo PopulateFixStatusInfo(DiagnosticReport diagnosticReport, bool determineNoFixStatusAutomatically, MonitorInfo[] monitors, string fixNotNeededString)
        {
            FixStatusInfo fixStatusInfo = new FixStatusInfo();

            if (IsMILLOFF(monitors))
            {
                //Set Fix status to NOT NEEDED for all system
                fixStatusInfo.PwrFixStatus = (int)DiagnosticReportFixStatus.FixNotNeeded;
                fixStatusInfo.PwrFixStatusDesc = fixNotNeededString;
                fixStatusInfo.Obd1FixStatus = (int)DiagnosticReportFixStatus.FixNotNeeded;
                fixStatusInfo.Obd1FixStatusDesc = fixNotNeededString;
                fixStatusInfo.AbsFixStatus = (int)DiagnosticReportFixStatus.FixNotNeeded;
                fixStatusInfo.AbsFixStatusDesc = fixNotNeededString;
                fixStatusInfo.SrsFixStatus = (int)DiagnosticReportFixStatus.FixNotNeeded;
                fixStatusInfo.SrsFixStatusDesc = fixNotNeededString;

                return fixStatusInfo;
            }

            //ok if we have a diagnostic report, then input the fix information
            if (diagnosticReport != null)
            {
                fixStatusInfo.PwrFixStatus = (int)diagnosticReport.PwrDiagnosticReportFixStatus;
                fixStatusInfo.PwrFixStatusDesc = diagnosticReport.Registry.GetEnumDescription(diagnosticReport.PwrDiagnosticReportFixStatus);
                if (diagnosticReport.PwrDiagnosticReportFixStatus == DiagnosticReportFixStatus.FixNotFoundLookupCanceled)
                {
                    fixStatusInfo.PwrFixLookupCancelledReason = diagnosticReport.PwrDiagnosticReportFixCancelReason;
                }

                fixStatusInfo.Obd1FixStatus = (int)diagnosticReport.Obd1DiagnosticReportFixStatus;
                fixStatusInfo.Obd1FixStatusDesc = diagnosticReport.Registry.GetEnumDescription(diagnosticReport.Obd1DiagnosticReportFixStatus);
                if (diagnosticReport.Obd1DiagnosticReportFixStatus == DiagnosticReportFixStatus.FixNotFoundLookupCanceled)
                {
                    fixStatusInfo.Obd1FixLookupCancelledReason = diagnosticReport.Obd1DiagnosticReportFixCancelReason;
                }

                fixStatusInfo.AbsFixStatus = (int)diagnosticReport.AbsDiagnosticReportFixStatus;
                fixStatusInfo.AbsFixStatusDesc = diagnosticReport.Registry.GetEnumDescription(diagnosticReport.AbsDiagnosticReportFixStatus);
                if (diagnosticReport.AbsDiagnosticReportFixStatus == DiagnosticReportFixStatus.FixNotFoundLookupCanceled)
                {
                    fixStatusInfo.AbsFixLookupCancelledReason = diagnosticReport.AbsDiagnosticReportFixCancelReason;
                }
                fixStatusInfo.SrsFixStatus = (int)diagnosticReport.SrsDiagnosticReportFixStatus;
                fixStatusInfo.SrsFixStatusDesc = diagnosticReport.Registry.GetEnumDescription(diagnosticReport.SrsDiagnosticReportFixStatus);
                if (diagnosticReport.SrsDiagnosticReportFixStatus == DiagnosticReportFixStatus.FixNotFoundLookupCanceled)
                {
                    fixStatusInfo.SrsFixLookupCancelledReason = diagnosticReport.SrsDiagnosticReportFixCancelReason;
                }

                //determine if we want to do this here (Older note, no date)
                //STW 2011-08-25 Noting that this probably doesn't belong here (2nd time), should probably be it's own method on the DR report object itself and invoked when appropriate
                if (determineNoFixStatusAutomatically)
                {
                    //if we haven't completed the process, but we no longer have statuses and we're about to send this data out, then set the date
                    if (diagnosticReport.NoFixProcessCompletedAndSentDateTimeUTC.IsNull
                    && diagnosticReport.PwrDiagnosticReportFixStatus != DiagnosticReportFixStatus.FixNotFound
                    && diagnosticReport.Obd1DiagnosticReportFixStatus != DiagnosticReportFixStatus.FixNotFound
                    && diagnosticReport.AbsDiagnosticReportFixStatus != DiagnosticReportFixStatus.FixNotFound
                    && diagnosticReport.SrsDiagnosticReportFixStatus != DiagnosticReportFixStatus.FixNotFound)
                    {
                        diagnosticReport.NoFixProcessCompletedAndSentDateTimeUTC = DateTime.UtcNow;
                    }
                }
            }

            return fixStatusInfo;
        }

        /// <summary>
        /// PopulateMonitorInfo
        /// </summary>
        /// <param name="diagnosticReport"></param>
        /// <returns></returns>
        public static MonitorInfo[] PopulateMonitorInfo(DiagnosticReport diagnosticReport)
        {
            var ti = diagnosticReport.ToolInformation;

            var monitors = new MonitorInfo[0];

            if (ti == null)
            {
                return monitors;
            }

            monitors = new MonitorInfo[ti.Monitors.Count];

            for (int i = 0; i < ti.Monitors.Count; i++)
            {
                MonitorInfo mi = new MonitorInfo
                {
                    Description = ti.Monitors[i].Description,
                    Value = ti.Monitors[i].Value
                };

                monitors[i] = mi;
            }

            return monitors;
        }

        /// <summary>
        /// PopulateFreezeFrameInfo
        /// </summary>
        /// <param name="diagnosticReport"></param>
        /// <param name="monitors"></param>
        /// <returns></returns>
        public static FreezeFrameInfo[] PopulateFreezeFrameInfo(DiagnosticReport diagnosticReport, MonitorInfo[] monitors)
        {
            var ti = diagnosticReport.ToolInformation;

            var freezeFrames = new FreezeFrameInfo[0];

            if (ti == null)
            {
                return freezeFrames;
            }

            //now lets set the freeze frame
            freezeFrames = new FreezeFrameInfo[ti.FreezeFrames.Count];

            for (int i = 0; i < ti.FreezeFrames.Count; i++)
            {
                FreezeFrameInfo ffi = new FreezeFrameInfo
                {
                    Description = ti.FreezeFrames[i].Description,
                    Value = ti.FreezeFrames[i].Value
                };

                freezeFrames[i] = ffi;
            }

            if (IsMILLOFF(monitors))
            {
                //Clear FF DTC that caused required freeze frame data storage
                if (freezeFrames != null && freezeFrames.Any())
                {
                    var firstFF = freezeFrames.FirstOrDefault(f =>
                        string.Equals("DTC that caused required freeze frame data storage", f.Description,
                            StringComparison.OrdinalIgnoreCase));

                    if (firstFF != null)
                    {
                        firstFF.Value = string.Empty;
                    }
                }
            }

            return freezeFrames;
        }

        /// <summary>
        /// PopulateRecallInfo
        /// </summary>
        /// <param name="diagnosticReport"></param>
        /// <param name="includeRecallsForVehicle"></param>
        /// <returns></returns>
        public static RecallInfo[] PopulateRecallInfo(DiagnosticReport diagnosticReport, string includeRecallsForVehicle)
        {
            var recallInfos = new RecallInfo[0];
            if (!string.IsNullOrEmpty(includeRecallsForVehicle) && includeRecallsForVehicle.Trim().ToLower() == "true")
            {
                var recalls = Recall.Search(Global.Registry, diagnosticReport.Vehicle.Year, diagnosticReport.Vehicle.Make, diagnosticReport.Vehicle.Model, diagnosticReport.Vehicle.TrimLevel);

                recallInfos = new RecallInfo[recalls.Count];
                for (int i = 0; i < recalls.Count; i++)
                {
                    recallInfos[i] = RecallInfo.GetWebServiceObject(recalls[i]);
                }
            }

            return recallInfos;
        }

        /// <summary>
        /// PopulateTSBCategoryInfo
        /// </summary>
        /// <param name="diagnosticReport"></param>
        /// <param name="includeTSBCountForVehicle"></param>
        /// <returns></returns>
        public static TSBCategoryInfo[] PopulateTSBCategoryInfo(DiagnosticReport diagnosticReport, string includeTSBCountForVehicle)
        {
            var tsbCategories = new TSBCategoryInfo[0];
            if (!string.IsNullOrEmpty(includeTSBCountForVehicle) && includeTSBCountForVehicle.Trim().ToLower() == "true")
            {
                //switched to get this from the Vehicle VIN....
                tsbCategories = GetTSBCountByVehicleByCategory(diagnosticReport.Vehicle.Vin);
            }

            return tsbCategories;
        }

        /// <summary>
        /// PopulateTSBCountAll
        /// </summary>
        /// <param name="diagnosticReport"></param>
        /// <param name="includeTSBCountForVehicle"></param>
        /// <returns></returns>
        public static int? PopulateTSBCountAll(DiagnosticReport diagnosticReport, string includeTSBCountForVehicle)
        {
            int? tsbCountAll = null;
            if (!string.IsNullOrEmpty(includeTSBCountForVehicle) && includeTSBCountForVehicle.Trim().ToLower() == "true")
            {
                //switched to get this from the Vehicle VIN....
                tsbCountAll = GetTSBCountByVehicle(diagnosticReport.Vehicle.Vin);
            }

            return tsbCountAll;
        }

        /// <summary>
        /// PopulateVehicleWarrantyDetailInfo
        /// </summary>
        /// <param name="diagnosticReport"></param>
        /// <param name="includeWarrantyInfo"></param>
        /// <returns></returns>
        public static VehicleWarrantyDetailInfo[] PopulateVehicleWarrantyDetailInfo(DiagnosticReport diagnosticReport, string includeWarrantyInfo)
        {
            var vehicleWarrantyDetails = new VehicleWarrantyDetailInfo[0];

            if (!string.IsNullOrEmpty(includeWarrantyInfo) && includeWarrantyInfo.ToLower() == "true")
            {
                VehicleWarranty warranty = VehicleWarranty.GetCurrentlyValidWarranty(Global.Registry, diagnosticReport.Vehicle, Global.AverageMilesDrivenPerDay);

                if (warranty != null && warranty.VehicleWarrantyDetails.Count > 0)
                {
                    vehicleWarrantyDetails = new VehicleWarrantyDetailInfo[warranty.VehicleWarrantyDetails.Count];

                    for (int i = 0; i < warranty.VehicleWarrantyDetails.Count; i++)
                    {
                        vehicleWarrantyDetails[i] = VehicleWarrantyDetailInfo.GetWebServiceObject(warranty.VehicleWarrantyDetails[i]);
                    }
                }
            }

            return vehicleWarrantyDetails;
        }

        /// <summary>
        /// PopulateScheduleMaintenanceServiceInfo
        /// </summary>
        /// <param name="diagnosticReport"></param>
        /// <param name="includeNextScheduledMaintenance"></param>
        /// <returns></returns>
        public static (ScheduleMaintenanceServiceInfo[], bool, int?) PopulateScheduleMaintenanceServiceInfo(DiagnosticReport diagnosticReport, string includeNextScheduledMaintenance)
        {
            ScheduleMaintenanceServiceInfo[] scheduleMaintenanceServices = null;
            bool hasScheduledMaintenance = false;
            int? scheduledMaintenanceNextMileage = null;

            if (!string.IsNullOrEmpty(includeNextScheduledMaintenance) && includeNextScheduledMaintenance.ToLower() == "true" && diagnosticReport.Vehicle.Mileage.IsNull == false)
            {
                diagnosticReport.Vehicle.ScheduleMaintenancePlan = ScheduleMaintenancePlan.LookupScheduleMaintenancePlan(Global.Registry, diagnosticReport.Vehicle, ScheduleMaintenanceType.ScheduledMaintenance);
                diagnosticReport.Vehicle.Save();

                //if we have a scheduled maintenance plan for the vehicle..
                if (diagnosticReport.Vehicle.ScheduleMaintenancePlan != null)
                {
                    //This logic should be moved into the ScheduledMaintenancePlan in the future...
                    ScheduleMaintenancePlanDetailCollection planDetails = diagnosticReport.Vehicle.ScheduleMaintenancePlan.GetNextServices(diagnosticReport.Vehicle, true, DateTime.UtcNow, Global.AverageMilesDrivenPerDay, false);

                    if (planDetails.Count > 0)
                    {
                        hasScheduledMaintenance = true;
                        scheduledMaintenanceNextMileage = planDetails[0].NextServiceMileageInterval;
                        scheduleMaintenanceServices = new ScheduleMaintenanceServiceInfo[planDetails.Count];

                        for (int i = 0; i < planDetails.Count; i++)
                        {
                            scheduleMaintenanceServices[i] = ScheduleMaintenanceServiceInfo.GetWebServiceObject(planDetails[i]);
                        }
                    }
                }

                //if we didn't add any details, then mark false and set the details to null
                if (scheduleMaintenanceServices == null)
                {
                    hasScheduledMaintenance = false;
                    scheduleMaintenanceServices = new ScheduleMaintenanceServiceInfo[0];
                }
            }

            return (scheduleMaintenanceServices, hasScheduledMaintenance, scheduledMaintenanceNextMileage);
        }

        /// <summary>
        /// PopulateUnScheduleMaintenanceServiceInfo
        /// </summary>
        /// <param name="diagnosticReport"></param>
        /// <param name="includeNextScheduledMaintenance"></param>
        /// <returns></returns>
        public static (ScheduleMaintenanceServiceInfo[], bool, int?) PopulateUnScheduleMaintenanceServiceInfo(DiagnosticReport diagnosticReport, string includeNextScheduledMaintenance)
        {
            ScheduleMaintenanceServiceInfo[] unScheduledMaintenanceServices = null;
            bool hasUnScheduledMaintenance = false;
            int? unScheduledMaintenanceNextMileage = null;

            if (!string.IsNullOrEmpty(includeNextScheduledMaintenance) && includeNextScheduledMaintenance.ToLower() == "true" && diagnosticReport.Vehicle.Mileage.IsNull == false)
            {
                /****************************************************
				 * NOW REPEAT SERVICE FOR UNSCHEDULED MAINTENANCE
				 ****************************************************/

                ScheduleMaintenancePlan unscheduledMaintenancePlan = ScheduleMaintenancePlan.LookupScheduleMaintenancePlan(Global.Registry, diagnosticReport.Vehicle, ScheduleMaintenanceType.UnscheduledMaintenance);

                //we don't currently have this to save on the vehicle.... I think the reason we save it on the vehicle is in case we do updates for the user via email
                //if we have a scheduled maintenance plan for the vehicle..
                if (unscheduledMaintenancePlan != null)
                {
                    //This logic should be moved into the ScheduledMaintenancePlan in the future...
                    ScheduleMaintenancePlanDetailCollection planDetails = unscheduledMaintenancePlan.GetNextServices(diagnosticReport.Vehicle, true, DateTime.UtcNow, Global.AverageMilesDrivenPerDay, false);

                    if (planDetails.Count > 0)
                    {
                        hasUnScheduledMaintenance = true;
                        unScheduledMaintenanceNextMileage = planDetails[0].NextServiceMileageInterval;
                        unScheduledMaintenanceServices = new ScheduleMaintenanceServiceInfo[planDetails.Count];

                        for (int i = 0; i < planDetails.Count; i++)
                        {
                            unScheduledMaintenanceServices[i] = ScheduleMaintenanceServiceInfo.GetWebServiceObject(planDetails[i]);
                        }
                    }
                }

                //if we didn't add any details, then mark false and set the details to null
                if (unScheduledMaintenanceServices == null)
                {
                    hasUnScheduledMaintenance = false;
                    unScheduledMaintenanceServices = new ScheduleMaintenanceServiceInfo[0];
                }
            }

            return (unScheduledMaintenanceServices, hasUnScheduledMaintenance, unScheduledMaintenanceNextMileage);
        }

        private static FixInfo[] GroupFixByDTCs(FixInfo[] fixes)
        {
            if (fixes == null || fixes.Length == 0)
            {
                return new FixInfo[] { };
            }

            var oldFixes = fixes.ToList();
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

        private static TSBCategoryInfo[] GetTSBCountByVehicleByCategory(string vin)
        {
            string tsbConnectionString = ConfigurationManager.AppSettings["ConnectionString_ChiltonBulletins"];

            VehicleInfo vehicleInfo = GetVehicleInfo(vin);

            if (vehicleInfo == null || (vehicleInfo.ValidationFailures != null && vehicleInfo.ValidationFailures.Length > 0))
            {
                return null;
            }

            List<TsbCategory> tsbCategories = Tsb.GetTSBCountByVehicleByCategory(tsbConnectionString, vehicleInfo.AAIA, "/");

            TSBCategoryInfo[] tsbCategoryInfos = new TSBCategoryInfo[tsbCategories.Count];

            for (int i = 0; i < tsbCategories.Count; i++)
            {
                tsbCategoryInfos[i] = TSBCategoryInfo.GetWebServiceObject(tsbCategories[i]);
            }

            return tsbCategoryInfos;
        }

        private static VehicleInfo GetVehicleInfo(string vin)
        {
            //create vehicle info
            VehicleInfo v = new VehicleInfo();

            //Added on 2017-06-22 3:34 PM by INNOVA Dev Team to capture and store the VIN that's failed to decode.
            string errMessage = string.Empty;

            try
            {
                if (!string.IsNullOrEmpty(vin))
                {
                    vin = vin.Trim();

                    PolkVinDecoder pvd = new PolkVinDecoder(Global.RegistryReadOnly);

                    //Updated on 2018-02-5 4:15 PM by INNOVA Dev Team
                    PolkVehicleYMME pvYmme;

                    //Added on 2017-07-21 8:50 AM by INNOVA Dev Team to check for the requested VIN is a masked VIN or not
                    if (pvd.IsVinAValidMaskPattern(vin))
                        pvYmme = pvd.DecodeVIN(vin, false);
                    else //or if the requested VIN is a full VIN then we need to validate the VIN
                        pvYmme = pvd.DecodeVIN(vin, true);
                    //Ends the update of 2018-02-5 4:15 PM by INNOVA Dev Team

                    if (pvYmme != null)
                    {
                        VehicleInfo.PopulateVehicleInfoFromPolkVehicle(v, pvYmme, vin);
                        v.VIN = vin;
                    }
                    else
                    {
                        v.ValidationFailures = new ValidationFailure[1];
                        v.ValidationFailures[0] = new ValidationFailure();
                        v.ValidationFailures[0].Code = "415";
                        v.ValidationFailures[0].Description = "The VIN supplied does not appear to be valid.";
                        v.IsValid = false;
                    }
                }
            }
            catch (Exception ex)
            {
                //Gather the error message
                errMessage = ex.ToString();
            }

            //Throw exeption - error message is it is not null.
            if (!string.IsNullOrEmpty(errMessage))
            {
                throw new Exception(errMessage);
            }

            return v;
        }

        private static int? GetTSBCountByVehicle(string vin)
        {
            string tsbConnectionString = ConfigurationManager.AppSettings["ConnectionString_ChiltonBulletins"];

            VehicleInfo vehicleInfo = GetVehicleInfo(vin);

            if (vehicleInfo == null || (vehicleInfo.ValidationFailures != null && vehicleInfo.ValidationFailures.Length > 0))
            {
                return (-1);
            }

            //return the TSB by vehicle count

            //Added on 2017-09-15 at 2:36 PM by INNOVA Dev Team
            string aaia = vehicleInfo.AAIA;
            return Tsb.GetTSBCountByVehicle(tsbConnectionString, aaia, "/");
        }

        //Clear fixes if MIL is OFF - Updated 04 Jan, 2022
        private static bool IsMILLOFF(MonitorInfo[] monitors)
        {
            const string MIL_NAME = "Malfunction Indicator Lamp (MIL)";
            if (monitors != null && monitors.Any())
            {
                var milEcu = monitors.FirstOrDefault(m => string.Equals(m.Description, MIL_NAME, StringComparison.CurrentCultureIgnoreCase));
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
using Innova.Devices;
using Innova.DiagnosticReports;
using Innova.InnovaDLLParsers;
using Innova.Users;
using Innova.Vehicles;
using Innova.WebServiceV07.RO.DataObjects;
using Innova.WebServiceV07.RO.Helpers;
using Metafuse3.BusinessObjects;
using System;

namespace Innova.WebServiceV07.RO.Services
{
    public class DiagnosticReportService
    {
        public static DiagReportInfo GetDiagnosticReport
            (
                Registry registry,
                Registry registryReadOnly,
                WebServiceKey key = null,
                string methodInvoked = "",
                string externalSystemReportId = "",
                string externalSystemUserIdGuidString = "",
                string externalSystemUserFirstName = "",
                string externalSystemUserLastName = "",
                string externalSystemUserEmailAddress = "",
                string externalSystemUserPhoneNumber = "",
                string externalSystemUserRegion = "",
                string vin = "",
                int mileage = -1,
                string rawUpload = "",
                bool validateVin = false,
                string pwrFixNotFoundFixPromisedByDateTimeUTCString = "",
                string obd1FixNotFoundFixPromisedByDateTimeUTCString = "",
                string absFixNotFoundFixPromisedByDateTimeUTCString = "",
                string srsFixNotFoundFixPromisedByDateTimeUTCString = "",
                string includeRecallsForVehicle = "",
                string includeTSBCountForVehicle = "",
                string includeNextScheduledMaintenance = "",
                string includeWarrantyInfo = "",
                string createdDateTimeUTCString = "",
                string newDiagnosticReportId = "" //used for RabbitMQ call
            )
        {
            DiagReportInfo dr = new DiagReportInfo();
            WebServiceSessionStatus errors = new WebServiceSessionStatus();
            dr.WebServiceSessionStatus = errors;

            /*****************************************************************************
            * User processing
            * ***************************************************************************/
            var user = GetOrCreateUserFromSystemUserIdGuidString(registry, externalSystemUserIdGuidString, key.ExternalSystemId, errors, createUserIfNotFound: true);

            if (errors.ValidationFailures.Length > 0 || user == null)
            {
                return dr;
            }

            if (user != null)
            {
                //update the user in the main Innova database
                if (!string.IsNullOrEmpty(externalSystemUserFirstName))
                {
                    user.FirstName = externalSystemUserFirstName;
                }
                if (!string.IsNullOrEmpty(externalSystemUserLastName))
                {
                    user.LastName = externalSystemUserLastName;
                }
                if (!string.IsNullOrEmpty(externalSystemUserEmailAddress))
                {
                    user.EmailAddress = externalSystemUserEmailAddress;
                }
                if (!string.IsNullOrEmpty(externalSystemUserPhoneNumber))
                {
                    user.PhoneNumber = externalSystemUserPhoneNumber;
                }
                if (!string.IsNullOrEmpty(externalSystemUserRegion))
                {
                    user.Region = externalSystemUserRegion.ToUpper(); //ie. CA
                }
            }

            /*****************************************************************************
			* Vehicle processing
			* ***************************************************************************/
            PolkVehicleYMME polkVehicleYMME = null;
            string errorMessageDecodeVIN = string.Empty;

            (polkVehicleYMME, errorMessageDecodeVIN) = VINDecodingService.DecodeVIN(registry, vin, validateVin);

            if (!string.IsNullOrEmpty(errorMessageDecodeVIN))
            {
                errors.AddValidationFailure("50001", errorMessageDecodeVIN);

                return dr;
            }

            Vehicle vehicle = null;

            if (user != null)
            {
                vehicle = user.GetVehicle(vin);
            }

            if (vehicle == null)
            {
                vehicle = (Vehicle)registry.CreateInstance(typeof(Vehicle));
                vehicle.User = user;
            }
            vehicle.Vin = vin;
            vehicle.PolkVehicleYMME = polkVehicleYMME;
            vehicle.Nickname = "My " + polkVehicleYMME.Year + " " + polkVehicleYMME.Make + " " + polkVehicleYMME.Model;

            if (mileage >= 0)
            {
                vehicle.Mileage = mileage;
                vehicle.MileageLastRecordedDateTimeUTC = DateTime.UtcNow;
            }
            else
            {
                vehicle.Mileage = 0;
            }

            /****************************************************************************
			* Create the diagnostic report
			* **************************************************************************/
            DiagnosticReport diagnosticReport = null;

            if (!string.IsNullOrEmpty(newDiagnosticReportId))
            {
                #region used for RabbitMQ call

                diagnosticReport = (DiagnosticReport)registry.CreateInstance(typeof(DiagnosticReport), new Guid(newDiagnosticReportId));
                diagnosticReport.IsObjectCreated = true;

                #endregion used for RabbitMQ call
            }
            else
            {
                diagnosticReport = (DiagnosticReport)registry.CreateInstance(typeof(DiagnosticReport));
            }

            diagnosticReport.ExternalSystemReportId = externalSystemReportId;
            diagnosticReport.Vehicle = vehicle;
            diagnosticReport.User = user;
            diagnosticReport.VehicleMileage = vehicle.Mileage.Value;

            try
            {
                diagnosticReport.SetPropertiesAndToolInformationFromRawUploadString(rawUpload);
            }
            catch (Exception ex)
            {
                errors.AddValidationFailure("60001", "The payload could not be decoded and/or processed. Please check your settings and try again." + Environment.NewLine + Environment.NewLine + ex.ToString());

                return dr;
            }

            /*****************************************************************************
			* Device processing
			* ***************************************************************************/
            //apply the device to the diagnostic report
            Device device = null;
            Guid? toolId = null;

            if (user.ExternalSystem.Id == new Guid("00000000-0000-0000-0000-000000000015"))
            {
                toolId = Global.AUTOZONE_TOOL_ID;
            }
            else if (Metafuse3.Web.UI.Formatting.IsGuid(diagnosticReport.ToolInformation.ToolId))
            {
                toolId = new Guid(diagnosticReport.ToolInformation.ToolId);
            }

            if (toolId.HasValue)
            {
                device = Device.GetDeviceByChipIdAndUserIdAndActive(registry, toolId.Value, user.Id);
            }

            if (device == null)
            {
                device = (Device)registry.CreateInstance(typeof(Device));
                if (toolId.HasValue)
                {
                    device.ChipId = toolId.Value;
                }
                else
                {
                    device.ChipId = Global.FLEET_TOOL_ID;
                }
                device.User = user;
                device.IsPrimaryOwner = true;
                device.IsActive = true;
                device.CreatedDateTimeUTC = DateTime.UtcNow;
                device.UpdatedDateTimeUTC = device.CreatedDateTimeUTC;
            }

            diagnosticReport.Device = device;

            if (user != null)
            {
                //get the manual device for the current user (for now)
                diagnosticReport.Device = user.GetManualDevice();
            }

            diagnosticReport.CreateDiagnosticReportResult(true, false);

            //if this is a new report, then let's set the dates that things are supposed to be sent to the customer by.
            if (diagnosticReport.IsObjectCreated)
            {
                if ((!string.IsNullOrEmpty(diagnosticReport.PwrMilCode) || diagnosticReport.Symptom != null) && diagnosticReport.PwrDiagnosticReportFixStatus == DiagnosticReportFixStatus.FixNotFound)
                {
                    diagnosticReport.PwrFixNotFoundFixPromisedByDateTimeUTC = DateTimeHelper.GetNullableDateTimeFromString(pwrFixNotFoundFixPromisedByDateTimeUTCString);
                }
                if ((diagnosticReport.Obd1StoredCodes.Count > 0 || diagnosticReport.Symptom != null) && diagnosticReport.Obd1DiagnosticReportFixStatus == DiagnosticReportFixStatus.FixNotFound)
                {
                    diagnosticReport.Obd1FixNotFoundFixPromisedByDateTimeUTC = DateTimeHelper.GetNullableDateTimeFromString(obd1FixNotFoundFixPromisedByDateTimeUTCString);
                }
                if ((diagnosticReport.AbsStoredCodes.Count > 0 || diagnosticReport.Symptom != null) && diagnosticReport.AbsDiagnosticReportFixStatus == DiagnosticReportFixStatus.FixNotFound)
                {
                    diagnosticReport.AbsFixNotFoundFixPromisedByDateTimeUTC = DateTimeHelper.GetNullableDateTimeFromString(absFixNotFoundFixPromisedByDateTimeUTCString);
                }
                if ((diagnosticReport.SrsStoredCodes.Count > 0 || diagnosticReport.Symptom != null) && diagnosticReport.SrsDiagnosticReportFixStatus == DiagnosticReportFixStatus.FixNotFound)
                {
                    diagnosticReport.SrsFixNotFoundFixPromisedByDateTimeUTC = DateTimeHelper.GetNullableDateTimeFromString(srsFixNotFoundFixPromisedByDateTimeUTCString);
                }
            }

            //save the diagnostic report
            if (!string.IsNullOrEmpty(createdDateTimeUTCString))
            {
                DateTime.TryParse(createdDateTimeUTCString, out DateTime createdDateTimeUTC);

                if (createdDateTimeUTC != DateTime.MinValue)
                {
                    diagnosticReport.CreatedDateTimeUTC = createdDateTimeUTC;
                    diagnosticReport.UpdatedDateTimeUTC = createdDateTimeUTC;
                }
            }

            dr.DiagnosticReportId = diagnosticReport.Id;

            /*********************************************************************************************************
			 * Main processing is complete, now prepare the data and output it to the Web Service Object!
			 * *******************************************************************************************************/
            SetDiagReportInfoFromSDKObject
                (
                    registry,
                    registryReadOnly,
                    dr,
                    diagnosticReport,
                    determineNoFixStatusAutomatically: true,
                    includeRecallsForVehicle,
                    includeTSBCountForVehicle,
                    includeNextScheduledMaintenance,
                    includeWarrantyInfo
                );

            return dr;
        }

        public static void SetDiagReportInfoFromSDKObject
            (
                Registry registry,
                Registry registryReadOnly,
                DiagReportInfo dr,
                DiagnosticReport diagnosticReport,
                bool determineNoFixStatusAutomatically,
                string includeRecallsForVehicle,
                string includeTSBCountForVehicle,
                string includeNextScheduledMaintenance,
                string includeWarrantyInfo
           )
        {
            /*********************************************************************************************************
			 * Main processing is complete, now prepare the data and output it to the Web Service Object!
			 * *******************************************************************************************************/

            dr.SILStatus = diagnosticReport.SILStatus;
            dr.ToolLEDStatusDesc = diagnosticReport.ToolLEDStatus.ToString();

            dr.IsAbsSupported = InnovaRawPayloadParser.IsAbsSupported(diagnosticReport.VehicleDataLibEx);
            dr.IsSrsSupported = InnovaRawPayloadParser.IsSrsSupported(diagnosticReport.VehicleDataLibEx);

            dr.Vehicle = PopulateDataForDiagReportInfo.PopulateVehicleInfo(diagnosticReport);

            //now lets set the monitors
            dr.Monitors = PopulateDataForDiagReportInfo.PopulateMonitorInfo(diagnosticReport);

            //set the error codes
            dr.Errors = PopulateDataForDiagReportInfo.PopulateErrorCodeInfo(diagnosticReport, dr.Monitors);

            //now lets set the freeze frame
            dr.FreezeFrame = PopulateDataForDiagReportInfo.PopulateFreezeFrameInfo(diagnosticReport, dr.Monitors);

            //now lets set the fixes
            dr.Fixes = PopulateDataForDiagReportInfo.PopulateFixInfo(diagnosticReport, dr.Monitors);

            //setup the fix status here....
            dr.FixStatusInfo = PopulateDataForDiagReportInfo.PopulateFixStatusInfo(diagnosticReport, determineNoFixStatusAutomatically, dr.Monitors, registry.GetEnumDescription(DiagnosticReportFixStatus.FixNotNeeded));

            //set recalls
            dr.Recalls = PopulateDataForDiagReportInfo.PopulateRecallInfo(registry, diagnosticReport, includeRecallsForVehicle);

            //set tsb categories
            dr.TSBCategories = PopulateDataForDiagReportInfo.PopulateTSBCategoryInfo(registryReadOnly, diagnosticReport, includeTSBCountForVehicle);

            //set tsb count all
            dr.TSBCountAll = PopulateDataForDiagReportInfo.PopulateTSBCountAll(registryReadOnly, diagnosticReport, includeTSBCountForVehicle);

            //set vehicle warranty
            dr.VehicleWarrantyDetails = PopulateDataForDiagReportInfo.PopulateVehicleWarrantyDetailInfo(registry, diagnosticReport, includeWarrantyInfo);

            if (dr.VehicleWarrantyDetails.Length > 0)
            {
                dr.HasVehicleWarrantyDetails = true;
            }
            else
            {
                dr.HasVehicleWarrantyDetails = false;
            }

            //set scheduled maintenance
            var scheduleMaintenanceServices = new ScheduleMaintenanceServiceInfo[0];
            bool hasScheduledMaintenance = false;
            int? scheduledMaintenanceNextMileage = null;

            (scheduleMaintenanceServices, hasScheduledMaintenance, scheduledMaintenanceNextMileage) = PopulateDataForDiagReportInfo.PopulateScheduleMaintenanceServiceInfo(registry, diagnosticReport, includeNextScheduledMaintenance);
            dr.ScheduleMaintenanceServices = scheduleMaintenanceServices;
            dr.HasScheduledMaintenance = hasScheduledMaintenance;
            dr.ScheduledMaintenanceNextMileage = scheduledMaintenanceNextMileage;

            //set unscheduled maintenance
            var unScheduleMaintenanceServices = new ScheduleMaintenanceServiceInfo[0];
            bool hasUnScheduledMaintenance = false;
            int? unScheduledMaintenanceNextMileage = null;

            (unScheduleMaintenanceServices, hasUnScheduledMaintenance, unScheduledMaintenanceNextMileage) = PopulateDataForDiagReportInfo.PopulateUnScheduleMaintenanceServiceInfo(registry, diagnosticReport, includeNextScheduledMaintenance);
            dr.UnScheduledMaintenanceServices = unScheduleMaintenanceServices;
            dr.HasUnScheduledMaintenance = hasUnScheduledMaintenance;
            dr.UnScheduledMaintenanceNextMileage = unScheduledMaintenanceNextMileage;
        }

        private static User GetOrCreateUserFromSystemUserIdGuidString(Registry registry, string externalSystemUserIdGuidString, Guid? externalSystemId, WebServiceSessionStatus errors, bool createUserIfNotFound)
        {
            Guid userId = Guid.Empty;

            try
            {
                userId = new Guid(externalSystemUserIdGuidString);
            }
            catch
            {
                if (createUserIfNotFound)
                {
                    errors.AddValidationFailure("40001", "You must supply a valid GUID for the userID in string format");
                }
                return null;
            }

            //first let's make sure the user is in the database, if not we'll need to add them
            User user = (User)registry.CreateInstance(typeof(User), userId);

            try
            {
                user.Load();
            }
            catch (Exception e)
            {
                if (createUserIfNotFound)
                {
                    if (e.ToString().ToLower().Contains("load failed for type"))
                    {
                        try
                        {
                            //user not found, let's create the user then update the ID to match...this is done the first time...
                            user = (User)registry.CreateInstance(typeof(User));
                            user.UserType = UserType.ExternalSystemUser;
                            user.ExternalSystem = (ExternalSystem)registry.CreateInstance(typeof(ExternalSystem), externalSystemId.Value);

                            user.Save();

                            //the newly created user needs to have the ID changed...
                            using (Metafuse3.Data.SqlClient.SqlDataReaderWrapper updateUserIdDataReader = new Metafuse3.Data.SqlClient.SqlDataReaderWrapper(registry.ConnectionStringDefault))
                            {
                                updateUserIdDataReader.ExecuteNonQuery("UPDATE [User] Set UserId = '" + Guid.Parse(externalSystemUserIdGuidString).ToString() + "' WHERE UserId = '" + user.Id.ToString() + "'");
                            }
                            //set the local reference to null
                            user = null;
                            user = (User)registry.CreateInstance(typeof(User), userId);
                        }
                        catch (Exception ex)
                        {
                            errors.AddValidationFailure("40001", $"{ex}");

                            return null;
                        }
                    }
                    else
                    {
                        errors.AddValidationFailure("40001", $"{e}");

                        return null;
                    }
                }
            }

            //Updated on 2017-09-29 by Nam Lu - INNOVA Dev Team: Does not validate externalUserId and WS Key for OBDFIX
            if (user.ExternalSystem.Id != externalSystemId.Value)
            {
                errors.AddValidationFailure("40001", "The User ID specified with the parameter \"externalSystemUserIdGuidString\" was created by an External System that does not match the current external system key.");
                return null;
            }
            //Updated on 2017-09-29 by Nam Lu - INNOVA Dev Team: Does not validate externalUserId and WS Key for OBDFIX

            return user;
        }
    }
}
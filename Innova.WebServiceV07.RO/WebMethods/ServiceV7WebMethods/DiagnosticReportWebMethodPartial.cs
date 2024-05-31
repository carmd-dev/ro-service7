using AutoMapper;
using Innova.DiagnosticReports;
using Innova.Fixes;
using Innova.WebServiceV07.RO.Common.Enums;
using Innova.WebServiceV07.RO.DataModels.RabbitMQModels;
using Innova.WebServiceV07.RO.DataModels.ServiceV7Models.DiagnosticReport;
using Innova.WebServiceV07.RO.DataModels.ServiceV7Models.WebServiceKey;
using Innova.WebServiceV07.RO.DataObjects;
using Innova.WebServiceV07.RO.Helpers;
using Innova.WebServiceV07.RO.Services;
using Metafuse3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;

namespace Innova.WebServiceV07.RO
{
    public partial class ServiceV7
    {
        [WebMethod(Description = "Creates a new diagnostic report for the specified payload and also optionally include TSBs and Recalls Information for the vehicle")]
        public DiagReportInfo CreateDiagnosticReportForVDK(
            WebServiceKey key,
            string externalSystemUserIdGuidString,
            string externalSystemUserFirstName,
            string externalSystemUserLastName,
            string externalSystemUserEmailAddress,
            string externalSystemUserPhoneNumber,
            string externalSystemUserRegion,
            string vin,
            int mileage,
            string transmission,
            string includeRecallsForVehicle,
            string includeTSBsForVehicleAndMatchingErrorCodes,
            string includeTSBCountForVehicle,
            string includeNextScheduledMaintenance,
            string includeWarrantyInfo,
            int softwareTypeInt,
            int toolTypeFormatInt,
            string rawUpload,
            string pwrFixNotFoundFixPromisedByDateTimeUTCString,
            string obd1FixNotFoundFixPromisedByDateTimeUTCString,
            string absFixNotFoundFixPromisedByDateTimeUTCString,
            string srsFixNotFoundFixPromisedByDateTimeUTCString)
        {
            DiagReportInfo reportInfo = new DiagReportInfo();
            WebServiceSessionStatus errors = new WebServiceSessionStatus();
            reportInfo.WebServiceSessionStatus = errors;

            try
            {
                //make sure they have a valid key
                if (!this.ValidateKeyAndLogTransaction(key, "CreateDiagnosticReportForVDK"))
                {
                    errors.AddValidationFailure("000", "Invalid Key");
                    return reportInfo;
                }

                //make sure UserId Valid
                if (!IsUserIdValid(externalSystemUserIdGuidString))
                {
                    errors.AddValidationFailure("40004", "ExternalSystemUserIdGuidString format is not valid");
                    return reportInfo;
                }

                //make sure rawUpload not empty
                if (string.IsNullOrEmpty(rawUpload))
                {
                    errors.AddValidationFailure("30000", "You must supply a raw upload string to run the CreateDiagnosticReport method");
                    return reportInfo;
                }

                reportInfo = DiagnosticReportService.GetDiagnosticReport
                    (
                        registry: this.Registry,
                        registryReadOnly: this.RegistryReadOnly,
                        key: key,
                        methodInvoked: "CreateDiagnosticReportForVDK",
                        externalSystemReportId: string.Empty,
                        externalSystemUserIdGuidString: externalSystemUserIdGuidString,
                        externalSystemUserFirstName: externalSystemUserFirstName,
                        externalSystemUserLastName: externalSystemUserLastName,
                        externalSystemUserEmailAddress: externalSystemUserEmailAddress,
                        externalSystemUserPhoneNumber: externalSystemUserPhoneNumber,
                        externalSystemUserRegion: externalSystemUserRegion,
                        vin: vin,
                        mileage: mileage,
                        rawUpload: rawUpload,
                        validateVin: true,
                        pwrFixNotFoundFixPromisedByDateTimeUTCString: pwrFixNotFoundFixPromisedByDateTimeUTCString,
                        obd1FixNotFoundFixPromisedByDateTimeUTCString: obd1FixNotFoundFixPromisedByDateTimeUTCString,
                        absFixNotFoundFixPromisedByDateTimeUTCString: absFixNotFoundFixPromisedByDateTimeUTCString,
                        srsFixNotFoundFixPromisedByDateTimeUTCString: srsFixNotFoundFixPromisedByDateTimeUTCString,
                        includeRecallsForVehicle: includeRecallsForVehicle,
                        includeTSBCountForVehicle: includeTSBCountForVehicle,
                        includeNextScheduledMaintenance: includeNextScheduledMaintenance,
                        includeWarrantyInfo: includeWarrantyInfo,
                        createdDateTimeUTCString: string.Empty,
                        newDiagnosticReportId: string.Empty
                    );

                #region Service ReadOnly - Send request to RabbitMQ

                if (reportInfo.WebServiceSessionStatus.ValidationFailures.Count() == 0)
                {
                    var logId = Guid.NewGuid().ToString();
                    string newDiagnosticReportId = reportInfo.DiagnosticReportId.ToString();

                    var rabbitMQRequestModel = new RabbitMQRequestModel<DiagnosticReportForVDKModel>
                    {
                        ServiceName = ServiceTypeEnum.ServiceV7.ToString(),
                        MethodName = MethodServiceV07Enum.CreateDiagnosticReportForVDK.ToString(),
                        ExternalSystemName = GetExternalSystemName(),
                        WebServiceKey = new WebServiceKeyModel
                        {
                            Key = key.Key,
                            LanguageString = key.LanguageString,
                            Region = key.Region,
                            Currency = key.Currency,
                            MarketString = key.MarketString
                        },
                        PayloadInfo = PayloadHelper.BuildPayloadInfo
                        (
                            ServiceTypeEnum.ServiceV7.ToString(),
                            MethodServiceV07Enum.CreateDiagnosticReportForVDK.ToString(),
                            newDiagnosticReportId,
                            externalSystemUserIdGuidString,
                            vin,
                            mileage.ToString(),
                            rawUpload
                        ),
                        Data = new DiagnosticReportForVDKModel
                        {
                            externalSystemUserIdGuidString = externalSystemUserIdGuidString,
                            externalSystemUserFirstName = externalSystemUserFirstName,
                            externalSystemUserLastName = externalSystemUserLastName,
                            externalSystemUserEmailAddress = externalSystemUserEmailAddress,
                            externalSystemUserPhoneNumber = externalSystemUserPhoneNumber,
                            externalSystemUserRegion = externalSystemUserRegion,
                            vin = vin,
                            mileage = mileage,
                            transmission = transmission,
                            softwareTypeInt = softwareTypeInt,
                            toolTypeFormatInt = toolTypeFormatInt,
                            rawUpload = rawUpload,
                            pwrFixNotFoundFixPromisedByDateTimeUTCString = pwrFixNotFoundFixPromisedByDateTimeUTCString,
                            obd1FixNotFoundFixPromisedByDateTimeUTCString = obd1FixNotFoundFixPromisedByDateTimeUTCString,
                            absFixNotFoundFixPromisedByDateTimeUTCString = absFixNotFoundFixPromisedByDateTimeUTCString,
                            srsFixNotFoundFixPromisedByDateTimeUTCString = srsFixNotFoundFixPromisedByDateTimeUTCString
                        },
                        NewDiagnosticReportId = newDiagnosticReportId
                    };

                    SendRequestToRabbitMQ(rabbitMQRequestModel, logId, Global.RabbitMQ_QueueName_ServiceRO);
                }

                #endregion Service ReadOnly - Send request to RabbitMQ
            }
            catch (Exception ex)
            {
                errors.AddValidationFailure("90001", $"Error occurs when you create diagnostic report for VDK => Exception: {ex}");
            }

            return reportInfo;
        }

        [WebMethod(Description = "Creates a new diagnostic report for the specified payload and also optionally include TSBs and Recalls, RepairTips Information for the vehicle")]
        public DiagReportInfo CreateDiagnosticReportForVDKAndGetRepairTips(
           WebServiceKey key,
           string externalSystemUserIdGuidString,
           string externalSystemUserFirstName,
           string externalSystemUserLastName,
           string externalSystemUserEmailAddress,
           string externalSystemUserPhoneNumber,
           string externalSystemUserRegion,
           string vin,
           int mileage,
           string transmission,
           string includeRecallsForVehicle,
           string includeTSBsForVehicleAndMatchingErrorCodes,
           string includeTSBCountForVehicle,
           string includeNextScheduledMaintenance,
           string includeWarrantyInfo,
           int softwareTypeInt,
           int toolTypeFormatInt,
           string rawUpload,
           string pwrFixNotFoundFixPromisedByDateTimeUTCString,
           string obd1FixNotFoundFixPromisedByDateTimeUTCString,
           string absFixNotFoundFixPromisedByDateTimeUTCString,
           string srsFixNotFoundFixPromisedByDateTimeUTCString)
        {
            var drInfo = CreateDiagnosticReportForVDK
                 (
                     key,
                     externalSystemUserIdGuidString,
                     externalSystemUserFirstName,
                     externalSystemUserLastName,
                     externalSystemUserEmailAddress,
                     externalSystemUserPhoneNumber,
                     externalSystemUserRegion,
                     vin,
                     mileage,
                     transmission,
                     includeRecallsForVehicle,
                     includeTSBsForVehicleAndMatchingErrorCodes,
                     includeTSBCountForVehicle,
                     includeNextScheduledMaintenance,
                     includeWarrantyInfo,
                     softwareTypeInt,
                     toolTypeFormatInt,
                     rawUpload,
                     pwrFixNotFoundFixPromisedByDateTimeUTCString,
                     obd1FixNotFoundFixPromisedByDateTimeUTCString,
                     absFixNotFoundFixPromisedByDateTimeUTCString,
                     srsFixNotFoundFixPromisedByDateTimeUTCString
                 );

            if (drInfo.WebServiceSessionStatus.ValidationFailures.Length > 0)
            {
                return drInfo;
            }
            var mostlikelyFixes = new List<FixInfo>();

            try
            {
                if (drInfo?.Fixes != null && drInfo.Fixes.Any())
                {
                    var pwrFixes = drInfo.Fixes
                        .OrderBy(f => f.SortOrder)
                        .Where(fi => fi.ErrorCodeSystemType == (int)DiagnosticReportErrorCodeSystemType.PowertrainObd2)
                        .Take(3);
                    if (pwrFixes != null && pwrFixes.Any()) mostlikelyFixes.AddRange(pwrFixes);

                    if (mostlikelyFixes.Count() < 3)
                    {
                        var absFix = drInfo.Fixes
                            .OrderBy(f => f.SortOrder)
                            .FirstOrDefault(fi => fi.ErrorCodeSystemType == (int)DiagnosticReportErrorCodeSystemType.ABS);
                        if (absFix != null) mostlikelyFixes.Add(absFix);
                    }

                    if (mostlikelyFixes.Count() < 3)
                    {
                        var srsFix = drInfo.Fixes
                        .OrderBy(f => f.SortOrder)
                        .FirstOrDefault(fi => fi.ErrorCodeSystemType == (int)DiagnosticReportErrorCodeSystemType.SRS);
                        if (srsFix != null) mostlikelyFixes.Add(srsFix);
                    }
                }

                foreach (var fixInfo in mostlikelyFixes)
                {
                    int.TryParse(drInfo.Vehicle.Year, out var yearInt);
                    var rtips = RepairTip.Search(this.Registry, fixInfo.FixNameId, string.Empty, null,
                        yearInt, drInfo.Vehicle.Make, drInfo.Vehicle.Model, drInfo.Vehicle.EngineType,
                        string.Empty, SortDirection.Ascending, 1, 1, true, false, fixInfo.ErrorCode);
                    if (rtips != null && rtips.Count > 0)
                    {
                        fixInfo.RepairTipInfo = new RepairTipInfo
                        {
                            FixName = fixInfo.Name,
                            InitialInspection = rtips[0].Description,
                            DiagnosticProcedure = rtips[0].DiagnosticProcedure,
                            PossibleCause = rtips[0].PossibleCause,
                            RepairValidation = rtips[0].RepairValidation
                        };
                    }
                    else
                    {
                        fixInfo.RepairTipInfo = new RepairTipInfo();
                    }
                }

                drInfo.Fixes = mostlikelyFixes.ToArray();
            }
            catch (Exception ex)
            {
                drInfo.WebServiceSessionStatus.AddValidationFailure("90001", $"Error occurs when you create diagnostic report for VDK and get repair tips => Exception: {ex}");
            }

            return drInfo;
        }

        [WebMethod(Description = "Creates a new diagnostic report for the specified payload and also optionally include TSBs and Recalls Information for the vehicle")]
        public DiagReportInfo CreateDiagnosticReportForVDKReadOnly(
            WebServiceKey key,
            string externalSystemUserIdGuidString,
            string externalSystemUserFirstName,
            string externalSystemUserLastName,
            string externalSystemUserEmailAddress,
            string externalSystemUserPhoneNumber,
            string externalSystemUserRegion,
            string vin,
            int mileage,
            string transmission,
            string includeRecallsForVehicle,
            string includeTSBsForVehicleAndMatchingErrorCodes,
            string includeTSBCountForVehicle,
            string includeNextScheduledMaintenance,
            string includeWarrantyInfo,
            int softwareTypeInt,
            int toolTypeFormatInt,
            string rawUpload,
            string pwrFixNotFoundFixPromisedByDateTimeUTCString,
            string obd1FixNotFoundFixPromisedByDateTimeUTCString,
            string absFixNotFoundFixPromisedByDateTimeUTCString,
            string srsFixNotFoundFixPromisedByDateTimeUTCString)
        {
            DiagReportInfo reportInfo = new DiagReportInfo();
            WebServiceSessionStatus errors = new WebServiceSessionStatus();
            reportInfo.WebServiceSessionStatus = errors;

            try
            {
                //make sure they have a valid key
                if (!this.ValidateKeyAndLogTransaction(key, "CreateDiagnosticReportForVDK"))
                {
                    errors.AddValidationFailure("000", "Invalid Key");
                    return reportInfo;
                }

                //make sure UserId Valid
                if (!IsUserIdValid(externalSystemUserIdGuidString))
                {
                    errors.AddValidationFailure("40004", "ExternalSystemUserIdGuidString format is not valid");
                    return reportInfo;
                }

                //make sure rawUpload not empty
                if (string.IsNullOrEmpty(rawUpload))
                {
                    errors.AddValidationFailure("30000", "You must supply a raw upload string to run the CreateDiagnosticReport method");
                    return reportInfo;
                }

                reportInfo = DiagnosticReportService.GetDiagnosticReport
                    (
                        registry: this.Registry,
                        registryReadOnly: this.RegistryReadOnly,
                        key: key,
                        methodInvoked: "CreateDiagnosticReportForVDK",
                        externalSystemReportId: string.Empty,
                        externalSystemUserIdGuidString: externalSystemUserIdGuidString,
                        externalSystemUserFirstName: externalSystemUserFirstName,
                        externalSystemUserLastName: externalSystemUserLastName,
                        externalSystemUserEmailAddress: externalSystemUserEmailAddress,
                        externalSystemUserPhoneNumber: externalSystemUserPhoneNumber,
                        externalSystemUserRegion: externalSystemUserRegion,
                        vin: vin,
                        mileage: mileage,
                        rawUpload: rawUpload,
                        validateVin: true,
                        pwrFixNotFoundFixPromisedByDateTimeUTCString: pwrFixNotFoundFixPromisedByDateTimeUTCString,
                        obd1FixNotFoundFixPromisedByDateTimeUTCString: obd1FixNotFoundFixPromisedByDateTimeUTCString,
                        absFixNotFoundFixPromisedByDateTimeUTCString: absFixNotFoundFixPromisedByDateTimeUTCString,
                        srsFixNotFoundFixPromisedByDateTimeUTCString: srsFixNotFoundFixPromisedByDateTimeUTCString,
                        includeRecallsForVehicle: includeRecallsForVehicle,
                        includeTSBCountForVehicle: includeTSBCountForVehicle,
                        includeNextScheduledMaintenance: includeNextScheduledMaintenance,
                        includeWarrantyInfo: includeWarrantyInfo,
                        createdDateTimeUTCString: string.Empty,
                        newDiagnosticReportId: string.Empty
                    );
            }
            catch (Exception ex)
            {
                errors.AddValidationFailure("90001", $"Error occurs when you create diagnostic report for VDK => Exception: {ex}");
            }

            return reportInfo;
        }

        [WebMethod(Description = "Gets the diagnostic as it is already in the database.  Should be used only for special circumstances to retrieve the existing diagnostic report for presentation (i.e. see it in a different language than what was stored the first time)")]
        public DiagReportInfo GetDiagnosticReportExisting(
            WebServiceKey key,
            string diagnosticReportIdGuidString,
            string includeRecallsForVehicle,
            string includeTSBsForVehicleAndMatchingErrorCodes,
            string includeTSBCountForVehicle,
            string includeNextScheduledMaintenance,
            string includeWarrantyInfo)
        {
            //create a diagnostic report info class
            DiagReportInfo dr = new DiagReportInfo();
            WebServiceSessionStatus errors = new WebServiceSessionStatus();
            dr.WebServiceSessionStatus = errors;

            try
            {
                //make sure they have a valid key
                if (!this.ValidateKeyAndLogTransaction(key, "GetDiagnosticReportExisting"))
                {
                    errors.AddValidationFailure("10001", "Invalid Key");
                    return dr;
                }

                if (string.IsNullOrEmpty(diagnosticReportIdGuidString))
                {
                    errors.AddValidationFailure("85000", "You must supply the diagnostic report ID to retreive an existing diagnostic report");
                    return dr;
                }

                #region Service ReadOnly - Call ServiceV7 directly

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<WebServiceKey, ServiceV7Client.WebServiceKey>();
                    cfg.CreateMap<ServiceV7Client.ValidationFailure, ValidationFailure>();
                    cfg.CreateMap<ServiceV7Client.WebServiceSessionStatus, WebServiceSessionStatus>();
                    cfg.CreateMap<ServiceV7Client.VehicleInfo, VehicleInfo>();
                    cfg.CreateMap<ServiceV7Client.RecallInfo, RecallInfo>();
                    cfg.CreateMap<ServiceV7Client.TSBInfo, TSBInfo>();
                    cfg.CreateMap<ServiceV7Client.ScheduleMaintenanceServiceInfo, ScheduleMaintenanceServiceInfo>();
                    cfg.CreateMap<ServiceV7Client.ErrorCodeInfo, ErrorCodeInfo>();
                    cfg.CreateMap<ServiceV7Client.ErrorCodeInfoDefinition, ErrorCodeInfoDefinition>();
                    cfg.CreateMap<ServiceV7Client.FreezeFrameInfo, FreezeFrameInfo>();
                    cfg.CreateMap<ServiceV7Client.MonitorInfo, MonitorInfo>();
                    cfg.CreateMap<ServiceV7Client.FixInfo, FixInfo>();
                    cfg.CreateMap<ServiceV7Client.FixFeedbackInfo, FixFeedbackInfo>();
                    cfg.CreateMap<ServiceV7Client.ArticleInfo, ArticleInfo>();
                    cfg.CreateMap<ServiceV7Client.FixPartInfo, FixPartInfo>();
                    cfg.CreateMap<ServiceV7Client.FixToolInfo, FixToolInfo>();
                    cfg.CreateMap<ServiceV7Client.RepairTipInfo, RepairTipInfo>();
                    cfg.CreateMap<ServiceV7Client.TSBTypeInfo, TSBTypeInfo>();
                    cfg.CreateMap<ServiceV7Client.TSBCategoryInfo, TSBCategoryInfo>();
                    cfg.CreateMap<ServiceV7Client.VehicleWarrantyDetailInfo, VehicleWarrantyDetailInfo>();
                    cfg.CreateMap<ServiceV7Client.FixStatusInfo, FixStatusInfo>();
                    cfg.CreateMap<ServiceV7Client.FixFeedbackPartInfo, FixFeedbackPartInfo>();
                    cfg.CreateMap<ServiceV7Client.FixFeedbackToolInfo, FixFeedbackToolInfo>();
                    cfg.CreateMap<ServiceV7Client.FixPartOemInfo, FixPartOemInfo>();
                    cfg.CreateMap<ServiceV7Client.DiagReportInfo, DiagReportInfo>();
                });

                var mapper = new Mapper(config);
                var webServiceKey = mapper.Map<ServiceV7Client.WebServiceKey>(key);

                //Fix the issue of "System.Net.Sockets.SocketException: An existing connection was forcibly closed by the remote host"
                System.Net.ServicePointManager.SecurityProtocol
                    = System.Net.SecurityProtocolType.Tls12
                    | System.Net.SecurityProtocolType.Tls12
                    | System.Net.SecurityProtocolType.Tls11
                    | System.Net.SecurityProtocolType.Tls;

                using (var servicev7 = new ServiceV7Client.ServiceV7())
                {
                    servicev7.Url = Global.InnovaWebServiceV7Url;
                    servicev7.Timeout = 45000;

                    var diagReportInfoResponse = servicev7.GetDiagnosticReportExisting
                   (
                       webServiceKey,
                       diagnosticReportIdGuidString,
                       includeRecallsForVehicle,
                       includeTSBsForVehicleAndMatchingErrorCodes,
                       includeTSBCountForVehicle,
                       includeNextScheduledMaintenance,
                       includeWarrantyInfo
                   );

                    dr = mapper.Map<DiagReportInfo>(diagReportInfoResponse);
                }

                #endregion Service ReadOnly - Call ServiceV7 directly
            }
            catch (Exception ex)
            {
                errors.AddValidationFailure("90001", $"Error occurs when you get diagnostic report existing => Exception: {ex}");
            }

            return dr;
        }
    }
}
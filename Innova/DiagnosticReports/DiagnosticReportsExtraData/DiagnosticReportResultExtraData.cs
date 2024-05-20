using Innova.DtcCodes;
using Innova.InnovaDLLParsers;
using Innova.Utilities.Shared;
using Innova2.VehicleDataLib.Entities.Version5;
using Metafuse3.BusinessObjects;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Innova.DiagnosticReports.DiagnosticReportsExtraData
{
    //#SP36Extra
    /// <summary>
    /// DiagnosticReportResultExtraData
    /// </summary>
    public class DiagnosticReportResultExtraData
    {
        private const string InsertFFSQL = @"INSERT INTO [dbo].[DiagnosticReportResultFreezeFrame]
           ([DiagnosticReportResultFreezeFrameId]
           ,[DiagnosticReportResultId]
           ,[PId]
           ,[Name]
           ,[Value]
           ,[Unit])
     VALUES
           (NewId()
           ,'{DiagnosticReportResultId}'
           ,0--<PId, int,>
           ,'{Name}'
           ,'{Value}'
           ,'{Unit}')";

        private const string InsertMonitorSQL = @"INSERT INTO [dbo].[DiagnosticReportResultMonitor]
           ([DiagnosticReportResultMonitorId]
           ,[DiagnosticReportResultId]
           ,[PId]
           ,[Name]
           ,[Value]
           ,[Unit])
     VALUES
           (NewId()
           ,'{DiagnosticReportResultId}'
           ,0--<PId, int,>
           ,'{Name}'
           ,'{Value}'
           ,'{Unit}')
        ";

        private const string InsertTpmsSQL = @"INSERT INTO [dbo].[DiagnosticReportResultTPMS]
           ([DiagnosticReportResultTPMSId]
           ,[DiagnosticReportResultId]
           ,[PId]
           ,[Name]
           ,[Value]
           ,[Unit])
     VALUES
           (NewId()
           ,'{DiagnosticReportResultId}'
           ,0--{pId}
           ,'{Name}'
           ,'{Value}'
           ,'{Unit}')";

        private const string InsertDtcStatusSQL = @"INSERT INTO [dbo].[DiagnosticReportResultErrorCodeSatus]
           ([DiagnosticReportResultErrorCodeSatusId]
           ,[DiagnosticReportResultErrorCodeId]
           ,[DiagnosticReportResultId]
           ,[ErrorCode]
           ,[ErrorCodeType]
           ,[DiagnosticReportErrorCodeType]
           ,[DiagnosticReportErrorCodeSystemType]
           ,[DTCStatus]
           ,[SystemStatus])
     VALUES
           (NewId()
           ,'{DiagnosticReportResultErrorCodeId}'
           ,'{DiagnosticReportResultId}'
           ,'{ErrorCode}'
           ,'{ErrorCodeType}'
           ,{DiagnosticReportErrorCodeType} --Int
           ,{DiagnosticReportErrorCodeSystemType} -- Int
           ,'{DTCStatus}'
           ,'{SystemStatus}' --Repair status
           )";

        private const string InsertBrakeLifeSQL = @"INSERT INTO [dbo].[DiagnosticReportResultBrakeLife]
           ([DiagnosticReportResultBrakeLifeId]
           ,[DiagnosticReportResultId]
           ,[PId]
           ,[Name]
           ,[Value]
           ,[Unit]
           ,[Message])
     VALUES
           (NewId()
           ,'{DiagnosticReportResultId}'
           ,0--<PId, int,>
           ,'{Name}'
           ,'{Value}'
           ,'{Unit}'
           ,'{Message}')";

        private const string InsertReportExtraDataSQL = @"
                INSERT INTO [dbo].[DiagnosticReportExtraData]
                           ([DiagnosticReportExtraDataId]
                           ,[DiagnosticReportId]
                           ,[Name]
                           ,[ValueInt]
                           ,[ValueString]
                           ,[ValueLongString]
                           ,[ValueStatus])
                     VALUES
                           (NewId()
                           ,'{DiagnosticReportId}'
                           ,'{Name}'
                           ,0
                           ,'{ValueString}'
                           ,''
                           ,'')";

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="registry"></param>
        /// <param name="report"></param>
        public static void Save(Registry registry, DiagnosticReport report)
        {
            Task.Run(() =>
            {
                DoSaveVehicleData(registry, report);
            });
        }

        /// <summary>
        /// SaveVehicleData
        /// </summary>
        /// <param name="registry"></param>
        /// <param name="report"></param>
        private static void DoSaveVehicleData(Registry registry, DiagnosticReport report)
        {
            List<string> sqlInsertBatch = new List<string>();
            var ofmItems = InnovaRawPayloadParser.GetOFMItems(report.VehicleDataLibEx);

            GenerateInsertFF(report, report.VehicleDataLibEx, ref sqlInsertBatch);
            GenerateInsertMonitor(report, report.ToolInformation, ref sqlInsertBatch);

            GenerateInsertDtcStatus(report, report.VehicleDataLibEx, ref sqlInsertBatch);
            GenerateInsertBattery(report, report.VehicleDataLibEx, ref sqlInsertBatch);

            if (ofmItems.Any())
            {
                GenerateInsertTpms(report, ofmItems, ref sqlInsertBatch);
                GenerateInsertBrakeLife(report, ofmItems, ref sqlInsertBatch);
                GenerateInsertEngineOil(report, ofmItems, ref sqlInsertBatch);
            }

            if (sqlInsertBatch.Any())
            {
                SaveToolInfoData(registry, sqlInsertBatch, report.Id.ToString());
            }
        }

        /// <summary>
        /// SetOEMDTCStatusForNoFixReport
        /// </summary>
        /// <param name="registry"></param>
        /// <param name="report"></param>
        public static void SetOEMDTCStatusForNoFixReport(Registry registry, DiagnosticReport report)
        {
            if (report.IsAbsFixFeedbackRequired)
            {
                Task.Run(() =>
                {
                    DoSetOEMDTCStatusForNoFixReport(registry, report);
                });
            }
        }

        /// <summary>
        /// Set OEMDTC RepairStatus to Extra data if fix feedback required
        /// </summary>
        /// <param name="registry"></param>
        /// <param name="report"></param>
        private static void DoSetOEMDTCStatusForNoFixReport(Registry registry, DiagnosticReport report)
        {
            //ABS only for Payload V2
            var absDtcList = new List<OEMDTCType>();
            foreach (var dtc in report.VehicleDataLibEx.OemData.Dtcs)
            {
                if (string.Equals("No code", dtc.Code, StringComparison.CurrentCultureIgnoreCase))
                {
                    continue;
                }
                if (string.Equals("ABS", dtc.Group, StringComparison.OrdinalIgnoreCase))
                {
                    absDtcList.Add(new OEMDTCType
                    {
                        DTC = dtc.Code,
                        Group = dtc.Group,
                        RepairStatus = string.Empty,
                        Title = dtc.Definition,
                        TypeList = dtc.Types
                    });
                }
            }

            if (absDtcList.Any())
            {
                var dtcRepairStatuses = OEMDTCTypeRepairStatusV1.GetStatuses(registry, absDtcList, report.Vehicle.Make);

                DiagnosticReportExtraData.SetAbsDTCsToReportExtraData(registry, report.Id, report.Vehicle.Make, absDtcList, dtcRepairStatuses);
            }
        }

        private static void GenerateInsertFF(DiagnosticReport report, VehicleDataEx vehicleDataEx, ref List<string> sqlInsertBatch)
        {
            if (vehicleDataEx != null
                && vehicleDataEx.Obd2Data != null
                && vehicleDataEx.Obd2Data.ECU != null
                && vehicleDataEx.Obd2Data.ECU.FreezeFrame != null
                && vehicleDataEx.Obd2Data.ECU.FreezeFrame.Count > 0)
            {
                for (int i = 0; i < vehicleDataEx.Obd2Data.ECU.FreezeFrame.Count; i++)
                {
                    sqlInsertBatch.Add
                    (
                        InsertFFSQL.Replace("{DiagnosticReportResultId}", report.DiagnosticReportResult.Id.ToString())
                        .Replace("{Name}", vehicleDataEx.Obd2Data.ECU.FreezeFrame[i].Name)
                        .Replace("{Value}", string.Join(" ", vehicleDataEx.Obd2Data.ECU.FreezeFrame[i].Values))
                        .Replace("{Unit}", vehicleDataEx.Obd2Data.ECU.FreezeFrame[i].Unit)
                    );
                }
            }

            //Check if TCU FF attached also?
        }

        private static void GenerateInsertMonitor(DiagnosticReport report, ToolInformation ti, ref List<string> sqlInsertBatch)
        {
            if (ti != null && ti.Monitors != null && ti.Monitors.Count > 0)
            {
                for (int i = 0; i < ti.Monitors.Count; i++)
                {
                    sqlInsertBatch.Add
                    (
                        InsertMonitorSQL.Replace("{DiagnosticReportResultId}", report.DiagnosticReportResult.Id.ToString())
                        .Replace("{Name}", ti.Monitors[i].Description)
                        .Replace("{Value}", ti.Monitors[i].Value)
                        .Replace("{Unit}", string.Empty)
                    );
                }
            }
        }

        private static void GenerateInsertTpms(DiagnosticReport report, List<OFMItem> ofmItems, ref List<string> sqlInsertBatch)
        {
            var tirePressureItems = InnovaRawPayloadParser.GetTirePressureItems(ofmItems);

            foreach (var tp in tirePressureItems)
            {
                sqlInsertBatch.Add
                (
                    InsertTpmsSQL.Replace("{DiagnosticReportResultId}", report.DiagnosticReportResult.Id.ToString())
                    .Replace("{Name}", tp.itemname)
                    .Replace("{Value}", tp.value)
                    .Replace("{Unit}", "")
                );
            }
        }

        private static void GenerateInsertBrakeLife(DiagnosticReport report, List<OFMItem> ofmItems, ref List<string> sqlInsertBatch)
        {
            var brakePadCheck = InnovaRawPayloadParser.GetBrakePadCheck(ofmItems);

            if (brakePadCheck != null)
            {
                sqlInsertBatch.Add
                (
                    InsertBrakeLifeSQL.Replace("{DiagnosticReportResultId}", report.DiagnosticReportResult.Id.ToString())
                    .Replace("{Name}", brakePadCheck.itemname)
                    .Replace("{Value}", brakePadCheck.value)
                    .Replace("{Message}", OFMBrakePadCheckManager.GetMessage(report.Vehicle.Make, brakePadCheck.itemname, brakePadCheck.value))
                    .Replace("{Unit}", string.Empty)
                );
            }

            var frontBrakePadCheck = InnovaRawPayloadParser.GetFrontBrakePadCheck(ofmItems);

            if (frontBrakePadCheck != null)
            {
                sqlInsertBatch.Add
                (
                    InsertBrakeLifeSQL.Replace("{DiagnosticReportResultId}", report.DiagnosticReportResult.Id.ToString())
                    .Replace("{Name}", frontBrakePadCheck.itemname)
                    .Replace("{Value}", frontBrakePadCheck.value)
                    .Replace("{Message}", OFMBrakePadCheckManager.GetMessage(report.Vehicle.Make, frontBrakePadCheck.itemname, frontBrakePadCheck.value))
                    .Replace("{Unit}", string.Empty)
                );
            }

            var rearBrakePadCheck = InnovaRawPayloadParser.GetRearBrakePadCheck(ofmItems);

            if (rearBrakePadCheck != null)
            {
                sqlInsertBatch.Add
                (
                    InsertBrakeLifeSQL.Replace("{DiagnosticReportResultId}", report.DiagnosticReportResult.Id.ToString())
                    .Replace("{Name}", rearBrakePadCheck.itemname)
                    .Replace("{Value}", rearBrakePadCheck.value)
                    .Replace("{Message}", OFMBrakePadCheckManager.GetMessage(report.Vehicle.Make, rearBrakePadCheck.itemname, rearBrakePadCheck.value))
                    .Replace("{Unit}", string.Empty)
                );
            }
        }

        private static void GenerateInsertEngineOil(DiagnosticReport report, List<OFMItem> ofmItems, ref List<string> sqlInsertBatch)
        {
            var engineOilLevel = InnovaRawPayloadParser.GetEngineOilLevel(ofmItems);

            if (engineOilLevel != null)
            {
                sqlInsertBatch.Add
               (
                   InsertReportExtraDataSQL.Replace("{DiagnosticReportId}", report.Id.ToString())
                   .Replace("{Name}", "EngineOilLevel")
                   .Replace("{ValueString}", engineOilLevel.value ?? "")
               );
            }

            var engineOilLife = InnovaRawPayloadParser.GetEngineOilLife(ofmItems);

            if (engineOilLife != null)
            {
                sqlInsertBatch.Add
               (
                   InsertReportExtraDataSQL.Replace("{DiagnosticReportId}", report.Id.ToString())
                   .Replace("{Name}", "EngineOilLife")
                   .Replace("{ValueString}", engineOilLife.value ?? "")
               );
            }
        }

        private static void GenerateInsertBattery(DiagnosticReport report, VehicleDataEx vehicleDataEx, ref List<string> sqlInsertBatch)
        {
            if (vehicleDataEx == null)
            {
                return;
            }

            if (!string.IsNullOrWhiteSpace(vehicleDataEx.BatteryConditionStatus))
            {
                sqlInsertBatch.Add
                    (
                        InsertReportExtraDataSQL.Replace("{DiagnosticReportId}", report.Id.ToString())
                        .Replace("{Name}", "BatteryHealth")
                        .Replace("{ValueString}", vehicleDataEx.BatteryConditionStatus ?? "")
                    );
            }

            var batteryVoltage = (vehicleDataEx.BatteryVoltage ?? "").Replace("V\0", "V");

            if (!string.IsNullOrWhiteSpace(batteryVoltage))
            {
                sqlInsertBatch.Add
                    (
                        InsertReportExtraDataSQL.Replace("{DiagnosticReportId}", report.Id.ToString())
                        .Replace("{Name}", "BatteryVoltage")
                        .Replace("{ValueString}", batteryVoltage)
                    );
            }
        }

        private static void GenerateInsertDtcStatus(DiagnosticReport report, VehicleDataEx vehicleDataEx, ref List<string> sqlInsertBatch)
        {
            foreach (DiagnosticReportResultErrorCode errorCode in report.DiagnosticReportResult.DiagnosticReportResultErrorCodes)
            {
                var dtcInfo = vehicleDataEx.AllDtcs.FirstOrDefault(dtc => string.Equals(errorCode.ErrorCode, dtc.Code, StringComparison.OrdinalIgnoreCase));

                if (dtcInfo != null)
                {
                    sqlInsertBatch.Add
                    (
                        InsertDtcStatusSQL.Replace("{DiagnosticReportResultErrorCodeId}", errorCode.Id.ToString())
                        .Replace("{DiagnosticReportResultId}", report.DiagnosticReportResult.Id.ToString())
                        .Replace("{ErrorCode}", errorCode.ErrorCode)
                        .Replace("{ErrorCodeType}", errorCode.ErrorCodeType)
                        .Replace("{DiagnosticReportErrorCodeType}", ((int)errorCode.DiagnosticReportErrorCodeType).ToString())
                        .Replace("{DiagnosticReportErrorCodeSystemType}", ((int)errorCode.DiagnosticReportErrorCodeSystemType).ToString())
                        .Replace("{DTCStatus}", string.Join("|", dtcInfo.Types))
                        .Replace("{SystemStatus}", dtcInfo.RepairStatus ?? "")
                    );
                }
            }
        }

        private static void SaveToolInfoData(Registry registry, List<string> sqlBatch, string diagnosticReportId)
        {
            if (sqlBatch.Any())
            {
                using (var connection = new SqlConnection(registry.ConnectionStringDefault))
                {
                    connection.Open();

                    try
                    {
                        using (var transaction = connection.BeginTransaction())
                        {
                            try
                            {
                                using (var cmd = connection.CreateCommand())
                                {
                                    cmd.Connection = connection;
                                    cmd.Transaction = transaction;

                                    cmd.CommandText = string.Join(Environment.NewLine, sqlBatch);
                                    cmd.CommandType = CommandType.Text;
                                    cmd.CommandTimeout = 60;

                                    cmd.ExecuteNonQuery();
                                }

                                transaction.Commit();
                            }
                            catch (Exception ex)
                            {
                                Logger.Write($"DiagnosticReportResultExtraData => SaveToolInfoData => diagnosticReportId: {diagnosticReportId} => Exception: {ex}");

                                if (transaction != null)
                                {
                                    transaction.Rollback();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Write($"DiagnosticReportResultExtraData => SaveToolInfoData => diagnosticReportId: {diagnosticReportId} => Exception: {ex}");
                    }
                }
            }
        }
    }
}
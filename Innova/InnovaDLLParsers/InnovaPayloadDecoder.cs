using Innova.InnovaDLLParsers.InnovaPayloadModels;
using Innova.Utilities.Shared;
using Innova.Utilities.Shared.Model;
using Innova2.VehicleDataLib.Entities.Version5;
using Innova2.VehicleDataLib.Enums.Vehicle;
using Innova2.VehicleDataLib.Parsing.Version5;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Innova.InnovaDLLParsers
{
    /// <summary>
    ///
    /// </summary>
    public class InnovaPayloadDecoder
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="rawString"></param>
        /// <returns></returns>
        public static PayloadInfoMasterV02Model DecodePayload(string rawString)
        {
            var result = new PayloadInfoMasterV02Model();

            var buffer = Convert.FromBase64String(rawString);
            var len = buffer.Length;

            byte[] bufferSegment = VehicleDataParser.GetBufferSegment(buffer, VehicleDataParser.AZBufferIndex.Version);

            byte[] bufferSegment2 = VehicleDataParser.GetBufferSegment(buffer, VehicleDataParser.AZBufferIndex.VehicleInfoECU);
            var vehicleInfoECU = VehicleInfoParser.Parse(bufferSegment2);
            List<string> callibrationIds = new List<string>();

            if (vehicleInfoECU != null && vehicleInfoECU.CallibrationIds.Any())
            {
                callibrationIds.AddRange(vehicleInfoECU.CallibrationIds);
            }

            byte[] bufferSegment3 = VehicleDataParser.GetBufferSegment(buffer, VehicleDataParser.AZBufferIndex.VehicleInfoTCU);
            var vehicleInfoTCU = VehicleInfoParser.Parse(bufferSegment2);
            if (vehicleInfoTCU != null && vehicleInfoTCU.CallibrationIds.Any())
            {
                callibrationIds.AddRange(vehicleInfoTCU.CallibrationIds);
            }
            result.CallibrationIds = string.Join(", ", callibrationIds.Distinct());

            var monitors = InnovaRawPayloadParser.TransformMonitors(buffer);

            ToolInformation toolInformation = null;
            VehicleDataEx vehicleExInfo = null;

            try
            {
                (toolInformation, vehicleExInfo) = InnovaRawPayloadParser.ParseToolInformationAndVehicleData(rawString, Language.English);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while decoding the raw upload string within the Innova2.VehicleDataLib.Parsing.dll. The exception received is : {ex}");
            }

            //Re-get values from VehicleExInfo
            if (toolInformation != null)
            {
                result.AppVersion = toolInformation.SoftwareVersion;
                result.UniqueToolID = toolInformation.ToolId;
                result.FirmwareVersion = toolInformation.FirmwareVersion;
            }

            //Re-get values from VehicleExInfo

            result.CompletedMonitors = string.Join(", ",
                GetMonitorsByStatus(monitors, MonitorStatusKey.Complete.ToString()));
            result.InCompleteMonitors = string.Join(", ",
                GetMonitorsByStatus(monitors, MonitorStatusKey.NotComplete.ToString()));

            result.MonitorStatuses = GetAllMonitorsByStatus(monitors);

            result.MILStatus = GetMILStatus(monitors);

            //#PayloadVIN
            result.VIN = vehicleExInfo.Vin;

            //Re-get values from VehicleExInfo
            result.ProductId = vehicleExInfo.ProductID;

            if (!string.IsNullOrEmpty(vehicleExInfo.Odometer))
            {
                ////Remove all whitespace from a string
                result.Odometer = System.Text.RegularExpressions.Regex.Match(vehicleExInfo.Odometer, @"\d+").Value;
            }

            /*******************************************************************************************
            * LEDStatus Data
            ********************************************************************************************/
            if (vehicleExInfo.FWBLVerLedStatus != null)
            {
                result.LEDStatus = vehicleExInfo.FWBLVerLedStatus.LedStatus ?? string.Empty;
            }

            /*******************************************************************************************
            * SIL + ABSLight Data
            ********************************************************************************************/
            result.ABSLightStatus = vehicleExInfo.ABSLightStatus ?? "";
            result.SILStatus = vehicleExInfo.SIL ?? "";

            /*******************************************************************************************
            * Record Time Total Ms Data
            ********************************************************************************************/
            var recordTimeTotalMs = 0;
            if (vehicleExInfo.RecordTime != null)
            {
                recordTimeTotalMs += GetIntValue(vehicleExInfo.RecordTime.ABS);
                recordTimeTotalMs += GetIntValue(vehicleExInfo.RecordTime.ABS_LIGHT);
                recordTimeTotalMs += GetIntValue(vehicleExInfo.RecordTime.ECM_PCM);
                recordTimeTotalMs += GetIntValue(vehicleExInfo.RecordTime.OBD2);
                recordTimeTotalMs += GetIntValue(vehicleExInfo.RecordTime.ODO);
                recordTimeTotalMs += GetIntValue(vehicleExInfo.RecordTime.OFM_ITEMS);
                recordTimeTotalMs += GetIntValue(vehicleExInfo.RecordTime.SIL);
                recordTimeTotalMs += GetIntValue(vehicleExInfo.RecordTime.TCM);
                recordTimeTotalMs += GetIntValue(vehicleExInfo.RecordTime.TPMS);
                recordTimeTotalMs += GetIntValue(vehicleExInfo.RecordTime.TPMS_LIGHT);
            }
            result.RecordTimeTotal = recordTimeTotalMs;

            /*******************************************************************************************
            * CEL DTC Data
            ********************************************************************************************/
            result.CELDTC = vehicleExInfo.PrimaryDtc ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(vehicleExInfo.PrimaryDtc))
            {
                var dtc = vehicleExInfo.AllDtcs.FirstOrDefault(c => string.Equals(vehicleExInfo.PrimaryDtc, c.Code));

                result.CELDTCGotDefCount = dtc != null && !string.IsNullOrWhiteSpace(dtc.Definition) ? 1 : 0;
                result.CELDTCCount = 1;
            }

            /*******************************************************************************************
            * ABS DTCs Data
            ********************************************************************************************/
            result.OEMDtcStateString = string.Join("$", vehicleExInfo.AllDtcs.Select(a => $"{a.Code} : {string.Join(",", a.Types)}"));

            result.OEMDtcRepairStatusString = string.Join("|", vehicleExInfo.AllDtcs.Select(a => $"{a.Code} : {a.RepairStatus ?? ""}"));

            var absDtcs = vehicleExInfo.AllDtcs.Where(c => c.Group == "ABS");
            result.ABSDTCGotDefCount = vehicleExInfo.AllDtcs.Where(c => c.Group == "ABS" && !string.IsNullOrWhiteSpace(c.Definition)).ToList().Count;
            if (absDtcs != null && absDtcs.Any())
                result.ABSDTCCount = absDtcs.Count();

            //Abs dtc defs string
            result.ABSDTCDefsString = string.Join("|", absDtcs.Select(a => $"{a.Code} : {(a.Definition ?? "").Length}"));
            //Abs dtc defs string

            //Abs dtc repair status string
            result.ABSDTCRepairStatusString = string.Join("|", absDtcs.Select(a => $"{a.Code} : {a.RepairStatus ?? ""}"));
            //Abs dtc repair status string

            result.ABSStateString = string.Join("$", absDtcs.Select(a => $"{a.Code} : {string.Join(",", a.Types)}"));

            /*******************************************************************************************
            * SRS DTCs Data
            ********************************************************************************************/
            var srsDtcs = vehicleExInfo.AllDtcs.Where(c => c.Group == "SRS");
            result.SRSDTCGotDefCount = vehicleExInfo.AllDtcs.Where(c => c.Group == "SRS" && !string.IsNullOrWhiteSpace(c.Definition)).ToList().Count;
            if (srsDtcs != null && srsDtcs.Any())
            {
                result.SRSDTCCount = absDtcs.Count();
            }

            //srs dtc defs string
            result.SRSDTCDefsString = string.Join("|", srsDtcs.Select(a => $"{a.Code} : {(a.Definition ?? "").Length}"));
            //srs dtc defs string

            //srs dtc repair status string
            result.SRSDTCRepairStatusString = string.Join("|", srsDtcs.Select(a => $"{a.Code} : {a.RepairStatus ?? ""}"));
            //srs dtc repair status string

            /*******************************************************************************************
            * YMME Data if possible
            ********************************************************************************************/
            if (vehicleExInfo != null && vehicleExInfo.VinProfile != null)
            {
                result.Year = vehicleExInfo.VinProfile.YearString ?? "";
                result.Make = vehicleExInfo.VinProfile.MakeString ?? "";
                result.Model = vehicleExInfo.VinProfile.ModelString ?? "";
                result.EngineType = vehicleExInfo.VinProfile.EngineString ?? "";
            }

            //Get more fields for FF Master Report V3
            var makeString = string.Empty;
            if (vehicleExInfo != null && vehicleExInfo.VinProfile != null && !string.IsNullOrWhiteSpace(vehicleExInfo.VinProfile.MakeString))
            {
                makeString = vehicleExInfo.VinProfile.MakeString.Trim();
            }

            /*******************************************************************************************
            * TCM DTCs Data
            ********************************************************************************************/
            var tcmDtcs = vehicleExInfo.AllDtcs.Where(c => c.Group == "TCM");
            if (tcmDtcs != null && tcmDtcs.Any())
            {
                result.TCMDTCCount = tcmDtcs.Count();

                //Tcm dtc defs string
                result.TCMDTCDefsString = string.Join("|", tcmDtcs.Select(a => $"{a.Code} : {(a.Definition ?? "").Length}"));
                result.TCMDTCGotDefCount = vehicleExInfo.AllDtcs.Where(c => c.Group == "TCM" && !string.IsNullOrWhiteSpace(c.Definition)).ToList().Count;
                //Tcm dtc defs string

                //Tcm dtc repair status string
                result.TCMDTCRepairStatusString = string.Join("|", tcmDtcs.Select(a => $"{a.Code} : {a.RepairStatus ?? ""}"));
                //Tcm dtc repair status string
            }

            /*******************************************************************************************
            * Battery Data
            ********************************************************************************************/
            if (!string.IsNullOrWhiteSpace(vehicleExInfo.BatteryVoltage))
            {
                var items = vehicleExInfo.BatteryVoltage.Split('V');
                if (items.Length > 0)
                {
                    result.BateryVoltage = items[0].Replace("&#x0;", "") + "V";
                }
            }

            /*******************************************************************************************
            * OFM Data
            ********************************************************************************************/
            var ofmData = InnovaRawPayloadParser.GetOFMItems(vehicleExInfo);

            #region Engine Oil

            var engineOilLife = InnovaRawPayloadParser.GetEngineOilLife(ofmData);
            if (engineOilLife != null)
            {
                result.OilLifeStatus = engineOilLife.value;
            }

            var engineOilLevel = InnovaRawPayloadParser.GetEngineOilLevel(ofmData);
            if (engineOilLevel != null)
            {
                result.OilLevel = engineOilLevel.value;
            }

            #endregion Engine Oil

            #region Brake Life

            var brakePadLife = new List<string>();

            var frontBrake = InnovaRawPayloadParser.GetFrontBrakePadCheck(ofmData);
            if (frontBrake != null)
            {
                brakePadLife.Add($"{frontBrake.itemname}({frontBrake.value}):" +
                    $"{OFMBrakePadCheckManager.GetMessage(makeString, frontBrake.itemname, frontBrake.value)}");
            }

            var rearBrake = InnovaRawPayloadParser.GetRearBrakePadCheck(ofmData);
            if (rearBrake != null)
            {
                brakePadLife.Add($"{rearBrake.itemname}({rearBrake.value}):" +
                    $"{OFMBrakePadCheckManager.GetMessage(makeString, rearBrake.itemname, rearBrake.value)}");
            }

            var brakePadCheck = InnovaRawPayloadParser.GetBrakePadCheck(ofmData);
            if (brakePadCheck != null)
            {
                brakePadLife.Add($"{brakePadCheck.itemname}({brakePadCheck.value}):" +
                    $"{OFMBrakePadCheckManager.GetMessage(makeString, brakePadCheck.itemname, brakePadCheck.value)}");
            }

            if (brakePadLife.Any())
            {
                result.BrakePadLife = string.Join(Environment.NewLine, brakePadLife);
            }

            #endregion Brake Life

            #region TPMS

            var tpms = new List<string>();

            var tirePressureItems = InnovaRawPayloadParser.GetTirePressureStatusItems(ofmData);
            foreach (var tp in tirePressureItems)
            {
                tpms.Add($"{tp.itemname}: {tp.value}");
            }

            if (tpms.Any())
            {
                result.TPMS = string.Join(Environment.NewLine, tpms);
            }

            result.TPMSMILStatus = vehicleExInfo.TPMSLightStatus ?? string.Empty;

            #endregion TPMS

            //Get more fields for FF Master Report V3

            return result;
        }

        private static int GetIntValue(string stringValue)
        {
            if (string.IsNullOrWhiteSpace(stringValue)) return 0;
            int.TryParse(stringValue.Replace("(ms)", "").Trim(), out var intVal);

            return intVal;
        }

        private static string GetMILStatus(MonitorCollection monitors)
        {
            //Or Contains (MIL) only
            if (monitors == null || monitors.Count == 0) return "N/A";

            var firstItem = monitors[0];
            if (firstItem != null
                && string.Equals("Malfunction Indicator Lamp (MIL)", firstItem.Description, StringComparison.OrdinalIgnoreCase))
            {
                return firstItem.Value;
            }

            foreach (Monitor m in monitors)
            {
                if (string.Equals("Malfunction Indicator Lamp (MIL)", m.Description, StringComparison.OrdinalIgnoreCase))
                {
                    return m.Value;
                }
            }

            return "N/A";
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="monitors"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        private static List<string> GetMonitorsByStatus(MonitorCollection monitors, string status)
        {
            if (monitors == null) return new List<string>();

            List<string> result = new List<string>();
            foreach (Monitor item in monitors)
            {
                if (item.Value.Contains(status))
                    result.Add(item.Description);
            }

            return result;
        }

        private static string GetAllMonitorsByStatus(MonitorCollection monitors)
        {
            if (monitors == null) return string.Empty;

            List<string> result = new List<string>();

            foreach (Monitor item in monitors)
            {
                result.Add($"{item.Description}: {item.Value}");
            }

            return string.Join(Environment.NewLine, result);
        }
    }
}
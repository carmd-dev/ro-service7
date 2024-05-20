using Innova.DiagnosticReports;
using Innova.Utilities.Shared;
using Innova.Utilities.Shared.Enums;
using Innova.Utilities.Shared.Model;
using Innova2.VehicleDataLib.Entities;
using Innova2.VehicleDataLib.Entities.Version5;
using Innova2.VehicleDataLib.Enums.Vehicle;
using Innova2.VehicleDataLib.Parsing.Version5;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Innova.InnovaDLLParsers
{
    /// <summary>
    /// InnovaRawPayloadParserHelper
    /// </summary>
    public class InnovaRawPayloadParser
    {
        private const string FLEET_PAYLOAD_PREFIX = "__FLEET_PAYLOAD__";

        /// <summary>
        /// Parse Tool Information And Vehicle Data
        /// </summary>
        /// <param name="rawPayloadString"></param>
        /// <param name="currentLanguage"></param>
        /// <returns></returns>
        public static (ToolInformation, VehicleDataEx) ParseToolInformationAndVehicleData(string rawPayloadString, Language currentLanguage)
        {
            var language = GetCurrentLanguageByInnova2VehicleDataLib(currentLanguage);

            ToolInformation toolInformation = ToolInformationParser.Parse(rawPayloadString.Replace(FLEET_PAYLOAD_PREFIX, string.Empty), 0, language, out object vehicleDataExObj);

            return (toolInformation, (VehicleDataEx)vehicleDataExObj);
        }

        /// <summary>
        /// Parse Vehicle Data
        /// </summary>
        /// <param name="rawPayloadString"></param>
        /// <param name="currentLanguage"></param>
        /// <returns></returns>
        public static VehicleDataEx ParseVehicleData(string rawPayloadString, Language currentLanguage)
        {
            var language = GetCurrentLanguageByInnova2VehicleDataLib(currentLanguage);

            var vehicleDataEx = VehicleDataParser.Parse(Convert.FromBase64String(rawPayloadString), language, "0", true);

            return vehicleDataEx;
        }

        /// <summary>
        /// Get Current Language By Innova2.VehicleDataLib
        /// </summary>
        /// <param name="currentLanguage"></param>
        /// <returns></returns>
        private static Innova2.VehicleDataLib.Enums.Common.Language GetCurrentLanguageByInnova2VehicleDataLib(Language currentLanguage)
        {
            var language = Innova2.VehicleDataLib.Enums.Common.Language.English;

            switch (currentLanguage)
            {
                case Language.English:
                    language = Innova2.VehicleDataLib.Enums.Common.Language.English;
                    break;

                case Language.French:
                    language = Innova2.VehicleDataLib.Enums.Common.Language.French;
                    break;

                case Language.SpanishMX:
                    language = Innova2.VehicleDataLib.Enums.Common.Language.Spanish;
                    break;
            }

            return language;
        }

        /// <summary>
        /// FWBLLedStatus
        /// </summary>
        /// <param name="vehicleDataEx"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static ToolLEDStatus FWBLLedStatus(VehicleDataEx vehicleDataEx)
        {
            var ledStatus = string.Empty;
            if (vehicleDataEx != null && vehicleDataEx.FWBLVerLedStatus != null && !string.IsNullOrWhiteSpace(vehicleDataEx.FWBLVerLedStatus.LedStatus))
            {
                ledStatus = vehicleDataEx.FWBLVerLedStatus.LedStatus.ToUpper().Trim();
            }

            //Temporary defined LED Status handling in a case if FWBLVerLedStatus.LedStatus is empty.
            if (string.IsNullOrWhiteSpace(ledStatus) && vehicleDataEx != null)
            {
                ledStatus = vehicleDataEx.ToolLEDStatus.ToString().ToUpper().Trim();
            }

            switch (ledStatus)
            {
                case "RED":
                    return ToolLEDStatus.Red;

                case "YELLOW":
                    return ToolLEDStatus.Yellow;

                case "GREEN":
                    return ToolLEDStatus.Green;

                case "OFF":
                    return ToolLEDStatus.Off;

                case "ERROR":
                    return ToolLEDStatus.Error;

                default:
                    throw new Exception("FWBLVerLedStatus is NULL");
            }
        }

        /// <summary>
        /// SILStatus
        /// </summary>
        /// <param name="vehicleDataEx"></param>
        /// <returns></returns>
        public static string SILStatus(VehicleDataEx vehicleDataEx)
        {
            if (vehicleDataEx != null && !string.IsNullOrWhiteSpace(vehicleDataEx.SIL))
            {
                return vehicleDataEx.SIL;
            }

            return string.Empty;
        }

        /// <summary>
        /// GetOemDtcInfo
        /// </summary>
        /// <param name="vehicleData"></param>
        /// <returns></returns>
        public static OemDtcInfo GetOemDtcInfo(VehicleDataEx vehicleData)
        {
            OemDtcInfo oemDtcInfo = new OemDtcInfo();
            List<string> srsRecommendRepairDtcs = new List<string>();
            AbsDtcInfo absDtcInfo = new AbsDtcInfo { RecommendRepairDtcs = new List<string>() };
            try
            {
                //Try to get SIL Status
                if (vehicleData != null)
                {
                    absDtcInfo.AbsLightStatus = !string.IsNullOrWhiteSpace(vehicleData.ABSLightStatus)
                        ? vehicleData.ABSLightStatus
                        : string.Empty;

                    if (vehicleData.OemData != null && vehicleData.OemData.Dtcs != null)
                    {
                        foreach (var dtc in vehicleData.OemData.Dtcs)
                        {
                            //#IgnoreNOCODE
                            if (string.Equals("No code", dtc.Code, StringComparison.CurrentCultureIgnoreCase))
                            {
                                continue;
                            }

                            if (string.Equals("ABS", dtc.Group, StringComparison.OrdinalIgnoreCase))
                            {
                                if (!string.IsNullOrWhiteSpace(dtc.RepairStatus) &&
                                        (string.Equals("Recommend Repair", dtc.RepairStatus, StringComparison.OrdinalIgnoreCase)
                                                || string.Equals("Recomendar Reparación", dtc.RepairStatus, StringComparison.OrdinalIgnoreCase)
                                        )
                                   )
                                {
                                    absDtcInfo.RecommendRepairDtcs.Add(dtc.Code.ToUpper());
                                }
                            }
                            else if (string.Equals("SRS", dtc.Group, StringComparison.OrdinalIgnoreCase))
                            {
                                if (!string.IsNullOrWhiteSpace(dtc.RepairStatus) &&
                                        (string.Equals("Recommend Repair", dtc.RepairStatus, StringComparison.OrdinalIgnoreCase)
                                                || string.Equals("Recomendar Reparación", dtc.RepairStatus, StringComparison.OrdinalIgnoreCase)
                                        )
                                   )
                                {
                                    srsRecommendRepairDtcs.Add(dtc.Code.ToUpper());
                                }
                            }
                        }
                    }
                }

                oemDtcInfo.AbsDtcInfo = absDtcInfo;
                oemDtcInfo.SrsRecommendRepairDtcs = srsRecommendRepairDtcs;
            }
            catch
            {
            }
            return oemDtcInfo;
        }

        /// <summary>
        /// GetOFMItems
        /// </summary>
        /// <param name="vehicleDataEx"></param>
        /// <returns></returns>
        public static List<OFMItem> GetOFMItems(VehicleDataEx vehicleDataEx)
        {
            var ofmItems = new List<OFMItem>();

            if (vehicleDataEx == null)
            {
                return ofmItems;
            }

            if (vehicleDataEx?.OFM?.OfmItems != null && vehicleDataEx.OFM.OfmItems.Any())
            {
                ofmItems = vehicleDataEx.OFM.OfmItems.ToList();
            }

            return ofmItems;
        }

        #region Tpms

        /// <summary>
        /// GetTirePressureItems => Tpms
        /// </summary>
        /// <param name="ofmItems"></param>
        /// <returns></returns>
        public static List<OFMItem> GetTirePressureItems(List<OFMItem> ofmItems)
        {
            return ofmItems.Where(o => o.itemname.ToLower().Contains("tire pressure")).ToList();
        }

        /// <summary>
        /// GetTirePressureStatusItems
        /// </summary>
        /// <param name="ofmItems"></param>
        /// <returns></returns>
        public static List<OFMItem> GetTirePressureStatusItems(List<OFMItem> ofmItems)
        {
            return ofmItems.Where(o => o.itemname.ToLower().Contains("tire pressure status")).ToList();
        }

        #endregion Tpms

        #region Brake Life

        /// <summary>
        /// GetBrakePadCheck
        /// </summary>
        /// <param name="ofmItems"></param>
        /// <returns></returns>
        public static OFMItem GetBrakePadCheck(List<OFMItem> ofmItems)
        {
            return ofmItems.FirstOrDefault(o => string.Equals("Brake Pad Check", o.itemname.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// GetFrontBrakePadCheck
        /// </summary>
        /// <param name="ofmItems"></param>
        /// <returns></returns>
        public static OFMItem GetFrontBrakePadCheck(List<OFMItem> ofmItems)
        {
            return ofmItems.FirstOrDefault(o => string.Equals("Front Brake Pad Check", o.itemname.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// GetRearBrakePadCheck
        /// </summary>
        /// <param name="ofmItems"></param>
        /// <returns></returns>
        public static OFMItem GetRearBrakePadCheck(List<OFMItem> ofmItems)
        {
            return ofmItems.FirstOrDefault(o => string.Equals("Rear Brake Pad Check", o.itemname.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        #endregion Brake Life

        #region Engine Oil

        /// <summary>
        /// GetEngineOilLevel
        /// </summary>
        /// <param name="ofmItems"></param>
        /// <returns></returns>
        public static OFMItem GetEngineOilLevel(List<OFMItem> ofmItems)
        {
            return ofmItems.FirstOrDefault(o => o.itemdescription_enum == 1079 || string.Equals("Engine Oil Level", o.itemname.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// GetEngineOilLife
        /// </summary>
        /// <param name="ofmItems"></param>
        /// <returns></returns>
        public static OFMItem GetEngineOilLife(List<OFMItem> ofmItems)
        {
            return ofmItems.FirstOrDefault(o => o.itemdescription_enum == 1053 || string.Equals("Oil Life Remaining", o.itemname.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        #endregion Engine Oil

        public static bool IsAbsSupported(VehicleDataEx vehicleDataEx)
        {
            bool result = false;
            if (vehicleDataEx?.OemData?.Dtcs != null)
            {
                result = vehicleDataEx.OemData.Dtcs.Any(c => string.Equals(c.Group, "ABS", StringComparison.OrdinalIgnoreCase));
            }

            return result;
        }

        public static bool IsSrsSupported(VehicleDataEx vehicleDataEx)
        {
            bool result = false;
            if (vehicleDataEx?.OemData?.Dtcs != null)
            {
                result = vehicleDataEx.OemData.Dtcs.Any(c => string.Equals(c.Group, "SRS", StringComparison.OrdinalIgnoreCase));
            }

            return result;
        }

        /// <summary>
        /// TransformMonitors
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static MonitorCollection TransformMonitors(byte[] buffer)
        {
            Dictionary<string, MonitorStatusKey> dictionary = new Dictionary<string, MonitorStatusKey>();
            MonitorCollection monitorCollection = new MonitorCollection();
            var monitorEcuBuffer = VehicleDataParser.GetBufferSegment(buffer, VehicleDataParser.AZBufferIndex.MonitorStatusECU);
            var monitorStatusEcu = MonitorStatusParser.Parse(monitorEcuBuffer);

            var monitorTcuBuffer = VehicleDataParser.GetBufferSegment(buffer, VehicleDataParser.AZBufferIndex.MonitorStatusTCU);
            var monitorStatusTcu = MonitorStatusParser.Parse(monitorTcuBuffer);
            //FF
            var ffEcuBuffer = VehicleDataParser.GetBufferSegment(buffer, VehicleDataParser.AZBufferIndex.FreezeFrameECU);
            var ffStatusEcu = FreezeFrameParser.Parse(ffEcuBuffer);

            var ffTcuBuffer = VehicleDataParser.GetBufferSegment(buffer, VehicleDataParser.AZBufferIndex.FreezeFrameTCU);
            var ffStatusTcu = FreezeFrameParser.Parse(ffTcuBuffer);

            if (!HasMilDTC(ffStatusEcu) && monitorStatusEcu != null && monitorStatusEcu.Count != 0)
            {
                monitorStatusEcu[0].Key = MonitorStatusKey.OFF;
            }
            if (!HasMilDTC(ffStatusTcu) && monitorStatusTcu != null && monitorStatusTcu.Count != 0)
            {
                monitorStatusTcu[0].Key = MonitorStatusKey.OFF;
            }

            //FF

            TransformMonitors(monitorStatusEcu, dictionary);
            TransformMonitors(monitorStatusTcu, dictionary);
            foreach (string key in dictionary.Keys)
            {
                monitorCollection.Add(new Monitor
                {
                    Description = key,
                    Value = dictionary[key].ToString()
                });
            }

            return monitorCollection;
        }

        private static void TransformMonitors(List<MonitorStatusItem> monitors, Dictionary<string, MonitorStatusKey> monitorMap)
        {
            foreach (var monitor in monitors)
            {
                if (!monitorMap.ContainsKey(monitor.Name))
                {
                    monitorMap[monitor.Name] = monitor.Key;
                }
                else if (monitorMap[monitor.Name] == MonitorStatusKey.OFF
                         || monitorMap[monitor.Name] == MonitorStatusKey.NotSupported
                         || (monitorMap[monitor.Name] == MonitorStatusKey.Complete && monitor.Key == MonitorStatusKey.NotComplete))
                {
                    monitorMap[monitor.Name] = monitor.Key;
                }
            }
        }

        //Check if MILDTC in FF presented?
        private static bool HasMilDTC(LiveDatas liveData)
        {
            if (liveData == null || liveData.Count == 0) return false;

            LiveDataItem firstItem = liveData.FirstOrDefault(l =>
                l.Name.StartsWith("DTC that caused required freeze frame data storage",
                    StringComparison.OrdinalIgnoreCase));

            return firstItem?.Values != null && firstItem.Values.Count > 0;
        }
    }
}
using Innova.DiagnosticReports;
using Innova.InnovaDLLParsers;
using Innova.Utilities.Shared;
using Innova.Vehicles;
using Innova.WebServiceV07.RO.DataObjects;
using Innova2.VehicleDataLib.Entities.Version5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Services;

namespace Innova.WebServiceV07.RO
{
    public partial class ServiceV7
    {
        [WebMethod(Description = "Decodes the raw upload string and returns a ToolInformation object and Vehicle information. If an error occurs, an unpopulated ToolInfo object is returned with errors on the field WebServiceSessionStatus")]
        public ToolInfo DecodeRawStringAndGetVehicleInfo(WebServiceKey key, string rawUploadString, int toolTypeFormatInt)
        {
            ToolInfo toolInfo = new ToolInfo();
            WebServiceSessionStatus errors = new WebServiceSessionStatus();
            toolInfo.WebServiceSessionStatus = errors;

            //make sure they have a valid key
            if (!this.ValidateKeyAndLogTransaction(key, "DecodeRawString"))
            {
                errors.AddValidationFailure("000", "Invalid Key");
                return toolInfo;
            }

            ToolInformation toolInformation = null;
            VehicleDataEx vehicleExInfo = null;

            try
            {
                (toolInformation, vehicleExInfo) = InnovaRawPayloadParser.ParseToolInformationAndVehicleData(rawUploadString, Language.English);
            }
            catch (Exception e)
            {
                errors.AddValidationFailure("60000", "An error occurred while decoding the raw upload string within the Innova2.VehicleDataLib.Parsing.dll.  The exception received is : " + e.ToString());
                return toolInfo;
            }

            try
            {
                if (toolInformation != null)
                {
                    //get the new tool information since we've passed the initial validation

                    toolInformation.Vin = XmlHelper.CleanInvalidXmlChars2(toolInformation.Vin);//#SP36

                    toolInfo = ToolInfo.GetWebServiceObject(toolInformation);
                    toolInfo.WebServiceSessionStatus = errors;

                    byte[] raw = Convert.FromBase64String(rawUploadString);

                    if (vehicleExInfo != null)
                    {
                        toolInfo.ProductId = vehicleExInfo.ProductID;

                        VehicleInfoEx vx = new VehicleInfoEx
                        {
                            VehicleInfoVIN = XmlHelper.CleanInvalidXmlChars2(vehicleExInfo.Vin),//#SP36
                            VinProfileVIN = XmlHelper.CleanInvalidXmlChars2(vehicleExInfo.VinProfile.Vin.Trim())
                        };

                        if (!string.IsNullOrEmpty(vehicleExInfo.Odometer))
                        {
                            //Fix issue on ODO invalid unicode charactor
                            vx.Odometer = Regex.Match(vehicleExInfo.Odometer, @"\d+").Value;
                            //Fix issue on ODO invalid unicode charactor
                        }

                        //#Sprint23
                        if (!string.IsNullOrWhiteSpace(vx.VinProfileVIN))
                        {
                            Regex regex = new Regex(@"[^a-zA-Z0-9\s]", (RegexOptions)0);
                            vx.VinProfileVIN = regex.Replace(vx.VinProfileVIN, "");
                        }
                        //#Sprint23
                        toolInfo.VehicleInfoEx = vx;
                    }

                    //if we have a VIN, let's get the VIN here...
                    if (!String.IsNullOrEmpty(toolInformation.Vin))
                    {
                        VehicleInfo v = new VehicleInfo();

                        //now we have to decode the vin (we'll need it for vehicle info)
                        PolkVinDecoder vinDecoder = new PolkVinDecoder(this.Registry);
                        string errorMessage = "";
                        try
                        {
                            //Add new 3/19/2020
                            PolkVehicleYMME pvYmme = null;
                            if (vinDecoder.IsVinAValidMaskPattern(toolInformation.Vin))
                                pvYmme = vinDecoder.DecodeVIN(toolInformation.Vin, false);
                            else //or if the requested VIN is a full VIN then we need to validate the VIN
                                pvYmme = vinDecoder.DecodeVIN(toolInformation.Vin, true);
                            //Add new 3/19/2020

                            if (pvYmme != null)
                            {
                                VehicleInfo.PopulateVehicleInfoFromPolkVehicle(v, pvYmme, toolInformation.Vin);
                                toolInfo.VehicleInfo = v;
                            }
                            else
                            {
                                v.ValidationFailures = new ValidationFailure[1];
                                v.ValidationFailures[0] = new ValidationFailure();
                                v.ValidationFailures[0].Code = "415";
                                v.ValidationFailures[0].Description = "The VIN supplied does not appear to be valid.";
                                v.IsValid = false;

                                errors.AddValidationFailure("415", errorMessage);
                            }
                        }
                        catch (Exception ex)
                        {
                            errorMessage = "There was an error decoding the vin returned by the tool with exception: " + ex.ToString();
                        }

                        if (errorMessage.Length > 0 || vinDecoder == null)
                        {
                            v.ValidationFailures = new ValidationFailure[1];
                            v.ValidationFailures[0] = new ValidationFailure();
                            v.ValidationFailures[0].Code = "420";
                            v.ValidationFailures[0].Description = errorMessage;
                            v.IsValid = false;

                            errors.AddValidationFailure("420", errorMessage);
                        }
                    }

                    AddErrorCodeDefinitonTools(toolInfo);
                }
            }
            catch (Exception ex)
            {
                errors.AddValidationFailure("9999", $"Error occurs when you decode raw payload and get vehicle information => Exception: {ex}");
            }

            return toolInfo;
        }

        /// <summary>
        /// Get Payload Info
        /// </summary>
        [WebMethod(Description = "Get Payload Info V02")]
        public PayloadInfoMasterV02 GetPayloadInfoV02(WebServiceKey key, string payload)
        {
            PayloadInfoMasterV02 payloadInfoMasterV02 = new PayloadInfoMasterV02();
            WebServiceSessionStatus errors = new WebServiceSessionStatus();
            payloadInfoMasterV02.WebServiceSessionStatus = errors;

            if (!this.ValidateKey(key))
            {
                errors.AddValidationFailure("000", "Invalid Key");
                return payloadInfoMasterV02;
            }

            try
            {
                var result = InnovaPayloadDecoder.DecodePayload(payload);

                payloadInfoMasterV02.PIds = result.PIds;
                payloadInfoMasterV02.Protocol = result.Protocol;
                payloadInfoMasterV02.Address = result.Address;
                payloadInfoMasterV02.Manufacture = result.Manufacture;
                payloadInfoMasterV02.MonitorStatuses = result.MonitorStatuses;
                payloadInfoMasterV02.CallibrationIds = result.CallibrationIds;
                payloadInfoMasterV02.CallibrationVerificationNumbers = result.CallibrationVerificationNumbers;
                payloadInfoMasterV02.IPTItem = result.IPTItem;
                payloadInfoMasterV02.MILStatus = result.MILStatus;
                payloadInfoMasterV02.LEDStatus = result.LEDStatus;
                payloadInfoMasterV02.UniqueToolID = result.UniqueToolID;
                payloadInfoMasterV02.ProductId = result.ProductId;
                payloadInfoMasterV02.AppVersion = result.AppVersion;
                payloadInfoMasterV02.FirmwareVersion = result.FirmwareVersion;
                payloadInfoMasterV02.CompletedMonitors = result.CompletedMonitors;
                payloadInfoMasterV02.InCompleteMonitors = result.InCompleteMonitors;
                payloadInfoMasterV02.SILStatus = result.SILStatus;
                payloadInfoMasterV02.ABSLightStatus = result.ABSLightStatus;
                payloadInfoMasterV02.VIN = result.VIN;
                payloadInfoMasterV02.Odometer = result.Odometer;
                payloadInfoMasterV02.CELDTCCount = result.CELDTCCount;
                payloadInfoMasterV02.CELDTC = result.CELDTC;
                payloadInfoMasterV02.CELDTCGotDefCount = result.CELDTCGotDefCount;
                payloadInfoMasterV02.ABSDTCCount = result.ABSDTCCount;
                payloadInfoMasterV02.ABSDTCGotDefCount = result.ABSDTCGotDefCount;
                payloadInfoMasterV02.ABSDTCDefsString = result.ABSDTCDefsString;
                payloadInfoMasterV02.ABSDTCRepairStatusString = result.ABSDTCRepairStatusString;
                payloadInfoMasterV02.ABSStateString = result.ABSStateString;
                payloadInfoMasterV02.OEMDtcStateString = result.OEMDtcStateString;
                payloadInfoMasterV02.OEMDtcRepairStatusString = result.OEMDtcRepairStatusString;
                payloadInfoMasterV02.SRSDTCCount = result.SRSDTCCount;
                payloadInfoMasterV02.SRSDTCGotDefCount = result.SRSDTCGotDefCount;
                payloadInfoMasterV02.SRSDTCDefsString = result.SRSDTCDefsString;
                payloadInfoMasterV02.SRSDTCRepairStatusString = result.SRSDTCRepairStatusString;
                payloadInfoMasterV02.TCMDTCCount = result.TCMDTCCount;
                payloadInfoMasterV02.TCMDTCGotDefCount = result.TCMDTCGotDefCount;
                payloadInfoMasterV02.TCMDTCDefsString = result.TCMDTCDefsString;
                payloadInfoMasterV02.TCMDTCRepairStatusString = result.TCMDTCRepairStatusString;
                payloadInfoMasterV02.RecordTimeTotal = result.RecordTimeTotal;
                payloadInfoMasterV02.OilLifeStatus = result.OilLifeStatus;
                payloadInfoMasterV02.OilLevel = result.OilLevel;
                payloadInfoMasterV02.BrakePadLife = result.BrakePadLife;
                payloadInfoMasterV02.BateryVoltage = result.BateryVoltage;
                payloadInfoMasterV02.TPMS = result.TPMS;
                payloadInfoMasterV02.TPMSMILStatus = result.TPMSMILStatus;
                payloadInfoMasterV02.Year = result.Year;
                payloadInfoMasterV02.Make = result.Make;
                payloadInfoMasterV02.Model = result.Model;
                payloadInfoMasterV02.EngineType = result.EngineType;
            }
            catch (Exception ex)
            {
                errors.AddValidationFailure("9999", $"Error occurs when you decode raw payload => Exception: {ex}");
            }

            return payloadInfoMasterV02;
        }

        /// <summary>
        /// Add ErrorCode Definitions to ToolInfo - Added by INNOVA DEV TEAM 6/20/2017
        /// </summary>
        /// <param name="toolInfo"></param>
        private void AddErrorCodeDefinitonTools(ToolInfo toolInfo)
        {
            var allCodes = new List<string>();
            if (!string.IsNullOrWhiteSpace(toolInfo.MilDTC))
                allCodes.Add(toolInfo.MilDTC);
            allCodes.AddRange(toolInfo.EnhancedCodes ?? new string[] { });
            allCodes.AddRange(toolInfo.PendingABSCodes ?? new string[] { });
            allCodes.AddRange(toolInfo.PendingOBD1Codes ?? new string[] { });
            allCodes.AddRange(toolInfo.PendingSRSCodes ?? new string[] { });
            allCodes.AddRange(toolInfo.StoredABSCodes ?? new string[] { });
            allCodes.AddRange(toolInfo.StoredOBD1Codes ?? new string[] { });
            allCodes.AddRange(toolInfo.StoredSRSCodes ?? new string[] { });
            allCodes.AddRange(toolInfo.PendingPowerTrains ?? new string[] { });
            allCodes.AddRange(toolInfo.StoredPowerTrains ?? new string[] { });
            allCodes.AddRange(toolInfo.PermanentPowerTrains ?? new string[] { });

            allCodes = allCodes.Distinct().ToList();

            if (allCodes.Any())
            {
                //
                DiagnosticReportErrorCodeDefinition ed = new DiagnosticReportErrorCodeDefinition();
                ed.Registry = this.Registry;

                var defs = new List<DiagnosticReportResultErrorCodeDefinition>();
                if (toolInfo.VehicleInfo != null)
                {
                    defs = ed.GetDefinitions(
                        allCodes,
                        int.Parse(!string.IsNullOrEmpty(toolInfo.VehicleInfo.Year) ? toolInfo.VehicleInfo.Year : "0"), //Updated on 2017-08-1  3:40 PM by INNOVA Dev Team to fix the NULL issue to the Vehicle year reported by Tom Chang
                        toolInfo.VehicleInfo.Make,
                        toolInfo.VehicleInfo.Model,
                        toolInfo.VehicleInfo.Transmission,
                        toolInfo.VehicleInfo.TrimLevel
                        );
                }
                else
                {
                    defs = ed.GetDefinitions(allCodes, null, string.Empty, string.Empty, string.Empty,
                        string.Empty);
                }

                if (defs.Any())
                {
                    toolInfo.ErrorCodeInfoDefinitionTools = ErrorCodeInfoDefinitionTool.GetWebServiceObjects(defs, false);
                }
                else
                {
                    toolInfo.ErrorCodeInfoDefinitionTools = new List<ErrorCodeInfoDefinitionTool>();
                }
            }
        }
    }
}
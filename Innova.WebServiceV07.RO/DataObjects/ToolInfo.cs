using Innova.Utilities.Shared;
using Innova.Utilities.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Innova.WebServiceV07.RO.DataObjects
{
    /// <summary>
    /// The tool information object displays the basic information retrieved from the tool info.
    /// </summary>
    public class ToolInfo
    {
        /// <summary>
        /// The <see cref="string"/> vin decoded from the raw upload string
        /// </summary>
        public string Vin = null;

        /// <summary>
        /// The <see cref="string"/> tool ID (normally a valid GUID)
        /// </summary>
        public string ToolId = null;

        /// <summary>
        /// The <see cref="string"/> Product ID (normally a valid Hex code)
        /// </summary>
        public string ProductId = null;

        /// <summary>
        /// The <see cref="string"/> firmware version decoded from the raw upload string sent from the tool.
        /// </summary>
        public string FirmwareVersion = null;

        /// <summary>
        /// The <see cref="string"/> software version decoded from the raw upload string sent from the tool.
        /// </summary>
        public string SoftwareVersion = null;

        /// <summary>
        /// The <see cref="int"/> value of the SoftwareType enumeration (from Innova.Utilities) of the software type reported within the raw upload string sent from the tool.
        /// </summary>
        public int? SoftwareType = null;

        /// <summary>
        /// The <see cref="int"/> value of the ToolTypeFormat enumeration (from Innova.Utilities) of the tool type format reported within the raw upload string sent from the tool.
        /// </summary>
        public int? ToolTypeFormat = null;

        /// <summary>
        /// The <see cref="VehicleInfo"/> decoded from the Vin which is decoded from the raw upload string sent from the tool.  If there is no VIN sent up, the VehicleInfo will be null.
        /// </summary>
        public VehicleInfo VehicleInfo = null;

        /// <summary>
        /// The <see cref="string"/> MilDTC (also known as primary dtc) for power train decoded from the raw upload string sent from the tool.
        /// </summary>
        public string MilDTC = "";

        /// <summary>
        /// The <see cref="string"/> array of power-train stored Dtcs decoded from the raw upload string sent from the tool.
        /// </summary>
        public string[] StoredPowerTrains = null;

        /// <summary>
        /// The <see cref="string"/> array of power-train pending Dtcs decoded from the raw upload string sent from the tool.
        /// </summary>
        public string[] PendingPowerTrains = null;

        /// <summary>
        /// The <see cref="string"/> array of power-train permanent Dtcs decoded from the raw upload string sent from the tool.
        /// </summary>
        public string[] PermanentPowerTrains = null;

        /// <summary>
        /// The <see cref="string"/> array of ABS stored Dtcs decoded from the raw upload string sent from the tool.
        /// </summary>
        public string[] StoredABSCodes = null;

        /// <summary>
        /// The <see cref="string"/> array of ABS pending Dtcs decoded from the raw upload string sent from the tool.
        /// </summary>
        public string[] PendingABSCodes = null;

        /// <summary>
        /// The <see cref="string"/> array of ABS all Dtcs decoded from the raw upload string sent from the tool.
        /// </summary>
        public string[] AllABS = null;

        /// <summary>
        /// The <see cref="string"/> array of SRS stored Dtcs decoded from the raw upload string sent from the tool.
        /// </summary>
        public string[] StoredSRSCodes = null;

        /// <summary>
        /// The <see cref="string"/> array of SRS pending Dtcs decoded from the raw upload string sent from the tool.
        /// </summary>
        public string[] PendingSRSCodes = null;

        /// <summary>
        /// The <see cref="string"/> array of SRS all Dtcs decoded from the raw upload string sent from the tool.
        /// </summary>
        public string[] AllSRS = null;

        /// <summary>
        /// The <see cref="string"/> array of OBD1 stored Dtcs decoded from the raw upload string sent from the tool.
        /// </summary>
        public string[] StoredOBD1Codes = null;

        /// <summary>
        /// The <see cref="string"/> array of OBD1 pending Dtcs decoded from the raw upload string sent from the tool.
        /// </summary>
        public string[] PendingOBD1Codes = null;

        /// <summary>
        /// The <see cref="string"/> array of Enhanced codes.
        /// </summary>
        public string[] EnhancedCodes = null;

        /// <summary>
        /// The <see cref="WebServiceSessionStatus"/> associated with the diagnostic report.  Determines if there were any errors retrieving the diagnostic report including user session problems.
        /// </summary>
        public WebServiceSessionStatus WebServiceSessionStatus;

        //Add ErrorCode Definitions to ToolInfo - Added by INNOVA DEV TEAM 6/20/2017
        /// <summary>
        ///
        /// </summary>
        public List<ErrorCodeInfoDefinitionTool> ErrorCodeInfoDefinitionTools;

        //Monitor/FrezeeFrame update
        //Nam added 5/25/2017: Add Monitors and FrezeeFrames to Tool Information

        /// <summary>
        /// The <see cref="VehicleInfoEx"/> decoded from the Vin which is decoded from the raw upload string sent from the tool.  If there is no VIN sent up, the VehicleInfo will be null.
        /// </summary>
        public VehicleInfoEx VehicleInfoEx = null;

        /// <summary>
        /// The <see cref="ToolInfoEx"/> decoded from the Vin which is decoded from the raw upload string sent from the tool.  If there is no VIN sent up, the VehicleInfo will be null.
        /// </summary>
        public ToolInfoEx ToolInfoEx = null;

        /// <summary>
        /// The array of <see cref="FreezeFrameInfo"/> freeze frame data.
        /// </summary>
        public FreezeFrameInfo[] FreezeFrame;

        /// <summary>
        /// The array of <see cref="MonitorInfo"/> monitor data.
        /// </summary>
        public MonitorInfo[] Monitors;

        //Nam added 5/25/2017: Add Monitors and FrezeeFrames to Tool Information

        /// <summary>
        /// Method updates the web service object from the supplied SDK object
        /// </summary>
        /// <param name="sdkObject"><see cref="Innova.Vehicles.DlcLocation"/> object to create the object from.</param>
        /// <returns><see cref="DLCLocationInfo"/> object created from the supplied SDK object.</returns>
        protected internal static ToolInfo GetWebServiceObject(ToolInformation sdkObject)
        {
            //Added by Nam on 03/29/2017
            ToolInfo wsObject = new ToolInfo
            {
                StoredPowerTrains = new string[] { },
                PendingPowerTrains = new string[] { },
                PermanentPowerTrains = new string[] { },
                StoredABSCodes = new string[] { },
                PendingABSCodes = new string[] { },
                AllABS = new string[] { },
                StoredSRSCodes = new string[] { },
                PendingSRSCodes = new string[] { },
                AllSRS = new string[] { },
                StoredOBD1Codes = new string[] { },
                PendingOBD1Codes = new string[] { },
                EnhancedCodes = new string[] { }
            };

            if (sdkObject == null)
            {
                return wsObject;
            }

            wsObject.Vin = XmlHelper.CleanInvalidXmlChars2(sdkObject.Vin);//#SP36
            wsObject.ToolId = sdkObject.ToolId;

            wsObject.FirmwareVersion = sdkObject.FirmwareVersion;
            wsObject.SoftwareVersion = sdkObject.SoftwareVersion;
            wsObject.SoftwareType = (int)sdkObject.SoftwareType;
            wsObject.ToolTypeFormat = (int)sdkObject.ToolTypeFormat;

            //will be handled outside of this method in a separate call, consolidate the various snippets of code for vehicle decoding
            //into the class later...
            wsObject.VehicleInfo = null;

            wsObject.MilDTC = sdkObject.PrimaryDtc;

            IList<PowerTrain> powerTrainlist = sdkObject.StoredPowerTrains;
            wsObject.StoredPowerTrains = new string[powerTrainlist.Count];
            for (int i = 0; i < powerTrainlist.Count; i++)
            {
                wsObject.StoredPowerTrains[i] = powerTrainlist[i].DTC;
            }
            powerTrainlist = sdkObject.PendingPowerTrains;
            wsObject.PendingPowerTrains = new string[powerTrainlist.Count];
            for (int i = 0; i < powerTrainlist.Count; i++)
            {
                wsObject.PendingPowerTrains[i] = powerTrainlist[i].DTC;
            }

            powerTrainlist = sdkObject.PermanentPowerTrains; //add Permanent DTCs to List
            wsObject.PermanentPowerTrains = new string[powerTrainlist.Count];
            for (int i = 0; i < powerTrainlist.Count; i++)
            {
                wsObject.PermanentPowerTrains[i] = powerTrainlist[i].DTC;
            }
            //Nam updated 4/15/2017 - Wrong values for Permanent Powertrain DTCs

            List<Abs> absList = sdkObject.StoredAbsCodes;
            wsObject.StoredABSCodes = new string[absList.Count];
            for (int i = 0; i < absList.Count; i++)
            {
                wsObject.StoredABSCodes[i] = absList[i].Value;
            }
            absList = sdkObject.PendingAbsCodes;
            wsObject.PendingABSCodes = new string[absList.Count];
            for (int i = 0; i < absList.Count; i++)
            {
                wsObject.PendingABSCodes[i] = absList[i].Value;
            }

            //Nam added 4/12/2017
            if ((sdkObject.AllAbss != null && sdkObject.AllAbss.Any())
                || (sdkObject.AllAbsCodes != null && sdkObject.AllAbsCodes.Any()))
            {
                absList = new List<Abs>();
                if (sdkObject.AllAbss != null) absList.AddRange(sdkObject.AllAbss);
                if (sdkObject.AllAbsCodes != null) absList.AddRange(sdkObject.AllAbsCodes);
                absList = absList.Distinct().ToList();

                wsObject.AllABS = new string[absList.Count];
                for (int i = 0; i < absList.Count; i++)
                {
                    wsObject.AllABS[i] = absList[i].Value;
                }
            }
            //Nam added 4/12/2017

            List<Srs> srsList = sdkObject.StoredSrsCodes;
            wsObject.StoredSRSCodes = new string[srsList.Count];
            for (int i = 0; i < srsList.Count; i++)
            {
                wsObject.StoredSRSCodes[i] = srsList[i].Value;
            }
            srsList = sdkObject.PendingSrsCodes;
            wsObject.PendingSRSCodes = new string[srsList.Count];
            for (int i = 0; i < srsList.Count; i++)
            {
                wsObject.PendingSRSCodes[i] = srsList[i].Value;
            }

            //Nam added 4/12/2017
            if ((sdkObject.AllSrss != null && sdkObject.AllSrss.Any())
              || (sdkObject.AllSrsCodes != null && sdkObject.AllSrsCodes.Any()))
            {
                srsList = new List<Srs>();
                if (sdkObject.AllSrss != null) srsList.AddRange(sdkObject.AllSrss);
                if (sdkObject.AllSrsCodes != null) srsList.AddRange(sdkObject.AllSrsCodes);
                srsList = srsList.Distinct().ToList();

                wsObject.AllSRS = new string[srsList.Count];
                for (int i = 0; i < srsList.Count; i++)
                {
                    wsObject.AllSRS[i] = srsList[i].Value;
                }
            }
            //Nam added 4/12/2017

            List<Obd1Dtc> obd1List = sdkObject.AllObd1Codes;
            wsObject.StoredOBD1Codes = new string[obd1List.Count];
            for (int i = 0; i < obd1List.Count; i++)
            {
                wsObject.StoredOBD1Codes[i] = obd1List[i].Value;
            }

            Innova.DtcCodes.DtcCodeViewCollection enhancedCodeObjects = DiagnosticReports.DiagnosticReport.GetEnhancedDtcsFromToolInformation(sdkObject);

            System.Collections.Specialized.StringCollection enhancedCodes = new System.Collections.Specialized.StringCollection();

            foreach (Innova.DtcCodes.DtcCodeView dcv in enhancedCodeObjects)
            {
                foreach (string code in dcv.Codes)
                {
                    if (!String.IsNullOrEmpty(code) && !enhancedCodes.Contains(code))
                    {
                        enhancedCodes.Add(code);
                    }
                }
            }

            wsObject.EnhancedCodes = new string[enhancedCodes.Count];
            for (int i = 0; i < enhancedCodes.Count; i++)
            {
                wsObject.EnhancedCodes[i] = enhancedCodes[i];
            }

            if (sdkObject.Monitors != null && sdkObject.Monitors.Count > 0)
            {
                wsObject.Monitors = new MonitorInfo[sdkObject.Monitors.Count];

                for (int i = 0; i < sdkObject.Monitors.Count; i++)
                {
                    MonitorInfo mi = new MonitorInfo();

                    mi.Description = sdkObject.Monitors[i].Description;
                    mi.Value = sdkObject.Monitors[i].Value;

                    wsObject.Monitors[i] = mi;
                }
            }
            else
            {
                wsObject.Monitors = new MonitorInfo[] { };
            }

            if (sdkObject.FreezeFrames != null && sdkObject.FreezeFrames.Count > 0)
            {
                //now lets set the freeze frame
                wsObject.FreezeFrame = new FreezeFrameInfo[sdkObject.FreezeFrames.Count];

                for (int i = 0; i < sdkObject.FreezeFrames.Count; i++)
                {
                    FreezeFrameInfo ffi = new FreezeFrameInfo();

                    ffi.Description = sdkObject.FreezeFrames[i].Description;

                    ffi.Value = XmlHelper.CleanInvalidXmlChars2(sdkObject.FreezeFrames[i].Value);

                    wsObject.FreezeFrame[i] = ffi;
                }
            }
            else
            {
                wsObject.FreezeFrame = new FreezeFrameInfo[] { };
            }

            //Monitor/FrezeeFrame update
            //Nam added 5/25/2017: Add Monitors and FrezeeFrames to Tool Information

            return wsObject;
        }
    }
}
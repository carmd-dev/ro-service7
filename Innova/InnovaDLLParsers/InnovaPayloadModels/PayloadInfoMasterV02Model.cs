using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innova.InnovaDLLParsers.InnovaPayloadModels
{
    public class PayloadInfoMasterV02Model
    {
        /// <summary>
        ///
        /// </summary>
        public string PIds { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Protocol { get; set; }

        //Added new 2018-15-05
        /// <summary>
        ///
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Manufacture { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string MonitorStatuses { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string CallibrationIds { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string CallibrationVerificationNumbers { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string IPTItem { get; set; }

        //Added new 2018-15-05

        /// <summary>
        /// MIL Status: ON/OFF
        /// </summary>
        public string MILStatus { get; set; }

        /// <summary>
        /// LED Status: GREEN/RED/YELLOW
        /// </summary>
        public string LEDStatus { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string UniqueToolID { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string AppVersion { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string FirmwareVersion { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string CompletedMonitors { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string InCompleteMonitors { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string SILStatus { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string ABSLightStatus { get; set; }

        /// <summary>
        /// Payload VIN
        /// </summary>
        public string VIN { get; set; }//#PayloadVIN

        /// <summary>
        /// Payload Odometer
        /// </summary>
        public string Odometer { get; set; }//#PayloadVIN

        /// <summary>
        /// Count the total of CEL DTCs
        /// </summary>
        public int CELDTCCount { get; set; } = 0;

        public string CELDTC { get; set; }

        public int CELDTCGotDefCount { get; set; } = 0;

        /// <summary>
        /// Count the total of ABS DTCs
        /// </summary>
        public int ABSDTCCount { get; set; } = 0;

        public int ABSDTCGotDefCount { get; set; } = 0;

        public string ABSDTCDefsString { get; set; }

        public string ABSDTCRepairStatusString { get; set; }

        public string ABSStateString { get; set; }

        public string OEMDtcStateString { get; set; }

        public string OEMDtcRepairStatusString { get; set; }

        public int SRSDTCCount { get; set; } = 0;

        public int SRSDTCGotDefCount { get; set; } = 0;

        public string SRSDTCDefsString { get; set; }

        public string SRSDTCRepairStatusString { get; set; }

        /// <summary>
        /// Count the total of TCM DTCs
        /// </summary>
        public int TCMDTCCount { get; set; } = 0;

        /// <summary>
        /// TCM DTC Def
        /// </summary>
        public int TCMDTCGotDefCount { get; set; } = 0;

        public string TCMDTCDefsString { get; set; } = string.Empty;

        public string TCMDTCRepairStatusString { get; set; }

        public int RecordTimeTotal { get; set; } = 0;

        //For FF Master report V3
        public string OilLifeStatus { get; set; } = string.Empty;

        public string OilLevel { get; set; } = string.Empty;
        public string BrakePadLife { get; set; } = string.Empty;
        public string BateryVoltage { get; set; } = string.Empty;
        public string TPMS { get; set; } = string.Empty;
        public string TPMSMILStatus { get; set; }

        public string Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string EngineType { get; set; }
    }
}
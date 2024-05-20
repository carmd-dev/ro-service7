namespace Innova.DtcCodes
{
    /// <summary>
    /// Summary description for DtcCodeViewDetail.
    /// </summary>
    public class DtcCodeViewDetail
    {
        private string code;
        private string title;
        private string conditions = "";
        private string monitorFile = "";
        private string monitorType = "";
        private string passiveAntiTheftIndicatorLampFile = "";
        private string possibleCauses = "";
        private string serviceThrottleSoonIndicatorLampFile = "";
        private string transmissionControlIndicatorLampFile = "";

        /// <summary>
        /// Contructs an DTC view detail object
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="title">The title.</param>
        public DtcCodeViewDetail(string code, string title)
        {
            this.code = code;
            this.title = title;
        }

        /// <summary>
        /// Gets the <see cref="string"/> DTC Code
        /// </summary>
        public string Code
        {
            get
            {
                return this.code;
            }
        }

        /// <summary>
        /// Get the <see cref="string"/> DTC description
        /// </summary>
        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                this.title = value;
            }
        }

        /// <summary>
        /// Get the <see cref="string"/> DTC description
        /// </summary>
        public string Conditions
        {
            get
            {
                return this.conditions;
            }
            set
            {
                this.conditions = value;
            }
        }

        /// <summary>
        /// Get the <see cref="string"/> Monitor file name.
        /// </summary>
        public string MonitorFile
        {
            get
            {
                return this.monitorFile;
            }
            set
            {
                this.monitorFile = value;
            }
        }

        /// <summary>
        /// Get the <see cref="string"/> Monitor Type
        /// </summary>
        public string MonitorType
        {
            get
            {
                return this.monitorType;
            }
            set
            {
                this.monitorType = value;
            }
        }

        /// <summary>
        /// Get the <see cref="string"/> Passive Anti-Theft Indicator Lamp File
        /// </summary>
        public string PassiveAntiTheftIndicatorLampFile
        {
            get
            {
                return this.passiveAntiTheftIndicatorLampFile;
            }
            set
            {
                this.passiveAntiTheftIndicatorLampFile = value;
            }
        }

        /// <summary>
        /// Get the <see cref="string"/> Possible Causes
        /// </summary>
        public string PossibleCauses
        {
            get
            {
                return this.possibleCauses;
            }
            set
            {
                this.possibleCauses = value;
            }
        }

        /// <summary>
        /// Get the <see cref="string"/> Service Throttle Soon Indicator Lamp File
        /// </summary>
        public string ServiceThrottleSoonIndicatorLampFile
        {
            get
            {
                return this.serviceThrottleSoonIndicatorLampFile;
            }
            set
            {
                this.serviceThrottleSoonIndicatorLampFile = value;
            }
        }

        /// <summary>
        /// Get the <see cref="string"/> Transmission Control Indicator Lamp File
        /// </summary>
        public string TransmissionControlIndicatorLampFile
        {
            get
            {
                return this.transmissionControlIndicatorLampFile;
            }
            set
            {
                this.transmissionControlIndicatorLampFile = value;
            }
        }

        /// <summary>
        /// Gets the <see cref="bool"/> that indicates if this DTC view has detail information.
        /// </summary>
        public bool HasDetailInformation
        {
            get
            {
                return (this.Conditions != ""
                    || this.MonitorFile != ""
                    || this.MonitorType != ""
                    || this.PassiveAntiTheftIndicatorLampFile != ""
                    || this.PossibleCauses != ""
                    || this.ServiceThrottleSoonIndicatorLampFile != ""
                    || this.TransmissionControlIndicatorLampFile != "");
            }
        }
    }
}
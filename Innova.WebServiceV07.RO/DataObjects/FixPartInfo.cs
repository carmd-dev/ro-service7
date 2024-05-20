namespace Innova.WebServiceV07.RO.DataObjects
{
    /// <summary>
    /// The fix part class holds the information for each part that is associated with a fix on the diagnostic report.
    /// </summary>
    public class FixPartInfo
    {
        /// <summary>
        /// Default constructor for the fix part info object.
        /// </summary>
        public FixPartInfo()
        {
        }

        /// <summary>
        /// The <see cref="string"/> ACES part type ID.
        /// </summary>
        public string ACESPartTypeID = "";

        /// <summary>
        /// The <see cref="int"/> quantity of fix parts.
        /// </summary>
        public int Quantity = 0;

        /// <summary>
        /// The <see cref="string"/> name of the part.
        /// </summary>
        public string Name = "";

        /// <summary>
        /// DEPRECATED, only use Name
        /// </summary>
        public string Description = "";

        /// <summary>
        /// The <see cref="string"/> manufacturer name of the part.
        /// </summary>
        public string ManufacturerName = "";

        /// <summary>
        /// The <see cref="string"/> comma delimitted of makes of the part.
        /// </summary>
        public string MakesList = "";

        /// <summary>
        /// The <see cref="string"/> part number.
        /// </summary>
        public string PartNumber = "";

        /// <summary>
        /// The <see cref="string"/> NAPA part number.
        /// </summary>
        public string NapaPartNumber = "";

        /// <summary>
        /// The <see cref="decimal"/> price of the part (in USD).
        /// </summary>
        public decimal Price = 0;

        /// <summary>
        /// The <see cref="string"/> Codemaster ID of the part.
        /// </summary>
        public string CodemasterID = "";

        /// <summary>
        /// The <see cref="FixPartOemInfo"/> Part Oem info of the part.
        /// </summary>
        public FixPartOemInfo[] FixPartOemInfos;

        /// <summary>
        /// Method updates the web service object from the supplied SDK object
        /// </summary>
        /// <param name="sdkObject"><see cref="Innova.DiagnosticReports.DiagnosticReportResultFixPart"/> object to create the object from.</param>
        /// <returns><see cref="FixPartInfo"/> object created from the supplied SDK object.</returns>
        protected internal static FixPartInfo GetWebServiceObject(Innova.DiagnosticReports.DiagnosticReportResultFixPart sdkObject)
        {
            FixPartInfo wsObject = new FixPartInfo();

            if (sdkObject.PartName != null && !string.IsNullOrWhiteSpace(sdkObject.PartName.ACESId))//#ValveTask
            {
                wsObject.ACESPartTypeID = sdkObject.PartName.ACESId;
            }

            //Added on 2020-06-22 12:13 AM by INNOVA Dev Team
            wsObject.Name = sdkObject.Name_Translated;
            wsObject.Description = sdkObject.Name_Translated;
            wsObject.Quantity = sdkObject.Quantity; //Relocated on 2020-06-22 12:13 AM

            wsObject.ManufacturerName = "";
            wsObject.MakesList = sdkObject.MakesList;
            wsObject.PartNumber = sdkObject.PartNumber;

            wsObject.FixPartOemInfos = new FixPartOemInfo[sdkObject.DiagnosticReportResultFixPartPartOems.Count];
            for (int i = 0; i < sdkObject.DiagnosticReportResultFixPartPartOems.Count; i++)
            {
                wsObject.FixPartOemInfos[i] = FixPartOemInfo.GetWebServiceObject(sdkObject.DiagnosticReportResultFixPartPartOems[i]);
            }

            wsObject.Price = sdkObject.Price_Presented;
            wsObject.CodemasterID = sdkObject.CodemasterID;

            return wsObject;
        }

        /// <summary>
        /// Method updates the web service object from the supplied SDK object
        /// </summary>
        /// <param name="sdkObject"><see cref="Innova.Fixes.FixPart"/> object to create the object from.</param>
        /// <returns><see cref="FixPartInfo"/> object created from the supplied SDK object.</returns>
        protected internal static FixPartInfo GetWebServiceObject(Innova.Fixes.FixPart sdkObject)
        {
            FixPartInfo wsObject = new FixPartInfo();

            wsObject.ACESPartTypeID = sdkObject.Part.PartName.ACESId;
            wsObject.Quantity = sdkObject.Quantity;

            wsObject.Name = sdkObject.Part.PartName.Name_Translated;
            wsObject.Description = sdkObject.Part.PartName.Name_Translated;

            wsObject.ManufacturerName = "";
            wsObject.MakesList = sdkObject.Part.MakesAsCommaDelimittedString;
            wsObject.PartNumber = sdkObject.Part.PartNumber;

            wsObject.Price = sdkObject.Part.Price_Presented;
            wsObject.CodemasterID = sdkObject.CodemasterID;

            return wsObject;
        }
    }
}
namespace Innova.WebServiceV07.RO.DataObjects
{
    /// <summary>
    /// The class that contains data for TSB information.
    /// </summary>
    public class TSBInfo
    {
        /// <summary>
        /// Default constructor for the TSB info class
        /// </summary>
        public TSBInfo()
        {
        }

        /// <summary>
        /// The <see cref="int"/> id of the TSB record.
        /// </summary>
        public int TsbId;

        /// <summary>
        /// The <see cref="string"/> description.
        /// </summary>
        public string Description;

        /// <summary>
        /// The <see cref="string"/> file name of the PDF of the TSB.
        /// </summary>
        public string FileNamePDF;

        /// <summary>
        /// The <see cref="string"/> URL to the PDF of the TSB.
        /// </summary>
        public string PDFFileUrl;

        /// <summary>
        /// The <see cref="string"/> manufacturer number.
        /// </summary>
        public string ManufacturerNumber;

        /// <summary>
        /// The <see cref="string"/> date that the TSB was issued.
        /// </summary>
        public string IssueDateString;

        /// <summary>
        /// The <see cref="string"/> text of the TSB.
        /// </summary>
        public string TsbText;

        /// <summary>
        /// The <see cref="string"/> string that the TSB record was created.
        /// </summary>
        public string CreatedDateString;

        /// <summary>
        /// The <see cref="string"/> string that the TSB record was last updated.
        /// </summary>
        public string UpdatedDateString;

        /// <summary>
        /// The <see cref="string"/> system this TSB belongs to.
        /// </summary>
        public string System;

        /// <summary>
        /// The <see cref="string"/> sub-system this TSB belongs to.
        /// </summary>
        public string SubSystem;

        /// <summary>
        /// The <see cref="string"/> auto-system this TSB belongs to.
        /// </summary>
        public string AutoSystem;

        /// <summary>
        /// The array of <see cref="string"/> DTC Codes which are associated to this TSB that were supplied as input to the TSB page span search
        /// </summary>
        public string[] DTCcodes = null;

        /// <summary>
        /// The array of <see cref="TSBTypeInfo"/> objects associated with this TSB
        /// </summary>
        public TSBTypeInfo[] TSBTypes = null;

        /// <summary>
        /// The array of <see cref="TSBCategoryInfo"/> objects associated with this TSB
        /// </summary>
        public TSBCategoryInfo[] TSBCategories = null;

        /// <summary>
        /// Method updates the web service object from the supplied SDK object
        /// </summary>
        /// <param name="sdkObject"><see cref="Innova.Vehicles.Tsb"/> object to create the object from.</param>
        /// <returns><see cref="TSBInfo"/> object created from the supplied SDK object.</returns>
        protected internal static TSBInfo GetWebServiceObject(Innova.Vehicles.Tsb sdkObject)
        {
            TSBInfo wsObject = new TSBInfo();
            wsObject.TsbId = sdkObject.TsbId;
            wsObject.Description = sdkObject.Description;

            wsObject.FileNamePDF = sdkObject.FileNamePDF;
            wsObject.PDFFileUrl = sdkObject.GetTsbPdfUrl(Global.TsbRootUrl);
            wsObject.ManufacturerNumber = sdkObject.ManufacturerNumber;

            wsObject.IssueDateString = sdkObject.IssueDate.ToShortDateString();
            wsObject.TsbText = sdkObject.TsbText;

            wsObject.CreatedDateString = sdkObject.CreatedDateTime.ToShortDateString();
            wsObject.UpdatedDateString = sdkObject.UpdatedDateTime.ToShortDateString();

            wsObject.System = sdkObject.System;
            wsObject.SubSystem = sdkObject.SubSystem;
            wsObject.AutoSystem = sdkObject.AutoSystem;

            if (sdkObject.DTCcodes != null && sdkObject.DTCcodes.Count > 0)
            {
                wsObject.DTCcodes = new string[sdkObject.DTCcodes.Count];

                for (int i = 0; i < sdkObject.DTCcodes.Count; i++)
                {
                    wsObject.DTCcodes[i] = sdkObject.DTCcodes[i];
                }
            }
            if (sdkObject.TsbTypes != null && sdkObject.TsbTypes.Count > 0)
            {
                wsObject.TSBTypes = new TSBTypeInfo[sdkObject.TsbTypes.Count];

                for (int i = 0; i < sdkObject.TsbTypes.Count; i++)
                {
                    wsObject.TSBTypes[i] = TSBTypeInfo.GetWebServiceObject(sdkObject.TsbTypes[i]);
                }
            }

            if (sdkObject.TsbCategories != null && sdkObject.TsbCategories.Count > 0)
            {
                wsObject.TSBCategories = new TSBCategoryInfo[sdkObject.TsbCategories.Count];

                for (int i = 0; i < sdkObject.TsbCategories.Count; i++)
                {
                    wsObject.TSBCategories[i] = TSBCategoryInfo.GetWebServiceObject(sdkObject.TsbCategories[i]);
                }
            }

            return wsObject;
        }
    }
}
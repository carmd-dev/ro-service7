namespace Innova.WebServiceV07.RO.DataObjects
{
    /// <summary>
    /// The fix part class holds the information for each part that is associated with a fix on the diagnostic report.
    /// </summary>
    public class FixToolInfo
    {
        /// <summary>
        /// Default constructor for the fix part info object.
        /// </summary>
        public FixToolInfo()
        {
        }

        /// <summary>
        ///
        /// </summary>
        public string ToolId = "";

        /// <summary>
        /// The <see cref="int"/> quantity of fix tools.
        /// </summary>
        public int Quantity = 0;

        /// <summary>
        /// The <see cref="string"/> manufacturer name of the part.
        /// </summary>
        public string ManufacturerName = "";

        /// <summary>
        /// The <see cref="string"/> comma delimited of makes of the part.
        /// </summary>
        public string MakesList = "";

        /// <summary>
        /// Retailers
        /// </summary>
        public string RetailersList = "";

        /// <summary>
        /// The <see cref="string"/> tool number.
        /// </summary>
        public string ToolNumber = "";

        /// <summary>
        /// The <see cref="string"/> name of the tool.
        /// </summary>
        public string Name = "";

        /// <summary>
        /// DEPRECATED, only use Name
        /// </summary>
        public string Description = "";

        /// <summary>
        /// The <see cref="decimal"/> price of the tool (in USD).
        /// </summary>
        public decimal Price = 0;

        /// <summary>
        /// Method updates the web service object from the supplied SDK object
        /// </summary>
        /// <param name="sdkObject"><see cref="Innova.DiagnosticReports.DiagnosticReportResultFixTool"/> object to create the object from.</param>
        /// <returns><see cref="FixToolInfo"/> object created from the supplied SDK object.</returns>
        protected internal static FixToolInfo GetWebServiceObject(Innova.DiagnosticReports.DiagnosticReportResultFixTool sdkObject)
        {
            FixToolInfo wsObject = new FixToolInfo();
            wsObject.Quantity = sdkObject.Quantity;
            wsObject.ToolId = sdkObject.Tool.Id.ToString();

            wsObject.Name = sdkObject.Name_Translated;
            wsObject.ToolNumber = sdkObject.ToolNumber;
            wsObject.Description = sdkObject.Name_Translated;
            wsObject.ManufacturerName = sdkObject.ManufacturerName; //ToolDB1_, rename Manufacture to Manufacturer
            wsObject.MakesList = sdkObject.MakesList;
            wsObject.RetailersList = sdkObject.RetailersString;

            wsObject.Price = sdkObject.Price_Presented;

            return wsObject;
        }

        /// <summary>
        /// Method updates the web service object from the supplied SDK object
        /// </summary>
        /// <param name="sdkObject"><see cref="Innova.Fixes.FixTool"/> object to create the object from.</param>
        /// <returns><see cref="FixToolInfo"/> object created from the supplied SDK object.</returns>
        protected internal static FixToolInfo GetWebServiceObject(Innova.Fixes.FixTool sdkObject)
        {
            FixToolInfo wsObject = new FixToolInfo();

            wsObject.ToolId = sdkObject.Tool.Id.ToString();
            wsObject.Quantity = sdkObject.Quantity;

            wsObject.Name = sdkObject.Tool.ToolName.Description_Translated;
            wsObject.ToolNumber = sdkObject.Tool.ToolNumber;
            wsObject.Description = sdkObject.Tool.ToolName.Description_Translated;
            wsObject.ManufacturerName = sdkObject.Tool.ManufacturerName;
            wsObject.MakesList = sdkObject.Tool.MakesAsCommaDelimittedString;
            wsObject.RetailersList = string.Join(", ", sdkObject.Tool.Retailers);

            wsObject.Price = sdkObject.Tool.Price_Presented; //in USD, need to convert to current currency

            return wsObject;
        }
    }
}
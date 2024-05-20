namespace Innova.WebServiceV07.RO.DataObjects
{
    /// <summary>
    /// The DLCLocation information class contains DLC Location information that is sent back when DLC location information is requested.
    /// </summary>
    public class DLCLocationInfo
    {
        /// <summary>
        /// The <see cref="string"/> year of the vehicle the DLC Location info is for.
        /// </summary>
        public string Year = "";

        /// <summary>
        /// The <see cref="string"/> make of the vehicle the DLC Location info is for.
        /// </summary>
        public string Make = "";

        /// <summary>
        /// The <see cref="string"/> model of the vehicle the DLC Location info is for.
        /// </summary>
        public string Model = "";

        /// <summary>
        /// The <see cref="int"/> location number the DLC Location info is for.
        /// </summary>
        public int? LocationNumber = null;

        /// <summary>
        /// The <see cref="string"/> access information the DLC Location info is for.
        /// </summary>
        public string Access = "";

        /// <summary>
        /// The <see cref="string"/> comments for the DLC Location.
        /// </summary>
        public string Comments = "";

        /// <summary>
        /// The <see cref="string"/> image file name for the DLC Location.
        /// </summary>
        public string ImageFileName = "";

        /// <summary>
        /// The <see cref="string"/> image file URL for the DLC Location.
        /// </summary>
        public string ImageFileUrl = "";

        /// <summary>
        /// The <see cref="string"/> small image file URL for the DLC Location.
        /// </summary>
        public string ImageFileUrlSmall = "";
    }
}
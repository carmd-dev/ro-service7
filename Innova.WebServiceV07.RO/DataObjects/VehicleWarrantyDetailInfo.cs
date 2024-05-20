namespace Innova.WebServiceV07.RO.DataObjects
{
    /// <summary>
    /// Object holds the vehicle warranty info details
    /// </summary>
    public class VehicleWarrantyDetailInfo
    {
        /// <summary>
        /// The <see cref="string"/> warranty type description
        /// </summary>
        public string WarrantyTypeDescription = "";

        /// <summary>
        /// The <see cref="int"/> Warranty Type (int enum value) 0
        /// </summary>
        public int WarrantyType;

        /// <summary>
        /// The nullable <see cref="int"/> maximum number of years for the warranty detail.
        /// </summary>
        public int? MaxYears = null;

        /// <summary>
        /// The nullable <see cref="int"/> maximum mileage for the warranty detail.
        /// </summary>
        public int? MaxMileage = null;

        /// <summary>
        /// The	<see cref="string"/> notes for the warranty item
        /// </summary>
        public string Notes = "";

        /// <summary>
        /// A <see cref="bool"/> flag indicating if the warranty is transferrable.
        /// </summary>
        public bool IsTransferable;

        /// <summary>
        /// The <see cref="string"/> description formatted (shown on CanOBD2, could change, use as an example only)
        /// </summary>
        public string DescriptionFormatted = "";

        /// <summary>
        /// Method updates the web service object from the supplied SDK object
        /// </summary>
        /// <param name="sdkObject"><see cref="Innova.Vehicles.VehicleWarrantyDetail"/> object to create the object from.</param>
        /// <returns><see cref="VehicleWarrantyDetailInfo"/> object created from the supplied SDK object.</returns>
        protected internal static VehicleWarrantyDetailInfo GetWebServiceObject(Innova.Vehicles.VehicleWarrantyDetail sdkObject)
        {
            VehicleWarrantyDetailInfo wsObject = new VehicleWarrantyDetailInfo();

            wsObject.WarrantyTypeDescription = sdkObject.Registry.GetEnumDescription(sdkObject.WarrantyType);
            wsObject.WarrantyType = (int)sdkObject.WarrantyType;

            if (!sdkObject.MaxYears.IsNull)
            {
                wsObject.MaxYears = sdkObject.MaxYears.Value;
            }
            if (!sdkObject.MaxMileage.IsNull)
            {
                wsObject.MaxMileage = sdkObject.MaxMileage.Value;
            }
            wsObject.Notes = sdkObject.Notes_Translated;
            wsObject.IsTransferable = sdkObject.VehicleWarranty.IsTransferable;

            wsObject.DescriptionFormatted = sdkObject.ToString();

            return wsObject;
        }
    }
}
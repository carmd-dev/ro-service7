namespace Innova.WebServiceV07.RO.DataObjects
{
    /// <summary>
    /// The vehicle information class holds vehicle information for the vehicle that was payload decoded for and for the diagnostic reports.
    /// </summary>
    public class VehicleInfoEx
    {
        /// <summary>
        /// The default constructor for the vehicle info class.
        /// </summary>
        public VehicleInfoEx()
        {
        }

        /// <summary>
        /// The <see cref="string"/> VIN that was decoded or attempted to be decoded.
        /// </summary>
        public string VinProfileVIN = "";

        /// <summary>
        /// The <see cref="string"/> VIN that was decoded or attempted to be decoded.
        /// </summary>
        public string VehicleInfoVIN = "";

        /// <summary>
        /// The <see cref="string"/> Odometer that was decoded or attempted to be decoded.
        /// </summary>
        public string Odometer = "";
    }
}
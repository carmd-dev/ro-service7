namespace Innova.WebServiceV07.RO.DataObjects
{
    /// <summary>
	/// The vehicle information class holds vehicle information for the vehicle that was payload decoded for and for the diagnostic reports.
	/// </summary>
	public class ToolInfoEx
    {
        /// <summary>
        /// The default constructor for the vehicle info class.
        /// </summary>
        public ToolInfoEx()
        {
        }

        /// <summary>
        /// The <see cref="string"/> Product Id that was decoded or attempted to be decoded.
        /// </summary>
        public string ProductId = "";
    }
}
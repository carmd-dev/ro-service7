namespace Innova.WebServiceV07.RO.DataObjects
{
    /// <summary>
    /// Object holds the TSB type information.
    /// </summary>
    public class TSBTypeInfo
    {
        /// <summary>
        /// The <see cref="int"/> Id of the TSB type.
        /// </summary>
        public int Id;

        /// <summary>
        /// The <see cref="string"/> description of the TSB type.
        /// </summary>
        public string Description;

        /// <summary>
        /// Method updates the web service object from the supplied SDK object
        /// </summary>
        /// <param name="sdkObject"><see cref="Innova.Vehicles.TsbType"/> object to create the object from.</param>
        /// <returns><see cref="TSBTypeInfo"/> object created from the supplied SDK object.</returns>
        protected internal static TSBTypeInfo GetWebServiceObject(Innova.Vehicles.TsbType sdkObject)
        {
            TSBTypeInfo wsObject = new TSBTypeInfo();

            wsObject.Id = sdkObject.Id;
            wsObject.Description = sdkObject.Description;

            return wsObject;
        }
    }
}
using Innova.Vehicles;

namespace Innova.WebServiceV07.RO.DataObjects
{
    /// <summary>
    /// Holds the TSB Category information
    /// </summary>
    public class TSBCategoryInfo
    {
        /// <summary>
        /// The <see cref="int"/> Id of the category
        /// </summary>
        public int Id;

        /// <summary>
        /// The <see cref="string"/> description of the category
        /// </summary>
        public string Description;

        /// <summary>
        /// The <see cref="int"/> (nullable) TSB Count
        /// </summary>
        public int? TSBCount;

        /// <summary>
        /// Method updates the web service object from the supplied SDK object
        /// </summary>
        /// <param name="sdkObject"><see cref="TsbCategory"/> object to create the object from.</param>
        /// <returns><see cref="TSBCategoryInfo"/> object created from the supplied SDK object.</returns>
        protected internal static TSBCategoryInfo GetWebServiceObject(TsbCategory sdkObject)
        {
            TSBCategoryInfo wsObject = new TSBCategoryInfo();

            wsObject.Id = sdkObject.Id;
            wsObject.Description = sdkObject.Description;
            wsObject.TSBCount = sdkObject.Count;

            return wsObject;
        }
    }
}
namespace Innova.WebServiceV07.RO.DataObjects
{
    /// <summary>
    /// Class contains the basic vehicle information that the error code applies to.  Used when <see cref="ErrorCodeInfoDefinition"/> objects exist on an <see cref="ErrorCodeInfo"/> exists.
    /// </summary>
    public class ErrorCodeInfoDefinitionVehicle
    {
        /// <summary>
        /// Default constructor for the error code definition.
        /// </summary>
        public ErrorCodeInfoDefinitionVehicle()
        {
        }

        /// <summary>
        /// The <see cref="string"/> engine type the definition applies to.
        /// </summary>
        public string EngineType = "";

        /// <summary>
        /// The <see cref="string"/> body code the defintion applies to.
        /// </summary>
        public string BodyCode = "";
    }
}
namespace Innova.WebServiceV07.RO.DataObjects
{
    /// <summary>
    /// The validation failure object holds information for reasons why a request failed.
    /// </summary>
    public class ValidationFailure
    {
        /// <summary>
        /// Default constructor for the validation failure class
        /// </summary>
        public ValidationFailure()
        {
        }

        /// <summary>
        /// The <see cref="string"/> error code
        /// </summary>
        public string Code = "";

        /// <summary>
        /// The <see cref="string"/> description of the error code.
        /// </summary>
        public string Description = "";
    }
}
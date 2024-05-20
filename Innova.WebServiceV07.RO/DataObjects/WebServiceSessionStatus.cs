namespace Innova.WebServiceV07.RO.DataObjects
{
    /// <summary>
    /// The session class handles general session availability.
    /// </summary>
    public class WebServiceSessionStatus
    {
        /// <summary>
        /// Default constructor web service session status.
        /// </summary>
        public WebServiceSessionStatus()
        {
            this.ValidationFailures = new ValidationFailure[0];
        }

        /// <summary>
        /// The array of <see cref="ValidationFailure"/> objects holding validation failure codes and descriptions.
        /// </summary>
        public ValidationFailure[] ValidationFailures = null;

        /// <summary>
        /// Adds validation failure to the existing validation failures
        /// </summary>
        /// <param name="code"><see cref="string"/> code to add.</param>
        /// <param name="description"><see cref="string"/> description of the validation error</param>
        protected internal void AddValidationFailure(string code, string description)
        {
            ValidationFailure[] previous = this.ValidationFailures;

            this.ValidationFailures = new ValidationFailure[previous.Length + 1];

            for (int i = 0; i < previous.Length; i++)
            {
                this.ValidationFailures[i] = previous[i];
            }

            ValidationFailure vf = new ValidationFailure();
            vf.Code = code;
            vf.Description = description;

            //add the newest failure to the end of the list
            ValidationFailures[this.ValidationFailures.Length - 1] = vf;
        }
    }
}
using Innova.Symptoms;

namespace Innova.Fixes
{
    /// <summary>
    /// Class holding the DTC Codes which are attached to the fix
    /// </summary>
    public class FixSymptom
    {
        /// <summary>
		/// Default constructor for the DTC Object
		/// </summary>
		public FixSymptom()
        {
        }

        /// <summary>
        ///
        /// </summary>
        public FixSymptom(Fix fix, Symptom symptom)
        {
            this.Fix = fix;
            this.Symptom = symptom;
        }

        /// <summary>
        /// Gets or sets the <see cref="Fix"/> this <see cref="FixDTC"/> object is associated to.
        /// </summary>
        public Fix Fix { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Symptom"/> symptom associated with this <see cref="Fix"/> record
        /// </summary>
        public Symptom Symptom { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="symptom"></param>
        public bool Equals(FixSymptom symptom)
        {
            if (symptom == null)
            {
                return false;
            }

            return this.Fix.Id == symptom.Fix.Id && this.Symptom.Id == symptom.Symptom.Id;
        }
    }
}
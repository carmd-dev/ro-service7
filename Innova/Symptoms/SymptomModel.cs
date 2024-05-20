namespace Innova.Symptoms
{
    /// <summary>
    ///
    /// </summary>
    public class SymptomModel
    {
        /// <summary>
		///
		/// </summary>
		public SymptomModel()
        {
            this.Model = "";
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="symptom"></param>
        /// <param name="model"></param>
        public SymptomModel(Symptom symptom, string model)
        {
            this.Symptom = symptom;
            this.Model = model;
        }

        /// <summary>
        ///
        /// </summary>
        public Symptom Symptom { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="des"></param>
        /// <returns></returns>
        public bool Equals(SymptomModel des)
        {
            if (des == null)
            {
                return false;
            }

            if (this.Symptom.Id != des.Symptom.Id)
            {
                return false;
            }

            return this.Model == des.Model;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string stringValue = this.Model;
            return stringValue;
        }
    }
}
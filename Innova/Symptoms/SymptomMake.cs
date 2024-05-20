namespace Innova.Symptoms
{
    /// <summary>
    ///
    /// </summary>
    public class SymptomMake
    {
        /// <summary>
		///
		/// </summary>
		public SymptomMake()
        {
            this.Make = "";
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="symptom"></param>
        /// <param name="make"></param>
        public SymptomMake(Symptom symptom, string make)
        {
            this.Symptom = symptom;
            this.Make = make;
        }

        /// <summary>
        ///
        /// </summary>
        public Symptom Symptom { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Make { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="des"></param>
        /// <returns></returns>
        public bool Equals(SymptomMake des)
        {
            if (des == null)
            {
                return false;
            }

            if (this.Symptom.Id != des.Symptom.Id)
            {
                return false;
            }

            return this.Make == des.Make;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string stringValue = this.Make;
            return stringValue;
        }
    }
}
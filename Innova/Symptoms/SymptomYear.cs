namespace Innova.Symptoms
{
    /// <summary>
    ///
    /// </summary>
    public class SymptomYear
    {
        /// <summary>
		///
		/// </summary>
		public SymptomYear()
        {
            this.Year = 0;
        }

        /// <summary>
        ///
        /// </summary>
        public SymptomYear(Symptom symptom, int year)
        {
            this.Symptom = symptom;
            this.Year = year;
        }

        /// <summary>
        ///
        /// </summary>
        public Symptom Symptom { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="des"></param>
        /// <returns></returns>
        public bool Equals(SymptomYear des)
        {
            if (des == null)
            {
                return false;
            }

            if (this.Symptom.Id != des.Symptom.Id)
            {
                return false;
            }

            return this.Year == des.Year;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Year.ToString();
        }
    }
}
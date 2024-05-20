namespace Innova.Symptoms
{
    /// <summary>
    ///
    /// </summary>
    public class SymptomEngineType
    {
        /// <summary>
		///
		/// </summary>
		public SymptomEngineType()
        {
            this.EngineType = "";
        }

        /// <summary>
        ///
        /// </summary>
        public SymptomEngineType(Symptom symptom, string engineType)
        {
            this.Symptom = symptom;
            this.EngineType = engineType;
        }

        /// <summary>
        ///
        /// </summary>
        public Symptom Symptom { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string EngineType { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="des"></param>
        /// <returns></returns>
        public bool Equals(SymptomEngineType des)
        {
            if (des == null)
            {
                return false;
            }

            if (this.Symptom.Id != des.Symptom.Id)
            {
                return false;
            }

            return this.EngineType == des.EngineType;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string stringValue = this.EngineType;
            return stringValue;
        }
    }
}
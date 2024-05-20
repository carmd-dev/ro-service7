namespace Innova.DiagnosticReports
{
    /// <summary>
    /// Collection of <see cref="PossibleCause"/> objects associated with the error codes
    /// </summary>
    public class PossibleCauseCollection : Metafuse3.BusinessObjects.SmartCollectionBase
    {
        /// <summary>
        /// Default constructor for the possible cause collection.
        /// </summary>
        public PossibleCauseCollection()
        {
        }

        /// <summary>
        /// Indexer, used to return the <see cref="PossibleCause"/> located at the index (<see cref="int"/>) position of the list.
        /// </summary>
        public PossibleCause this[int index]
        {
            get
            {
                return (PossibleCause)List[index];
            }
        }

        /// <summary>
        /// Adds a <see cref="PossibleCause"/> object to the list.
        /// </summary>
        /// <param name="possibleCause"><see cref="PossibleCause"/> to add.</param>
        public void Add(PossibleCause possibleCause)
        {
            base.Add(possibleCause);
        }
    }
}
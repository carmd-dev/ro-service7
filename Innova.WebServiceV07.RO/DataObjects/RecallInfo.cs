namespace Innova.WebServiceV07.RO.DataObjects
{
    /// <summary>
    /// The class that contains data for recall information.
    /// </summary>
    public class RecallInfo
    {
        /// <summary>
        /// Default constructor for the recall info class
        /// </summary>
        public RecallInfo()
        {
        }

        /// <summary>
        /// The <see cref="int"/> record number.
        /// </summary>
        public int RecordNumber;

        /// <summary>
        /// The <see cref="string"/> campaign number.
        /// </summary>
        public string CampaignNumber;

        /// <summary>
        /// The <see cref="string"/> date of the recall.
        /// </summary>
        public string RecallDateString;

        /// <summary>
        /// The <see cref="string"/> description of the defect..
        /// </summary>
        public string DefectDescription;

        /// <summary>
        /// The <see cref="string"/> consequnce of the defect.
        /// </summary>
        public string DefectConsequence;

        /// <summary>
        /// The <see cref="string"/> corrective action required to fix the defect.
        /// </summary>
        public string DefectCorrectiveAction;

        /// <summary>
        /// Method updates the web service object from the supplied SDK object
        /// </summary>
        /// <param name="sdkObject"><see cref="Innova.Vehicles.Recall"/> object to create the object from.</param>
        /// <returns><see cref="RecallInfo"/> object created from the supplied SDK object.</returns>
        protected internal static RecallInfo GetWebServiceObject(Innova.Vehicles.Recall sdkObject)
        {
            RecallInfo wsObject = new RecallInfo();

            wsObject.CampaignNumber = sdkObject.CampaignNumber;
            wsObject.DefectConsequence = sdkObject.DefectConsequence_Translated;
            wsObject.DefectCorrectiveAction = sdkObject.DefectCorrectiveAction_Translated;
            wsObject.DefectDescription = sdkObject.DefectDescription_Translated;

            wsObject.RecallDateString = sdkObject.RecallDate.ToShortDateString();
            wsObject.RecordNumber = sdkObject.RecordNumber;

            return wsObject;
        }
    }
}
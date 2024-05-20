using System.ComponentModel;

namespace Innova.Symptoms
{
    /// <summary>
    /// The symptom fragment type used to build symptoms
    /// </summary>
    public enum SymptomFragmentType
    {
        /// <summary>
        /// Type
        /// </summary>
        [Description("Type")]
        Type = 0,

        /// <summary>
        /// ObservedEvent
        /// </summary>
        [Description("Observed Event")]
        ObservedEvent = 1,

        /// <summary>
        /// Location
        /// </summary>
        [Description("Location")]
        Location = 2,

        /// <summary>
        /// Condition
        /// </summary>
        [Description("Operational Condition")]
        OperationalCondition = 3,

        /// <summary>
        /// Survey/Technical Inspection
        /// </summary>
        [Description("Survey/Technical Inspection")]
        SurveyTechnicalInspection = 4,

        /// <summary>
        /// Fix Assist Description
        /// </summary>
        [Description("Fix Assist Description")]
        FixAssistDescription = 5
    }
}
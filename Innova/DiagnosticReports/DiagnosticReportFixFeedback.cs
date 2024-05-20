using Innova.Fixes;
using Innova.ObdFixes;
using Innova.Users;
using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using Metafuse3.NullableTypes;
using System;
using System.Collections.Specialized;
using System.Data.SqlClient;

namespace Innova.DiagnosticReports
{
    /// <summary>
    /// The DiagnosticReportFixFeedback object handles the business logic and data access for the specialized business object, DiagnosticReportFixFeedback.
    /// The class inherits from <see cref="BusinessObjectBase"/> class.
    /// </summary>
    /// <example>
    /// See the examples of how to construct the DiagnosticReportFixFeedback object.
    ///
    /// To create a new instance of a new of DiagnosticReportFixFeedback.
    /// <code>DiagnosticReportFixFeedback o = (DiagnosticReportFixFeedback)this.Registry.CreateInstance(typeof(DiagnosticReportFixFeedback));</code>
    ///
    /// To create an new instance of an existing DiagnosticReportFixFeedback.
    /// <code>DiagnosticReportFixFeedback o = (DiagnosticReportFixFeedback)this.Registry.CreateInstance(typeof(DiagnosticReportFixFeedback), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of DiagnosticReportFixFeedback, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectBase.ConnectionString"/> to the <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable()]
    public class DiagnosticReportFixFeedback : BusinessObjectBase
    {
        private DiagnosticReport diagnosticReport;
        private bool isReportValid = true;
        private string couldNotFixReason = "";
        private string primaryErrorCode;
        private DiagnosticReportFixFeedbackStatus diagnosticReportFixFeedbackStatus;
        private DiagnosticReportErrorCodeSystemType diagnosticReportErrorCodeSystemType;
        private User feedbackProvidedByUser;
        private User feedbackReviewedByUser;
        private AdminUser feedbackReviewedByAdminUser;
        private Fix fix;
        private int averageDiagnosticTime = 0;
        private int laborTime = 0; //#Sprint18IA106
        private int frequencyEncountered = 0;
        private FixDifficultyRating fixDifficultyRating;
        private ObdFix obdFix;
        private string errorCodesThatApply = "";
        private ProRSFix proRSFix; //Added on 2017-08-10 by Nam Lu - INNOVA Dev Team
        private string techComments = "";
        private string basicToolsRequired = "";
        private string specialtyToolsRequired = "";
        private string tipsAndTricks = "";
        private string reviewerComments = "";
        private NullableDateTime feedbackReviewedDateTimeUTC = NullableDateTime.Null;
        private int score = 0;
        private NullableBoolean isPaymentDtcAllowed = NullableBoolean.Null;
        private NullableBoolean isPaymentToolsRequiredAllowed = NullableBoolean.Null;
        private NullableDecimal paymentAmount = NullableDecimal.Null;
        private NullableDateTime paymentDateTimeUTC = NullableDateTime.Null;
        private DateTime createdDateTimeUTC = DateTime.MinValue;
        private DateTime updatedDateTimeUTC = DateTime.MinValue;

        private DiagnosticReportFixFeedbackPartCollection parts;
        private DiagnosticReportFixFeedbackToolCollection tools; //ToolDB_

        #region Contructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new (one that does not exist, assigning a new <see cref="Guid"/> Id). DiagnosticReportFixFeedback object.
        /// In order to create a new DiagnosticReportFixFeedback which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <example>
        /// <code>
        /// DiagnosticReportFixFeedback o = (DiagnosticReportFixFeedback)Registry.CreateInstance(typeof(DiagnosticReportFixFeedback));
        /// </code>
        /// </example>
        protected internal DiagnosticReportFixFeedback()
            : base()
        {
            this.IsObjectCreated = true;
            this.IsObjectLoaded = true;
            this.IsObjectDirty = true;
        }

        /// <summary>
        /// Constructor creates an existing (one that exists, and already has a <see cref="Guid"/> Id).  DiagnosticReportFixFeedback object.
        /// In order to create an existing DiagnosticReportFixFeedback object, which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <param name="id">The <see cref="Guid"/> or SQL uniqueidentifier indentification "Id" of the current object.</param>
        /// <example>
        /// <code>
        /// DiagnosticReportFixFeedback o = (DiagnosticReportFixFeedback)Registry.CreateInstance(typeof(DiagnosticReportFixFeedback), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));
        /// </code>
        /// </example>
        protected internal DiagnosticReportFixFeedback(Guid id)
            : base(id)
        {
            this.id = id;
        }

        #endregion Contructors

        #region System Properties DO NOT EDIT

        // private member variables used to handle the system properties.
        private bool isObjectDirty = false;

        private bool isObjectLoaded = false;
        private bool isObjectCreated = false;

        private StringCollection updatedFields = null;
        /*****************************************************************************************
         *
         * System Properties: DO NOT EDIT
         *
         * **************************************************************************************/

        /// <summary>
        /// Gets or sets the <see cref="bool"/> flag which determines whether or not the current object has been loaded from the database.
        /// The property is marked new so that each layer of the inheritence chain has it's own IsObjectLoaded property.
        /// Base layers may or may not be loaded.  The IsObjectLoaded propery is automatically set to true when the object is loaded from the database.
        /// The IsObjectLoaded property is used primarily for the internal Load methods to determine whether or not the object needs to load itself when a property is accessed or the Load method is invoked.
        /// </summary>
        public new bool IsObjectLoaded
        {
            get { return this.isObjectLoaded; }
            set { this.isObjectLoaded = value; }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/> flag which determines whether or not the current object has been updated and needs to be saved to the database.
        /// The property is marked new so that each layer of the inheritence chain has it's own IsObjectDirty property.
        /// Base layers may or may not be dirty.  The IsObjectDirty flag should set to true when a property is updated, and the object automatically sets the IsObjectDirty flag to false when the object is saved successfully.
        /// The IsObjectDirty property is used primarly for the internal Save methods to determine whether or not the object needs to save itself when the Save method is invoked.
        /// </summary>
        public new bool IsObjectDirty
        {
            get { return this.isObjectDirty; }
            set
            {
                this.isObjectDirty = value;

                if (!this.isObjectDirty)
                {
                    this.isObjectCreated = false;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/> flag which determines whether or not the current object has just been created (is new) and will need to be saved to the database.
        /// The property is marked new so that each layer of the inheritence chain has it's own IsObjectCreated property.
        /// The IsObjectCreated flag is automatically set to false when the object is saved.
        /// Base layers may or may not be created.  The IsObjectCreated flag is set to false when saved.
        /// </summary>
        public new bool IsObjectCreated
        {
            get { return this.isObjectCreated; }
            set { this.isObjectCreated = value; }
        }

        /// <summary>
        /// Adds an updated field to the collection of updated fields used to create the update statement for the object.
        /// </summary>
        /// <param name="databaseField"><see cref="string"/> updated database field to add.</param>
        protected internal new void UpdatedField(string databaseField)
        {
            //if this is not a created object, then we need to keep track of the updated list
            if (!this.isObjectCreated)
            {
                if (this.updatedFields == null)
                {
                    this.updatedFields = new StringCollection();
                }

                if (this.updatedFields.Contains(databaseField.ToLower()) == false)
                {
                    this.updatedFields.Add(databaseField.ToLower());
                }
            }
        }

        #endregion System Properties DO NOT EDIT

        #region Public Properties

        /**************************************************************************************
         *
         * Object Properties: Add Custom Fields
         *
         * **************************************************************************************/

        /// <summary>
        /// Gets or sets the <see cref="DiagnosticReport"/> diagnostic report.
        /// </summary>
        public DiagnosticReport DiagnosticReport
        {
            get
            {
                this.EnsureLoaded();
                return this.diagnosticReport;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.diagnosticReport)
                {
                    this.IsObjectDirty = true;
                    this.diagnosticReport = value;
                    this.UpdatedField("DiagnosticReportId");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/> indicating if the report is valid.
        /// </summary>
        public bool IsReportValid
        {
            get
            {
                this.EnsureLoaded();
                return this.isReportValid;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.isReportValid)
                {
                    this.IsObjectDirty = true;
                    this.isReportValid = value;
                    this.UpdatedField("IsReportValid");
                }
            }
        }

        /// <summary>
        /// Gets or set the <see cref="string"/> reason the vehicle could not be fixed.
        /// </summary>
        [PropertyDefinition("Fix Comment Type", "Reason", "Reason", "The reason the vehicle count not be fixed.")]
        public string CouldNotFixReason
        {
            get
            {
                this.EnsureLoaded();
                return this.couldNotFixReason;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.couldNotFixReason)
                {
                    this.IsObjectDirty = true;
                    this.couldNotFixReason = value;
                    this.UpdatedField("CouldNotFixReason");
                }
            }
        }

        /// <summary>
        /// Gets or set the <see cref="string"/> primary error code for the fix.
        /// </summary>
        public string PrimaryErrorCode
        {
            get
            {
                this.EnsureLoaded();
                return this.primaryErrorCode;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.primaryErrorCode)
                {
                    this.IsObjectDirty = true;
                    this.primaryErrorCode = value;
                    this.UpdatedField("PrimaryErrorCode");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DiagnosticReportFixFeedbackStatus"/> diagnostic report fix feedback status.
        /// </summary>
        [PropertyDefinition("Status", "The feedback status of the report.")]
        public DiagnosticReportFixFeedbackStatus DiagnosticReportFixFeedbackStatus
        {
            get
            {
                this.EnsureLoaded();
                return this.diagnosticReportFixFeedbackStatus;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.diagnosticReportFixFeedbackStatus)
                {
                    this.IsObjectDirty = true;
                    this.diagnosticReportFixFeedbackStatus = value;
                    this.UpdatedField("DiagnosticReportFixFeedbackStatus");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DiagnosticReportErrorCodeSystemType"/> diagnostic report error code status.
        /// </summary>
        [PropertyDefinition("Diagnostic Report Error Code System Type", "System", "System",
            "The diagnostic report error code status.")]
        public DiagnosticReportErrorCodeSystemType DiagnosticReportErrorCodeSystemType
        {
            get
            {
                this.EnsureLoaded();
                return this.diagnosticReportErrorCodeSystemType;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.diagnosticReportErrorCodeSystemType)
                {
                    this.IsObjectDirty = true;
                    this.diagnosticReportErrorCodeSystemType = value;
                    this.UpdatedField("DiagnosticReportErrorCodeSystemType");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="User"/> technician who is providing the fix feedback.
        /// </summary>
        public User FeedbackProvidedByUser
        {
            get
            {
                this.EnsureLoaded();
                return this.feedbackProvidedByUser;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.feedbackProvidedByUser)
                {
                    this.IsObjectDirty = true;
                    this.feedbackProvidedByUser = value;
                    this.UpdatedField("UserId_FeedbackProvidedBy");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="User"/> technician who reviewed the fix feedback.
        /// </summary>
        [PropertyDefinition("Reviewed By", "Consultant Reviewer", "Consultant Reviewer",
            "User who reviewed the fix feedback report.")]
        public User FeedbackReviewedByUser
        {
            get
            {
                this.EnsureLoaded();
                return this.feedbackReviewedByUser;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.feedbackReviewedByUser)
                {
                    this.IsObjectDirty = true;
                    this.feedbackReviewedByUser = value;
                    this.UpdatedField("UserId_FeedbackReviewedBy");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="AdminUser"/> the admin who reviewed the fix feedback.
        /// </summary>
        [PropertyDefinition("Reviewed By", "Admin Reviewer", "Admin Reviewer",
            "Admin who reviewed the fix feedback report.")]
        public AdminUser FeedbackReviewedByAdminUser
        {
            get
            {
                this.EnsureLoaded();
                return this.feedbackReviewedByAdminUser;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.feedbackReviewedByAdminUser)
                {
                    this.IsObjectDirty = true;
                    this.feedbackReviewedByAdminUser = value;
                    this.UpdatedField("AdminUserId_FeedbackReviewedBy");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Fix"/> fix that the technician selects.
        /// </summary>
        [PropertyDefinition("Fix", "The fix.")]
        public Fix Fix
        {
            get
            {
                this.EnsureLoaded();
                return this.fix;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.fix)
                {
                    this.IsObjectDirty = true;
                    this.fix = value;
                    this.UpdatedField("FixId");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="AverageDiagnosticTime"/> average diagnostic time the fix takes.
        /// </summary>
        [PropertyDefinition("Average Diagnostic Time", "The average time to diagnose this fix.")]
        public int AverageDiagnosticTime
        {
            get
            {
                this.EnsureLoaded();
                return this.averageDiagnosticTime;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.averageDiagnosticTime)
                {
                    this.IsObjectDirty = true;
                    this.averageDiagnosticTime = value;
                    this.UpdatedField("AverageDiagnosticTime");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="LaborTime"/> average diagnostic time the fix takes.
        /// </summary>
        [PropertyDefinition("Labor Time", "The labor time for this fix.")]
        public int LaborTime
        {
            get
            {
                this.EnsureLoaded();
                return this.laborTime;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.laborTime)
                {
                    this.IsObjectDirty = true;
                    this.laborTime = value;
                    this.UpdatedField("LaborTime");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="FrequencyEncountered"/> frequency of the encounter for this fix.
        /// </summary>
        [PropertyDefinition("Times Encountered", "The number of times this fix has been encountered.")]
        public int FrequencyEncountered
        {
            get
            {
                this.EnsureLoaded();
                return this.frequencyEncountered;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.frequencyEncountered)
                {
                    this.IsObjectDirty = true;
                    this.frequencyEncountered = value;
                    this.UpdatedField("FrequencyEncountered");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="FixDifficultyRating"/> fix difficulty rating for the fix.
        /// </summary>
        [PropertyDefinition("Fix Difficulty Rating", "Difficulty", "Difficulty", "The difficulty rating for the fix.")]
        public FixDifficultyRating FixDifficultyRating
        {
            get
            {
                this.EnsureLoaded();
                return this.fixDifficultyRating;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.fixDifficultyRating)
                {
                    this.IsObjectDirty = true;
                    this.fixDifficultyRating = value;
                    this.UpdatedField("FixDifficultyRating");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="ObdFix"/> OBD fix for this report.
        /// </summary>
        [PropertyDefinition("OBD Fix", "The OBD fix.")]
        public ObdFix ObdFix
        {
            get
            {
                this.EnsureLoaded();
                return this.obdFix;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.obdFix)
                {
                    this.IsObjectDirty = true;
                    this.obdFix = value;
                    this.UpdatedField("ObdFixId");
                }
            }
        }

        //Added on 2017-08-10 by INNOVA Dev Team
        /// <summary>
        /// Gets or sets the <see cref="ProRSFix"/> ProRS fix for this report.
        /// </summary>
        [PropertyDefinition("ProRS Fix", "The ProRS fix.")]
        public ProRSFix ProRSFix
        {
            get
            {
                this.EnsureLoaded();
                return this.proRSFix;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.proRSFix)
                {
                    this.IsObjectDirty = true;
                    this.proRSFix = value;
                    this.UpdatedField("ProRSFixId");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> error codes that the technician thinks should apply to the fix.
        /// </summary>
        [PropertyDefinition("DTCs", "DTCs That Apply", "DTCs That Apply", "Possible error codes for the fix.")]
        public string ErrorCodesThatApply
        {
            get
            {
                this.EnsureLoaded();
                return this.errorCodesThatApply;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.errorCodesThatApply)
                {
                    this.IsObjectDirty = true;
                    this.errorCodesThatApply = value;
                    this.UpdatedField("ErrorCodesThatApply");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> technicians comments.
        /// </summary>
        [PropertyDefinition("Tech Comments", "Comments", "Comments", "The technicians comments about the fix.")]
        public string TechComments
        {
            get
            {
                this.EnsureLoaded();
                return this.techComments;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.techComments)
                {
                    this.IsObjectDirty = true;
                    this.techComments = value;
                    this.UpdatedField("TechComments");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> of the technicians comments of how to diagnose the fix (formly basic tools required).
        /// </summary>
        [PropertyDefinition("Basic Tools", "Basic tools used for the fix.")]
        public string BasicToolsRequired
        {
            get
            {
                this.EnsureLoaded();
                return this.basicToolsRequired;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.basicToolsRequired)
                {
                    this.IsObjectDirty = true;
                    this.basicToolsRequired = value;
                    this.UpdatedField("BasicToolsRequired");
                }
            }
        }

        /// <summary>
        /// Gets or sets <see cref="string"/> technicians comments to specify any special tools required.
        /// </summary>
        [PropertyDefinition("Specialty Tools", "Specialty tools used for the fix.")]
        public string SpecialtyToolsRequired
        {
            get
            {
                this.EnsureLoaded();
                return this.specialtyToolsRequired;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.specialtyToolsRequired)
                {
                    this.IsObjectDirty = true;
                    this.specialtyToolsRequired = value;
                    this.UpdatedField("SpecialtyToolsRequired");
                }
            }
        }

        /// <summary>
        /// Gets or sets <see cref="string"/> technicians comments to specify any tips or shortcuts for the repair.
        /// </summary>
        [PropertyDefinition("Tips/Tricks", "Tips and tricks for the fix.")]
        public string TipsAndTricks
        {
            get
            {
                this.EnsureLoaded();
                return this.tipsAndTricks;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.tipsAndTricks)
                {
                    this.IsObjectDirty = true;
                    this.tipsAndTricks = value;
                    this.UpdatedField("TipsAndTricks");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> reviewers comments.
        /// </summary>
        [PropertyDefinition("Reviewer's Comments", "Admin or Consultant comments.")]
        public string ReviewerComments
        {
            get
            {
                this.EnsureLoaded();
                return this.reviewerComments;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.reviewerComments)
                {
                    this.IsObjectDirty = true;
                    this.reviewerComments = value;
                    this.UpdatedField("ReviewerComments");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/> date time the fix feeback was reviewed.
        /// </summary>
        [PropertyDefinition("Feedback Review Date", "Last Reviewed Date", "Last Reviewed Date",
            "Date the feedback was reviewed.")]
        public NullableDateTime FeedbackReviewedDateTimeUTC
        {
            get
            {
                this.EnsureLoaded();
                return this.feedbackReviewedDateTimeUTC;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.feedbackReviewedDateTimeUTC)
                {
                    this.IsObjectDirty = true;
                    this.feedbackReviewedDateTimeUTC = value;
                    this.UpdatedField("FeedbackReviewedDateTimeUTC");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/> score earned for the technicians.
        /// </summary>
        [PropertyDefinition("Score", "Score Earned", "Score Earned", "The score the technican will earn.")]
        public int Score
        {
            get
            {
                this.EnsureLoaded();
                return this.score;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.score)
                {
                    this.IsObjectDirty = true;
                    this.score = value;
                    this.UpdatedField("Score");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableBoolean"/> flag if the technician will be paid for the fix feedback.
        /// </summary>
        [PropertyDefinition("DO NOT pay technician for feedback on this report.",
            "Allow payment for the DTC feedback by the technician.")]
        public NullableBoolean IsPaymentDtcAllowed
        {
            get
            {
                this.EnsureLoaded();
                return this.isPaymentDtcAllowed;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.isPaymentDtcAllowed)
                {
                    this.IsObjectDirty = true;
                    this.isPaymentDtcAllowed = value;
                    this.UpdatedField("IsPaymentDtcAllowed");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableBoolean"/> flag if the technician will be paid for the tools required comments for this report.
        /// </summary>
        [PropertyDefinition("DO NOT pay technician for Tools Required on this report.",
            "Allow payment for the tools required for fix by the technician.")]
        public NullableBoolean IsPaymentToolsRequiredAllowed
        {
            get
            {
                this.EnsureLoaded();
                return this.isPaymentToolsRequiredAllowed;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.isPaymentToolsRequiredAllowed)
                {
                    this.IsObjectDirty = true;
                    this.isPaymentToolsRequiredAllowed = value;
                    this.UpdatedField("IsPaymentToolsRequiredAllowed");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableDecimal"/> the payment amount the technician will receive for the report.
        /// </summary>
        [PropertyDefinition("Payment Amount", "Amount Paid", "Amount Paid", "Payment amount for the feedback report.")]
        public NullableDecimal PaymentAmount
        {
            get
            {
                this.EnsureLoaded();
                return this.paymentAmount;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.paymentAmount)
                {
                    this.IsObjectDirty = true;
                    this.paymentAmount = value;
                    this.UpdatedField("PaymentAmount");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/> date time the payment will be made.
        /// </summary>
        public NullableDateTime PaymentDateTimeUTC
        {
            get
            {
                this.EnsureLoaded();
                return this.paymentDateTimeUTC;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.paymentDateTimeUTC)
                {
                    this.IsObjectDirty = true;
                    this.paymentDateTimeUTC = value;
                    this.UpdatedField("PaymentDateTimeUTC");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/> date time the fix feedback report is created.
        /// </summary>
        public DateTime CreatedDateTimeUTC
        {
            get
            {
                this.EnsureLoaded();
                return this.createdDateTimeUTC;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.createdDateTimeUTC)
                {
                    this.IsObjectDirty = true;
                    this.createdDateTimeUTC = value;
                    this.UpdatedField("CreatedDateTimeUTC");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/> date time the fix feedback report is updated.
        /// </summary>
        public DateTime UpdatedDateTimeUTC
        {
            get
            {
                this.EnsureLoaded();
                return this.updatedDateTimeUTC;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.updatedDateTimeUTC)
                {
                    this.IsObjectDirty = true;
                    this.updatedDateTimeUTC = value;
                    this.UpdatedField("UpdatedDateTimeUTC");
                }
            }
        }

        #endregion Public Properties

        #region Object Properties (Related Objects)

        /*****************************************************************************************
		 *
		 * Object Relationships: Add correlated Object Collections
		 *
		*******************************************************************************************/

        /// <summary>
        /// Gets the <see cref="DiagnosticReportFixFeedbackPartCollection"/> of <see cref="DiagnosticReportFixFeedbackPart"/> objects associated with this fix feedback.
        /// </summary>
        [PropertyDefinition("Required Parts", "Required Parts")]
        public DiagnosticReportFixFeedbackPartCollection Parts
        {
            get
            {
                if (this.parts == null)
                {
                    this.parts = new DiagnosticReportFixFeedbackPartCollection(this.Registry);

                    //load if not a user created element
                    if (!this.isObjectCreated)
                    {
                        SqlProcedureCommand call = new SqlProcedureCommand();
                        this.EnsureValidId();

                        call.ProcedureName = "DiagnosticReportFixFeedbackPart_LoadDiagnosticReportFixFeedback";
                        call.AddGuid("DiagnosticReportFixFeedbackId", this.Id);

                        this.parts.Load(call, "DiagnosticReportFixFeedbackPartId", true, true);
                    }
                }

                return this.parts;
            }
        }

        //ToolDB_
        /// <summary>
        /// //ToolDB_
        /// </summary>
        [PropertyDefinition("Required Tools", "Required Tools")]
        public DiagnosticReportFixFeedbackToolCollection Tools
        {
            get
            {
                if (this.tools == null)
                {
                    this.tools = new DiagnosticReportFixFeedbackToolCollection(this.Registry);

                    //load if not a user created element
                    if (!this.isObjectCreated)
                    {
                        SqlProcedureCommand call = new SqlProcedureCommand();
                        this.EnsureValidId();

                        call.ProcedureName = "DiagnosticReportFixFeedbackTool_LoadDiagnosticReportFixFeedback";
                        call.AddGuid("DiagnosticReportFixFeedbackId", this.Id);

                        this.tools.Load(call, "DiagnosticReportFixFeedbackToolId", true, true);
                    }
                }

                return this.tools;
            }
        }

        //ToolDB_

        #endregion Object Properties (Related Objects)

        #region Business Logic Methods

        /***********************************************************************************************
         *
         * Custom Business Logic
         *
         * **********************************************************************************************/

        #endregion Business Logic Methods

        #region Required Methods (Load, Save, Delete, Etc)

        /***********************************************************************************************
		 *
		 * Required Object Methods (Load, Save, Save Collections, Delete)
		 *
		 * **********************************************************************************************/
        // Edit Required Object Methods

        #region System Load Calls (DO NOT EDIT)

        /// <summary>
        /// Loads the data for the current object (this layer in the inheritence chain) if <see cref="IsObjectLoaded"/> is false.
        /// </summary>
        public new void Load()
        {
            this.Load(null, null, false);
        }

        /// <summary>
        /// Loads the data for the current object (this layer in the inheritence chain) if <see cref="IsObjectLoaded"/> is false.
        /// </summary>
        /// <param name="connection"><see cref="SqlConnection"/> currently being used (if any), if null, a new <see cref="SqlConnection"/> is created to perform the operation.</param>
        /// <param name="isLoadBase"><see cref="bool"/> when set to true, base layers (if any) will also be loaded.</param>
        /// <returns><see cref="SqlConnection"/> supplied or the one created internally.</returns>
        public new SqlConnection Load(SqlConnection connection, bool isLoadBase)
        {
            this.Load(connection, null, isLoadBase);

            return connection;
        }

        /// <summary>
        /// Loads the data for the current object (this layer in the inheritence chain) if <see cref="IsObjectLoaded"/> is false.
        /// </summary>
        /// <param name="connection"><see cref="SqlConnection"/> currently being used (if any), if null, a new <see cref="SqlConnection"/> is created to perform the operation.</param>
        /// <param name="transaction"><see cref="SqlTransaction"/> currently being used (if any), otherwise if set to null the command will be executed outside the contect of a current transaction.</param>
        /// <param name="isLoadBase"><see cref="bool"/> when set to true, base layers (if any) will also be loaded.</param>
        /// <returns><see cref="SqlTransaction"/> currently being used (if any) to access the database.  If none, returns null.</returns>
        public new SqlTransaction Load(SqlConnection connection, SqlTransaction transaction, bool isLoadBase)
        {
            this.EnsureValidId();

            if (isLoadBase)
            {
                //load the base item if user selected it.
                transaction = base.Load(connection, transaction, isLoadBase);
            }

            if (!this.IsObjectLoaded)
            {
                SqlDataReaderWrapper dr;
                if (connection == null)
                {
                    dr = new SqlDataReaderWrapper(ConnectionString);
                }
                else
                {
                    dr = new SqlDataReaderWrapper(connection, false);
                }

                using (dr)
                {
                    this.SetLoadProcedureCall(dr);

                    if (transaction == null)
                    {
                        dr.Execute();
                    }
                    else
                    {
                        dr.Execute(transaction);
                    }

                    if (dr.Read())
                    {
                        this.LoadPropertiesFromDataReader(dr, isLoadBase);
                    }
                    else
                    {
                        throw (new ApplicationException("Load Failed for type " + this.GetType().ToString() +
                                                        " using Procedure: " + dr.ProcedureCall));
                    }
                }
            }

            return transaction;
        }

        /// <summary>
        /// Loads all the properties of this object from <see cref="SqlDataReaderWrapper"/> supplied.
        /// This method calls the protected internal method <see cref="SetPropertiesFromDataReader"/>.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> recordset containing the fields required to set the properties of this object.</param>
        /// <param name="isSetBase"><see cref="bool"/> determines whether or not to load base layers (if any) of the object.  Set to true if the recordset contains the fields necessary to load the properties of base layers of this object.</param>
        public new void LoadPropertiesFromDataReader(SqlDataReaderWrapper dr, bool isSetBase)
        {
            if (isSetBase)
            {
                base.LoadPropertiesFromDataReader(dr, isSetBase);
            }

            if (!IsObjectLoaded)
            {
                this.SetPropertiesFromDataReader(dr);
            }

            this.IsObjectLoaded = true;
        }

        /// <summary>
        /// Method ensures the object is loaded.  This method is located in the get portion of the a property representing data in the database and is called there.  If the object's <see cref="IsObjectLoaded"/> property is false and the <see cref="IsObjectCreated"/> property is false, then the <see cref="Load()"/> method is invoked.
        /// </summary>
        protected new void EnsureLoaded()
        {
            if (!this.IsObjectLoaded && !this.IsObjectCreated)
            {
                this.Load();
            }
        }

        #endregion System Load Calls (DO NOT EDIT)

        /// <summary>
        /// Sets the base load procedure call and parameters to the supplied <see cref="SqlDataReaderWrapper"/>, to be executed.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> to set the procedure call and parameters to.</param>
        protected new void SetLoadProcedureCall(SqlDataReaderWrapper dr)
        {
            dr.ProcedureName = "DiagnosticReportFixFeedback_Load";
            dr.AddGuid("DiagnosticReportFixFeedbackId", this.Id);
        }

        /// <summary>
        /// Sets the properties of this object from the fields in the recordset located in the supplied <see cref="SqlDataReaderWrapper"/>.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> containing the recordset and fields used to set the properties of this object (this layer only).</param>
        protected new void SetPropertiesFromDataReader(SqlDataReaderWrapper dr)
        {
            this.diagnosticReport =
                (DiagnosticReport)dr.GetBusinessObjectBase(this.Registry, typeof(DiagnosticReport),
                    "DiagnosticReportId");
            this.isReportValid = dr.GetBoolean("IsReportValid");
            this.couldNotFixReason = dr.GetString("CouldNotFixReason");
            this.primaryErrorCode = dr.GetString("PrimaryErrorCode");
            this.diagnosticReportErrorCodeSystemType =
                (DiagnosticReportErrorCodeSystemType)dr.GetInt32("DiagnosticReportErrorCodeSystemType");
            this.diagnosticReportFixFeedbackStatus =
                (DiagnosticReportFixFeedbackStatus)dr.GetInt32("DiagnosticReportFixFeedbackStatus");
            this.feedbackProvidedByUser =
                (User)dr.GetBusinessObjectBase(this.Registry, typeof(User), "UserId_FeedbackProvidedBy");
            this.feedbackReviewedByUser =
                (User)dr.GetBusinessObjectBase(this.Registry, typeof(User), "UserId_FeedbackReviewedBy");
            this.feedbackReviewedByAdminUser = (AdminUser)dr.GetBusinessObjectBase(this.Registry, typeof(AdminUser),
                "AdminUserId_FeedbackReviewedBy");
            this.fix = (Fix)dr.GetBusinessObjectBase(this.Registry, typeof(Fix), "FixId");
            this.averageDiagnosticTime = dr.GetInt32("AverageDiagnosticTime");
            this.laborTime = dr.GetInt32("LaborTime"); //#Sprint18IA106
            this.frequencyEncountered = dr.GetInt32("FrequencyEncountered");
            this.fixDifficultyRating = (FixDifficultyRating)dr.GetInt32("FixDifficultyRating");
            this.obdFix = (ObdFix)dr.GetBusinessObjectBase(this.Registry, typeof(ObdFix), "ObdFixId");
            //Added on 08/10/2017 by INNOVA Dev Team: Support ProRS Fix - 8/10/2017
            if (dr.ColumnExists("ProRSFixId"))
                this.proRSFix = (ProRSFix)dr.GetBusinessObjectBase(this.Registry, typeof(ProRSFix), "ProRSFixId");
            this.errorCodesThatApply = dr.GetString("ErrorCodesThatApply");
            this.techComments = dr.GetString("TechComments");
            this.basicToolsRequired = dr.GetString("BasicToolsRequired");
            this.specialtyToolsRequired = dr.GetString("SpecialtyToolsRequired");
            this.tipsAndTricks = dr.GetString("TipsAndTricks");
            this.reviewerComments = dr.GetString("ReviewerComments");
            if (!dr.IsDBNull("FeedbackReviewedDateTimeUTC"))
            {
                this.feedbackReviewedDateTimeUTC = dr.GetDateTime("FeedbackReviewedDateTimeUTC");
            }

            this.score = dr.GetInt32("Score");
            if (!dr.IsDBNull("IsPaymentDtcAllowed"))
            {
                this.isPaymentDtcAllowed = dr.GetBoolean("IsPaymentDtcAllowed");
            }

            if (!dr.IsDBNull("IsPaymentToolsRequiredAllowed"))
            {
                this.isPaymentToolsRequiredAllowed = dr.GetBoolean("IsPaymentToolsRequiredAllowed");
            }

            if (!dr.IsDBNull("PaymentAmount"))
            {
                this.paymentAmount = dr.GetDecimal("PaymentAmount");
            }

            if (!dr.IsDBNull("PaymentDateTimeUTC"))
            {
                this.paymentDateTimeUTC = dr.GetDateTime("PaymentDateTimeUTC");
            }

            this.createdDateTimeUTC = dr.GetDateTime("CreatedDateTimeUTC");
            this.updatedDateTimeUTC = dr.GetDateTime("UpdatedDateTimeUTC");

            this.IsObjectLoaded = true;
        }

        /// <summary>
        /// Saves the current object, all base layers, and all related collections (calls <see cref="SaveCollections"/>).
        /// </summary>
        /// <param name="connection"><see cref="SqlConnection"/> currently being used if any.  If null is supplied a new <see cref="SqlConnection"/> is created.</param>
        /// <param name="transaction"><see cref="SqlTransaction"/> currently being used if any.  If null is supplied a new <see cref="SqlConnection"/> is created.</param>
        /// <returns><see cref="SqlTransaction"/> currently being used.</returns>
        public override SqlTransaction Save(SqlConnection connection, SqlTransaction transaction)
        {
            // Call base save method of base class.
            transaction = base.Save(connection, transaction);

            //Custom save business logic here. Modify procedure name.
            if (this.IsObjectDirty)
            {
                transaction = this.EnsureDatabasePrepared(connection, transaction);

                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    if (this.IsObjectCreated)
                    {
                        dr.ProcedureName = "DiagnosticReportFixFeedback_Create";
                        this.CreatedDateTimeUTC = DateTime.UtcNow;
                        this.UpdatedDateTimeUTC = this.CreatedDateTimeUTC;
                    }
                    else
                    {
                        dr.UpdateFields("DiagnosticReportFixFeedback", "DiagnosticReportFixFeedbackId",
                            this.updatedFields);
                    }

                    dr.AddGuid("DiagnosticReportFixFeedbackId", this.Id);
                    dr.AddBoolean("IsReportValid", this.IsReportValid);
                    dr.AddNVarChar("CouldNotFixReason", this.CouldNotFixReason);
                    dr.AddNVarChar("PrimaryErrorCode", this.PrimaryErrorCode);
                    dr.AddInt32("DiagnosticReportErrorCodeSystemType", (int)this.DiagnosticReportErrorCodeSystemType);
                    dr.AddInt32("DiagnosticReportFixFeedbackStatus", (int)this.DiagnosticReportFixFeedbackStatus);
                    dr.AddBusinessObject("DiagnosticReportId", this.DiagnosticReport);
                    dr.AddBusinessObject("UserId_FeedbackProvidedBy", this.FeedbackProvidedByUser);
                    dr.AddBusinessObject("UserId_FeedbackReviewedBy", this.FeedbackReviewedByUser);
                    dr.AddBusinessObject("AdminUserId_FeedbackReviewedBy", this.FeedbackReviewedByAdminUser);
                    dr.AddBusinessObject("FixId", this.Fix);
                    dr.AddInt32("AverageDiagnosticTime", this.AverageDiagnosticTime);
                    dr.AddInt32("LaborTime", this.LaborTime); //#Sprint18IA106
                    dr.AddInt32("FrequencyEncountered", this.FrequencyEncountered);
                    dr.AddInt32("FixDifficultyRating", (int)this.FixDifficultyRating);
                    dr.AddBusinessObject("ObdFixId", this.ObdFix);
                    dr.AddNVarChar("ErrorCodesThatApply", this.ErrorCodesThatApply);
                    dr.AddNVarChar("TechComments", this.TechComments);
                    dr.AddNVarChar("BasicToolsRequired", this.BasicToolsRequired);
                    dr.AddNVarChar("SpecialtyToolsRequired", this.SpecialtyToolsRequired);
                    dr.AddNVarChar("TipsAndTricks", this.TipsAndTricks);
                    dr.AddNVarChar("ReviewerComments", this.ReviewerComments);
                    if (!this.FeedbackReviewedDateTimeUTC.IsNull)
                    {
                        dr.AddDateTime("FeedbackReviewedDateTimeUTC", this.FeedbackReviewedDateTimeUTC.Value);
                    }

                    dr.AddInt32("Score", this.Score);
                    if (!this.IsPaymentDtcAllowed.IsNull)
                    {
                        dr.AddBoolean("IsPaymentDtcAllowed", this.IsPaymentDtcAllowed.Value);
                    }

                    if (!this.IsPaymentToolsRequiredAllowed.IsNull)
                    {
                        dr.AddBoolean("IsPaymentToolsRequiredAllowed", this.IsPaymentToolsRequiredAllowed.Value);
                    }

                    //Addedd on 2017/10/2017 by INNOVA DEV Team (Nam Lu)
                    dr.AddBusinessObject("ProRSFixId", this.ProRSFix);

                    if (!this.PaymentAmount.IsNull)
                    {
                        dr.AddDecimal("PaymentAmount", this.PaymentAmount.Value);
                    }

                    dr.AddDateTime("PaymentDateTimeUTC", this.PaymentDateTimeUTC);

                    dr.AddDateTime("CreatedDateTimeUTC", this.CreatedDateTimeUTC);
                    dr.AddDateTime("UpdatedDateTimeUTC", this.UpdatedDateTimeUTC);

                    dr.Execute(transaction);
                }

                this.IsObjectDirty = false;
            }

            // call the save collections method
            transaction = this.SaveCollections(connection, transaction);

            return transaction;
        }

        /// <summary>
        /// Saves the related collections (normally set as properties) that the object needs to save to maintain integrity.
        /// </summary>
        /// <param name="connection"><see cref="SqlConnection"/> currently being used.</param>
        /// <param name="transaction"><see cref="SqlTransaction"/> currently being used.</param>
        /// <returns><see cref="SqlTransaction"/> currently being used.</returns>
        protected new SqlTransaction SaveCollections(SqlConnection connection, SqlTransaction transaction)
        {
            // Customized related object collection saving business logic.

            return transaction;
        }

        /// <summary>
        /// Deletes the object, base layers, and related collections necessary to delete the object.
        /// </summary>
        /// <param name="connection"><see cref="SqlConnection"/> currently being used, if null a <see cref="SqlConnection"/> is created.</param>
        /// <param name="transaction"><see cref="SqlTransaction"/> currently being used, if null a <see cref="SqlTransaction"/> is created.</param>
        /// <returns><see cref="SqlTransaction"/> currently being used.</returns>
        public override SqlTransaction Delete(SqlConnection connection, SqlTransaction transaction)
        {
            this.EnsureValidId();

            transaction = this.EnsureDatabasePrepared(connection, transaction);

            // Custom delete business logic here.

            /* Example of deleting the items
             * (This could potentially cause a lock since the property calls the lookup
             *  with a new connection, implement a load method for the property in that case,
             *  or created a specialied delete call which won't load the collection to loop and
             *  delete, but in that case you won't be automatically calling the delete of the
             *  child related items. See example below).
             *
            // delete the child related objects
            foreach (ItemChild itemChild in ItemChilds)
            {
                transaction = itemChild.Delete(connection, transaction);
            }
            */

            // delete this current object
            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                //remove related objects with a specialized delete.
                /*
                dr.ProcedureName = "ItemsOthers_Delete";
                dr.AddGuid("ItemId", Id);

                dr.ExecuteNonQuery(transaction);
                dr.ClearParameters();
                */

                //delete the item
                dr.ProcedureName = "DiagnosticReportFixFeedback_Delete";
                dr.AddGuid("DiagnosticReportFixFeedbackId", this.Id);

                dr.ExecuteNonQuery(transaction);
            }

            transaction = base.Delete(connection, transaction);

            return transaction;
        }

        #endregion Required Methods (Load, Save, Delete, Etc)
    }

    /// <summary>
    /// Container class holds all of the <see cref="FixFeedback"/> objects associated with this DiagnosticReportFixFeedback
    /// </summary>
    [Serializable]
    public class FixFeedback
    {
        /// <summary>
        /// Default constructor for the part needed object.
        /// </summary>
        public FixFeedback()
        {
            this.Id = new Guid();
            this.FixNameId = null;
        }

        /// <summary>
        /// Gets or sets the <see cref="Guid"/> ID of the fix feedback.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Guid"/> ID of the fix name.
        /// </summary>
        public Guid? FixNameId { get; set; }

        /// <summary>
        ///  Gets or sets the <see cref="string"/> fix name provided.
        /// </summary>
        public string FixPerformed { get; set; }

        /// <summary>
        /// Gets or set the <see cref="string"/> primary error code for the fix.
        /// </summary>
        public string PrimaryErrorCode { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="string"/> error codes that the technician thinks should apply to the fix.
        /// </summary>
        public string ErrorCodesThatApply { get; set; }

        /// <summary>
        /// Gets or set the <see cref="string"/> reason the vehicle could not be fixed.
        /// </summary>
        public string CouldNotFixReason { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="AverageDiagnosticTime"/> average diagnostic time the fix takes.
        /// </summary>
        //public int AverageDiagnosticTime { get; set; }
        public decimal AverageDiagnosticTime { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="FrequencyEncountered"/> frequency of the encounter for this fix.
        /// </summary>
        public int FrequencyEncountered { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="FixDifficultyRating"/> fix difficulty rating for the fix.
        /// </summary>
        public FixDifficultyRating FixDifficultyRating { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="string"/> of the technicians comments of how to diagnose the fix (formly basic tools required).
        /// </summary>
        public string BasicToolsRequired { get; set; }

        /// <summary>
        /// Gets or sets <see cref="string"/> technicians comments to specify any special tools required.
        /// </summary>
        public string SpecialtyToolsRequired { get; set; }

        /// <summary>
        /// Gets or sets <see cref="string"/> technicians comments to specify any tips or shortcuts for the repair.
        /// </summary>
        public string TipsAndTricks { get; set; }

        /// <summary>
        /// Container class holds all of the <see cref="FixFeedbackPart"/> objects associated with this DiagnosticReportFixFeedback
        /// </summary>
        public FixFeedbackPart[] FixFeedbackPart { get; set; }

        //ToolDB_
        /// <summary>
        /// Container class holds all of the <see cref="FixFeedbackTool"/> objects associated with this DiagnosticReportFixFeedback
        /// </summary>
        public FixFeedbackTool[] FixFeedbackTool { get; set; }

        //ToolDB_

        /// <summary>
        /// Labor time
        /// </summary>
        public decimal LaborTime { get; set; } //#Sprint18IA106
    }
}
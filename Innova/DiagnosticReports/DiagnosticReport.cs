using Innova.Core.DSL;
using Innova.Devices;
using Innova.DiagnosticReports.Feedback;
using Innova.DtcCodes;
using Innova.Fixes;
using Innova.InnovaDLLParsers;
using Innova.Markets;
using Innova.Symptoms;
using Innova.Users;
using Innova.Utilities.Shared;
using Innova.Utilities.Shared.Enums;
using Innova.Utilities.Shared.Model;
using Innova.Vehicles;
using Innova2.VehicleDataLib.Entities.Version5;
using Metafuse3;
using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using Metafuse3.NullableTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Innova.DiagnosticReports
{
    /// <summary>
    /// The DiagnosticReport object handles the business logic and data access for the specialized business object, DiagnosticReport.
    /// The class inherits from <see cref="BusinessObjectBase"/> class.
    /// </summary>
    /// <example>
    /// See the examples of how to construct the DiagnosticReport object.
    ///
    /// To create a new instance of a new of DiagnosticReport.
    /// <code>DiagnosticReport o = (DiagnosticReport)Registry.CreateInstance(typeof(DiagnosticReport));</code>
    ///
    /// To create an new instance of an existing DiagnosticReport.
    /// <code>DiagnosticReport o = (DiagnosticReport)Registry.CreateInstance(typeof(DiagnosticReport), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of DiagnosticReport, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectBase.ConnectionString"/> to the <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable()]
    public partial class DiagnosticReport : InnovaBusinessObjectBase
    {
        public static bool CacheOutFreezeFrameValues = false;
        public static bool PerformFreezeFrameSorting = false;
        public static bool SaveNormalSort = false;
        public static string ReportType = "";

        // data object variables
        private Symptom symptom;

        private SymptomDiagnosticReportItemCollection symptomDiagnosticReportItemCollection;

        private DiagnosticReportResult diagnosticReportResult;
        private DiagnosticReportFixFeedback diagnosticReportFixFeedback;
        private User user;

        private bool hasMasterTechsAssigned = false;
        private string masterTechsAssignedIdList = "";
        private bool isMasterTechsAssignedDirty = false;
        private UserCollection masterTechsAssigned;

        private NullableDateTime masterTechProvideFixFeedbackByOverrideDateTimeUTC = NullableDateTime.Null;
        private AdminUser pwrAdminUserWorkingOnFix;
        private NullableDateTime pwrAdminUserWorkingOnFixAssignedDateTimeUTC;
        private AdminUser absAdminUserWorkingOnFix;
        private NullableDateTime absAdminUserWorkingOnFixAssignedDateTimeUTC;
        private AdminUser srsAdminUserWorkingOnFix;
        private NullableDateTime srsAdminUserWorkingOnFixAssignedDateTimeUTC;
        private Vehicle vehicle;
        private Device device;

        private string repairOrderNumber = "";
        private int vehicleMileage = 0;
        private Market? market;
        private Language? language;
        private Currency? currency;
        private decimal? currencyExchangeRate;
        private string rawUploadString = "";
        private string rawFreezeFrameDataString = "";
        private string rawMonitorsDataString = "";

        private SoftwareType softwareType = SoftwareType.CarMD;  //default to CarMD but should change to
        private ToolTypeFormat toolTypeFormat = ToolTypeFormat.CarMD; //default to carmd but should change to
        private string softwareVersion = "";
        private string firmwareVersion = "";
        private string vin = ""; //CAN THIS BE DEPRICATED?  Vehicle has VIN on it.

        private DiagnosticReportFixStatus pwrDiagnosticReportFixStatusWhenCreated = DiagnosticReportFixStatus.FixNotNeeded;
        private DiagnosticReportFixStatus obd1DiagnosticReportFixStatusWhenCreated = DiagnosticReportFixStatus.FixNotNeeded;
        private DiagnosticReportFixStatus absDiagnosticReportFixStatusWhenCreated = DiagnosticReportFixStatus.FixNotNeeded;
        private DiagnosticReportFixStatus srsDiagnosticReportFixStatusWhenCreated = DiagnosticReportFixStatus.FixNotNeeded;

        private DiagnosticReportFixStatus pwrDiagnosticReportFixStatus = DiagnosticReportFixStatus.FixNotNeeded;
        private DiagnosticReportFixStatus obd1DiagnosticReportFixStatus = DiagnosticReportFixStatus.FixNotNeeded;
        private DiagnosticReportFixStatus absDiagnosticReportFixStatus = DiagnosticReportFixStatus.FixNotNeeded;
        private DiagnosticReportFixStatus srsDiagnosticReportFixStatus = DiagnosticReportFixStatus.FixNotNeeded;

        private string pwrDiagnosticReportFixCancelReason = "";
        private string obd1DiagnosticReportFixCancelReason = "";
        private string absDiagnosticReportFixCancelReason = "";
        private string srsDiagnosticReportFixCancelReason = "";

        //2010-02-21 STW, we need to add the date that we found the fix so that we know the difference between when the report was created and when we "closed" this status...
        private NullableDateTime pwrDiagnosticReportFixStatusClosedDateTimeUTC = NullableDateTime.Null;

        private NullableDateTime obd1DiagnosticReportFixStatusClosedDateTimeUTC = NullableDateTime.Null;
        private NullableDateTime absDiagnosticReportFixStatusClosedDateTimeUTC = NullableDateTime.Null;
        private NullableDateTime srsDiagnosticReportFixStatusClosedDateTimeUTC = NullableDateTime.Null;

        //STW 2010-02-21 We still need these properties, the dates the fixes need to be delivered by....
        //this is info for Bob's team to know how to process these things.  These will be inputs that can be sent to us...
        //the difference between the closed date and this date would be some performance data....
        private NullableDateTime pwrFixNotFoundFixPromisedByDateTimeUTC = NullableDateTime.Null;

        private NullableDateTime obd1FixNotFoundFixPromisedByDateTimeUTC = NullableDateTime.Null;
        private NullableDateTime absFixNotFoundFixPromisedByDateTimeUTC = NullableDateTime.Null;
        private NullableDateTime srsFixNotFoundFixPromisedByDateTimeUTC = NullableDateTime.Null;

        /// <summary>
        /// Gets or sets the <see cref="NullableDateTime"/> date and time the update no fix process is completed (meaning the web services have returned all fixes that we are going to for this report).
        /// The flag is used to prevent unnecessary hits beyond just this diagnostic report.   Once set and the update no fix process is called, then processing is stopped to reduce load.
        /// This also indicates the difference between the last date the
        /// </summary>
        private NullableDateTime noFixProcessCompletedAndSentDateTimeUTC = NullableDateTime.Null;

        private string pwrMilCode = "";             // primaryErrorCode
        private StringCollection pwrStoredCodes;                    // storedErrorCodes;
        private string pwrStoredCodesString = "";       // storedErrorCodesString;
        private StringCollection pwrPendingCodes;               // pendingErrorCodes;
        private string pwrPendingCodesString = "";      // pendingErrorCodesString;

        private StringCollection pwrPermanentCodes;              // permanentErrorCodes;
        private string pwrPermanentCodesString = "";   // permanentCodesString;

        /* STW 2010-01-22 */

        //NEW add these and update powertrain to work as simply as possible with one model*
        private string absStoredCodesString = "";

        private string absPendingCodesString = "";
        private string srsStoredCodesString = "";
        private string srsPendingCodesString = "";
        private string obd1StoredCodesString = "";
        private string obd1PendingCodesString = "";
        private string enhancedDtcsString = "";

        private ToolLEDStatus toolLEDStatus = ToolLEDStatus.Error;//this is default for now but should always be set
        private ToolMilStatus toolMilStatus = ToolMilStatus.Unknown;//this is default for now but should always be set

        private DateTime updatedDateTimeUTC = DateTime.UtcNow;
        private DateTime createdDateTimeUTC = DateTime.UtcNow;
        private NullableDateTime requestedTechnicianContactDateTimeUTC = NullableDateTime.Null;
        private string requestedTechnicianContactComments = "";

        private bool isManualReport;
        private DiagnosticReportType manualDiagnosticReportType; //ADD this would figure out type...

        private bool isPwrObd1FixFeedbackRequired;
        private bool isPwrObd2FixFeedbackRequired;
        private bool isAbsFixFeedbackRequired;
        private bool isSrsFixFeedbackRequired;

        private bool? pwrIsFixNotificationRequested;
        private bool? srsIsFixNotificationRequested;
        private bool? absIsFixNotificationRequested;

        private NullableDateTime masterTechNotificationsSentDateTimeUTC = NullableDateTime.Null;

        private DiagnosticReportFixFeedbackCollection fixFeedbacks = null;
        private DiagnosticReportFeedbackCollection diagnosticReportFeedbacks;

        private DtcCodeViewCollection obd1StoredCodes;
        private DtcCodeViewCollection obd1PendingCodes;
        private DtcCodeViewCollection absStoredCodes;
        private DtcCodeViewCollection absPendingCodes;
        private DtcCodeViewCollection srsStoredCodes;
        private DtcCodeViewCollection srsPendingCodes;
        private DtcCodeViewCollection enhancedDtcs;

        private bool pwrFixFoundAfterLastFixLookup;
        private bool obd1FixFoundAfterLastFixLookup;
        private bool absFixFoundAfterLastFixLookup;
        private bool srsFixFoundAfterLastFixLookup;

        private NullableDateTime fixProvidedDateTimeUTC = NullableDateTime.Null;
        private NullableDateTime whatFixedMyCarEmailSentDateTimeUTC = NullableDateTime.Null;
        private NullableDateTime pastDueEmailSentDateTimeUTC = NullableDateTime.Null;

        private string externalSystemReportId = "";

        public OemDtcInfo OemDtcInfo { get; set; } //#SP36

        public VehicleDataEx VehicleDataLibEx { get; set; }
        public ToolInformation ToolInformation { get; set; }

        public string SILStatus { get; set; }

        #region System Constructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new (one that does not exist, assigning a new <see cref="Guid"/> Id). DiagnosticReport object.
        /// In order to create a new DiagnosticReport which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <example>
        /// <code>
        /// DiagnosticReport o = (DiagnosticReport)Registry.CreateInstance(typeof(DiagnosticReport));
        /// </code>
        /// </example>
        protected internal DiagnosticReport() : base()
        {
            IsObjectCreated = true;
            IsObjectLoaded = true;
            IsObjectDirty = true;
        }

        /// <summary>
        /// Constructor creates an existing (one that exists, and already has a <see cref="Guid"/> Id).  DiagnosticReport object.
        /// In order to create an existing DiagnosticReport object, which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <param name="id">The <see cref="Guid"/> or SQL uniqueidentifier indentification "Id" of the current object.</param>
        /// <example>
        /// <code>
        /// DiagnosticReport o = (DiagnosticReport)Registry.CreateInstance(typeof(DiagnosticReport), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));
        /// </code>
        /// </example>
        protected internal DiagnosticReport(Guid id) : base(id)
        {
            this.id = id;
        }

        #endregion System Constructors

        #region System Properties DO NOT EDIT

        // private member variables used to handle the system properties.
        private bool isObjectDirty = false;

        private bool isObjectLoaded = false;
        private bool isObjectCreated = false;
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
            get
            {
                return isObjectLoaded;
            }
            set
            {
                isObjectLoaded = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/> flag which determines whether or not the current object has been updated and needs to be saved to the database.
        /// The property is marked new so that each layer of the inheritence chain has it's own IsObjectDirty property.
        /// Base layers may or may not be dirty.  The IsObjectDirty flag should set to true when a property is updated, and the object automatically sets the IsObjectDirty flag to false when the object is saved successfully.
        /// The IsObjectDirty property is used primarly for the internal Save methods to determine whether or not the object needs to save itself when the Save method is invoked.
        /// </summary>
        public new bool IsObjectDirty
        {
            get
            {
                return isObjectDirty;
            }
            set
            {
                isObjectDirty = value;

                if (!isObjectDirty)
                {
                    isObjectCreated = false;
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
            get
            {
                return isObjectCreated;
            }
            set
            {
                isObjectCreated = value;
            }
        }

        #endregion System Properties DO NOT EDIT

        #region Object Properties

        /**************************************************************************************
		 *
		 * Object Properties: Add Custom Fields
		 *
		 * **************************************************************************************/

        /// <summary>
        /// Gets or sets the <see cref="NullableGuid"/> for the diagnostic report.
        /// </summary>
        public Symptom Symptom
        {
            get
            {
                this.EnsureLoaded();
                return this.symptom;
            }
            set
            {
                this.EnsureLoaded();
                if (this.symptom != value)
                {
                    this.IsObjectDirty = true;
                    this.symptom = value;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public SymptomDiagnosticReportItemCollection SymptomDiagnosticReportItemCollection
        {
            get
            {
                if (symptomDiagnosticReportItemCollection == null)
                {
                    //Load Symptom from DB
                    symptomDiagnosticReportItemCollection = new SymptomDiagnosticReportItemCollection(this.Registry);
                    var symptomReports =
                   SymptomDiagnosticReportItemCollection.Search(this.Registry, $"<GuidList><Guid id=\"{this.id}\"/></GuidList>");

                    if (symptomReports != null && symptomReports.Count > 0)
                    {
                        foreach (SymptomDiagnosticReportItem symptomDiagnostic in symptomReports)
                        {
                            this.symptomDiagnosticReportItemCollection.Add(symptomDiagnostic);
                        }
                    }
                }
                //Search from
                return symptomDiagnosticReportItemCollection;
            }
        }

        /// <summary>
        /// Primary Symptom of Report if any
        /// </summary>
        public SymptomDiagnosticReportItem PrimarySymptomDiagnosticReportItem
        {
            get
            {
                if (SymptomDiagnosticReportItemCollection.Count == 0) return null;

                foreach (SymptomDiagnosticReportItem st in SymptomDiagnosticReportItemCollection)
                {
                    if (st.PrimarySymptomDiagnosticReportItemId == null)
                        return st;
                }

                return null;
            }
        }

        /// <summary>
        /// Symptoms ref to this report, assigned when creating new in WS
        /// These Ids used to create Report result fix and set reort fix status
        /// </summary>
        ///

        private List<Guid> symptomGuids;

        /// <summary>
        ///
        /// </summary>
        public List<Guid> SymptomGuids
        {
            get
            {
                if (symptomGuids == null)
                {
                    symptomGuids = new List<Guid>();
                    foreach (SymptomDiagnosticReportItem st in this.SymptomDiagnosticReportItemCollection)
                    {
                        symptomGuids.Add(st.Symptom.Id);
                    }
                }

                return symptomGuids;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DiagnosticReportResult"/> for the diagnostic report.
        /// </summary>
        [PropertyDefinition("Result", "The error codes, fixes and causes that were looked up for the report.")]
        public DiagnosticReportResult DiagnosticReportResult
        {
            get
            {
                this.EnsureLoaded();
                return this.diagnosticReportResult;
            }
            set
            {
                this.EnsureLoaded();
                if (this.diagnosticReportResult != value)
                {
                    this.IsObjectDirty = true;
                    this.diagnosticReportResult = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets <see cref="DiagnosticReportFixFeedback"/> for the diagnostic report fix feedback.
        /// </summary>
        public DiagnosticReportFixFeedback DiagnosticReportFixFeedback
        {
            get
            {
                this.EnsureLoaded();
                return this.diagnosticReportFixFeedback;
            }
            set
            {
                this.EnsureLoaded();
                if (this.diagnosticReportFixFeedback != value)
                {
                    this.IsObjectDirty = true;
                    this.diagnosticReportFixFeedback = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="User"/> who created the diagnostic report.
        /// </summary>
        [PropertyDefinition("User", "The user who created the report.")]
        public User User
        {
            get
            {
                this.EnsureLoaded();
                return this.user;
            }
            set
            {
                this.EnsureLoaded();
                if (this.user != value)
                {
                    this.IsObjectDirty = true;
                    this.user = value;
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="bool"/> flag indicating whether or not Master Techs have been assigned.
        /// </summary>
        [PropertyDefinition("Master Techs Assigned", "Indicates that Master Techs have been assigned.")]
        public bool HasMasterTechsAssigned
        {
            get
            {
                this.EnsureLoaded();
                return this.hasMasterTechsAssigned;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableDateTime"/> override of when the MasterTech is allowed to provide fix feedback by.
        /// </summary>
        [PropertyDefinition("Master Tech Fix Feedback Cutoff", "The date/time override of when the MasterTech is allowed to provide fix feedback by.")]
        public NullableDateTime MasterTechProvideFixFeedbackByOverrideDateTimeUTC
        {
            get
            {
                this.EnsureLoaded();
                return this.masterTechProvideFixFeedbackByOverrideDateTimeUTC;
            }
            set
            {
                this.EnsureLoaded();
                if (this.masterTechProvideFixFeedbackByOverrideDateTimeUTC != value)
                {
                    this.IsObjectDirty = true;
                    this.masterTechProvideFixFeedbackByOverrideDateTimeUTC = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="AdminUser"/> who is currently working on a powertrain fix for this report.
        /// </summary>
        [PropertyDefinition("Admin Working On Powertrain Fix", "The admin who is currently working on a powertrain fix for the report.")]
        public AdminUser PwrAdminUserWorkingOnFix
        {
            get
            {
                this.EnsureLoaded();
                return this.pwrAdminUserWorkingOnFix;
            }
            set
            {
                this.EnsureLoaded();
                if (this.pwrAdminUserWorkingOnFix != value)
                {
                    this.IsObjectDirty = true;
                    this.pwrAdminUserWorkingOnFix = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableDateTime"/> when the admin user was assigned to work on a powertrain fix for this report.
        /// </summary>
        [PropertyDefinition("Date Admin Assigned to Find Powertrain Fix", "The admin when the admin user was assigned to work on a powertrain fix for the report.")]
        public NullableDateTime PwrAdminUserWorkingOnFixAssignedDateTimeUTC
        {
            get
            {
                this.EnsureLoaded();
                return this.pwrAdminUserWorkingOnFixAssignedDateTimeUTC;
            }
            set
            {
                this.EnsureLoaded();
                if (this.pwrAdminUserWorkingOnFixAssignedDateTimeUTC != value)
                {
                    this.IsObjectDirty = true;
                    this.pwrAdminUserWorkingOnFixAssignedDateTimeUTC = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="AdminUser"/> who is currently working on a ABS fix for this report.
        /// </summary>
        [PropertyDefinition("Admin Working On ABS Fix", "The admin who is currently working on a ABS fix for the report.")]
        public AdminUser AbsAdminUserWorkingOnFix
        {
            get
            {
                this.EnsureLoaded();
                return this.absAdminUserWorkingOnFix;
            }
            set
            {
                this.EnsureLoaded();
                if (this.absAdminUserWorkingOnFix != value)
                {
                    this.IsObjectDirty = true;
                    this.absAdminUserWorkingOnFix = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableDateTime"/> when the admin user was assigned to work on a abs fix for this report.
        /// </summary>
        [PropertyDefinition("Date Admin Assigned to Find ABS Fix", "The admin when the admin user was assigned to work on a ABS fix for the report.")]
        public NullableDateTime AbsAdminUserWorkingOnFixAssignedDateTimeUTC
        {
            get
            {
                this.EnsureLoaded();
                return this.absAdminUserWorkingOnFixAssignedDateTimeUTC;
            }
            set
            {
                this.EnsureLoaded();
                if (this.absAdminUserWorkingOnFixAssignedDateTimeUTC != value)
                {
                    this.IsObjectDirty = true;
                    this.absAdminUserWorkingOnFixAssignedDateTimeUTC = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="AdminUser"/> who is currently working on a SRS fix for this report.
        /// </summary>
        [PropertyDefinition("Admin Working On SRS Fix", "The admin who is currently working on a SRS fix for the report.")]
        public AdminUser SrsAdminUserWorkingOnFix
        {
            get
            {
                this.EnsureLoaded();
                return this.srsAdminUserWorkingOnFix;
            }
            set
            {
                this.EnsureLoaded();
                if (this.srsAdminUserWorkingOnFix != value)
                {
                    this.IsObjectDirty = true;
                    this.srsAdminUserWorkingOnFix = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableDateTime"/> when the admin user was assigned to work on a SRS fix for this report.
        /// </summary>
        [PropertyDefinition("Date Admin Assigned to Find SRS Fix", "The admin when the admin user was assigned to work on a SRS fix for the report.")]
        public NullableDateTime SrsAdminUserWorkingOnFixAssignedDateTimeUTC
        {
            get
            {
                this.EnsureLoaded();
                return this.srsAdminUserWorkingOnFixAssignedDateTimeUTC;
            }
            set
            {
                this.EnsureLoaded();
                if (this.srsAdminUserWorkingOnFixAssignedDateTimeUTC != value)
                {
                    this.IsObjectDirty = true;
                    this.srsAdminUserWorkingOnFixAssignedDateTimeUTC = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Vehicle"/> the diagnostic report is for.
        /// </summary>
        [PropertyDefinition("Vehicle", "The vehicle on the report.")]
        public Vehicle Vehicle
        {
            get
            {
                this.EnsureLoaded();
                return this.vehicle;
            }
            set
            {
                this.EnsureLoaded();
                if (this.vehicle != value)
                {
                    this.IsObjectDirty = true;
                    this.vehicle = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Device"/> that uploaded the raw report data.
        /// </summary>
        [PropertyDefinition("Device", "The device that uploaded the raw report data.")]
        public Device Device
        {
            get
            {
                this.EnsureLoaded();
                return this.device;
            }
            set
            {
                this.EnsureLoaded();
                if (this.device != value)
                {
                    this.IsObjectDirty = true;
                    this.device = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> repair order number.
        /// </summary>
        public string RepairOrderNumber
        {
            get
            {
                this.EnsureLoaded();
                return this.repairOrderNumber;
            }
            set
            {
                this.EnsureLoaded();
                if (this.repairOrderNumber != value)
                {
                    this.IsObjectDirty = true;
                    this.repairOrderNumber = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/> mileage of the vehicle on this report.
        /// </summary>
        public int VehicleMileage
        {
            get
            {
                this.EnsureLoaded();
                return this.vehicleMileage;
            }
            set
            {
                this.EnsureLoaded();
                if (this.vehicleMileage != value)
                {
                    this.IsObjectDirty = true;
                    this.vehicleMileage = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the default <see cref="Market"/> for the report.
        /// </summary>
        public Market Market
        {
            get
            {
                this.EnsureLoaded();
                if (!this.market.HasValue)
                {
                    this.market = this.RuntimeInfo.CurrentMarket;
                }
                return this.market.Value;
            }
            set
            {
                this.EnsureLoaded();
                if (this.market != value)
                {
                    this.IsObjectDirty = true;
                    this.market = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the default <see cref="Language"/> for the report.
        /// </summary>
        public Language Language
        {
            get
            {
                this.EnsureLoaded();
                if (!this.language.HasValue)
                {
                    this.language = this.RuntimeInfo.CurrentLanguage;
                }
                return this.language.Value;
            }
            set
            {
                this.EnsureLoaded();
                if (this.language != value)
                {
                    this.IsObjectDirty = true;
                    this.language = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the default <see cref="Currency"/> for the report.
        /// </summary>
        public Currency Currency
        {
            get
            {
                this.EnsureLoaded();
                if (!this.currency.HasValue)
                {
                    this.currency = this.RuntimeInfo.CurrentCurrency;
                }
                return this.currency.Value;
            }
            set
            {
                this.EnsureLoaded();
                if (this.currency != value)
                {
                    this.IsObjectDirty = true;
                    this.currency = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="decimal"/> exchange rate used at the time the report was created.
        /// </summary>
        public decimal CurrencyExchangeRate
        {
            get
            {
                this.EnsureLoaded();
                if (!this.currencyExchangeRate.HasValue)
                {
                    this.currencyExchangeRate = this.RuntimeInfo.CurrentCurrencyExchangeRate.ExchangeRatePerUSD;
                }
                return this.currencyExchangeRate.Value;
            }
            set
            {
                this.EnsureLoaded();
                if (this.currencyExchangeRate != value)
                {
                    this.IsObjectDirty = true;
                    this.currencyExchangeRate = value;
                }
            }
        }

        /// [Obsolete("This property has been depricated. Use Vehicle.VIN instead.")]
        /// <summary>
        /// Gets or sets the <see cref="string"/> vehicle identification number.
        /// </summary>
        public string VIN
        {
            get
            {
                this.EnsureLoaded();
                return this.vin;
            }
            set
            {
                this.EnsureLoaded();
                if (this.vin != value)
                {
                    this.IsObjectDirty = true;
                    this.vin = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> reason the powertrain fix lookup was canceled.
        /// </summary>
        public string PwrDiagnosticReportFixCancelReason
        {
            get
            {
                this.EnsureLoaded();
                return this.pwrDiagnosticReportFixCancelReason;
            }
            set
            {
                this.EnsureLoaded();
                if (this.pwrDiagnosticReportFixCancelReason != value)
                {
                    this.IsObjectDirty = true;
                    this.pwrDiagnosticReportFixCancelReason = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> reason the OBD1 fix lookup was canceled.
        /// </summary>
        public string Obd1DiagnosticReportFixCancelReason
        {
            get
            {
                this.EnsureLoaded();
                return this.obd1DiagnosticReportFixCancelReason;
            }
            set
            {
                this.EnsureLoaded();
                if (this.obd1DiagnosticReportFixCancelReason != value)
                {
                    this.IsObjectDirty = true;
                    this.obd1DiagnosticReportFixCancelReason = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> reason the ABS fix lookup was canceled.
        /// </summary>
        public string AbsDiagnosticReportFixCancelReason
        {
            get
            {
                this.EnsureLoaded();
                return this.absDiagnosticReportFixCancelReason;
            }
            set
            {
                this.EnsureLoaded();
                if (this.absDiagnosticReportFixCancelReason != value)
                {
                    this.IsObjectDirty = true;
                    this.absDiagnosticReportFixCancelReason = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> reason the SRS fix lookup was canceled.
        /// </summary>
        public string SrsDiagnosticReportFixCancelReason
        {
            get
            {
                this.EnsureLoaded();
                return this.srsDiagnosticReportFixCancelReason;
            }
            set
            {
                this.EnsureLoaded();
                if (this.srsDiagnosticReportFixCancelReason != value)
                {
                    this.IsObjectDirty = true;
                    this.srsDiagnosticReportFixCancelReason = value;
                }
            }
        }

        #region DTCs

        /// <summary>
        /// Gets or sets the <see cref="StringCollection"/> of error codes.
        /// </summary>
        public StringCollection PwrAllCodes
        {
            get
            {
                StringCollection codes = new StringCollection();

                if (!string.IsNullOrEmpty(this.PwrMilCode))
                {
                    codes.Add(this.PwrMilCode);
                }

                foreach (string code in this.PwrStoredCodes)
                {
                    if (!string.IsNullOrEmpty(code) && !codes.Contains(code))
                    {
                        codes.Add(code);
                    }
                }
                foreach (string code in this.PwrPendingCodes)
                {
                    if (!string.IsNullOrEmpty(code) && !codes.Contains(code))
                    {
                        codes.Add(code);
                    }
                }

                //Nam add 4/18/2017 - Add Permanent Codes to PwrAllCodes
                foreach (string code in this.PwrPermanentCodes)
                {
                    if (!string.IsNullOrEmpty(code) && !codes.Contains(code))
                    {
                        codes.Add(code);
                    }
                }
                //Nam add 4/18/2017 - Add Permanent Codes to PwrAllCodes

                return codes;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> powertrain MIL error code. (Or Freeze Frame DTC)
        /// </summary>
        [PropertyDefinition("Primary DTC", "The primary powertrain DTC.")]
        public string PwrMilCode
        {
            get
            {
                this.EnsureLoaded();
                return this.pwrMilCode;
            }
            set
            {
                this.EnsureLoaded();
                if (this.pwrMilCode != value)
                {
                    this.IsObjectDirty = true;
                    this.pwrMilCode = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="StringCollection"/> of secondary error codes.
        /// </summary>
        public StringCollection SecondaryErrorCodes
        {
            get
            {
                StringCollection codes = new StringCollection();

                foreach (string code in this.PwrStoredCodes)
                {
                    if (!string.IsNullOrEmpty(code) && code.ToLower() != this.PwrMilCode.ToLower() && !codes.Contains(code))
                    {
                        codes.Add(code);
                    }
                }
                foreach (string code in this.PwrPendingCodes)
                {
                    if (!string.IsNullOrEmpty(code) && code.ToLower() != this.PwrMilCode.ToLower() && !codes.Contains(code))
                    {
                        codes.Add(code);
                    }
                }

                return codes;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="StringCollection"/> of powertrain stored codes.
        /// </summary>
        public StringCollection PwrStoredCodes
        {
            get
            {
                this.EnsureLoaded();
                if (this.pwrStoredCodes == null)
                {
                    this.pwrStoredCodes = this.GetStringCollectionFromCommaSeparatedList(this.pwrStoredCodesString);
                }

                return this.pwrStoredCodes;
            }
            set
            {
                this.EnsureLoaded();
                //if(storedErrorCodes != value)
                //{
                this.IsObjectDirty = true;
                this.pwrStoredCodes = value;
                this.pwrStoredCodesString = this.GetCommaSeparatedListFromStringCollection(this.pwrStoredCodes);
                //}
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="StringCollection"/> of pending error codes.
        /// </summary>
        public StringCollection PwrPendingCodes
        {
            get
            {
                this.EnsureLoaded();
                if (this.pwrPendingCodes == null)
                {
                    this.pwrPendingCodes = this.GetStringCollectionFromCommaSeparatedList(this.pwrPendingCodesString);
                }

                return this.pwrPendingCodes;
            }
            set
            {
                this.EnsureLoaded();

                this.IsObjectDirty = true;
                this.pwrPendingCodes = value;
                this.pwrPendingCodesString = this.GetCommaSeparatedListFromStringCollection(this.pwrPendingCodes);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="StringCollection"/> of pending error codes.
        /// </summary>
        public StringCollection PwrPermanentCodes
        {
            get
            {
                this.EnsureLoaded();
                if (this.pwrPermanentCodes == null)
                {
                    this.pwrPermanentCodes = this.GetStringCollectionFromCommaSeparatedList(this.pwrPermanentCodesString);
                }

                return this.pwrPermanentCodes;
            }
            set
            {
                this.EnsureLoaded();

                this.IsObjectDirty = true;
                this.pwrPermanentCodes = value;
                this.pwrPermanentCodesString = this.GetCommaSeparatedListFromStringCollection(this.pwrPermanentCodes);
            }
        }

        /// <summary>
        /// Gets the <see cref="DtcCodeViewCollection"/> of <see cref="DtcCodeView"/> objects for the ABS stored codes.
        /// </summary>
        public DtcCodeViewCollection AbsStoredCodes
        {
            get
            {
                if (this.absStoredCodes == null)
                {
                    this.absStoredCodes = this.GetDtcCodeViewCollectionFromDelimitedString(this.absStoredCodesString);
                }

                return this.absStoredCodes;
            }
        }

        /// <summary>
        /// Gets the <see cref="DtcCodeViewCollection"/> of <see cref="DtcCodeView"/> objects for the ABS pending codes.
        /// </summary>
        public DtcCodeViewCollection AbsPendingCodes
        {
            get
            {
                if (this.absPendingCodes == null)
                {
                    this.absPendingCodes = this.GetDtcCodeViewCollectionFromDelimitedString(this.absPendingCodesString);
                }

                return this.absPendingCodes;
            }
        }

        /// <summary>
        /// Gets the <see cref="StringCollection"/> of all ABS codes.
        /// </summary>
        public StringCollection AbsAllCodes
        {
            get
            {
                return this.GetAllErrorCodes(DiagnosticReportErrorCodeSystemType.ABS);
            }
        }

        /// <summary>
        /// Gets the <see cref="DtcCodeViewCollection"/> of <see cref="DtcCodeView"/> objects for the SRS stored codes.
        /// </summary>
        public DtcCodeViewCollection SrsStoredCodes
        {
            get
            {
                if (this.srsStoredCodes == null)
                {
                    this.srsStoredCodes = this.GetDtcCodeViewCollectionFromDelimitedString(this.srsStoredCodesString);
                }

                return this.srsStoredCodes;
            }
        }

        /// <summary>
        /// Gets the <see cref="DtcCodeViewCollection"/> of <see cref="DtcCodeView"/> objects for the SRS pending codes.
        /// </summary>
        public DtcCodeViewCollection SrsPendingCodes
        {
            get
            {
                if (this.srsPendingCodes == null)
                {
                    this.srsPendingCodes = this.GetDtcCodeViewCollectionFromDelimitedString(this.srsPendingCodesString);
                }

                return this.srsPendingCodes;
            }
        }

        /// <summary>
        /// Gets the <see cref="StringCollection"/> of all SRS codes.
        /// </summary>
        public StringCollection SrsAllCodes
        {
            get
            {
                return this.GetAllErrorCodes(DiagnosticReportErrorCodeSystemType.SRS);
            }
        }

        /// <summary>
        /// Gets the <see cref="DtcCodeViewCollection"/> of <see cref="DtcCodeView"/> objects for the OBD1 stored codes.
        /// </summary>
        public DtcCodeViewCollection Obd1StoredCodes
        {
            get
            {
                if (this.obd1StoredCodes == null)
                {
                    this.obd1StoredCodes = this.GetDtcCodeViewCollectionFromDelimitedString(this.obd1StoredCodesString);
                }

                return this.obd1StoredCodes;
            }
        }

        /// <summary>
        /// Gets the <see cref="DtcCodeViewCollection"/> of <see cref="DtcCodeView"/> objects for the OBD1 pending codes.
        /// </summary>
        public DtcCodeViewCollection Obd1PendingCodes
        {
            get
            {
                if (this.obd1PendingCodes == null)
                {
                    this.obd1PendingCodes = this.GetDtcCodeViewCollectionFromDelimitedString(this.obd1PendingCodesString);
                }

                return this.obd1PendingCodes;
            }
        }

        /// <summary>
        /// Gets the <see cref="StringCollection"/> of all OBD1 codes.
        /// </summary>
        public StringCollection Obd1AllCodes
        {
            get
            {
                return this.GetAllErrorCodes(DiagnosticReportErrorCodeSystemType.PowertrainOBD1);
            }
        }

        /// <summary>
        /// Gets the <see cref="DtcCodeViewCollection"/> of <see cref="DtcCodeView"/> objects for the enhanced DTCs.
        /// </summary>
        public DtcCodeViewCollection EnhancedDtcs
        {
            get
            {
                if (this.enhancedDtcs == null)
                {
                    this.enhancedDtcs = this.GetDtcCodeViewCollectionFromDelimitedString(this.enhancedDtcsString);
                }

                return this.enhancedDtcs;
            }
        }

        #endregion DTCs

        #region Tool Information

        /// <summary>
        /// Gets or sets the <see cref="string"/> raw upload string that was uploaded by the tool, setting the raw upload sets the properties
        /// </summary>
        public string RawUploadString
        {
            get
            {
                this.EnsureLoaded();
                return this.rawUploadString;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> raw freeze frame data string.
        /// </summary>
        public string RawFreezeFrameDataString
        {
            get
            {
                this.EnsureLoaded();
                return this.rawFreezeFrameDataString;
            }
            set
            {
                this.EnsureLoaded();
                if (this.rawFreezeFrameDataString != value)
                {
                    this.IsObjectDirty = true;
                    this.rawFreezeFrameDataString = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> raw monitors data string.
        /// </summary>
        public string RawMonitorsDataString
        {
            get
            {
                this.EnsureLoaded();
                return this.rawMonitorsDataString;
            }
            set
            {
                this.EnsureLoaded();
                if (this.rawMonitorsDataString != value)
                {
                    this.IsObjectDirty = true;
                    this.rawMonitorsDataString = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="SoftwareType"/> indicating the type of software which created the rawUploadString
        /// </summary>
        public SoftwareType SoftwareType
        {
            get
            {
                this.EnsureLoaded();
                return this.softwareType;
            }
            set
            {
                this.EnsureLoaded();
                if (this.softwareType != value)
                {
                    this.IsObjectDirty = true;
                    this.softwareType = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="ToolTypeFormat"/> indicating the type of tool which created the rawUploadString
        /// </summary>
        public ToolTypeFormat ToolTypeFormat
        {
            get
            {
                this.EnsureLoaded();
                return this.toolTypeFormat;
            }
            set
            {
                this.EnsureLoaded();
                if (this.toolTypeFormat != value)
                {
                    this.IsObjectDirty = true;
                    this.toolTypeFormat = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> software version.
        /// </summary>
        public string SoftwareVersion
        {
            get
            {
                this.EnsureLoaded();
                return this.softwareVersion;
            }
            set
            {
                this.EnsureLoaded();
                if (this.softwareVersion != value)
                {
                    this.IsObjectDirty = true;
                    this.softwareVersion = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> firmware version.
        /// </summary>
        public string FirmwareVersion
        {
            get
            {
                this.EnsureLoaded();
                return this.firmwareVersion;
            }
            set
            {
                this.EnsureLoaded();
                if (this.firmwareVersion != value)
                {
                    this.IsObjectDirty = true;
                    this.firmwareVersion = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="ToolLEDStatus"/> of the LED.
        /// </summary>
        public ToolLEDStatus ToolLEDStatus
        {
            get
            {
                this.EnsureLoaded();
                return this.toolLEDStatus;
            }
            set
            {
                this.EnsureLoaded();
                if (this.toolLEDStatus != value)
                {
                    this.IsObjectDirty = true;
                    this.toolLEDStatus = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="ToolMilStatus"/>.
        /// </summary>
        public ToolMilStatus ToolMilStatus
        {
            get
            {
                this.EnsureLoaded();
                return this.toolMilStatus;
            }
            set
            {
                this.EnsureLoaded();
                if (this.toolMilStatus != value)
                {
                    this.IsObjectDirty = true;
                    this.toolMilStatus = value;
                }
            }
        }

        #endregion Tool Information

        #region Flags

        /// <summary>
        /// Gets or sets the <see cref="bool"/> that indicates if this report was manually created.
        /// </summary>
        public bool IsManualReport
        {
            get
            {
                this.EnsureLoaded();
                return this.isManualReport;
            }
            set
            {
                this.EnsureLoaded();
                if (this.isManualReport != value)
                {
                    this.IsObjectDirty = true;
                    this.isManualReport = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/> flag that indicates if the user wants to be notifified when a powertrain fix for this report becomes available.
        /// </summary>
        public bool? PwrIsFixNotificationRequested
        {
            get
            {
                this.EnsureLoaded();
                return this.pwrIsFixNotificationRequested;
            }
            set
            {
                this.EnsureLoaded();
                if (this.pwrIsFixNotificationRequested != value)
                {
                    this.IsObjectDirty = true;
                    this.pwrIsFixNotificationRequested = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/> flag that indicates if the user wants to be notifified when a SRS fix for this report becomes available.
        /// </summary>
        public bool? SrsIsFixNotificationRequested
        {
            get
            {
                this.EnsureLoaded();
                return this.srsIsFixNotificationRequested;
            }
            set
            {
                this.EnsureLoaded();
                if (this.srsIsFixNotificationRequested != value)
                {
                    this.IsObjectDirty = true;
                    this.srsIsFixNotificationRequested = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/> flag that indicates if the user wants to be notifified when a ABS fix for this report becomes available.
        /// </summary>
        public bool? AbsIsFixNotificationRequested
        {
            get
            {
                this.EnsureLoaded();
                return this.absIsFixNotificationRequested;
            }
            set
            {
                this.EnsureLoaded();
                if (this.absIsFixNotificationRequested != value)
                {
                    this.IsObjectDirty = true;
                    this.absIsFixNotificationRequested = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/> flag indicating if fix feedback is needed for OBD1 codes.
        /// </summary>
        public bool IsPwrObd1FixFeedbackRequired
        {
            get
            {
                this.EnsureLoaded();
                return this.isPwrObd1FixFeedbackRequired;
            }
            set
            {
                this.EnsureLoaded();
                if (this.isPwrObd1FixFeedbackRequired != value)
                {
                    this.IsObjectDirty = true;
                    this.isPwrObd1FixFeedbackRequired = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/> flag indicating if fix feedback is needed for OBD2 codes.
        /// </summary>
        public bool IsPwrObd2FixFeedbackRequired
        {
            get
            {
                this.EnsureLoaded();
                return this.isPwrObd2FixFeedbackRequired;
            }
            set
            {
                this.EnsureLoaded();
                if (this.isPwrObd2FixFeedbackRequired != value)
                {
                    this.IsObjectDirty = true;
                    this.isPwrObd2FixFeedbackRequired = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/> flag indicating if fix feedback is needed for ABS codes.
        /// </summary>
        public bool IsAbsFixFeedbackRequired
        {
            get
            {
                this.EnsureLoaded();
                return this.isAbsFixFeedbackRequired;
            }
            set
            {
                this.EnsureLoaded();
                if (this.isAbsFixFeedbackRequired != value)
                {
                    this.IsObjectDirty = true;
                    this.isAbsFixFeedbackRequired = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/> flag indicating if fix feedback is needed for SRS codes.
        /// </summary>
        public bool IsSrsFixFeedbackRequired
        {
            get
            {
                this.EnsureLoaded();
                return this.isSrsFixFeedbackRequired;
            }
            set
            {
                this.EnsureLoaded();
                if (this.isSrsFixFeedbackRequired != value)
                {
                    this.IsObjectDirty = true;
                    this.isSrsFixFeedbackRequired = value;
                }
            }
        }

        #endregion Flags

        #region Statuses

        /// <summary>
        /// Gets or sets the <see cref="DiagnosticReportType"/> types of manual report. Only matter when IsManualReport is true.
        /// </summary>
        public DiagnosticReportType ManualDiagnosticReportType
        {
            get
            {
                this.EnsureLoaded();
                return this.manualDiagnosticReportType;
            }
            set
            {
                this.EnsureLoaded();
                if (this.manualDiagnosticReportType != value)
                {
                    this.IsObjectDirty = true;
                    this.manualDiagnosticReportType = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DiagnosticReportFixStatus"/>.
        /// </summary>
        public DiagnosticReportFixStatus PwrDiagnosticReportFixStatusWhenCreated
        {
            get
            {
                this.EnsureLoaded();
                return this.pwrDiagnosticReportFixStatusWhenCreated;
            }
            set
            {
                this.EnsureLoaded();
                if (this.pwrDiagnosticReportFixStatusWhenCreated != value)
                {
                    this.IsObjectDirty = true;
                    this.pwrDiagnosticReportFixStatusWhenCreated = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DiagnosticReportFixStatus"/>.
        /// </summary>
        public DiagnosticReportFixStatus Obd1DiagnosticReportFixStatusWhenCreated
        {
            get
            {
                this.EnsureLoaded();
                return this.obd1DiagnosticReportFixStatusWhenCreated;
            }
            set
            {
                this.EnsureLoaded();
                if (this.obd1DiagnosticReportFixStatusWhenCreated != value)
                {
                    this.IsObjectDirty = true;
                    this.obd1DiagnosticReportFixStatusWhenCreated = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DiagnosticReportFixStatus"/>.
        /// </summary>
        public DiagnosticReportFixStatus AbsDiagnosticReportFixStatusWhenCreated
        {
            get
            {
                this.EnsureLoaded();
                return this.absDiagnosticReportFixStatusWhenCreated;
            }
            set
            {
                this.EnsureLoaded();
                if (this.absDiagnosticReportFixStatusWhenCreated != value)
                {
                    this.IsObjectDirty = true;
                    this.absDiagnosticReportFixStatusWhenCreated = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DiagnosticReportFixStatus"/>.
        /// </summary>
        public DiagnosticReportFixStatus SrsDiagnosticReportFixStatusWhenCreated
        {
            get
            {
                this.EnsureLoaded();
                return this.srsDiagnosticReportFixStatusWhenCreated;
            }
            set
            {
                this.EnsureLoaded();
                if (this.srsDiagnosticReportFixStatusWhenCreated != value)
                {
                    this.IsObjectDirty = true;
                    this.srsDiagnosticReportFixStatusWhenCreated = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DiagnosticReportFixStatus"/>.
        /// </summary>
        public DiagnosticReportFixStatus PwrDiagnosticReportFixStatus
        {
            get
            {
                this.EnsureLoaded();
                return this.pwrDiagnosticReportFixStatus;
            }
            set
            {
                this.EnsureLoaded();
                if (this.pwrDiagnosticReportFixStatus != value)
                {
                    if (this.pwrDiagnosticReportFixStatus == DiagnosticReportFixStatus.FixNotFound && this.PwrDiagnosticReportFixStatusClosedDateTimeUTC.IsNull)
                    {
                        this.PwrDiagnosticReportFixStatusClosedDateTimeUTC = DateTime.UtcNow;
                    }

                    if (value == DiagnosticReportFixStatus.FixNotFound)
                    {
                        this.NoFixProcessCompletedAndSentDateTimeUTC = NullableDateTime.Null;
                    }

                    this.IsObjectDirty = true;
                    this.pwrDiagnosticReportFixStatus = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DiagnosticReportFixStatus"/>.
        /// </summary>
        public DiagnosticReportFixStatus Obd1DiagnosticReportFixStatus
        {
            get
            {
                this.EnsureLoaded();
                return this.obd1DiagnosticReportFixStatus;
            }
            set
            {
                this.EnsureLoaded();
                if (this.obd1DiagnosticReportFixStatus != value)
                {
                    if (this.obd1DiagnosticReportFixStatus == DiagnosticReportFixStatus.FixNotFound && this.Obd1DiagnosticReportFixStatusClosedDateTimeUTC.IsNull)
                    {
                        this.Obd1DiagnosticReportFixStatusClosedDateTimeUTC = DateTime.UtcNow;
                    }

                    if (value == DiagnosticReportFixStatus.FixNotFound)
                    {
                        this.NoFixProcessCompletedAndSentDateTimeUTC = NullableDateTime.Null;
                    }

                    this.IsObjectDirty = true;
                    this.obd1DiagnosticReportFixStatus = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DiagnosticReportFixStatus"/>.
        /// </summary>
        public DiagnosticReportFixStatus AbsDiagnosticReportFixStatus
        {
            get
            {
                this.EnsureLoaded();
                return this.absDiagnosticReportFixStatus;
            }
            set
            {
                this.EnsureLoaded();
                if (this.absDiagnosticReportFixStatus != value)
                {
                    if (this.absDiagnosticReportFixStatus == DiagnosticReportFixStatus.FixNotFound && this.AbsDiagnosticReportFixStatusClosedDateTimeUTC.IsNull)
                    {
                        this.AbsDiagnosticReportFixStatusClosedDateTimeUTC = DateTime.UtcNow;
                    }

                    if (value == DiagnosticReportFixStatus.FixNotFound)
                    {
                        this.NoFixProcessCompletedAndSentDateTimeUTC = NullableDateTime.Null;
                    }

                    this.IsObjectDirty = true;
                    this.absDiagnosticReportFixStatus = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DiagnosticReportFixStatus"/>.
        /// </summary>
        public DiagnosticReportFixStatus SrsDiagnosticReportFixStatus
        {
            get
            {
                this.EnsureLoaded();
                return this.srsDiagnosticReportFixStatus;
            }
            set
            {
                this.EnsureLoaded();
                if (this.srsDiagnosticReportFixStatus != value)
                {
                    if (this.srsDiagnosticReportFixStatus == DiagnosticReportFixStatus.FixNotFound && this.SrsDiagnosticReportFixStatusClosedDateTimeUTC.IsNull)
                    {
                        this.SrsDiagnosticReportFixStatusClosedDateTimeUTC = DateTime.UtcNow;
                    }

                    if (value == DiagnosticReportFixStatus.FixNotFound)
                    {
                        this.NoFixProcessCompletedAndSentDateTimeUTC = NullableDateTime.Null;
                    }

                    this.IsObjectDirty = true;
                    this.srsDiagnosticReportFixStatus = value;
                }
            }
        }

        #endregion Statuses

        /// <summary>
        /// Gets or sets the <see cref="NullableDateTime"/> when the OBD2 powertrain fix lookup process was completed and "closed".
        /// </summary>
        public NullableDateTime PwrDiagnosticReportFixStatusClosedDateTimeUTC
        {
            get
            {
                this.EnsureLoaded();
                return this.pwrDiagnosticReportFixStatusClosedDateTimeUTC;
            }
            set
            {
                this.EnsureLoaded();
                if (this.pwrDiagnosticReportFixStatusClosedDateTimeUTC != value)
                {
                    this.IsObjectDirty = true;
                    this.pwrDiagnosticReportFixStatusClosedDateTimeUTC = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableDateTime"/> when the OBD1 powertrain fix lookup process was completed and "closed".
        /// </summary>
        public NullableDateTime Obd1DiagnosticReportFixStatusClosedDateTimeUTC
        {
            get
            {
                this.EnsureLoaded();
                return this.obd1DiagnosticReportFixStatusClosedDateTimeUTC;
            }
            set
            {
                this.EnsureLoaded();
                if (this.obd1DiagnosticReportFixStatusClosedDateTimeUTC != value)
                {
                    this.IsObjectDirty = true;
                    this.obd1DiagnosticReportFixStatusClosedDateTimeUTC = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableDateTime"/> when the ABS fix lookup process was completed and "closed".
        /// </summary>
        public NullableDateTime AbsDiagnosticReportFixStatusClosedDateTimeUTC
        {
            get
            {
                this.EnsureLoaded();
                return this.absDiagnosticReportFixStatusClosedDateTimeUTC;
            }
            set
            {
                this.EnsureLoaded();
                if (this.absDiagnosticReportFixStatusClosedDateTimeUTC != value)
                {
                    this.IsObjectDirty = true;
                    this.absDiagnosticReportFixStatusClosedDateTimeUTC = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableDateTime"/> when the SRS fix lookup process was completed and "closed".
        /// </summary>
        public NullableDateTime SrsDiagnosticReportFixStatusClosedDateTimeUTC
        {
            get
            {
                this.EnsureLoaded();
                return this.srsDiagnosticReportFixStatusClosedDateTimeUTC;
            }
            set
            {
                this.EnsureLoaded();
                if (this.srsDiagnosticReportFixStatusClosedDateTimeUTC != value)
                {
                    this.IsObjectDirty = true;
                    this.srsDiagnosticReportFixStatusClosedDateTimeUTC = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableDateTime"/> when a OBD2 powertrain fix has been promised to the report owner.
        /// </summary>
        public NullableDateTime PwrFixNotFoundFixPromisedByDateTimeUTC
        {
            get
            {
                this.EnsureLoaded();
                return this.pwrFixNotFoundFixPromisedByDateTimeUTC;
            }
            set
            {
                this.EnsureLoaded();
                if (this.pwrFixNotFoundFixPromisedByDateTimeUTC != value)
                {
                    this.IsObjectDirty = true;
                    this.pwrFixNotFoundFixPromisedByDateTimeUTC = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableDateTime"/> when a OBD1 powertrain fix has been promised to the report owner.
        /// </summary>
        public NullableDateTime Obd1FixNotFoundFixPromisedByDateTimeUTC
        {
            get
            {
                this.EnsureLoaded();
                return this.obd1FixNotFoundFixPromisedByDateTimeUTC;
            }
            set
            {
                this.EnsureLoaded();
                if (this.obd1FixNotFoundFixPromisedByDateTimeUTC != value)
                {
                    this.IsObjectDirty = true;
                    this.obd1FixNotFoundFixPromisedByDateTimeUTC = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableDateTime"/> when a ABS fix has been promised to the report owner.
        /// </summary>
        public NullableDateTime AbsFixNotFoundFixPromisedByDateTimeUTC
        {
            get
            {
                this.EnsureLoaded();
                return this.absFixNotFoundFixPromisedByDateTimeUTC;
            }
            set
            {
                this.EnsureLoaded();
                if (this.absFixNotFoundFixPromisedByDateTimeUTC != value)
                {
                    this.IsObjectDirty = true;
                    this.absFixNotFoundFixPromisedByDateTimeUTC = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableDateTime"/> when a SRS fix has been promised to the report owner.
        /// </summary>
        public NullableDateTime SrsFixNotFoundFixPromisedByDateTimeUTC
        {
            get
            {
                this.EnsureLoaded();
                return this.srsFixNotFoundFixPromisedByDateTimeUTC;
            }
            set
            {
                this.EnsureLoaded();
                if (this.srsFixNotFoundFixPromisedByDateTimeUTC != value)
                {
                    this.IsObjectDirty = true;
                    this.srsFixNotFoundFixPromisedByDateTimeUTC = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableDateTime"/> date and time the update no fix process is completed (meaning the web services have returned all fixes that we are going to for this report).
        /// The flag is used to prevent unnecessary hits beyond just this diagnostic report.   Once set and the update no fix process is called, then processing is stopped to reduce load.
        /// This also indicates the difference between the last date the
        /// </summary>
        public NullableDateTime NoFixProcessCompletedAndSentDateTimeUTC
        {
            get
            {
                this.EnsureLoaded();
                return this.noFixProcessCompletedAndSentDateTimeUTC;
            }
            set
            {
                this.EnsureLoaded();
                if (this.noFixProcessCompletedAndSentDateTimeUTC != value)
                {
                    this.IsObjectDirty = true;
                    this.noFixProcessCompletedAndSentDateTimeUTC = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/> the diagnostic report was last updated (Refresh and Save would cause this).
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
                if (this.updatedDateTimeUTC != value)
                {
                    this.IsObjectDirty = true;
                    this.updatedDateTimeUTC = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/> the diagnostic report was created.
        /// </summary>
        [PropertyDefinition("Created", "Created", "Created", "Created", "The date/time the report was created.")]
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
                if (this.createdDateTimeUTC != value)
                {
                    this.IsObjectDirty = true;
                    this.createdDateTimeUTC = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableDateTime"/> when the user requested to be contacted by a technician.
        /// </summary>
        public NullableDateTime RequestedTechnicianContactDateTimeUTC
        {
            get
            {
                this.EnsureLoaded();
                return this.requestedTechnicianContactDateTimeUTC;
            }
            set
            {
                this.EnsureLoaded();
                if (this.requestedTechnicianContactDateTimeUTC != value)
                {
                    this.IsObjectDirty = true;
                    this.requestedTechnicianContactDateTimeUTC = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> comments the user submitted when they requested to be contacted by a technician.
        /// </summary>
        public string RequestedTechnicianContactComments
        {
            get
            {
                this.EnsureLoaded();
                return this.requestedTechnicianContactComments;
            }
            set
            {
                this.EnsureLoaded();
                if (this.requestedTechnicianContactComments != value)
                {
                    this.IsObjectDirty = true;
                    this.requestedTechnicianContactComments = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableDateTime"/> that email notification were sent to OBDFix MasterTechs.
        /// </summary>
        public NullableDateTime MasterTechNotificationsSentDateTimeUTC
        {
            get
            {
                this.EnsureLoaded();
                return this.masterTechNotificationsSentDateTimeUTC;
            }
            set
            {
                this.EnsureLoaded();
                if (this.masterTechNotificationsSentDateTimeUTC != value)
                {
                    this.IsObjectDirty = true;
                    this.masterTechNotificationsSentDateTimeUTC = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableDateTime"/> when the initial fix was provided for this report.
        /// </summary>
        public NullableDateTime FixProvidedDateTimeUTC
        {
            get
            {
                this.EnsureLoaded();
                return this.fixProvidedDateTimeUTC;
            }
            set
            {
                this.EnsureLoaded();
                if (this.fixProvidedDateTimeUTC != value)
                {
                    this.IsObjectDirty = true;
                    this.fixProvidedDateTimeUTC = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableDateTime"/> when the "What Fixed My Car" email reminder was sent.
        /// </summary>
        public NullableDateTime WhatFixedMyCarEmailSentDateTimeUTC
        {
            get
            {
                this.EnsureLoaded();
                return this.whatFixedMyCarEmailSentDateTimeUTC;
            }
            set
            {
                this.EnsureLoaded();
                if (this.whatFixedMyCarEmailSentDateTimeUTC != value)
                {
                    this.IsObjectDirty = true;
                    this.whatFixedMyCarEmailSentDateTimeUTC = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableDateTime"/> when the "No-Fix past due" email notification was sent.
        /// </summary>
        public NullableDateTime PastDueEmailSentDateTimeUTC
        {
            get
            {
                this.EnsureLoaded();
                return this.pastDueEmailSentDateTimeUTC;
            }
            set
            {
                this.EnsureLoaded();
                if (this.pastDueEmailSentDateTimeUTC != value)
                {
                    this.IsObjectDirty = true;
                    this.pastDueEmailSentDateTimeUTC = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> custom report ID as specified by the external system.
        /// </summary>
        public string ExternalSystemReportId
        {
            get
            {
                this.EnsureLoaded();
                return this.externalSystemReportId;
            }
            set
            {
                this.EnsureLoaded();
                if (this.externalSystemReportId != value)
                {
                    this.IsObjectDirty = true;
                    this.externalSystemReportId = value;
                }
            }
        }

        #endregion Object Properties

        #region Object Properties (Related Objects)

        /*****************************************************************************************
		 *
		 * Object Relationships: Add correlated Object Collections
		 *
		*******************************************************************************************/

        /// <summary>
        /// Gets the collection of <see cref="DiagnosticReportFixFeedback"/> objects associated with this report.
        /// </summary>
        public DiagnosticReportFixFeedbackCollection FixFeedbacks
        {
            get
            {
                if (this.fixFeedbacks == null)
                {
                    this.fixFeedbacks = new DiagnosticReportFixFeedbackCollection(Registry);

                    SqlProcedureCommand call = new SqlProcedureCommand();
                    call.ProcedureName = "DiagnosticReportFixFeedback_LoadByDiagnosticReport";
                    call.AddGuid("DiagnosticReportId", Id);

                    this.fixFeedbacks.Load(call, "DiagnosticReportFixFeedbackId", true, true);
                }

                return this.fixFeedbacks;
            }
        }

        /// <summary>
        /// Gets the collection of <see cref="DiagnosticReportFeedback"/> objects associated with this report.
        /// </summary>
        public DiagnosticReportFeedbackCollection DiagnosticReportFeedbacks
        {
            get
            {
                if (this.diagnosticReportFeedbacks == null)
                {
                    this.diagnosticReportFeedbacks = new DiagnosticReportFeedbackCollection(this.Registry);

                    SqlProcedureCommand call = new SqlProcedureCommand();
                    call.ProcedureName = "DiagnosticReportFeedback_LoadByDiagnosticReport";
                    call.AddGuid("DiagnosticReportId", this.Id);

                    this.diagnosticReportFeedbacks.Load(call, "DiagnosticReportFeedbackId", true, true);
                }

                return this.diagnosticReportFeedbacks;
            }
        }

        /// <summary>
        /// Gets the <see cref="UserCollection"/> of Master Techs that are assigned to this report.
        /// NOTE: DO NOT add to this collection directly. Use the AddAssignedMasterTech() method.
        /// </summary>
        [PropertyDefinition("Master Techs Assigned", "Master Techs that are assigned to this report.")]
        public UserCollection MasterTechsAssigned
        {
            get
            {
                if (this.masterTechsAssigned == null)
                {
                    this.EnsureLoaded();

                    this.masterTechsAssigned = new UserCollection(this.Registry);

                    if (!this.isObjectCreated && this.masterTechsAssignedIdList != "")
                    {
                        foreach (string s in this.masterTechsAssignedIdList.Split("|".ToCharArray()))
                        {
                            if (s != null && s != "")
                            {
                                this.masterTechsAssigned.Add((User)this.Registry.CreateInstance(typeof(User), new Guid(s)));
                            }
                        }
                    }
                }
                return this.masterTechsAssigned;
            }
        }

        #endregion Object Properties (Related Objects)

        #region Business Logic Methods

        /***********************************************************************************************
		 *
		 * Custom Business Logic
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Adds a <see cref="User"/> Master Tech to the AssignedMastrTechs property.
        /// </summary>
        /// <param name="masterTech">A <see cref="User"/> Master Tech to add to the list.</param>
        public void AddAssignedMasterTech(User masterTech)
        {
            if (this.MasterTechsAssigned.FindByProperty("Id", masterTech.Id) == null)
            {
                this.MasterTechsAssigned.Add(masterTech);
                this.hasMasterTechsAssigned = true;
                this.isMasterTechsAssignedDirty = true;
                this.IsObjectDirty = true;
            }
        }

        #region Error Codes

        /// <summary>
        /// Gets a collection of error codes of the specified system type.
        /// </summary>
        /// <param name="systemType">The <see cref="DiagnosticReportErrorCodeSystemType"/> system type to look for.</param>
        /// <returns>A <see cref="StringCollection"/> of error codes.</returns>
        public StringCollection GetAllErrorCodes(DiagnosticReportErrorCodeSystemType systemType)
        {
            StringCollection codes = new StringCollection();
            string milDtc = "";
            StringCollection storedCodes = this.GetStoredErrorCodes(systemType);
            StringCollection pendingCodes = this.GetPendingErrorCodes(systemType);

            if (systemType == DiagnosticReportErrorCodeSystemType.PowertrainObd2)
            {
                milDtc = this.PwrMilCode;
            }

            if (milDtc != "")
            {
                codes.Add(milDtc);
            }

            foreach (string code in storedCodes)
            {
                if (!string.IsNullOrEmpty(code) && !codes.Contains(code))
                {
                    codes.Add(code);
                }
            }
            foreach (string code in pendingCodes)
            {
                if (!string.IsNullOrEmpty(code) && !codes.Contains(code))
                {
                    codes.Add(code);
                }
            }

            return codes;
        }

        /// <summary>
        /// Gets a collection of pending error codes of the specified system type.
        /// </summary>
        /// <param name="systemType">The <see cref="DiagnosticReportErrorCodeSystemType"/> system type to look for.</param>
        /// <returns>A <see cref="StringCollection"/> of pending error codes.</returns>
        public StringCollection GetPendingErrorCodes(DiagnosticReportErrorCodeSystemType systemType)
        {
            StringCollection pendingCodes = new StringCollection();

            switch (systemType)
            {
                case DiagnosticReportErrorCodeSystemType.ABS:
                    pendingCodes = this.AbsPendingCodes.AllDtcs;
                    break;

                case DiagnosticReportErrorCodeSystemType.PowertrainOBD1:
                    pendingCodes = this.Obd1PendingCodes.AllDtcs;
                    break;

                case DiagnosticReportErrorCodeSystemType.PowertrainObd2:
                    pendingCodes = this.PwrPendingCodes;
                    break;

                case DiagnosticReportErrorCodeSystemType.SRS:
                    pendingCodes = this.SrsPendingCodes.AllDtcs;
                    break;
            }

            return pendingCodes;
        }

        /// <summary>
        /// Gets a collection of stored error codes of the specified system type.
        /// </summary>
        /// <param name="systemType">The <see cref="DiagnosticReportErrorCodeSystemType"/> system type to look for.</param>
        /// <returns>A <see cref="StringCollection"/> of stored error codes.</returns>
        public StringCollection GetStoredErrorCodes(DiagnosticReportErrorCodeSystemType systemType)
        {
            StringCollection storedCodes = new StringCollection();

            switch (systemType)
            {
                case DiagnosticReportErrorCodeSystemType.ABS:
                    storedCodes = this.AbsStoredCodes.AllDtcs;
                    break;

                case DiagnosticReportErrorCodeSystemType.PowertrainOBD1:
                    storedCodes = this.Obd1StoredCodes.AllDtcs;
                    break;

                case DiagnosticReportErrorCodeSystemType.PowertrainObd2:
                    storedCodes = this.PwrStoredCodes;
                    break;

                case DiagnosticReportErrorCodeSystemType.SRS:
                    storedCodes = this.SrsStoredCodes.AllDtcs;
                    break;
            }

            return storedCodes;
        }

        /// <summary>
        /// Sets the OBD1 stored codes from the provided comma delimited value.
        /// </summary>
        /// <param name="commaDelimitedCodes">A <see cref="string"/> of comma separated code values.</param>
        public void Obd1StoredCodesSet(string commaDelimitedCodes)
        {
            this.obd1StoredCodesString = this.GetCodesStringFromCommaDelimitedString(commaDelimitedCodes);
            this.obd1StoredCodes = null;
        }

        /// <summary>
        /// Sets the OBD1 pending codes from the provided comma delimited value.
        /// </summary>
        /// <param name="commaDelimitedCodes">A <see cref="string"/> of comma separated code values.</param>
        public void Obd1PendingCodesSet(string commaDelimitedCodes)
        {
            this.obd1PendingCodesString = this.GetCodesStringFromCommaDelimitedString(commaDelimitedCodes);
            this.obd1PendingCodes = null;
        }

        /// <summary>
        /// Sets the ABS stored codes from the provided comma delimited value.
        /// </summary>
        /// <param name="commaDelimitedCodes">A <see cref="string"/> of comma separated code values.</param>
        public void AbsStoredCodesSet(string commaDelimitedCodes)
        {
            this.absStoredCodesString = this.GetCodesStringFromCommaDelimitedString(commaDelimitedCodes);
            this.absStoredCodes = null;
        }

        /// <summary>
        /// Sets the ABS pending codes from the provided comma delimited value.
        /// </summary>
        /// <param name="commaDelimitedCodes">A <see cref="string"/> of comma separated code values.</param>
        public void AbsPendingCodesSet(string commaDelimitedCodes)
        {
            this.absPendingCodesString = this.GetCodesStringFromCommaDelimitedString(commaDelimitedCodes);
            this.absPendingCodes = null;
        }

        /// <summary>
        /// Sets the SRS stored codes from the provided comma delimited value.
        /// </summary>
        /// <param name="commaDelimitedCodes">A <see cref="string"/> of comma separated code values.</param>
        public void SrsStoredCodesSet(string commaDelimitedCodes)
        {
            this.srsStoredCodesString = this.GetCodesStringFromCommaDelimitedString(commaDelimitedCodes);
            this.srsStoredCodes = null;
        }

        /// <summary>
        /// Sets the SRS pending codes from the provided comma delimited value.
        /// </summary>
        /// <param name="commaDelimitedCodes">A <see cref="string"/> of comma separated code values.</param>
        public void SrsPendingCodesSet(string commaDelimitedCodes)
        {
            this.srsPendingCodesString = this.GetCodesStringFromCommaDelimitedString(commaDelimitedCodes);
            this.srsPendingCodes = null;
        }

        private string GetCodesStringFromCommaDelimitedString(string commaDelimitedCodes)
        {
            string stringValue = "";
            if (!String.IsNullOrEmpty(commaDelimitedCodes))
            {
                commaDelimitedCodes = commaDelimitedCodes.Replace(" ", "");
                stringValue = commaDelimitedCodes.Replace(",", ",Manual|") + ",Manual";
            }

            return stringValue;
        }

        #endregion Error Codes

        /// <summary>
        /// Determines if the report needs to be refreshed based on a missing value in the desired languge as set in the RuntimeInfo.
        /// </summary>
        /// <returns>A <see cref="Boolean"/> indicating if the report needs to be refreshed.</returns>
        public bool IsLanguageRefreshRequired()
        {
            bool refreshRequired = false;

            foreach (DiagnosticReportResultErrorCode ec in this.DiagnosticReportResult.DiagnosticReportResultErrorCodes)
            {
                DiagnosticReportResultErrorCodeDefinitionDisplayCollection defs = ec.GetDiagnosticReportResultErrorCodeDefinitions(DiagnosticReportResultType.CarScan);

                foreach (DiagnosticReportResultErrorCodeDefinitionDisplay ecDef in defs)
                {
                    refreshRequired = !this.RuntimeInfo.DoesTranslatedValueExist(ecDef.Title, ecDef.Title_es, ecDef.Title_fr, ecDef.Title_zh);
                    if (refreshRequired)
                    {
                        break;
                    }

                    refreshRequired = !this.RuntimeInfo.DoesTranslatedValueExist(ecDef.Conditions, ecDef.Conditions_es, ecDef.Conditions_fr, ecDef.Conditions_zh);
                    if (refreshRequired)
                    {
                        break;
                    }

                    refreshRequired = !this.RuntimeInfo.DoesTranslatedValueExist(ecDef.PossibleCauses, ecDef.PossibleCauses_es, ecDef.PossibleCauses_fr, ecDef.PossibleCauses_zh);
                    if (refreshRequired)
                    {
                        break;
                    }
                }

                if (refreshRequired)
                {
                    break;
                }
            }

            if (!refreshRequired)
            {
                foreach (DiagnosticReportResultFix fix in this.DiagnosticReportResult.DiagnosticReportResultFixes)
                {
                    refreshRequired = !this.RuntimeInfo.DoesTranslatedValueExist(fix.Description, fix.Description_es, fix.Description_fr, fix.Description_zh);
                    if (refreshRequired)
                    {
                        break;
                    }

                    refreshRequired = !this.RuntimeInfo.DoesTranslatedValueExist(fix.Name, fix.Name_es, fix.Name_fr, fix.Name_zh);
                    if (refreshRequired)
                    {
                        break;
                    }

                    foreach (DiagnosticReportResultFixPart part in fix.DiagnosticReportResultFixParts)
                    {
                        refreshRequired = !this.RuntimeInfo.DoesTranslatedValueExist(part.Description, part.Description_es, part.Description_fr, part.Description_zh);
                        if (refreshRequired)
                        {
                            break;
                        }

                        refreshRequired = !this.RuntimeInfo.DoesTranslatedValueExist(part.Name, part.Name_es, part.Name_fr, part.Name_zh);
                        if (refreshRequired)
                        {
                            break;
                        }
                    }
                }
            }

            return refreshRequired;
        }

        /// <summary>
        /// Gets a bool that indicates if the report already exists.
        /// </summary>
        /// <param name="registry">The <see cref="Registry"/> currently in use.</param>
        /// <param name="externalSystem">The <see cref="ExternalSystem"/> in which the report was created.</param>
        /// <param name="vin">The <see cref="string"/> VIN of the vehicle for which the report was created.</param>
        /// <param name="payload">The <see cref="string"/> tool payload that was used to create the report.</param>
        /// <param name="externalSystemReportID">The <see cref="string"/> custom report ID that was assigned by the external system.</param>
        /// <returns>A <see cref="bool"/> that indicates if the report already exists.</returns>
        public static bool IsDuplicate(Registry registry, ExternalSystem externalSystem, string vin, string payload, string externalSystemReportID)
        {
            bool isDuplicate = false;

            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(registry.ConnectionStringDefault))
            {
                dr.CommandTimeout = 360; //Added on 2017-05-19 8:37 AM by INNOVA Dev Team to increase the db connection timeout to 360 seconds (6 minutes)
                dr.ProcedureName = "DiagnosticReport_GetExists";
                dr.AddGuid("ExternalSystemId", externalSystem.Id);
                dr.AddNVarChar("VIN", vin);
                dr.AddNVarChar("RawUploadString", payload);
                dr.AddNVarChar("ExternalSystemReportID", externalSystemReportID);

                dr.Execute();

                if (dr.Read())
                {
                    isDuplicate = dr.GetBoolean("Exists");
                }
            }

            return isDuplicate;
        }

        #region Reporting and Statistics

        #region Tool Information Decoding and Property Setting

        /// <summary>
        /// Sets the raw upload string, tool information object and properties from the object.
        /// </summary>
        /// <param name="rawUploadString"><see cref="string"/> raw upload string.</param>
        public void SetPropertiesAndToolInformationFromRawUploadString(string rawUploadString)
        {
            ToolInformation toolInformation = null;
            VehicleDataEx vehicleDataEx = null;

            (toolInformation, vehicleDataEx) = InnovaRawPayloadParser.ParseToolInformationAndVehicleData(rawUploadString, RuntimeInfo.CurrentLanguage);

            if (toolInformation == null)
            {
                throw new Exception("Can't parse Tool Information from raw payload");
            }

            if (vehicleDataEx == null)
            {
                throw new Exception("Can't parse Vehicle Data from raw payload");
            }

            this.rawUploadString = rawUploadString;
            this.SetPropertiesFromToolInformation(toolInformation, vehicleDataEx);
        }

        /// <summary>
        /// Sets the properties to the diagnostic report from the tool information object.
        /// </summary>
        /// <param name="toolInformation"></param>
        /// <param name="vehicleDataEx"></param>
        private void SetPropertiesFromToolInformation(ToolInformation toolInformation, VehicleDataEx vehicleDataEx)
        {
            this.ToolInformation = toolInformation;
            this.VehicleDataLibEx = vehicleDataEx;

            this.ToolTypeFormat = toolInformation.ToolTypeFormat;
            this.SoftwareType = toolInformation.SoftwareType;
            this.SILStatus = InnovaRawPayloadParser.SILStatus(vehicleDataEx);

            if (toolInformation.SoftwareVersion != null)
            {
                this.SoftwareVersion = toolInformation.SoftwareVersion.ToString();
            }
            if (toolInformation.FirmwareVersion != null)
            {
                this.FirmwareVersion = toolInformation.FirmwareVersion.ToString();
            }

            this.VIN = toolInformation.Vin;

            foreach (Obd1Dtc obd1 in toolInformation.AllObd1Codes)
            {
                //#IgnoreNOCODE
                if (string.Equals("No code", obd1.Value, StringComparison.CurrentCultureIgnoreCase))
                {
                    continue;
                }

                DtcCodeView dcv = (DtcCodeView)this.Obd1StoredCodes.FindByProperty("CodeType", obd1.Description);
                bool isNew = false;

                if (dcv == null)
                {
                    dcv = new DtcCodeView(obd1.Description);
                    isNew = true;
                }

                if (!dcv.Codes.Contains(obd1.Value))
                {
                    dcv.Codes.Add(obd1.Value);
                }

                if (isNew)
                {
                    this.Obd1StoredCodes.Add(dcv);
                }
            }

            foreach (Abs abs in toolInformation.PendingAbsCodes)
            {
                //#IgnoreNOCODE
                if (string.Equals("No code", abs.Value, StringComparison.CurrentCultureIgnoreCase))
                {
                    continue;
                }

                DtcCodeView dcv = (DtcCodeView)this.AbsPendingCodes.FindByProperty("CodeType", abs.Description);
                bool isNew = false;

                if (dcv == null)
                {
                    dcv = new DtcCodeView(abs.Description);
                    isNew = true;
                }

                if (!dcv.Codes.Contains(abs.Value))
                {
                    dcv.Codes.Add(abs.Value);
                }

                if (isNew)
                {
                    this.AbsPendingCodes.Add(dcv);
                }
            }

            foreach (Abs abs in toolInformation.StoredAbsCodes)
            {
                //#IgnoreNOCODE
                if (string.Equals("No code", abs.Value, StringComparison.CurrentCultureIgnoreCase))
                {
                    continue;
                }
                DtcCodeView dcv = (DtcCodeView)this.AbsStoredCodes.FindByProperty("CodeType", abs.Description);
                bool isNew = false;

                if (dcv == null)
                {
                    dcv = new DtcCodeView(abs.Description);
                    isNew = true;
                }

                if (!dcv.Codes.Contains(abs.Value))
                {
                    dcv.Codes.Add(abs.Value);
                }

                if (isNew)
                {
                    this.AbsStoredCodes.Add(dcv);
                }
            }

            //Nam added 4/12/2017: add AllABSs to AbsStoredCodes
            if (toolInformation.AllAbss != null)
            {
                foreach (Abs abs in toolInformation.AllAbss)
                {
                    //#IgnoreNOCODE
                    if (string.Equals("No code", abs.Value, StringComparison.CurrentCultureIgnoreCase))
                    {
                        continue;
                    }
                    DtcCodeView dcv = (DtcCodeView)this.AbsStoredCodes.FindByProperty("CodeType", string.Empty);
                    bool isNew = false;

                    if (dcv == null)
                    {
                        dcv = new DtcCodeView(string.Empty);
                        isNew = true;
                    }

                    if (!dcv.Codes.Contains(abs.Value))
                    {
                        dcv.Codes.Add(abs.Value);
                    }

                    if (isNew)
                    {
                        this.AbsStoredCodes.Add(dcv);
                    }
                }
            }
            ////Nam added 4/12/2017: add AllABSs to AbsStoredCodes

            this.enhancedDtcs = DiagnosticReport.GetEnhancedDtcsFromToolInformation(toolInformation);

            foreach (Srs srs in toolInformation.StoredSrsCodes)
            {
                //#IgnoreNOCODE
                if (string.Equals("No code", srs.Value, StringComparison.CurrentCultureIgnoreCase))
                {
                    continue;
                }
                DtcCodeView dcv = (DtcCodeView)this.SrsStoredCodes.FindByProperty("CodeType", srs.Description);
                bool isNew = false;

                if (dcv == null)
                {
                    dcv = new DtcCodeView(srs.Description);
                    isNew = true;
                }

                if (!dcv.Codes.Contains(srs.Value))
                {
                    dcv.Codes.Add(srs.Value);
                }

                if (isNew)
                {
                    this.SrsStoredCodes.Add(dcv);
                }
            }

            //Nam added 4/12/2017: add AllSRSs to SrsStoredCodes
            if (toolInformation.AllSrss != null)
            {
                foreach (Srs srs in toolInformation.AllSrss)
                {
                    //#IgnoreNOCODE
                    if (string.Equals("No code", srs.Value, StringComparison.CurrentCultureIgnoreCase))
                    {
                        continue;
                    }
                    DtcCodeView dcv = (DtcCodeView)this.SrsStoredCodes.FindByProperty("CodeType", string.Empty);
                    bool isNew = false;

                    if (dcv == null)
                    {
                        dcv = new DtcCodeView(string.Empty);
                        isNew = true;
                    }

                    if (!dcv.Codes.Contains(srs.Value))
                    {
                        dcv.Codes.Add(srs.Value);
                    }

                    if (isNew)
                    {
                        this.SrsStoredCodes.Add(dcv);
                    }
                }
            }
            ////Nam added: add AllABSs to AbsStoredCodes - 4/12/2017

            foreach (Srs srs in toolInformation.PendingSrsCodes)
            {
                //#IgnoreNOCODE
                if (string.Equals("No code", srs.Value, StringComparison.CurrentCultureIgnoreCase))
                {
                    continue;
                }
                DtcCodeView dcv = (DtcCodeView)this.SrsPendingCodes.FindByProperty("CodeType", srs.Description);
                bool isNew = false;

                if (dcv == null)
                {
                    dcv = new DtcCodeView(srs.Description);
                    isNew = true;
                }

                if (!dcv.Codes.Contains(srs.Value))
                {
                    dcv.Codes.Add(srs.Value);
                }

                if (isNew)
                {
                    this.SrsPendingCodes.Add(dcv);
                }
            }

            foreach (PowerTrain pt in toolInformation.PendingPowerTrains)
            {
                //#IgnoreNOCODE
                if (string.Equals("No code", pt.DTC, StringComparison.CurrentCultureIgnoreCase))
                {
                    continue;
                }
                this.PwrPendingCodes.Add(pt.DTC);
            }

            foreach (PowerTrain pt in toolInformation.StoredPowerTrains)
            {
                //#IgnoreNOCODE
                if (string.Equals("No code", pt.DTC, StringComparison.CurrentCultureIgnoreCase))
                {
                    continue;
                }
                this.PwrStoredCodes.Add(pt.DTC);
            }

            foreach (PowerTrain pt in toolInformation.PermanentPowerTrains)
            {
                //#IgnoreNOCODE
                if (string.Equals("No code", pt.DTC, StringComparison.CurrentCultureIgnoreCase))
                {
                    continue;
                }
                //Nam updated 4/15/2017 - Assign to wrong property
                this.PwrPermanentCodes.Add(pt.DTC);
            }

            //#IgnoreNOCODE
            if (!string.Equals("No code", toolInformation.PrimaryDtc, StringComparison.CurrentCultureIgnoreCase))
            {
                this.PwrMilCode = toolInformation.PrimaryDtc;
            }

            this.OemDtcInfo = InnovaRawPayloadParser.GetOemDtcInfo(vehicleDataEx);
            this.ToolLEDStatus = InnovaRawPayloadParser.FWBLLedStatus(vehicleDataEx);
            this.ToolMilStatus = toolInformation.ToolMilStatus;
        }

        /// <summary>
        /// Gets the <see cref="DtcCodeViewCollection"/> of <see cref="DtcCodeView"/> objects processed from the EnhancedDtcs on the Tool Information class.
        /// </summary>
        /// <param name="toolInformation"><see cref="ToolInformation"/> object to retrieve the values from</param>
        /// <returns><see cref="DtcCodeViewCollection"/> of <see cref="DtcCodeView"/> objects with enhanced Dtcs.</returns>
        public static DtcCodeViewCollection GetEnhancedDtcsFromToolInformation(ToolInformation toolInformation)
        {
            DtcCodeViewCollection enhancedDtcs = new DtcCodeViewCollection();
            if (toolInformation != null && toolInformation.EnhanceDtcs != null)
            {
                foreach (object c in toolInformation.EnhanceDtcs)
                {
                    Hashtable keyvaluepair = (Hashtable)c;

                    foreach (DictionaryEntry de in keyvaluepair)
                    {
                        EnhancedCollection ec = de.Value as EnhancedCollection;
                        DtcCodeView dcv = new DtcCodeView(ec.Description);
                        enhancedDtcs.Add(dcv);

                        foreach (Enhanced e in ec)
                        {
                            dcv.Codes.Add(e.Value);
                        }
                    }
                }
            }
            return enhancedDtcs;
        }

        #endregion Tool Information Decoding and Property Setting

        #endregion Reporting and Statistics

        #region Fixes

        /// <summary>
        /// Gets the <see cref="FixCollection"/> of fixes that match the diagnostic upload data for this car.
        /// </summary>
        /// <param name="primaryErrorCode">The <see cref="string"/> primary DTC.</param>
        /// <param name="secondaryDtcs">The <see cref="StringCollection"/> of secondary DTCs.</param>
        /// <param name="logDiscrepancies">A <see cref="bool"/> that indicates if discrepancies should be logged.</param>
        /// <param name="onlyProcessFixes">A <see cref="bool"/> that indicates if only logic necessary for looking up fixes should be executed. Everything else should be skipped.</param>
        /// <param name="diagnosticReportErrorCodeSystemType">The <see cref="DiagnosticReportErrorCodeSystemType"/> type of system the codes are for.</param>
        /// <returns>A <see cref="FixCollection"/> of <see cref="Fix"/> objects.</returns>
        private FixCollection GetFixesSorted(string primaryErrorCode, StringCollection secondaryDtcs, bool logDiscrepancies, bool onlyProcessFixes, DiagnosticReportErrorCodeSystemType diagnosticReportErrorCodeSystemType)
        {
            //#Sprint23
            primaryErrorCode = primaryErrorCode.Split('-')[0].Trim();
            secondaryDtcs = GetFirstPartErrorCodes(secondaryDtcs);
            //#Sprint23

            FixCollection fixesPolk = new FixCollection(this.Registry);
            SqlProcedureCommand call = new SqlProcedureCommand();
            if (this.Vehicle.PolkVehicleYMME != null)
            {
                call.ProcedureName = "Fix_LoadByDiagnosticReport";

                call.AddNVarChar("PrimaryCode", primaryErrorCode);
                call.AddInt32("Year", this.Vehicle.PolkVehicleYMME.Year);
                call.AddNVarChar("Make", this.Vehicle.PolkVehicleYMME.Make);
                call.AddNVarChar("Model", this.Vehicle.PolkVehicleYMME.Model);
                call.AddNVarChar("TrimLevel", this.Vehicle.PolkVehicleYMME.Trim);
                call.AddNVarChar("Transmission", this.Vehicle.PolkVehicleYMME.Transmission);
                call.AddNVarChar("EngineVINCode", this.Vehicle.PolkVehicleYMME.EngineVinCode);
                call.AddNVarChar("EngineType", this.Vehicle.PolkVehicleYMME.EngineType);

                call.AddInt32("Market", (int)this.RuntimeInfo.CurrentMarket);

                call.AddNVarChar("ManufacturerName", this.Vehicle.PolkVehicleYMME.Manufacturer);
                call.AddNVarChar("PolkVehicleYmmeCountryId", this.vehicle.PolkVehicleYMME.CountryID);

                var diagnosticReportId = this.Id;
                if (diagnosticReportId != Guid.Empty)
                {
                    call.AddGuid("DiagnosticReportId", diagnosticReportId);
                }

                fixesPolk.Load(call, "FixId", true, true);
                fixesPolk.SetIsFromPolkMatch(true);
            }

            FixCollection fixesVinPower = new FixCollection(this.Registry);

            call = new SqlProcedureCommand
            {
                ProcedureName = "Fix_LoadByDiagnosticReportUsingVinPower"
            };
            call.AddInt32("Year", this.Vehicle.VPYear);
            call.AddNVarChar("Make", this.Vehicle.VPMake);
            call.AddNVarChar("Model", this.Vehicle.VPModel);
            call.AddNVarChar("TrimLevel", this.Vehicle.VPTrimLevel);
            call.AddNVarChar("Transmission", this.Vehicle.TransmissionControlType);
            call.AddNVarChar("PrimaryCode", primaryErrorCode);
            call.AddNVarChar("EngineVINCode", this.Vehicle.VPEngineVINCode);
            call.AddNVarChar("EngineType", this.Vehicle.VPEngineType);
            call.AddInt32("Market", (int)this.RuntimeInfo.CurrentMarket);

            fixesVinPower.Load(call, "FixId", true, true);
            fixesVinPower.SetIsFromVinPowerMatch(true);

            FixCollection fixes = new FixCollection(this.Registry);

            foreach (Fix f in fixesVinPower)
            {
                Fix polkFix = (Fix)fixesPolk.FindByProperty("Id", f.Id);

                if (polkFix == null)
                {
                    if (logDiscrepancies && this.Vehicle.PolkVehicleYMME != null)
                    {
                        FixPolkVehicleDiscrepancy.AddDiscrepancy(this.Registry, f, this.Vehicle.PolkVehicleYMME, true, false);
                    }
                }
                else
                {
                    f.IsFromPolkMatch = true;
                }

                fixes.Add(f);
            }

            fixes.ClearLookupTables();

            foreach (Fix f in fixesPolk)
            {
                Fix vinPowerFix = (Fix)fixesVinPower.FindByProperty("Id", f.Id);

                if (vinPowerFix == null)
                {
                    if (logDiscrepancies && this.Vehicle.PolkVehicleYMME != null)
                    {
                        FixPolkVehicleDiscrepancy.AddDiscrepancy(this.Registry, f, this.Vehicle.PolkVehicleYMME, false, true);
                    }
                }
                else
                {
                    f.IsFromVinPowerMatch = true;
                }

                if (fixes.FindByProperty("Id", f.Id) == null)
                {
                    fixes.Add(f);
                    fixes.ClearLookupTables();
                }
            }

            if (!onlyProcessFixes)
            {
                //load the relations
                this.SortFixes(fixes, primaryErrorCode, secondaryDtcs, diagnosticReportErrorCodeSystemType);
            }

            return fixes;
        }

        /// <summary>
        /// Sorts the fixes according to the rules for finding the fixes for the diagnostic report.
        /// </summary>
        /// <param name="fixesToSort"><see cref="FixCollection"/> to sort.</param>
        /// <param name="primaryDTC">The <see cref="String"/> primary DTC.</param>
        /// <param name="secondaryDtcs">The <see cref="StringCollection"/> of secondary DTCs.</param>
        /// <param name="diagnosticReportErrorCodeSystemType">The <see cref="DiagnosticReportErrorCodeSystemType"/> to sort on.</param>
        private void SortFixes(FixCollection fixesToSort, string primaryDTC, StringCollection secondaryDtcs, DiagnosticReportErrorCodeSystemType diagnosticReportErrorCodeSystemType)
        {
            //foreach fix calculate the total cost
            foreach (Fix f in fixesToSort)
            {
                if (PerformFreezeFrameSorting)
                {
                    f.FreezeFrameMatches = f.FixName.GetFreezeFrameMatchCount(this);
                }

                FixDTC fixDTC = f.GetFixDTCMatchingPrimaryDTC(primaryDTC);

                //shouldn't be null, but if it is, we don't want to blow up
                if (fixDTC != null)
                {
                    StringCollection secondaryErrorCodesList = fixDTC.GetSecondaryDTCListAsStringCollection();

                    if (secondaryErrorCodesList.Count > 0)
                    {
                        if (secondaryDtcs.Count > 0)
                        {
                            bool isExactMatch = false;
                            int matchCount = 0;
                            //if the errors are the same we need to compare them all
                            foreach (string errorCode in secondaryDtcs)
                            {
                                if (secondaryErrorCodesList.Contains(errorCode))
                                {
                                    matchCount++;
                                }
                            }
                            //if the match count equals the upload count and the fix error count we have an exact match
                            if (matchCount == secondaryDtcs.Count
                            && matchCount == secondaryErrorCodesList.Count)
                            {
                                isExactMatch = true;
                            }

                            //set the exact match flag
                            f.SetDiagnosticReportIsExactMatch(isExactMatch);
                            //set the number of matches
                            f.SetDiagnosticReportSecondaryCodeAssignmentMatches(matchCount);
                        }
                        else
                        {
                            //the fix has a count but the upload does not
                            f.SetDiagnosticReportIsExactMatch(false);
                            //count will be negative since we have fewer than 0 matches in this case
                            f.SetDiagnosticReportSecondaryCodeAssignmentMatches(0 - secondaryErrorCodesList.Count);
                        }
                    }
                    else//the fix does not have any extra codes
                    {
                        if (secondaryDtcs.Count > 0)
                        {
                            f.SetDiagnosticReportIsExactMatch(false);
                            f.SetDiagnosticReportSecondaryCodeAssignmentMatches(0 - secondaryDtcs.Count);
                        }
                        else
                        {
                            //both are zero
                            f.SetDiagnosticReportIsExactMatch(true);
                            f.SetDiagnosticReportSecondaryCodeAssignmentMatches(0);
                        }
                    }
                }
            }

            //finally sort the fixes
            SortDictionaryCollection sorts = new SortDictionaryCollection();

            sorts = new SortDictionaryCollection();
            sorts.Add(new SortDictionary("DiagnosticReportIsExactMatch", SortDirection.Descending));
            sorts.Add(new SortDictionary("DiagnosticReportSecondaryCodeAssignmentMatches", SortDirection.Descending));
            sorts.Add(new SortDictionary("FrequencyCount", SortDirection.Descending));
            sorts.Add(new SortDictionary("TotalCost", SortDirection.Descending));
            sorts.Add(new SortDictionary("Name"));
            sorts.Add(new SortDictionary("CreatedDateTimeUTC", SortDirection.Descending));

            fixesToSort.Sort(sorts);

            if (DiagnosticReport.PerformFreezeFrameSorting)
            {
                this.SaveFixSortPriority(fixesToSort, "Normal" + DiagnosticReport.ReportType, diagnosticReportErrorCodeSystemType);

                sorts = new SortDictionaryCollection();
                sorts.Add(new SortDictionary("FreezeFrameMatches", SortDirection.Descending));
                sorts.Add(new SortDictionary("DiagnosticReportIsExactMatch", SortDirection.Descending));
                sorts.Add(new SortDictionary("DiagnosticReportSecondaryCodeAssignmentMatches", SortDirection.Descending));
                sorts.Add(new SortDictionary("FrequencyCount", SortDirection.Descending));
                sorts.Add(new SortDictionary("TotalCost", SortDirection.Descending));
                sorts.Add(new SortDictionary("Name"));
                sorts.Add(new SortDictionary("CreatedDateTimeUTC", SortDirection.Descending));

                fixesToSort.Sort(sorts);
                this.SaveFixSortPriority(fixesToSort, "FreezeFrameFirst" + DiagnosticReport.ReportType, diagnosticReportErrorCodeSystemType);

                sorts = new SortDictionaryCollection();
                sorts.Add(new SortDictionary("DiagnosticReportIsExactMatch", SortDirection.Descending));
                sorts.Add(new SortDictionary("Name"));
                sorts.Add(new SortDictionary("CreatedDateTimeUTC", SortDirection.Descending));

                fixesToSort.Sort(sorts);
                this.SaveFixSortPriority(fixesToSort, "PrimaryDTCOnly" + DiagnosticReport.ReportType, diagnosticReportErrorCodeSystemType);
            }
        }

        /// <summary>
        /// Gets the fix status for the system type provided
        /// </summary>
        /// <param name="diagnosticReportErrorCodeSystemType"></param>
        /// <returns></returns>
        public DiagnosticReportFixStatus GetFixStatus(DiagnosticReportErrorCodeSystemType diagnosticReportErrorCodeSystemType)
        {
            DiagnosticReportFixStatus fixStatus = DiagnosticReportFixStatus.FixFound;

            switch (diagnosticReportErrorCodeSystemType)
            {
                case DiagnosticReportErrorCodeSystemType.ABS:
                    fixStatus = this.AbsDiagnosticReportFixStatus;
                    break;

                case DiagnosticReportErrorCodeSystemType.PowertrainOBD1:
                    fixStatus = this.Obd1DiagnosticReportFixStatus;
                    break;

                case DiagnosticReportErrorCodeSystemType.PowertrainObd2:
                    fixStatus = this.PwrDiagnosticReportFixStatus;
                    break;

                case DiagnosticReportErrorCodeSystemType.SRS:
                    fixStatus = this.SrsDiagnosticReportFixStatus;
                    break;
            }

            return fixStatus;
        }

        #endregion Fixes

        #region Create Diagnostic Report Result

        /// <summary>
        /// Creates (or updates if one exists) a <see cref="DiagnosticReportResult"/> for this report.
        /// </summary>
        public void CreateDiagnosticReportResult()
        {
            if (this.SymptomGuids.Any())
            {
                this.CreateDiagnosticReportResultFixForSymptoms();
                return;
            }

            this.CreateDiagnosticReportResult(false, false);
        }

        /// <summary>
        /// Creates (or updates if one exists) a <see cref="DiagnosticReportResult"/> for this report.
        /// </summary>
        /// <param name="logDiscrepancies">A <see cref="bool"/> that indicates if discrepancies should be logged.</param>
        /// <param name="onlyProcessFixes">A <see cref="bool"/> that indicates if only logic necessary for looking up fixes should be executed. Everything else should be skipped.</param>
        public void CreateDiagnosticReportResult(bool logDiscrepancies, bool onlyProcessFixes)
        {
            if (this.SymptomGuids.Any())
            {
                this.CreateDiagnosticReportResultFixForSymptoms();
                return;
            }

            bool fixFound = false;

            if (this.PwrDiagnosticReportFixStatus != DiagnosticReportFixStatus.FixNotFoundLookupCanceled)
            {
                if (!string.IsNullOrEmpty(this.PwrMilCode))
                {
                    fixFound = this.CreateDiagnosticReportResult(this.pwrMilCode, DiagnosticReportErrorCodeSystemType.PowertrainObd2, true, logDiscrepancies, onlyProcessFixes);

                    if (!this.pwrFixFoundAfterLastFixLookup)
                    {
                        this.pwrFixFoundAfterLastFixLookup = fixFound;
                    }
                }
                else
                {
                    if (!onlyProcessFixes)
                    {
                        this.CreateDiagnosticReportResultErrorCodes(DiagnosticReportErrorCodeSystemType.PowertrainObd2);
                    }
                }
                this.SetFixStatuses(DiagnosticReportErrorCodeSystemType.PowertrainObd2);
            }

            if (this.Obd1DiagnosticReportFixStatus != DiagnosticReportFixStatus.FixNotFoundLookupCanceled)
            {
                if (this.Obd1StoredCodes.Count > 0)
                {
                    foreach (string code in this.Obd1StoredCodes.AllDtcs)
                    {
                        fixFound = this.CreateDiagnosticReportResult(code, DiagnosticReportErrorCodeSystemType.PowertrainOBD1, true, logDiscrepancies, onlyProcessFixes);

                        if (!this.obd1FixFoundAfterLastFixLookup)
                        {
                            this.obd1FixFoundAfterLastFixLookup = fixFound;
                        }
                    }
                    this.SetFixStatuses(DiagnosticReportErrorCodeSystemType.PowertrainOBD1);
                }
                else
                {
                    if (!onlyProcessFixes)
                    {
                        this.CreateDiagnosticReportResultErrorCodes(DiagnosticReportErrorCodeSystemType.PowertrainOBD1);
                    }
                }
            }

            if (this.AbsDiagnosticReportFixStatus != DiagnosticReportFixStatus.FixNotFoundLookupCanceled)
            {
                if (this.AbsStoredCodes.Count > 0)
                {
                    //#BCUCodes
                    List<string> allDtcs = new List<string>();
                    foreach (string code in this.AbsStoredCodes.AllDtcs)
                    {
                        allDtcs.Add(code);
                    }

                    fixFound = this.CreateDiagnosticReportResult(allDtcs,
                        DiagnosticReportErrorCodeSystemType.ABS, true, logDiscrepancies, onlyProcessFixes);

                    //#LEDMILUpdate
                    if (this.AbsDiagnosticReportFixStatus == DiagnosticReportFixStatus.FixNotNeeded)
                    {
                        this.SetFixStatuses(DiagnosticReportErrorCodeSystemType.ABS, true);
                    }
                    else
                    {
                        this.SetFixStatuses(DiagnosticReportErrorCodeSystemType.ABS);
                    }
                }
                else
                {
                    if (!onlyProcessFixes)
                    {
                        this.CreateDiagnosticReportResultErrorCodes(DiagnosticReportErrorCodeSystemType.ABS);
                    }
                }
            }

            if (this.SrsDiagnosticReportFixStatus != DiagnosticReportFixStatus.FixNotFoundLookupCanceled)
            {
                if (this.SrsStoredCodes.Count > 0)
                {
                    //#BCUCodes
                    List<string> allDtcs = new List<string>();
                    foreach (string code in this.SrsStoredCodes.AllDtcs)
                    {
                        allDtcs.Add(code);
                    }

                    fixFound = this.CreateDiagnosticReportResult(allDtcs,
                        DiagnosticReportErrorCodeSystemType.SRS, true, logDiscrepancies, onlyProcessFixes);

                    this.SetFixStatuses(DiagnosticReportErrorCodeSystemType.SRS);
                }
                else
                {
                    if (!onlyProcessFixes)
                    {
                        this.CreateDiagnosticReportResultErrorCodes(DiagnosticReportErrorCodeSystemType.SRS);
                    }
                }
            }

            if (!onlyProcessFixes)
            {
                this.CreateDiagnosticReportResultErrorCodes(DiagnosticReportErrorCodeSystemType.Enhanced);
            }

            if (fixFound && this.FixProvidedDateTimeUTC.IsNull)
            {
                this.FixProvidedDateTimeUTC = DateTime.UtcNow;
            }
        }

        /// <summary>
        /// CreateDiagnosticReportResult
        /// </summary>
        /// <param name="diagnosticReportErrorCodeSystemType"></param>
        /// <param name="updateErrorCodes"></param>
        public void CreateDiagnosticReportResult(DiagnosticReportErrorCodeSystemType diagnosticReportErrorCodeSystemType, bool updateErrorCodes)
        {
            this.CreateDiagnosticReportResult(diagnosticReportErrorCodeSystemType, updateErrorCodes, false, false);
        }

        /// <summary>
        /// Creates (or updates if one exists) a <see cref="DiagnosticReportResult"/> for this report.
        /// </summary>
        /// <param name="diagnosticReportErrorCodeSystemType">The <see cref="DiagnosticReportErrorCodeSystemType"/> fixes to be updated.</param>
        /// <param name="updateErrorCodes">A <see cref="bool"/> that indicates whether the error code definitions should be updated.</param>
        /// <param name="logDiscrepancies">A <see cref="bool"/> that indicates if discrepancies should be logged.</param>
        /// <param name="onlyProcessFixes">A <see cref="bool"/> that indicates if only logic necessary for looking up fixes should be executed. Everything else should be skipped.</param>
        public void CreateDiagnosticReportResult(DiagnosticReportErrorCodeSystemType diagnosticReportErrorCodeSystemType, bool updateErrorCodes, bool logDiscrepancies, bool onlyProcessFixes)
        {
            if (this.SymptomGuids.Any())
            {
                this.CreateDiagnosticReportResultFixForSymptoms();
                return;
            }

            bool fixFound = false;

            if (diagnosticReportErrorCodeSystemType == DiagnosticReportErrorCodeSystemType.PowertrainObd2)
            {
                fixFound = this.CreateDiagnosticReportResult(this.pwrMilCode, DiagnosticReportErrorCodeSystemType.PowertrainObd2, updateErrorCodes, logDiscrepancies, onlyProcessFixes);

                if (!this.pwrFixFoundAfterLastFixLookup)
                {
                    this.pwrFixFoundAfterLastFixLookup = fixFound;
                }
            }
            else if (diagnosticReportErrorCodeSystemType == DiagnosticReportErrorCodeSystemType.PowertrainOBD1)
            {
                foreach (string code in this.Obd1StoredCodes.AllDtcs)
                {
                    if (this.CreateDiagnosticReportResult(code, DiagnosticReportErrorCodeSystemType.PowertrainOBD1, updateErrorCodes, logDiscrepancies, onlyProcessFixes))
                    {
                        fixFound = true;
                    }
                }

                if (!this.obd1FixFoundAfterLastFixLookup)
                {
                    this.obd1FixFoundAfterLastFixLookup = fixFound;
                }
            }
            else if (diagnosticReportErrorCodeSystemType == DiagnosticReportErrorCodeSystemType.ABS)
            {
                //#BCUCodes
                List<string> allDtcs = new List<string>();
                foreach (string code in this.AbsStoredCodes.AllDtcs)
                {
                    allDtcs.Add(code);
                }

                fixFound = this.CreateDiagnosticReportResult(allDtcs,
                    DiagnosticReportErrorCodeSystemType.ABS, true, logDiscrepancies, onlyProcessFixes);

                if (!this.absFixFoundAfterLastFixLookup)
                {
                    this.absFixFoundAfterLastFixLookup = fixFound;
                }
            }
            else if (diagnosticReportErrorCodeSystemType == DiagnosticReportErrorCodeSystemType.SRS)
            {
                //#BCUCodes
                List<string> allDtcs = new List<string>();
                foreach (string code in this.SrsStoredCodes.AllDtcs)
                {
                    allDtcs.Add(code);
                }

                fixFound = this.CreateDiagnosticReportResult(allDtcs,
                    DiagnosticReportErrorCodeSystemType.SRS, true, logDiscrepancies, onlyProcessFixes);

                if (!this.srsFixFoundAfterLastFixLookup)
                {
                    this.srsFixFoundAfterLastFixLookup = fixFound;
                }
            }
            else if (diagnosticReportErrorCodeSystemType == DiagnosticReportErrorCodeSystemType.Enhanced && updateErrorCodes)
            {
                if (this.DiagnosticReportResult == null)
                {
                    //creates a new diagnostic report result
                    this.DiagnosticReportResult = (DiagnosticReportResult)Registry.CreateInstance(typeof(DiagnosticReportResult));
                    this.DiagnosticReportResult.DiagnosticReport = this;
                }
                this.CreateDiagnosticReportResultErrorCodes(DiagnosticReportErrorCodeSystemType.Enhanced);
            }

            this.SetFixStatuses(diagnosticReportErrorCodeSystemType);

            if (fixFound && this.FixProvidedDateTimeUTC.IsNull)
            {
                this.FixProvidedDateTimeUTC = DateTime.UtcNow;
            }
        }

        /// <summary>
        /// Creates (or updates if one exists) a <see cref="DiagnosticReportResult"/> for this report.
        /// </summary>
        /// <param name="primaryDtc">The <see cref="string"/> primary DTC.</param>
        /// <param name="diagnosticReportErrorCodeSystemType">The <see cref="DiagnosticReportErrorCodeSystemType"/> fixes to be updated.</param>
        /// <param name="updateErrorCodes">A <see cref="bool"/> that indicates whether the error code definitions should be updated.</param>
        /// <param name="logDiscrepancies">A <see cref="bool"/> that indicates if discrepancies should be logged.</param>
        /// <param name="onlyProcessFixes">A <see cref="bool"/> that indicates if only logic necessary for looking up fixes should be executed. Everything else should be skipped.</param>
        /// <returns>A <see cref="bool"/> that indicates if a fix was found for the system when one did not previously exist.</returns>
        private bool CreateDiagnosticReportResult(string primaryDtc, DiagnosticReportErrorCodeSystemType diagnosticReportErrorCodeSystemType, bool updateErrorCodes, bool logDiscrepancies, bool onlyProcessFixes)
        {
            bool newFixFound = false;
            DiagnosticReportFixStatus oldFixStatus = DiagnosticReportFixStatus.FixNotFound;

            //#LEDMILUpdate
            bool isFixNotNeeded = false;

            if (diagnosticReportErrorCodeSystemType != DiagnosticReportErrorCodeSystemType.Enhanced)
            {
                if (string.IsNullOrEmpty(primaryDtc))
                {
                    return false;
                }

                FixCollection fixes = new FixCollection(this.Registry);

                if (this.DiagnosticReportResult == null)
                {
                    //creates a new diagnostic report result
                    this.DiagnosticReportResult = (DiagnosticReportResult)Registry.CreateInstance(typeof(DiagnosticReportResult));
                    this.DiagnosticReportResult.DiagnosticReport = this;
                }

                switch (diagnosticReportErrorCodeSystemType)
                {
                    case DiagnosticReportErrorCodeSystemType.ABS:
                        //#Logic for P0420, P0421, P0422, P0430, P0431, P0432

                        fixes = this.GetFixesSorted2(primaryDtc, this.AbsPendingCodes.AllDtcs, logDiscrepancies, onlyProcessFixes, diagnosticReportErrorCodeSystemType);
                        oldFixStatus = this.AbsDiagnosticReportFixStatus;
                        break;

                    case DiagnosticReportErrorCodeSystemType.PowertrainOBD1:
                        //#Logic for P0420, P0421, P0422, P0430, P0431, P0432

                        fixes = this.GetFixesSorted2(primaryDtc, this.Obd1PendingCodes.AllDtcs, logDiscrepancies, onlyProcessFixes, diagnosticReportErrorCodeSystemType);
                        oldFixStatus = this.Obd1DiagnosticReportFixStatus;
                        break;

                    //#LEDMILUpdate
                    case DiagnosticReportErrorCodeSystemType.PowertrainObd2:
                        if (this.ToolMilStatus == ToolMilStatus.On && this.ToolLEDStatus == ToolLEDStatus.Red)
                        {
                            //#Logic for P0420, P0421, P0422, P0430, P0431, P0432
                            fixes = GetFixesSorted3(primaryDtc, this.SecondaryErrorCodes, logDiscrepancies, onlyProcessFixes, diagnosticReportErrorCodeSystemType);//#IWD-12
                        }
                        else
                        {
                            isFixNotNeeded = true;
                            this.PwrDiagnosticReportFixStatus = DiagnosticReportFixStatus.FixNotNeeded;
                        }
                        oldFixStatus = this.PwrDiagnosticReportFixStatus;
                        break;

                    case DiagnosticReportErrorCodeSystemType.SRS:
                        //#Logic for P0420, P0421, P0422, P0430, P0431, P0432

                        fixes = this.GetFixesSorted2(primaryDtc, this.SrsPendingCodes.AllDtcs, logDiscrepancies, onlyProcessFixes, diagnosticReportErrorCodeSystemType);
                        oldFixStatus = this.SrsDiagnosticReportFixStatus;
                        break;
                }

                if (!onlyProcessFixes)
                {
                    /*************************************************************
					* Remove the existing fixes for the given DTC and system type
					*************************************************************/

                    this.DiagnosticReportResult.DiagnosticReportResultFixes.LoadRelation(this.DiagnosticReportResult
                        .DiagnosticReportResultFixes.RelationDiagnosticReportResultFixeParts);

                    //ToolDB_
                    this.DiagnosticReportResult.DiagnosticReportResultFixes.LoadRelation(this.DiagnosticReportResult
                        .DiagnosticReportResultFixes.RelationDiagnosticReportResultFixTools);
                    //ToolDB_

                    ArrayList oldFixes = null;

                    foreach (DiagnosticReportResultFix f in this.DiagnosticReportResult.DiagnosticReportResultFixes)
                    {
                        //only do add if the report
                        if (f.DiagnosticReportErrorCodeSystemType == diagnosticReportErrorCodeSystemType)
                        {
                            //2012-06-19 STW changing this to check the stored primary code since fixes have multiple codes now
                            //if(String.Equals(f.Fix.PrimaryErrorCode, primaryDtc, StringComparison.OrdinalIgnoreCase))
                            if (String.Equals(f.PrimaryErrorCode, primaryDtc, StringComparison.OrdinalIgnoreCase))
                            {
                                if (oldFixes == null)
                                {
                                    oldFixes = new ArrayList();
                                }
                                oldFixes.Add(f);
                            }
                        }
                    }

                    if (oldFixes != null && oldFixes.Count > 0)
                    {
                        foreach (DiagnosticReportResultFix drrFix in oldFixes)
                        {
                            this.DiagnosticReportResult.DiagnosticReportResultFixes.Remove(drrFix);
                        }
                    }

                    /*************************************************************
					* Add the new fixes
					*************************************************************/
                    for (int i = 0; i < fixes.Count; i++)
                    {
                        Fix f = fixes[i];

                        DiagnosticReportResultFix drrFix = f.ToDiagnosticReportResultFix(diagnosticReportErrorCodeSystemType, DiagnosticReportResult);

                        drrFix.PrimaryErrorCode = primaryDtc;

                        //#Logic for P0420, P0421, P0422, P0430, P0431, P0432
                        if (!string.IsNullOrWhiteSpace(f.PrimaryDTCs) && !f.PrimaryDTCs.Contains(primaryDtc))
                        {
                            drrFix.PrimaryErrorCode = f.PrimaryDTCs.Split(',').FirstOrDefault().Trim();
                        }

                        drrFix.DiagnosticReportErrorCodeSystemType = diagnosticReportErrorCodeSystemType;
                        drrFix.SortOrder = i;
                        drrFix.SortDescription = "";
                        this.DiagnosticReportResult.DiagnosticReportResultFixes.Add(drrFix);
                    }

                    //resort the entire list now
                    SortDictionaryCollection sorts = new SortDictionaryCollection();
                    sorts.Add(new SortDictionary("DiagnosticReportErrorCodeSystemType"));
                    sorts.Add(new SortDictionary("SortOrder"));
                    this.DiagnosticReportResult.DiagnosticReportResultFixes.Sort(sorts);
                }
            }

            if (!onlyProcessFixes)
            {
                //#LEDMILUpdate
                this.SetFixStatuses(diagnosticReportErrorCodeSystemType, isFixNotNeeded);

                DiagnosticReportFixStatus newFixStatus = this.GetFixStatus(diagnosticReportErrorCodeSystemType);

                // See if we went from something else to FixFound
                if (newFixStatus == DiagnosticReportFixStatus.FixFound && newFixStatus != oldFixStatus)
                {
                    newFixFound = true;
                }

                if (!onlyProcessFixes && updateErrorCodes)
                {
                    this.CreateDiagnosticReportResultErrorCodes(diagnosticReportErrorCodeSystemType);
                }
            }

            return newFixFound;
        }

        //#ABSFixStatus_ReplaceMethod
        /// <summary>
        /// Sets the <see cref="DiagnosticReportFixStatus"/> based on the current values of <see cref="DiagnosticReport.ToolMilStatus"/>, <see cref="DiagnosticReport.ToolLEDStatus"/>, <see cref="string"/> DiagnosticReport.PrimaryErrorCode and <see cref="int"/> DiagnosticReportResult.DiagnosticReportResultFixes.Count on the report.
        /// </summary>
        public void SetFixStatuses(DiagnosticReportErrorCodeSystemType systemType, bool fixNotNeeded = false) //#LEDMILUpdate
        {
            StringCollection storedCodes = new StringCollection();
            bool isPwrLookForFix = (this.ToolMilStatus == ToolMilStatus.On && this.ToolLEDStatus == ToolLEDStatus.Red && !string.IsNullOrEmpty(this.PwrMilCode));
            DiagnosticReportFixStatus fixStatus = DiagnosticReportFixStatus.FixNotFound;
            bool isFixFeedbackRequired = false;

            switch (systemType)
            {
                case DiagnosticReportErrorCodeSystemType.ABS:
                    storedCodes = this.AbsStoredCodes.AllDtcs;
                    break;

                case DiagnosticReportErrorCodeSystemType.PowertrainOBD1:
                    storedCodes = this.Obd1StoredCodes.AllDtcs;
                    break;

                case DiagnosticReportErrorCodeSystemType.PowertrainObd2:
                    storedCodes = this.PwrStoredCodes;
                    break;

                case DiagnosticReportErrorCodeSystemType.SRS:
                    storedCodes = this.SrsStoredCodes.AllDtcs;
                    break;
            }

            if (systemType == DiagnosticReportErrorCodeSystemType.PowertrainObd2)
            {
                //#LEDMILUpdate
                if (!isPwrLookForFix || fixNotNeeded == true)
                {
                    fixStatus = DiagnosticReportFixStatus.FixNotNeeded;
                }
                else
                {
                    if (this.DiagnosticReportResult.GetFixesBySystemType(systemType).Count == 0)
                    {
                        fixStatus = DiagnosticReportFixStatus.FixNotFound;
                        isFixFeedbackRequired = true;
                    }
                    else
                    {
                        fixStatus = DiagnosticReportFixStatus.FixFound;
                    }
                }
            }
            else
            {
                if (storedCodes.Count == 0 || fixNotNeeded == true)
                {
                    fixStatus = DiagnosticReportFixStatus.FixNotNeeded;
                }
                else
                {
                    if (this.DiagnosticReportResult.GetFixesBySystemType(systemType).Count == 0)
                    {
                        fixStatus = DiagnosticReportFixStatus.FixNotFound;
                        isFixFeedbackRequired = true;
                    }
                    else
                    {
                        fixStatus = DiagnosticReportFixStatus.FixFound;
                    }
                }
            }

            switch (systemType)
            {
                case DiagnosticReportErrorCodeSystemType.ABS:
                    //#LEDMILUpdate
                    if (fixNotNeeded == true)
                    {
                        this.AbsDiagnosticReportFixStatus = DiagnosticReportFixStatus.FixNotNeeded;
                        this.IsAbsFixFeedbackRequired = false;
                        if (this.IsObjectCreated)
                        {
                            this.AbsDiagnosticReportFixStatusWhenCreated = this.AbsDiagnosticReportFixStatus;
                        }
                    }
                    else
                    {
                        this.AbsDiagnosticReportFixStatus = fixStatus;
                        this.IsAbsFixFeedbackRequired = isFixFeedbackRequired;
                        if (this.IsObjectCreated)
                        {
                            this.AbsDiagnosticReportFixStatusWhenCreated = this.AbsDiagnosticReportFixStatus;
                        }
                    }

                    break;

                case DiagnosticReportErrorCodeSystemType.PowertrainOBD1:
                    this.Obd1DiagnosticReportFixStatus = fixStatus;
                    this.IsPwrObd1FixFeedbackRequired = isFixFeedbackRequired;
                    if (this.IsObjectCreated)
                    {
                        this.Obd1DiagnosticReportFixStatusWhenCreated = this.Obd1DiagnosticReportFixStatus;
                    }
                    break;

                case DiagnosticReportErrorCodeSystemType.PowertrainObd2:
                    this.PwrDiagnosticReportFixStatus = fixStatus;
                    this.IsPwrObd2FixFeedbackRequired = isFixFeedbackRequired;
                    if (this.IsObjectCreated)
                    {
                        this.PwrDiagnosticReportFixStatusWhenCreated = this.PwrDiagnosticReportFixStatus;
                    }
                    break;

                case DiagnosticReportErrorCodeSystemType.SRS: //#SP36
                    if (fixNotNeeded == true)
                    {
                        this.SrsDiagnosticReportFixStatus = DiagnosticReportFixStatus.FixNotNeeded;
                        this.IsSrsFixFeedbackRequired = false;
                        if (this.IsObjectCreated)
                        {
                            this.SrsDiagnosticReportFixStatusWhenCreated = this.SrsDiagnosticReportFixStatus;
                        }
                    }
                    else
                    {
                        this.SrsDiagnosticReportFixStatus = fixStatus;
                        this.IsSrsFixFeedbackRequired = isFixFeedbackRequired;
                        if (this.IsObjectCreated)
                        {
                            this.SrsDiagnosticReportFixStatusWhenCreated = this.SrsDiagnosticReportFixStatus;
                        }
                    }

                    break;
            }
        }

        /// <summary>
        /// Sets the diagnostic report error codes, always called internally.
        /// </summary>
        private void CreateDiagnosticReportResultErrorCodes(DiagnosticReportErrorCodeSystemType diagnosticReportErrorCodeSystemType)
        {
            if (this.DiagnosticReportResult == null)
            {
                //creates a new diagnostic report result
                this.DiagnosticReportResult = (DiagnosticReportResult)Registry.CreateInstance(typeof(DiagnosticReportResult));
                this.DiagnosticReportResult.DiagnosticReport = this;
            }

            StringCollection allCodes = new StringCollection();
            StringCollection storedCodes = new StringCollection();
            StringCollection pendingCodes = new StringCollection();
            StringCollection permanentCodes = new StringCollection(); //Added on November 23 2016 3:55PM to support the new Code Type

            switch (diagnosticReportErrorCodeSystemType)
            {
                case DiagnosticReportErrorCodeSystemType.ABS:
                    allCodes = this.AbsAllCodes;
                    storedCodes = this.AbsStoredCodes.AllDtcs;
                    pendingCodes = this.AbsPendingCodes.AllDtcs;
                    break;

                case DiagnosticReportErrorCodeSystemType.Enhanced:
                    allCodes = this.EnhancedDtcs.AllDtcs;
                    storedCodes = this.EnhancedDtcs.AllDtcs;
                    break;

                case DiagnosticReportErrorCodeSystemType.PowertrainOBD1:
                    allCodes = this.Obd1AllCodes;
                    storedCodes = this.Obd1StoredCodes.AllDtcs;
                    pendingCodes = this.Obd1PendingCodes.AllDtcs;
                    break;

                case DiagnosticReportErrorCodeSystemType.PowertrainObd2:
                    allCodes = this.PwrAllCodes;
                    storedCodes = this.PwrStoredCodes;
                    pendingCodes = this.PwrPendingCodes;
                    permanentCodes = this.PwrPermanentCodes; //Added on November 23 2016 3:57PM to support the new Code Type
                    break;

                case DiagnosticReportErrorCodeSystemType.SRS:
                    allCodes = this.SrsAllCodes;
                    storedCodes = this.SrsStoredCodes.AllDtcs;
                    pendingCodes = this.SrsPendingCodes.AllDtcs;
                    break;
            }

            if (allCodes.Count == 0)
            {
                return;
            }

            //flags for finding the type
            bool foundPrimary = false;
            bool foundFirstStored = false;
            bool foundFirstPending = false;
            bool foundFirstPermanent = false;

            StringCollection tempStoredErrorCodes = new StringCollection();
            StringCollection tempPendingErrorCodes = new StringCollection();
            StringCollection tempPermanentErrorCodes = new StringCollection();

            //get the dtc codes
            //#Sprint23
            DtcCodeCollection dtcCodes = this.GetDtcCodes(GetFirstPartErrorCodes(allCodes));
            //get the master codes
            //#Sprint23
            DtcMasterCodeListCollection dtcMasterCodes = this.GetDtcMasterCodes(GetFirstPartErrorCodes(allCodes));
            //and get the delmar codes

            //#ChiltonDTC_reintegrate
            VehicleTypeCodeAssignmentCollection codeAssignments = null;
            VehicleTypeCollection vehicleTypes = this.GetDelmarVehicleTypes();
            if (vehicleTypes.Count > 0)
            {
                //#Sprint23
                codeAssignments = this.GetDelmarVehicleCodeAssignments(vehicleTypes, GetFirstPartErrorCodes(allCodes));
            }

            /*
			 * 2010-02-23 STW, this can be simplied.  The codes are now in order, so the SortErrorCodes method can be removed and the "SortOrder" can be set
			 * while processing.   Once the SortOrder is set the entire list can simply be sorted based on error code system (Pwr, Abs etc) and the SortOrder
			 * property.  The database outputs these values in order (sorted in SPROC)
			 */

            // First see if we have any existing error objects for this system type. If so, remove them.
            ArrayList oldDrrErrorCodes = this.DiagnosticReportResult.DiagnosticReportResultErrorCodes.GetAllByProperty("DiagnosticReportErrorCodeSystemType", diagnosticReportErrorCodeSystemType);
            //clear the lookup tables here so we can remove the items from the list
            this.DiagnosticReportResult.DiagnosticReportResultErrorCodes.ClearLookupTables();

            //loop through the array and remove error codes that will be replaced.
            if (oldDrrErrorCodes != null)
            {
                foreach (DiagnosticReportResultErrorCode oldDrrErrorCode in oldDrrErrorCodes)
                {
                    this.DiagnosticReportResult.DiagnosticReportResultErrorCodes.Remove(oldDrrErrorCode);
                }
            }

            for (int i = 0; i < allCodes.Count; i++)
            {
                string errorCode = allCodes[i];

                DiagnosticReportErrorCodeType diagnosticReportErrorCodeType;

                //determine the type of error code from the list
                if (diagnosticReportErrorCodeSystemType == DiagnosticReportErrorCodeSystemType.PowertrainObd2 &&
                    !foundPrimary && errorCode == this.PwrMilCode
                    && this.ToolLEDStatus == ToolLEDStatus.Red)
                {
                    tempStoredErrorCodes.Add(errorCode);

                    foundPrimary = true;
                    diagnosticReportErrorCodeType = DiagnosticReportErrorCodeType.PrimaryDiagnosticReportErrorCode;
                    //#ChiltonDTC_reintegrate
                    DiagnosticReportResultErrorCode drrErrorCode = this.GetDiagnosticReportResultErrorCode(GetFirstPartErrorCode(errorCode), diagnosticReportErrorCodeType, "", dtcCodes, codeAssignments, dtcMasterCodes);

                    if (drrErrorCode != null)
                    {
                        //#Sprint23
                        drrErrorCode.ErrorCode = errorCode;//assign back to original code
                        drrErrorCode.DiagnosticReportErrorCodeSystemType = diagnosticReportErrorCodeSystemType;
                        drrErrorCode.SortOrder = i;
                        //add the error code to the list for this report result
                        this.DiagnosticReportResult.DiagnosticReportResultErrorCodes.Add(drrErrorCode);
                    }
                }

                //if the code is in the stored code list and not in the processed list then
                if (storedCodes.Contains(errorCode) && !tempStoredErrorCodes.Contains(errorCode))
                {
                    tempStoredErrorCodes.Add(errorCode);

                    if (!foundFirstStored)
                    {
                        diagnosticReportErrorCodeType = DiagnosticReportErrorCodeType.FirstStoredDiagnosticReportErrorCode;
                        foundFirstStored = true;
                    }
                    else
                    {
                        diagnosticReportErrorCodeType = DiagnosticReportErrorCodeType.AdditionalStoredDiagnosticReportErrorCode;
                    }

                    //#ChiltonDTC_reintegrate
                    DiagnosticReportResultErrorCode drrErrorCode = this.GetDiagnosticReportResultErrorCode(GetFirstPartErrorCode(errorCode), diagnosticReportErrorCodeType, "", dtcCodes, codeAssignments, dtcMasterCodes);

                    if (drrErrorCode != null)
                    {
                        //#Sprint23
                        drrErrorCode.ErrorCode = errorCode;//assign back to original code
                        drrErrorCode.DiagnosticReportErrorCodeSystemType = diagnosticReportErrorCodeSystemType;
                        drrErrorCode.SortOrder = i;
                        //add the error code to the list for this report result
                        this.DiagnosticReportResult.DiagnosticReportResultErrorCodes.Add(drrErrorCode);
                    }
                }

                if (pendingCodes.Contains(errorCode) && !tempPendingErrorCodes.Contains(errorCode))
                {
                    if (!foundFirstPending)
                    {
                        diagnosticReportErrorCodeType = DiagnosticReportErrorCodeType.FirstPendingDiagnosticReportErrorCode;
                        foundFirstPending = true;
                    }
                    else
                    {
                        diagnosticReportErrorCodeType = DiagnosticReportErrorCodeType.AdditionalPendingDiagnosticReportErrorCode;
                    }

                    //#ChiltonDTC_reintegrate
                    DiagnosticReportResultErrorCode drrErrorCode = this.GetDiagnosticReportResultErrorCode(GetFirstPartErrorCode(errorCode), diagnosticReportErrorCodeType, "", dtcCodes, codeAssignments, dtcMasterCodes);

                    if (drrErrorCode != null)
                    {
                        //#Sprint23
                        drrErrorCode.ErrorCode = errorCode;//assign back to original code
                        drrErrorCode.DiagnosticReportErrorCodeSystemType = diagnosticReportErrorCodeSystemType;
                        drrErrorCode.SortOrder = i;
                        //add the error code to the list for this report result
                        this.DiagnosticReportResult.DiagnosticReportResultErrorCodes.Add(drrErrorCode);
                    }
                }

                //Added on November 23 2016 4:05PM to support the Permanent Code Type.
                if (permanentCodes.Contains(errorCode) && !tempPermanentErrorCodes.Contains(errorCode))
                {
                    if (!foundFirstPermanent)
                    {
                        diagnosticReportErrorCodeType = DiagnosticReportErrorCodeType.FirstPermanentDiagnosticReportErrorCode;
                        foundFirstPermanent = true;
                    }
                    else
                    {
                        diagnosticReportErrorCodeType = DiagnosticReportErrorCodeType.AdditionalPermanentDiagnosticReportErrorCode;
                    }

                    //#ChiltonDTC_reintegrate
                    DiagnosticReportResultErrorCode drrErrorCode = this.GetDiagnosticReportResultErrorCode(GetFirstPartErrorCode(errorCode), diagnosticReportErrorCodeType, "", dtcCodes, codeAssignments, dtcMasterCodes);

                    if (drrErrorCode != null)
                    {
                        //#Sprint23
                        drrErrorCode.ErrorCode = errorCode;//assign back to original code
                        drrErrorCode.DiagnosticReportErrorCodeSystemType = diagnosticReportErrorCodeSystemType;
                        drrErrorCode.SortOrder = i;
                        //add the error code to the list for this report result
                        this.DiagnosticReportResult.DiagnosticReportResultErrorCodes.Add(drrErrorCode);
                    }
                }
            }

            //resort the entire list
            SortDictionaryCollection sorts = new SortDictionaryCollection();
            sorts.Add(new SortDictionary("DiagnosticReportErrorCodeSystemType"));
            sorts.Add(new SortDictionary("DiagnosticReportErrorCodeType"));
            sorts.Add(new SortDictionary("SortOrder"));
            this.DiagnosticReportResult.DiagnosticReportResultErrorCodes.Sort(sorts);
        }

        #region CarMD Error Code and Master DTC Code List

        /// <summary>
        /// Gets a <see cref="DtcCodeCollection"/> of <see cref="DtcCode"/> objects that match this diagnostic report from the DtcCode table.
        /// </summary>
        /// <param name="errorCodes">The <see cref="StringCollection"/> of error codes to be looked up.</param>
        /// <returns><see cref="DtcCodeCollection"/> of <see cref="DtcCode"/> objects that match this diagnostic report from the DtcCode table.</returns>
        private DtcCodeCollection GetDtcCodes(StringCollection errorCodes)
        {
            DtcCodeCollection dtcCodes = new DtcCodeCollection(this.Registry);

            SqlProcedureCommand call = new SqlProcedureCommand();
            call.ProcedureName = "DTCCode_LoadByDiagnosticReportAndErrorCodes";
            call.AddInt32("Year", this.Vehicle.Year);

            call.AddNVarChar("Make", this.Vehicle.Make);
            call.AddNVarChar("Model", this.Vehicle.Model);
            call.AddNVarChar("Transmission", this.Vehicle.TransmissionControlType);

            call.AddNVarChar("EngineType", this.Vehicle.EngineType);
            call.AddNVarChar("TrimLevel", this.Vehicle.TrimLevel);
            call.AddNVarChar("EngineVINCode", this.Vehicle.EngineVINCode);
            call.AddNVarChar("ErrorCodes", this.GetErrorCodesAsXmlList(errorCodes));

            dtcCodes.Load(call, "DTCCodeId", true, true);

            SortDictionaryCollection sorts = new SortDictionaryCollection();

            sorts.Add(new SortDictionary("HasDefinedCount"));
            sorts.Add(new SortDictionary("FrequencyCount"));
            sorts.Add(new SortDictionary("UpdatedDateTimeUTC", SortDirection.Descending));

            dtcCodes.Sort(sorts);

            return dtcCodes;
        }

        /// <summary>
        /// Gets a <see cref="DtcMasterCodeListCollection"/> of <see cref="DtcMasterCodeList"/> objects that match this diagnostic report from the DtcCode table.
        /// </summary>
        /// <param name="errorCodes">The <see cref="StringCollection"/> of error codes to be looked up.</param>
        /// <returns><see cref="DtcMasterCodeListCollection"/> of <see cref="DtcMasterCodeList"/> objects that match this diagnostic report from the DtcCode table.</returns>
        private DtcMasterCodeListCollection GetDtcMasterCodes(StringCollection errorCodes)
        {
            DtcMasterCodeListCollection dtcMasterCodes = new DtcMasterCodeListCollection(this.Registry);

            SqlProcedureCommand call = new SqlProcedureCommand();
            call.SqlCommand.CommandTimeout = 120; //Added on 2018-01-12 1:57 PM by INNOVA Dev Team to increase the timeout wait due to a heavy load of the No-Fix Report data.

            call.ProcedureName = "DTCMasterCodeList_LoadByDiagnosticReportAndErrorCodes";
            call.AddNVarChar("Make", this.Vehicle.Make);
            call.AddNVarChar("ErrorCodesXmlList", this.GetErrorCodesAsXmlList(errorCodes));

            dtcMasterCodes.Load(call, "DTCMasterCodeId", true, true);

            return dtcMasterCodes;
        }

        #endregion CarMD Error Code and Master DTC Code List

        //Re-AddChiltonDTC

        #region Delmar Error Code == Remove Chilton DTCs on 2018-10-15 3:46 PM

        //#DisableChiltonTDC
        ///// <summary>
        ///// Gets the collection of vehicle type code assignments for the vehicle types supplied.
        ///// </summary>
        ///// <param name="vehicleTypes"><see cref="VehicleTypeCollection"/> of <see cref="VehicleType"/> objects to obtain the assignments for.</param>
        ///// <param name="errorCodes">A <see cref="StringCollection"/> of error codes.</param>
        ///// <returns><see cref="VehicleTypeCodeAssignmentCollection"/> of <see cref="VehicleTypeCodeAssignment"/> objects matching the codes (error codes for report) vehicles supplied.</returns>
        //#ChiltonDTC_reintegrate
        private VehicleTypeCodeAssignmentCollection GetDelmarVehicleCodeAssignments(VehicleTypeCollection vehicleTypes, StringCollection errorCodes)
        {
            VehicleTypeCodeAssignmentCollection codeAssignments = new VehicleTypeCodeAssignmentCollection(Registry);

            SqlProcedureCommand call = new SqlProcedureCommand();
            call.ProcedureName = "VehicleTypeCodeAssignment_LoadByVehicleTypeXmlGuidListAndErrorCodeXmlList";
            call.AddNText("XmlVehicleTypeGuidList", vehicleTypes.ToXmlGuidList());
            call.AddNText("XmlErrorList", this.GetErrorCodesAsXmlList(errorCodes));

            codeAssignments.Load(call, "VehicleTypeCodeAssignmentId", true, true);

            //load the relations for the codes for these assignments
            codeAssignments.LoadRelation(codeAssignments.RelationVehicleTypeCodes);

            codeAssignments.Sort("ErrorCode");

            return codeAssignments;
        }

        ///// <summary>
        ///// Gets the <see cref="VehicleTypeCollection"/> of <see cref="VehicleType"/> objects that match the diagnostic upload.
        ///// </summary>
        ///// <returns><see cref="VehicleTypeCollection"/> of <see cref="VehicleType"/> objects that match the diagnostic upload.</returns>
        //#ChiltonDTC_reintegrate
        private VehicleTypeCollection GetDelmarVehicleTypes()
        {
            return this.SearchForDelmarVehicleTypes(this.Vehicle.Year, this.Vehicle.Make, this.Vehicle.Model, this.Vehicle.TransmissionControlType, this.Vehicle.EngineVINCode, this.Vehicle.EngineType, this.Vehicle.BodyCode);
        }

        ///// <summary>
        ///// Gets a collection of VehicleTypes given various criteria. Any parameters that are null will be ommitted as criteria for the search.
        ///// If an exact match is found then the collection will only contin one item.
        ///// </summary>
        ///// <param name="modelYear">The <see cref="int"/> year the vehicle was released. Null is allowed.</param>
        ///// <param name="make">The <see cref="string"/> make of the vehicle. Null is allowed.</param>
        ///// <param name="model">The <see cref="string"/> model of the vehicle. Null is allowed.</param>
        ///// <param name="transmissionType">The <see cref="string"/> transmission type of the vehicle. Null is allowed.</param>
        ///// <param name="engineVINCode">The <see cref="string"/> engine VIN code of the vehicle. Null is allowed.</param>
        ///// <param name="engineType">The <see cref="string"/> engine type of the vehicle. Null is allowed.</param>
        ///// <param name="bodyCode">The <see cref="string"/> body code of the vehicle. Null is allowed.</param>
        ///// <returns>A <see cref="VehicleTypeCollection"/> collection of any matching vehicles.</returns>
        //#ChiltonDTC_reintegrate
        private VehicleTypeCollection SearchForDelmarVehicleTypes(int modelYear, string make, string model, string transmissionType, string engineVINCode, string engineType, string bodyCode)
        {
            VehicleTypeCollection vehicleTypes = null;

            // If we have a body code then do the search
            if (bodyCode != null)
            {
                vehicleTypes = this.GetDelmarVehicleTypes(modelYear, make, model, engineVINCode, transmissionType, engineType, bodyCode);
            }

            // If we got no results or have not done the search yet then try again using less criteria
            if (vehicleTypes == null || vehicleTypes.Count == 0)
            {
                //remove the body type
                vehicleTypes = this.GetDelmarVehicleTypes(modelYear, make, model, engineVINCode, transmissionType, engineType, null);

                // If we got no results or have not done the search yet then try again using less criteria
                if (vehicleTypes == null || vehicleTypes.Count == 0)
                {
                    //remove the engine type
                    vehicleTypes = this.GetDelmarVehicleTypes(modelYear, make, model, engineVINCode, transmissionType, null, null);

                    // If we got no results or have not done the search yet then try again using less criteria
                    if (vehicleTypes == null || vehicleTypes.Count == 0)
                    {
                        // If we have a transmission type then do the search
                        //remove the transmission type
                        vehicleTypes = this.GetDelmarVehicleTypes(modelYear, make, model, engineVINCode, null, null, null);

                        //remove the engine vin code YMM only
                        if (vehicleTypes == null || vehicleTypes.Count == 0)
                        {
                            vehicleTypes = this.GetDelmarVehicleTypes(modelYear, make, model, null, null, null, null);
                        }
                    }
                }
            }

            return vehicleTypes;
        }

        ///// <summary>
        ///// Gets a collection of VehicleTypes given various criteria. Any parameters that are null will be ommitted as criteria for the search.
        ///// If an exact match is found then the collection will only contin one item.
        ///// </summary>
        ///// <param name="modelYear">The <see cref="int"/> year the vehicle was released. Null is allowed.</param>
        ///// <param name="make">The <see cref="string"/> make of the vehicle. Null is allowed.</param>
        ///// <param name="model">The <see cref="string"/> model of the vehicle. Null is allowed.</param>
        ///// <param name="engineVINCode">The <see cref="string"/> engine VIN code of the vehicle. Null is allowed.</param>
        ///// <param name="transmissionType">The <see cref="string"/> transmission type of the vehicle. Null is allowed.</param>
        ///// <param name="engineType">The <see cref="string"/> engine type of the vehicle. Null is allowed.</param>
        ///// <param name="bodyCode">The <see cref="string"/> body code of the vehicle. Null is allowed.</param>
        ///// <returns>A <see cref="VehicleTypeCollection"/> collection of any matching vehicles.</returns>
        //#ChiltonDTC_reintegrate
        private VehicleTypeCollection GetDelmarVehicleTypes(int modelYear, string make, string model, string engineVINCode, string transmissionType, string engineType, string bodyCode)
        {
            VehicleTypeCollection vehicleTypes = new VehicleTypeCollection(Registry);
            SqlProcedureCommand call = new SqlProcedureCommand();

            call.ProcedureName = "VehicleType_LoadByVinData";
            call.AddInt32("Year", modelYear);
            if (make != null)
            {
                call.AddNVarChar("Make", make);
            }
            if (model != null)
            {
                call.AddNVarChar("Model", model);
            }
            if (engineVINCode != null)
            {
                call.AddNVarChar("EngineVINCode", engineVINCode);
            }
            if (transmissionType != null)
            {
                call.AddNVarChar("TransmissionType", transmissionType.ToString());
            }
            if (engineType != null)
            {
                call.AddNVarChar("EngineType", engineType);
            }
            if (bodyCode != null)
            {
                call.AddNVarChar("BodyCode", bodyCode);
            }

            vehicleTypes.Load(call, "VehicleTypeId", true, true);
            return vehicleTypes;
        }

        #endregion Delmar Error Code == Remove Chilton DTCs on 2018-10-15 3:46 PM

        //Re-AddChiltonDTC

        #endregion Create Diagnostic Report Result

        #region Miscellaneous Utilities for Class (Comma Separated List etc)

        private StringCollection GetStringCollectionFromCommaSeparatedList(string commaSeparatedList)
        {
            StringCollection sc = new StringCollection();

            if (commaSeparatedList != null && commaSeparatedList.Length > 0)
            {
                string[] list = commaSeparatedList.Split(",".ToCharArray());

                foreach (string s in list)
                {
                    sc.Add(s);
                }
            }
            return sc;
        }

        /// <summary>
        /// Gets a comma separated <see cref="string"/> from the string collection
        /// </summary>
        /// <param name="stringCollection"><see cref="StringCollection"/> to create the comma separated list from</param>
        /// <returns><see cref="string"/> of comma separated values of the string collection</returns>
        private string GetCommaSeparatedListFromStringCollection(StringCollection stringCollection)
        {
            string errorCodeList = "";

            if (stringCollection != null && stringCollection.Count > 0)
            {
                foreach (string s in stringCollection)
                {
                    if (errorCodeList.Length > 0)
                    {
                        errorCodeList += ",";
                    }
                    errorCodeList += s;
                }
            }
            return errorCodeList;
        }

        /// <summary>
        /// Gets a <see cref="string"/> of error codes as an XML list.
        /// </summary>
        /// <param name="errorCodes">The <see cref="StringCollection"/> of error codes to process.</param>
        /// <returns><see cref="string"/> xml list of error codes.</returns>
        public string GetErrorCodesAsXmlList(StringCollection errorCodes)
        {
            StringBuilder xml = new StringBuilder();

            xml.Append("<List>\n");

            for (int i = 0; i < errorCodes.Count; i++)
            {
                string id = errorCodes[i];
                xml.AppendFormat("\t<Item value=\"{0}\"/>\n", id);
            }

            xml.Append("</List>");

            return xml.ToString();
        }

        /// <summary>
        /// Gets a <see cref="DtcCodeViewCollection"/> from the provided delimited string.
        /// </summary>
        /// <param name="delimitedDtcCategoryValues">The <see cref="string"/> of delimited DTC values. (EXAMPLE: "dtc1,category1|dtc2,category2|dtc3,category3")</param>
        /// <returns>A <see cref="DtcCodeViewCollection"/> of <see cref="DtcCodeView"/> objects.</returns>
        private DtcCodeViewCollection GetDtcCodeViewCollectionFromDelimitedString(string delimitedDtcCategoryValues)
        {
            DtcCodeViewCollection dtcCodeViews = new DtcCodeViewCollection();

            if (!string.IsNullOrEmpty(delimitedDtcCategoryValues))
            {
                string[] codeCategoryDelimitedString = delimitedDtcCategoryValues.Split(new char[] { '|' });
                for (int i = 0; i < codeCategoryDelimitedString.Length; i++)
                {
                    string[] codeCategoryValues = codeCategoryDelimitedString[i].Split(new char[] { ',' });
                    string code = codeCategoryValues[0];
                    string category = codeCategoryValues[1];

                    DtcCodeView dtcView = (DtcCodeView)dtcCodeViews.FindByProperty("CodeType", category);

                    if (dtcView == null)
                    {
                        dtcView = new DtcCodeView(category);
                        dtcCodeViews.Add(dtcView);
                    }

                    dtcView.Codes.Add(code);
                }
            }

            return dtcCodeViews;
        }

        /// <summary>
        /// Gets a <see cref="DtcCodeViewCollection"/> from the provided delimited string.
        /// </summary>
        /// <param name="dtcCodeViews">The <see cref="DtcCodeViewCollection"/> of DTC values.</param>
        /// <returns>A <see cref="string"/> of dilimited values. (EXAMPLE: "dtc1,category1|dtc2,category2|dtc3,category3")</returns>
        private string GetDelimitedStringFromDtcCodeViewCollection(DtcCodeViewCollection dtcCodeViews)
        {
            StringBuilder delimitedDtcCategoryValues = new StringBuilder();

            if (dtcCodeViews != null)
            {
                foreach (DtcCodeView dtcCodeView in dtcCodeViews)
                {
                    foreach (string code in dtcCodeView.Codes)
                    {
                        if (delimitedDtcCategoryValues.Length != 0)
                        {
                            delimitedDtcCategoryValues.Append("|");
                        }

                        delimitedDtcCategoryValues.Append(code + "," + dtcCodeView.CodeType);
                    }
                }
            }

            return delimitedDtcCategoryValues.ToString();
        }

        #endregion Miscellaneous Utilities for Class (Comma Separated List etc)

        /// <summary>
        /// Assigns this report to a Master Tech for providing fix feedback and updates the user's no fix report count.
        /// Both the report and the MasterTech <see cref="User"/> will be saved in the process.
        /// </summary>
        /// <param name="maxDaysToProvideFeedback">The <see cref="int"/> maximum number of days that a MasterTech has to provide fix feedback.</param>
        /// <param name="masterTechAssignPwrNoFixReports">A <see cref="bool"/> indicating if powertrain no-fix reports should be assigned to MasterTech users.</param>
        /// <param name="masterTechAssignObd1NoFixReports">A <see cref="bool"/> indicating if OBD1 no-fix reports should be assigned to MasterTech users.</param>
        /// <param name="masterTechAssignAbsNoFixReports">A <see cref="bool"/> indicating if ABS no-fix reports should be assigned to MasterTech users.</param>
        /// <param name="masterTechAssignSrsNoFixReports">A <see cref="bool"/> indicating if SRS no-fix reports should be assigned to MasterTech users.</param>
        public void AssignNoFixReportToMasterTechAndSave(int maxDaysToProvideFeedback, bool masterTechAssignPwrNoFixReports, bool masterTechAssignObd1NoFixReports, bool masterTechAssignAbsNoFixReports, bool masterTechAssignSrsNoFixReports)
        {
            UserCollection masterTechUsers = Innova.Users.User.GetOBDFixMasterTechs(this.Registry, this.Vehicle.Make);
            this.AssignNoFixReportToMasterTechAndSave(masterTechUsers, maxDaysToProvideFeedback, masterTechAssignPwrNoFixReports, masterTechAssignObd1NoFixReports, masterTechAssignAbsNoFixReports, masterTechAssignSrsNoFixReports);
        }

        /// <summary>
        /// Assigns this report to a Master Tech for providing fix feedback and updates the user's no fix report count.
        /// Both the report and the MasterTech <see cref="User"/> will be saved in the process.
        /// </summary>
        /// <param name="masterTechs">A <see cref="UserCollection"/> of possible MasterTech users to be assigned.</param>
        /// <param name="maxDaysToProvideFeedback">The <see cref="int"/> maximum number of days that a MasterTech has to provide fix feedback.</param>
        /// <param name="masterTechAssignPwrNoFixReports">A <see cref="bool"/> indicating if powertrain no-fix reports should be assigned to MasterTech users.</param>
        /// <param name="masterTechAssignObd1NoFixReports">A <see cref="bool"/> indicating if OBD1 no-fix reports should be assigned to MasterTech users.</param>
        /// <param name="masterTechAssignAbsNoFixReports">A <see cref="bool"/> indicating if ABS no-fix reports should be assigned to MasterTech users.</param>
        /// <param name="masterTechAssignSrsNoFixReports">A <see cref="bool"/> indicating if SRS no-fix reports should be assigned to MasterTech users.</param>
        public void AssignNoFixReportToMasterTechAndSave(UserCollection masterTechs, int maxDaysToProvideFeedback, bool masterTechAssignPwrNoFixReports, bool masterTechAssignObd1NoFixReports, bool masterTechAssignAbsNoFixReports, bool masterTechAssignSrsNoFixReports)
        {
            DateTime minCreatedDateTimeUTC = DateTime.Now.Date.AddDays(0 - maxDaysToProvideFeedback).ToUniversalTime();

            if (!this.HasMasterTechsAssigned
                    &&
                    masterTechs != null
                    &&
                    (
                        this.CreatedDateTimeUTC >= minCreatedDateTimeUTC
                        ||
                        this.MasterTechProvideFixFeedbackByOverrideDateTimeUTC >= DateTime.UtcNow
                    )
                    &&
                    (
                        (masterTechAssignPwrNoFixReports && this.PwrDiagnosticReportFixStatus == DiagnosticReportFixStatus.FixNotFound && this.PwrFixNotFoundFixPromisedByDateTimeUTC.HasValue)
                        ||
                        (masterTechAssignObd1NoFixReports && this.Obd1DiagnosticReportFixStatus == DiagnosticReportFixStatus.FixNotFound && this.Obd1FixNotFoundFixPromisedByDateTimeUTC.HasValue)
                        ||
                        (masterTechAssignAbsNoFixReports && this.AbsDiagnosticReportFixStatus == DiagnosticReportFixStatus.FixNotFound && this.AbsFixNotFoundFixPromisedByDateTimeUTC.HasValue)
                        ||
                        (masterTechAssignSrsNoFixReports && this.SrsDiagnosticReportFixStatus == DiagnosticReportFixStatus.FixNotFound && this.SrsFixNotFoundFixPromisedByDateTimeUTC.HasValue)
                    )
                )
            {
                foreach (User mt in masterTechs)
                {
                    if (mt.MasterTechMakesLowerCase.Contains(this.Vehicle.Make.ToLower()))
                    {
                        this.AddAssignedMasterTech(mt);
                        mt.MasterTechNoFixReportLastAssignedDateTimeUTC = DateTime.UtcNow;
                        mt.Save();
                    }
                }

                this.Save();
            }
        }

        /// <summary>
        /// Gets a <see cref="decimal"/> currency value for presentation.
        /// </summary>
        /// <param name="usDollars">A <see cref="decimal"/> currency value in USD.</param>
        /// <param name="existingConvertedCurrencyValue">A <see cref="NullableDecimal"/> currency value that has already been converted.</param>
        /// <returns>A <see cref="decimal"/> currency value for presentation.</returns>
        public decimal GetCurrencyValueForPresentration(decimal usDollars, NullableDecimal existingConvertedCurrencyValue)
        {
            decimal convertedValue = 0m;

            // If the current currency is USD or the existing converted value currency is USD then return the usDollars value.
            if (this.RuntimeInfo.CurrentCurrency == Currency.USD || this.Currency == Innova.Currency.USD)
            {
                convertedValue = usDollars;
            }
            // If the existing converted currency is equal to the CurrencyCurrency then return the converted value.
            else if (this.RuntimeInfo.CurrentCurrency == this.Currency)
            {
                convertedValue = existingConvertedCurrencyValue.Value;
            }
            // Otherwise perform a currency conversion on the provided usDollars value.
            else
            {
                convertedValue = this.RuntimeInfo.GetLocalCurrencyValueFromUSDollars(usDollars);
            }

            return convertedValue;
        }


        private void SaveFixSortPriority(FixCollection fixes, string sortType, DiagnosticReportErrorCodeSystemType diagnosticReportErrorCodeSystemType)
        {
            for (int i = 0; i < fixes.Count; i++)
            {
                Fix f = fixes[i];

                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(this.Registry.ConnectionStringDefault))
                {
                    dr.ProcedureName = "DiagnosticReportFixSortPriority_Create";
                    dr.AddGuid("DiagnosticReportId", this.Id);
                    dr.AddGuid("FixId", f.Id);
                    dr.AddGuid("FixNameId", f.FixName.Id);
                    dr.AddInt32("DiagnosticReportErrorCodeSystemType", (int)diagnosticReportErrorCodeSystemType);
                    dr.AddInt32("SortOrder", i);
                    dr.AddNVarChar("SortType", sortType);
                    dr.AddDateTime("CreateDateTimeUTC", DateTime.UtcNow);
                    dr.ExecuteNonQuery();
                }
            }
        }

        #endregion Business Logic Methods

        #region Required Object Methods (Load, Save, Delete, Etc)

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
            Load(null, null, false);
        }

        /// <summary>
        /// Loads the data for the current object (this layer in the inheritence chain) if <see cref="IsObjectLoaded"/> is false.
        /// </summary>
        /// <param name="connection"><see cref="SqlConnection"/> currently being used (if any), if null, a new <see cref="SqlConnection"/> is created to perform the operation.</param>
        /// <param name="isLoadBase"><see cref="bool"/> when set to true, base layers (if any) will also be loaded.</param>
        /// <returns><see cref="SqlConnection"/> supplied or the one created internally.</returns>
        public new SqlConnection Load(SqlConnection connection, bool isLoadBase)
        {
            Load(connection, null, isLoadBase);

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
            EnsureValidId();

            if (isLoadBase)
            {
                //load the base item if user selected it.
                transaction = base.Load(connection, transaction, isLoadBase);
            }

            if (!IsObjectLoaded)
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
                    SetLoadProcedureCall(dr);

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
                        LoadPropertiesFromDataReader(dr, isLoadBase);
                    }
                    else
                    {
                        throw (new ApplicationException("Load Failed for type " + this.GetType().ToString() + " using Procedure: " + dr.ProcedureCall));
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
                SetPropertiesFromDataReader(dr);
            }

            IsObjectLoaded = true;
        }

        /// <summary>
        /// Method ensures the object is loaded.  This method is located in the get portion of the a property representing data in the database and is called there.  If the object's <see cref="IsObjectLoaded"/> property is false and the <see cref="IsObjectCreated"/> property is false, then the <see cref="Load()"/> method is invoked.
        /// </summary>
        protected new void EnsureLoaded()
        {
            if (!IsObjectLoaded && !IsObjectCreated)
            {
                Load();
            }
        }

        #endregion System Load Calls (DO NOT EDIT)

        /// <summary>
        /// Sets the base load procedure call and parameters to the supplied <see cref="SqlDataReaderWrapper"/>, to be executed.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> to set the procedure call and parameters to.</param>
        protected new void SetLoadProcedureCall(SqlDataReaderWrapper dr)
        {
            dr.ProcedureName = "DiagnosticReport_Load";
            dr.AddGuid("DiagnosticReportId", Id);
        }

        /// <summary>
        /// Sets the properties of this object from the fields in the recordset located in the supplied <see cref="SqlDataReaderWrapper"/>.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> containing the recordset and fields used to set the properties of this object (this layer only).</param>
        protected new void SetPropertiesFromDataReader(SqlDataReaderWrapper dr)
        {
            if (!dr.IsDBNull("DiagnosticReportResultId"))
            {
                this.diagnosticReportResult = (DiagnosticReportResult)Registry.CreateInstance(typeof(DiagnosticReportResult), dr.GetGuid("DiagnosticReportResultId"));
            }

            this.user = (User)Registry.CreateInstance(typeof(User), dr.GetGuid("UserId"));
            this.hasMasterTechsAssigned = dr.GetBoolean("HasMasterTechsAssigned");
            this.masterTechsAssignedIdList = dr.GetString("MasterTechsAssignedIdList");
            this.masterTechProvideFixFeedbackByOverrideDateTimeUTC = dr.GetNullableDateTime("MasterTechProvideFixFeedbackByOverrideDateTimeUTC");
            this.masterTechNotificationsSentDateTimeUTC = dr.GetNullableDateTime("MasterTechNotificationsSentDateTimeUTC");
            if (!dr.IsDBNull("AdminUserId_PwrWorkingOnFix"))
            {
                this.pwrAdminUserWorkingOnFix = (AdminUser)Registry.CreateInstance(typeof(AdminUser), dr.GetGuid("AdminUserId_PwrWorkingOnFix"));
            }
            if (!dr.IsDBNull("PwrAdminUserWorkingOnFixAssignedDateTimeUTC"))
            {
                this.pwrAdminUserWorkingOnFixAssignedDateTimeUTC = dr.GetDateTime("PwrAdminUserWorkingOnFixAssignedDateTimeUTC");
            }
            if (!dr.IsDBNull("AdminUserId_AbsWorkingOnFix"))
            {
                this.absAdminUserWorkingOnFix = (AdminUser)Registry.CreateInstance(typeof(AdminUser), dr.GetGuid("AdminUserId_AbsWorkingOnFix"));
            }
            if (!dr.IsDBNull("AbsAdminUserWorkingOnFixAssignedDateTimeUTC"))
            {
                this.absAdminUserWorkingOnFixAssignedDateTimeUTC = dr.GetDateTime("AbsAdminUserWorkingOnFixAssignedDateTimeUTC");
            }
            if (!dr.IsDBNull("AdminUserId_SrsWorkingOnFix"))
            {
                this.srsAdminUserWorkingOnFix = (AdminUser)Registry.CreateInstance(typeof(AdminUser), dr.GetGuid("AdminUserId_SrsWorkingOnFix"));
            }
            if (!dr.IsDBNull("SrsAdminUserWorkingOnFixAssignedDateTimeUTC"))
            {
                this.srsAdminUserWorkingOnFixAssignedDateTimeUTC = dr.GetDateTime("SrsAdminUserWorkingOnFixAssignedDateTimeUTC");
            }

            this.vehicle = (Vehicle)Registry.CreateInstance(typeof(Vehicle), dr.GetGuid("VehicleId"));
            this.device = (Device)Registry.CreateInstance(typeof(Device), dr.GetGuid("DeviceId"));

            if (!dr.IsDBNull("SymptomId"))
            {
                this.symptom = (Symptom)Registry.CreateInstance(typeof(Symptom), dr.GetGuid("SymptomId"));
            }

            this.repairOrderNumber = dr.GetString("RepairOrderNumber");
            this.vehicleMileage = dr.GetInt32("VehicleMileage");
            this.market = (Market)dr.GetInt32("Market");
            this.language = (Language)dr.GetInt32("Language");
            this.currency = (Currency)dr.GetInt32("Currency");
            this.currencyExchangeRate = dr.GetDecimal("CurrencyExchangeRate");
            this.rawUploadString = dr.GetString("RawUploadString");
            this.rawFreezeFrameDataString = dr.GetString("RawFreezeFrameDataString");
            this.rawMonitorsDataString = dr.GetString("RawMonitorsDataString");

            this.manualDiagnosticReportType = (DiagnosticReportType)dr.GetInt32("ManualDiagnosticReportType");

            this.pwrDiagnosticReportFixStatusWhenCreated = (DiagnosticReportFixStatus)dr.GetInt32("PwrDiagnosticReportFixStatusWhenCreated");
            this.obd1DiagnosticReportFixStatusWhenCreated = (DiagnosticReportFixStatus)dr.GetInt32("Obd1DiagnosticReportFixStatusWhenCreated");
            this.absDiagnosticReportFixStatusWhenCreated = (DiagnosticReportFixStatus)dr.GetInt32("AbsDiagnosticReportFixStatusWhenCreated");
            this.srsDiagnosticReportFixStatusWhenCreated = (DiagnosticReportFixStatus)dr.GetInt32("SrsDiagnosticReportFixStatusWhenCreated");

            this.pwrDiagnosticReportFixStatus = (DiagnosticReportFixStatus)dr.GetInt32("PwrDiagnosticReportFixStatus");
            this.obd1DiagnosticReportFixStatus = (DiagnosticReportFixStatus)dr.GetInt32("Obd1DiagnosticReportFixStatus");
            this.absDiagnosticReportFixStatus = (DiagnosticReportFixStatus)dr.GetInt32("AbsDiagnosticReportFixStatus");
            this.srsDiagnosticReportFixStatus = (DiagnosticReportFixStatus)dr.GetInt32("SrsDiagnosticReportFixStatus");

            this.pwrDiagnosticReportFixCancelReason = dr.GetString("PwrDiagnosticReportFixCancelReason");
            this.obd1DiagnosticReportFixCancelReason = dr.GetString("Obd1DiagnosticReportFixCancelReason");
            this.absDiagnosticReportFixCancelReason = dr.GetString("AbsDiagnosticReportFixCancelReason");
            this.srsDiagnosticReportFixCancelReason = dr.GetString("SrsDiagnosticReportFixCancelReason");

            this.pwrDiagnosticReportFixStatusClosedDateTimeUTC = dr.GetNullableDateTime("PwrDiagnosticReportFixStatusClosedDateTimeUTC");
            this.obd1DiagnosticReportFixStatusClosedDateTimeUTC = dr.GetNullableDateTime("Obd1DiagnosticReportFixStatusClosedDateTimeUTC");
            this.absDiagnosticReportFixStatusClosedDateTimeUTC = dr.GetNullableDateTime("AbsDiagnosticReportFixStatusClosedDateTimeUTC");
            this.srsDiagnosticReportFixStatusClosedDateTimeUTC = dr.GetNullableDateTime("SrsDiagnosticReportFixStatusClosedDateTimeUTC");

            this.pwrFixNotFoundFixPromisedByDateTimeUTC = dr.GetNullableDateTime("PwrFixNotFoundFixPromisedByDateTimeUTC");
            this.obd1FixNotFoundFixPromisedByDateTimeUTC = dr.GetNullableDateTime("Obd1FixNotFoundFixPromisedByDateTimeUTC");
            this.absFixNotFoundFixPromisedByDateTimeUTC = dr.GetNullableDateTime("AbsFixNotFoundFixPromisedByDateTimeUTC");
            this.srsFixNotFoundFixPromisedByDateTimeUTC = dr.GetNullableDateTime("SrsFixNotFoundFixPromisedByDateTimeUTC");

            this.noFixProcessCompletedAndSentDateTimeUTC = dr.GetNullableDateTime("NoFixProcessCompletedAndSentDateTimeUTC");

            this.toolTypeFormat = (ToolTypeFormat)dr.GetInt32("ToolTypeFormat");
            this.softwareType = (SoftwareType)dr.GetInt32("SoftwareType");
            this.softwareVersion = dr.GetString("SoftwareVersion");
            this.firmwareVersion = dr.GetString("FirmwareVersion");
            this.vin = dr.GetString("VIN");
            this.pwrMilCode = dr.GetString("PwrMilCode");               //PrimaryErrorCode
            this.pwrStoredCodesString = dr.GetString("PwrStoredCodesString");       //StoredErrorCodes
            this.pwrPendingCodesString = dr.GetString("PwrPendingCodesString"); //PendingErrorCodes
            this.pwrPermanentCodesString = dr.GetString("PwrPermanentCodesString"); //PermanentErrorCodes
            this.obd1StoredCodesString = dr.GetString("Obd1StoredCodesString"); //New
            this.obd1PendingCodesString = dr.GetString("Obd1PendingCodesString");   //New
            this.absStoredCodesString = dr.GetString("AbsStoredCodesString");       //New
            this.absPendingCodesString = dr.GetString("AbsPendingCodesString"); //New
            this.srsStoredCodesString = dr.GetString("SrsStoredCodesString");       //New
            this.srsPendingCodesString = dr.GetString("SrsPendingCodesString"); //New
            this.enhancedDtcsString = dr.GetString("EnhancedDtcsString");

            this.toolLEDStatus = (ToolLEDStatus)dr.GetInt32("ToolLEDStatus");
            this.toolMilStatus = (ToolMilStatus)dr.GetInt32("ToolMilStatus");

            this.updatedDateTimeUTC = dr.GetDateTime("UpdatedDateTimeUTC");
            this.createdDateTimeUTC = dr.GetDateTime("CreatedDateTimeUTC");
            this.requestedTechnicianContactDateTimeUTC = dr.GetNullableDateTime("RequestedTechnicianContactDateTimeUTC");
            this.requestedTechnicianContactComments = dr.GetString("RequestedTechnicianContactComments");
            this.isManualReport = dr.GetBoolean("IsManualReport");
            this.manualDiagnosticReportType = (DiagnosticReportType)dr.GetInt32("ManualDiagnosticReportType");  //New

            this.isPwrObd1FixFeedbackRequired = dr.GetBoolean("IsPwrObd1FixFeedbackRequired");
            this.isPwrObd2FixFeedbackRequired = dr.GetBoolean("IsPwrObd2FixFeedbackRequired");
            this.isAbsFixFeedbackRequired = dr.GetBoolean("IsAbsFixFeedbackRequired");
            this.isSrsFixFeedbackRequired = dr.GetBoolean("IsSrsFixFeedbackRequired");

            this.masterTechNotificationsSentDateTimeUTC = dr.GetNullableDateTime("MasterTechNotificationsSentDateTimeUTC");
            if (!dr.IsDBNull("PwrIsFixNotificationRequested"))
            {
                this.pwrIsFixNotificationRequested = dr.GetBoolean("PwrIsFixNotificationRequested"); //IsFixNotificationRequested
            }
            if (!dr.IsDBNull("AbsIsFixNotificationRequested"))
            {
                this.absIsFixNotificationRequested = dr.GetBoolean("AbsIsFixNotificationRequested"); //New
            }
            if (!dr.IsDBNull("SrsIsFixNotificationRequested"))
            {
                this.srsIsFixNotificationRequested = dr.GetBoolean("SrsIsFixNotificationRequested"); //New
            }

            this.fixProvidedDateTimeUTC = dr.GetNullableDateTime("FixProvidedDateTimeUTC");
            this.whatFixedMyCarEmailSentDateTimeUTC = dr.GetNullableDateTime("WhatFixedMyCarEmailSentDateTimeUTC");
            this.pastDueEmailSentDateTimeUTC = dr.GetNullableDateTime("PastDueEmailSentDateTimeUTC");

            this.externalSystemReportId = dr.GetString("ExternalSystemReportId");

            //Nam added for new OBDFix support
            if (!dr.IsDBNull("ParentDiagnosticReportId"))
            {
                this.parentDiagnosticReportIdGuid = dr.GetGuid("ParentDiagnosticReportId");
            }
            this.manualRawFreezeFrameDataString = dr.GetSysNullableString("ManualRawFreezeFrameDataString");
            this.additionalHelpRequired = dr.GetBoolean("AdditionalHelpRequired");
            if (!dr.IsDBNull("IsNotifiedRequester"))
            {
                this.isNotifiedRequester = dr.GetBoolean("IsNotifiedRequester");
            }
            this.notifiedRequesterDateTimeUTC = dr.GetNullableDateTime("NotifiedRequesterDateTimeUTC");
            this.notifiedRequesterVia = dr.GetSysNullableString("NotifiedRequesterVia");
            this.note = dr.GetSysNullableString("Note");
            //Nam added for new OBDFix support

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
            //if a result exists, save the result first
            if (this.diagnosticReportResult != null)
            {
                transaction = DiagnosticReportResult.Save(connection, transaction);
            }

            // Call base save method of base class.
            transaction = base.Save(connection, transaction);

            //Custom save business logic here. Modify procedure name.
            if (this.IsObjectDirty)
            {
                transaction = EnsureDatabasePrepared(connection, transaction);

                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    if (this.IsObjectCreated)
                    {
                        dr.ProcedureName = "DiagnosticReport_Create";
                        this.CreatedDateTimeUTC = DateTime.UtcNow;
                        this.UpdatedDateTimeUTC = this.CreatedDateTimeUTC;
                        this.Market = this.User.Market;
                    }
                    else
                    {
                        dr.ProcedureName = "DiagnosticReport_Save";
                        this.UpdatedDateTimeUTC = DateTime.UtcNow;
                    }

                    dr.AddGuid("DiagnosticReportId", this.Id);

                    if (this.DiagnosticReportResult != null)
                    {
                        dr.AddGuid("DiagnosticReportResultId", this.DiagnosticReportResult.Id);
                    }
                    if (this.symptom != null && this.symptom.Id != null)
                    {
                        dr.AddGuid("SymptomId", this.symptom.Id);
                    }

                    dr.AddGuid("UserId", this.User.Id);
                    dr.AddBoolean("HasMasterTechsAssigned", this.HasMasterTechsAssigned);
                    if (this.isMasterTechsAssignedDirty)
                    {
                        this.masterTechsAssignedIdList = this.MasterTechsAssigned.GetAsDelimittedList("|");
                    }
                    dr.AddNVarChar("MasterTechsAssignedIdList", this.masterTechsAssignedIdList);
                    dr.AddDateTime("MasterTechProvideFixFeedbackByOverrideDateTimeUTC", this.MasterTechProvideFixFeedbackByOverrideDateTimeUTC);
                    dr.AddBusinessObject("AdminUserId_PwrWorkingOnFix", this.PwrAdminUserWorkingOnFix);
                    dr.AddDateTime("PwrAdminUserWorkingOnFixAssignedDateTimeUTC", this.PwrAdminUserWorkingOnFixAssignedDateTimeUTC);
                    dr.AddBusinessObject("AdminUserId_AbsWorkingOnFix", this.AbsAdminUserWorkingOnFix);
                    dr.AddDateTime("AbsAdminUserWorkingOnFixAssignedDateTimeUTC", this.AbsAdminUserWorkingOnFixAssignedDateTimeUTC);
                    dr.AddBusinessObject("AdminUserId_SrsWorkingOnFix", this.SrsAdminUserWorkingOnFix);
                    dr.AddDateTime("SrsAdminUserWorkingOnFixAssignedDateTimeUTC", this.SrsAdminUserWorkingOnFixAssignedDateTimeUTC);
                    dr.AddGuid("VehicleId", this.Vehicle.Id);

                    dr.AddGuid("DeviceId", this.Device.Id);

                    dr.AddInt32("PwrDiagnosticReportFixStatusWhenCreated", (int)this.PwrDiagnosticReportFixStatusWhenCreated);
                    dr.AddInt32("Obd1DiagnosticReportFixStatusWhenCreated", (int)this.Obd1DiagnosticReportFixStatusWhenCreated);
                    dr.AddInt32("AbsDiagnosticReportFixStatusWhenCreated", (int)this.AbsDiagnosticReportFixStatusWhenCreated);
                    dr.AddInt32("SrsDiagnosticReportFixStatusWhenCreated", (int)this.SrsDiagnosticReportFixStatusWhenCreated);

                    dr.AddInt32("PwrDiagnosticReportFixStatus", (int)this.PwrDiagnosticReportFixStatus);
                    dr.AddInt32("Obd1DiagnosticReportFixStatus", (int)this.Obd1DiagnosticReportFixStatus);
                    dr.AddInt32("AbsDiagnosticReportFixStatus", (int)this.AbsDiagnosticReportFixStatus);
                    dr.AddInt32("SrsDiagnosticReportFixStatus", (int)this.SrsDiagnosticReportFixStatus);

                    dr.AddNVarChar("PwrDiagnosticReportFixCancelReason", this.PwrDiagnosticReportFixCancelReason);
                    dr.AddNVarChar("Obd1DiagnosticReportFixCancelReason", this.Obd1DiagnosticReportFixCancelReason);
                    dr.AddNVarChar("AbsDiagnosticReportFixCancelReason", this.AbsDiagnosticReportFixCancelReason);
                    dr.AddNVarChar("SrsDiagnosticReportFixCancelReason", this.SrsDiagnosticReportFixCancelReason);

                    dr.AddDateTime("PwrDiagnosticReportFixStatusClosedDateTimeUTC", this.PwrDiagnosticReportFixStatusClosedDateTimeUTC);
                    dr.AddDateTime("Obd1DiagnosticReportFixStatusClosedDateTimeUTC", this.Obd1DiagnosticReportFixStatusClosedDateTimeUTC);
                    dr.AddDateTime("AbsDiagnosticReportFixStatusClosedDateTimeUTC", this.AbsDiagnosticReportFixStatusClosedDateTimeUTC);
                    dr.AddDateTime("SrsDiagnosticReportFixStatusClosedDateTimeUTC", this.SrsDiagnosticReportFixStatusClosedDateTimeUTC);

                    // The SearchNoFixReports method sets these dates to MaxValue for sorting purposes so set back to null if necessary.
                    if (this.PwrFixNotFoundFixPromisedByDateTimeUTC == DateTime.MaxValue)
                    {
                        this.PwrFixNotFoundFixPromisedByDateTimeUTC = NullableDateTime.Null;
                    }
                    if (this.Obd1FixNotFoundFixPromisedByDateTimeUTC == DateTime.MaxValue)
                    {
                        this.Obd1FixNotFoundFixPromisedByDateTimeUTC = NullableDateTime.Null;
                    }
                    if (this.AbsFixNotFoundFixPromisedByDateTimeUTC == DateTime.MaxValue)
                    {
                        this.AbsFixNotFoundFixPromisedByDateTimeUTC = NullableDateTime.Null;
                    }
                    if (this.SrsFixNotFoundFixPromisedByDateTimeUTC == DateTime.MaxValue)
                    {
                        this.SrsFixNotFoundFixPromisedByDateTimeUTC = NullableDateTime.Null;
                    }
                    dr.AddDateTime("PwrFixNotFoundFixPromisedByDateTimeUTC", this.PwrFixNotFoundFixPromisedByDateTimeUTC);
                    dr.AddDateTime("Obd1FixNotFoundFixPromisedByDateTimeUTC", this.Obd1FixNotFoundFixPromisedByDateTimeUTC);
                    dr.AddDateTime("AbsFixNotFoundFixPromisedByDateTimeUTC", this.AbsFixNotFoundFixPromisedByDateTimeUTC);
                    dr.AddDateTime("SrsFixNotFoundFixPromisedByDateTimeUTC", this.SrsFixNotFoundFixPromisedByDateTimeUTC);

                    dr.AddDateTime("NoFixProcessCompletedAndSentDateTimeUTC", this.NoFixProcessCompletedAndSentDateTimeUTC);

                    dr.AddNVarChar("RepairOrderNumber", this.RepairOrderNumber);
                    dr.AddInt32("VehicleMileage", this.VehicleMileage);
                    dr.AddInt32("Market", (int)this.Market);
                    dr.AddInt32("Language", (int)this.Language);
                    dr.AddInt32("Currency", (int)this.Currency);
                    dr.AddDecimal("CurrencyExchangeRate", this.CurrencyExchangeRate);
                    dr.AddNText("RawUploadString", this.RawUploadString);
                    dr.AddNVarChar("RawFreezeFrameDataString", this.RawFreezeFrameDataString);
                    dr.AddNVarChar("RawMonitorsDataString", this.RawMonitorsDataString);

                    dr.AddInt32("SoftwareType", (int)this.SoftwareType);
                    dr.AddInt32("ToolTypeFormat", (int)this.ToolTypeFormat);
                    dr.AddNVarChar("SoftwareVersion", this.SoftwareVersion);
                    dr.AddNVarChar("FirmwareVersion", this.FirmwareVersion);

                    dr.AddNVarChar("VIN", this.VIN);
                    dr.AddNVarChar("PwrMilCode", this.PwrMilCode);
                    dr.AddNVarChar("PwrStoredCodesString", this.GetCommaSeparatedListFromStringCollection(this.PwrStoredCodes));
                    dr.AddNVarChar("PwrPendingCodesString", this.GetCommaSeparatedListFromStringCollection(this.PwrPendingCodes));
                    dr.AddNVarChar("PwrPermanentCodesString", this.GetCommaSeparatedListFromStringCollection(this.PwrPermanentCodes));
                    dr.AddNVarChar("Obd1StoredCodesString", this.GetDelimitedStringFromDtcCodeViewCollection(this.Obd1StoredCodes));
                    dr.AddNVarChar("Obd1PendingCodesString", this.GetDelimitedStringFromDtcCodeViewCollection(this.Obd1PendingCodes));
                    dr.AddNVarChar("AbsStoredCodesString", this.GetDelimitedStringFromDtcCodeViewCollection(this.AbsStoredCodes));
                    dr.AddNVarChar("AbsPendingCodesString", this.GetDelimitedStringFromDtcCodeViewCollection(this.AbsPendingCodes));
                    dr.AddNVarChar("SrsStoredCodesString", this.GetDelimitedStringFromDtcCodeViewCollection(this.SrsStoredCodes));
                    dr.AddNVarChar("SrsPendingCodesString", this.GetDelimitedStringFromDtcCodeViewCollection(this.SrsPendingCodes));
                    dr.AddNVarChar("EnhancedDtcsString", this.GetDelimitedStringFromDtcCodeViewCollection(this.EnhancedDtcs));

                    dr.AddInt32("ToolLEDStatus", (int)this.ToolLEDStatus);
                    dr.AddInt32("ToolMilStatus", (int)this.ToolMilStatus);

                    dr.AddDateTime("UpdatedDateTimeUTC", this.UpdatedDateTimeUTC);
                    dr.AddDateTime("CreatedDateTimeUTC", this.CreatedDateTimeUTC);

                    dr.AddDateTime("RequestedTechnicianContactDateTimeUTC", this.requestedTechnicianContactDateTimeUTC);

                    dr.AddNText("RequestedTechnicianContactComments", this.RequestedTechnicianContactComments);
                    dr.AddBoolean("IsManualReport", this.IsManualReport);
                    dr.AddInt32("ManualDiagnosticReportType", (int)this.ManualDiagnosticReportType);

                    dr.AddBoolean("IsPwrObd1FixFeedbackRequired", this.IsPwrObd1FixFeedbackRequired);
                    dr.AddBoolean("IsPwrObd2FixFeedbackRequired", this.IsPwrObd2FixFeedbackRequired);
                    dr.AddBoolean("IsAbsFixFeedbackRequired", this.IsAbsFixFeedbackRequired);
                    dr.AddBoolean("IsSrsFixFeedbackRequired", this.IsSrsFixFeedbackRequired);

                    dr.AddDateTime("MasterTechNotificationsSentDateTimeUTC", this.MasterTechNotificationsSentDateTimeUTC);
                    if (this.PwrIsFixNotificationRequested.HasValue)
                    {
                        dr.AddBoolean("PwrIsFixNotificationRequested", this.PwrIsFixNotificationRequested.Value);
                    }
                    if (this.AbsIsFixNotificationRequested.HasValue)
                    {
                        dr.AddBoolean("AbsIsFixNotificationRequested", this.AbsIsFixNotificationRequested.Value); //New
                    }
                    if (this.SrsIsFixNotificationRequested.HasValue)
                    {
                        dr.AddBoolean("SrsIsFixNotificationRequested", this.SrsIsFixNotificationRequested.Value); //New
                    }

                    dr.AddDateTime("FixProvidedDateTimeUTC", this.FixProvidedDateTimeUTC);
                    dr.AddDateTime("WhatFixedMyCarEmailSentDateTimeUTC", this.WhatFixedMyCarEmailSentDateTimeUTC);
                    dr.AddDateTime("PastDueEmailSentDateTimeUTC", this.PastDueEmailSentDateTimeUTC);

                    dr.AddNVarChar("ExternalSystemReportId", this.ExternalSystemReportId);

                    //Nam added for new OBDFix support 1/10/2017
                    if (this.ParentDiagnosticReportIdGuid.HasValue)
                    {
                        dr.AddGuid("ParentDiagnosticReportId", this.ParentDiagnosticReportIdGuid);
                    }
                    dr.AddNVarChar("ManualRawFreezeFrameDataString", this.ManualRawFreezeFrameDataString);
                    dr.AddBoolean("AdditionalHelpRequired", this.AdditionalHelpRequired);
                    if (this.IsNotifiedRequester.HasValue)
                    {
                        dr.AddBoolean("IsNotifiedRequester", this.IsNotifiedRequester);
                    }
                    dr.AddDateTime("NotifiedRequesterDateTimeUTC", this.NotifiedRequesterDateTimeUTC);
                    dr.AddVarChar("NotifiedRequesterVia", this.NotifiedRequesterVia);
                    dr.AddNVarChar("Note", this.Note);
                    //Nam added for new OBDFix support 1/10/2017

                    dr.SqlProcedureCommand.SqlCommand.CommandTimeout = 0; //Added on 2018-01-12 2:19 PM by INNOVA Dev Team to increase the timeout wait due to a heavy load of the No-Fix Report data.
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
            // Save the DiagnosticReportFeedbacks collection
            if (this.diagnosticReportFeedbacks != null)
            {
                foreach (DiagnosticReportFeedback fb in this.DiagnosticReportFeedbacks.Removed)
                {
                    transaction = fb.Delete(connection, transaction);
                }

                foreach (DiagnosticReportFeedback fb in this.DiagnosticReportFeedbacks)
                {
                    transaction = fb.Save(connection, transaction);
                }
            }

            if (this.isMasterTechsAssignedDirty)
            {
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    dr.ProcedureName = "DiagnosticReport_SaveAssignedMasterTechs";
                    dr.AddGuid("DiagnosticReportId", Id);
                    dr.AddNText("XmlList", this.MasterTechsAssigned.ToXmlGuidList());
                    dr.ExecuteNonQuery(transaction);
                }
                this.isMasterTechsAssignedDirty = false;
            }

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
            EnsureValidId();

            transaction = this.EnsureDatabasePrepared(connection, transaction);

            // Custom delete business logic here.

            // delete the DiagnosticReportFeedback objects
            foreach (DiagnosticReportFeedback fb in this.DiagnosticReportFeedbacks)
            {
                transaction = fb.Delete(connection, transaction);
            }

            // delete this current object
            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                //delete the item
                dr.ProcedureName = "DiagnosticReport_Delete";
                dr.AddGuid("DiagnosticReportId", this.Id);

                dr.ExecuteNonQuery(transaction);
            }

            transaction = base.Delete(connection, transaction);

            return transaction;
        }

        #endregion Required Object Methods (Load, Save, Delete, Etc)
    }
}
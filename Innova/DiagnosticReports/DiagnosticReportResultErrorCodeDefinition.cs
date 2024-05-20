using Innova.DtcCodes;
using Innova.Vehicles;
using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using System;
using System.Data.SqlClient;

namespace Innova.DiagnosticReports
{
    /// <summary>
    /// The DiagnosticReportResultErrorCodeDefinition object handles the business logic and data access for the specialized business object, DiagnosticReportResultErrorCodeDefinition.
    /// The class inherits from <see cref="BusinessObjectBase"/> class.
    /// </summary>
    /// <example>
    /// See the examples of how to construct the DiagnosticReportResultErrorCodeDefinition object.
    ///
    /// To create a new instance of a new of DiagnosticReportResultErrorCodeDefinition.
    /// <code>DiagnosticReportResultErrorCodeDefinition o = (DiagnosticReportResultErrorCodeDefinition)Registry.CreateInstance(typeof(DiagnosticReportResultErrorCodeDefinition));</code>
    ///
    /// To create an new instance of an existing DiagnosticReportResultErrorCodeDefinition.
    /// <code>DiagnosticReportResultErrorCodeDefinition o = (DiagnosticReportResultErrorCodeDefinition)Registry.CreateInstance(typeof(DiagnosticReportResultErrorCodeDefinition), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of DiagnosticReportResultErrorCodeDefinition, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectBase.ConnectionString"/> to the <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable()]
    public class DiagnosticReportResultErrorCodeDefinition : InnovaBusinessObjectBase
    {
        // data object variables
        private DiagnosticReportResultErrorCode diagnosticReportResultErrorCode;

        private string title = "";
        private string title_es = "";
        private string title_fr = "";
        private string title_zh = "";
        private string conditions = "";
        private string conditions_es = "";
        private string conditions_fr = "";
        private string conditions_zh = "";
        private string possibleCauses = "";
        private string possibleCauses_es = "";
        private string possibleCauses_fr = "";
        private string possibleCauses_zh = "";
        private int trips;
        private string messageIndicatorLampFile = "";
        private string transmissionControlIndicatorLampFile = "";
        private string passiveAntiTheftIndicatorLampFile = "";
        private string serviceThrottleSoonIndicatorLampFile = "";
        private string monitorType = "";
        private string monitorFile = "";
        private DtcMasterCodeList dTCMasterCode;
        private DtcCode dTCCode;
        private VehicleTypeCode vehicleTypeCode;
        private VehicleType vehicleType;
        private string make = "";
        private string model = "";
        private int year = 0;
        private string bodyCode = "";
        private string engineType = "";
        private string engineVINCode = "";
        private string engineTypeVINLookup = "";
        private string transmissionControlType = "";

        private string laymansTermTitle = "";
        private string laymansTermTitle_es = "";
        private string laymansTermTitle_fr = "";
        private string laymansTermTitle_zh = "";
        private string laymansTermDescription = "";
        private string laymansTermDescription_es = "";
        private string laymansTermDescription_fr = "";
        private string laymansTermDescription_zh = "";
        private string laymansTermConditions = "";
        private string laymansTermConditions_es = "";
        private string laymansTermConditions_fr = "";
        private string laymansTermConditions_zh = "";
        private int laymansTermSeverityLevel;
        private string laymansTermEffectOnVehicle = "";
        private string laymansTermEffectOnVehicle_es = "";
        private string laymansTermEffectOnVehicle_fr = "";
        private string laymansTermEffectOnVehicle_zh = "";
        private string laymansTermResponsibleComponentOrSystem = "";
        private string laymansTermResponsibleComponentOrSystem_es = "";
        private string laymansTermResponsibleComponentOrSystem_fr = "";
        private string laymansTermResponsibleComponentOrSystem_zh = "";
        private string laymansTermWhyItsImportant = "";
        private string laymansTermWhyItsImportant_es = "";
        private string laymansTermWhyItsImportant_fr = "";
        private string laymansTermWhyItsImportant_zh = "";
        private string laymansTermSeverityLevelDefinition = "";
        private string laymansTermSeverityLevelDefinition_es = "";
        private string laymansTermSeverityLevelDefinition_fr = "";
        private string laymansTermSeverityLevelDefinition_zh = "";

        #region System Constructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new (one that does not exist, assigning a new <see cref="Guid"/> Id). DiagnosticReportResultErrorCodeDefinition object.
        /// In order to create a new DiagnosticReportResultErrorCodeDefinition which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <example>
        /// <code>
        /// DiagnosticReportResultErrorCodeDefinition o = (DiagnosticReportResultErrorCodeDefinition)Registry.CreateInstance(typeof(DiagnosticReportResultErrorCodeDefinition));
        /// </code>
        /// </example>
        protected internal DiagnosticReportResultErrorCodeDefinition() : base()
        {
            IsObjectCreated = true;
            IsObjectLoaded = true;
            IsObjectDirty = true;
        }

        /// <summary>
        /// Constructor creates an existing (one that exists, and already has a <see cref="Guid"/> Id).  DiagnosticReportResultErrorCodeDefinition object.
        /// In order to create an existing DiagnosticReportResultErrorCodeDefinition object, which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <param name="id">The <see cref="Guid"/> or SQL uniqueidentifier indentification "Id" of the current object.</param>
        /// <example>
        /// <code>
        /// DiagnosticReportResultErrorCodeDefinition o = (DiagnosticReportResultErrorCodeDefinition)Registry.CreateInstance(typeof(DiagnosticReportResultErrorCodeDefinition), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));
        /// </code>
        /// </example>
        protected internal DiagnosticReportResultErrorCodeDefinition(Guid id) : base(id)
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
        /// Gets or sets the <see cref="DiagnosticReportResultErrorCode"/>
        /// </summary>
        public DiagnosticReportResultErrorCode DiagnosticReportResultErrorCode
        {
            get
            {
                EnsureLoaded();
                return diagnosticReportResultErrorCode;
            }
            set
            {
                EnsureLoaded();
                if (diagnosticReportResultErrorCode != value)
                {
                    IsObjectDirty = true;
                    diagnosticReportResultErrorCode = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        public string Title
        {
            get
            {
                this.EnsureLoaded();
                return this.title;
            }
            set
            {
                this.EnsureLoaded();
                if (this.title != value)
                {
                    this.IsObjectDirty = true;
                    this.title = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        public string Title_es
        {
            get
            {
                this.EnsureLoaded();
                return this.title_es;
            }
            set
            {
                this.EnsureLoaded();
                if (this.title_es != value)
                {
                    this.IsObjectDirty = true;
                    this.title_es = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        public string Title_fr
        {
            get
            {
                this.EnsureLoaded();
                return this.title_fr;
            }
            set
            {
                this.EnsureLoaded();
                if (this.title_fr != value)
                {
                    this.IsObjectDirty = true;
                    this.title_fr = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        public string Title_zh
        {
            get
            {
                this.EnsureLoaded();
                return this.title_zh;
            }
            set
            {
                this.EnsureLoaded();
                if (this.title_zh != value)
                {
                    this.IsObjectDirty = true;
                    this.title_zh = value;
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> title in the language specified in the Registry.
        /// </summary>
        public string Title_Translated
        {
            get
            {
                return this.RuntimeInfo.GetTranslatedValue(this.Title, this.Title_es, this.Title_fr, this.Title_zh);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        public string Conditions
        {
            get
            {
                this.EnsureLoaded();
                return this.conditions;
            }
            set
            {
                this.EnsureLoaded();
                if (this.conditions != value)
                {
                    this.IsObjectDirty = true;
                    this.conditions = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        public string Conditions_es
        {
            get
            {
                this.EnsureLoaded();
                return this.conditions_es;
            }
            set
            {
                this.EnsureLoaded();
                if (this.conditions_es != value)
                {
                    this.IsObjectDirty = true;
                    this.conditions_es = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        public string Conditions_fr
        {
            get
            {
                this.EnsureLoaded();
                return this.conditions_fr;
            }
            set
            {
                this.EnsureLoaded();
                if (this.conditions_fr != value)
                {
                    this.IsObjectDirty = true;
                    this.conditions_fr = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        public string Conditions_zh
        {
            get
            {
                this.EnsureLoaded();
                return this.conditions_zh;
            }
            set
            {
                this.EnsureLoaded();
                if (this.conditions_zh != value)
                {
                    this.IsObjectDirty = true;
                    this.conditions_zh = value;
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> conditions in the language specified in the Registry.
        /// </summary>
        public string Conditions_Translated
        {
            get
            {
                return this.RuntimeInfo.GetTranslatedValue(this.Conditions, this.Conditions_es, this.Conditions_fr, this.Conditions_zh);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        public string PossibleCauses
        {
            get
            {
                this.EnsureLoaded();
                return this.possibleCauses;
            }
            set
            {
                this.EnsureLoaded();
                if (this.possibleCauses != value)
                {
                    this.IsObjectDirty = true;
                    this.possibleCauses = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        public string PossibleCauses_es
        {
            get
            {
                this.EnsureLoaded();
                return this.possibleCauses_es;
            }
            set
            {
                this.EnsureLoaded();
                if (this.possibleCauses_es != value)
                {
                    this.IsObjectDirty = true;
                    this.possibleCauses_es = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        public string PossibleCauses_fr
        {
            get
            {
                this.EnsureLoaded();
                return this.possibleCauses_fr;
            }
            set
            {
                this.EnsureLoaded();
                if (this.possibleCauses_fr != value)
                {
                    this.IsObjectDirty = true;
                    this.possibleCauses_fr = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        public string PossibleCauses_zh
        {
            get
            {
                this.EnsureLoaded();
                return this.possibleCauses_zh;
            }
            set
            {
                this.EnsureLoaded();
                if (this.possibleCauses_zh != value)
                {
                    this.IsObjectDirty = true;
                    this.possibleCauses_zh = value;
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> possible causes in the language specified in the Registry.
        /// </summary>
        public string PossibleCauses_Translated
        {
            get
            {
                return this.RuntimeInfo.GetTranslatedValue(this.PossibleCauses, this.PossibleCauses_es, this.PossibleCauses_fr, this.PossibleCauses_zh);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/>
        /// </summary>
        public int Trips
        {
            get
            {
                EnsureLoaded();
                return trips;
            }
            set
            {
                EnsureLoaded();
                if (trips != value)
                {
                    IsObjectDirty = true;
                    trips = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        public string MessageIndicatorLampFile
        {
            get
            {
                EnsureLoaded();
                return messageIndicatorLampFile;
            }
            set
            {
                EnsureLoaded();
                if (messageIndicatorLampFile != value)
                {
                    IsObjectDirty = true;
                    messageIndicatorLampFile = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        public string TransmissionControlIndicatorLampFile
        {
            get
            {
                EnsureLoaded();
                return transmissionControlIndicatorLampFile;
            }
            set
            {
                EnsureLoaded();
                if (transmissionControlIndicatorLampFile != value)
                {
                    IsObjectDirty = true;
                    transmissionControlIndicatorLampFile = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        public string PassiveAntiTheftIndicatorLampFile
        {
            get
            {
                EnsureLoaded();
                return passiveAntiTheftIndicatorLampFile;
            }
            set
            {
                EnsureLoaded();
                if (passiveAntiTheftIndicatorLampFile != value)
                {
                    IsObjectDirty = true;
                    passiveAntiTheftIndicatorLampFile = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        public string ServiceThrottleSoonIndicatorLampFile
        {
            get
            {
                EnsureLoaded();
                return serviceThrottleSoonIndicatorLampFile;
            }
            set
            {
                EnsureLoaded();
                if (serviceThrottleSoonIndicatorLampFile != value)
                {
                    IsObjectDirty = true;
                    serviceThrottleSoonIndicatorLampFile = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        public string MonitorType
        {
            get
            {
                EnsureLoaded();
                return monitorType;
            }
            set
            {
                EnsureLoaded();
                if (monitorType != value)
                {
                    IsObjectDirty = true;
                    monitorType = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        public string MonitorFile
        {
            get
            {
                EnsureLoaded();
                return monitorFile;
            }
            set
            {
                EnsureLoaded();
                if (monitorFile != value)
                {
                    IsObjectDirty = true;
                    monitorFile = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DTCMasterCode"/>
        /// </summary>
        public DtcMasterCodeList DTCMasterCode
        {
            get
            {
                EnsureLoaded();
                return dTCMasterCode;
            }
            set
            {
                EnsureLoaded();
                if (dTCMasterCode != value)
                {
                    IsObjectDirty = true;
                    dTCMasterCode = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DTCCode"/>
        /// </summary>
        public DtcCode DTCCode
        {
            get
            {
                EnsureLoaded();
                return dTCCode;
            }
            set
            {
                EnsureLoaded();
                if (dTCCode != value)
                {
                    IsObjectDirty = true;
                    dTCCode = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="VehicleTypeCode"/>
        /// </summary>
        public VehicleTypeCode VehicleTypeCode
        {
            get
            {
                EnsureLoaded();
                return vehicleTypeCode;
            }
            set
            {
                EnsureLoaded();
                if (vehicleTypeCode != value)
                {
                    IsObjectDirty = true;
                    vehicleTypeCode = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="VehicleType"/>
        /// </summary>
        public VehicleType VehicleType
        {
            get
            {
                EnsureLoaded();
                return vehicleType;
            }
            set
            {
                EnsureLoaded();
                if (vehicleType != value)
                {
                    IsObjectDirty = true;
                    vehicleType = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        public string Make
        {
            get
            {
                EnsureLoaded();
                return make;
            }
            set
            {
                EnsureLoaded();
                if (make != value)
                {
                    IsObjectDirty = true;
                    make = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        public string Model
        {
            get
            {
                EnsureLoaded();
                return model;
            }
            set
            {
                EnsureLoaded();
                if (model != value)
                {
                    IsObjectDirty = true;
                    model = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        public int Year
        {
            get
            {
                EnsureLoaded();
                return year;
            }
            set
            {
                EnsureLoaded();
                if (year != value)
                {
                    IsObjectDirty = true;
                    year = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        public string BodyCode
        {
            get
            {
                EnsureLoaded();
                return bodyCode;
            }
            set
            {
                EnsureLoaded();
                if (bodyCode != value)
                {
                    IsObjectDirty = true;
                    bodyCode = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        public string EngineType
        {
            get
            {
                EnsureLoaded();
                return engineType;
            }
            set
            {
                EnsureLoaded();
                if (engineType != value)
                {
                    IsObjectDirty = true;
                    engineType = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        public string EngineVINCode
        {
            get
            {
                EnsureLoaded();
                return engineVINCode;
            }
            set
            {
                EnsureLoaded();
                if (engineVINCode != value)
                {
                    IsObjectDirty = true;
                    engineVINCode = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        public string EngineTypeVINLookup
        {
            get
            {
                EnsureLoaded();
                return engineTypeVINLookup;
            }
            set
            {
                EnsureLoaded();
                if (engineTypeVINLookup != value)
                {
                    IsObjectDirty = true;
                    engineTypeVINLookup = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        public string TransmissionControlType
        {
            get
            {
                EnsureLoaded();
                return transmissionControlType;
            }
            set
            {
                EnsureLoaded();
                if (transmissionControlType != value)
                {
                    IsObjectDirty = true;
                    transmissionControlType = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> layman's term for title.
        /// </summary>
        public string LaymansTermTitle
        {
            get
            {
                this.EnsureLoaded();
                return this.laymansTermTitle;
            }
            set
            {
                this.EnsureLoaded();
                if (this.laymansTermTitle != value)
                {
                    this.IsObjectDirty = true;
                    this.laymansTermTitle = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> layman's term for title in Spanish.
        /// </summary>
        public string LaymansTermTitle_es
        {
            get
            {
                this.EnsureLoaded();
                return this.laymansTermTitle_es;
            }
            set
            {
                this.EnsureLoaded();
                if (this.laymansTermTitle_es != value)
                {
                    this.IsObjectDirty = true;
                    this.laymansTermTitle_es = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> layman's term for title in French.
        /// </summary>
        public string LaymansTermTitle_fr
        {
            get
            {
                this.EnsureLoaded();
                return this.laymansTermTitle_fr;
            }
            set
            {
                this.EnsureLoaded();
                if (this.laymansTermTitle_fr != value)
                {
                    this.IsObjectDirty = true;
                    this.laymansTermTitle_fr = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> layman's term for title in Mandarin Chinese.
        /// </summary>
        public string LaymansTermTitle_zh
        {
            get
            {
                this.EnsureLoaded();
                return this.laymansTermTitle_zh;
            }
            set
            {
                this.EnsureLoaded();
                if (this.laymansTermTitle_zh != value)
                {
                    this.IsObjectDirty = true;
                    this.laymansTermTitle_zh = value;
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> layman's term for title in the language specified in the Registry.
        /// </summary>
        public string LaymansTermTitle_Translated
        {
            get
            {
                return this.RuntimeInfo.GetTranslatedValue(this.LaymansTermTitle, this.LaymansTermTitle_es, this.LaymansTermTitle_fr, this.LaymansTermTitle_zh);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> layman's term for description.
        /// </summary>
        public string LaymansTermDescription
        {
            get
            {
                this.EnsureLoaded();
                return this.laymansTermDescription;
            }
            set
            {
                this.EnsureLoaded();
                if (this.laymansTermDescription != value)
                {
                    this.IsObjectDirty = true;
                    this.laymansTermDescription = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> layman's term for description in Spanish.
        /// </summary>
        public string LaymansTermDescription_es
        {
            get
            {
                this.EnsureLoaded();
                return this.laymansTermDescription_es;
            }
            set
            {
                this.EnsureLoaded();
                if (this.laymansTermDescription_es != value)
                {
                    this.IsObjectDirty = true;
                    this.laymansTermDescription_es = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> layman's term for description in French.
        /// </summary>
        public string LaymansTermDescription_fr
        {
            get
            {
                this.EnsureLoaded();
                return this.laymansTermDescription_fr;
            }
            set
            {
                this.EnsureLoaded();
                if (this.laymansTermDescription_fr != value)
                {
                    this.IsObjectDirty = true;
                    this.laymansTermDescription_fr = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> layman's term for description in Mandarin Chinese.
        /// </summary>
        public string LaymansTermDescription_zh
        {
            get
            {
                this.EnsureLoaded();
                return this.laymansTermDescription_zh;
            }
            set
            {
                this.EnsureLoaded();
                if (this.laymansTermDescription_zh != value)
                {
                    this.IsObjectDirty = true;
                    this.laymansTermDescription_zh = value;
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> layman's term for description in the language specified in the Registry.
        /// </summary>
        public string LaymansTermDescription_Translated
        {
            get
            {
                return this.RuntimeInfo.GetTranslatedValue(this.LaymansTermDescription, this.LaymansTermDescription_es, this.LaymansTermDescription_fr, this.LaymansTermDescription_zh);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> layman's term for conditions.
        /// </summary>
        public string LaymansTermConditions
        {
            get
            {
                this.EnsureLoaded();
                return this.laymansTermConditions;
            }
            set
            {
                this.EnsureLoaded();
                if (this.laymansTermConditions != value)
                {
                    this.IsObjectDirty = true;
                    this.laymansTermConditions = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> layman's term for conditions in Spanish.
        /// </summary>
        public string LaymansTermConditions_es
        {
            get
            {
                this.EnsureLoaded();
                return this.laymansTermConditions_es;
            }
            set
            {
                this.EnsureLoaded();
                if (this.laymansTermConditions_es != value)
                {
                    this.IsObjectDirty = true;
                    this.laymansTermConditions_es = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> layman's term for conditions in French.
        /// </summary>
        public string LaymansTermConditions_fr
        {
            get
            {
                this.EnsureLoaded();
                return this.laymansTermConditions_fr;
            }
            set
            {
                this.EnsureLoaded();
                if (this.laymansTermConditions_fr != value)
                {
                    this.IsObjectDirty = true;
                    this.laymansTermConditions_fr = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> layman's term for conditions in Mandarin Chinese.
        /// </summary>
        public string LaymansTermConditions_zh
        {
            get
            {
                this.EnsureLoaded();
                return this.laymansTermConditions_zh;
            }
            set
            {
                this.EnsureLoaded();
                if (this.laymansTermConditions_zh != value)
                {
                    this.IsObjectDirty = true;
                    this.laymansTermConditions_zh = value;
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> layman's term for conditions in the language specified in the Registry.
        /// </summary>
        public string LaymansTermConditions_Translated
        {
            get
            {
                return this.RuntimeInfo.GetTranslatedValue(this.LaymansTermConditions, this.LaymansTermConditions_es, this.LaymansTermConditions_fr, this.LaymansTermConditions_zh);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/> layman's term for severity level.
        /// </summary>
        public int LaymansTermSeverityLevel
        {
            get
            {
                this.EnsureLoaded();
                return this.laymansTermSeverityLevel;
            }
            set
            {
                this.EnsureLoaded();
                if (this.laymansTermSeverityLevel != value)
                {
                    this.IsObjectDirty = true;
                    this.laymansTermSeverityLevel = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> layman's term for effect on the vehicle.
        /// </summary>
        public string LaymansTermEffectOnVehicle
        {
            get
            {
                this.EnsureLoaded();
                return this.laymansTermEffectOnVehicle;
            }
            set
            {
                this.EnsureLoaded();
                if (this.laymansTermEffectOnVehicle != value)
                {
                    this.IsObjectDirty = true;
                    this.laymansTermEffectOnVehicle = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> layman's term for effect on the vehicle in Spanish.
        /// </summary>
        public string LaymansTermEffectOnVehicle_es
        {
            get
            {
                this.EnsureLoaded();
                return this.laymansTermEffectOnVehicle_es;
            }
            set
            {
                this.EnsureLoaded();
                if (this.laymansTermEffectOnVehicle_es != value)
                {
                    this.IsObjectDirty = true;
                    this.laymansTermEffectOnVehicle_es = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> layman's term for effect on the vehicle in French.
        /// </summary>
        public string LaymansTermEffectOnVehicle_fr
        {
            get
            {
                this.EnsureLoaded();
                return this.laymansTermEffectOnVehicle_fr;
            }
            set
            {
                this.EnsureLoaded();
                if (this.laymansTermEffectOnVehicle_fr != value)
                {
                    this.IsObjectDirty = true;
                    this.laymansTermEffectOnVehicle_fr = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> layman's term for effect on the vehicle in Chinese.
        /// </summary>
        public string LaymansTermEffectOnVehicle_zh
        {
            get
            {
                this.EnsureLoaded();
                return this.laymansTermEffectOnVehicle_zh;
            }
            set
            {
                this.EnsureLoaded();
                if (this.laymansTermEffectOnVehicle_zh != value)
                {
                    this.IsObjectDirty = true;
                    this.laymansTermEffectOnVehicle_zh = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> layman's term for effect on the vehicle in the language specified in the <see cref="RuntimeInfo"/>.
        /// </summary>
        public string LaymansTermEffectOnVehicle_Translated
        {
            get
            {
                this.EnsureLoaded();
                return this.RuntimeInfo.GetTranslatedValue(this.LaymansTermEffectOnVehicle, this.LaymansTermEffectOnVehicle_es, this.LaymansTermEffectOnVehicle_fr, this.LaymansTermEffectOnVehicle_zh);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> layman's term for responsible component or system.
        /// </summary>
        public string LaymansTermResponsibleComponentOrSystem
        {
            get
            {
                this.EnsureLoaded();
                return this.laymansTermResponsibleComponentOrSystem;
            }
            set
            {
                this.EnsureLoaded();
                if (this.laymansTermResponsibleComponentOrSystem != value)
                {
                    this.IsObjectDirty = true;
                    this.laymansTermResponsibleComponentOrSystem = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> layman's term for responsible component or system in Spanish.
        /// </summary>
        public string LaymansTermResponsibleComponentOrSystem_es
        {
            get
            {
                this.EnsureLoaded();
                return this.laymansTermResponsibleComponentOrSystem_es;
            }
            set
            {
                this.EnsureLoaded();
                if (this.laymansTermResponsibleComponentOrSystem_es != value)
                {
                    this.IsObjectDirty = true;
                    this.laymansTermResponsibleComponentOrSystem_es = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> layman's term for responsible component or system in French.
        /// </summary>
        public string LaymansTermResponsibleComponentOrSystem_fr
        {
            get
            {
                this.EnsureLoaded();
                return this.laymansTermResponsibleComponentOrSystem_fr;
            }
            set
            {
                this.EnsureLoaded();
                if (this.laymansTermResponsibleComponentOrSystem_fr != value)
                {
                    this.IsObjectDirty = true;
                    this.laymansTermResponsibleComponentOrSystem_fr = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> layman's term for responsible component or system in Chinese.
        /// </summary>
        public string LaymansTermResponsibleComponentOrSystem_zh
        {
            get
            {
                this.EnsureLoaded();
                return this.laymansTermResponsibleComponentOrSystem_zh;
            }
            set
            {
                this.EnsureLoaded();
                if (this.laymansTermResponsibleComponentOrSystem_zh != value)
                {
                    this.IsObjectDirty = true;
                    this.laymansTermResponsibleComponentOrSystem_zh = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> layman's term for responsible component or system in the language specified in the <see cref="RuntimeInfo"/>.
        /// </summary>
        public string LaymansTermResponsibleComponentOrSystem_Translated
        {
            get
            {
                this.EnsureLoaded();
                return this.RuntimeInfo.GetTranslatedValue(this.LaymansTermResponsibleComponentOrSystem, this.LaymansTermResponsibleComponentOrSystem_es, this.LaymansTermResponsibleComponentOrSystem_fr, this.LaymansTermResponsibleComponentOrSystem_zh);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> layman's term for describing why the component or system is important.
        /// </summary>
        public string LaymansTermWhyItsImportant
        {
            get
            {
                this.EnsureLoaded();
                return this.laymansTermWhyItsImportant;
            }
            set
            {
                this.EnsureLoaded();
                if (this.laymansTermWhyItsImportant != value)
                {
                    this.IsObjectDirty = true;
                    this.laymansTermWhyItsImportant = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> layman's term for describing why the component or system is important in Spanish.
        /// </summary>
        public string LaymansTermWhyItsImportant_es
        {
            get
            {
                this.EnsureLoaded();
                return this.laymansTermWhyItsImportant_es;
            }
            set
            {
                this.EnsureLoaded();
                if (this.laymansTermWhyItsImportant_es != value)
                {
                    this.IsObjectDirty = true;
                    this.laymansTermWhyItsImportant_es = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> layman's term for describing why the component or system is important in French.
        /// </summary>
        public string LaymansTermWhyItsImportant_fr
        {
            get
            {
                this.EnsureLoaded();
                return this.laymansTermWhyItsImportant_fr;
            }
            set
            {
                this.EnsureLoaded();
                if (this.laymansTermWhyItsImportant_fr != value)
                {
                    this.IsObjectDirty = true;
                    this.laymansTermWhyItsImportant_fr = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> layman's term for describing why the component or system is important in Chinese.
        /// </summary>
        public string LaymansTermWhyItsImportant_zh
        {
            get
            {
                this.EnsureLoaded();
                return this.laymansTermWhyItsImportant_zh;
            }
            set
            {
                this.EnsureLoaded();
                if (this.laymansTermWhyItsImportant_zh != value)
                {
                    this.IsObjectDirty = true;
                    this.laymansTermWhyItsImportant_zh = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> layman's term for describing why the component or system is important in the language specified in the <see cref="RuntimeInfo"/>.
        /// </summary>
        public string LaymansTermWhyItsImportant_Translated
        {
            get
            {
                this.EnsureLoaded();
                return this.RuntimeInfo.GetTranslatedValue(this.LaymansTermWhyItsImportant, this.LaymansTermWhyItsImportant_es, this.LaymansTermWhyItsImportant_fr, this.LaymansTermWhyItsImportant_zh);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> layman's term for describing the severity level.
        /// </summary>
        public string LaymansTermSeverityLevelDefinition
        {
            get
            {
                this.EnsureLoaded();
                return this.laymansTermSeverityLevelDefinition;
            }
            set
            {
                this.EnsureLoaded();
                if (this.laymansTermSeverityLevelDefinition != value)
                {
                    this.IsObjectDirty = true;
                    this.laymansTermSeverityLevelDefinition = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> layman's term for describing describing the severity level in Spanish.
        /// </summary>
        public string LaymansTermSeverityLevelDefinition_es
        {
            get
            {
                this.EnsureLoaded();
                return this.laymansTermSeverityLevelDefinition_es;
            }
            set
            {
                this.EnsureLoaded();
                if (this.laymansTermSeverityLevelDefinition_es != value)
                {
                    this.IsObjectDirty = true;
                    this.laymansTermSeverityLevelDefinition_es = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> layman's term for describing the severity level in French.
        /// </summary>
        public string LaymansTermSeverityLevelDefinition_fr
        {
            get
            {
                this.EnsureLoaded();
                return this.laymansTermSeverityLevelDefinition_fr;
            }
            set
            {
                this.EnsureLoaded();
                if (this.laymansTermSeverityLevelDefinition_fr != value)
                {
                    this.IsObjectDirty = true;
                    this.laymansTermSeverityLevelDefinition_fr = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> layman's term for describing the severity level in Chinese.
        /// </summary>
        public string LaymansTermSeverityLevelDefinition_zh
        {
            get
            {
                this.EnsureLoaded();
                return this.laymansTermSeverityLevelDefinition_zh;
            }
            set
            {
                this.EnsureLoaded();
                if (this.laymansTermSeverityLevelDefinition_zh != value)
                {
                    this.IsObjectDirty = true;
                    this.laymansTermSeverityLevelDefinition_zh = value;
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> layman's term for describing the severity level in the language specified in the <see cref="RuntimeInfo"/>.
        /// </summary>
        public string LaymansTermSeverityLevelDefinition_Translated
        {
            get
            {
                this.EnsureLoaded();
                return this.RuntimeInfo.GetTranslatedValue(this.LaymansTermSeverityLevelDefinition, this.LaymansTermSeverityLevelDefinition_es, this.LaymansTermSeverityLevelDefinition_fr, this.LaymansTermSeverityLevelDefinition_zh);
            }
        }

        #endregion Object Properties

        #region Object Properties (Related Objects)

        /*****************************************************************************************
		 *
		 * Object Relationships: Add correlated Object Collections
		 *
		*******************************************************************************************/

        #endregion Object Properties (Related Objects)

        #region Business Logic Methods

        /***********************************************************************************************
		 *
		 * Custom Business Logic
		 *
		 * **********************************************************************************************/

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
            dr.CommandTimeout = 120; //Added on 2017-06-7 12:46 PM by INNOVA Dev Team to increase the db connection timeout to 120 seconds (2 minutes)

            dr.ProcedureName = "DiagnosticReportResultErrorCodeDefinition_Load";
            dr.AddGuid("DiagnosticReportResultErrorCodeDefinitionId", Id);
        }

        /// <summary>
        /// Sets the properties of this object from the fields in the recordset located in the supplied <see cref="SqlDataReaderWrapper"/>.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> containing the recordset and fields used to set the properties of this object (this layer only).</param>
        protected new void SetPropertiesFromDataReader(SqlDataReaderWrapper dr)
        {
            this.diagnosticReportResultErrorCode = (DiagnosticReportResultErrorCode)Registry.CreateInstance(typeof(DiagnosticReportResultErrorCode), dr.GetGuid("DiagnosticReportResultErrorCodeId"));
            this.title = dr.GetString("Title");
            this.title_es = dr.GetString("Title_es");
            this.title_fr = dr.GetString("Title_fr");
            this.title_zh = dr.GetString("Title_zh");
            this.conditions = dr.GetString("Conditions");
            this.conditions_es = dr.GetString("Conditions_es");
            this.conditions_fr = dr.GetString("Conditions_fr");
            this.conditions_zh = dr.GetString("Conditions_zh");
            this.possibleCauses = dr.GetString("PossibleCauses");
            this.possibleCauses_es = dr.GetString("PossibleCauses_es");
            this.possibleCauses_fr = dr.GetString("PossibleCauses_fr");
            this.possibleCauses_zh = dr.GetString("PossibleCauses_zh");
            this.trips = dr.GetInt32("Trips");
            this.messageIndicatorLampFile = dr.GetString("MessageIndicatorLampFile");
            this.transmissionControlIndicatorLampFile = dr.GetString("TransmissionControlIndicatorLampFile");
            this.passiveAntiTheftIndicatorLampFile = dr.GetString("PassiveAntiTheftIndicatorLampFile");
            this.serviceThrottleSoonIndicatorLampFile = dr.GetString("ServiceThrottleSoonIndicatorLampFile");
            this.monitorType = dr.GetString("MonitorType");
            this.monitorFile = dr.GetString("MonitorFile");
            if (!dr.IsDBNull("DTCMasterCodeId"))
            {
                this.dTCMasterCode = (DtcMasterCodeList)Registry.CreateInstance(typeof(DtcMasterCodeList), dr.GetGuid("DTCMasterCodeId"));
            }
            if (!dr.IsDBNull("DTCCodeId"))
            {
                this.dTCCode = (DtcCode)Registry.CreateInstance(typeof(DtcCode), dr.GetGuid("DTCCodeId"));
            }
            if (!dr.IsDBNull("VehicleTypeId"))
            {
                this.vehicleType = (VehicleType)Registry.CreateInstance(typeof(VehicleType), dr.GetGuid("VehicleTypeId"));
            }
            this.make = dr.GetString("Make");
            this.model = dr.GetString("Model");
            this.year = dr.GetInt32("Year");
            this.bodyCode = dr.GetString("BodyCode");
            this.engineType = dr.GetString("EngineType");
            this.engineVINCode = dr.GetString("EngineVINCode");
            this.engineTypeVINLookup = dr.GetString("EngineTypeVINLookup");
            this.transmissionControlType = dr.GetString("TransmissionControlType");
            this.laymansTermTitle = dr.GetString("LaymansTermTitle");
            this.laymansTermTitle_es = dr.GetString("LaymansTermTitle_es");
            this.laymansTermTitle_fr = dr.GetString("LaymansTermTitle_es");
            this.laymansTermTitle_zh = dr.GetString("LaymansTermTitle_zh");
            this.laymansTermDescription = dr.GetString("LaymansTermDescription");
            this.laymansTermDescription_es = dr.GetString("LaymansTermDescription_es");
            this.laymansTermDescription_fr = dr.GetString("LaymansTermDescription_es");
            this.laymansTermDescription_zh = dr.GetString("LaymansTermDescription_zh");
            this.laymansTermConditions = dr.GetString("LaymansTermConditions");
            this.laymansTermConditions_es = dr.GetString("LaymansTermConditions_es");
            this.laymansTermConditions_fr = dr.GetString("LaymansTermConditions_fr");
            this.laymansTermConditions_zh = dr.GetString("LaymansTermConditions_zh");
            this.laymansTermSeverityLevel = dr.GetInt32("LaymansTermSeverityLevel");
            this.laymansTermEffectOnVehicle = dr.GetString("LaymansTermEffectOnVehicle");
            this.laymansTermEffectOnVehicle_es = dr.GetString("LaymansTermEffectOnVehicle_es");
            this.laymansTermEffectOnVehicle_fr = dr.GetString("LaymansTermEffectOnVehicle_fr");
            this.laymansTermEffectOnVehicle_zh = dr.GetString("LaymansTermEffectOnVehicle_zh");
            this.laymansTermResponsibleComponentOrSystem = dr.GetString("LaymansTermResponsibleComponentOrSystem");
            this.laymansTermResponsibleComponentOrSystem_es = dr.GetString("LaymansTermResponsibleComponentOrSystem_es");
            this.laymansTermResponsibleComponentOrSystem_fr = dr.GetString("LaymansTermResponsibleComponentOrSystem_fr");
            this.laymansTermResponsibleComponentOrSystem_zh = dr.GetString("LaymansTermResponsibleComponentOrSystem_zh");
            this.laymansTermWhyItsImportant = dr.GetString("LaymansTermWhyItsImportant");
            this.laymansTermWhyItsImportant_es = dr.GetString("LaymansTermWhyItsImportant_es");
            this.laymansTermWhyItsImportant_fr = dr.GetString("LaymansTermWhyItsImportant_fr");
            this.laymansTermWhyItsImportant_zh = dr.GetString("LaymansTermWhyItsImportant_zh");
            this.laymansTermSeverityLevelDefinition = dr.GetString("LaymansTermSeverityLevelDefinition");
            this.laymansTermSeverityLevelDefinition_es = dr.GetString("LaymansTermSeverityLevelDefinition_es");
            this.laymansTermSeverityLevelDefinition_fr = dr.GetString("LaymansTermSeverityLevelDefinition_fr");
            this.laymansTermSeverityLevelDefinition_zh = dr.GetString("LaymansTermSeverityLevelDefinition_zh");

            IsObjectLoaded = true;
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
            if (IsObjectDirty)
            {
                transaction = EnsureDatabasePrepared(connection, transaction);

                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    dr.CommandTimeout = 120; //Added on 2017-06-7 12:46 PM by INNOVA Dev Team to increase the db connection timeout to 120 seconds (2 minutes)

                    if (IsObjectCreated)
                    {
                        dr.ProcedureName = "DiagnosticReportResultErrorCodeDefinition_Create";
                    }
                    else
                    {
                        dr.ProcedureName = "DiagnosticReportResultErrorCodeDefinition_Save";
                    }

                    dr.AddGuid("DiagnosticReportResultErrorCodeDefinitionId", Id);
                    dr.AddGuid("DiagnosticReportResultErrorCodeId", DiagnosticReportResultErrorCode.Id);
                    dr.AddNVarChar("Title", Title);
                    dr.AddNVarChar("Title_es", Title_es);
                    dr.AddNVarChar("Title_fr", Title_fr);
                    dr.AddNVarChar("Title_zh", Title_zh);
                    dr.AddNVarChar("Conditions", Conditions);
                    dr.AddNVarChar("Conditions_es", Conditions_es);
                    dr.AddNVarChar("Conditions_fr", Conditions_fr);
                    dr.AddNVarChar("Conditions_zh", Conditions_zh);
                    dr.AddNVarChar("PossibleCauses", PossibleCauses);
                    dr.AddNVarChar("PossibleCauses_es", PossibleCauses_es);
                    dr.AddNVarChar("PossibleCauses_fr", PossibleCauses_fr);
                    dr.AddNVarChar("PossibleCauses_zh", PossibleCauses_zh);
                    dr.AddInt32("Trips", Trips);
                    dr.AddNVarChar("MessageIndicatorLampFile", MessageIndicatorLampFile);
                    dr.AddNVarChar("TransmissionControlIndicatorLampFile", TransmissionControlIndicatorLampFile);
                    dr.AddNVarChar("PassiveAntiTheftIndicatorLampFile", PassiveAntiTheftIndicatorLampFile);
                    dr.AddNVarChar("ServiceThrottleSoonIndicatorLampFile", ServiceThrottleSoonIndicatorLampFile);
                    dr.AddNVarChar("MonitorType", MonitorType);
                    dr.AddNVarChar("MonitorFile", MonitorFile);
                    if (DTCMasterCode != null)
                    {
                        dr.AddGuid("DTCMasterCodeId", DTCMasterCode.Id);
                    }
                    if (DTCCode != null)
                    {
                        dr.AddGuid("DTCCodeId", DTCCode.Id);
                    }
                    //Removed Chilton DTCs on 2018-10-12 1:50 PM
                    //Re-AddChiltonDTC
                    if (VehicleTypeCode != null)
                    {
                        dr.AddGuid("VehicleTypeCodeId", VehicleTypeCode.Id);
                    }
                    //Re-AddChiltonDTC
                    if (VehicleType != null)
                    {
                        dr.AddGuid("VehicleTypeId", VehicleType.Id);
                    }
                    dr.AddNVarChar("Make", Make);
                    dr.AddNVarChar("Model", Model);
                    dr.AddInt32("Year", Year);
                    dr.AddNVarChar("BodyCode", BodyCode);
                    dr.AddNVarChar("EngineType", EngineType);
                    dr.AddNVarChar("EngineVINCode", EngineVINCode);
                    dr.AddNVarChar("EngineTypeVINLookup", EngineTypeVINLookup);
                    dr.AddNVarChar("TransmissionControlType", TransmissionControlType);
                    dr.AddNVarChar("LaymansTermTitle", this.LaymansTermTitle);
                    dr.AddNVarChar("LaymansTermTitle_es", this.LaymansTermTitle_es);
                    dr.AddNVarChar("LaymansTermTitle_fr", this.LaymansTermTitle_fr);
                    dr.AddNVarChar("LaymansTermTitle_zh", this.LaymansTermTitle_zh);
                    dr.AddNVarChar("LaymansTermDescription", this.LaymansTermDescription);
                    dr.AddNVarChar("LaymansTermDescription_es", this.LaymansTermDescription_es);
                    dr.AddNVarChar("LaymansTermDescription_fr", this.LaymansTermDescription_fr);
                    dr.AddNVarChar("LaymansTermDescription_zh", this.LaymansTermDescription_zh);
                    dr.AddNVarChar("LaymansTermConditions", this.LaymansTermConditions);
                    dr.AddNVarChar("LaymansTermConditions_es", this.LaymansTermConditions_es);
                    dr.AddNVarChar("LaymansTermConditions_fr", this.LaymansTermConditions_fr);
                    dr.AddNVarChar("LaymansTermConditions_zh", this.LaymansTermConditions_zh);
                    dr.AddInt32("LaymansTermSeverityLevel", this.LaymansTermSeverityLevel);
                    dr.AddNVarChar("LaymansTermEffectOnVehicle", this.LaymansTermEffectOnVehicle);
                    dr.AddNVarChar("LaymansTermEffectOnVehicle_es", this.LaymansTermEffectOnVehicle_es);
                    dr.AddNVarChar("LaymansTermEffectOnVehicle_fr", this.LaymansTermEffectOnVehicle_fr);
                    dr.AddNVarChar("LaymansTermEffectOnVehicle_zh", this.LaymansTermEffectOnVehicle_zh);
                    dr.AddNVarChar("LaymansTermResponsibleComponentOrSystem", this.LaymansTermResponsibleComponentOrSystem);
                    dr.AddNVarChar("LaymansTermResponsibleComponentOrSystem_es", this.LaymansTermResponsibleComponentOrSystem_es);
                    dr.AddNVarChar("LaymansTermResponsibleComponentOrSystem_fr", this.LaymansTermResponsibleComponentOrSystem_fr);
                    dr.AddNVarChar("LaymansTermResponsibleComponentOrSystem_zh", this.LaymansTermResponsibleComponentOrSystem_zh);
                    dr.AddNVarChar("LaymansTermWhyItsImportant", this.LaymansTermWhyItsImportant);
                    dr.AddNVarChar("LaymansTermWhyItsImportant_es", this.LaymansTermWhyItsImportant_es);
                    dr.AddNVarChar("LaymansTermWhyItsImportant_fr", this.LaymansTermWhyItsImportant_fr);
                    dr.AddNVarChar("LaymansTermWhyItsImportant_zh", this.LaymansTermWhyItsImportant_zh);
                    dr.AddNVarChar("LaymansTermSeverityLevelDefinition", this.LaymansTermSeverityLevelDefinition);
                    dr.AddNVarChar("LaymansTermSeverityLevelDefinition_es", this.LaymansTermSeverityLevelDefinition_es);
                    dr.AddNVarChar("LaymansTermSeverityLevelDefinition_fr", this.LaymansTermSeverityLevelDefinition_fr);
                    dr.AddNVarChar("LaymansTermSeverityLevelDefinition_zh", this.LaymansTermSeverityLevelDefinition_zh);

                    dr.Execute(transaction);
                }

                IsObjectDirty = false;
            }

            // call the save collections method
            transaction = SaveCollections(connection, transaction);

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
            EnsureValidId();

            transaction = EnsureDatabasePrepared(connection, transaction);

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
                dr.CommandTimeout = 120; //Added on 2017-06-7 12:46 PM by INNOVA Dev Team to increase the db connection timeout to 120 seconds (2 minutes)

                //remove related objects with a specialized delete.
                /*
				dr.ProcedureName = "ItemsOthers_Delete";
				dr.AddGuid("ItemId", Id);

				dr.ExecuteNonQuery(transaction);
				dr.ClearParameters();
				*/

                //delete the item
                dr.ProcedureName = "Item_Delete";
                dr.AddGuid("ItemId", Id);

                dr.ExecuteNonQuery(transaction);
            }

            transaction = base.Delete(connection, transaction);

            return transaction;
        }

        #endregion Required Object Methods (Load, Save, Delete, Etc)
    }
}
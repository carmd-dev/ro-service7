using Innova.DtcCodes;
using Innova.Vehicles;
using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using System;
using System.Collections;
using System.Data.SqlClient;

namespace Innova.DiagnosticReports
{
    /// <summary>
    /// The DiagnosticReportResultErrorCode object handles the business logic and data access for the specialized business object, DiagnosticReportResultErrorCode.
    /// The class inherits from <see cref="BusinessObjectBase"/> class.
    /// </summary>
    /// <example>
    /// See the examples of how to construct the DiagnosticReportResultErrorCode object.
    ///
    /// To create a new instance of a new of DiagnosticReportResultErrorCode.
    /// <code>DiagnosticReportResultErrorCode o = (DiagnosticReportResultErrorCode)Registry.CreateInstance(typeof(DiagnosticReportResultErrorCode));</code>
    ///
    /// To create an new instance of an existing DiagnosticReportResultErrorCode.
    /// <code>DiagnosticReportResultErrorCode o = (DiagnosticReportResultErrorCode)Registry.CreateInstance(typeof(DiagnosticReportResultErrorCode), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of DiagnosticReportResultErrorCode, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectBase.ConnectionString"/> to the <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable()]
    public class DiagnosticReportResultErrorCode : BusinessObjectBase
    {
        private DiagnosticReportResult diagnosticReportResult;
        private VehicleTypeCode vehicleTypeCode;
        private DtcCode dtcCode;
        private DtcMasterCodeList dtcMasterCodeList;

        private string errorCode = "";
        private string errorCodeType = "";
        private DiagnosticReportErrorCodeType diagnosticReportErrorCodeType;
        private DiagnosticReportErrorCodeSystemType diagnosticReportErrorCodeSystemType;
        private int sortOrder = 0;

        private DiagnosticReportResultErrorCodeDefinitionCollection diagnosticReportResultErrorCodeDefinitions;

        private bool unableToFindCodeData = true;



        #region System Constructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new (one that does not exist, assigning a new <see cref="Guid"/> Id). DiagnosticReportResultErrorCode object.
        /// In order to create a new DiagnosticReportResultErrorCode which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <example>
        /// <code>
        /// DiagnosticReportResultErrorCode o = (DiagnosticReportResultErrorCode)Registry.CreateInstance(typeof(DiagnosticReportResultErrorCode));
        /// </code>
        /// </example>
        protected internal DiagnosticReportResultErrorCode() : base()
        {
            IsObjectCreated = true;
            IsObjectLoaded = true;
            IsObjectDirty = true;
        }

        /// <summary>
        /// Constructor creates an existing (one that exists, and already has a <see cref="Guid"/> Id).  DiagnosticReportResultErrorCode object.
        /// In order to create an existing DiagnosticReportResultErrorCode object, which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <param name="id">The <see cref="Guid"/> or SQL uniqueidentifier indentification "Id" of the current object.</param>
        /// <example>
        /// <code>
        /// DiagnosticReportResultErrorCode o = (DiagnosticReportResultErrorCode)Registry.CreateInstance(typeof(DiagnosticReportResultErrorCode), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));
        /// </code>
        /// </example>
        protected internal DiagnosticReportResultErrorCode(Guid id) : base(id)
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
        /// Gets or sets the <see cref="DiagnosticReportResultFix"/>
        /// </summary>
        public DiagnosticReportResult DiagnosticReportResult
        {
            get
            {
                EnsureLoaded();
                return diagnosticReportResult;
            }
            set
            {
                EnsureLoaded();
                if (diagnosticReportResult != value)
                {
                    IsObjectDirty = true;
                    diagnosticReportResult = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DiagnosticReport"/>
        /// </summary>
        [PropertyDefinition("DiagnosticReport", "The diagnostic report.")]
        public DiagnosticReport DiagnosticReport
        {
            get
            {
                EnsureLoaded();
                return this.DiagnosticReportResult.DiagnosticReport;
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
        /// Get or sets the <see cref="Vehicle"/>
        /// </summary>
        [PropertyDefinition("Vehicle", "The vehicle of the report.")]
        public Vehicle Vehicle
        {
            get
            {
                EnsureLoaded();
                return this.DiagnosticReport.Vehicle;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DtcCode"/>
        /// </summary>
        public DtcCode DtcCode
        {
            get
            {
                EnsureLoaded();
                return dtcCode;
            }
            set
            {
                EnsureLoaded();
                if (dtcCode != value)
                {
                    IsObjectDirty = true;
                    dtcCode = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DtcMasterCodeList"/>
        /// </summary>
        public DtcMasterCodeList DtcMasterCodeList
        {
            get
            {
                EnsureLoaded();
                return dtcMasterCodeList;
            }
            set
            {
                EnsureLoaded();
                if (dtcMasterCodeList != value)
                {
                    IsObjectDirty = true;
                    dtcMasterCodeList = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        public string ErrorCode
        {
            get
            {
                EnsureLoaded();
                return errorCode;
            }
            set
            {
                EnsureLoaded();
                if (errorCode != value)
                {
                    IsObjectDirty = true;
                    errorCode = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        public string ErrorCodeType
        {
            get
            {
                EnsureLoaded();
                return errorCodeType;
            }
            set
            {
                EnsureLoaded();
                if (errorCodeType != value)
                {
                    IsObjectDirty = true;
                    errorCodeType = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DiagnosticReportErrorCodeType"/>
        /// </summary>
        [PropertyDefinition("DiagnosticReportErrorCodeType", "DiagnosticReportErrorCodeType", "DiagnosticReportErrorCodeType", "Error Code", "The error code of the diagnostic report.")]
        public DiagnosticReportErrorCodeType DiagnosticReportErrorCodeType
        {
            get
            {
                EnsureLoaded();
                return diagnosticReportErrorCodeType;
            }
            set
            {
                EnsureLoaded();
                if (diagnosticReportErrorCodeType != value)
                {
                    IsObjectDirty = true;
                    diagnosticReportErrorCodeType = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DiagnosticReportErrorCodeSystemType"/>
        /// </summary>
        [PropertyDefinition("DiagnosticReportErrorCodeSystemType", "System Types", "System Types", "Type", "The error code for the system type.")]
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
                if (this.diagnosticReportErrorCodeSystemType != value)
                {
                    this.IsObjectDirty = true;
                    this.diagnosticReportErrorCodeSystemType = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/>
        /// </summary>
        public int SortOrder
        {
            get
            {
                EnsureLoaded();
                return sortOrder;
            }
            set
            {
                EnsureLoaded();
                if (sortOrder != value)
                {
                    IsObjectDirty = true;
                    sortOrder = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/> flag indicating whether or not code data exists for this error code.
        /// </summary>
        public bool UnableToFindCodeData
        {
            get
            {
                //this.unableToFindCodeData = this.DiagnosticReportResultErrorCodeDefinitions.Count == 0;
                return this.unableToFindCodeData;
            }
        }

        /// <summary>
        /// Gets the <see cref="DateTime"/> of the diagnostic report.
        /// </summary>
        [PropertyDefinition("DiagnotsicReportCreatedDateTimeUTC", "Created", "Created", "Created", "The date of the daignostic report.")]
        public DateTime DiagnotsicReportCreatedDateTimeUTC
        {
            get
            {
                EnsureLoaded();
                return DiagnosticReport.CreatedDateTimeUTC;
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
        /// Gets the <see cref="DiagnosticReportResultErrorCodeDefinitionCollection"/> collection of <see cref="DiagnosticReportResultErrorCodeDefinition"/> objects which are defined for this error code.
        /// </summary>
        private DiagnosticReportResultErrorCodeDefinitionCollection DiagnosticReportResultErrorCodeDefinitions
        {
            get
            {
                if (diagnosticReportResultErrorCodeDefinitions == null)
                {
                    diagnosticReportResultErrorCodeDefinitions = new DiagnosticReportResultErrorCodeDefinitionCollection(Registry);

                    //load if not a user created element
                    if (!isObjectCreated)
                    {
                        SqlProcedureCommand call = new SqlProcedureCommand();
                        EnsureValidId();

                        call.ProcedureName = "DiagnosticReportResultErrorCodeDefinition_LoadByDiagnosticReportResultErrorCode";
                        call.AddGuid("DiagnosticReportResultErrorCodeId", Id);

                        diagnosticReportResultErrorCodeDefinitions.Load(call, "DiagnosticReportResultErrorCodeDefinitionId", true, true);
                    }
                }

                return diagnosticReportResultErrorCodeDefinitions;
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
        /// Gets the <see cref="DiagnosticReportResultErrorCodeDefinitionDisplayCollection"/> of <see cref="DiagnosticReportResultErrorCodeDefinition"/> objects that are associated with this error code. There could be more than one.  The method de-dupes them based on the type of report.  For example, CarMD only shows Title and PossibleCauses, so the de-duping is handled that way.
        /// </summary>
        /// <param name="diagnosticReportResultType"></param>
        /// <returns></returns>
        public DiagnosticReportResultErrorCodeDefinitionDisplayCollection GetDiagnosticReportResultErrorCodeDefinitions(DiagnosticReportResultType diagnosticReportResultType)
        {
            DiagnosticReportResultErrorCodeDefinitionDisplayCollection defs = new DiagnosticReportResultErrorCodeDefinitionDisplayCollection();

            if (this.UnableToFindCodeData == false)
            {
                DiagnosticReportResultErrorCodeDefinitionDisplay lastDef = null;

                //if we have more than one definition we have to merge them together based on the type of diagnostic report we are running
                if (this.DiagnosticReportResultErrorCodeDefinitions.Count > 1)
                {
                    //lets sort them
                    SortDictionaryCollection sorts = new SortDictionaryCollection();
                    sorts.Add(new SortDictionary("Title"));
                    sorts.Add(new SortDictionary("PossibleCauses"));
                    sorts.Add(new SortDictionary("Conditions"));
                    sorts.Add(new SortDictionary("Trips"));
                    sorts.Add(new SortDictionary("MessageIndicatorLampFile"));
                    sorts.Add(new SortDictionary("TransmissionControlIndicatorLampFile"));
                    sorts.Add(new SortDictionary("PassiveAntiTheftIndicatorLampFile"));
                    sorts.Add(new SortDictionary("ServiceThrottleSoonIndicatorLampFile"));
                    sorts.Add(new SortDictionary("MonitorType"));
                    sorts.Add(new SortDictionary("MonitorFile"));

                    //sort them to see if we have dupes or not
                    this.DiagnosticReportResultErrorCodeDefinitions.Sort(sorts);
                }

                foreach (DiagnosticReportResultErrorCodeDefinition errorCodeDef in this.DiagnosticReportResultErrorCodeDefinitions)
                {
                    bool isDupe = false;

                    DiagnosticReportResultErrorCodeDefinitionDisplay def = null;

                    //now determine if it is a dupe based on the rules
                    if (lastDef != null)
                    {
                        if (diagnosticReportResultType == DiagnosticReportResultType.ExternalSystem)
                        {
                            if (lastDef.Title == errorCodeDef.Title
                                && lastDef.PossibleCauses == errorCodeDef.PossibleCauses)
                            {
                                isDupe = true;
                            }
                        }
                        else if (diagnosticReportResultType == DiagnosticReportResultType.CarScan)
                        {
                            if (lastDef.Title == errorCodeDef.Title
                                && lastDef.PossibleCauses == errorCodeDef.PossibleCauses
                                && lastDef.Conditions == errorCodeDef.Conditions)
                            {
                                isDupe = true;
                            }
                        }
                        else//else everything has to match
                        {
                            if (lastDef.Title == errorCodeDef.Title
                                && lastDef.PossibleCauses == errorCodeDef.PossibleCauses
                                && lastDef.Conditions == errorCodeDef.Conditions
                                && lastDef.Trips == errorCodeDef.Trips
                                && lastDef.MonitorType == errorCodeDef.MonitorType
                                && lastDef.MessageIndicatorLampFile == errorCodeDef.MessageIndicatorLampFile
                                && lastDef.TransmissionControlIndicatorLampFile == errorCodeDef.TransmissionControlIndicatorLampFile
                                && lastDef.PassiveAntiTheftIndicatorLampFile == errorCodeDef.PassiveAntiTheftIndicatorLampFile
                                && lastDef.ServiceThrottleSoonIndicatorLampFile == errorCodeDef.ServiceThrottleSoonIndicatorLampFile
                                && lastDef.MonitorFile == errorCodeDef.MonitorFile)
                            {
                                isDupe = true;
                            }
                        }
                    }

                    //if this is not a dupe, create the new definition
                    if (!isDupe)
                    {
                        def = new DiagnosticReportResultErrorCodeDefinitionDisplay();

                        //2012-12-15 STW, we maybe don't even need the non translated fields here....
                        def.Title = errorCodeDef.Title;
                        def.Title_es = errorCodeDef.Title_es;
                        def.Title_fr = errorCodeDef.Title_fr;
                        def.Title_zh = errorCodeDef.Title_zh;
                        def.Title_Translated = errorCodeDef.Title_Translated;
                        def.PossibleCauses = errorCodeDef.PossibleCauses;
                        def.PossibleCauses_es = errorCodeDef.PossibleCauses_es;
                        def.PossibleCauses_fr = errorCodeDef.PossibleCauses_fr;
                        def.PossibleCauses_zh = errorCodeDef.PossibleCauses_zh;
                        def.PossibleCauses_Translated = errorCodeDef.PossibleCauses_Translated;
                        def.Conditions = errorCodeDef.Conditions;
                        def.Conditions_es = errorCodeDef.Conditions_es;
                        def.Conditions_fr = errorCodeDef.Conditions_fr;
                        def.Conditions_zh = errorCodeDef.Conditions_zh;
                        def.Conditions_Translated = errorCodeDef.Conditions_Translated;
                        def.Trips = errorCodeDef.Trips;
                        def.MessageIndicatorLampFile = errorCodeDef.MessageIndicatorLampFile;
                        def.TransmissionControlIndicatorLampFile = errorCodeDef.TransmissionControlIndicatorLampFile;
                        def.PassiveAntiTheftIndicatorLampFile = errorCodeDef.PassiveAntiTheftIndicatorLampFile;
                        def.ServiceThrottleSoonIndicatorLampFile = errorCodeDef.ServiceThrottleSoonIndicatorLampFile;
                        def.MonitorType = errorCodeDef.MonitorType;
                        def.MonitorFile = errorCodeDef.MonitorFile;
                        def.LaymansTermTitle = errorCodeDef.LaymansTermTitle;
                        def.LaymansTermTitle_es = errorCodeDef.LaymansTermTitle_es;
                        def.LaymansTermTitle_fr = errorCodeDef.LaymansTermTitle_fr;
                        def.LaymansTermTitle_zh = errorCodeDef.LaymansTermTitle_zh;
                        def.LaymansTermTitle_Translated = errorCodeDef.LaymansTermTitle_Translated;
                        def.LaymansTermDescription = errorCodeDef.LaymansTermDescription;
                        def.LaymansTermDescription_es = errorCodeDef.LaymansTermDescription_es;
                        def.LaymansTermDescription_fr = errorCodeDef.LaymansTermDescription_fr;
                        def.LaymansTermDescription_zh = errorCodeDef.LaymansTermDescription_zh;
                        def.LaymansTermDescription_Translated = errorCodeDef.LaymansTermDescription_Translated;
                        def.LaymansTermConditions = errorCodeDef.LaymansTermConditions;
                        def.LaymansTermConditions_es = errorCodeDef.LaymansTermConditions_es;
                        def.LaymansTermConditions_fr = errorCodeDef.LaymansTermConditions_fr;
                        def.LaymansTermConditions_zh = errorCodeDef.LaymansTermConditions_zh;
                        def.LaymansTermConditions_Translated = errorCodeDef.LaymansTermConditions_Translated;
                        def.LaymansTermSeverityLevel = errorCodeDef.LaymansTermSeverityLevel;
                        def.LaymansTermEffectOnVehicle = errorCodeDef.LaymansTermEffectOnVehicle;
                        def.LaymansTermEffectOnVehicle_es = errorCodeDef.LaymansTermEffectOnVehicle_es;
                        def.LaymansTermEffectOnVehicle_fr = errorCodeDef.LaymansTermEffectOnVehicle_fr;
                        def.LaymansTermEffectOnVehicle_zh = errorCodeDef.LaymansTermEffectOnVehicle_zh;
                        def.LaymansTermEffectOnVehicle_Translated = errorCodeDef.LaymansTermEffectOnVehicle_Translated;
                        def.LaymansTermResponsibleComponentOrSystem = errorCodeDef.LaymansTermResponsibleComponentOrSystem;
                        def.LaymansTermResponsibleComponentOrSystem_es = errorCodeDef.LaymansTermResponsibleComponentOrSystem_es;
                        def.LaymansTermResponsibleComponentOrSystem_fr = errorCodeDef.LaymansTermResponsibleComponentOrSystem_fr;
                        def.LaymansTermResponsibleComponentOrSystem_zh = errorCodeDef.LaymansTermResponsibleComponentOrSystem_zh;
                        def.LaymansTermResponsibleComponentOrSystem_Translated = errorCodeDef.LaymansTermResponsibleComponentOrSystem_Translated;
                        def.LaymansTermWhyItsImportant = errorCodeDef.LaymansTermWhyItsImportant;
                        def.LaymansTermWhyItsImportant_es = errorCodeDef.LaymansTermWhyItsImportant_es;
                        def.LaymansTermWhyItsImportant_fr = errorCodeDef.LaymansTermWhyItsImportant_fr;
                        def.LaymansTermWhyItsImportant_zh = errorCodeDef.LaymansTermWhyItsImportant_zh;
                        def.LaymansTermWhyItsImportant_Translated = errorCodeDef.LaymansTermWhyItsImportant_Translated;
                        def.LaymansTermSeverityLevelDefinition = errorCodeDef.LaymansTermSeverityLevelDefinition;
                        def.LaymansTermSeverityLevelDefinition_es = errorCodeDef.LaymansTermSeverityLevelDefinition_es;
                        def.LaymansTermSeverityLevelDefinition_fr = errorCodeDef.LaymansTermSeverityLevelDefinition_fr;
                        def.LaymansTermSeverityLevelDefinition_zh = errorCodeDef.LaymansTermSeverityLevelDefinition_zh;
                        def.LaymansTermSeverityLevelDefinition_Translated = errorCodeDef.LaymansTermSeverityLevelDefinition_Translated;

                        //add the defs to the def
                        defs.Add(def);

                        //set the last def equal to the one we just added
                        lastDef = def;
                    }
                    else
                    {
                        //else is dupe, then current def is last def
                        def = lastDef;
                    }

                    //add the vehicle type to the list if there is more than one definition then add vehicle info.
                    /* STW 2007-07-31 break on first no matter what for all systems Per Keith Andreason. */
                    if (this.DiagnosticReportResultErrorCodeDefinitions.Count > 0)
                    {
                        if (def.DiagnosticReportResultErrorCodeDefinitionDisplayVehicles == null)
                        {
                            def.DiagnosticReportResultErrorCodeDefinitionDisplayVehicles = new DiagnosticReportResultErrorCodeDefinitionDisplayVehicleCollection();
                        }

                        DiagnosticReportResultErrorCodeDefinitionDisplayVehicle veh = new DiagnosticReportResultErrorCodeDefinitionDisplayVehicle();

                        veh.ManufacturerName = errorCodeDef.Make;
                        veh.Make = errorCodeDef.Make;
                        veh.Model = errorCodeDef.Model;
                        veh.EngineType = errorCodeDef.EngineType;
                        veh.EngineVINCode = errorCodeDef.EngineVINCode;
                        veh.EngineTypeVINLookup = errorCodeDef.EngineTypeVINLookup;
                        veh.BodyCode = errorCodeDef.BodyCode;
                        veh.TransmissionControlType = errorCodeDef.TransmissionControlType;

                        //add the vehicle to the list of the ones with the definition.
                        def.DiagnosticReportResultErrorCodeDefinitionDisplayVehicles.Add(veh);
                    }

                    break;
                }
            }

            return defs;
        }

        /// <summary>
        /// Adds a diagnostic report result error code to the diagnostic report result.
        /// </summary>
        /// <param name="dtcCode"><see cref="DtcCode"/> to add a definition for.</param>
        public void AddDiagnosticReportResultErrorCodeDefinition(DtcCode dtcCode)
        {
            DiagnosticReportResultErrorCodeDefinition def = (DiagnosticReportResultErrorCodeDefinition)Registry.CreateInstance(typeof(DiagnosticReportResultErrorCodeDefinition));
            def.DiagnosticReportResultErrorCode = this;

            def.DTCCode = dtcCode;

            def.Title = dtcCode.Title;
            def.Title_es = dtcCode.Title_es;
            def.Title_fr = dtcCode.Title_fr;
            def.Title_zh = dtcCode.Title_zh;
            def.Conditions = dtcCode.Conditions;
            def.Conditions_es = dtcCode.Conditions_es;
            def.Conditions_fr = dtcCode.Conditions_fr;
            def.Conditions_zh = dtcCode.Conditions_zh;
            def.PossibleCauses = dtcCode.PossibleCauses;
            def.PossibleCauses_es = dtcCode.PossibleCauses_es;
            def.PossibleCauses_fr = dtcCode.PossibleCauses_fr;
            def.PossibleCauses_zh = dtcCode.PossibleCauses_zh;
            def.Trips = dtcCode.Trips;
            def.MessageIndicatorLampFile = dtcCode.MessageIndicatorLampFile;
            def.TransmissionControlIndicatorLampFile = dtcCode.TransmissionControlIndicatorLampFile;
            def.PassiveAntiTheftIndicatorLampFile = dtcCode.PassiveAntiTheftIndicatorLampFile;
            def.ServiceThrottleSoonIndicatorLampFile = dtcCode.ServiceThrottleSoonIndicatorLampFile;
            def.MonitorType = dtcCode.MonitorType;
            def.MonitorFile = dtcCode.MonitorFile;

            def.Model = GetDelimittedString(dtcCode.Models, "|");
            def.Make = GetDelimittedString(dtcCode.Makes, "|");
            def.Year = 0;
            if (dtcCode.Years.Count > 0)
            {
                def.Year = (int)dtcCode.Years[0];
            }
            def.EngineType = GetDelimittedString(dtcCode.EngineTypes, "|");
            def.EngineVINCode = GetDelimittedString(dtcCode.EngineVINCodes, "|");
            def.TransmissionControlType = GetDelimittedString(dtcCode.TransmissionControlTypes, "|");

            this.SetLaymanTermProperties(dtcCode.ErrorCode, def);

            this.DiagnosticReportResultErrorCodeDefinitions.Add(def);

            this.unableToFindCodeData = false;
        }

        private static string GetDelimittedString(IEnumerable list, string delimiter)
        {
            string s = "";

            foreach (object o in list)
            {
                string os = o.ToString();

                if (os != null && os != "")
                {
                    if (s.Length > 0)
                    {
                        s += delimiter;
                    }
                    s += os.Trim();
                }
            }
            return s;
        }

        /// <summary>
        /// Adds a diagnostic report result error code to the diagnostic report result.
        /// </summary>
        /// <param name="dtcMasterCode"><see cref="DtcMasterCodeList"/> to add a definition for.</param>
        public void AddDiagnosticReportResultErrorCodeDefinition(DtcMasterCodeList dtcMasterCode)
        {
            DiagnosticReportResultErrorCodeDefinition def = (DiagnosticReportResultErrorCodeDefinition)Registry.CreateInstance(typeof(DiagnosticReportResultErrorCodeDefinition));
            def.DiagnosticReportResultErrorCode = this;

            def.DTCMasterCode = dtcMasterCode;
            def.Title = dtcMasterCode.Title;
            def.Title_es = dtcMasterCode.Title_es;
            def.Title_fr = dtcMasterCode.Title_fr;

            def.Make = GetDelimittedString(dtcMasterCode.Makes, "|");

            this.SetLaymanTermProperties(dtcMasterCode.ErrorCode, def);

            this.DiagnosticReportResultErrorCodeDefinitions.Add(def);

            this.unableToFindCodeData = false;
        }

        //Re-AddChiltonDTC - Add
        /// <summary>
        /// Adds a diagnostic report result error code to the diagnostic report result.
        /// </summary>
        /// <param name="vehicleTypeCodeAssignments"><see cref="ArrayList"/> of <see cref="VehicleTypeCodeAssignment"/> vehicle type code assignment objects that are associated with the vehicle.</param>
        public void AddDiagnosticReportResultErrorCodeDefinition(ArrayList vehicleTypeCodeAssignments)
        {
            //for each vehicle type code assignment in the list add the assignment to the list
            foreach (VehicleTypeCodeAssignment vca in vehicleTypeCodeAssignments)
            {
                PropertyDictionaryCollection props = new PropertyDictionaryCollection();
                props.Add(new PropertyDictionary("VehicleTypeCode.Id", vca.VehicleTypeCode.Id));
                props.Add(new PropertyDictionary("VehicleType.Id", vca.VehicleType.Id));

                //if the vehicle type code isn't already in the list (it shouldn't be) don't add the same code twice.
                if (this.DiagnosticReportResultErrorCodeDefinitions.FindByMultipleProperties(props) == null)
                {
                    //create a new one for each assignment
                    DiagnosticReportResultErrorCodeDefinition def = (DiagnosticReportResultErrorCodeDefinition)Registry.CreateInstance(typeof(DiagnosticReportResultErrorCodeDefinition));
                    def.DiagnosticReportResultErrorCode = this;

                    def.VehicleTypeCode = vca.VehicleTypeCode;

                    def.Title = vca.VehicleTypeCode.Title;
                    def.Title_es = vca.VehicleTypeCode.Title_es;
                    def.Title_fr = vca.VehicleTypeCode.Title_fr;
                    def.Title_zh = vca.VehicleTypeCode.Title_zh;
                    def.Conditions = vca.VehicleTypeCode.Conditions;
                    def.Conditions_es = vca.VehicleTypeCode.Conditions_es;
                    def.Conditions_fr = vca.VehicleTypeCode.Conditions_fr;
                    def.Conditions_zh = vca.VehicleTypeCode.Conditions_zh;
                    def.PossibleCauses = vca.VehicleTypeCode.PossibleCauses;
                    def.PossibleCauses_es = vca.VehicleTypeCode.PossibleCauses_es;
                    def.PossibleCauses_fr = vca.VehicleTypeCode.PossibleCauses_fr;
                    def.PossibleCauses_zh = vca.VehicleTypeCode.PossibleCauses_zh;
                    def.Trips = vca.VehicleTypeCode.Trips;
                    def.MessageIndicatorLampFile = vca.VehicleTypeCode.MessageIndicatorLampFile;
                    def.TransmissionControlIndicatorLampFile = vca.VehicleTypeCode.TransmissionControlIndicatorLampFile;
                    def.PassiveAntiTheftIndicatorLampFile = vca.VehicleTypeCode.PassiveAntiTheftIndicatorLampFile;
                    def.ServiceThrottleSoonIndicatorLampFile = vca.VehicleTypeCode.ServiceThrottleSoonIndicatorLampFile;
                    def.MonitorType = vca.VehicleTypeCode.MonitorType;
                    def.MonitorFile = vca.VehicleTypeCode.MonitorFile;

                    def.Model = vca.VehicleType.Model;
                    def.Make = vca.VehicleType.Make;
                    def.Year = vca.VehicleType.Year;
                    def.EngineType = vca.VehicleType.EngineType;
                    def.EngineVINCode = vca.VehicleType.EngineVINCode;
                    def.EngineTypeVINLookup = vca.VehicleType.EngineTypeVINLookup;
                    def.BodyCode = vca.VehicleType.BodyCode;
                    def.TransmissionControlType = vca.VehicleType.TransmissionControlType;

                    //set the layman terms when loading them in...
                    this.SetLaymanTermProperties(vca.ErrorCode, def);

                    this.DiagnosticReportResultErrorCodeDefinitions.Add(def);

                    this.unableToFindCodeData = false;
                }
            }
        }

        //Re-AddChiltonDTC

        /// <summary>
        /// This method will set the Title and Conditions to layman's terms if any exist.
        /// </summary>
        /// <param name="errorCode">The <see cref="string"/> error code.</param>
        /// <param name="def">The <see cref="DiagnosticReportResultErrorCodeDefinition"/> to be updated.</param>
        private void SetLaymanTermProperties(string errorCode, DiagnosticReportResultErrorCodeDefinition def)
        {
            DtcCodeLaymanTerm dtcCodeLaymanTerm = null;

            // Look up the layman's term object
            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(ConnectionString))
            {
                string make = "";

                if (this.DiagnosticReport != null && this.Vehicle != null)
                {
                    make = (this.Vehicle.PolkVehicleYMME != null) ? this.Vehicle.PolkVehicleYMME.Make : this.Vehicle.Make;
                }

                dr.ProcedureName = "DTCCodeLaymanTerm_LoadByErrorCodeAndMake";
                dr.AddNVarChar("ErrorCode", errorCode);
                dr.AddNVarChar("Make", make);

                dr.Execute();

                if (dr.Read())
                {
                    dtcCodeLaymanTerm = (DtcCodeLaymanTerm)this.Registry.CreateInstance(typeof(DtcCodeLaymanTerm), dr.GetGuid("DtcCodeLaymanTermId"));
                    dtcCodeLaymanTerm.LoadPropertiesFromDataReader(dr, true);
                }
            }

            // If we found a layman's term object then update the definition.
            if (dtcCodeLaymanTerm != null)
            {
                def.LaymansTermTitle = dtcCodeLaymanTerm.Title;
                def.LaymansTermTitle_es = dtcCodeLaymanTerm.Title_es;
                def.LaymansTermTitle_fr = dtcCodeLaymanTerm.Title_fr;
                def.LaymansTermTitle_zh = dtcCodeLaymanTerm.Title_zh;
                def.LaymansTermDescription = dtcCodeLaymanTerm.Description;
                def.LaymansTermDescription_es = dtcCodeLaymanTerm.Description_es;
                def.LaymansTermDescription_fr = dtcCodeLaymanTerm.Description_fr;
                def.LaymansTermDescription_zh = dtcCodeLaymanTerm.Description_zh;
                def.LaymansTermConditions = dtcCodeLaymanTerm.Description;
                def.LaymansTermConditions_es = dtcCodeLaymanTerm.Description_es;
                def.LaymansTermConditions_fr = dtcCodeLaymanTerm.Description_fr;
                def.LaymansTermConditions_zh = dtcCodeLaymanTerm.Description_zh;
                def.LaymansTermSeverityLevel = dtcCodeLaymanTerm.SeverityLevel;
                def.LaymansTermEffectOnVehicle = dtcCodeLaymanTerm.EffectOnVehicle;
                def.LaymansTermEffectOnVehicle_es = dtcCodeLaymanTerm.EffectOnVehicle_es;
                def.LaymansTermEffectOnVehicle_fr = dtcCodeLaymanTerm.EffectOnVehicle_fr;
                def.LaymansTermEffectOnVehicle_zh = dtcCodeLaymanTerm.EffectOnVehicle_zh;
                def.LaymansTermResponsibleComponentOrSystem = dtcCodeLaymanTerm.ResponsibleComponentOrSystem;
                def.LaymansTermResponsibleComponentOrSystem_es = dtcCodeLaymanTerm.ResponsibleComponentOrSystem_es;
                def.LaymansTermResponsibleComponentOrSystem_fr = dtcCodeLaymanTerm.ResponsibleComponentOrSystem_fr;
                def.LaymansTermResponsibleComponentOrSystem_zh = dtcCodeLaymanTerm.ResponsibleComponentOrSystem_zh;
                def.LaymansTermWhyItsImportant = dtcCodeLaymanTerm.WhyItsImportant;
                def.LaymansTermWhyItsImportant_es = dtcCodeLaymanTerm.WhyItsImportant_es;
                def.LaymansTermWhyItsImportant_fr = dtcCodeLaymanTerm.WhyItsImportant_fr;
                def.LaymansTermWhyItsImportant_zh = dtcCodeLaymanTerm.WhyItsImportant_zh;
                def.LaymansTermSeverityLevelDefinition = dtcCodeLaymanTerm.SeverityDefinition;
                def.LaymansTermSeverityLevelDefinition_es = dtcCodeLaymanTerm.SeverityDefinition_es;
                def.LaymansTermSeverityLevelDefinition_fr = dtcCodeLaymanTerm.SeverityDefinition_fr;
                def.LaymansTermSeverityLevelDefinition_zh = dtcCodeLaymanTerm.SeverityDefinition_zh;
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
                //load the base diagnosticReportResultErrorCode if user selected it.
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
            dr.ProcedureName = "DiagnosticReportResultErrorCode_Load";
            dr.AddGuid("DiagnosticReportResultErrorCodeId", Id);
        }

        /// <summary>
        /// Sets the properties of this object from the fields in the recordset located in the supplied <see cref="SqlDataReaderWrapper"/>.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> containing the recordset and fields used to set the properties of this object (this layer only).</param>
        protected new void SetPropertiesFromDataReader(SqlDataReaderWrapper dr)
        {
            this.diagnosticReportResult = (DiagnosticReportResult)Registry.CreateInstance(typeof(DiagnosticReportResult), dr.GetGuid("DiagnosticReportResultId"));
            //Removed Chilton DTCs on 2018-10-17 10:52 AM by INNOVA Dev Team
            //Re-AddChiltonDTC
            if (!dr.IsDBNull("VehicleTypeCodeId"))
            {
                this.vehicleTypeCode = (VehicleTypeCode)Registry.CreateInstance(typeof(VehicleTypeCode), dr.GetGuid("VehicleTypeCodeId"));
            }
            //Re-AddChiltonDTC
            if (!dr.IsDBNull("DtcCodeId"))
            {
                this.dtcCode = (DtcCode)Registry.CreateInstance(typeof(DtcCode), dr.GetGuid("DtcCodeId"));
            }
            if (!dr.IsDBNull("DtcMasterCodeListId"))
            {
                this.dtcMasterCodeList = (DtcMasterCodeList)Registry.CreateInstance(typeof(DtcMasterCodeList), dr.GetGuid("DtcMasterCodeListId"));
            }
            this.errorCode = dr.GetString("ErrorCode");
            this.errorCodeType = dr.GetString("ErrorCodeType");
            this.diagnosticReportErrorCodeType = (DiagnosticReportErrorCodeType)dr.GetInt32("DiagnosticReportErrorCodeType");
            this.diagnosticReportErrorCodeSystemType = (DiagnosticReportErrorCodeSystemType)dr.GetInt32("DiagnosticReportErrorCodeSystemType");
            this.sortOrder = dr.GetInt32("SortOrder");
            this.unableToFindCodeData = dr.GetBoolean("UnableToFindCodeData");

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
            if (IsObjectDirty)
            {
                transaction = EnsureDatabasePrepared(connection, transaction);

                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    if (IsObjectCreated)
                    {
                        dr.ProcedureName = "DiagnosticReportResultErrorCode_Create";
                    }
                    else
                    {
                        dr.ProcedureName = "DiagnosticReportResultErrorCode_Save";
                    }

                    dr.AddGuid("DiagnosticReportResultErrorCodeId", this.Id);
                    dr.AddGuid("DiagnosticReportResultId", this.DiagnosticReportResult.Id);
                    //Removed Chilton DTCs on 2018-10-12 2:00 PM by INNOVA Dev Team
                    //Re-AddChiltonDTC
                    if (this.VehicleTypeCode != null)
                    {
                        dr.AddGuid("VehicleTypeCodeId", this.VehicleTypeCode.Id);
                    }
                    //Re-AddChiltonDTC
                    if (this.DtcCode != null)
                    {
                        dr.AddGuid("DtcCodeId", this.DtcCode.Id);
                    }
                    if (this.DtcMasterCodeList != null)
                    {
                        dr.AddGuid("DtcMasterCodeListId", this.DtcMasterCodeList.Id);
                    }
                    dr.AddNVarChar("ErrorCode", this.ErrorCode);
                    dr.AddNVarChar("ErrorCodeType", this.ErrorCodeType);
                    dr.AddInt32("DiagnosticReportErrorCodeType", (int)this.DiagnosticReportErrorCodeType);
                    dr.AddInt32("DiagnosticReportErrorCodeSystemType", (int)this.DiagnosticReportErrorCodeSystemType);
                    dr.AddInt32("SortOrder", this.SortOrder);
                    dr.AddBoolean("UnableToFindCodeData", this.UnableToFindCodeData);

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
            if (this.diagnosticReportResultErrorCodeDefinitions != null)
            {
                for (int i = 0; i < this.diagnosticReportResultErrorCodeDefinitions.Removed.Count; i++)
                {
                    transaction = ((DiagnosticReportResultErrorCodeDefinition)this.diagnosticReportResultErrorCodeDefinitions.Removed[i]).Delete(connection, transaction);
                }
                for (int i = 0; i < this.diagnosticReportResultErrorCodeDefinitions.Count; i++)
                {
                    transaction = this.diagnosticReportResultErrorCodeDefinitions[i].Save(connection, transaction);
                }
            }

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

            // delete this current object
            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                //delete the possible causes for the error code
                dr.ProcedureName = "DiagnosticReportResultPossibleCause_DeleteByDiagnosticReportResultErrorCode";
                dr.AddGuid("DiagnosticReportResultErrorCodeId", Id);
                dr.ExecuteNonQuery(transaction);
                dr.ClearParameters();

                //delete the definitions for the error code
                dr.ProcedureName = "DiagnosticReportResultErrorCodeDefinition_DeleteByDiagnosticReportResultErrorCode";
                dr.AddGuid("DiagnosticReportResultErrorCodeId", Id);
                dr.ExecuteNonQuery(transaction);
                dr.ClearParameters();

                //delete the diagnosticReportResultErrorCode
                dr.ProcedureName = "DiagnosticReportResultErrorCode_Delete";
                dr.AddGuid("DiagnosticReportResultErrorCodeId", Id);
                dr.ExecuteNonQuery(transaction);
            }

            transaction = base.Delete(connection, transaction);

            return transaction;
        }

        #endregion Required Object Methods (Load, Save, Delete, Etc)
    }
}
using Innova.Users;
using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using Metafuse3.NullableTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Innova.DtcCodes
{
    /// <summary>
    /// The DtcCode object handles the business logic and data access for the specialized business object, DtcCode.
    /// The class inherits from <see cref="BusinessObjectBase"/> class.
    /// </summary>
    /// <example>
    /// See the examples of how to construct the DtcCode object.
    ///
    /// To create a new instance of a new of DtcCode.
    /// <code>DtcCode o = (DtcCode)Registry.CreateInstance(typeof(DtcCode));</code>
    ///
    /// To create an new instance of an existing DtcCode.
    /// <code>DtcCode o = (DtcCode)Registry.CreateInstance(typeof(DtcCode), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of DtcCode, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectBase.ConnectionString"/> to the <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable()]
    [ClassDefinition("DTC", "DTCs", "DTC", "DtcCodeId")]
    public class DtcCode : InnovaBusinessObjectBase
    {
        // data object variables
        private string manufacturerName = "";

        private string errorCode = "";
        private AdminUser createdByAdminUser;
        private AdminUser updatedByAdminUser;
        private AdminUser approvedByAdminUser;
        private bool isApproved;
        private NullableDateTime approvedDateTimeUTC = NullableDateTime.Null;

        private bool hasEngineTypeDefined = false;
        private string engineTypesString = "";
        private bool isEngineTypesDirty = false;

        private bool hasEngineVINCodeDefined;
        private string engineVINCodesString = "";
        private bool isEngineVINCodesDirty = false;

        private bool hasYearDefined = false;
        private string yearsString = "";
        private bool isYearsDirty = false;

        private bool hasMakeDefined = false;
        private string makesString = "";
        private bool isMakesDirty = false;

        private bool hasModelDefined = false;
        private string modelsString = "";
        private bool isModelsDirty = false;

        private bool hasTrimLevelDefined = false;
        private string trimLevelsString = "";
        private bool isTrimLevelsDirty = false;

        private bool hasTransmissionDefined = false;
        private string transmissionsString = "";
        private bool isTransmissionsDirty = false;

        private string title = "";
        private string title_es = "";
        private string title_fr = "";
        private string title_zh = "";
        private string possibleCauses = "";
        private string possibleCauses_es = "";
        private string possibleCauses_fr = "";
        private string possibleCauses_zh = "";
        private string conditions = "";
        private string conditions_es = "";
        private string conditions_fr = "";
        private string conditions_zh = "";
        private int frequencyCount;
        private int trips;
        private string messageIndicatorLampFile = "";
        private string transmissionControlIndicatorLampFile = "";
        private string passiveAntiTheftIndicatorLampFile = "";
        private string serviceThrottleSoonIndicatorLampFile = "";
        private string monitorType = "";
        private string monitorFile = "";
        private DateTime createdDateTimeUTC;
        private DateTime updatedDateTimeUTC;

        private List<string> engineTypes;
        private List<string> engineVINCodes;
        private List<string> makes;
        private List<string> models;
        private List<string> trimLevels;
        private List<string> transmissionControlTypes;
        private List<int> years;

        #region System Constructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new (one that does not exist, assigning a new <see cref="Guid"/> Id). DtcCode object.
        /// In order to create a new DtcCode which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <example>
        /// <code>
        /// DtcCode o = (DtcCode)Registry.CreateInstance(typeof(DtcCode));
        /// </code>
        /// </example>
        protected internal DtcCode()
            : base()
        {
            IsObjectCreated = true;
            IsObjectLoaded = true;
            IsObjectDirty = true;
        }

        /// <summary>
        /// Constructor creates an existing (one that exists, and already has a <see cref="Guid"/> Id).  DtcCode object.
        /// In order to create an existing DtcCode object, which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <param name="id">The <see cref="Guid"/> or SQL uniqueidentifier indentification "Id" of the current object.</param>
        /// <example>
        /// <code>
        /// DtcCode o = (DtcCode)Registry.CreateInstance(typeof(DtcCode), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));
        /// </code>
        /// </example>
        protected internal DtcCode(Guid id)
            : base(id)
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
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        [PropertyDefinition("Manufacturer", "Name of the DTC's Manufacturer.")]
        public string ManufacturerName
        {
            get
            {
                EnsureLoaded();
                return manufacturerName;
            }
            set
            {
                EnsureLoaded();
                if (manufacturerName != value)
                {
                    IsObjectDirty = true;
                    manufacturerName = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        [PropertyDefinition("Error Code", "Code Value of the Diagnostic Trouble Code.")]
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
        /// Gets or sets the <see cref="AdminUser"/>
        /// </summary>
        public AdminUser CreatedByAdminUser
        {
            get
            {
                EnsureLoaded();
                return createdByAdminUser;
            }
            set
            {
                EnsureLoaded();
                if (createdByAdminUser != value)
                {
                    IsObjectDirty = true;
                    createdByAdminUser = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="AdminUser"/>
        /// </summary>
        public AdminUser UpdatedByAdminUser
        {
            get
            {
                EnsureLoaded();
                return updatedByAdminUser;
            }
            set
            {
                EnsureLoaded();
                if (updatedByAdminUser != value)
                {
                    IsObjectDirty = true;
                    updatedByAdminUser = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="AdminUser"/>
        /// </summary>
        public AdminUser ApprovedByAdminUser
        {
            get
            {
                EnsureLoaded();
                return approvedByAdminUser;
            }
            set
            {
                EnsureLoaded();
                if (approvedByAdminUser != value)
                {
                    IsObjectDirty = true;
                    approvedByAdminUser = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/>
        /// </summary>
        [PropertyDefinition("Approved", "Indicates that the DTC has been Approved.")]
        public bool IsApproved
        {
            get
            {
                EnsureLoaded();
                return isApproved;
            }
            set
            {
                EnsureLoaded();
                if (isApproved != value)
                {
                    IsObjectDirty = true;
                    isApproved = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/>
        /// </summary>
        [PropertyDefinition("Approved", "Name or Description of the part.")]
        public NullableDateTime ApprovedDateTimeUTC
        {
            get
            {
                EnsureLoaded();
                return approvedDateTimeUTC;
            }
            set
            {
                EnsureLoaded();
                if (approvedDateTimeUTC != value)
                {
                    IsObjectDirty = true;
                    approvedDateTimeUTC = value;
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="bool"/> flag indicating whether or not the engine type is defined.
        /// </summary>
        public bool HasEngineTypeDefined
        {
            get
            {
                EnsureLoaded();
                return hasEngineTypeDefined;
            }
        }

        /// <summary>
        /// Gets the <see cref="bool"/> flag indicating whether or not the engine type is defined.
        /// </summary>
        public bool HasEngineVINCodeDefined
        {
            get
            {
                EnsureLoaded();
                return hasEngineVINCodeDefined;
            }
        }

        /// <summary>
        /// Gets the <see cref="bool"/> flag indicating whether or not the year is defined.
        /// </summary>
        public bool HasYearDefined
        {
            get
            {
                EnsureLoaded();
                return hasYearDefined;
            }
        }

        /// <summary>
        /// Gets the <see cref="bool"/> flag indicating whether or not the make is defined.
        /// </summary>
        public bool HasMakeDefined
        {
            get
            {
                EnsureLoaded();
                return hasMakeDefined;
            }
        }

        /// <summary>
        /// Gets the <see cref="bool"/> flag indicating whether or not the model is defined.
        /// </summary>
        public bool HasModelDefined
        {
            get
            {
                EnsureLoaded();
                return hasModelDefined;
            }
        }

        /// <summary>
        /// Gets the <see cref="bool"/> flag indicating whether or not the trim level is defined.
        /// </summary>
        public bool HasTrimLevelDefined
        {
            get
            {
                EnsureLoaded();
                return hasTrimLevelDefined;
            }
        }

        /// <summary>
        /// Gets the <see cref="bool"/> flag indicating whether or not the transmission control type is defined.
        /// </summary>
        public bool HasTransmissionDefined
        {
            get
            {
                EnsureLoaded();
                return hasTransmissionDefined;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        [PropertyDefinition("Definition", "Title or Definition of the DTC.")]
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
        [PropertyDefinition("Definition - Spanish", "Title or Definition of the DTC in Spanish.")]
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
        [PropertyDefinition("Definition - French", "Title or Definition of the DTC in French.")]
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
        [PropertyDefinition("Definition - Mandarin", "Title or Definition of the DTC in Mandarin.")]
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
        /// Gets or sets the <see cref="string"/> title in the language specified in the <see cref="RuntimeInfo"/>.
        /// </summary>
        [PropertyDefinition("Definition - Translated", "Title or Definition of the DTC Translated into another language.")]
        public string Title_Translated
        {
            get
            {
                this.EnsureLoaded();
                return this.RuntimeInfo.GetTranslatedValue(this.Title, this.Title_es, this.Title_fr, this.Title_zh);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        [PropertyDefinition("Possible Causes", "Explaination of the possible cause of the DTC")]
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
        [PropertyDefinition("Possible Causes - Spanish", "Explaination of the possible cause of the DTC in Spanish")]
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
        [PropertyDefinition("Possible Causes - French", "Explaination of the possible cause of the DTC in French")]
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
        [PropertyDefinition("Possible Causes - Mandarin", "Explaination of the possible cause of the DTC in Mandarin")]
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
        /// Gets or sets the
        /// </summary>
        [PropertyDefinition("Possible Causes", "Explaination of the possible cause of the DTC.")]
        public string PossibleCauses_Translated
        {
            get
            {
                this.EnsureLoaded();
                return this.RuntimeInfo.GetTranslatedValue(this.PossibleCauses, this.PossibleCauses_es, this.PossibleCauses_fr, this.PossibleCauses_zh);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        [PropertyDefinition("Conditions", "Conditions of the DTC")]
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
        [PropertyDefinition("Conditions - Spanish", "Conditions of the DTC in Spanish")]
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
        [PropertyDefinition("Conditions - French", "Conditions of the DTC in French")]
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
        [PropertyDefinition("Conditions - Mandarin", "Conditions of the DTC in Mandarin")]
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
        /// Gets or sets the <see cref="string"/> conditions in the language specified in the <see cref="RuntimeInfo"/>.
        /// </summary>
        [PropertyDefinition("Conditions", "Conditions of the DTC")]
        public string Conditions_Translated
        {
            get
            {
                this.EnsureLoaded();
                return this.RuntimeInfo.GetTranslatedValue(this.Conditions, this.Conditions_es, this.Conditions_fr, this.Conditions_zh);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/>
        /// </summary>
        [PropertyDefinition("Frequency Count", "Frequency of the DTC.")]
        public int FrequencyCount
        {
            get
            {
                EnsureLoaded();
                return frequencyCount;
            }
            set
            {
                EnsureLoaded();
                if (frequencyCount != value)
                {
                    IsObjectDirty = true;
                    frequencyCount = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/>
        /// </summary>
        [PropertyDefinition("Trips", "Number of consecutive trips that report the DTC before the MIL is turned on.")]
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
        [PropertyDefinition("Malfunction Indicator Lamp file", "Name of the file containing the information for the Malfunction Indicator Lamp")]
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
        [PropertyDefinition("Transmission Control Indicator Lamp File", "Name of the file containing the information for the Transmission Control Indicator Lamp")]
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
        [PropertyDefinition("Passive AntiTheft Indicator Lamp File", "Name of the file containing the information for the AntiTheift Indicator Lamp")]
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
        [PropertyDefinition("Service Throttle Soon Indicator Lamp File", "Name of the file containing the information for the Service Throttle Soon indicator lamp.")]
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
        [PropertyDefinition("Monitor Type", "Type of monitor for the DTC")]
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
        [PropertyDefinition("Monitor File", "Information file for the DTC Monitor.")]
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
        /// Gets or sets the <see cref="DateTime"/>
        /// </summary>
        public DateTime CreatedDateTimeUTC
        {
            get
            {
                EnsureLoaded();
                return createdDateTimeUTC;
            }
            set
            {
                EnsureLoaded();
                if (createdDateTimeUTC != value)
                {
                    IsObjectDirty = true;
                    createdDateTimeUTC = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/>
        /// </summary>
        public DateTime UpdatedDateTimeUTC
        {
            get
            {
                EnsureLoaded();
                return updatedDateTimeUTC;
            }
            set
            {
                EnsureLoaded();
                if (updatedDateTimeUTC != value)
                {
                    IsObjectDirty = true;
                    updatedDateTimeUTC = value;
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="int"/> count number of elements that have a specific item called out on the error code.
        /// </summary>
        public int HasDefinedCount
        {
            get
            {
                int count = 0;

                if (this.HasMakeDefined)
                {
                    count++;
                }
                if (this.HasModelDefined)
                {
                    count++;
                }
                if (this.HasYearDefined)
                {
                    count++;
                }
                if (this.HasEngineVINCodeDefined)
                {
                    count++;
                }
                if (this.HasTransmissionDefined)
                {
                    count++;
                }

                return count;
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
        /// Gets the <see cref="List{String}"/> of vehicle engine types that apply to this fix.
        /// NOTE: DO NOT add to this collection directly. Use the AddEngineType() method.
        /// </summary>
        [PropertyDefinition("Engines", "Types of Engines that the DTC is associated with")]
        public List<string> EngineTypes
        {
            get
            {
                this.EnsureLoaded();

                if (this.engineTypes == null)
                {
                    this.engineTypes = new List<string>();

                    if (!isObjectCreated && this.engineTypesString != "")
                    {
                        this.engineTypes = new List<string>(this.engineTypesString.Split("|".ToCharArray()));
                    }
                }
                return this.engineTypes;
            }
        }

        /// <summary>
        /// Gets the <see cref="List{String}"/> of vehicle engine VIN codes that apply to this fix.
        /// NOTE: DO NOT add to this collection directly. Use the AddEngineVINCode() method.
        /// </summary>
        [PropertyDefinition("Engine VIN Code", "VIN Code the engine is assocated with")]
        public List<string> EngineVINCodes
        {
            get
            {
                this.EnsureLoaded();

                if (this.engineVINCodes == null)
                {
                    this.engineVINCodes = new List<string>();

                    if (!isObjectCreated && this.engineVINCodesString != "")
                    {
                        this.engineVINCodes = new List<string>(this.engineVINCodesString.Split("|".ToCharArray()));
                    }
                }
                return this.engineVINCodes;
            }
        }

        /// <summary>
        /// Get the <see cref="List{String}"/> of vehicle makes that apply to this fix.
        /// NOTE: DO NOT add to this collection directly. Use the AddMake() method.
        /// </summary>
        [PropertyDefinition("Makes", "Makes that the DTC is associated with.")]
        public List<string> Makes
        {
            get
            {
                this.EnsureLoaded();

                if (this.makes == null)
                {
                    this.makes = new List<string>();

                    if (!isObjectCreated && this.makesString != "")
                    {
                        this.makes = new List<string>(this.makesString.Split("|".ToCharArray()));
                    }
                }

                return this.makes;
            }
        }

        /// <summary>
        /// Get the <see cref="List{String}"/> of vehicle models that apply to this fix.
        /// NOTE: DO NOT add to this collection directly. Use the AddModel() method.
        /// </summary>
        [PropertyDefinition("Models", "Models that the DTC is associated with")]
        public List<String> Models
        {
            get
            {
                this.EnsureLoaded();

                if (this.models == null)
                {
                    this.models = new List<string>();

                    if (!isObjectCreated && this.modelsString != "")
                    {
                        this.models = new List<string>(this.modelsString.Split("|".ToCharArray()));
                    }
                }

                return this.models;
            }
        }

        /// <summary>
        /// Get the <see cref="List{String}"/> of vehicle trim levels that apply to this fix.
        /// NOTE: DO NOT add to this collection directly. Use the AddTrimLevel() method.
        /// </summary>
        [PropertyDefinition("Trim Levels", "Trim Levels associated with this DTC.")]
        public List<string> TrimLevels
        {
            get
            {
                this.EnsureLoaded();

                if (this.trimLevels == null)
                {
                    this.trimLevels = new List<string>();

                    if (!isObjectCreated && this.trimLevelsString != "")
                    {
                        this.trimLevels = new List<string>(this.trimLevelsString.Split("|".ToCharArray()));
                    }
                }

                return this.trimLevels;
            }
        }

        /// <summary>
        /// Get the <see cref="List{String}"/> of transmission control types that apply to this fix.
        /// NOTE: DO NOT add to this collection directly. Use the AddTransmissionControlType() method.
        /// </summary>
        [PropertyDefinition("Transmission Control Type", "Types of Transmissiosn that are associated witht his DTC.")]
        public List<string> TransmissionControlTypes
        {
            get
            {
                this.EnsureLoaded();

                if (this.transmissionControlTypes == null)
                {
                    this.transmissionControlTypes = new List<string>();

                    if (!isObjectCreated && this.transmissionsString != "")
                    {
                        this.transmissionControlTypes = new List<string>(this.transmissionsString.Split("|".ToCharArray()));
                    }
                }

                return this.transmissionControlTypes;
            }
        }

        /// <summary>
        /// Get an <see cref="List{Int32}"/> of <see cref="int"/> vehicle years that apply to this fix.
        /// NOTE: DO NOT add to this collection directly. Use the AddYear() method.
        /// </summary>
        [PropertyDefinition("Years", "Years that this DTC applies to.")]
        public List<int> Years
        {
            get
            {
                this.EnsureLoaded();

                if (this.years == null)
                {
                    this.years = new List<int>();

                    if (!isObjectCreated && this.yearsString != "")
                    {
                        foreach (string s in this.yearsString.Split("|".ToCharArray()))
                        {
                            if (s != null && s != "")
                            {
                                this.years.Add(Int32.Parse(s));
                            }
                        }
                    }
                }

                return this.years;
            }
        }

        #endregion Object Properties (Related Objects)

        #region Business Logic Methods

        /***********************************************************************************************
		 *
		 * Custom Business Logic
		 *
		 * **********************************************************************************************/

        private static string GetOBD1ErrorMessage(string manufacturerName, int year)
        {
            string errorMessage = "";

            if (year <= 1995)
            {
                switch (manufacturerName.ToLower())
                {
                    case "gm":
                    case "isuzu":
                    case "chrysler":
                    case "jeep":
                    case "nissan":
                    case "infinity":
                        errorMessage = "must be a 2 digit number.";
                        break;

                    case "ford":
                    case "lincoln":
                    case "mercury":
                        errorMessage = "must be a 2 or 3 digit number.";
                        break;

                    case "honda":
                    case "acura":
                    case "toyota":
                    case "lexus":
                        errorMessage = "must be a 1 or 2 digit number.";
                        break;

                    default:
                        errorMessage = "must be a 1 to 3 digit number.";
                        break;
                }
            }

            return errorMessage;
        }

        private static string GetOBD2ErrorMessage(string manufacturerName, int year)
        {
            string errorMessage = "";

            if (year >= 1996
                || (year >= 1994 &&
                (
                    manufacturerName.ToLower() == "gm"
                    || manufacturerName.ToLower() == "ford"
                    || manufacturerName.ToLower() == "chrysler"
                    || manufacturerName.ToLower() == "toyota"
                )))
            {
                errorMessage = "must be 5 characters in length and it must begin with B, C, P or U followed by a number and then 3 letters or numbers (example: P0141).";
            }

            return errorMessage;
        }

        /// <summary>
        /// Builds a string list from the enumerable list spearated by a pipe "|" symbol.
        /// </summary>
        /// <param name="list"><see cref="IEnumerable"/> list.</param>
        /// <returns><see cref="string"/> of the list separated by the pipe symbol.</returns>
        private string BuildStringList(IEnumerable list)
        {
            string s = "";

            foreach (object o in list)
            {
                string os = o.ToString();

                if (os != null && os != "")
                {
                    if (s.Length > 0)
                    {
                        s += "|";
                    }
                    s += os.Trim();
                }
            }
            return s;
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
                //load the base dtcCode if user selected it.
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
            dr.ProcedureName = "DTCCode_Load";
            dr.AddGuid("DTCCodeId", Id);
        }

        /// <summary>
        /// Sets the properties of this object from the fields in the recordset located in the supplied <see cref="SqlDataReaderWrapper"/>.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> containing the recordset and fields used to set the properties of this object (this layer only).</param>
        protected new void SetPropertiesFromDataReader(SqlDataReaderWrapper dr)
        {
            this.manufacturerName = dr.GetString("ManufacturerName");
            this.errorCode = dr.GetString("ErrorCode");
            this.createdByAdminUser = (AdminUser)Registry.CreateInstance(typeof(AdminUser), dr.GetGuid("CreatedByAdminUserId"));
            this.updatedByAdminUser = (AdminUser)Registry.CreateInstance(typeof(AdminUser), dr.GetGuid("UpdatedByAdminUserId"));
            if (!dr.IsDBNull("ApprovedByAdminUserId"))
            {
                this.approvedByAdminUser = (AdminUser)Registry.CreateInstance(typeof(AdminUser), dr.GetGuid("ApprovedByAdminUserId"));
            }
            this.isApproved = dr.GetBoolean("IsApproved");
            this.approvedDateTimeUTC = dr.GetNullableDateTime("ApprovedDateTimeUTC");
            this.hasEngineTypeDefined = dr.GetBoolean("HasEngineTypeDefined");
            this.engineTypesString = dr.GetString("EngineTypesString");
            this.hasEngineVINCodeDefined = dr.GetBoolean("HasEngineVINCodeDefined");
            this.engineVINCodesString = dr.GetString("EngineVINCodesString");
            this.hasYearDefined = dr.GetBoolean("HasYearDefined");
            this.yearsString = dr.GetString("YearsString");
            this.hasMakeDefined = dr.GetBoolean("HasMakeDefined");
            this.makesString = dr.GetString("MakesString");
            this.hasModelDefined = dr.GetBoolean("HasModelDefined");
            this.modelsString = dr.GetString("ModelsString");
            this.hasTrimLevelDefined = dr.GetBoolean("HasTrimLevelDefined");
            this.trimLevelsString = dr.GetString("TrimLevelsString");
            this.hasTransmissionDefined = dr.GetBoolean("HasTransmissionDefined");
            this.transmissionsString = dr.GetString("TransmissionsString");
            this.title = dr.GetString("Title");
            this.title_es = dr.GetString("Title_es");
            this.title_fr = dr.GetString("Title_fr");
            this.title_zh = dr.GetString("Title_zh");
            this.possibleCauses = dr.GetString("PossibleCauses");
            this.possibleCauses_es = dr.GetString("PossibleCauses_es");
            this.possibleCauses_fr = dr.GetString("PossibleCauses_fr");
            this.possibleCauses_zh = dr.GetString("PossibleCauses_zh");
            this.conditions = dr.GetString("Conditions");
            this.conditions_es = dr.GetString("Conditions_es");
            this.conditions_fr = dr.GetString("Conditions_fr");
            this.conditions_zh = dr.GetString("Conditions_zh");
            this.frequencyCount = dr.GetInt32("FrequencyCount");
            this.trips = dr.GetInt32("Trips");
            this.messageIndicatorLampFile = dr.GetString("MessageIndicatorLampFile");
            this.transmissionControlIndicatorLampFile = dr.GetString("TransmissionControlIndicatorLampFile");
            this.passiveAntiTheftIndicatorLampFile = dr.GetString("PassiveAntiTheftIndicatorLampFile");
            this.serviceThrottleSoonIndicatorLampFile = dr.GetString("ServiceThrottleSoonIndicatorLampFile");
            this.monitorType = dr.GetString("MonitorType");
            this.monitorFile = dr.GetString("MonitorFile");
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
            if (IsObjectDirty)
            {
                transaction = EnsureDatabasePrepared(connection, transaction);

                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    if (IsObjectCreated)
                    {
                        dr.ProcedureName = "DTCCode_Create";
                        CreatedDateTimeUTC = DateTime.Now.ToUniversalTime();
                        UpdatedDateTimeUTC = CreatedDateTimeUTC;
                    }
                    else
                    {
                        dr.ProcedureName = "DTCCode_Save";
                    }

                    dr.AddGuid("DTCCodeId", Id);
                    dr.AddNVarChar("ManufacturerName", ManufacturerName);
                    dr.AddNVarChar("ErrorCode", ErrorCode);
                    dr.AddGuid("CreatedByAdminUserId", CreatedByAdminUser.Id);
                    dr.AddGuid("UpdatedByAdminUserId", UpdatedByAdminUser.Id);
                    if (ApprovedByAdminUser != null)
                    {
                        dr.AddGuid("ApprovedByAdminUserId", ApprovedByAdminUser.Id);
                    }
                    dr.AddBoolean("IsApproved", IsApproved);
                    if (!ApprovedDateTimeUTC.IsNull)
                    {
                        dr.AddDateTime("ApprovedDateTimeUTC", ApprovedDateTimeUTC.Value);
                    }
                    dr.AddBoolean("HasEngineTypeDefined", this.HasEngineTypeDefined);

                    if (this.isEngineTypesDirty)
                    {
                        this.engineTypesString = this.BuildStringList(this.EngineTypes);
                    }
                    dr.AddNVarChar("EngineTypesString", this.engineTypesString);

                    dr.AddBoolean("HasEngineVINCodeDefined", this.HasEngineVINCodeDefined);
                    if (this.isEngineVINCodesDirty)
                    {
                        this.engineVINCodesString = this.BuildStringList(this.EngineVINCodes);
                    }
                    dr.AddNVarChar("EngineVINCodesString", this.engineVINCodesString);

                    dr.AddBoolean("HasYearDefined", HasYearDefined);
                    if (this.isYearsDirty)
                    {
                        this.yearsString = this.BuildStringList(this.Years);
                    }
                    dr.AddNVarChar("YearsString", this.yearsString);

                    dr.AddBoolean("HasMakeDefined", HasMakeDefined);
                    if (this.isMakesDirty)
                    {
                        this.makesString = this.BuildStringList(this.Makes);
                    }
                    dr.AddNVarChar("MakesString", this.makesString);

                    dr.AddBoolean("HasModelDefined", HasModelDefined);
                    if (this.isModelsDirty)
                    {
                        this.modelsString = this.BuildStringList(this.Models);
                    }
                    dr.AddNVarChar("ModelsString", this.modelsString);

                    dr.AddBoolean("HasTrimLevelDefined", HasTrimLevelDefined);
                    if (this.isTrimLevelsDirty)
                    {
                        this.trimLevelsString = this.BuildStringList(this.TrimLevels);
                    }
                    dr.AddNVarChar("TrimLevelsString", this.trimLevelsString);

                    dr.AddBoolean("HasTransmissionDefined", HasTransmissionDefined);
                    if (this.isTransmissionsDirty)
                    {
                        this.transmissionsString = this.BuildStringList(this.TransmissionControlTypes);
                    }
                    dr.AddNVarChar("TransmissionsString", this.transmissionsString);

                    dr.AddNVarChar("Title", this.Title);
                    dr.AddNVarChar("Title_es", this.Title_es);
                    dr.AddNVarChar("Title_fr", this.Title_fr);
                    dr.AddNVarChar("Title_zh", this.Title_zh);
                    dr.AddNVarChar("PossibleCauses", this.PossibleCauses);
                    dr.AddNVarChar("PossibleCauses_es", this.PossibleCauses_es);
                    dr.AddNVarChar("PossibleCauses_fr", this.PossibleCauses_fr);
                    dr.AddNVarChar("PossibleCauses_zh", this.PossibleCauses_zh);
                    dr.AddNVarChar("Conditions", this.Conditions);
                    dr.AddNVarChar("Conditions_es", this.Conditions_es);
                    dr.AddNVarChar("Conditions_fr", this.Conditions_fr);
                    dr.AddNVarChar("Conditions_zh", this.Conditions_zh);
                    dr.AddInt32("FrequencyCount", FrequencyCount);
                    dr.AddInt32("Trips", Trips);
                    dr.AddNVarChar("MessageIndicatorLampFile", MessageIndicatorLampFile);
                    dr.AddNVarChar("TransmissionControlIndicatorLampFile", TransmissionControlIndicatorLampFile);
                    dr.AddNVarChar("PassiveAntiTheftIndicatorLampFile", PassiveAntiTheftIndicatorLampFile);
                    dr.AddNVarChar("ServiceThrottleSoonIndicatorLampFile", ServiceThrottleSoonIndicatorLampFile);
                    dr.AddNVarChar("MonitorType", MonitorType);
                    dr.AddNVarChar("MonitorFile", MonitorFile);
                    dr.AddDateTime("CreatedDateTimeUTC", CreatedDateTimeUTC);
                    dr.AddDateTime("UpdatedDateTimeUTC", UpdatedDateTimeUTC);

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
            if (this.isEngineVINCodesDirty)
            {
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    dr.ProcedureName = "DTCCode_SaveEngineVINCodes";
                    dr.AddGuid("DTCCodeId", Id);
                    dr.AddNText("EngineVINCodesXmlList", Metafuse3.Xml.XmlList.ToXml(this.EngineVINCodes));
                    dr.ExecuteNonQuery(transaction);
                }
                this.isEngineVINCodesDirty = false;
            }

            if (this.isEngineTypesDirty)
            {
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    dr.ProcedureName = "DTCCode_SaveEngineTypes";
                    dr.AddGuid("DTCCodeId", Id);
                    dr.AddNText("EngineTypesXmlList", Metafuse3.Xml.XmlList.ToXml(this.EngineTypes));
                    dr.ExecuteNonQuery(transaction);
                }
                this.isEngineTypesDirty = false;
            }

            if (this.isMakesDirty)
            {
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    dr.ProcedureName = "DTCCode_SaveMakes";
                    dr.AddGuid("DTCCodeId", Id);
                    dr.AddNText("MakesXmlList", Metafuse3.Xml.XmlList.ToXml(this.Makes));
                    dr.ExecuteNonQuery(transaction);
                }
                this.isMakesDirty = false;
            }
            if (this.isModelsDirty)
            {
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    dr.ProcedureName = "DTCCode_SaveModels";
                    dr.AddGuid("DTCCodeId", Id);
                    dr.AddNText("ModelsXmlList", Metafuse3.Xml.XmlList.ToXml(this.Models));
                    dr.ExecuteNonQuery(transaction);
                }
                this.isModelsDirty = false;
            }
            if (this.isTrimLevelsDirty)
            {
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    dr.ProcedureName = "DTCCode_SaveTrimLevels";
                    dr.AddGuid("DTCCodeId", Id);
                    dr.AddNText("TrimLevelsXmlList", Metafuse3.Xml.XmlList.ToXml(this.TrimLevels));
                    dr.ExecuteNonQuery(transaction);
                }
                this.isTrimLevelsDirty = false;
            }
            if (this.isYearsDirty)
            {
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    dr.ProcedureName = "DTCCode_SaveYears";
                    dr.AddGuid("DTCCodeId", Id);
                    dr.AddNText("YearsXmlList", Metafuse3.Xml.XmlList.ToXml(this.Years));
                    dr.ExecuteNonQuery(transaction);
                }
                this.isYearsDirty = false;
            }
            if (this.isTransmissionsDirty)
            {
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    dr.ProcedureName = "DTCCode_SaveTransmissionControlTypes";
                    dr.AddGuid("DTCCodeId", Id);
                    dr.AddNText("TransmissionControlTypesXmlList", Metafuse3.Xml.XmlList.ToXml(TransmissionControlTypes));
                    dr.ExecuteNonQuery(transaction);
                }
                this.isTransmissionsDirty = false;
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

            //Copy deleted DTCCode to Audit DB first
            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                dr.ProcedureName = "Audit_CopyInnovaDTC";
                dr.AddGuid("DTCCodeId", Id);
                dr.ExecuteNonQuery(transaction);
            }
            //Copy deleted DTCCode to Audit DB first

            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                dr.ProcedureName = "DTCEngineType_DeleteByDTCCode";
                dr.AddGuid("DTCCodeId", Id);
                dr.ExecuteNonQuery(transaction);
            }

            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                dr.ProcedureName = "DTCEngineVINCode_DeleteByDTCCode";
                dr.AddGuid("DTCCodeId", Id);
                dr.ExecuteNonQuery(transaction);
            }

            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                dr.ProcedureName = "DTCMake_DeleteByDTCCode";
                dr.AddGuid("DTCCodeId", Id);
                dr.ExecuteNonQuery(transaction);
            }

            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                dr.ProcedureName = "DTCModel_DeleteByDTCCode";
                dr.AddGuid("DTCCodeId", Id);
                dr.ExecuteNonQuery(transaction);
            }

            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                dr.ProcedureName = "DTCTrimLevel_DeleteByDTCCode";
                dr.AddGuid("DTCCodeId", Id);
                dr.ExecuteNonQuery(transaction);
            }

            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                dr.ProcedureName = "DTCTransmission_DeleteByDTCCode";
                dr.AddGuid("DTCCodeId", Id);
                dr.ExecuteNonQuery(transaction);
            }

            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                dr.ProcedureName = "DTCYear_DeleteByDTCCode";
                dr.AddGuid("DTCCodeId", Id);
                dr.ExecuteNonQuery(transaction);
            }

            // delete this current object
            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                //delete the dtcCode
                dr.ProcedureName = "DtcCode_Delete";
                dr.AddGuid("DtcCodeId", Id);

                dr.ExecuteNonQuery(transaction);
            }

            transaction = base.Delete(connection, transaction);

            return transaction;
        }

        #endregion Required Object Methods (Load, Save, Delete, Etc)
    }
}
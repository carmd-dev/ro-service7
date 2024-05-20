using Innova.DiagnosticReports;
using Innova.Markets;
using Innova.ObdFixes;
using Innova.Symptoms;
using Innova.Users;
using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using Metafuse3.NullableTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;

namespace Innova.Fixes
{
    /// <summary>
    /// The Fix object handles the business logic and data access for the specialized business object, Fix.
    /// The class inherits from <see cref="BusinessObjectBase"/> class.
    /// </summary>
    /// <example>
    /// See the examples of how to construct the Fix object.
    ///
    /// To create a new instance of a new of Fix.
    /// <code>Fix o = (Fix)Registry.CreateInstance(typeof(Fix));</code>
    ///
    /// To create an new instance of an existing Fix.
    /// <code>Fix o = (Fix)Registry.CreateInstance(typeof(Fix), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of Fix, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectBase.ConnectionString"/> to the <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable()]
    [ClassDefinition("Fix", "Fixes", "Fix", "FixId")]
    public class Fix : InnovaBusinessObjectBase
    {
        /// <summary>
        /// The <see cref="string"/> encryption passphrase.
        /// </summary>
        public static string EncryptionPassphrase = "g7yoqIKVjTs19!&al5wN#hJsCggecd56NubJBqDs@71OM1@S!RcvoszfP2%7a00!76&kLIe5*Rst7af7";

        /// <summary>
        /// The <see cref="string"/> hash salt.
        /// </summary>
        public static string HashSalt = "&al5w";

        /// <summary>
        /// //#FixLogicStep
        /// </summary>
        public int FixLogicStep { get; set; }

        /// <summary>
        /// //#DiagnosticReportId
        /// </summary>
        public NullableGuid DiagnosticReportId { get; set; }

        private FixName fixName;
        private FixType fixType = FixType.Repair;
        private ObdFix obdFix;

        //Added on 2017-08-10 by INNOVA Dev Team (Nam Lu): Support ProRS Fix
        private ProRSFix proRsFix;

        private string primaryErrorCode = "";

        private string secondaryErrorCode = "";
        private AdminUser createdByAdminUser;
        private User createdByUser;
        private AdminUser updatedByAdminUser;
        private AdminUser approvedByAdminUser;

        private bool isApproved = false;
        private bool isApprovedChangedToTrue = false;

        private bool isInitiallyReviewed;
        private NullableDateTime approvedDateTimeUTC = NullableDateTime.Null;

        private string marketsString = "";
        private bool isMarketsDirty = false;

        private bool hasEngineTypeDefined = false;
        private string engineTypesString = "";
        private bool isEngineTypesDirty = false;

        private bool hasEngineVINCodeDefined;
        private string engineVINCodesString = "";
        private bool isEngineVINCodesDirty = false;

        private bool hasYearDefined = false;
        private string yearsString = "";
        private bool isYearsDirty = false;

        private bool hasGenerationDefined = false;
        private string generationsString = "";
        private bool isGenerationsDirty = false;

        private string makesString = "";

        private bool isMakesDirty = false;

        private bool hasModelDefined = false;
        private string modelsString = "";
        private bool isModelsDirty = false;

        private bool hastrimLevelDefined = false;
        private string trimLevelsString = "";
        private bool isTrimLevelsDirty = false;

        private bool hasTransmissionDefined = false;
        private string transmissionsString = "";
        private bool isTransmissionsDirty = false;

        private bool hasEngineTypeDefinedPolk = false;
        private string engineTypesStringPolk = "";
        private bool isEngineTypesDirtyPolk = false;
        private NullableDateTime engineTypePolkVerifiedDateTimeUTC = NullableDateTime.Null;

        private bool hasEngineVINCodeDefinedPolk;
        private string engineVINCodesStringPolk = "";
        private bool isEngineVINCodesDirtyPolk = false;
        private NullableDateTime engineVINCodePolkVerifiedDateTimeUTC = NullableDateTime.Null;

        private bool hasYearDefinedPolk = false;
        private string yearsStringPolk = "";
        private bool isYearsDirtyPolk = false;
        private NullableDateTime yearPolkVerifiedDateTimeUTC = NullableDateTime.Null;

        private bool hasMakeDefinedPolk = false;
        private string makesStringPolk = "";
        private bool isMakesDirtyPolk = false;
        private NullableDateTime makePolkVerifiedDateTimeUTC = NullableDateTime.Null;

        private bool hasModelDefinedPolk = false;
        private string modelsStringPolk = "";
        private bool isModelsDirtyPolk = false;
        private NullableDateTime modelPolkVerifiedDateTimeUTC = NullableDateTime.Null;

        private bool hastrimLevelDefinedPolk = false;
        private string trimLevelsStringPolk = "";
        private bool isTrimLevelsDirtyPolk = false;
        private NullableDateTime trimLevelPolkVerifiedDateTimeUTC = NullableDateTime.Null;

        //#FixManufacturer
        private bool hasManufacturerDefined = false;

        private string manufacturersString = "";
        private bool isManufacturersDirty = false;

        #region Legacy VinPower

        private bool hasEngineTypeDefinedVP = false;
        private string engineTypesStringVP = "";

        private bool hasEngineVINCodeDefinedVP;
        private string engineVINCodesStringVP = "";

        private bool hasYearDefinedVP = false;
        private string yearsStringVP = "";

        private bool hasMakeDefinedVP = false;
        private string makesStringVP = "";

        private bool hasModelDefinedVP = false;
        private string modelsStringVP = "";

        private bool hastrimLevelDefinedVP = false;
        private string trimLevelsStringVP = "";

        private bool hasTransmissionDefinedVP = false;
        private string transmissionsStringVP = "";

        private List<string> engineTypes;
        private List<string> engineVINCodes;
        private List<string> makes;
        private List<string> models;
        private List<string> trimLevels;
        private List<string> transmissionControlTypes;
        private List<int> years;
        private List<string> generations;

        //#FixManufacturer
        private List<string> manufacturers;

        #endregion Legacy VinPower

        private string description = "";
        private string description_es = "";
        private string description_fr = "";
        private string description_zh = "";
        private decimal labor;
        private decimal additionalCost;
        private int frequencyCount;
        private DateTime createdDateTimeUTC;
        private DateTime updatedDateTimeUTC;

        private FixPartCollection fixParts;
        private FixToolCollection fixTools; //ToolDB_

        private List<Market> markets;

        private List<string> engineTypesVP;

        private List<string> engineVINCodesVP;
        private List<string> makesVP;
        private List<string> modelsVP;
        private List<string> trimLevelsVP;
        private List<string> transmissionControlTypesVP;
        private List<int> yearsVP;

        private List<string> engineTypesPolk;
        private List<string> engineVINCodesPolk;
        private List<string> makesPolk;
        private List<string> modelsPolk;
        private List<string> trimLevelsPolk;
        private List<int> yearsPolk;

        private StringCollection secondaryErrorCodesList;

        private decimal? laborRate = null;
        private decimal? laborCost = null;
        private decimal? partsCost = null;
        private decimal? totalCost = null;
        private NullableDecimal laborRateInLocalCurrency = NullableDecimal.Null;
        private NullableDecimal laborCostInLocalCurrency = NullableDecimal.Null;
        private NullableDecimal additionalCostInLocalCurrency = NullableDecimal.Null;
        private NullableDecimal totalCostInLocalCurrency = NullableDecimal.Null;
        private NullableDecimal partsCostInLocalCurrency = NullableDecimal.Null;

        private bool diagnosticReportIsExactMatch = false;
        private int diagnosticReportSecondaryCodeAssignmentMatches = 0;

        /// <summary>
        /// Allow access internally so that the collection can gain access to it for the relations (special since this is not a business object)
        /// </summary>
        protected internal SmartCollection fixDTCs = null;

        /// <summary>
        ///
        /// </summary>
        protected internal SmartCollection fixSymptoms = null;

        // Flags used for temp storage when the fix is looked up for diagnostic reports
        private bool isFromPolkMatch;

        private bool isFromVinPowerMatch;

        // Property for temporary storage of the number of freeze frame matches.
        public int FreezeFrameMatches { get; set; }

        //#Sprint23
        private bool isLaborRequired = false;

        private bool isPartRequired = false;
        private bool isToolRequired = false;
        //#Sprint23

        #region System Constructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new (one that does not exist, assigning a new <see cref="Guid"/> Id). Fix object.
        /// In order to create a new Fix which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <example>
        /// <code>
        /// Fix o = (Fix)Registry.CreateInstance(typeof(Fix));
        /// </code>
        /// </example>
        protected internal Fix() : base()
        {
            IsObjectCreated = true;
            IsObjectLoaded = true;
            IsObjectDirty = true;
        }

        /// <summary>
        /// Constructor creates an existing (one that exists, and already has a <see cref="Guid"/> Id).  Fix object.
        /// In order to create an existing Fix object, which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <param name="id">The <see cref="Guid"/> or SQL uniqueidentifier indentification "Id" of the current object.</param>
        /// <example>
        /// <code>
        /// Fix o = (Fix)Registry.CreateInstance(typeof(Fix), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));
        /// </code>
        /// </example>
        protected internal Fix(Guid id) : base(id)
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
        /// Gets or sets the <see cref="FixName"/> associated with the fix.
        /// </summary>
        [PropertyDefinition("Fix Name", "The name of the fix required.")]
        public FixName FixName
        {
            get
            {
                this.EnsureLoaded();
                return this.fixName;
            }
            set
            {
                this.EnsureLoaded();
                if (this.fixName != value)
                {
                    this.IsObjectDirty = true;
                    this.fixName = value;
                    this.UpdatedField("FixNameId");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="FixType"/> of fix.
        /// </summary>
        [PropertyDefinition("Fix Type", "The type of fix needed.")]
        public FixType FixType
        {
            get
            {
                this.EnsureLoaded();
                return this.fixType;
            }
            set
            {
                this.EnsureLoaded();
                if (this.fixType != value)
                {
                    this.IsObjectDirty = true;
                    this.fixType = value;
                    this.UpdatedField("FixType");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="ObdFix"/> (feedback object) associated with the fix.
        /// </summary>
        [PropertyDefinition("OBD Fix", "OBD Feedback Object assoicated with the fix.")]
        public ObdFix ObdFix
        {
            get
            {
                EnsureLoaded();
                return obdFix;
            }
            set
            {
                EnsureLoaded();
                if (obdFix != value)
                {
                    IsObjectDirty = true;
                    obdFix = value;
                }
            }
        }

        //Added on 2017-08-10 by INNOVA Dev Team: Support ProRS Fix - 8/10/2017
        /// <summary>
        /// Gets or sets the <see cref="ProRSFix"/> (feedback object) associated with the fix.
        /// </summary>
        [PropertyDefinition("ProRS Fix", "ProRS Feedback Object assoicated witht the fix.")]
        public ProRSFix ProRSFix
        {
            get
            {
                EnsureLoaded();
                return proRsFix;
            }
            set
            {
                EnsureLoaded();
                if (proRsFix != value)
                {
                    IsObjectDirty = true;
                    proRsFix = value;
                }
            }
        }

        /// <summary>
        /// DEPRECATED
        /// </summary>
        [PropertyDefinition("Primary Code", "Primary Error Code")]
        [Obsolete("Converted to FixDTCs list")]
        private string PrimaryErrorCode
        {
            get
            {
                EnsureLoaded();
                return primaryErrorCode;
            }
            set
            {
                EnsureLoaded();
                if (primaryErrorCode != value)
                {
                    IsObjectDirty = true;
                    primaryErrorCode = value;
                }
            }
        }

        private string primaryDTCs = null;

        /// <summary>
        /// Gets the <see cref="string"/> list of primary DTC codes associated to this fix.
        /// </summary>
        [PropertyDefinition("Primary DTCs", "List of primary DTC codes associated to this fix")]
        public string PrimaryDTCs
        {
            get
            {
                if (this.primaryDTCs == null)
                {
                    this.PrimaryDTCs = this.GetFixPrimaryDTCCodes();
                }
                return this.primaryDTCs;
            }
            protected internal set
            {
                this.primaryDTCs = value;
            }
        }

        /// <summary>
        /// DEPRECATED
        /// </summary>
        [PropertyDefinition("Secondary Code", "Secondary Error Code")]
        [Obsolete("Converted to FixDTCs list")]
        private string SecondaryErrorCode
        {
            get
            {
                EnsureLoaded();
                return secondaryErrorCode;
            }
            set
            {
                EnsureLoaded();
                if (secondaryErrorCode != value)
                {
                    IsObjectDirty = true;
                    secondaryErrorCodesList = null;
                    secondaryErrorCode = value;
                }
            }
        }

        /// <summary>
        /// DEPRECATED
        /// </summary>
        [PropertyDefinition("Secondary Error Codes", "List of Secondary Error Codes.")]
        [Obsolete("Converted to FixDTCs list")]
        private StringCollection SecondaryErrorCodesList
        {
            get
            {
                if (secondaryErrorCodesList == null)
                {
                    secondaryErrorCodesList = new StringCollection();

                    if (this.SecondaryErrorCode != null && this.SecondaryErrorCode.Length > 0)
                    {
                        string[] codes = this.SecondaryErrorCode.Split(",".ToCharArray());

                        foreach (string s in codes)
                        {
                            this.secondaryErrorCodesList.Add(s);
                        }
                    }
                }
                return secondaryErrorCodesList;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="AdminUser"/>
        /// </summary>
        [PropertyDefinition("Created By Admin", "The administrator who created this fix.")]
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
        /// Gets or sets the <see cref="User"/>
        /// </summary>
        [PropertyDefinition("Created By User", "The user who created this fix.")]
        public User CreatedByUser
        {
            get
            {
                EnsureLoaded();
                return createdByUser;
            }
            set
            {
                EnsureLoaded();
                if (createdByUser != value)
                {
                    IsObjectDirty = true;
                    createdByUser = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="UpdatedByAdminUser"/>
        /// </summary>
        [PropertyDefinition("Updated By Admin", "The admin who updated this fix")]
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
        /// Gets or sets the <see cref="ApprovedByAdminUser"/>
        /// </summary>
        [PropertyDefinition("Admin Approved", "The admin who approved this fix.")]
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
        [PropertyDefinition("Approved", "Indicator that this fix has been approved.")]
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
                    if (!isApprovedChangedToTrue && !isApproved)
                    {
                        isApprovedChangedToTrue = true;
                    }
                    else if (isApprovedChangedToTrue && isApproved)
                    {
                        isApprovedChangedToTrue = false;
                    }

                    IsObjectDirty = true;
                    isApproved = value;
                }
            }
        }

        /// <summary>
        /// Gets a <see cref="bool"/> value that indicates if the fix was changed from unapproved to approved.
        /// </summary>
        [PropertyDefinition("Changed to Approved", "Indicates that fix was changed form unapproved to approved")]
        public bool IsApprovedChangedToTrue
        {
            get
            {
                return isApprovedChangedToTrue;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/>
        /// </summary>
        [PropertyDefinition("Initially Reviewed", "Reviewed", "Reviewed", "Indicates that fix is initially reviewed.")]
        public bool IsInitiallyReviewed
        {
            get
            {
                EnsureLoaded();
                return isInitiallyReviewed;
            }
            set
            {
                EnsureLoaded();
                if (isInitiallyReviewed != value)
                {
                    IsObjectDirty = true;
                    isInitiallyReviewed = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableDateTime"/>
        /// </summary>
        [PropertyDefinition("Approved Date", "The date when the fix was Approved.")]
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
        [PropertyDefinition("Engine Type Defined", "Indicates that engine type was specified.")]
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
        [PropertyDefinition("Engine VIN code defined", "Indicates if engine type is specified.")]
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
        [PropertyDefinition("Year Defined", "Indicates if year is specified.")]
        public bool HasYearDefined
        {
            get
            {
                EnsureLoaded();
                return hasYearDefined;
            }
        }

        /// <summary>
        /// Gets the <see cref="bool"/> flag indicating whether or not the generation is defined.
        /// </summary>
        [PropertyDefinition("Generation Defined", "Indicates if generation is specified.")]
        public bool HasGenerationDefined
        {
            get
            {
                EnsureLoaded();
                return hasGenerationDefined;
            }
        }

        /// <summary>
        /// Gets the <see cref="bool"/> flag indicating whether or not the model is defined.
        /// </summary>
        [PropertyDefinition("Model Defined", "Indicates that the model is specified.")]
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
        [PropertyDefinition("Trim Level Defined", "Indicates that the trim level is specified.")]
        public bool HasTrimLevelDefined
        {
            get
            {
                EnsureLoaded();
                return hastrimLevelDefined;
            }
        }

        /// <summary>
        /// Gets the <see cref="bool"/> flag indicating whether or not the transmission control type is defined.
        /// </summary>
        [PropertyDefinition("Transmission Defined", "Indicates that the type of transmission is specified.")]
        public bool HasTransmissionDefined
        {
            get
            {
                EnsureLoaded();
                return hasTransmissionDefined;
            }
        }

        /// <summary>
        /// Gets the <see cref="bool"/> flag indicating whether or not the engine type is defined.
        /// </summary>
        [PropertyDefinition("Engine Type Defined (Polk)", "Indicates that Polk engine type was specified.")]
        public bool HasEngineTypeDefinedPolk
        {
            get
            {
                this.EnsureLoaded();
                return this.hasEngineTypeDefinedPolk;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableDateTime"/> when the Polk engine type values were verified to be correct and complete.
        /// </summary>
        public NullableDateTime EngineTypePolkVerifiedDateTimeUTC
        {
            get
            {
                this.EnsureLoaded();
                return this.engineTypePolkVerifiedDateTimeUTC;
            }
            set
            {
                this.EnsureLoaded();
                if (this.engineTypePolkVerifiedDateTimeUTC != value)
                {
                    this.IsObjectDirty = true;
                    this.engineTypePolkVerifiedDateTimeUTC = value;
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="bool"/> flag indicating whether or not the engine type is defined.
        /// </summary>
        [PropertyDefinition("Engine VIN code defined (Polk)", "Indicates if Polk engine VIN code is specified.")]
        public bool HasEngineVINCodeDefinedPolk
        {
            get
            {
                this.EnsureLoaded();
                return this.hasEngineVINCodeDefinedPolk;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableDateTime"/> when the Polk engine VIN code values were verified to be correct and complete.
        /// </summary>
        public NullableDateTime EngineVINCodePolkVerifiedDateTimeUTC
        {
            get
            {
                this.EnsureLoaded();
                return this.engineVINCodePolkVerifiedDateTimeUTC;
            }
            set
            {
                this.EnsureLoaded();
                if (this.engineVINCodePolkVerifiedDateTimeUTC != value)
                {
                    this.IsObjectDirty = true;
                    this.engineVINCodePolkVerifiedDateTimeUTC = value;
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="bool"/> flag indicating whether or not the year is defined.
        /// </summary>
        [PropertyDefinition("Year Defined (Polk)", "Indicates if Polk year is specified.")]
        public bool HasYearDefinedPolk
        {
            get
            {
                this.EnsureLoaded();
                return this.hasYearDefinedPolk;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableDateTime"/> when the Polk year values were verified to be correct and complete.
        /// </summary>
        public NullableDateTime YearPolkVerifiedDateTimeUTC
        {
            get
            {
                this.EnsureLoaded();
                return this.yearPolkVerifiedDateTimeUTC;
            }
            set
            {
                this.EnsureLoaded();
                if (this.yearPolkVerifiedDateTimeUTC != value)
                {
                    this.IsObjectDirty = true;
                    this.yearPolkVerifiedDateTimeUTC = value;
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="bool"/> flag indicating whether or not the make is defined.
        /// </summary>
        [PropertyDefinition("Make Defined (Polk)", "Indicates that the Polk make is specified.")]
        public bool HasMakeDefinedPolk
        {
            get
            {
                this.EnsureLoaded();
                return this.hasMakeDefinedPolk;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableDateTime"/> when the Polk make values were verified to be correct and complete.
        /// </summary>
        public NullableDateTime MakePolkVerifiedDateTimeUTC
        {
            get
            {
                this.EnsureLoaded();
                return this.makePolkVerifiedDateTimeUTC;
            }
            set
            {
                this.EnsureLoaded();
                if (this.makePolkVerifiedDateTimeUTC != value)
                {
                    this.IsObjectDirty = true;
                    this.makePolkVerifiedDateTimeUTC = value;
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="bool"/> flag indicating whether or not the model is defined.
        /// </summary>
        [PropertyDefinition("Model Defined (Polk)", "Indicates that the Polk model is specified.")]
        public bool HasModelDefinedPolk
        {
            get
            {
                this.EnsureLoaded();
                return this.hasModelDefinedPolk;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableDateTime"/> when the Polk model values were verified to be correct and complete.
        /// </summary>
        public NullableDateTime ModelPolkVerifiedDateTimeUTC
        {
            get
            {
                this.EnsureLoaded();
                return this.modelPolkVerifiedDateTimeUTC;
            }
            set
            {
                this.EnsureLoaded();
                if (this.modelPolkVerifiedDateTimeUTC != value)
                {
                    this.IsObjectDirty = true;
                    this.modelPolkVerifiedDateTimeUTC = value;
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="bool"/> flag indicating whether or not the trim level is defined.
        /// </summary>
        [PropertyDefinition("Trim Level Defined (Polk)", "Indicates that the Polk trim level is specified.")]
        public bool HasTrimLevelDefinedPolk
        {
            get
            {
                this.EnsureLoaded();
                return this.hastrimLevelDefinedPolk;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableDateTime"/> when the Polk trim level values were verified to be correct and complete.
        /// </summary>
        public NullableDateTime TrimLevelPolkVerifiedDateTimeUTC
        {
            get
            {
                this.EnsureLoaded();
                return this.trimLevelPolkVerifiedDateTimeUTC;
            }
            set
            {
                this.EnsureLoaded();
                if (this.trimLevelPolkVerifiedDateTimeUTC != value)
                {
                    this.IsObjectDirty = true;
                    this.trimLevelPolkVerifiedDateTimeUTC = value;
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> name of the fix
        /// </summary>
        [PropertyDefinition("Name", "Existing Fix Selected", "Existing Fix Selected", "Description of the type of Fix.")]
        public string Name
        {
            get
            {
                return this.FixName.Description;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        [PropertyDefinition("Description", "Description of Fix")]
        public string Description
        {
            get
            {
                this.EnsureLoaded();
                return this.description;
            }
            set
            {
                this.EnsureLoaded();
                if (this.description != value)
                {
                    this.IsObjectDirty = true;
                    this.description = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        [PropertyDefinition("Description English", "Description in English")]
        public string Description_es
        {
            get
            {
                this.EnsureLoaded();
                return this.description_es;
            }
            set
            {
                this.EnsureLoaded();
                if (this.description_es != value)
                {
                    this.IsObjectDirty = true;
                    this.description_es = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        [PropertyDefinition("Description French", "Description in French")]
        public string Description_fr
        {
            get
            {
                this.EnsureLoaded();
                return this.description_fr;
            }
            set
            {
                this.EnsureLoaded();
                if (this.description_fr != value)
                {
                    this.IsObjectDirty = true;
                    this.description_fr = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        [PropertyDefinition("Description Chinese", "Description in Chinese")]
        public string Description_zh
        {
            get
            {
                this.EnsureLoaded();
                return this.description_zh;
            }
            set
            {
                this.EnsureLoaded();
                if (this.description_zh != value)
                {
                    this.IsObjectDirty = true;
                    this.description_zh = value;
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> description in the language specified in the <see cref="RuntimeInfo"/>.
        /// </summary>
        [PropertyDefinition("Description Translated", "Description Translated")]
        public string Description_Translated
        {
            get
            {
                this.EnsureLoaded();
                return this.RuntimeInfo.GetTranslatedValue(this.Description, this.Description_es, this.Description_fr, this.Description_zh);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="decimal"/>
        /// </summary>
        [PropertyDefinition("Labor", "Hours of Labor")]
        public decimal Labor
        {
            get
            {
                this.EnsureLoaded();
                return this.labor;
            }
            set
            {
                this.EnsureLoaded();
                if (this.labor != value)
                {
                    this.IsObjectDirty = true;
                    this.labor = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="decimal"/>
        /// </summary>
        [PropertyDefinition("Additional Costs", "Additional Costs")]
        public decimal AdditionalCost
        {
            get
            {
                this.EnsureLoaded();
                return this.additionalCost;
            }
            set
            {
                this.EnsureLoaded();
                if (this.additionalCost != value)
                {
                    this.IsObjectDirty = true;
                    this.additionalCost = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/>
        /// </summary>
        [PropertyDefinition("Frequency Count", "How often fix is performed.")]
        public int FrequencyCount
        {
            get
            {
                this.EnsureLoaded();
                return this.frequencyCount;
            }
            set
            {
                this.EnsureLoaded();
                if (this.frequencyCount != value)
                {
                    this.IsObjectDirty = true;
                    this.frequencyCount = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/>
        /// </summary>
        [PropertyDefinition("Created Date", "Date fix was created.")]
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
        /// Gets or sets the <see cref="DateTime"/>
        /// </summary>
        [PropertyDefinition("Updated Date", "Date fix was updated.")]
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
        /// Gets the <see cref="decimal"/> monetary value of the labor rate in US dollars.
        /// </summary>
        [PropertyDefinition("Labor Rate", "Monetary value of labor rate in US dollars")]
        public decimal LaborRate
        {
            get
            {
                if (!this.laborRate.HasValue)
                {
                    this.CalculateFixCosts();
                }
                return this.laborRate.Value;
            }
        }

        /// <summary>
        /// Gets the <see cref="decimal"/> monetary cost of the labor in US dollars.
        /// </summary>
        [PropertyDefinition("Labor Cost", "Monetary vost of labor in US dollars.")]
        public decimal LaborCost
        {
            get
            {
                if (!this.laborCost.HasValue)
                {
                    this.CalculateFixCosts();
                }
                return this.laborCost.Value;
            }
        }

        /// <summary>
        /// Gets the <see cref="decimal"/> monetary cost of the parts in US dollars.
        /// </summary>
        [PropertyDefinition("Parts Cost", "Monetary cost of the parts in US dollars")]
        public decimal PartsCost
        {
            get
            {
                if (!this.partsCost.HasValue)
                {
                    this.CalculateFixCosts();
                }
                return this.partsCost.Value;
            }
        }

        /// <summary>
        /// Gets the <see cref="decimal"/> monetary cost of the fix in US dollars.
        /// </summary>
        [PropertyDefinition("Total Cost", "Monetary cost of the fix in US dollars")]
        public decimal TotalCost
        {
            get
            {
                if (!this.totalCost.HasValue)
                {
                    this.CalculateFixCosts();
                }
                return this.totalCost.Value;
            }
        }

        /// <summary>
        /// Gets the <see cref="decimal"/> monetary value of the labor rate in the local currency as specified in the <see cref="RuntimeInfo"/>.
        /// </summary>
        [PropertyDefinition("Labor Rate", "Monetary value of labor rate in local currency.")]
        public NullableDecimal LaborRateInLocalCurrency
        {
            get
            {
                if (!this.laborRate.HasValue)
                {
                    this.CalculateFixCosts();
                }
                return this.laborRateInLocalCurrency;
            }
        }

        /// <summary>
        /// Gets the <see cref="decimal"/> monetary cost of the labor in the local currency as specified in the <see cref="RuntimeInfo"/>.
        /// </summary>
        [PropertyDefinition("Labor Cost", "Monetary cost of labor in local currency.")]
        public NullableDecimal LaborCostInLocalCurrency
        {
            get
            {
                if (!this.laborCost.HasValue)
                {
                    this.CalculateFixCosts();
                }
                return this.laborCostInLocalCurrency;
            }
        }

        /// <summary>
        /// Gets the <see cref="decimal"/> monetary additional cost of the fix in the local currency as specified in the <see cref="RuntimeInfo"/>.
        /// </summary>
        [PropertyDefinition("Additional Cost", "Monetary additional cost of the fix in local currency.")]
        public NullableDecimal AdditionalCostInLocalCurrency
        {
            get
            {
                if (!this.totalCost.HasValue)
                {
                    this.CalculateFixCosts();
                }
                return this.additionalCostInLocalCurrency;
            }
        }

        /// <summary>
        /// Gets the <see cref="decimal"/> monetary total cost of the fix in the local currency as specified in the <see cref="RuntimeInfo"/>.
        /// </summary>
        [PropertyDefinition("Total Cost", "Monetary total cost of the fix in local currency.")]
        public NullableDecimal TotalCostInLocalCurrency
        {
            get
            {
                if (!this.totalCost.HasValue)
                {
                    this.CalculateFixCosts();
                }
                return this.totalCostInLocalCurrency;
            }
        }

        /// <summary>
        /// Gets the <see cref="decimal"/> monetary cost of the parts in the local currency as specified in the <see cref="RuntimeInfo"/>.
        /// </summary>
        [PropertyDefinition("Parts Cost", "Monetary cost of the parts in local currency.")]
        public NullableDecimal PartsCostInLocalCurrency
        {
            get
            {
                if (!this.partsCost.HasValue)
                {
                    this.CalculateFixCosts();
                }
                return this.partsCostInLocalCurrency;
            }
        }

        /// <summary>
        /// Gets the <see cref="bool"/> flag indicating whether or not this is an exact match, used when processing the prioritized solutions for the diagnostic report.
        /// </summary>
        [PropertyDefinition("Diagnostic Report Exact Match", "Indicator if this is an exact match, used when processing prioritized solutions for diagnositic report.")]
        public bool DiagnosticReportIsExactMatch
        {
            get
            {
                return diagnosticReportIsExactMatch;
            }
        }

        /// <summary>
        /// Gets the <see cref="int"/> number of secondary code assignment matches there are, used when processing the prioritized solutions for the diagnostic report.
        /// </summary>
        [PropertyDefinition("Secondary Diganostic Report Assignment Matches", "Number of secondary code assignemnt matches there are, used when processing the prioritized solutiosn for the diagnoistic report")]
        public int DiagnosticReportSecondaryCodeAssignmentMatches
        {
            get
            {
                return diagnosticReportSecondaryCodeAssignmentMatches;
            }
        }

        /// <summary>
        /// Gets the <see cref="decimal"/> monetary value of the labor rate in the local currency if other than US, otherwise in US dollars.
        /// </summary>
        [PropertyDefinition("Labor Rate", "Monetary value of labor rate in US dollars or local currency if outside US")]
        public decimal LaborRate_Presented
        {
            get
            {
                return this.laborRateInLocalCurrency.HasValue ? this.laborRateInLocalCurrency.Value : this.LaborRate;
            }
        }

        /// <summary>
        /// Gets the <see cref="decimal"/> monetary cost of the labor in the local currency if other than US, otherwise in US dollars.
        /// </summary>
        [PropertyDefinition("Labor Cost", "Monetary cost of the labor in US dollars or local currency if outside US.")]
        public decimal LaborCost_Presented
        {
            get
            {
                return this.laborCostInLocalCurrency.HasValue ? this.laborCostInLocalCurrency.Value : this.LaborCost;
            }
        }

        /// <summary>
        /// Gets the <see cref="decimal"/> monetary additional cost of the fix in the local currency if other than US, otherwise in US dollars.
        /// </summary>
        [PropertyDefinition("Additional Cost", "Monetary additional costs of the fix in US dollars or local currency if outside US.")]
        public decimal AdditionalCost_Presented
        {
            get
            {
                return this.additionalCostInLocalCurrency.HasValue ? this.additionalCostInLocalCurrency.Value : this.AdditionalCost;
            }
        }

        /// <summary>
        /// Gets the <see cref="decimal"/> monetary total cost of the fix in the local currency if other than US, otherwise in US dollars.
        /// </summary>
        [PropertyDefinition("Total Cost", "Monetary total cost of the fix in US Dollars or local currency if outside US.")]
        public decimal TotalCost_Presented
        {
            get
            {
                return this.totalCostInLocalCurrency.HasValue ? this.totalCostInLocalCurrency.Value : this.TotalCost;
            }
        }

        /// <summary>
        /// Gets the <see cref="decimal"/> monetary cost of the parts in the local currency if other than US, otherwise in US dollars.
        /// </summary>
        [PropertyDefinition("Parts Cost", "Monetary cost of the parts in US Dollars or local currency if outside US")]
        public decimal PartsCost_Presented
        {
            get
            {
                return this.partsCostInLocalCurrency.HasValue ? this.partsCostInLocalCurrency.Value : this.PartsCost;
            }
        }

        private string partListDescription;

        /// <summary>
        /// Gets the <see cref="string"/> part list description for presentation purposes, (read only and not for us when editing the fix part list, if accessed first, then the part list is modified, this property will be dirty)
        /// </summary>
        [PropertyDefinition("Part List Description", "Part list description for presentation")]
        public string PartListDescription
        {
            get
            {
                if (this.partListDescription == null)
                {
                    foreach (FixPart part in this.FixParts)
                    {
                        if (this.partListDescription.Length > 0)
                        {
                            partListDescription += ", ";
                        }
                        partListDescription += part.Part.PartNumber + " " + part.Part.PartName;
                    }
                }

                return this.partListDescription;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/> indicating if the fix was found as a result of Polk vehicle YMME values that matched the fix.
        /// NOTE: This property is not saved to the database. It's for temporary use and should be passed on to the DiagnosticReportResultFix object.
        /// </summary>
        public bool IsFromPolkMatch
        {
            get
            {
                return this.isFromPolkMatch;
            }
            set
            {
                this.isFromPolkMatch = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/> indicating if the fix was found as a result of VinPower vehicle YMME values that matched the fix.
        /// NOTE: This property is not saved to the database. It's for temporary use and should be passed on to the DiagnosticReportResultFix object.
        /// </summary>
        public bool IsFromVinPowerMatch
        {
            get
            {
                return this.isFromVinPowerMatch;
            }
            set
            {
                this.isFromVinPowerMatch = value;
            }
        }

        //#FixManufacturer
        /// <summary>
        /// Gets the <see cref="bool"/> flag indicating whether or not the Manufacturer is defined.
        /// </summary>
        [PropertyDefinition("Manufacturer Defined", "Indicates that the Manufacturer is specified.")]
        public bool HasManufacturerDefined
        {
            get
            {
                EnsureLoaded();
                return hasManufacturerDefined;
            }
        }

        #endregion Object Properties

        #region Object Properties (Related Objects)

        /*****************************************************************************************
		 *
		 * Object Relationships: Add correlated Object Collections
		 *
		*******************************************************************************************/

        //#FixManufacturer
        /// <summary>
        /// Get the <see cref="List{T}"/> of vehicle manufacturer that apply to this fix.
        /// NOTE: DO NOT add to this collection directly. Use the AddManufacturer() method.
        /// </summary>
        [PropertyDefinition("Manufacturers", "Manufacturers of vehicles that apply to this fix.")]
        public List<string> Manufacturers
        {
            get
            {
                if (this.manufacturers == null)
                {
                    this.EnsureLoaded();

                    this.manufacturers = new List<string>();

                    if (!this.isObjectCreated && this.manufacturersString != "")
                    {
                        foreach (string s in this.manufacturersString.Split("|".ToCharArray()))
                        {
                            if (s != null && s != "")
                            {
                                this.manufacturers.Add(s);
                            }
                        }
                    }
                }

                return this.manufacturers;
            }
        }

        /// <summary>
        /// Get the <see cref="FixPartCollection"/> of parts that apply to this fix.
        /// </summary>
        [PropertyDefinition("Fix Parts", "Collection of parts that apply to this fix.")]
        public FixPartCollection FixParts
        {
            get
            {
                if (fixParts == null)
                {
                    this.EnsureLoaded();

                    fixParts = new FixPartCollection(Registry);

                    //load if not a user created element
                    if (!isObjectCreated)
                    {
                        SqlProcedureCommand call = new SqlProcedureCommand();
                        EnsureValidId();

                        call.ProcedureName = "FixPart_LoadByFix";
                        call.AddGuid("FixId", Id);

                        fixParts.Load(call, "FixPartId", true, true);
                    }
                }

                return fixParts;
            }
        }

        //ToolDB_
        /// <summary>
        /// Get the <see cref="FixToolCollection"/> of parts that apply to this fix.
        /// </summary>
        [PropertyDefinition("Fix Tools", "Collection of tools that apply to this fix.")]
        public FixToolCollection FixTools
        {
            get
            {
                if (fixTools == null)
                {
                    this.EnsureLoaded();

                    fixTools = new FixToolCollection(Registry);

                    //load if not a user created element
                    if (!isObjectCreated)
                    {
                        SqlProcedureCommand call = new SqlProcedureCommand();
                        EnsureValidId();

                        call.ProcedureName = "FixTool_LoadByFix";
                        call.AddGuid("FixId", Id);

                        fixTools.Load(call, "FixToolId", true, true);
                    }
                }

                return fixTools;
            }
        }

        //ToolDB_

        /// <summary>
        /// Gets or sets the list of <see cref="Market"/> that this fix applies to.
        /// NOTE: DO NOT add to this collection directly. Use the AddMarket() method.
        /// </summary>
        [PropertyDefinition("Markets", "Markets that this fix covers.")]
        public List<Market> Markets
        {
            get
            {
                if (this.markets == null)
                {
                    this.EnsureLoaded();

                    this.markets = new List<Market>();

                    if (!isObjectCreated && this.marketsString != "")
                    {
                        foreach (string s in this.marketsString.Split("|".ToCharArray()))
                        {
                            if (s != null && s != "")
                            {
                                markets.Add((Market)int.Parse(s));
                            }
                        }
                    }
                }
                return markets;
            }
        }

        /// <summary>
        /// Gets the <see cref="List{T}"/> of vehicle engine types that apply to this fix.
        /// NOTE: DO NOT add to this collection directly. Use the AddEngineType() method.
        /// </summary>
        [PropertyDefinition("Engine Types", "Engine Types that apply to this fix.")]
        public List<string> EngineTypes
        {
            get
            {
                if (this.engineTypes == null)
                {
                    this.EnsureLoaded();

                    this.engineTypes = new List<string>();

                    if (!this.isObjectCreated && this.engineTypesString != "")
                    {
                        foreach (string s in this.engineTypesString.Split("|".ToCharArray()))
                        {
                            if (s != null && s != "")
                            {
                                this.engineTypes.Add(s);
                            }
                        }
                    }
                }
                return this.engineTypes;
            }
        }

        /// <summary>
        /// Gets the <see cref="List{T}"/> of vehicle engine VIN codes that apply to this fix.
        /// NOTE: DO NOT add to this collection directly. Use the AddEngineVINCode() method.
        /// </summary>
        [PropertyDefinition("VIN Codes", "Vehicle Engine VIN codes that apply to this fix.")]
        public List<string> EngineVINCodes
        {
            get
            {
                if (this.engineVINCodes == null)
                {
                    this.EnsureLoaded();

                    this.engineVINCodes = new List<string>();

                    if (!this.isObjectCreated && this.engineVINCodesString != "")
                    {
                        foreach (string s in this.engineVINCodesString.Split("|".ToCharArray()))
                        {
                            if (s != null && s != "")
                            {
                                this.engineVINCodes.Add(s);
                            }
                        }
                    }
                }
                return this.engineVINCodes;
            }
        }

        /// <summary>
        /// Get the <see cref="List{T}"/> of vehicle makes that apply to this fix.
        /// NOTE: DO NOT add to this collection directly. Use the AddMake() method.
        /// </summary>
        [PropertyDefinition("Makes", "Makes of vehicles that apply to this fix.")]
        public List<string> Makes
        {
            get
            {
                if (this.makes == null)
                {
                    this.EnsureLoaded();

                    this.makes = new List<string>();

                    //load if not a user created element

                    if (!this.isObjectCreated && this.makesString != "")
                    {
                        foreach (string s in this.makesString.Split("|".ToCharArray()))
                        {
                            if (s != null && s != "")
                            {
                                this.makes.Add(s);
                            }
                        }
                    }
                }

                return this.makes;
            }
        }

        /// <summary>
        /// Get the <see cref="List{T}"/> of vehicle models that apply to this fix.
        /// NOTE: DO NOT add to this collection directly. Use the AddModel() method.
        /// </summary>
        [PropertyDefinition("Models", "Models of vehicles that apply to this fix.")]
        public List<string> Models
        {
            get
            {
                if (this.models == null)
                {
                    this.EnsureLoaded();

                    this.models = new List<string>();

                    if (!this.isObjectCreated && this.modelsString != "")
                    {
                        foreach (string s in this.modelsString.Split("|".ToCharArray()))
                        {
                            if (s != null && s != "")
                            {
                                //this.models.Add(s.ToUpper()); //Added on 2018-12-10 2:50 PM by INNOVA Dev Team
                                this.models.Add(s);
                            }
                        }
                    }
                }

                return this.models;
            }
        }

        /// <summary>
        /// Get the <see cref="List{T}"/> of vehicle trim levels that apply to this fix.
        /// NOTE: DO NOT add to this collection directly. Use the AddTrimLevel() method.
        /// </summary>
        ///
        [PropertyDefinition("Trim Levels", "Trim Levels of vechicles that apply to this fix.")]
        public List<string> TrimLevels
        {
            get
            {
                if (this.trimLevels == null)
                {
                    this.EnsureLoaded();

                    this.trimLevels = new List<string>();

                    if (!this.isObjectCreated && this.trimLevelsString != "")
                    {
                        foreach (string s in this.trimLevelsString.Split("|".ToCharArray()))
                        {
                            if (s != null && s != "")
                            {
                                this.trimLevels.Add(s);
                            }
                        }
                    }
                }

                return this.trimLevels;
            }
        }

        /// <summary>
        /// Get the <see cref="List{T}"/> of transmission control types that apply to this fix.
        /// NOTE: DO NOT add to this collection directly. Use the AddTransmissionControlType() method.
        /// </summary>
        [PropertyDefinition("Transmission", "Transmission control types that apply to this fix.")]
        public List<string> TransmissionControlTypes
        {
            get
            {
                if (this.transmissionControlTypes == null)
                {
                    this.EnsureLoaded();

                    this.transmissionControlTypes = new List<string>();

                    if (!this.isObjectCreated && this.transmissionsString != "")
                    {
                        foreach (string s in this.transmissionsString.Split("|".ToCharArray()))
                        {
                            if (s != null && s != "")
                            {
                                this.transmissionControlTypes.Add(s);
                            }
                        }
                    }
                }

                return this.transmissionControlTypes;
            }
        }

        /// <summary>
        /// Get an <see cref="List{T}"/> of <see cref="int"/> vehicle years that apply to this fix.
        /// NOTE: DO NOT add to this collection directly. Use the AddYear() method.
        /// </summary>
        [PropertyDefinition("Years", "Vehicle Years that apply to this fix.")]
        public List<int> Years
        {
            get
            {
                if (this.years == null)
                {
                    this.EnsureLoaded();

                    this.years = new List<int>();

                    if (!this.isObjectCreated && this.yearsString != "")
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

        /// <summary>
        /// Get an <see cref="List{T}"/> of vehicle years that apply to this fix.
        /// NOTE: DO NOT add to this collection directly. Use the AddYear() method.
        /// </summary>
        public List<string> YearsAsStrings
        {
            get
            {
                List<string> yearsAsStrings = new List<string>();

                foreach (int y in this.Years)
                {
                    yearsAsStrings.Add(y.ToString());
                }

                return yearsAsStrings;
            }
        }

        /// <summary>
        /// Get an <see cref="List{T}"/> of vehicle generations that apply to this fix.
        /// NOTE: DO NOT add to this collection directly. Use the AddGeneration() method.
        /// </summary>
        public List<string> Generations
        {
            get
            {
                if (this.generations == null)
                {
                    this.EnsureLoaded();

                    this.generations = new List<string>();

                    if (!this.isObjectCreated && this.generationsString != "")
                    {
                        foreach (string s in this.generationsString.Split("|".ToCharArray()))
                        {
                            if (s != null && s != "")
                            {
                                this.generations.Add(s);
                            }
                        }
                    }
                }

                return this.generations;
            }
        }

        /// <summary>
        /// Gets the <see cref="List{T}"/> of Polk vehicle engine types that apply to this fix.
        /// NOTE: DO NOT add to this collection directly. Use the AddEngineTypePolk() method.
        /// </summary>
        [PropertyDefinition("Engine Types (Polk)", "Polk Engine Types that apply to this fix.")]
        public List<string> EngineTypesPolk
        {
            get
            {
                if (this.engineTypesPolk == null)
                {
                    this.engineTypesPolk = new List<string>();

                    if (!this.isObjectCreated && this.engineTypesStringPolk != "")
                    {
                        foreach (string s in this.engineTypesStringPolk.Split("|".ToCharArray()))
                        {
                            if (s != null && s != "")
                            {
                                this.engineTypesPolk.Add(s);
                            }
                        }
                    }
                }
                return this.engineTypesPolk;
            }
        }

        /// <summary>
        /// Gets the <see cref="List{T}"/> of Polk vehicle engine VIN codes that apply to this fix.
        /// NOTE: DO NOT add to this collection directly. Use the AddEngineVINCodePolk() method.
        /// </summary>
        [PropertyDefinition("VIN Codes (Polk)", "Polk Vehicle Engine VIN codes that apply to this fix.")]
        public List<string> EngineVINCodesPolk
        {
            get
            {
                if (this.engineVINCodesPolk == null)
                {
                    this.engineVINCodesPolk = new List<string>();

                    if (!this.isObjectCreated && this.engineVINCodesStringPolk != "")
                    {
                        foreach (string s in this.engineVINCodesStringPolk.Split("|".ToCharArray()))
                        {
                            if (s != null && s != "")
                            {
                                this.engineVINCodesPolk.Add(s);
                            }
                        }
                    }
                }
                return this.engineVINCodesPolk;
            }
        }

        /// <summary>
        /// Get the <see cref="List{T}"/> of Polk vehicle makes that apply to this fix.
        /// NOTE: DO NOT add to this collection directly. Use the AddMakePolk() method.
        /// </summary>
        [PropertyDefinition("Makes (Polk)", "Polk Makes of vehicles that apply to this fix.")]
        public List<string> MakesPolk
        {
            get
            {
                if (this.makesPolk == null)
                {
                    this.makesPolk = new List<string>();

                    //load if not a user created element

                    if (!this.isObjectCreated && this.makesStringPolk != "")
                    {
                        foreach (string s in this.makesStringPolk.Split("|".ToCharArray()))
                        {
                            if (s != null && s != "")
                            {
                                this.makesPolk.Add(s);
                            }
                        }
                    }
                }

                return this.makesPolk;
            }
        }

        /// <summary>
        /// Get the <see cref="List{T}"/> of Polk vehicle models that apply to this fix.
        /// NOTE: DO NOT add to this collection directly. Use the AddModelPolk() method.
        /// </summary>
        [PropertyDefinition("ModelsPolk", "Polk Models of vehicles that apply to this fix.")]
        public List<string> ModelsPolk
        {
            get
            {
                if (this.modelsPolk == null)
                {
                    this.modelsPolk = new List<string>();

                    if (!this.isObjectCreated && this.modelsStringPolk != "")
                    {
                        foreach (string s in this.modelsStringPolk.Split("|".ToCharArray()))
                        {
                            if (s != null && s != "")
                            {
                                this.modelsPolk.Add(s.ToUpper()); //Added on 2018-12-10 1:50 PM: Fix the Not Showing the Selected Model(s)
                            }
                        }
                    }
                }

                return this.modelsPolk;
            }
        }

        /// <summary>
        /// Get the <see cref="List{T}"/> of Polk vehicle trim levels that apply to this fix.
        /// NOTE: DO NOT add to this collection directly. Use the AddTrimLevelPolk() method.
        /// </summary>
        ///
        [PropertyDefinition("Trim Levels (Polk)", "Polk Trim Levels of vechicles that apply to this fix.")]
        public List<string> TrimLevelsPolk
        {
            get
            {
                if (this.trimLevelsPolk == null)
                {
                    this.trimLevelsPolk = new List<string>();

                    if (!this.isObjectCreated && this.trimLevelsStringPolk != "")
                    {
                        foreach (string s in this.trimLevelsStringPolk.Split("|".ToCharArray()))
                        {
                            if (s != null && s != "")
                            {
                                this.trimLevelsPolk.Add(s);
                            }
                        }
                    }
                }

                return this.trimLevelsPolk;
            }
        }

        /// <summary>
        /// Get an <see cref="List{T}"/> of <see cref="int"/> Polk vehicle years that apply to this fix.
        /// NOTE: DO NOT add to this collection directly. Use the AddYearPolk() method.
        /// </summary>
        [PropertyDefinition("Years (Polk)", "Polk Vehicle Years that apply to this fix.")]
        public List<int> YearsPolk
        {
            get
            {
                if (this.yearsPolk == null)
                {
                    this.yearsPolk = new List<int>();

                    if (!this.isObjectCreated && this.yearsStringPolk != "")
                    {
                        foreach (string s in this.yearsStringPolk.Split("|".ToCharArray()))
                        {
                            if (s != null && s != "")
                            {
                                this.yearsPolk.Add(Int32.Parse(s));
                            }
                        }
                    }
                }

                return this.yearsPolk;
            }
        }

        /// <summary>
        /// Gets the <see cref="List{T}"/> of vehicle engine types that apply to this fix.
        /// NOTE: DO NOT add to this collection directly. Use the AddEngineType() method.
        /// </summary>
        [PropertyDefinition("Engine Types", "VinPower Engine Types that apply to this fix.")]
        public List<string> EngineTypesVP
        {
            get
            {
                if (this.engineTypesVP == null)
                {
                    this.EnsureLoaded();

                    this.engineTypesVP = new List<string>();

                    if (!this.isObjectCreated && this.engineTypesStringVP != "")
                    {
                        foreach (string s in this.engineTypesStringVP.Split("|".ToCharArray()))
                        {
                            if (s != null && s != "")
                            {
                                this.engineTypesVP.Add(s);
                            }
                        }
                    }
                }
                return this.engineTypesVP;
            }
        }

        /// <summary>
        /// Gets the <see cref="List{T}"/> of vehicle engine VIN codes that apply to this fix.
        /// NOTE: DO NOT add to this collection directly. Use the AddEngineVINCode() method.
        /// </summary>
        [PropertyDefinition("VIN Codes", "VinPower Vehicle Engine VIN codes that apply to this fix.")]
        public List<string> EngineVINCodesVP
        {
            get
            {
                if (this.engineVINCodesVP == null)
                {
                    this.EnsureLoaded();

                    this.engineVINCodesVP = new List<string>();

                    if (!this.isObjectCreated && this.engineVINCodesStringVP != "")
                    {
                        foreach (string s in this.engineVINCodesStringVP.Split("|".ToCharArray()))
                        {
                            if (s != null && s != "")
                            {
                                this.engineVINCodesVP.Add(s);
                            }
                        }
                    }
                }
                return this.engineVINCodesVP;
            }
        }

        /// <summary>
        /// Get the <see cref="List{T}"/> of vehicle makes that apply to this fix.
        /// NOTE: DO NOT add to this collection directly. Use the AddMake() method.
        /// </summary>
        [PropertyDefinition("Makes", "VinPower Makes of vehicles that apply to this fix.")]
        public List<string> MakesVP
        {
            get
            {
                if (this.makesVP == null)
                {
                    this.EnsureLoaded();

                    this.makesVP = new List<string>();

                    //load if not a user created element

                    if (!this.isObjectCreated && this.makesStringVP != "")
                    {
                        foreach (string s in this.makesStringVP.Split("|".ToCharArray()))
                        {
                            if (s != null && s != "")
                            {
                                this.makesVP.Add(s);
                            }
                        }
                    }
                }

                return this.makesVP;
            }
        }

        /// <summary>
        /// Get the <see cref="List{T}"/> of vehicle models that apply to this fix.
        /// NOTE: DO NOT add to this collection directly. Use the AddModel() method.
        /// </summary>
        [PropertyDefinition("Models", "VinPower Models of vehicles that apply to this fix.")]
        public List<string> ModelsVP
        {
            get
            {
                if (this.modelsVP == null)
                {
                    this.EnsureLoaded();

                    this.modelsVP = new List<string>();

                    if (!this.isObjectCreated && this.modelsStringVP != "")
                    {
                        foreach (string s in this.modelsStringVP.Split("|".ToCharArray()))
                        {
                            if (s != null && s != "")
                            {
                                this.modelsVP.Add(s);
                            }
                        }
                    }
                }

                return this.modelsVP;
            }
        }

        /// <summary>
        /// Get the <see cref="List{T}"/> of vehicle trim levels that apply to this fix.
        /// NOTE: DO NOT add to this collection directly. Use the AddTrimLevel() method.
        /// </summary>
        ///
        [PropertyDefinition("Trim Levels", "VinPower Trim Levels of vechicles that apply to this fix.")]
        public List<string> TrimLevelsVP
        {
            get
            {
                if (this.trimLevelsVP == null)
                {
                    this.EnsureLoaded();

                    this.trimLevelsVP = new List<string>();

                    if (!this.isObjectCreated && this.trimLevelsStringVP != "")
                    {
                        foreach (string s in this.trimLevelsStringVP.Split("|".ToCharArray()))
                        {
                            if (s != null && s != "")
                            {
                                this.trimLevelsVP.Add(s);
                            }
                        }
                    }
                }

                return this.trimLevelsVP;
            }
        }

        /// <summary>
        /// Get the <see cref="List{T}"/> of transmission control types that apply to this fix.
        /// NOTE: DO NOT add to this collection directly. Use the AddTransmissionControlType() method.
        /// </summary>
        [PropertyDefinition("Transmission", "VinPower Transmission control types that apply to this fix.")]
        public List<string> TransmissionControlTypesVP
        {
            get
            {
                if (this.transmissionControlTypesVP == null)
                {
                    this.EnsureLoaded();

                    this.transmissionControlTypesVP = new List<string>();

                    if (!this.isObjectCreated && this.transmissionsStringVP != "")
                    {
                        foreach (string s in this.transmissionsStringVP.Split("|".ToCharArray()))
                        {
                            if (s != null && s != "")
                            {
                                this.transmissionControlTypesVP.Add(s);
                            }
                        }
                    }
                }

                return this.transmissionControlTypesVP;
            }
        }

        /// <summary>
        /// Get an <see cref="List{T}"/> of <see cref="int"/> vehicle years that apply to this fix.
        /// NOTE: DO NOT add to this collection directly. Use the AddYear() method.
        /// </summary>
        [PropertyDefinition("Years", "VinPower Vehicle Years that apply to this fix.")]
        public List<int> YearsVP
        {
            get
            {
                if (this.yearsVP == null)
                {
                    this.EnsureLoaded();

                    this.yearsVP = new List<int>();

                    if (!this.isObjectCreated && this.yearsStringVP != "")
                    {
                        foreach (string s in this.yearsStringVP.Split("|".ToCharArray()))
                        {
                            if (s != null && s != "")
                            {
                                this.yearsVP.Add(Int32.Parse(s));
                            }
                        }
                    }
                }

                return this.yearsVP;
            }
        }

        /// <summary>
        /// Gets the <see cref="SmartCollection"/> of <see cref="FixDTC"/> objects associated with this <see cref="Fix"/> object.
        /// </summary>
        [PropertyDefinition("DTCs", "DTCs that apply to this fix.")]
        public SmartCollection FixDTCs
        {
            get
            {
                if (this.fixDTCs == null)
                {
                    this.fixDTCs = new SmartCollection();

                    if (!this.IsObjectCreated)
                    {
                        using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(this.Registry.ConnectionStringDefault))
                        {
                            dr.ProcedureName = "FixDTC_LoadByFix";
                            dr.AddGuid("FixId", this.Id);

                            dr.Execute();

                            while (dr.Read())
                            {
                                this.fixDTCs.Add(new FixDTC(this, dr.GetString("PrimaryDTC"), dr.GetString("SecondaryDTCList")));
                            }
                        }
                    }
                }
                return this.fixDTCs;
            }
        }

        /// <summary>
        /// Get all sorted DTCs for a Fix
        /// </summary>
        public List<string> FixDtcComboList
        {
            get
            {
                var sortedDtcs = new List<string>();
                if (this.FixDTCs == null || this.FixDTCs.Count <= 0) return new List<string>();
                foreach (FixDTC fixDtc in this.FixDTCs)
                {
                    if (string.IsNullOrWhiteSpace(fixDtc.SecondaryDTCList))
                    {
                        sortedDtcs.Add(fixDtc.PrimaryDTC);
                        continue;
                    }

                    var sortedList = new List<string> { fixDtc.PrimaryDTC };
                    sortedList.AddRange(fixDtc.SecondaryDTCList.Split(new char[] { ',' },
                        StringSplitOptions.RemoveEmptyEntries));
                    sortedList = sortedList.OrderBy(s => s).ToList();
                    sortedDtcs.Add(string.Join(",", sortedList));
                }

                return sortedDtcs;
            }
        }

        /// <summary>
		/// Gets the <see cref="SmartCollection"/> of <see cref="FixDTC"/> objects associated with this <see cref="Fix"/> object.
		/// </summary>
		[PropertyDefinition("Symptoms", "Symptoms that apply to this fix.")]
        public SmartCollection FixSymptoms
        {
            get
            {
                if (this.fixSymptoms == null)
                {
                    this.fixSymptoms = new SmartCollection();

                    if (!this.IsObjectCreated)
                    {
                        using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(this.Registry.ConnectionStringDefault))
                        {
                            dr.ProcedureName = "FixSymptom_LoadByFix";
                            dr.AddGuid("FixId", this.Id);

                            dr.Execute();

                            while (dr.Read())
                            {
                                this.fixSymptoms.Add(new FixSymptom(this, (Symptom)Registry.CreateInstance(typeof(Symptom), dr.GetGuid("SymptomId"))));
                            }
                        }
                    }
                }
                return this.fixSymptoms;
            }
        }

        //#Sprint23
        /// <summary>
        /// Gets or sets the <see cref="bool"/>
        /// </summary>
        [PropertyDefinition("Is Labor Required", "IsLaborRequired", "IsLaborRequired", "Indicates that fix labor is required.")]
        public bool IsLaborRequired
        {
            get
            {
                EnsureLoaded();
                return isLaborRequired;
            }
            set
            {
                EnsureLoaded();
                if (isLaborRequired != value)
                {
                    IsObjectDirty = true;
                    isLaborRequired = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/>
        /// </summary>
        [PropertyDefinition("Is Part Required", "Is Part Required", "Is Part Required", "Indicates that fix part is required.")]
        public bool IsPartRequired
        {
            get
            {
                EnsureLoaded();
                return isPartRequired;
            }
            set
            {
                EnsureLoaded();
                if (isPartRequired != value)
                {
                    IsObjectDirty = true;
                    isPartRequired = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/>
        /// </summary>
        [PropertyDefinition("Is Tool Required", "Is Tool Required", "Is Tool Required", "Indicates that fix tool is required.")]
        public bool IsToolRequired
        {
            get
            {
                EnsureLoaded();
                return isToolRequired;
            }
            set
            {
                EnsureLoaded();
                if (isToolRequired != value)
                {
                    IsObjectDirty = true;
                    isToolRequired = value;
                }
            }
        }

        //#Sprint23

        #endregion Object Properties (Related Objects)

        #region Business Logic Methods

        /***********************************************************************************************
		 *
		 * Custom Business Logic
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Gets the <see cref="string"/> DTC matching the primary DTC Code supplied
        /// </summary>
        /// <param name="primaryDTC"><see cref="string"/> primary DTC attempting to be matched</param>
        /// <returns><see cref="FixDTC"/> mataching the supplied primary DTC.  Returns null if no matches are found</returns>
        public FixDTC GetFixDTCMatchingPrimaryDTC(string primaryDTC)
        {
            foreach (FixDTC fDTC in this.FixDTCs)
            {
                if (fDTC.PrimaryDTC.ToLower().Trim() == primaryDTC.ToLower().Trim())
                {
                    return fDTC;
                }
            }
            return null;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="symptomId"></param>
        /// <returns></returns>
        public FixSymptom GetFixSymptomMatchingSymptom(Guid symptomId)
        {
            foreach (FixSymptom fst in this.FixSymptoms)
            {
                if (fst.Symptom.Id == symptomId)
                {
                    return fst;
                }
            }
            return null;
        }

        /// <summary>
        /// Gets the <see cref="string"/> comma separated list of <see cref="string"/> primary DTC's this fix has associated to it.
        /// </summary>
        /// <returns><see cref="string"/> comma separated list of <see cref="string"/> primary DTC codes this fix has associated to it.</returns>
        private string GetFixPrimaryDTCCodes()
        {
            string s = "";
            foreach (FixDTC fixDTC in this.FixDTCs)
            {
                if (s.Length > 0)
                {
                    s += ", ";
                }
                s += fixDTC.PrimaryDTC;
            }

            return s;
        }

        /// <summary>
        /// Calculates the costs of the fix to the object.
        /// </summary>
        public void CalculateFixCosts()
        {
            // Calculate the total cost of the solution
            this.laborRate = this.RuntimeInfo.CurrentStateLaborRateInUSD;
            this.laborCost = this.Labor * this.laborRate;
            this.partsCost = 0;
            foreach (FixPart p in this.FixParts)
            {
                this.partsCost += (p.Quantity * p.Part.Price);
            }

            this.totalCost = this.laborCost + this.AdditionalCost + this.partsCost;

            // Now calculate the local curreny values if necessary.
            if (this.RuntimeInfo.CurrentCurrency != Currency.USD)
            {
                this.laborRateInLocalCurrency = this.RuntimeInfo.GetLocalCurrencyValueFromUSDollars(this.LaborRate);
                this.laborCostInLocalCurrency = this.RuntimeInfo.GetLocalCurrencyValueFromUSDollars(this.LaborCost);
                this.additionalCostInLocalCurrency = this.RuntimeInfo.GetLocalCurrencyValueFromUSDollars(this.AdditionalCost);
                this.partsCostInLocalCurrency = this.RuntimeInfo.GetLocalCurrencyValueFromUSDollars(this.PartsCost);
                this.totalCostInLocalCurrency = this.RuntimeInfo.GetLocalCurrencyValueFromUSDollars(this.TotalCost);
            }
        }

        /// <summary>
        /// Sets the <see cref="bool"/> flag indicating whether or not the diagnostic report is an exact match for the
        /// </summary>
        /// <param name="isExactMatch"><see cref="bool"/> flag indicating that the diagnostic report is an exact match.</param>
        protected internal void SetDiagnosticReportIsExactMatch(bool isExactMatch)
        {
            this.diagnosticReportIsExactMatch = isExactMatch;
        }

        /// <summary>
        /// Sets the total count of secondary code matches to the diagnostic report.
        /// </summary>
        /// <param name="exactMatches"><see cref="int"/> secondary code matches</param>
        protected internal void SetDiagnosticReportSecondaryCodeAssignmentMatches(int exactMatches)
        {
            this.diagnosticReportSecondaryCodeAssignmentMatches = exactMatches;
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

                if (!String.IsNullOrEmpty(os))
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

        /// <summary>
        /// Returns a populated DiagnosticReportResultFix object.
        /// </summary>
        /// <param name="diagnosticReportErrorCodeSystemType">The <see cref="DiagnosticReportErrorCodeSystemType"/> system type of the code.</param>
        /// <returns>A populated <see cref="DiagnosticReportResultFix"/> object.</returns>
        public DiagnosticReportResultFix ToDiagnosticReportResultFix(DiagnosticReportErrorCodeSystemType diagnosticReportErrorCodeSystemType, DiagnosticReportResult diagnosticReportResult)
        {
            DiagnosticReportResultFix drrFix = (DiagnosticReportResultFix)Registry.CreateInstance(typeof(DiagnosticReportResultFix));
            drrFix.AdditionalCost = this.AdditionalCost;
            drrFix.AdditionalCostInLocalCurrency = this.AdditionalCostInLocalCurrency;
            drrFix.Description = this.Description;
            drrFix.DiagnosticReportErrorCodeSystemType = diagnosticReportErrorCodeSystemType;
            drrFix.DiagnosticReportIsExactMatch = this.DiagnosticReportIsExactMatch;
            drrFix.DiagnosticReportSecondaryCodeAssignmentMatches = this.DiagnosticReportSecondaryCodeAssignmentMatches;
            drrFix.Fix = this;
            drrFix.FixName = this.FixName;
            drrFix.FrequencyCount = this.FrequencyCount;
            drrFix.Labor = this.Labor;
            drrFix.LaborCost = this.LaborCost;
            drrFix.LaborCostInLocalCurrency = this.LaborCostInLocalCurrency;
            drrFix.LaborRate = this.LaborRate;
            drrFix.LaborRateInLocalCurrency = this.LaborRateInLocalCurrency;
            drrFix.Name = this.FixName.Description;
            drrFix.Name_es = this.FixName.Description_es;
            drrFix.Name_fr = this.FixName.Description_fr;
            drrFix.Name_zh = this.FixName.Description_zh;
            drrFix.PartsCost = this.PartsCost;
            drrFix.PartsCostInLocalCurrency = this.PartsCostInLocalCurrency;
            drrFix.TotalCost = this.TotalCost;
            drrFix.TotalCostInLocalCurrency = this.TotalCostInLocalCurrency;
            drrFix.DiagnosticReportResult = diagnosticReportResult;

            //#FixLogicStep
            drrFix.FixLogicStep = this.FixLogicStep;

            //#DiagnosticReportId
            drrFix.DiagnosticReportId = this.DiagnosticReportId;

            foreach (FixPart part in this.FixParts)
            {
                DiagnosticReportResultFixPart drrFixPart = part.ToDiagnosticReportResultFixPart(drrFix);
                drrFix.DiagnosticReportResultFixParts.Add(drrFixPart);
            }

            //ToolDB_
            foreach (FixTool tool in this.FixTools)
            {
                DiagnosticReportResultFixTool drrFixTool = tool.ToDiagnosticReportResultFixTool(drrFix);
                drrFix.DiagnosticReportResultFixTools.Add(drrFixTool);
            }
            //ToolDB_

            return drrFix;
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
                //load the base fix if user selected it.
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
            dr.ProcedureName = "Fix_Load";
            dr.AddGuid("FixId", Id);
        }

        /// <summary>
        /// Sets the properties of this object from the fields in the recordset located in the supplied <see cref="SqlDataReaderWrapper"/>.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> containing the recordset and fields used to set the properties of this object (this layer only).</param>
        protected new void SetPropertiesFromDataReader(SqlDataReaderWrapper dr)
        {
            this.fixName = (FixName)Registry.CreateInstance(typeof(FixName), dr.GetGuid("FixNameId"));

            this.obdFix = (ObdFix)dr.GetBusinessObjectBase(this.Registry, typeof(ObdFix), "ObdFixId");
            //Added on 2017-08-10 by INNOVA Dev Team (Nam Lu): Support ProRS Fix
            if (dr.ColumnExists("ProRSFixId"))
                this.proRsFix = (ProRSFix)dr.GetBusinessObjectBase(this.Registry, typeof(ProRSFix), "ProRSFixId");

            this.fixType = (FixType)dr.GetInt32("FixType");

            this.marketsString = dr.GetString("MarketsString");

            this.createdByAdminUser = (AdminUser)dr.GetBusinessObjectBase(this.Registry, typeof(AdminUser), "CreatedByAdminUserId");
            this.createdByUser = (User)dr.GetBusinessObjectBase(this.Registry, typeof(User), "CreatedByUserId");
            this.updatedByAdminUser = (AdminUser)dr.GetBusinessObjectBase(this.Registry, typeof(AdminUser), "UpdatedByAdminUserId");
            this.approvedByAdminUser = (AdminUser)dr.GetBusinessObjectBase(this.Registry, typeof(AdminUser), "ApprovedByAdminUserId");

            this.isApproved = dr.GetBoolean("IsApproved");
            this.isInitiallyReviewed = dr.GetBoolean("IsInitiallyReviewed");
            this.approvedDateTimeUTC = dr.GetNullableDateTime("ApprovedDateTimeUTC");
            this.hasEngineTypeDefined = dr.GetBoolean("HasEngineTypeDefined");
            this.engineTypesString = dr.GetString("EngineTypesString");
            this.hasEngineVINCodeDefined = dr.GetBoolean("HasEngineVINCodeDefined");
            this.engineVINCodesString = dr.GetString("EngineVINCodesString");
            this.hasYearDefined = dr.GetBoolean("HasYearDefined");
            this.yearsString = dr.GetString("YearsString");
            this.makesString = dr.GetString("MakesString");
            this.hasModelDefined = dr.GetBoolean("HasModelDefined");

            this.modelsString = dr.GetString("ModelsString");

            //#FixManufacturer
            if (dr.ColumnExists("HasManufacturerDefined"))
                this.hasManufacturerDefined = dr.GetBoolean("HasManufacturerDefined");
            if (dr.ColumnExists("ManufacturersString"))
                this.manufacturersString = dr.GetString("ManufacturersString");

            this.hastrimLevelDefined = dr.GetBoolean("HasTrimLevelDefined");
            this.trimLevelsString = dr.GetString("TrimLevelsString");
            this.hasTransmissionDefined = dr.GetBoolean("HasTransmissionDefined");
            this.transmissionsString = dr.GetString("TransmissionsString");
            this.hasGenerationDefined = dr.GetBoolean("HasGenerationDefined");
            this.generationsString = dr.GetString("GenerationsString");

            this.hasEngineTypeDefinedPolk = dr.GetBoolean("HasEngineTypeDefinedPolk");
            this.engineTypesStringPolk = dr.GetString("EngineTypesStringPolk");
            this.engineTypePolkVerifiedDateTimeUTC = dr.GetNullableDateTime("EngineTypePolkVerifiedDateTimeUTC");
            this.hasEngineVINCodeDefinedPolk = dr.GetBoolean("HasEngineVINCodeDefinedPolk");
            this.engineVINCodesStringPolk = dr.GetString("EngineVINCodesStringPolk");
            this.engineVINCodePolkVerifiedDateTimeUTC = dr.GetNullableDateTime("EngineVINCodePolkVerifiedDateTimeUTC");
            this.hasYearDefinedPolk = dr.GetBoolean("HasYearDefinedPolk");
            this.yearsStringPolk = dr.GetString("YearsStringPolk");
            this.yearPolkVerifiedDateTimeUTC = dr.GetNullableDateTime("YearPolkVerifiedDateTimeUTC");
            this.hasMakeDefinedPolk = dr.GetBoolean("HasMakeDefinedPolk");
            this.makesStringPolk = dr.GetString("MakesStringPolk");
            this.makePolkVerifiedDateTimeUTC = dr.GetNullableDateTime("MakePolkVerifiedDateTimeUTC");
            this.hasModelDefinedPolk = dr.GetBoolean("HasModelDefinedPolk");

            this.modelsStringPolk = dr.GetString("ModelsStringPolk");

            this.modelPolkVerifiedDateTimeUTC = dr.GetNullableDateTime("ModelPolkVerifiedDateTimeUTC");
            this.hastrimLevelDefinedPolk = dr.GetBoolean("HasTrimLevelDefinedPolk");
            this.trimLevelsStringPolk = dr.GetString("TrimLevelsStringPolk");
            this.trimLevelPolkVerifiedDateTimeUTC = dr.GetNullableDateTime("TrimLevelPolkVerifiedDateTimeUTC");

            this.hasEngineTypeDefinedVP = dr.GetBoolean("HasEngineTypeDefinedVP");
            this.engineTypesStringVP = dr.GetString("EngineTypesStringVP");
            this.hasEngineVINCodeDefinedVP = dr.GetBoolean("HasEngineVINCodeDefinedVP");
            this.engineVINCodesStringVP = dr.GetString("EngineVINCodesStringVP");
            this.hasYearDefinedVP = dr.GetBoolean("HasYearDefinedVP");
            this.yearsStringVP = dr.GetString("YearsStringVP");
            this.hasMakeDefinedVP = dr.GetBoolean("HasMakeDefinedVP");
            this.makesStringVP = dr.GetString("MakesStringVP");
            this.hasModelDefinedVP = dr.GetBoolean("HasModelDefinedVP");
            this.modelsStringVP = dr.GetString("ModelsStringVP");
            this.hastrimLevelDefinedVP = dr.GetBoolean("HasTrimLevelDefinedVP");
            this.trimLevelsStringVP = dr.GetString("TrimLevelsStringVP");
            this.hasTransmissionDefinedVP = dr.GetBoolean("HasTransmissionDefinedVP");
            this.transmissionsStringVP = dr.GetString("TransmissionsStringVP");

            this.description = dr.GetString("Description");
            this.description_es = dr.GetString("Description_es");
            this.description_fr = dr.GetString("Description_fr");
            this.description_zh = dr.GetString("Description_zh");
            this.labor = dr.GetDecimal("Labor");
            this.additionalCost = dr.GetDecimal("AdditionalCost");
            this.frequencyCount = dr.GetInt32("FrequencyCount");
            this.createdDateTimeUTC = dr.GetDateTime("CreatedDateTimeUTC");
            this.updatedDateTimeUTC = dr.GetDateTime("UpdatedDateTimeUTC");

            //#Sprint23
            this.isLaborRequired = dr.GetBoolean("IsLaborRequired");
            this.isPartRequired = dr.GetBoolean("IsPartRequired");
            this.isToolRequired = dr.GetBoolean("IsToolRequired");
            //#Sprint23

            //#FixLogicStep
            if (dr.ColumnExists("FixLogicStep"))
            {
                this.FixLogicStep = dr.GetInt32("FixLogicStep");
            }

            //#DiagnosticReportId
            if (dr.ColumnExists("DiagnosticReportId"))
            {
                this.DiagnosticReportId = dr.GetNullableGuid("DiagnosticReportId");
            }

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
                        dr.ProcedureName = "Fix_Create";
                        //Added on 2017-08-10 by INNOVA Dev Team (Nam Lu): Support ProRS Fix
                        if (this.CreatedDateTimeUTC == DateTime.MinValue)
                            CreatedDateTimeUTC = DateTime.Now.ToUniversalTime();

                        UpdatedDateTimeUTC = CreatedDateTimeUTC;
                    }
                    else
                    {
                        dr.ProcedureName = "Fix_Save";
                    }

                    dr.AddGuid("FixId", this.Id);
                    dr.AddGuid("FixNameId", this.FixName.Id);
                    dr.AddInt32("FixType", (int)this.FixType);
                    dr.AddBusinessObject("ObdFixId", this.ObdFix);

                    //Added on 2017-08-10 by Nam Lu - INNOVA Dev Team: Support ProRS Fix
                    dr.AddBusinessObject("ProRSFixId", this.ProRSFix);

                    if (this.isMarketsDirty)
                    {
                        string s = "";

                        foreach (Market m in this.Markets)
                        {
                            string intString = ((int)m).ToString();

                            if (s.Length > 0)
                            {
                                s += "|";
                            }
                            s += intString.Trim();
                        }

                        this.marketsString = s;
                    }
                    dr.AddNVarChar("MarketsString", this.marketsString);

                    dr.AddBusinessObject("CreatedByAdminUserId", this.CreatedByAdminUser);
                    dr.AddBusinessObject("CreatedByUserId", this.CreatedByUser);
                    dr.AddBusinessObject("UpdatedByAdminUserId", this.UpdatedByAdminUser);
                    dr.AddBusinessObject("ApprovedByAdminUserId", this.ApprovedByAdminUser);

                    dr.AddBoolean("IsApproved", IsApproved);
                    dr.AddBoolean("IsInitiallyReviewed", IsInitiallyReviewed);

                    dr.AddDateTime("ApprovedDateTimeUTC", this.ApprovedDateTimeUTC);

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

                    if (this.isMakesDirty)
                    {
                        this.makesString = this.BuildStringList(this.Makes);
                    }
                    dr.AddNVarChar("MakesString", this.makesString);

                    //#Fixmanufacturer
                    if (this.isManufacturersDirty)
                    {
                        this.manufacturersString = this.BuildStringList(this.Manufacturers);
                    }
                    dr.AddNVarChar("ManufacturersString", this.manufacturersString);

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

                    dr.AddBoolean("HasGenerationDefined", this.HasGenerationDefined);
                    if (this.isGenerationsDirty)
                    {
                        this.generationsString = this.BuildStringList(this.Generations);
                    }
                    dr.AddNVarChar("GenerationsString", this.generationsString);

                    dr.AddBoolean("HasEngineTypeDefinedPolk", this.HasEngineTypeDefinedPolk);
                    if (this.isEngineTypesDirtyPolk)
                    {
                        this.engineTypesStringPolk = this.BuildStringList(this.EngineTypesPolk);
                    }
                    dr.AddNVarChar("EngineTypesStringPolk", this.engineTypesStringPolk);
                    dr.AddDateTime("EngineTypePolkVerifiedDateTimeUTC", this.EngineTypePolkVerifiedDateTimeUTC);

                    dr.AddBoolean("HasEngineVINCodeDefinedPolk", this.HasEngineVINCodeDefinedPolk);
                    if (this.isEngineVINCodesDirtyPolk)
                    {
                        this.engineVINCodesStringPolk = this.BuildStringList(this.EngineVINCodesPolk);
                    }
                    dr.AddNVarChar("EngineVINCodesStringPolk", this.engineVINCodesStringPolk);
                    dr.AddDateTime("EngineVINCodePolkVerifiedDateTimeUTC", this.EngineVINCodePolkVerifiedDateTimeUTC);

                    dr.AddBoolean("HasYearDefinedPolk", HasYearDefinedPolk);
                    if (this.isYearsDirtyPolk)
                    {
                        this.yearsStringPolk = this.BuildStringList(this.YearsPolk);
                    }
                    dr.AddNVarChar("YearsStringPolk", this.yearsStringPolk);
                    dr.AddDateTime("YearPolkVerifiedDateTimeUTC", this.YearPolkVerifiedDateTimeUTC);

                    dr.AddBoolean("HasMakeDefinedPolk", HasMakeDefinedPolk);
                    if (this.isMakesDirtyPolk)
                    {
                        this.makesStringPolk = this.BuildStringList(this.MakesPolk);
                    }
                    dr.AddNVarChar("MakesStringPolk", this.makesStringPolk);
                    dr.AddDateTime("MakePolkVerifiedDateTimeUTC", this.MakePolkVerifiedDateTimeUTC);

                    dr.AddBoolean("HasModelDefinedPolk", HasModelDefinedPolk);
                    if (this.isModelsDirtyPolk)
                    {
                        this.modelsStringPolk = this.BuildStringList(this.ModelsPolk);
                    }
                    dr.AddNVarChar("ModelsStringPolk", this.modelsStringPolk);
                    dr.AddDateTime("ModelPolkVerifiedDateTimeUTC", this.ModelPolkVerifiedDateTimeUTC);

                    dr.AddBoolean("HasTrimLevelDefinedPolk", HasTrimLevelDefinedPolk);
                    if (this.isTrimLevelsDirtyPolk)
                    {
                        this.trimLevelsStringPolk = this.BuildStringList(this.TrimLevelsPolk);
                    }
                    dr.AddNVarChar("TrimLevelsStringPolk", this.trimLevelsStringPolk);
                    dr.AddDateTime("TrimLevelPolkVerifiedDateTimeUTC", this.TrimLevelPolkVerifiedDateTimeUTC);

                    dr.AddNVarChar("Description", this.Description);
                    dr.AddNVarChar("Description_es", this.Description_es);
                    dr.AddNVarChar("Description_fr", this.Description_fr);
                    dr.AddNVarChar("Description_zh", this.Description_zh);
                    dr.AddDecimal("Labor", Math.Round(Labor, 2));
                    dr.AddDecimal("AdditionalCost", Math.Round(AdditionalCost, 2));
                    dr.AddInt32("FrequencyCount", FrequencyCount);
                    dr.AddDateTime("CreatedDateTimeUTC", CreatedDateTimeUTC);
                    dr.AddDateTime("UpdatedDateTimeUTC", UpdatedDateTimeUTC);

                    //#Sprint23
                    dr.AddBoolean("IsLaborRequired", IsLaborRequired);
                    dr.AddBoolean("IsPartRequired", IsPartRequired);
                    dr.AddBoolean("IsToolRequired", IsToolRequired);
                    //#Sprint23

                    dr.Execute(transaction);
                }

                IsObjectDirty = false;
            }

            // If the approved flag was changed to true then we need to check and see if an OBDFix technician should be paid.
            if (isApprovedChangedToTrue)
            {
                if (ObdFix != null)
                {
                    ObdFix.Load(connection, transaction, true);
                    ObdFix.DiagnosticReportFixFeedback.Load(connection, transaction, true);
                    ObdFix.User.Load(connection, transaction, true);

                    ObdFix.DiagnosticReportFixFeedback.PaymentAmount = 0;

                    if (ObdFix.DiagnosticReportFixFeedback.PaymentDateTimeUTC.IsNull)
                    {
                        if (ObdFix.DiagnosticReportFixFeedback.IsPaymentDtcAllowed.IsTrue && ObdFix.User.IsPaymentProgramMember)
                        {
                            string dtcPrefix = "";

                            if (
                                ObdFix.DiagnosticReportFixFeedback.DiagnosticReport.DiagnosticReportResult
                                    .DiagnosticReportResultErrorCodes.Count > 0)
                            {
                                string errorCode =
                                    ObdFix.DiagnosticReportFixFeedback.DiagnosticReport.DiagnosticReportResult
                                        .DiagnosticReportResultErrorCodes[0].ErrorCode;

                                if (Regex.IsMatch(errorCode, @"^\d{1,3}$"))
                                {
                                    dtcPrefix = "|";
                                }
                                else if (Regex.IsMatch(errorCode, @"^[bcpuBCPU]{1}\d{4}$"))
                                {
                                    dtcPrefix = errorCode.Substring(0, 1);
                                }
                            }
                            else
                            {
                                //todo: need to add Symptom Type to OBDFIX for faster load
                                //try to load Symptom if Any
                                var symptomReports =
                                         SymptomDiagnosticReportItemCollection.Search(this.Registry,
                                            $"<GuidList><Guid id=\"{ObdFix.DiagnosticReportFixFeedback.DiagnosticReport.Id}\"/></GuidList>");

                                if (symptomReports.Count > 0)
                                    dtcPrefix = "S";
                            }

                            ObdFixPaymentSchedule ps = ObdFixPaymentSchedule.GetByUserAndDTCPrefix(Registry, ObdFix.User, dtcPrefix);

                            if (ps != null)
                            {
                                ObdFix.DiagnosticReportFixFeedback.PaymentAmount = ps.PaymentAmount;
                            }
                            else
                            {
                                ObdFix.DiagnosticReportFixFeedback.PaymentAmount = 0;
                            }
                        }

                        ObdFix.DiagnosticReportFixFeedback.PaymentDateTimeUTC = DateTime.UtcNow;
                        transaction = ObdFix.DiagnosticReportFixFeedback.Save(connection, transaction);
                    }
                }
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
            if (this.fixDTCs != null)
            {
                //delete all of the existing fix DTCs and add all new ones
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    dr.ProcedureName = "FixDTC_DeleteByFix";
                    dr.AddGuid("FixId", this.Id);

                    dr.ExecuteNonQuery(transaction);

                    dr.ClearParameters();

                    foreach (FixDTC fixDTC in this.fixDTCs)
                    {
                        dr.ProcedureName = "FixDTC_Create";
                        dr.AddGuid("FixId", this.Id);
                        dr.AddNVarChar("PrimaryDTC", fixDTC.PrimaryDTC);
                        dr.AddNVarChar("SecondaryDTCList", fixDTC.SecondaryDTCList);

                        dr.ExecuteNonQuery(transaction);
                        dr.ClearParameters();
                    }
                }
            }

            //Save Fix Symptom
            if (this.fixSymptoms != null)
            {
                //delete all of the existing fix Symptom and add all new ones
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    //todo: if delete all fix Symptom links, must create symptom selection on Fix edit page for re-select symptom list for fix
                    dr.ProcedureName = "FixSymptom_DeleteByFix";
                    dr.AddGuid("FixId", this.Id);

                    dr.ExecuteNonQuery(transaction);

                    dr.ClearParameters();

                    foreach (FixSymptom fixSymptom in this.fixSymptoms)
                    {
                        dr.ProcedureName = "FixSymptom_Create"; //create new link if not existed
                        dr.AddGuid("FixId", this.Id);
                        dr.AddGuid("SymptomId", fixSymptom.Symptom.Id);

                        dr.ExecuteNonQuery(transaction);
                        dr.ClearParameters();
                    }
                }
            }

            if (this.fixParts != null)
            {
                for (int i = 0; i < this.FixParts.Removed.Count; i++)
                {
                    transaction = ((FixPart)FixParts.Removed[i]).Delete(connection, transaction);
                }
                for (int i = 0; i < this.FixParts.Count; i++)
                {
                    transaction = this.FixParts[i].Save(connection, transaction);
                }
            }

            //ToolDB_
            if (this.fixTools != null)
            {
                for (int i = 0; i < this.FixTools.Removed.Count; i++)
                {
                    transaction = ((FixTool)FixTools.Removed[i]).Delete(connection, transaction);
                }
                for (int i = 0; i < this.FixTools.Count; i++)
                {
                    transaction = this.FixTools[i].Save(connection, transaction);
                }
            }
            //ToolDB_

            if (this.isTrimLevelsDirty)
            {
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    dr.ProcedureName = "Fix_SaveTrimLevels";
                    dr.AddGuid("FixId", Id);
                    dr.AddNText("TrimLevelsXmlList", Metafuse3.Xml.XmlList.ToXml(this.TrimLevels));
                    dr.ExecuteNonQuery(transaction);
                }
                this.isTrimLevelsDirty = false;
            }

            if (this.isEngineVINCodesDirty)
            {
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    dr.ProcedureName = "Fix_SaveEngineVINCode";
                    dr.AddGuid("FixId", Id);
                    dr.AddNText("XmlList", Metafuse3.Xml.XmlList.ToXml(this.EngineVINCodes));
                    dr.ExecuteNonQuery(transaction);
                }
                this.isEngineVINCodesDirty = false;
            }

            if (this.isEngineTypesDirty)
            {
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    dr.ProcedureName = "Fix_SaveEngineType";
                    dr.AddGuid("FixId", Id);
                    dr.AddNText("XmlList", Metafuse3.Xml.XmlList.ToXml(this.EngineTypes));
                    dr.ExecuteNonQuery(transaction);
                }
                this.isEngineTypesDirty = false;
            }

            //#FixManufacturer
            if (this.isManufacturersDirty)
            {
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    dr.ProcedureName = "Fix_SaveManufactures";
                    dr.AddGuid("FixId", Id);
                    dr.AddNText("ManufacturersXmlList", Metafuse3.Xml.XmlList.ToXml(this.Manufacturers));
                    dr.ExecuteNonQuery(transaction);
                }
                this.isManufacturersDirty = false;
            }

            if (this.isMakesDirty)
            {
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    dr.ProcedureName = "Fix_SaveMakes";
                    dr.AddGuid("FixId", Id);
                    dr.AddNText("MakesXmlList", Metafuse3.Xml.XmlList.ToXml(this.Makes));
                    dr.ExecuteNonQuery(transaction);
                }
                this.isMakesDirty = false;
            }
            if (this.isModelsDirty)
            {
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    dr.ProcedureName = "Fix_SaveModels";
                    dr.AddGuid("FixId", Id);
                    dr.AddNText("ModelsXmlList", Metafuse3.Xml.XmlList.ToXml(this.Models).Replace("&amp;", "&").Replace("&", "&amp;"));
                    dr.ExecuteNonQuery(transaction);
                }
                this.isModelsDirty = false;
            }
            if (this.isYearsDirty)
            {
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    dr.ProcedureName = "Fix_SaveYears";
                    dr.AddGuid("FixId", Id);
                    dr.AddNText("YearsXmlList", Metafuse3.Xml.XmlList.ToXml(this.Years));
                    dr.ExecuteNonQuery(transaction);
                }
                this.isYearsDirty = false;
            }
            if (this.isTransmissionsDirty)
            {
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    dr.ProcedureName = "Fix_SaveTransmissionControlTypes";
                    dr.AddGuid("FixId", Id);
                    dr.AddNText("TransmissionControlTypesXmlList", Metafuse3.Xml.XmlList.ToXml(TransmissionControlTypes));
                    dr.ExecuteNonQuery(transaction);
                }
                this.isTransmissionsDirty = false;
            }
            if (this.isGenerationsDirty)
            {
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    dr.ProcedureName = "Fix_SaveGenerations";
                    dr.AddGuid("FixId", Id);
                    dr.AddNText("GenerationsXmlList", Metafuse3.Xml.XmlList.ToXml(this.Generations));
                    dr.ExecuteNonQuery(transaction);
                }
                this.isTransmissionsDirty = false;
            }

            if (this.isTrimLevelsDirtyPolk)
            {
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    dr.ProcedureName = "Fix_SaveTrimLevelsPolk";
                    dr.AddGuid("FixId", Id);
                    dr.AddNText("TrimLevelsXmlList", Metafuse3.Xml.XmlList.ToXml(this.TrimLevels));
                    dr.ExecuteNonQuery(transaction);
                }
                this.isTrimLevelsDirtyPolk = false;
            }
            if (this.isEngineVINCodesDirtyPolk)
            {
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    dr.ProcedureName = "Fix_SaveEngineVINCodePolk";
                    dr.AddGuid("FixId", Id);
                    dr.AddNText("XmlList", Metafuse3.Xml.XmlList.ToXml(this.EngineVINCodes));
                    dr.ExecuteNonQuery(transaction);
                }
                this.isEngineVINCodesDirtyPolk = false;
            }
            if (this.isEngineTypesDirtyPolk)
            {
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    dr.ProcedureName = "Fix_SaveEngineTypePolk";
                    dr.AddGuid("FixId", Id);
                    dr.AddNText("XmlList", Metafuse3.Xml.XmlList.ToXml(this.EngineTypes));
                    dr.ExecuteNonQuery(transaction);
                }
                this.isEngineTypesDirtyPolk = false;
            }
            if (this.isMakesDirtyPolk)
            {
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    dr.ProcedureName = "Fix_SaveMakesPolk";
                    dr.AddGuid("FixId", Id);
                    dr.AddNText("MakesXmlList", Metafuse3.Xml.XmlList.ToXml(this.Makes));
                    dr.ExecuteNonQuery(transaction);
                }
                this.isMakesDirtyPolk = false;
            }
            if (this.isModelsDirtyPolk)
            {
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    dr.ProcedureName = "Fix_SaveModelsPolk";
                    dr.AddGuid("FixId", Id);
                    dr.AddNText("ModelsXmlList", Metafuse3.Xml.XmlList.ToXml(this.Models).Replace("&amp;", "&").Replace("&", "&amp;"));
                    dr.ExecuteNonQuery(transaction);
                }
                this.isModelsDirtyPolk = false;
            }
            if (this.isYearsDirtyPolk)
            {
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    dr.ProcedureName = "Fix_SaveYearsPolk";
                    dr.AddGuid("FixId", Id);
                    dr.AddNText("YearsXmlList", Metafuse3.Xml.XmlList.ToXml(this.Years));
                    dr.ExecuteNonQuery(transaction);
                }
                this.isYearsDirtyPolk = false;
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

            //Copy deleted Fix to Audit DB first
            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                dr.ProcedureName = "Audit_CopyFix";
                dr.AddGuid("FixId", Id);
                dr.ExecuteNonQuery(transaction);
            }

            //Copy deleted Fix to Audit DB first

            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                dr.ProcedureName = "FixTrimLevel_DeleteByFix";
                dr.AddGuid("FixId", Id);
                dr.ExecuteNonQuery(transaction);
            }

            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                dr.ProcedureName = "FixEngineType_DeleteByFix";
                dr.AddGuid("FixId", Id);
                dr.ExecuteNonQuery(transaction);
            }

            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                dr.ProcedureName = "FixEngineVINCode_DeleteByFix";
                dr.AddGuid("FixId", Id);
                dr.ExecuteNonQuery(transaction);
            }

            //#FixManufacturer
            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                dr.ProcedureName = "FixManufacturer_DeleteByFix";
                dr.AddGuid("FixId", Id);
                dr.ExecuteNonQuery(transaction);
            }

            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                dr.ProcedureName = "FixMake_DeleteByFix";
                dr.AddGuid("FixId", Id);
                dr.ExecuteNonQuery(transaction);
            }

            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                dr.ProcedureName = "FixModel_DeleteByFix";
                dr.AddGuid("FixId", Id);
                dr.ExecuteNonQuery(transaction);
            }

            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                dr.ProcedureName = "FixTransmission_DeleteByFix";
                dr.AddGuid("FixId", Id);
                dr.ExecuteNonQuery(transaction);
            }

            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                dr.ProcedureName = "FixYear_DeleteByFix";
                dr.AddGuid("FixId", Id);
                dr.ExecuteNonQuery(transaction);
            }

            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                dr.ProcedureName = "FixGeneration_DeleteByFix";
                dr.AddGuid("FixId", Id);
                dr.ExecuteNonQuery(transaction);
            }

            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                dr.ProcedureName = "FixTrimLevelPolk_DeleteByFix";
                dr.AddGuid("FixId", Id);
                dr.ExecuteNonQuery(transaction);
            }

            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                dr.ProcedureName = "FixEngineTypePolk_DeleteByFix";
                dr.AddGuid("FixId", Id);
                dr.ExecuteNonQuery(transaction);
            }

            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                dr.ProcedureName = "FixEngineVINCodePolk_DeleteByFix";
                dr.AddGuid("FixId", Id);
                dr.ExecuteNonQuery(transaction);
            }

            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                dr.ProcedureName = "FixMakePolk_DeleteByFix";
                dr.AddGuid("FixId", Id);
                dr.ExecuteNonQuery(transaction);
            }

            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                dr.ProcedureName = "FixModelPolk_DeleteByFix";
                dr.AddGuid("FixId", Id);
                dr.ExecuteNonQuery(transaction);
            }

            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                dr.ProcedureName = "FixYearPolk_DeleteByFix";
                dr.AddGuid("FixId", Id);
                dr.ExecuteNonQuery(transaction);
            }

            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                dr.ProcedureName = "FixExtraFixStepData_DeleteByFix";
                dr.AddGuid("FixId", Id);
                dr.ExecuteNonQuery(transaction);
            }

            foreach (FixPart fp in FixParts)
            {
                transaction = fp.Delete(connection, transaction);
            }

            //ToolDB_
            foreach (FixTool ft in FixTools)
            {
                transaction = ft.Delete(connection, transaction);
            }
            //ToolDB_

            // delete this current object
            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                //delete the fix
                dr.ProcedureName = "Fix_Delete";
                dr.AddGuid("FixId", Id);

                dr.ExecuteNonQuery(transaction);
            }

            transaction = base.Delete(connection, transaction);

            return transaction;
        }

        #endregion Required Object Methods (Load, Save, Delete, Etc)
    }
}
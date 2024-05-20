using Innova.Fixes;
using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using Metafuse3.NullableTypes;
using System;
using System.Data.SqlClient;

namespace Innova.DiagnosticReports
{
    /// <summary>
    /// The DiagnosticReportResultFix object handles the business logic and data access for the specialized business object, DiagnosticReportResultFix.
    /// The class inherits from <see cref="BusinessObjectBase"/> class.
    /// </summary>
    /// <example>
    /// See the examples of how to construct the DiagnosticReportResultFix object.
    ///
    /// To create a new instance of a new of DiagnosticReportResultFix.
    /// <code>DiagnosticReportResultFix o = (DiagnosticReportResultFix)Registry.CreateInstance(typeof(DiagnosticReportResultFix));</code>
    ///
    /// To create an new instance of an existing DiagnosticReportResultFix.
    /// <code>DiagnosticReportResultFix o = (DiagnosticReportResultFix)Registry.CreateInstance(typeof(DiagnosticReportResultFix), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of DiagnosticReportResultFix, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectBase.ConnectionString"/> to the <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable()]
    public class DiagnosticReportResultFix : InnovaBusinessObjectBase
    {
        private DiagnosticReportResult diagnosticReportResult;
        private Fix fix;
        private FixName fixName;
        private string primaryErrorCode;

        //private     Symptom                              symptom;
        private DiagnosticReportErrorCodeSystemType diagnosticReportErrorCodeSystemType;

        private int sortOrder;
        private string name = "";
        private string name_es = "";
        private string name_fr = "";
        private string name_zh = "";
        private string description = "";
        private string description_es = "";
        private string description_fr = "";
        private string description_zh = "";
        private decimal labor;
        private decimal laborRate;
        private decimal laborCost;
        private NullableDecimal laborRateInLocalCurrency = NullableDecimal.Null;
        private NullableDecimal laborCostInLocalCurrency = NullableDecimal.Null;
        private decimal additionalCost;
        private NullableDecimal additionalCostInLocalCurrency = NullableDecimal.Null;
        private int frequencyCount;
        private decimal totalCost;
        private NullableDecimal totalCostInLocalCurrency = NullableDecimal.Null;
        private decimal partsCost;
        private NullableDecimal partsCostInLocalCurrency = NullableDecimal.Null;

        private bool diagnosticReportIsExactMatch;
        private int diagnosticReportSecondaryCodeAssignmentMatches;
        private bool isFromPolkMatch;
        private bool isFromVinPowerMatch;

        /// <summary>
        /// The <see cref="NullableDecimal"/> fix percentage.
        /// </summary>
        public NullableDecimal FixPercentage = NullableDecimal.Null;

        /// <summary>
        /// The <see cref="string"/> description of how the fixes were sorted.
        /// </summary>
        public string SortDescription = "";

        private DiagnosticReportResultFixPartCollection diagnosticReportResultFixParts;

        private DiagnosticReportResultFixToolCollection diagnosticReportResultFixTools;//ToolDB_

        #region System Constructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new (one that does not exist, assigning a new <see cref="Guid"/> Id). DiagnosticReportResultFix object.
        /// In order to create a new DiagnosticReportResultFix which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <example>
        /// <code>
        /// DiagnosticReportResultFix o = (DiagnosticReportResultFix)Registry.CreateInstance(typeof(DiagnosticReportResultFix));
        /// </code>
        /// </example>
        protected internal DiagnosticReportResultFix() : base()
        {
            IsObjectCreated = true;
            IsObjectLoaded = true;
            IsObjectDirty = true;
        }

        /// <summary>
        /// Constructor creates an existing (one that exists, and already has a <see cref="Guid"/> Id).  DiagnosticReportResultFix object.
        /// In order to create an existing DiagnosticReportResultFix object, which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <param name="id">The <see cref="Guid"/> or SQL uniqueidentifier indentification "Id" of the current object.</param>
        /// <example>
        /// <code>
        /// DiagnosticReportResultFix o = (DiagnosticReportResultFix)Registry.CreateInstance(typeof(DiagnosticReportResultFix), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));
        /// </code>
        /// </example>
        protected internal DiagnosticReportResultFix(Guid id) : base(id)
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

        //#FixLogicStep
        /// <summary>
        /// FixLogicStep
        /// </summary>
        public int FixLogicStep { get; set; }

        //#DiagnosticReportId
        /// <summary>
        /// DiagnosticReportId
        /// </summary>
        public NullableGuid DiagnosticReportId { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DiagnosticReportResult"/>
        /// </summary>
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
        /// Gets or sets the <see cref="Fix"/> the result was created from.  This record could be orphaned, so if accessed you need to test the "Load" to ensure that the record still exists in the DB.
        /// </summary>
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
                if (this.fix != value)
                {
                    this.IsObjectDirty = true;
                    this.fix = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="FixName"/> that was associated to the fix when the result was created.
        /// </summary>
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
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> primary error code for the fix.
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
                if (this.primaryErrorCode != value)
                {
                    this.IsObjectDirty = true;
                    this.primaryErrorCode = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DiagnosticReportErrorCodeSystemType"/>
        /// </summary>
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
                this.EnsureLoaded();
                return this.sortOrder;
            }
            set
            {
                this.EnsureLoaded();
                if (this.sortOrder != value)
                {
                    this.IsObjectDirty = true;
                    this.sortOrder = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        public string Name
        {
            get
            {
                this.EnsureLoaded();
                return this.name;
            }
            set
            {
                this.EnsureLoaded();
                if (this.name != value)
                {
                    this.IsObjectDirty = true;
                    this.name = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        public string Name_es
        {
            get
            {
                this.EnsureLoaded();
                return this.name_es;
            }
            set
            {
                this.EnsureLoaded();
                if (this.name_es != value)
                {
                    this.IsObjectDirty = true;
                    this.name_es = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        public string Name_fr
        {
            get
            {
                this.EnsureLoaded();
                return this.name_fr;
            }
            set
            {
                this.EnsureLoaded();
                if (this.name_fr != value)
                {
                    this.IsObjectDirty = true;
                    this.name_fr = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        public string Name_zh
        {
            get
            {
                this.EnsureLoaded();
                return this.name_zh;
            }
            set
            {
                this.EnsureLoaded();
                if (this.name_zh != value)
                {
                    this.IsObjectDirty = true;
                    this.name_zh = value;
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> name in the language specified in the Registry.
        /// </summary>
        public string Name_Translated
        {
            get
            {
                return this.RuntimeInfo.GetTranslatedValue(this.Name, this.Name_es, this.Name_fr, this.Name_zh);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
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
        /// Gets the <see cref="string"/> description in the language specified in the Registry.
        /// </summary>
        public string Description_Translated
        {
            get
            {
                return this.RuntimeInfo.GetTranslatedValue(this.Description, this.Description_es, this.Description_fr, this.Description_zh);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="decimal"/>
        /// </summary>
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
        public decimal LaborRate
        {
            get
            {
                this.EnsureLoaded();
                return this.laborRate;
            }
            set
            {
                this.EnsureLoaded();
                if (this.laborRate != value)
                {
                    this.IsObjectDirty = true;
                    this.laborRate = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="decimal"/>
        /// </summary>
        public decimal LaborCost
        {
            get
            {
                this.EnsureLoaded();
                return this.laborCost;
            }
            set
            {
                this.EnsureLoaded();
                if (this.laborCost != value)
                {
                    this.IsObjectDirty = true;
                    this.laborCost = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableDecimal"/> labor rate in the user's local currency
        /// </summary>
        public NullableDecimal LaborRateInLocalCurrency
        {
            get
            {
                this.EnsureLoaded();
                return this.laborRateInLocalCurrency;
            }
            set
            {
                this.EnsureLoaded();
                if (this.laborRateInLocalCurrency != value)
                {
                    this.IsObjectDirty = true;
                    this.laborRateInLocalCurrency = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableDecimal"/> labor cost in the user's local currency
        /// </summary>
        public NullableDecimal LaborCostInLocalCurrency
        {
            get
            {
                this.EnsureLoaded();
                return this.laborCostInLocalCurrency;
            }
            set
            {
                this.EnsureLoaded();
                if (this.laborCostInLocalCurrency != value)
                {
                    this.IsObjectDirty = true;
                    this.laborCostInLocalCurrency = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="decimal"/>
        /// </summary>
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
        /// Gets or sets the <see cref="NullableDecimal"/> additional cost in the user's local currency.
        /// </summary>
        public NullableDecimal AdditionalCostInLocalCurrency
        {
            get
            {
                this.EnsureLoaded();
                return this.additionalCostInLocalCurrency;
            }
            set
            {
                this.EnsureLoaded();
                if (this.additionalCostInLocalCurrency != value)
                {
                    this.IsObjectDirty = true;
                    this.additionalCostInLocalCurrency = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/>
        /// </summary>
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
        /// Gets or sets the <see cref="decimal"/>
        /// </summary>
        public decimal TotalCost
        {
            get
            {
                this.EnsureLoaded();
                return this.totalCost;
            }
            set
            {
                this.EnsureLoaded();
                if (this.totalCost != value)
                {
                    this.IsObjectDirty = true;
                    this.totalCost = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableDecimal"/> total cost in the user's local currency.
        /// </summary>
        public NullableDecimal TotalCostInLocalCurrency
        {
            get
            {
                this.EnsureLoaded();
                return this.totalCostInLocalCurrency;
            }
            set
            {
                this.EnsureLoaded();
                if (this.totalCostInLocalCurrency != value)
                {
                    this.IsObjectDirty = true;
                    this.totalCostInLocalCurrency = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="decimal"/>
        /// </summary>
        public decimal PartsCost
        {
            get
            {
                this.EnsureLoaded();
                return this.partsCost;
            }
            set
            {
                this.EnsureLoaded();
                if (this.partsCost != value)
                {
                    this.IsObjectDirty = true;
                    this.partsCost = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableDecimal"/> parts cost in the user's local currency.
        /// </summary>
        public NullableDecimal PartsCostInLocalCurrency
        {
            get
            {
                this.EnsureLoaded();
                return this.partsCostInLocalCurrency;
            }
            set
            {
                this.EnsureLoaded();
                if (this.partsCostInLocalCurrency != value)
                {
                    this.IsObjectDirty = true;
                    this.partsCostInLocalCurrency = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/>
        /// </summary>
        public bool DiagnosticReportIsExactMatch
        {
            get
            {
                this.EnsureLoaded();
                return this.diagnosticReportIsExactMatch;
            }
            set
            {
                this.EnsureLoaded();
                if (this.diagnosticReportIsExactMatch != value)
                {
                    this.IsObjectDirty = true;
                    this.diagnosticReportIsExactMatch = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/>
        /// </summary>
        public int DiagnosticReportSecondaryCodeAssignmentMatches
        {
            get
            {
                this.EnsureLoaded();
                return this.diagnosticReportSecondaryCodeAssignmentMatches;
            }
            set
            {
                this.EnsureLoaded();
                if (this.diagnosticReportSecondaryCodeAssignmentMatches != value)
                {
                    this.IsObjectDirty = true;
                    this.diagnosticReportSecondaryCodeAssignmentMatches = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/> indicating if the fix was found as a result of Polk vehicle YMME values that matched the fix.
        /// </summary>
        public bool IsFromPolkMatch
        {
            get
            {
                this.EnsureLoaded();
                return this.isFromPolkMatch;
            }
            set
            {
                this.EnsureLoaded();
                if (this.isFromPolkMatch != value)
                {
                    this.IsObjectDirty = true;
                    this.isFromPolkMatch = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/> indicating if the fix was found as a result of VinPower vehicle YMME values that matched the fix.
        /// </summary>
        public bool IsFromVinPowerMatch
        {
            get
            {
                this.EnsureLoaded();
                return this.isFromVinPowerMatch;
            }
            set
            {
                this.EnsureLoaded();
                if (this.isFromVinPowerMatch != value)
                {
                    this.IsObjectDirty = true;
                    this.isFromVinPowerMatch = value;
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="decimal"/> larbor rate to be presented based on the currenct currency in the <see cref="RuntimeInfo"/>.
        /// </summary>
        public decimal LaborRate_Presented
        {
            get
            {
                return this.DiagnosticReport.GetCurrencyValueForPresentration(this.LaborRate, this.LaborRateInLocalCurrency);
            }
        }

        /// <summary>
        /// Gets the <see cref="decimal"/> larbor cost to be presented based on the currenct currency in the <see cref="RuntimeInfo"/>.
        /// </summary>
        public decimal LaborCost_Presented
        {
            get
            {
                return this.DiagnosticReport.GetCurrencyValueForPresentration(this.LaborCost, this.LaborCostInLocalCurrency);
            }
        }

        /// <summary>
        /// Gets the <see cref="decimal"/> additional cost to be presented based on the currenct currency in the <see cref="RuntimeInfo"/>.
        /// </summary>
        public decimal AdditionalCost_Presented
        {
            get
            {
                return this.DiagnosticReport.GetCurrencyValueForPresentration(this.AdditionalCost, this.AdditionalCostInLocalCurrency);
            }
        }

        /// <summary>
        /// Gets the <see cref="decimal"/> parts cost to be presented based on the currenct currency in the <see cref="RuntimeInfo"/>.
        /// </summary>
        public decimal PartsCost_Presented
        {
            get
            {
                return this.DiagnosticReport.GetCurrencyValueForPresentration(this.PartsCost, this.PartsCostInLocalCurrency);
            }
        }

        /// <summary>
        /// Gets the <see cref="decimal"/> total cost to be presented based on the currenct currency in the <see cref="RuntimeInfo"/>.
        /// </summary>
        public decimal TotalCost_Presented
        {
            get
            {
                return this.DiagnosticReport.GetCurrencyValueForPresentration(this.TotalCost, this.TotalCostInLocalCurrency);
            }
        }

        private DiagnosticReport DiagnosticReport
        {
            get
            {
                return this.DiagnosticReportResult.DiagnosticReport;
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
        /// Get the <see cref="DiagnosticReportResultFixPartCollection"/> of part that apply to this fix.
        /// </summary>
        public DiagnosticReportResultFixPartCollection DiagnosticReportResultFixParts
        {
            get
            {
                if (diagnosticReportResultFixParts == null)
                {
                    diagnosticReportResultFixParts = new DiagnosticReportResultFixPartCollection(Registry);

                    //load if not a user created element
                    if (!isObjectCreated)
                    {
                        SqlProcedureCommand call = new SqlProcedureCommand();
                        EnsureValidId();

                        call.ProcedureName = "DiagnosticReportResultFixPart_LoadBydiagnosticReportResultFix";
                        call.AddGuid("DiagnosticReportResultFixId", Id);

                        diagnosticReportResultFixParts.Load(call, "DiagnosticReportResultFixPartId", true, true);
                    }
                }

                return diagnosticReportResultFixParts;
            }
        }

        //ToolDB_
        /// <summary>
        /// Get the <see cref="DiagnosticReportResultFixToolCollection"/> of part that apply to this fix.
        /// </summary>
        public DiagnosticReportResultFixToolCollection DiagnosticReportResultFixTools
        {
            get
            {
                if (diagnosticReportResultFixTools == null)
                {
                    diagnosticReportResultFixTools = new DiagnosticReportResultFixToolCollection(Registry);

                    //load if not a user created element
                    if (!isObjectCreated)
                    {
                        SqlProcedureCommand call = new SqlProcedureCommand();
                        EnsureValidId();

                        call.ProcedureName = "DiagnosticReportResultFixTool_LoadByDiagnosticReportResultFix";
                        call.AddGuid("DiagnosticReportResultFixId", Id);

                        diagnosticReportResultFixTools.Load(call, "DiagnosticReportResultFixToolId", true, true);
                    }
                }

                return diagnosticReportResultFixTools;
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

        /// <summary>
        /// Gets the <see cref="DiagnosticReport"/> associated with this fix.
        /// </summary>
        /// <returns>A <see cref="DiagnosticReport"/> object.</returns>
        public DiagnosticReport GetDiagnosticReport()
        {
            DiagnosticReport report = null;

            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(ConnectionString))
            {
                dr.ProcedureName = "DiagnosticReport_GetByDiagnosticReportResultFix";
                dr.AddGuid("DiagnosticReportResultFixId", this.Id);

                dr.Execute();

                if (dr.Read())
                {
                    report = (DiagnosticReport)Registry.CreateInstance(typeof(DiagnosticReport), dr.GetGuid("DiagnosticReportId"));
                    report.LoadPropertiesFromDataReader(dr, true);
                }
            }

            return report;
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
                //load the base diagnosticReportResultFix if user selected it.
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
            dr.ProcedureName = "DiagnosticReportResultFix_Load";
            dr.AddGuid("DiagnosticReportResultFixId", Id);
        }

        /// <summary>
        /// Sets the properties of this object from the fields in the recordset located in the supplied <see cref="SqlDataReaderWrapper"/>.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> containing the recordset and fields used to set the properties of this object (this layer only).</param>
        protected new void SetPropertiesFromDataReader(SqlDataReaderWrapper dr)
        {
            this.diagnosticReportResult = (DiagnosticReportResult)Registry.CreateInstance(typeof(DiagnosticReportResult), dr.GetGuid("DiagnosticReportResultId"));
            if (!dr.IsDBNull("FixId"))
            {
                this.fix = (Fix)Registry.CreateInstance(typeof(Fix), dr.GetGuid("FixId"));
            }

            this.fixName = (FixName)dr.GetBusinessObjectBase(Registry, typeof(FixName), "FixNameId");
            this.primaryErrorCode = dr.GetString("PrimaryErrorCode").Trim();
            this.diagnosticReportErrorCodeSystemType = (DiagnosticReportErrorCodeSystemType)dr.GetInt32("DiagnosticReportErrorCodeSystemType");
            this.sortOrder = dr.GetInt32("SortOrder");
            this.name = dr.GetString("Name");
            this.name_es = dr.GetString("Name_es");
            this.name_fr = dr.GetString("Name_fr");
            this.name_zh = dr.GetString("Name_zh");
            this.description = dr.GetString("Description");
            this.description_es = dr.GetString("Description_es");
            this.description_fr = dr.GetString("Description_fr");
            this.description_zh = dr.GetString("Description_zh");
            this.labor = dr.GetDecimal("Labor");
            this.laborRate = dr.GetDecimal("LaborRate");
            this.laborCost = dr.GetDecimal("LaborCost");
            this.laborRateInLocalCurrency = dr.GetNullableDecimal("LaborRateInLocalCurrency");
            this.laborCostInLocalCurrency = dr.GetNullableDecimal("LaborCostInLocalCurrency");
            this.additionalCost = dr.GetDecimal("AdditionalCost");
            this.additionalCostInLocalCurrency = dr.GetNullableDecimal("AdditionalCostInLocalCurrency");
            this.frequencyCount = dr.GetInt32("FrequencyCount");
            this.totalCost = dr.GetDecimal("TotalCost");
            this.totalCostInLocalCurrency = dr.GetNullableDecimal("TotalCostInLocalCurrency");
            this.partsCost = dr.GetDecimal("PartsCost");
            this.partsCostInLocalCurrency = dr.GetNullableDecimal("PartsCostInLocalCurrency");

            this.diagnosticReportIsExactMatch = dr.GetBoolean("DiagnosticReportIsExactMatch");
            this.diagnosticReportSecondaryCodeAssignmentMatches = dr.GetInt32("DiagnosticReportSecondaryCodeAssignmentMatches");
            this.isFromPolkMatch = dr.GetBoolean("IsFromPolkMatch");
            this.isFromVinPowerMatch = dr.GetBoolean("IsFromVinPowerMatch");

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
                transaction = EnsureDatabasePrepared(connection, transaction);

                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    if (IsObjectCreated)
                    {
                        dr.ProcedureName = "DiagnosticReportResultFix_Create";
                        //#FixLogicStep
                        dr.AddInt32("FixLogicStep", this.FixLogicStep);
                    }
                    else
                    {
                        dr.ProcedureName = "DiagnosticReportResultFix_Save";
                    }

                    dr.AddGuid("DiagnosticReportResultFixId", this.Id);
                    dr.AddGuid("DiagnosticReportResultId", this.DiagnosticReportResult.Id);
                    if (Fix != null)
                    {
                        dr.AddGuid("FixId", this.Fix.Id);
                    }

                    dr.AddBusinessObject("FixNameId", this.FixName);
                    dr.AddNVarChar("PrimaryErrorCode", this.PrimaryErrorCode.Trim());
                    dr.AddInt32("DiagnosticReportErrorCodeSystemType", (int)this.DiagnosticReportErrorCodeSystemType);
                    dr.AddInt32("SortOrder", this.SortOrder);
                    dr.AddNVarChar("Name", this.Name);
                    dr.AddNVarChar("Name_es", this.Name_es);
                    dr.AddNVarChar("Name_fr", this.Name_fr);
                    dr.AddNVarChar("Name_zh", this.Name_zh);
                    dr.AddNText("Description", this.Description);
                    dr.AddNVarChar("Description_es", this.Description_es);
                    dr.AddNVarChar("Description_fr", this.Description_fr);
                    dr.AddNVarChar("Description_zh", this.Description_zh);
                    dr.AddDecimal("Labor", Math.Round(this.Labor, 2));
                    dr.AddDecimal("LaborRate", Math.Round(this.LaborRate, 2));
                    dr.AddDecimal("LaborCost", Math.Round(this.LaborCost, 2));
                    if (this.LaborRateInLocalCurrency.HasValue)
                    {
                        dr.AddDecimal("LaborRateInLocalCurrency", Math.Round(this.LaborRateInLocalCurrency.Value, 2));
                    }
                    if (this.LaborCostInLocalCurrency.HasValue)
                    {
                        dr.AddDecimal("LaborCostInLocalCurrency", Math.Round(this.LaborCostInLocalCurrency.Value, 2));
                    }
                    dr.AddDecimal("AdditionalCost", Math.Round(this.AdditionalCost, 2));
                    if (this.AdditionalCostInLocalCurrency.HasValue)
                    {
                        dr.AddDecimal("AdditionalCostInLocalCurrency", Math.Round(this.AdditionalCostInLocalCurrency.Value, 2));
                    }
                    dr.AddInt32("FrequencyCount", this.FrequencyCount);
                    dr.AddDecimal("TotalCost", Math.Round(this.TotalCost, 2));
                    if (this.TotalCostInLocalCurrency.HasValue)
                    {
                        dr.AddDecimal("TotalCostInLocalCurrency", Math.Round(this.TotalCostInLocalCurrency.Value, 2));
                    }
                    dr.AddDecimal("PartsCost", Math.Round(this.PartsCost, 2));
                    if (this.PartsCostInLocalCurrency.HasValue)
                    {
                        dr.AddDecimal("PartsCostInLocalCurrency", Math.Round(this.PartsCostInLocalCurrency.Value, 2));
                    }

                    dr.AddBoolean("DiagnosticReportIsExactMatch", this.DiagnosticReportIsExactMatch);
                    dr.AddInt32("DiagnosticReportSecondaryCodeAssignmentMatches", this.DiagnosticReportSecondaryCodeAssignmentMatches);
                    dr.AddBoolean("IsFromPolkMatch", this.IsFromPolkMatch);
                    dr.AddBoolean("IsFromVinPowerMatch", this.IsFromVinPowerMatch);

                    dr.Execute(transaction);
                }

                this.IsObjectDirty = false;
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
            if (diagnosticReportResultFixParts != null)
            {
                for (int i = 0; i < DiagnosticReportResultFixParts.Removed.Count; i++)
                {
                    transaction = ((DiagnosticReportResultFixPart)DiagnosticReportResultFixParts.Removed[i]).Delete(connection, transaction);
                }
                for (int i = 0; i < DiagnosticReportResultFixParts.Count; i++)
                {
                    transaction = DiagnosticReportResultFixParts[i].Save(connection, transaction);
                }
            }

            //ToolDB_
            if (diagnosticReportResultFixTools != null)
            {
                for (int i = 0; i < DiagnosticReportResultFixTools.Removed.Count; i++)
                {
                    transaction = ((DiagnosticReportResultFixTool)DiagnosticReportResultFixTools.Removed[i]).Delete(connection, transaction);
                }
                for (int i = 0; i < DiagnosticReportResultFixTools.Count; i++)
                {
                    transaction = DiagnosticReportResultFixTools[i].Save(connection, transaction);
                }
            }
            //ToolDB_

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

            foreach (DiagnosticReportResultFixPart part in DiagnosticReportResultFixParts)
            {
                transaction = part.Delete(connection, transaction);
            }

            //ToolDB_
            foreach (DiagnosticReportResultFixTool tool in DiagnosticReportResultFixTools)
            {
                transaction = tool.Delete(connection, transaction);
            }
            //ToolDB_

            // delete this current object
            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                //delete the diagnosticReportResultFix
                dr.ProcedureName = "DiagnosticReportResultFix_Delete";
                dr.AddGuid("DiagnosticReportResultFixId", Id);

                dr.CommandTimeout = 0; //Set the timeout to 0 second.
                dr.ExecuteNonQuery(transaction);
            }

            transaction = base.Delete(connection, transaction);

            return transaction;
        }

        #endregion Required Object Methods (Load, Save, Delete, Etc)
    }
}
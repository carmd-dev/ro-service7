using Innova.Articles;
using Innova.DiagnosticReports;
using Innova.ScheduleMaintenance;
using Innova.Users;
using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using Metafuse3.NullableTypes;
using System;
using System.Collections.Specialized;
using System.Data.SqlClient;

namespace Innova.Fixes
{
    /// <summary>
    /// The FixName object handles the business logic and data access for the specialized business object, FixName.
    /// The class inherits from <see cref="BusinessObjectBase"/> class.
    /// </summary>
    /// <example>
    /// See the examples of how to construct the FixName object.
    ///
    /// To create a new instance of a new of FixName.
    /// <code>FixName o = (FixName)Registry.CreateInstance(typeof(FixName));</code>
    ///
    /// To create an new instance of an existing FixName.
    /// <code>FixName o = (FixName)Registry.CreateInstance(typeof(FixName), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of FixName, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectBase.ConnectionString"/> to the <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable()]
    [ClassDefinition("Fix Name", "Fix Names", "Fix Name", "FixNameId")]
    public class FixName : InnovaBusinessObjectBase
    {
        // data object variables
        private Guid fixNameIdChiltonComponentLocator;

        private Guid fixNameIdChiltonWiringDiagrams;

        private FixRating fixRating = FixRating.Unrated;

        private string description;
        private string description_es = "";
        private string description_fr = "";
        private string description_zh = "";

        //#IA176
        private string proNameDescription;

        private string proNameDescription_es = "";
        private string proNameDescription_fr = "";
        private string proNameDescription_zh = "";

        private bool isActive;

        private bool enableForSchedMaint = false;
        private bool enableForUnSchedMaint = false;
        private bool showParts = false;

        private NullableInt32 defaultUnschedMaintMileage = NullableInt32.Null;
        private NullableInt32 defaultUnschedMaintMileageRepeat = NullableInt32.Null;

        private AdminUser createdByAdminUser;
        private AdminUser updatedByAdminUser;
        private DateTime createdDateTimeUTC;
        private DateTime updatedDateTimeUTC;

        private ScheduleMaintenanceServiceCollection scheduleMaintenanceServices = null;

        #region System Constructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new (one that does not exist, assigning a new <see cref="Guid"/> Id). FixName object.
        /// In order to create a new FixName which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <example>
        /// <code>
        /// FixName o = (FixName)Registry.CreateInstance(typeof(FixName));
        /// </code>
        /// </example>
        protected internal FixName() : base()
        {
            IsObjectCreated = true;
            IsObjectLoaded = true;
            IsObjectDirty = true;

            this.CreatedDateTimeUTC = DateTime.UtcNow;
            this.UpdatedDateTimeUTC = this.CreatedDateTimeUTC;
        }

        /// <summary>
        /// Constructor creates an existing (one that exists, and already has a <see cref="Guid"/> Id).  FixName object.
        /// In order to create an existing FixName object, which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <param name="id">The <see cref="Guid"/> or SQL uniqueidentifier indentification "Id" of the current object.</param>
        /// <example>
        /// <code>
        /// FixName o = (FixName)Registry.CreateInstance(typeof(FixName), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));
        /// </code>
        /// </example>
        protected internal FixName(Guid id) : base(id)
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

        private StringCollection updatedFields = null;

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

        #region Object Properties

        /**************************************************************************************
		 *
		 * Object Properties: Add Custom Fields
		 *
		 * **************************************************************************************/

        //#TotalFixes
        public int TotalFixes { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Guid"/> that is used for mapping the fix name to a Chilton component locator.
        /// </summary>
        public Guid FixNameIdChiltonComponentLocator
        {
            get
            {
                this.EnsureLoaded();
                return this.fixNameIdChiltonComponentLocator;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Guid"/> that is used for mapping the fix name to a Chilton wiring diagrams.
        /// </summary>
        public Guid FixNameIdChiltonWiringDiagrams
        {
            get
            {
                this.EnsureLoaded();
                return this.fixNameIdChiltonWiringDiagrams;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="FixRating"/> rating of the fix
        /// </summary>
        [PropertyDefinition("Fix Rating", "Rating to give to fix service.")]
        public FixRating FixRating
        {
            get
            {
                this.EnsureLoaded();
                return this.fixRating;
            }
            set
            {
                this.EnsureLoaded();
                if (this.fixRating != value)
                {
                    this.IsObjectDirty = true;
                    this.fixRating = value;
                    this.UpdatedField("FixRating");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> description of the fix name (or the actual name).
        /// </summary>
        [PropertyDefinition("Description", "Description of the fix")]
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
                    this.UpdatedField("Description");
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> description of the fix name (or the actual name) in lowercase. Used for FindBy.. lookup in collections.
        /// </summary>
        public string DescriptionLowerCase
        {
            get
            {
                return this.Description.ToLower().Trim();
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> description in Spanish
        /// </summary>
        [PropertyDefinition("Description - Spanish", "Description of the fix in Spanish")]
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
                    this.UpdatedField("Description_es");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> description in French
        /// </summary>
        [PropertyDefinition("Description - French", "Description of the fix in French")]
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
                    this.UpdatedField("Description_fr");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> description in Mandarin
        /// </summary>
        [PropertyDefinition("Description - Mandarin", "Description of the fix in Mandarin")]
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
                    this.UpdatedField("description_zh");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> description in the language specified in the <see cref="RuntimeInfo"/>.
        /// </summary>
        [PropertyDefinition("Description", "Description of the fix")]
        public string Description_Translated
        {
            get
            {
                this.EnsureLoaded();
                return this.RuntimeInfo.GetTranslatedValue(this.Description, this.Description_es, this.Description_fr, this.Description_zh);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/> flag indicating whether or not the fix name is active or not.
        /// </summary>
        [PropertyDefinition("Active", "Is this Fix Name currently active.")]
        public bool IsActive
        {
            get
            {
                this.EnsureLoaded();
                return this.isActive;
            }
            set
            {
                this.EnsureLoaded();
                if (this.isActive != value)
                {
                    this.IsObjectDirty = true;
                    this.isActive = value;
                    this.UpdatedField("IsActive");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/> flag indicating whether or not the fix name is active or not.
        /// </summary>
        [PropertyDefinition("Sch Maint?", "Flag indicating the name can be used for scheduled maintenance plans")]
        public bool EnableForSchedMaint
        {
            get
            {
                this.EnsureLoaded();
                return this.enableForSchedMaint;
            }
            set
            {
                this.EnsureLoaded();
                if (this.enableForSchedMaint != value)
                {
                    this.IsObjectDirty = true;
                    this.enableForSchedMaint = value;
                    this.UpdatedField("EnableForSchedMaint");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/> flag indicating whether or not the fix name is active or not.
        /// </summary>
        [PropertyDefinition("UnSch Maint?", "Flag indicating the name can be used for scheduled maintenance plans")]
        public bool EnableForUnSchedMaint
        {
            get
            {
                this.EnsureLoaded();
                return this.enableForUnSchedMaint;
            }
            set
            {
                this.EnsureLoaded();
                if (this.enableForUnSchedMaint != value)
                {
                    this.IsObjectDirty = true;
                    this.enableForUnSchedMaint = value;
                    this.UpdatedField("EnableForUnSchedMaint");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/> flag indicating whether or not the fix name is active or not.
        /// </summary>
        [PropertyDefinition("Show Parts?", "Flag indicating the name can be used for displaying parts")]
        public bool ShowParts
        {
            get
            {
                this.EnsureLoaded();
                return this.showParts;
            }
            set
            {
                this.EnsureLoaded();
                if (this.showParts != value)
                {
                    this.IsObjectDirty = true;
                    this.showParts = value;
                    this.UpdatedField("ShowParts");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableInt32"/> default unscheduled maintenance mileage start.
        /// </summary>
        [PropertyDefinition("Def. Unsched Mileage", "The default unscheduled maintenance mileage start.")]
        public NullableInt32 DefaultUnschedMaintMileage
        {
            get
            {
                this.EnsureLoaded();
                return this.defaultUnschedMaintMileage;
            }
            set
            {
                this.EnsureLoaded();
                if (this.defaultUnschedMaintMileage != value)
                {
                    this.IsObjectDirty = true;
                    this.defaultUnschedMaintMileage = value;
                    this.UpdatedField("DefaultUnschedMaintMileage");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableInt32"/> default unscheduled maintenance mileage repeat.
        /// </summary>
        [PropertyDefinition("Def. Unsched Repeat", "The default unscheduled maintenance mileage repeat.")]
        public NullableInt32 DefaultUnschedMaintMileageRepeat
        {
            get
            {
                this.EnsureLoaded();
                return this.defaultUnschedMaintMileageRepeat;
            }
            set
            {
                this.EnsureLoaded();
                if (this.defaultUnschedMaintMileageRepeat != value)
                {
                    this.IsObjectDirty = true;
                    this.defaultUnschedMaintMileageRepeat = value;
                    this.UpdatedField("DefaultUnschedMaintMileageRepeat");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="AdminUser"/> who created the record.
        /// </summary>
        [PropertyDefinition("Created By", "Administrator who created Fix Name")]
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
                    this.UpdatedField("CreatedByAdminUserId");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="AdminUser"/> who last updated the record.
        /// </summary>
        [PropertyDefinition("Updated By", "Administrator who last updated this fix name.")]
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
                    this.UpdatedField("UpdatedByAdminUserId");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/> the record was created on in universal time
        /// </summary>
        [PropertyDefinition("Created", "Date and Time this fix name was created.")]
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
                    this.UpdatedField("CreatedDateTimeUTC");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/> the record was last updated on in universal time
        /// </summary>
        [PropertyDefinition("Updated", "The last time this fix name was updated.")]
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
                    this.UpdatedField("UpdatedDateTimeUTC");
                }
            }
        }

        /// <summary>
        /// Gets the total number of languages 1, for just english, and another for each additional description field added.
        /// </summary>
        [PropertyDefinition("Language Count", "Number of languages available to be viewed in for this fix name.")]
        public int LanguageCount
        {
            get
            {
                return 1
                    + (!String.IsNullOrEmpty(this.description_es) ? 1 : 0)
                    + (!String.IsNullOrEmpty(this.description_fr) ? 1 : 0)
                    + (!String.IsNullOrEmpty(this.description_zh) ? 1 : 0);
            }
        }

        //#IA176
        /// <summary>
        /// Gets or sets the <see cref="string"/> pro name description of the fix name (or the actual name).
        /// </summary>
        [PropertyDefinition("ProName Description", "ProName Description of the fix")]
        public string ProNameDescription
        {
            get
            {
                this.EnsureLoaded();
                return this.proNameDescription;
            }
            set
            {
                this.EnsureLoaded();
                if (this.proNameDescription != value)
                {
                    this.IsObjectDirty = true;
                    this.proNameDescription = value;
                    this.UpdatedField("ProNameDescription");
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/>pro name description of the fix name (or the actual name) in lowercase. Used for FindBy.. lookup in collections.
        /// </summary>
        public string ProNameDescriptionLowerCase => this.ProNameDescription.ToLower().Trim();

        /// <summary>
        /// Gets or sets the <see cref="string"/> description in Spanish
        /// </summary>
        [PropertyDefinition("ProName Description - Spanish", "ProName Description of the fix in Spanish")]
        public string ProNameDescription_es
        {
            get
            {
                this.EnsureLoaded();
                return this.proNameDescription_es;
            }
            set
            {
                this.EnsureLoaded();
                if (this.proNameDescription_es != value)
                {
                    this.IsObjectDirty = true;
                    this.proNameDescription_es = value;
                    this.UpdatedField("ProNameDescription_es");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> description in French
        /// </summary>
        [PropertyDefinition("ProName Description - French", "ProName Description of the fix in French")]
        public string ProNameDescription_fr
        {
            get
            {
                this.EnsureLoaded();
                return this.proNameDescription_fr;
            }
            set
            {
                this.EnsureLoaded();
                if (this.proNameDescription_fr != value)
                {
                    this.IsObjectDirty = true;
                    this.proNameDescription_fr = value;
                    this.UpdatedField("ProNameDescription_fr");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> description in Mandarin
        /// </summary>
        [PropertyDefinition("ProName Description - Mandarin", "ProName Description of the fix in Mandarin")]
        public string ProNameDescription_zh
        {
            get
            {
                this.EnsureLoaded();
                return this.proNameDescription_zh;
            }
            set
            {
                this.EnsureLoaded();
                if (this.proNameDescription_zh != value)
                {
                    this.IsObjectDirty = true;
                    this.proNameDescription_zh = value;
                    this.UpdatedField("ProNameDescription_zh");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> proName description in the language specified in the <see cref="RuntimeInfo"/>.
        /// </summary>
        [PropertyDefinition("ProName Description", "ProName Description of the fix")]
        public string ProNameDescription_Translated
        {
            get
            {
                this.EnsureLoaded();
                return this.RuntimeInfo.GetTranslatedValue(this.ProNameDescription, this.ProNameDescription_es,
                    this.ProNameDescription_fr, this.ProNameDescription_zh);
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
        /// Gets or sets the <see cref="Fix"/> that belongs to this plan detail for the specific vehicle.   This is populated by the plan details collection.
        /// </summary>
        public Fix FixService { get; protected internal set; }

        /// <summary>
        /// Gets the <see cref="ScheduleMaintenanceServiceCollection"/> of <see cref="ScheduleMaintenanceService"/> objects which are associated to this fix name in the schedule maintenance service records.
        /// </summary>
        public ScheduleMaintenanceServiceCollection ScheduleMaintenanceServices
        {
            get
            {
                if (this.scheduleMaintenanceServices == null)
                {
                    this.scheduleMaintenanceServices = new ScheduleMaintenanceServiceCollection(this.Registry);

                    SqlProcedureCommand call = new SqlProcedureCommand();
                    call.ProcedureName = "ScheduleMaintenanceService_LoadByFixName";
                    call.AddGuid("FixNameId", this.Id);

                    this.scheduleMaintenanceServices.Load(call, "ScheduleMaintenanceServiceId", true, true);
                }
                return this.scheduleMaintenanceServices;
            }
        }

        /// <summary>
        /// Gets the <see cref="ScheduleMaintenanceServiceCollection"/> of <see cref="ScheduleMaintenanceService"/> objects which are associated to this fix name in the schedule maintenance service records and are currently active.
        /// </summary>
        public ScheduleMaintenanceServiceCollection ScheduleMaintenanceServicesActive
        {
            get
            {
                ScheduleMaintenanceServiceCollection servicesActive = new ScheduleMaintenanceServiceCollection(this.Registry);

                foreach (ScheduleMaintenanceService sms in this.ScheduleMaintenanceServices)
                {
                    if (sms.IsActive)
                    {
                        servicesActive.Add(sms);
                    }
                }

                return servicesActive;
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
        /// Gets a <see cref="ArticleCollection"/> of <see cref="Article"/> objects related to this <see cref="FixName"/>.
        /// </summary>
        /// <returns></returns>
        public ArticleCollection GetRelatedArticles()
        {
            ArticleCollection articles = new ArticleCollection(this.Registry);

            SqlProcedureCommand call = new SqlProcedureCommand();
            call.ProcedureName = "Article_LoadByFixName";
            call.AddGuid("FixNameId", this.Id);

            articles.Load(call, "ArticleId", true, true, false);

            return articles;
        }

        public int GetFreezeFrameMatchCount(DiagnosticReport diagnosticReport)
        {
            int count = 0;

            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(this.Registry.ConnectionStringDefault))
            {
                dr.ProcedureName = "FixName_GetFreezeFrameMatchCount";
                dr.AddGuid("FixNameId", this.Id);
                dr.AddGuid("DiagnosticReportId", diagnosticReport.Id);

                dr.Execute();

                if (dr.Read())
                {
                    count = dr.GetInt32("FreezeFrameMatchCount");
                }
            }

            return count;
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
            dr.ProcedureName = "FixName_Load";

            dr.AddGuid("FixNameId", Id);
        }

        /// <summary>
        /// Sets the properties of this object from the fields in the recordset located in the supplied <see cref="SqlDataReaderWrapper"/>.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> containing the recordset and fields used to set the properties of this object (this layer only).</param>
        protected new void SetPropertiesFromDataReader(SqlDataReaderWrapper dr)
        {
            this.fixNameIdChiltonComponentLocator = dr.GetGuid("FixNameIdChiltonComponentLocator");
            this.fixNameIdChiltonWiringDiagrams = dr.GetGuid("FixNameIdChiltonWiringDiagrams");

            this.fixRating = (FixRating)dr.GetInt32("FixRating");
            this.description = dr.GetString("Description");
            this.description_es = dr.GetString("Description_es");
            this.description_fr = dr.GetString("Description_fr");
            this.description_zh = dr.GetString("Description_zh");
            //#IA176
            this.proNameDescription = dr.GetString("ProNameDescription");
            this.proNameDescription_es = dr.GetString("ProNameDescription_es");
            this.proNameDescription_fr = dr.GetString("ProNameDescription_fr");
            this.proNameDescription_zh = dr.GetString("ProNameDescription_zh");

            this.isActive = dr.GetBoolean("IsActive");
            this.showParts = dr.GetBoolean("ShowParts");
            this.enableForSchedMaint = dr.GetBoolean("EnableForSchedMaint");
            this.enableForUnSchedMaint = dr.GetBoolean("EnableForUnSchedMaint");

            this.defaultUnschedMaintMileage = dr.GetNullableInt32("DefaultUnschedMaintMileage");
            this.defaultUnschedMaintMileageRepeat = dr.GetNullableInt32("DefaultUnschedMaintMileageRepeat");

            this.createdByAdminUser = (AdminUser)Registry.CreateInstance(typeof(AdminUser), dr.GetGuid("CreatedByAdminUserId"));
            this.updatedByAdminUser = (AdminUser)Registry.CreateInstance(typeof(AdminUser), dr.GetGuid("UpdatedByAdminUserId"));
            this.createdDateTimeUTC = dr.GetDateTime("CreatedDateTimeUTC");
            this.updatedDateTimeUTC = dr.GetDateTime("UpdatedDateTimeUTC");

            //#TotalFixes
            if (dr.ColumnExists("TotalFixes"))
            {
                this.TotalFixes = dr.GetInt32("TotalFixes");
            }

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
                    if (IsObjectCreated)
                    {
                        dr.ProcedureName = "FixName_Create";
                    }
                    else
                    {
                        dr.UpdateFields("FixName", "FixNameId", this.updatedFields);
                        this.UpdatedDateTimeUTC = DateTime.UtcNow;
                    }

                    dr.AddGuid("FixNameId", this.Id);
                    dr.AddInt32("FixRating", (int)this.FixRating);
                    //##Security
                    //Need hash and encrypt Description before saving in DB
                    //##Security
                    dr.AddNVarChar("Description", this.Description);
                    dr.AddNVarChar("Description_es", this.Description_es);
                    dr.AddNVarChar("Description_fr", this.Description_fr);
                    dr.AddNVarChar("Description_zh", this.Description_zh);
                    //#IA176
                    dr.AddNVarChar("ProNameDescription", this.ProNameDescription);
                    dr.AddNVarChar("ProNameDescription_es", this.ProNameDescription_es);
                    dr.AddNVarChar("ProNameDescription_fr", this.ProNameDescription_fr);
                    dr.AddNVarChar("ProNameDescription_zh", this.ProNameDescription_zh);

                    dr.AddBoolean("IsActive", this.IsActive);
                    dr.AddBoolean("EnableForSchedMaint", this.EnableForSchedMaint);
                    dr.AddBoolean("EnableForUnSchedMaint", this.EnableForUnSchedMaint);
                    dr.AddBoolean("ShowParts", this.ShowParts);
                    dr.AddInt32("DefaultUnschedMaintMileage", this.DefaultUnschedMaintMileage);
                    dr.AddInt32("DefaultUnschedMaintMileageRepeat", this.DefaultUnschedMaintMileageRepeat);

                    dr.AddGuid("CreatedByAdminUserId", this.CreatedByAdminUser.Id);
                    dr.AddGuid("UpdatedByAdminUserId", this.UpdatedByAdminUser.Id);
                    dr.AddDateTime("CreatedDateTimeUTC", this.CreatedDateTimeUTC);
                    dr.AddDateTime("UpdatedDateTimeUTC", this.UpdatedDateTimeUTC);

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
            if (this.scheduleMaintenanceServices != null)
            {
                foreach (ScheduleMaintenanceService s in this.scheduleMaintenanceServices.Removed)
                {
                    transaction = s.Delete(connection, transaction);
                }
                foreach (ScheduleMaintenanceService s in this.scheduleMaintenanceServices)
                {
                    transaction = s.Save(connection, transaction);
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

            //Copy deleted FixName to Audit DB first
            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                dr.ProcedureName = "Audit_CopyFixName";
                dr.AddGuid("FixNameId", Id);
                dr.ExecuteNonQuery(transaction);
            }

            //Copy deleted FixName to Audit DB first

            // delete this current object
            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                //delete the item
                dr.ProcedureName = "FixName_Delete";
                dr.AddGuid("FixNameId", Id);

                dr.ExecuteNonQuery(transaction);
            }

            transaction = base.Delete(connection, transaction);

            return transaction;
        }

        #endregion Required Object Methods (Load, Save, Delete, Etc)
    }
}
using Innova.Fixes;
using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using System;
using System.Collections.Specialized;
using System.Data.SqlClient;

namespace Innova.Symptoms
{
    /// <summary>
    /// The Symptom object handles the business logic and data access for the specialized business object, Symptom.
    /// The class inherits from <see cref="BusinessObjectBase"/> class.
    /// </summary>
    /// <example>
    /// See the examples of how to construct the Symptom object.
    ///
    /// To create a new instance of a new of Symptom.
    /// <code>Symptom o = (Symptom)this.Registry.CreateInstance(typeof(Symptom));</code>
    ///
    /// To create an new instance of an existing Symptom.
    /// <code>Symptom o = (Symptom)this.Registry.CreateInstance(typeof(Symptom), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of Symptom, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectBase.ConnectionString"/> to the <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable()]
    public class Symptom : InnovaBusinessObjectBase
    {
        private SymptomFragment symptomFragmentType;
        private SymptomFragment symptomFragmentObservedEvent;
        private SymptomFragment symptomFragmentLocation;
        private SymptomFragment symptomFragmentOperationalCondition;
        private SymptomFragment symptomFragmentSurveyTechnicalInspection;
        private SymptomFragment symptomFragmentFixAssistDescription;
        private FixName fixName;
        private DateTime createdDateTimeUTC = DateTime.MinValue;
        private DateTime updatedDateTimeUTC = DateTime.MinValue;

        protected internal SmartCollection symptomMakes = null;
        protected internal SmartCollection symptomModels = null;
        protected internal SmartCollection symptomYears = null;
        protected internal SmartCollection symptomEngineTypes = null;

        #region Contructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new (one that does not exist, assigning a new <see cref="Guid"/> Id). Symptom object.
        /// In order to create a new Symptom which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <example>
        /// <code>
        /// Symptom o = (Symptom)Registry.CreateInstance(typeof(Symptom));
        /// </code>
        /// </example>
        protected internal Symptom()
            : base()
        {
            this.IsObjectCreated = true;
            this.IsObjectLoaded = true;
            this.IsObjectDirty = true;
        }

        /// <summary>
        /// Constructor creates an existing (one that exists, and already has a <see cref="Guid"/> Id).  Symptom object.
        /// In order to create an existing Symptom object, which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <param name="id">The <see cref="Guid"/> or SQL uniqueidentifier indentification "Id" of the current object.</param>
        /// <example>
        /// <code>
        /// Symptom o = (Symptom)Registry.CreateInstance(typeof(Symptom), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));
        /// </code>
        /// </example>
        protected internal Symptom(Guid id)
            : base(id)
        {
            this.id = id;
        }

        //Jacobus TEST!!!!!!
        public Symptom(Guid id, bool test) : base(id)
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
            get
            {
                return this.isObjectLoaded;
            }
            set
            {
                this.isObjectLoaded = value;
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
                return this.isObjectDirty;
            }
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
            get
            {
                return this.isObjectCreated;
            }
            set
            {
                this.isObjectCreated = value;
            }
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
        /// Gets or sets the <see cref="SymptomFragment"/> type of the symtom fragment.
        /// </summary>
        [PropertyDefinition("Symptom Type", "The type of the symtom fragment.")]
        public SymptomFragment SymptomFragmentType
        {
            get
            {
                this.EnsureLoaded();
                return this.symptomFragmentType;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.symptomFragmentType)
                {
                    this.IsObjectDirty = true;
                    this.symptomFragmentType = value;
                    this.UpdatedField("SymptomFragmentIdType");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="SymptomFragment"/> event that was observed.
        /// </summary>
        [PropertyDefinition("Observed Event", "The event that was observed.")]
        public SymptomFragment SymptomFragmentObservedEvent
        {
            get
            {
                this.EnsureLoaded();
                return this.symptomFragmentObservedEvent;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.symptomFragmentObservedEvent)
                {
                    this.IsObjectDirty = true;
                    this.symptomFragmentObservedEvent = value;
                    this.UpdatedField("SymptomFragmentIdObservedEvent");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="SymptomFragment"/> location on the vechile.
        /// </summary>
        [PropertyDefinition("Location", "The location on the vechile.")]
        public SymptomFragment SymptomFragmentLocation
        {
            get
            {
                this.EnsureLoaded();
                return this.symptomFragmentLocation;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.symptomFragmentLocation)
                {
                    this.IsObjectDirty = true;
                    this.symptomFragmentLocation = value;
                    this.UpdatedField("SymptomFragmentIdLocation");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="SymptomFragment"/> operational condition of the vehicle when the symptom presents itself.
        /// </summary>
        [PropertyDefinition("Operational Condition", "The operational condition of the vehicle when the symptom presents itself.")]
        public SymptomFragment SymptomFragmentOperationalCondition
        {
            get
            {
                this.EnsureLoaded();
                return this.symptomFragmentOperationalCondition;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.symptomFragmentOperationalCondition)
                {
                    this.IsObjectDirty = true;
                    this.symptomFragmentOperationalCondition = value;
                    this.UpdatedField("SymptomFragmentIdOperationalCondition");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="SymptomFragment"/> survey/technical inspection that should be done on the vehicle when the symptom presents itself.
        /// </summary>
        [PropertyDefinition("Survey/Technical Inspection", "The survey/technical inspection that should be done on the vehicle when the symptom presents itself.")]
        public SymptomFragment SymptomFragmentSurveyTechnicalInspection
        {
            get
            {
                this.EnsureLoaded();
                return this.symptomFragmentSurveyTechnicalInspection;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.symptomFragmentSurveyTechnicalInspection)
                {
                    this.IsObjectDirty = true;
                    this.symptomFragmentSurveyTechnicalInspection = value;
                    this.UpdatedField("SymptomFragmentIdSurveyTechnicalInspection");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="SymptomFragment"/> fix assist description for the vehicle when the symptom presents itself.
        /// </summary>
        [PropertyDefinition("Fix Assist Description", "The fix assist description for the vehicle when the symptom presents itself.")]
        public SymptomFragment SymptomFragmentFixAssistDescription
        {
            get
            {
                this.EnsureLoaded();
                return this.symptomFragmentFixAssistDescription;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.symptomFragmentFixAssistDescription)
                {
                    this.IsObjectDirty = true;
                    this.symptomFragmentFixAssistDescription = value;
                    this.UpdatedField("SymptomFragmentIdFixAssistDescription");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="FixName"/> assigned to this Symptom.
        /// </summary>
        [PropertyDefinition("Fix Name", "The Fix Name assigned to this Symptom.")]
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
                if (value != this.fixName)
                {
                    this.IsObjectDirty = true;
                    this.fixName = value;
                    this.UpdatedField("FixNameId");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/> this symtom was created.
        /// </summary>
        [PropertyDefinition("Created", "Date and Time this symtom was created.")]
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
        /// Gets or sets the <see cref="DateTime"/> this symtom was last updated.
        /// </summary>
        [PropertyDefinition("Updated", "The last time this symtom was updated.")]
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

        /// <summary>
		///
		/// </summary>
		[PropertyDefinition("Makes", "Makes that share this symptom.")]
        public SmartCollection SymptomMakes
        {
            get
            {
                if (this.symptomMakes == null)
                {
                    this.symptomMakes = new SmartCollection();

                    if (!this.IsObjectCreated)
                    {
                        using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(this.Registry.ConnectionStringDefault))
                        {
                            dr.ProcedureName = "SymptomMake_LoadBySymptom";
                            dr.AddGuid("SymptomId", this.Id);

                            dr.Execute();

                            while (dr.Read())
                            {
                                this.symptomMakes.Add(new SymptomMake(this, dr.GetString("Make")));
                            }
                        }
                    }
                }
                return this.symptomMakes;
            }
        }

        /// <summary>
        ///
        /// </summary>
        [PropertyDefinition("Models", "Models that share this symptom.")]
        public SmartCollection SymptomModels
        {
            get
            {
                if (this.symptomModels == null)
                {
                    this.symptomModels = new SmartCollection();

                    if (!this.IsObjectCreated)
                    {
                        using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(this.Registry.ConnectionStringDefault))
                        {
                            dr.ProcedureName = "SymptomModel_LoadBySymptom";
                            dr.AddGuid("SymptomId", this.Id);

                            dr.Execute();

                            while (dr.Read())
                            {
                                this.symptomModels.Add(new SymptomModel(this, dr.GetString("Model")));
                            }
                        }
                    }
                }
                return this.symptomModels;
            }
        }

        /// <summary>
        ///
        /// </summary>
        [PropertyDefinition("Years", "Years that share this symptom.")]
        public SmartCollection SymptomYears
        {
            get
            {
                if (this.symptomYears == null)
                {
                    this.symptomYears = new SmartCollection();

                    if (!this.IsObjectCreated)
                    {
                        using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(this.Registry.ConnectionStringDefault))
                        {
                            dr.ProcedureName = "SymptomYear_LoadBySymptom";
                            dr.AddGuid("SymptomId", this.Id);

                            dr.Execute();

                            while (dr.Read())
                            {
                                this.symptomYears.Add(new SymptomYear(this, dr.GetInt32("Year")));
                            }
                        }
                    }
                }
                return this.symptomYears;
            }
        }

        /// <summary>
        ///
        /// </summary>
        [PropertyDefinition("EngineTypes", "Engine types that share this symptom.")]
        public SmartCollection SymptomEngineTypes
        {
            get
            {
                if (this.symptomEngineTypes == null)
                {
                    this.symptomEngineTypes = new SmartCollection();

                    if (!this.IsObjectCreated)
                    {
                        using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(this.Registry.ConnectionStringDefault))
                        {
                            dr.ProcedureName = "SymptomEngineType_LoadBySymptom";
                            dr.AddGuid("SymptomId", this.Id);

                            dr.Execute();

                            while (dr.Read())
                            {
                                this.symptomEngineTypes.Add(new SymptomEngineType(this, dr.GetString("EngineType")));
                            }
                        }
                    }
                }
                return this.symptomEngineTypes;
            }
        }

        #endregion Public Properties

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
            dr.ProcedureName = "Symptom_Load";
            dr.AddGuid("SymptomId", this.Id);
        }

        /// <summary>
        /// Sets the properties of this object from the fields in the recordset located in the supplied <see cref="SqlDataReaderWrapper"/>.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> containing the recordset and fields used to set the properties of this object (this layer only).</param>
        protected new void SetPropertiesFromDataReader(SqlDataReaderWrapper dr)
        {
            this.symptomFragmentType = (SymptomFragment)dr.GetBusinessObjectBase(this.Registry, typeof(SymptomFragment), "SymptomFragmentIdType");
            this.symptomFragmentObservedEvent = (SymptomFragment)dr.GetBusinessObjectBase(this.Registry, typeof(SymptomFragment), "SymptomFragmentIdObservedEvent");
            this.symptomFragmentLocation = (SymptomFragment)dr.GetBusinessObjectBase(this.Registry, typeof(SymptomFragment), "SymptomFragmentIdLocation");
            this.symptomFragmentOperationalCondition = (SymptomFragment)dr.GetBusinessObjectBase(this.Registry, typeof(SymptomFragment), "SymptomFragmentIdOperationalCondition");
            this.symptomFragmentSurveyTechnicalInspection = (SymptomFragment)dr.GetBusinessObjectBase(this.Registry, typeof(SymptomFragment), "SymptomFragmentIdSurveyTechnicalInspection");
            this.symptomFragmentFixAssistDescription = (SymptomFragment)dr.GetBusinessObjectBase(this.Registry, typeof(SymptomFragment), "SymptomFragmentIdFixAssistDescription");
            this.fixName = (FixName)dr.GetBusinessObjectBase(this.Registry, typeof(FixName), "FixNameId");
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
                        dr.ProcedureName = "Symptom_Create";
                        this.CreatedDateTimeUTC = DateTime.UtcNow;
                        this.UpdatedDateTimeUTC = this.CreatedDateTimeUTC;
                    }
                    else
                    {
                        dr.UpdateFields("Symptom", "SymptomId", this.updatedFields);
                    }

                    dr.AddGuid("SymptomId", this.Id);
                    dr.AddBusinessObject("SymptomFragmentIdType", this.SymptomFragmentType);
                    dr.AddBusinessObject("SymptomFragmentIdObservedEvent", this.SymptomFragmentObservedEvent);
                    dr.AddBusinessObject("SymptomFragmentIdLocation", this.SymptomFragmentLocation);
                    dr.AddBusinessObject("SymptomFragmentIdOperationalCondition", this.SymptomFragmentOperationalCondition);
                    dr.AddBusinessObject("SymptomFragmentIdSurveyTechnicalInspection", this.SymptomFragmentSurveyTechnicalInspection);
                    dr.AddBusinessObject("SymptomFragmentIdFixAssistDescription", this.SymptomFragmentFixAssistDescription);
                    dr.AddBusinessObject("FixNameId", this.FixName);
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
            if (this.symptomMakes != null)
            {
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    dr.ProcedureName = "SymptomMake_DeleteBySymptom";
                    dr.AddGuid("SymptomId", this.Id);
                    dr.ExecuteNonQuery(transaction);

                    dr.ClearParameters();

                    foreach (SymptomMake symptomMake in this.SymptomMakes)
                    {
                        dr.ProcedureName = "SymptomMake_Create";
                        dr.AddGuid("SymptomId", this.Id);
                        dr.AddNVarChar("Make", symptomMake.Make);

                        dr.ExecuteNonQuery(transaction);
                        dr.ClearParameters();
                    }
                }
            }

            if (this.SymptomYears != null)
            {
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    dr.ProcedureName = "SymptomYear_DeleteBySymptom";
                    dr.AddGuid("SymptomId", this.Id);
                    dr.ExecuteNonQuery(transaction);

                    dr.ClearParameters();

                    foreach (SymptomYear symptomYear in this.SymptomYears)
                    {
                        dr.ProcedureName = "SymptomYear_Create";
                        dr.AddGuid("SymptomId", this.Id);
                        dr.AddInt32("Year", symptomYear.Year);

                        dr.ExecuteNonQuery(transaction);
                        dr.ClearParameters();
                    }
                }
            }

            if (this.SymptomModels != null)
            {
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    dr.ProcedureName = "SymptomModel_DeleteBySymptom";
                    dr.AddGuid("SymptomId", this.Id);
                    dr.ExecuteNonQuery(transaction);

                    dr.ClearParameters();

                    foreach (SymptomModel symptomModel in this.SymptomModels)
                    {
                        dr.ProcedureName = "SymptomModel_Create";
                        dr.AddGuid("SymptomId", this.Id);
                        dr.AddNVarChar("Model", symptomModel.Model);

                        dr.ExecuteNonQuery(transaction);
                        dr.ClearParameters();
                    }
                }
            }

            if (this.SymptomEngineTypes != null)
            {
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    dr.ProcedureName = "SymptomEngineType_DeleteBySymptom";
                    dr.AddGuid("SymptomId", this.Id);
                    dr.ExecuteNonQuery(transaction);

                    dr.ClearParameters();

                    foreach (SymptomEngineType sympEngineType in this.SymptomEngineTypes)
                    {
                        dr.ProcedureName = "SymptomEngineType_Create";
                        dr.AddGuid("SymptomId", this.Id);
                        dr.AddNVarChar("EngineType", sympEngineType.EngineType);

                        dr.ExecuteNonQuery(transaction);
                        dr.ClearParameters();
                    }
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
            this.EnsureValidId();

            transaction = this.EnsureDatabasePrepared(connection, transaction);

            // Custom delete business logic here.

            // delete this current object
            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                //delete the item
                dr.ProcedureName = "Symptom_Delete";
                dr.AddGuid("SymptomId", this.Id);

                dr.ExecuteNonQuery(transaction);
            }

            transaction = base.Delete(connection, transaction);

            return transaction;
        }

        #endregion Required Methods (Load, Save, Delete, Etc)
    }
}
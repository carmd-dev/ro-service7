using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using System;
using System.Data.SqlClient;

namespace Innova.Vehicles
{
    /// <summary>
    /// Class handles the vehicle type error code information.
    ///
    /// The VehicleTypeCode object handles the business logic and data access for the specialized business object, VehicleTypeCode.
    /// The class inherits from <see cref="BusinessObjectBase"/> class.
    /// </summary>
    /// <example>
    /// See the examples of how to construct the VehicleTypeCode object.
    ///
    /// To create a new instance of a new of VehicleTypeCode.
    /// <code>VehicleTypeCode o = (VehicleTypeCode)Registry.CreateInstance(typeof(VehicleTypeCode));</code>
    ///
    /// To create an new instance of an existing VehicleTypeCode.
    /// <code>VehicleTypeCode o = (VehicleTypeCode)Registry.CreateInstance(typeof(VehicleTypeCode), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of VehicleTypeCode, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectBase.ConnectionString"/> to the <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable()]
    [ClassDefinition("Vehicle Type Code", "Vehicle Type Codes", "Vehicle Type Code", "VehicleTypeCodeId")]
    public class VehicleTypeCode : BusinessObjectBase
    {
        // data object variables
        private int codeId;

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
        private bool isDelmarCode;
        private bool isActive;
        private VehicleTypeStatus vehicleTypeCodeStatus;
        private DateTime createdDateTimeUTC;
        private DateTime updatedDateTimeUTC;

        private VehicleTypeCodeAssignmentCollection vehicleTypeCodeAssignments;

        #region System Constructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new (one that does not exist, assigning a new <see cref="Guid"/> Id). VehicleTypeCode object.
        /// In order to create a new VehicleTypeCode which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <example>
        /// <code>
        /// VehicleTypeCode o = (VehicleTypeCode)Registry.CreateInstance(typeof(VehicleTypeCode));
        /// </code>
        /// </example>
        protected internal VehicleTypeCode() : base()
        {
            IsObjectCreated = true;
            IsObjectLoaded = true;
            IsObjectDirty = true;
        }

        /// <summary>
        /// Constructor creates an existing (one that exists, and already has a <see cref="Guid"/> Id).  VehicleTypeCode object.
        /// In order to create an existing VehicleTypeCode object, which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <param name="id">The <see cref="Guid"/> or SQL uniqueidentifier indentification "Id" of the current object.</param>
        /// <example>
        /// <code>
        /// VehicleTypeCode o = (VehicleTypeCode)Registry.CreateInstance(typeof(VehicleTypeCode), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));
        /// </code>
        /// </example>
        protected internal VehicleTypeCode(Guid id) : base(id)
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
        /// Gets or sets the <see cref="int"/> assigned to the error by the Delmar system.  WARNING, this code may change in future versions from the Delmar system.
        /// </summary>
        [PropertyDefinition("Code ID", "The ID assigned to the error by the Delmar system.")]
        public int CodeId
        {
            get
            {
                EnsureLoaded();
                return codeId;
            }
            set
            {
                EnsureLoaded();
                if (codeId != value)
                {
                    IsObjectDirty = true;
                    codeId = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> title of the error condition.
        /// </summary>
        [PropertyDefinition("Definition", "The definition of the error condition.")]
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
        /// Gets or sets the <see cref="string"/> title of the error condition in Spanish.
        /// </summary>
        [PropertyDefinition("Spanish Definition", "The definition of the error condition in Spanish.")]
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
        /// Gets or sets the <see cref="string"/> title of the error condition in French.
        /// </summary>
        [PropertyDefinition("French Definition", "The definition of the error condition in French.")]
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
        /// Gets or sets the <see cref="string"/> title of the error condition in Mandarin Chinese.
        /// </summary>
        [PropertyDefinition("Chinese Definition", "The definition of the error condition in Chinese.")]
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
        /// Gets or sets the <see cref="string"/> conditions of the error condition.
        /// </summary>
        [PropertyDefinition("Conditions", "The conditions of the error condition.")]
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
        /// Gets or sets the <see cref="string"/> conditions of the error condition in Spanish.
        /// </summary>
        [PropertyDefinition("Spanish Conditions", "The conditions of the error condition in Spanish.")]
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
        /// Gets or sets the <see cref="string"/> conditions of the error condition in French.
        /// </summary>
        [PropertyDefinition("French Conditions", "The conditions of the error condition in French.")]
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
        /// Gets or sets the <see cref="string"/> conditions of the error condition in Mandarin Chinese.
        /// </summary>
        [PropertyDefinition("Chinese Conditions", "The conditions of the error condition in Chinese.")]
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
        /// Gets or sets the <see cref="string"/> possible causes of the error condition.
        /// </summary>
        [PropertyDefinition("Possible Causes", "The possible causes of the error condition.")]
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
        /// Gets or sets the <see cref="string"/> possible causes of the error condition in Spanish.
        /// </summary>
        [PropertyDefinition("Spanish Possible Causes", "The possible causes of the error condition in Spanish.")]
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
        /// Gets or sets the <see cref="string"/> possible causes of the error condition in French.
        /// </summary>
        [PropertyDefinition("French Possible Causes", "The possible causes of the error condition in French.")]
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
        /// Gets or sets the <see cref="string"/> possible causes of the error condition in Mandarin Chinese.
        /// </summary>
        [PropertyDefinition("Chinese Possible Causes", "The possible causes of the error condition in Chinese.")]
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
        /// Gets or sets the <see cref="int"/> int trips for the error condition.
        /// </summary>
        [PropertyDefinition("Trips", "The trips for the error condition.")]
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
        /// Gets or sets the <see cref="string"/> message indicator lamp file for the error condition.
        /// </summary>
        [PropertyDefinition("Message Indicator Lamp File", "The message indicator lamp file for the error condition.")]
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
        /// Gets or sets the <see cref="string"/> transmission control indicator lamp file for the error condition.
        /// </summary>
        [PropertyDefinition("Transmission Control Indicator Lamp File", "The transmission control indicator lamp file for the error condition.")]
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
        /// Gets or sets the <see cref="string"/> passive anti-theft indicator lamp file for the error condition.
        /// </summary>
        [PropertyDefinition("Passive Anti-Theft Indicator Lamp File", "The passive anti-theft indicator lamp file for the error condition.")]
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
        /// Gets or sets the <see cref="string"/> service throttle soon indicator lamp file for the error condition.
        /// </summary>
        [PropertyDefinition("Service Throttle Soon Indicator Lamp File", "The service throttle soon indicator lamp file for the error condition.")]
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
        /// Gets or sets the <see cref="string"/> monitor type for the error condition.
        /// </summary>
        [PropertyDefinition("Monitor Type", "The monitor type for the error condition.")]
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
        /// Gets or sets the <see cref="string"/> monitor file for the error condition.
        /// </summary>
        [PropertyDefinition("Monitor File", "The monitor file for the error condition.")]
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
        /// Gets or sets the <see cref="Boolean"/> indicating whether the error condition is a Delmar code.
        /// </summary>
        [PropertyDefinition("Is Delmar Code", "A flag indicating whether the error condition is a Delmar code.")]
        public bool IsDelmarCode
        {
            get
            {
                EnsureLoaded();
                return isDelmarCode;
            }
            set
            {
                EnsureLoaded();
                if (isDelmarCode != value)
                {
                    IsObjectDirty = true;
                    isDelmarCode = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Boolean"/> indicating whether the error condition is active.
        /// </summary>
        [PropertyDefinition("Is Active", "A flag indicating whether the error condition is active.")]
        public bool IsActive
        {
            get
            {
                EnsureLoaded();
                return isActive;
            }
            set
            {
                EnsureLoaded();
                if (isActive != value)
                {
                    IsObjectDirty = true;
                    isActive = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="VehicleTypeStatus"/> status for the error condition.
        /// </summary>
        [PropertyDefinition("Code Status", "The status for the error condition.")]
        public VehicleTypeStatus VehicleTypeCodeStatus
        {
            get
            {
                EnsureLoaded();
                return vehicleTypeCodeStatus;
            }
            set
            {
                EnsureLoaded();
                if (vehicleTypeCodeStatus != value)
                {
                    IsObjectDirty = true;
                    vehicleTypeCodeStatus = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/> date and time when the object was created.
        /// </summary>
        [PropertyDefinition("Created DateTime UTC", "The date and time when the object was created.")]
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
        /// Gets or sets the <see cref="DateTime"/> date and time when the object was last updated.
        /// </summary>
        [PropertyDefinition("Updated DateTime UTC", "The date and time when the object was last updated.")]
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

        #endregion Object Properties

        #region Object Properties (Related Objects)

        /*****************************************************************************************
		 *
		 * Object Relationships: Add correlated Object Collections
		 *
		*******************************************************************************************/

        /// <summary>
        /// Pick a top ErrorCode in assignment
        /// </summary>
        public string TopErrorCode { get; set; }

        public VehicleTypeCodeAssignmentCollection VehicleTypeCodeAssignments
        {
            get
            {
                if (this.vehicleTypeCodeAssignments == null)
                {
                    this.vehicleTypeCodeAssignments = new VehicleTypeCodeAssignmentCollection(this.Registry);

                    SqlProcedureCommand command = new SqlProcedureCommand();
                    command.ProcedureName = "VehicleTypeCodeAssignment_LoadByVehicleTypeCode";
                    command.AddBusinessObjectBase("VehicleTypeCodeId", this);

                    this.vehicleTypeCodeAssignments.Load(command, "VehicleTypeCodeAssignmentId", true, true, false);
                }

                return this.vehicleTypeCodeAssignments;
            }
        }

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
                //load the base vehicleTypeCode if user selected it.
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
            dr.ProcedureName = "VehicleTypeCode_Load";
            dr.AddGuid("VehicleTypeCodeId", Id);
        }

        /// <summary>
        /// Sets the properties of this object from the fields in the recordset located in the supplied <see cref="SqlDataReaderWrapper"/>.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> containing the recordset and fields used to set the properties of this object (this layer only).</param>
        protected new void SetPropertiesFromDataReader(SqlDataReaderWrapper dr)
        {
            codeId = dr.GetInt32("CodeId");
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
            trips = dr.GetInt32("Trips");
            messageIndicatorLampFile = dr.GetString("MessageIndicatorLampFile");
            transmissionControlIndicatorLampFile = dr.GetString("TransmissionControlIndicatorLampFile");
            passiveAntiTheftIndicatorLampFile = dr.GetString("PassiveAntiTheftIndicatorLampFile");
            serviceThrottleSoonIndicatorLampFile = dr.GetString("ServiceThrottleSoonIndicatorLampFile");
            monitorType = dr.GetString("MonitorType");
            monitorFile = dr.GetString("MonitorFile");
            isDelmarCode = dr.GetBoolean("IsDelmarCode");
            isActive = dr.GetBoolean("IsActive");
            vehicleTypeCodeStatus = (VehicleTypeStatus)dr.GetInt32("VehicleTypeCodeStatus");
            createdDateTimeUTC = dr.GetDateTime("CreatedDateTimeUTC");
            updatedDateTimeUTC = dr.GetDateTime("UpdatedDateTimeUTC");

            possibleCauses = possibleCauses.Replace(" * ", Environment.NewLine + "* ");

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
                        dr.ProcedureName = "VehicleTypeCode_Create";
                        CreatedDateTimeUTC = DateTime.UtcNow;
                        UpdatedDateTimeUTC = CreatedDateTimeUTC;
                    }
                    else
                    {
                        dr.ProcedureName = "VehicleTypeCode_Save";
                    }

                    dr.AddGuid("VehicleTypeCodeId", Id);
                    dr.AddInt32("CodeId", CodeId);
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
                    dr.AddBoolean("IsDelmarCode", IsDelmarCode);
                    dr.AddBoolean("IsActive", IsActive);
                    dr.AddInt32("VehicleTypeCodeStatus", (int)VehicleTypeCodeStatus);
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
            return transaction;
        }

        #endregion Required Object Methods (Load, Save, Delete, Etc)
    }
}
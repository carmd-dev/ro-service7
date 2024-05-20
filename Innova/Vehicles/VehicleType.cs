using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using System;
using System.Data.SqlClient;

namespace Innova.Vehicles
{
    /// <summary>
    /// Class library for the Vehicle Type, which provides a lookup
    /// for all vehicles.  Most of the data provided in the Vehicle type
    /// was imported from the Delmar system.
    ///
    /// The VehicleType object handles the business logic and data access for the specialized business object, VehicleType.
    /// The class inherits from <see cref="BusinessObjectBase"/> class.
    /// </summary>
    /// <example>
    /// See the examples of how to construct the VehicleType object.
    ///
    /// To create a new instance of a new of VehicleType.
    /// <code>VehicleType o = (VehicleType)Registry.CreateInstance(typeof(VehicleType));</code>
    ///
    /// To create an new instance of an existing VehicleType.
    /// <code>VehicleType o = (VehicleType)Registry.CreateInstance(typeof(VehicleType), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of VehicleType, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectBase.ConnectionString"/> to the <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable()]
    [ClassDefinition("Vehicle Type", "Vehicle Types", "Vehicle Type", "VehicleTypeId")]
    public class VehicleType : BusinessObjectBase
    {
        // data object variables
        private int tcId;

        private int year;
        private string manufacturerName = "";
        private string make = "";
        private string model = "";
        private string bodyCode = "";
        private string engineType = "";
        private string engineVINCode = "";
        private string engineTypeVINLookup = "";
        private string transmissionControlType = "";
        private string dlcLocation = "";
        private string dlcAccess = "";
        private string dlcComments = "";
        private bool isDelmarVehicle;
        private bool isActive;
        private VehicleTypeStatus vehicleTypeStatus;
        private DateTime createdDateTimeUTC;
        private DateTime updatedDateTimeUTC;

        #region System Constructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new (one that does not exist, assigning a new <see cref="Guid"/> Id). VehicleType object.
        /// In order to create a new VehicleType which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <example>
        /// <code>
        /// VehicleType o = (VehicleType)Registry.CreateInstance(typeof(VehicleType));
        /// </code>
        /// </example>
        protected internal VehicleType() : base()
        {
            IsObjectCreated = true;
            IsObjectLoaded = true;
            IsObjectDirty = true;
        }

        /// <summary>
        /// Constructor creates an existing (one that exists, and already has a <see cref="Guid"/> Id).  VehicleType object.
        /// In order to create an existing VehicleType object, which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <param name="id">The <see cref="Guid"/> or SQL uniqueidentifier indentification "Id" of the current object.</param>
        /// <example>
        /// <code>
        /// VehicleType o = (VehicleType)Registry.CreateInstance(typeof(VehicleType), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));
        /// </code>
        /// </example>
        protected internal VehicleType(Guid id) : base(id)
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
        /// Gets or sets the <see cref="int"/> TCID of the vehicle assigned by the Delmar system.
        /// </summary>
        public int TCID
        {
            get
            {
                EnsureLoaded();
                return tcId;
            }
            set
            {
                EnsureLoaded();
                if (tcId != value)
                {
                    IsObjectDirty = true;
                    tcId = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/> year the car was manufactured.
        /// </summary>
        ///
        [PropertyDefinition("Year", "Year vehicle was manufactured")]
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
        /// Gets or sets the <see cref="string"/> manufacturers long name.
        /// </summary>
        [PropertyDefinition("Manufacturer", "Name of the manufacturer of the vehicle.")]
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
        /// Gets or sets the <see cref="string"/> make or short manufacturer name.
        /// </summary>
        [PropertyDefinition("Make", "Make of the vehicle")]
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
        /// Gets or sets the <see cref="string"/> model of the vehicle.
        /// </summary>
        [PropertyDefinition("Model", "Model of the vehicle")]
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
        /// Gets or sets the <see cref="string"/> body code of the vehicle.
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
        /// Gets or sets the <see cref="string"/> engine type provided by the delmar system of the vehicle.
        /// </summary>
        [PropertyDefinition("Engine", "Type of engine in the vehicle")]
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
        /// Gets or sets the <see cref="string"/> engine VIN code provided by the Delmar system.
        /// </summary>
        [PropertyDefinition("Engine Code", "Engine VIN code provided by Delmar System")]
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
        /// Gets or sets the <see cref="string"/> engine type used to match with the VINPower Vin Decoder.
        /// </summary>
        [PropertyDefinition("Engine VIN", "Engine Type used to match with the VINPower Vin Decode.")]
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
        /// Gets or sets the <see cref="string"/> transmission control type provided by the Delmar system.
        /// </summary>
        [PropertyDefinition("Transmission", "Type of transmission in the vehicle")]
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
        /// Gets or sets the <see cref="string"/> DLC Location provided by CarMD.
        /// </summary>
        public string DLCLocation
        {
            get
            {
                EnsureLoaded();
                return dlcLocation;
            }
            set
            {
                EnsureLoaded();
                if (dlcLocation != value)
                {
                    IsObjectDirty = true;
                    dlcLocation = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> DLC Access provided by CarMD.
        /// </summary>
        public string DLCAccess
        {
            get
            {
                EnsureLoaded();
                return dlcAccess;
            }
            set
            {
                EnsureLoaded();
                if (dlcAccess != value)
                {
                    IsObjectDirty = true;
                    dlcAccess = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> DLC Comments provided by CarMD.
        /// </summary>
        public string DLCComments
        {
            get
            {
                EnsureLoaded();
                return dlcComments;
            }
            set
            {
                EnsureLoaded();
                if (dlcComments != value)
                {
                    IsObjectDirty = true;
                    dlcComments = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/> value indicating whether this object was imported from Delmar.
        /// </summary>
        public bool IsDelmarVehicle
        {
            get
            {
                EnsureLoaded();
                return isDelmarVehicle;
            }
            set
            {
                EnsureLoaded();
                if (isDelmarVehicle != value)
                {
                    IsObjectDirty = true;
                    isDelmarVehicle = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/> value indicating whether this vehicle type is active.
        /// </summary>
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
        /// DEPRICATED -- Gets or sets the <see cref="VehicleTypeStatus"/> status of the vehicle type.
        /// </summary>
        public VehicleTypeStatus VehicleTypeStatus
        {
            get
            {
                EnsureLoaded();
                return vehicleTypeStatus;
            }
            set
            {
                EnsureLoaded();
                if (vehicleTypeStatus != value)
                {
                    IsObjectDirty = true;
                    vehicleTypeStatus = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/> date and time when the object was created.
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
        /// Gets or sets the <see cref="DateTime"/> date and time when the object was last updated.
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
            dr.ProcedureName = "VehicleType_Load";
            dr.AddGuid("VehicleTypeId", Id);
        }

        /// <summary>
        /// Sets the properties of this object from the fields in the recordset located in the supplied <see cref="SqlDataReaderWrapper"/>.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> containing the recordset and fields used to set the properties of this object (this layer only).</param>
        protected new void SetPropertiesFromDataReader(SqlDataReaderWrapper dr)
        {
            tcId = dr.GetInt32("TCID");
            year = dr.GetInt32("Year");
            manufacturerName = dr.GetString("ManufacturerName");
            make = dr.GetString("Make");
            model = dr.GetString("Model");
            bodyCode = dr.GetString("BodyCode");
            engineType = dr.GetString("EngineType");
            engineVINCode = dr.GetString("EngineVINCode");
            engineTypeVINLookup = dr.GetString("EngineTypeVINLookup");
            transmissionControlType = dr.GetString("TransmissionControlType");
            dlcLocation = dr.GetString("DLCLocation");
            dlcAccess = dr.GetString("DLCAccess");
            dlcComments = dr.GetString("DLCComments");
            isDelmarVehicle = dr.GetBoolean("IsDelmarVehicle");
            isActive = dr.GetBoolean("IsActiveVehicle");
            vehicleTypeStatus = (VehicleTypeStatus)dr.GetInt32("VehicleTypeStatus");
            createdDateTimeUTC = dr.GetDateTime("CreatedDateTimeUTC");
            updatedDateTimeUTC = dr.GetDateTime("UpdatedDateTimeUTC");

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
                        dr.ProcedureName = "VehicleType_Create";
                        CreatedDateTimeUTC = DateTime.UtcNow;
                        UpdatedDateTimeUTC = CreatedDateTimeUTC;
                    }
                    else
                    {
                        dr.ProcedureName = "VehicleType_Save";
                    }

                    dr.AddGuid("VehicleTypeId", Id);
                    dr.AddInt32("TCID", TCID);
                    dr.AddInt32("Year", Year);
                    dr.AddNVarChar("ManufacturerName", ManufacturerName);
                    dr.AddNVarChar("Make", Make);
                    dr.AddNVarChar("Model", Model);
                    dr.AddNVarChar("BodyCode", BodyCode);
                    dr.AddNVarChar("EngineType", EngineType);
                    dr.AddNVarChar("EngineVINCode", EngineVINCode);
                    dr.AddNVarChar("EngineTypeVINLookup", EngineTypeVINLookup);
                    dr.AddNVarChar("TransmissionControlType", TransmissionControlType);
                    dr.AddNVarChar("DLCLocation", DLCLocation);
                    dr.AddNVarChar("DLCAccess", DLCAccess);
                    dr.AddNText("DLCComments", DLCComments);
                    dr.AddBoolean("IsDelmarVehicle", IsDelmarVehicle);
                    dr.AddBoolean("IsActiveVehicle", IsActive);
                    dr.AddInt32("VehicleTypeStatus", (int)VehicleTypeStatus);
                    dr.AddDateTime("CreatedDateTimeUTC", CreatedDateTimeUTC);
                    dr.AddDateTime("UpdatedDateTimeUTC", UpdatedDateTimeUTC);
                    dr.AddNText("FullTextSearch", FullTextSearchUtilities.GetDataPhrase(ManufacturerName, Make, Model, BodyCode, EngineType, EngineVINCode, TransmissionControlType));

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
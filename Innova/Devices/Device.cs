using Innova.Users;
using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using Metafuse3.NullableTypes;
using System;
using System.Data.SqlClient;

namespace Innova.Devices
{
    /// <summary>
    /// The Device object handles the business logic and data access for the specialized business object, Device.
    /// The class inherits from <see cref="BusinessObjectBase"/> class.
    /// </summary>
    /// <example>
    /// See the examples of how to construct the Device object.
    ///
    /// To create a new instance of a new of Device.
    /// <code>Device o = (Device)Registry.CreateInstance(typeof(Device));</code>
    ///
    /// To create an new instance of an existing Device.
    /// <code>Device o = (Device)Registry.CreateInstance(typeof(Device), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of Device, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectBase.ConnectionString"/> to the <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable()]
    public class Device : BusinessObjectBase
    {
        // data object variables
        private User user;

        private Guid chipId;
        private int maximumVehicleCount = 5;
        private int maximumUploadCount = 30;
        private bool isActive = true;
        private bool isPrimaryOwner = true;
        private DateTime createdDateTimeUTC;
        private DateTime updatedDateTimeUTC;
        private User userRequestingTransfer;
        private NullableDateTime userRequestDateTimeUTC = NullableDateTime.Null;
        private bool isManualDevice;
        private int transferCount = 0;
        private NullableDateTime transferAcceptedDateTimeUTC = NullableDateTime.Null;
        private NullableDateTime deactivatedDateTimeUTC = NullableDateTime.Null;

        #region System Constructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new (one that does not exist, assigning a new <see cref="Guid"/> Id). Device object.
        /// In order to create a new Device which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <example>
        /// <code>
        /// Device o = (Device)Registry.CreateInstance(typeof(Device));
        /// </code>
        /// </example>
        protected internal Device() : base()
        {
            IsObjectCreated = true;
            IsObjectLoaded = true;
            IsObjectDirty = true;
        }

        /// <summary>
        /// Constructor creates an existing (one that exists, and already has a <see cref="Guid"/> Id).  Device object.
        /// In order to create an existing Device object, which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <param name="id">The <see cref="Guid"/> or SQL uniqueidentifier indentification "Id" of the current object.</param>
        /// <example>
        /// <code>
        /// Device o = (Device)Registry.CreateInstance(typeof(Device), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));
        /// </code>
        /// </example>
        protected internal Device(Guid id) : base(id)
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
        /// Gets or sets the <see cref="User"/> the device is associated with
        /// </summary>
        public User User
        {
            get
            {
                EnsureLoaded();
                return user;
            }
            set
            {
                EnsureLoaded();
                if (user != value)
                {
                    IsObjectDirty = true;
                    user = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Guid"/> id of the chip on the device.
        /// </summary>
        public Guid ChipId
        {
            get
            {
                EnsureLoaded();
                return chipId;
            }
            set
            {
                EnsureLoaded();
                if (chipId != value)
                {
                    IsObjectDirty = true;
                    chipId = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/> flag indicating whether or not the device is active for the user or not.
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
        /// Gets or sets the <see cref="bool"/> flag indicating whether or not the user is the primary owner, or the first person to upload with the device.
        /// </summary>
        public bool IsPrimaryOwner
        {
            get
            {
                EnsureLoaded();
                return isPrimaryOwner;
            }
            set
            {
                EnsureLoaded();
                if (isPrimaryOwner != value)
                {
                    IsObjectDirty = true;
                    isPrimaryOwner = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/> in universal time the record was originally created.
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
        /// Gets or sets the <see cref="DateTime"/> in universal time the record was last updated.
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
        /// Gets or sets the <see cref="User"/> requesting a device transfer
        /// </summary>
        public User UserRequestingTransfer
        {
            get
            {
                EnsureLoaded();
                return userRequestingTransfer;
            }
            set
            {
                EnsureLoaded();
                if (userRequestingTransfer != value)
                {
                    IsObjectDirty = true;
                    userRequestingTransfer = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableDateTime"/> when the user requested a device transfer.
        /// </summary>
        public NullableDateTime UserRequestDateTimeUTC
        {
            get
            {
                EnsureLoaded();
                return userRequestDateTimeUTC;
            }
            set
            {
                EnsureLoaded();
                if (userRequestDateTimeUTC != value)
                {
                    IsObjectDirty = true;
                    userRequestDateTimeUTC = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Boolean"/> indicating if this device is a manually created device.
        /// </summary>
        public bool IsManualDevice
        {
            get
            {
                EnsureLoaded();
                return isManualDevice;
            }
            set
            {
                EnsureLoaded();
                if (isManualDevice != value)
                {
                    IsObjectDirty = true;
                    isManualDevice = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/> numbers of time this device has been transfered.
        /// </summary>
        public int TransferCount
        {
            get
            {
                EnsureLoaded();
                return transferCount;
            }
            set
            {
                EnsureLoaded();
                if (transferCount != value)
                {
                    IsObjectDirty = true;
                    transferCount = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableDateTime"/> when the user accepted the transfer request.
        /// </summary>
        public NullableDateTime TransferAcceptedDateTimeUTC
        {
            get
            {
                EnsureLoaded();
                return transferAcceptedDateTimeUTC;
            }
            set
            {
                EnsureLoaded();
                if (transferAcceptedDateTimeUTC != value)
                {
                    IsObjectDirty = true;
                    transferAcceptedDateTimeUTC = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableDateTime"/> when the device was deactivated.
        /// </summary>
        public NullableDateTime DeactivatedDateTimeUTC
        {
            get
            {
                EnsureLoaded();
                return deactivatedDateTimeUTC;
            }
            set
            {
                EnsureLoaded();
                if (deactivatedDateTimeUTC != value)
                {
                    IsObjectDirty = true;
                    deactivatedDateTimeUTC = value;
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

        /// <summary>
        /// Gets the <see cref="Device"/> that was manually created and assigned to the user provided. Returns null if the device does not exist.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static Device GetManualDeviceForUser(User user)
        {
            Device d = null;

            using (SqlDataReaderWrapper dataReader = new SqlDataReaderWrapper(user.Registry.ConnectionStringDefault))
            {
                dataReader.ProcedureName = "Device_LoadByUserAndIsManual";
                dataReader.AddGuid("UserId", user.Id);
                dataReader.Execute();

                if (dataReader.Read())
                {
                    d = (Device)user.Registry.CreateInstance(typeof(Device), dataReader.GetGuid("DeviceId"));
                    d.LoadPropertiesFromDataReader(dataReader, true);
                }
            }

            return d;
        }

        /// <summary>
        /// Gets the <see cref="Device"/> based on the device chip id. Returns null if the device does not exist.
        /// </summary>
        /// <param name="registry"><see cref="Registry"/> currently being used.</param>
        /// <param name="chipId"><see cref="Guid"/> device chip id currently in use.</param>
        /// <param name="userId"><see cref="Guid"/> id of the user the device is supposed to be attached to.</param>
        /// <returns><see cref="Device"/> which matches the device id supplied. Returns null if no devices exist.</returns>
        public static Device GetDeviceByChipIdAndUserIdAndActive(Registry registry, Guid chipId, Guid userId)
        {
            Device d = null;

            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(registry.ConnectionStringDefault))
            {
                dr.ProcedureName = "Device_LoadByChipIdAndUserIdActive";
                dr.AddGuid("ChipId", chipId);
                dr.AddGuid("UserId", userId);

                dr.Execute();

                if (dr.Read())
                {
                    d = (Device)registry.CreateInstance(typeof(Device), dr.GetGuid("DeviceId"));
                    d.LoadPropertiesFromDataReader(dr, true);
                }
            }

            return d;
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
            dr.ProcedureName = "Device_Load";
            dr.AddGuid("DeviceId", Id);
        }

        /// <summary>
        /// Sets the properties of this object from the fields in the recordset located in the supplied <see cref="SqlDataReaderWrapper"/>.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> containing the recordset and fields used to set the properties of this object (this layer only).</param>
        protected new void SetPropertiesFromDataReader(SqlDataReaderWrapper dr)
        {
            user = (User)Registry.CreateInstance(typeof(User), dr.GetGuid("UserId"));
            chipId = dr.GetGuid("ChipId");
            maximumVehicleCount = dr.GetInt32("MaximumVehicleCount");
            maximumUploadCount = dr.GetInt32("MaximumUploadCount");
            isActive = dr.GetBoolean("IsActive");
            isPrimaryOwner = dr.GetBoolean("IsPrimaryOwner");
            createdDateTimeUTC = dr.GetDateTime("CreatedDateTimeUTC");
            updatedDateTimeUTC = dr.GetDateTime("UpdatedDateTimeUTC");
            if (!dr.IsDBNull("UserRequestingTransferId"))
            {
                userRequestingTransfer = (User)Registry.CreateInstance(typeof(User), dr.GetGuid("UserRequestingTransferId"));
            }
            userRequestDateTimeUTC = dr.GetNullableDateTime("UserRequestDateTimeUTC");
            isManualDevice = dr.GetBoolean("IsManualDevice");
            transferCount = dr.GetInt32("TransferCount");
            transferAcceptedDateTimeUTC = dr.GetNullableDateTime("TransferAcceptedDateTimeUTC");
            deactivatedDateTimeUTC = dr.GetNullableDateTime("DeactivatedDateTimeUTC");

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
                        dr.ProcedureName = "Device_Create";
                        // If this class has the following properties then verify the names and
                        // uncomment the line.  Otherwise just remove the following lines.
                        CreatedDateTimeUTC = DateTime.UtcNow;
                        UpdatedDateTimeUTC = CreatedDateTimeUTC;
                    }
                    else
                    {
                        dr.ProcedureName = "Device_Save";
                        UpdatedDateTimeUTC = DateTime.UtcNow;
                    }

                    dr.AddGuid("DeviceId", Id);
                    dr.AddGuid("UserId", User.Id);
                    dr.AddGuid("ChipId", ChipId);
                    // Depricated //
                    dr.AddInt32("MaximumVehicleCount", maximumVehicleCount);
                    dr.AddInt32("MaximumUploadCount", maximumUploadCount);
                    // ********** //
                    dr.AddBoolean("IsActive", IsActive);
                    dr.AddBoolean("IsPrimaryOwner", IsPrimaryOwner);
                    dr.AddDateTime("CreatedDateTimeUTC", CreatedDateTimeUTC);
                    dr.AddDateTime("UpdatedDateTimeUTC", UpdatedDateTimeUTC);
                    if (UserRequestingTransfer != null)
                    {
                        dr.AddGuid("UserRequestingTransferId", UserRequestingTransfer.Id);
                    }
                    if (!UserRequestDateTimeUTC.IsNull)
                    {
                        dr.AddDateTime("UserRequestDateTimeUTC", UserRequestDateTimeUTC.Value);
                    }
                    dr.AddBoolean("IsManualDevice", IsManualDevice);
                    dr.AddInt32("TransferCount", TransferCount);
                    if (!TransferAcceptedDateTimeUTC.IsNull)
                    {
                        dr.AddDateTime("TransferAcceptedDateTimeUTC", TransferAcceptedDateTimeUTC.Value);
                    }
                    if (!DeactivatedDateTimeUTC.IsNull)
                    {
                        dr.AddDateTime("DeactivatedDateTimeUTC", DeactivatedDateTimeUTC.Value);
                    }

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

            // delete this current object
            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                //delete the item
                dr.ProcedureName = "Device_Delete";
                dr.AddGuid("DeviceId", Id);

                dr.ExecuteNonQuery(transaction);
            }

            transaction = base.Delete(connection, transaction);

            return transaction;
        }

        #endregion Required Object Methods (Load, Save, Delete, Etc)
    }
}
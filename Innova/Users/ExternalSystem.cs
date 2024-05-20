using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using Metafuse3.NullableTypes;
using Metafuse3.Security;
using System;
using System.Collections.Specialized;
using System.Data.SqlClient;

namespace Innova.Users
{
    /// <summary>
    /// The ExternalSystem object handles the business logic and data access for the specialized business object, ExternalSystem.
    /// The class inherits from <see cref="BusinessObjectBase"/> class.
    /// </summary>
    /// <example>
    /// See the examples of how to construct the ExternalSystem object.
    ///
    /// To create a new instance of a new of ExternalSystem.
    /// <code>ExternalSystem o = (ExternalSystem)this.Registry.CreateInstance(typeof(ExternalSystem));</code>
    ///
    /// To create an new instance of an existing ExternalSystem.
    /// <code>ExternalSystem o = (ExternalSystem)this.Registry.CreateInstance(typeof(ExternalSystem), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of ExternalSystem, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectBase.ConnectionString"/> to the <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable()]
    [ClassDefinition("External System", "External Systems", "External System", "ExternalSystemId")]
    public class ExternalSystem : InnovaBusinessObjectBase
    {
        public static Guid EXTERNAL_SYSTEM_ID_CARMD = new Guid("00000000-0000-0000-0000-000000000001");
        public static Guid EXTERNAL_SYSTEM_ID_MATCO = new Guid("00000000-0000-0000-0000-000000000002");
        public static Guid EXTERNAL_SYSTEM_ID_INNOVA = new Guid("00000000-0000-0000-0000-000000000003");
        public static Guid EXTERNAL_SYSTEM_ID_PROINNOVA = new Guid("00000000-0000-0000-0000-000000000004");
        public static Guid EXTERNAL_SYSTEM_ID_OBDFIX = new Guid("00000000-0000-0000-0000-000000000005");
        public static Guid EXTERNAL_SYSTEM_ID_GREENLIGHT = new Guid("00000000-0000-0000-0000-000000000006");
        public static Guid EXTERNAL_SYSTEM_ID_MEXICO = new Guid("00000000-0000-0000-0000-000000000007");
        public static Guid EXTERNAL_SYSTEM_ID_JAPAN = new Guid("00000000-0000-0000-0000-000000000008");
        public static Guid EXTERNAL_SYSTEM_ID_CHINA = new Guid("00000000-0000-0000-0000-000000000009");
        public static Guid EXTERNAL_SYSTEM_ID_AUTOZONE = new Guid("00000000-0000-0000-0000-000000000010");
        public static Guid EXTERNAL_SYSTEM_ID_CALAMP = new Guid("00000000-0000-0000-0000-000000000011");
        public static Guid EXTERNAL_SYSTEM_ID_AUTOZONE_VDS = new Guid("00000000-0000-0000-0000-000000000012");
        public static Guid EXTERNAL_SYSTEM_ID_RISK = new Guid("00000000-0000-0000-0000-000000000013");
        public static Guid EXTERNAL_SYSTEM_ID_MODUS = new Guid("00000000-0000-0000-0000-000000000014");
        public static Guid EXTERNAL_SYSTEM_ID_AUTOZONE_BLACKBOX = new Guid("00000000-0000-0000-0000-000000000015");

        private NullableGuid keyGuid = null;
        private string name = "";
        private string partnerID = "";
        private string imageNameSuffix = "";
        private bool isActive;
        private AdminUser adminUserCreated;
        private AdminUser adminUserUpdated;
        private DateTime createdDateTimeUTC = DateTime.MinValue;
        private DateTime updatedDateTimeUTC = DateTime.MinValue;

        #region Contructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new (one that does not exist, assigning a new <see cref="Guid"/> Id). ExternalSystem object.
        /// In order to create a new ExternalSystem which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <example>
        /// <code>
        /// ExternalSystem o = (ExternalSystem)Registry.CreateInstance(typeof(ExternalSystem));
        /// </code>
        /// </example>
        protected internal ExternalSystem()
            : base()
        {
            this.IsObjectCreated = true;
            this.IsObjectLoaded = true;
            this.IsObjectDirty = true;

            this.CreatedDateTimeUTC = DateTime.UtcNow;
            this.UpdatedDateTimeUTC = this.CreatedDateTimeUTC;
        }

        /// <summary>
        /// Constructor creates an existing (one that exists, and already has a <see cref="Guid"/> Id).  ExternalSystem object.
        /// In order to create an existing ExternalSystem object, which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <param name="id">The <see cref="Guid"/> or SQL uniqueidentifier indentification "Id" of the current object.</param>
        /// <example>
        /// <code>
        /// ExternalSystem o = (ExternalSystem)Registry.CreateInstance(typeof(ExternalSystem), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));
        /// </code>
        /// </example>
        protected internal ExternalSystem(Guid id)
            : base(id)
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
        /// Gets the <see cref="Guid"/>
        /// </summary>
        [PropertyDefinition("Key Guid", "The Guid to be used as the key for the external system.")]
        public NullableGuid KeyGuid
        {
            get
            {
                this.EnsureLoaded();
                return this.keyGuid;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.keyGuid)
                {
                    this.IsObjectDirty = true;
                    this.keyGuid = value;
                    this.UpdatedField("KeyGuid");
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> encrypted key for this external system.
        /// </summary>
        public string EncryptedKey
        {
            get
            {
                if (this.KeyGuid.HasValue)
                {
                    return ExternalSystem.EncryptIdToExternalKeyString(this.KeyGuid.Value);
                }
                return "";
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> name of the external system.
        /// </summary>
        [PropertyDefinition("System", "System", "System", "Site", "Name of the external System")]
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
                if (value != this.name)
                {
                    this.IsObjectDirty = true;
                    this.name = value;
                    this.UpdatedField("Name");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> name of the external system.
        /// </summary>
        [PropertyDefinition("Partner ID", "Partner ID", "Partner ID", "Partner ID", "The partner ID used by the Innova Utilities DLL for decoding payloads.")]
        public string PartnerID
        {
            get
            {
                this.EnsureLoaded();
                return this.partnerID;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.partnerID)
                {
                    this.IsObjectDirty = true;
                    this.partnerID = value;
                    this.UpdatedField("PartnerID");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> suffix added to image files for this external System.
        /// </summary>
        [PropertyDefinition("System", "Suffix added to image files for this external System")]
        public string ImageNameSuffix
        {
            get
            {
                this.EnsureLoaded();
                return this.imageNameSuffix;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.imageNameSuffix)
                {
                    this.IsObjectDirty = true;
                    this.imageNameSuffix = value;
                    this.UpdatedField("ImageNameSuffix");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/> flag indicating whether or not the system is active or not.
        /// </summary>
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
                if (value != this.isActive)
                {
                    this.IsObjectDirty = true;
                    this.isActive = value;
                    this.UpdatedField("IsActive");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="AdminUser"/> who created the system record.
        /// </summary>
        public AdminUser AdminUserCreated
        {
            get
            {
                this.EnsureLoaded();
                return this.adminUserCreated;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.adminUserCreated)
                {
                    this.IsObjectDirty = true;
                    this.adminUserCreated = value;
                    this.UpdatedField("AdminUserCreatedId");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="AdminUser"/> who last updated the system record.
        /// </summary>
        public AdminUser AdminUserUpdated
        {
            get
            {
                this.EnsureLoaded();
                return this.adminUserUpdated;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.adminUserUpdated)
                {
                    this.IsObjectDirty = true;
                    this.adminUserUpdated = value;
                    this.UpdatedField("AdminUserUpdatedId");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/> the system record was created on.
        /// </summary>
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
        /// Gets or sets the <see cref="DateTime"/> the system record was last updated.
        /// </summary>
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
        /// Gets a <see cref="bool"/> indicating if the external system is an Innova site.
        /// </summary>
        public bool IsInnovaSite
        {
            get
            {
                return (this.Id == EXTERNAL_SYSTEM_ID_INNOVA || this.Id == EXTERNAL_SYSTEM_ID_MATCO || this.Id == EXTERNAL_SYSTEM_ID_PROINNOVA);
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

        /// <summary>
        /// Gets the <see cref="ExternalSystem"/> from the supplied encrypted <see cref="string"/> key.
        /// </summary>
        /// <param name="registry"><see cref="Registry"/> currently in use</param>
        /// <param name="encrpytedKey"><see cref="string"/> encrypted external system key</param>
        /// <returns><see cref="ExternalSystem"/></returns>
        public static ExternalSystem GetActiveExternalSystemFromKey(Registry registry, string encrpytedKey)
        {
            ExternalSystem externalSystem = null;

            Guid? id = ExternalSystem.DecryptExternalKeyStringToId(encrpytedKey);

            if (id.HasValue)
            {
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(registry.ConnectionStringDefault))
                {
                    dr.ProcedureName = "ExternalSystem_LoadByKeyGuidAndActive";
                    dr.AddGuid("KeyGuid", id.Value);

                    dr.Execute();

                    if (dr.Read())
                    {
                        externalSystem = (ExternalSystem)registry.CreateInstance(typeof(ExternalSystem), dr.GetGuid("ExternalSystemId"));
                        externalSystem.LoadPropertiesFromDataReader(dr, true);
                    }
                }
            }

            return externalSystem;
        }

        /// <summary>
        /// Gets the <see cref="Guid"/> Id that matches the supplied key string for the id of this object
        /// </summary>
        /// <param name="keyStringToDecode"><see cref="string"/> key to decode</param>
        /// <returns><see cref="Guid"/> Id equal to the string key supplied, null if invalid.</returns>
        private static Guid? DecryptExternalKeyStringToId(string keyStringToDecode)
        {
            DESEncryptor dec = new DESEncryptor();

            string s = null;

            try
            {
                s = dec.DecryptData(k, keyStringToDecode);
            }
            catch
            {
                s = null;
            }

            Guid? g = null;
            if (!String.IsNullOrEmpty(s))
            {
                try
                {
                    g = new Guid(s);
                }
                catch
                {
                    g = null;
                }
            }
            return g;
        }

        /// <summary>
        /// Gets the <see cref="string"/> key of the id for the external system.
        /// </summary>
        /// <param name="id"><see cref="Guid"/> id to encrypt.</param>
        /// <returns><see cref="string"/> encrypted key.</returns>
        public static string EncryptIdToExternalKeyString(Guid id)
        {
            DESEncryptor enc = new DESEncryptor();
            return enc.EncryptData(k, id.ToString());
        }

        private static string k = "WSKey1";

        /// <summary>
        /// Gets or sets the <see cref="bool"/> flag indicating whether or not the system can be deleted.  (Only if no records are made for this external system)
        /// </summary>
        /// <returns><see cref="bool"/> flag indicating whether or not the system is deletable or not.</returns>
        public override bool IsDeletable()
        {
            return false;
        }

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
            dr.ProcedureName = "ExternalSystem_Load";
            dr.AddGuid("ExternalSystemId", this.Id);
        }

        /// <summary>
        /// Sets the properties of this object from the fields in the recordset located in the supplied <see cref="SqlDataReaderWrapper"/>.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> containing the recordset and fields used to set the properties of this object (this layer only).</param>
        protected new void SetPropertiesFromDataReader(SqlDataReaderWrapper dr)
        {
            this.keyGuid = dr.GetNullableGuid("KeyGuid");
            this.name = dr.GetString("Name");
            this.partnerID = dr.GetString("PartnerID");
            this.imageNameSuffix = dr.GetString("ImageNameSuffix");
            this.isActive = dr.GetBoolean("IsActive");
            this.adminUserCreated = (AdminUser)dr.GetBusinessObjectBase(this.Registry, typeof(AdminUser), "AdminUserCreatedId");
            this.adminUserUpdated = (AdminUser)dr.GetBusinessObjectBase(this.Registry, typeof(AdminUser), "AdminUserUpdatedId");
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
                        dr.ProcedureName = "ExternalSystem_Create";
                    }
                    else
                    {
                        dr.UpdateFields("ExternalSystem", "ExternalSystemId", this.updatedFields);
                    }

                    dr.AddGuid("ExternalSystemId", this.Id);
                    dr.AddGuid("KeyGuid", this.KeyGuid);
                    dr.AddNVarChar("Name", this.Name);
                    dr.AddNVarChar("PartnerID", this.PartnerID);
                    dr.AddNVarChar("ImageNameSuffix", this.ImageNameSuffix);
                    dr.AddBoolean("IsActive", this.IsActive);
                    dr.AddBusinessObject("AdminUserCreatedId", this.AdminUserCreated);
                    dr.AddBusinessObject("AdminUserUpdatedId", this.AdminUserUpdated);
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

            // delete this current object
            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                //delete the item
                dr.ProcedureName = "ExternalSystem_Delete";
                dr.AddGuid("ExternalSystemId", this.Id);

                dr.ExecuteNonQuery(transaction);
            }

            transaction = base.Delete(connection, transaction);

            return transaction;
        }

        #endregion Required Methods (Load, Save, Delete, Etc)
    }
}
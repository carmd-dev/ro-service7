using Innova.Users;
using Innova.Vehicles;
using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using System;
using System.Collections.Specialized;
using System.Data.SqlClient;

namespace Innova.Fixes
{
    /// <summary>
    /// The FixPolkVehicleDiscrepancy object handles the business logic and data access for the specialized business object, FixPolkVehicleDiscrepancy.
    /// The class inherits from <see cref="BusinessObjectBase"/> class.
    /// </summary>
    /// <example>
    /// See the examples of how to construct the FixPolkVehicleDiscrepancy object.
    ///
    /// To create a new instance of a new of FixPolkVehicleDiscrepancy.
    /// <code>FixPolkVehicleDiscrepancy o = (FixPolkVehicleDiscrepancy)this.Registry.CreateInstance(typeof(FixPolkVehicleDiscrepancy));</code>
    ///
    /// To create an new instance of an existing FixPolkVehicleDiscrepancy.
    /// <code>FixPolkVehicleDiscrepancy o = (FixPolkVehicleDiscrepancy)this.Registry.CreateInstance(typeof(FixPolkVehicleDiscrepancy), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of FixPolkVehicleDiscrepancy, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectBase.ConnectionString"/> to the <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable()]
    public class FixPolkVehicleDiscrepancy : BusinessObjectBase
    {
        private Fix fix;
        private PolkVehicleYMME polkVehicleYMME;
        private int occurrencesOfPolkMissing = 0;
        private int occurrencesOfVinPowerMissing = 0;
        private bool isIgnored;
        private bool isDeleted;
        private AdminUser adminUserSetIgnoredBy;
        private AdminUser adminUserDeletedBy;
        private DateTime createdDateTimeUTC;
        private DateTime updatedDateTimeUTC;

        #region Contructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new (one that does not exist, assigning a new <see cref="Guid"/> Id). FixPolkVehicleDiscrepancy object.
        /// In order to create a new FixPolkVehicleDiscrepancy which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <example>
        /// <code>
        /// FixPolkVehicleDiscrepancy o = (FixPolkVehicleDiscrepancy)Registry.CreateInstance(typeof(FixPolkVehicleDiscrepancy));
        /// </code>
        /// </example>
        protected internal FixPolkVehicleDiscrepancy()
            : base()
        {
            this.IsObjectCreated = true;
            this.IsObjectLoaded = true;
            this.IsObjectDirty = true;
        }

        /// <summary>
        /// Constructor creates an existing (one that exists, and already has a <see cref="Guid"/> Id).  FixPolkVehicleDiscrepancy object.
        /// In order to create an existing FixPolkVehicleDiscrepancy object, which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <param name="id">The <see cref="Guid"/> or SQL uniqueidentifier indentification "Id" of the current object.</param>
        /// <example>
        /// <code>
        /// FixPolkVehicleDiscrepancy o = (FixPolkVehicleDiscrepancy)Registry.CreateInstance(typeof(FixPolkVehicleDiscrepancy), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));
        /// </code>
        /// </example>
        protected internal FixPolkVehicleDiscrepancy(Guid id)
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
        /// Gets or sets the <see cref="Fix"/> record.
        /// </summary>
        [PropertyDefinition("Fix", "The Fix Record.")]
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
                if (value != this.fix)
                {
                    this.IsObjectDirty = true;
                    this.fix = value;
                    this.UpdatedField("FixId");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="PolkVehicleYMME"/> Polk vehicle year, make, model and engine.
        /// </summary>
        [PropertyDefinition("Polk Vehicle YMME", "The Polk vehicle year, make, model and engine.")]
        public PolkVehicleYMME PolkVehicleYMME
        {
            get
            {
                this.EnsureLoaded();
                return this.polkVehicleYMME;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.polkVehicleYMME)
                {
                    this.IsObjectDirty = true;
                    this.polkVehicleYMME = value;
                    this.UpdatedField("PolkVehicleYMMEId");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/> occurrences of Polk discrepancies.
        /// </summary>
        [PropertyDefinition("OccurrencesOfPolkMissing", "The number of Polk discrepancies occurred.")]
        public int OccurrencesOfPolkMissing
        {
            get
            {
                this.EnsureLoaded();
                return this.occurrencesOfPolkMissing;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.occurrencesOfPolkMissing)
                {
                    this.IsObjectDirty = true;
                    this.occurrencesOfPolkMissing = value;
                    this.UpdatedField("OccurrencesOfPolkMissing");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/> occurrences of VinPower discrepancies.
        /// </summary>
        [PropertyDefinition("OccurrencesOfVinPowerMissing", "The number of VinPower discrepancies occurred.")]
        public int OccurrencesOfVinPowerMissing
        {
            get
            {
                this.EnsureLoaded();
                return this.occurrencesOfVinPowerMissing;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.occurrencesOfVinPowerMissing)
                {
                    this.IsObjectDirty = true;
                    this.occurrencesOfVinPowerMissing = value;
                    this.UpdatedField("OccurrencesOfVinPowerMissing");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/> to ignore the discrepancy.
        /// </summary>
        [PropertyDefinition("Ignore", "Ignore the discrepancy.")]
        public bool IsIgnored
        {
            get
            {
                this.EnsureLoaded();
                return this.isIgnored;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.isIgnored)
                {
                    this.IsObjectDirty = true;
                    this.isIgnored = value;
                    this.UpdatedField("IsIgnored");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/> to delete the discrepancy.
        /// </summary>
        [PropertyDefinition("Delete", "Delete the discrepancy.")]
        public bool IsDeleted
        {
            get
            {
                this.EnsureLoaded();
                return this.isDeleted;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.isDeleted)
                {
                    this.IsObjectDirty = true;
                    this.isDeleted = value;
                    this.UpdatedField("IsDeleted");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="AdminUser"/> user who ignored the discrepancy.
        /// </summary>
        [PropertyDefinition("Ignored By", "User who ignored the discrepancy.")]
        public AdminUser AdminUserSetIgnoredBy
        {
            get
            {
                this.EnsureLoaded();
                return this.adminUserSetIgnoredBy;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.adminUserSetIgnoredBy)
                {
                    this.IsObjectDirty = true;
                    this.adminUserSetIgnoredBy = value;
                    this.UpdatedField("AdminUserIdSetIgnoredBy");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="AdminUser"/> user who deleted the discrepancy.
        /// </summary>
        [PropertyDefinition("Deleted By", "User who deleted the discrepancy.")]
        public AdminUser AdminUserDeletedBy
        {
            get
            {
                this.EnsureLoaded();
                return this.adminUserDeletedBy;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.adminUserDeletedBy)
                {
                    this.IsObjectDirty = true;
                    this.adminUserDeletedBy = value;
                    this.UpdatedField("AdminUserIdDeletedBy");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/> of the discrepancy created.
        /// </summary>
        [PropertyDefinition("Created Date Time", "The created date time of the discrepancy.")]
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
        /// Gets or sets the <see cref="DateTime"/> of the discrepancy updated.
        /// </summary>
        [PropertyDefinition("Updated Date Time", "The updated date time of the discrepancy.")]
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
        /// Gets an existing discrepancy record if one exists.
        /// </summary>
        /// <param name="registry">The <see cref="Registry"/> currently in use.</param>
        /// <param name="fix">The <see cref="Fix"/>.</param>
        /// <param name="polkVehicleYMME">The <see cref="PolkVehicleYMME"/> vehicle.</param>
        /// <returns>A <see cref="FixPolkVehicleDiscrepancy"/> object.</returns>
        public static FixPolkVehicleDiscrepancy GetDiscrepancy(Registry registry, Fix fix, PolkVehicleYMME polkVehicleYMME)
        {
            FixPolkVehicleDiscrepancy fpd = null;

            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(registry.ConnectionStringDefault))
            {
                dr.ProcedureName = "FixPolkVehicleDiscrepancy_LoadByFixAndVehicle";
                dr.AddGuid("FixId", fix.Id);
                dr.AddGuid("PolkVehicleYMMEId", polkVehicleYMME.Id);

                dr.Execute();

                if (dr.Read())
                {
                    fpd = (FixPolkVehicleDiscrepancy)registry.CreateInstance(typeof(FixPolkVehicleDiscrepancy), dr.GetGuid("FixPolkVehicleDiscrepancyId"));
                    fpd.LoadPropertiesFromDataReader(dr, true);
                }
            }

            return fpd;
        }

        /// <summary>
        /// Adds a new discrepancy record or updates the occurance count if one already exists.
        /// </summary>
        /// <param name="registry">The <see cref="Registry"/> currently in use.</param>
        /// <param name="fix">The <see cref="Fix"/>.</param>
        /// <param name="polkVehicleYMME">The <see cref="PolkVehicleYMME"/> vehicle.</param>
        /// <param name="isPolkDiscrepancy">A <see cref="bool"/> indicating if the discrepancy is related to a missing Polk match.</param>
        /// <param name="isVinPowerDiscrepancy">A <see cref="bool"/> indicating if the discrepancy is related to a missing VinPower match.</param>
        public static void AddDiscrepancy(Registry registry, Fix fix, PolkVehicleYMME polkVehicleYMME, bool isPolkDiscrepancy, bool isVinPowerDiscrepancy)
        {
            FixPolkVehicleDiscrepancy fpd = GetDiscrepancy(registry, fix, polkVehicleYMME);

            if (fpd == null)
            {
                fpd = (FixPolkVehicleDiscrepancy)registry.CreateInstance(typeof(FixPolkVehicleDiscrepancy));
                fpd.Fix = fix;
                fpd.PolkVehicleYMME = polkVehicleYMME;
            }
            else
            {
                // Since we found an existing record make sure it's no longer considered deleted
                fpd.IsDeleted = false;
                fpd.AdminUserDeletedBy = null;
            }

            // Increment the occurances
            if (isPolkDiscrepancy)
            {
                fpd.OccurrencesOfPolkMissing++;
            }

            if (isVinPowerDiscrepancy)
            {
                fpd.OccurrencesOfVinPowerMissing++;
            }

            // Save
            fpd.UpdatedDateTimeUTC = DateTime.UtcNow;
            fpd.Save();
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
            dr.ProcedureName = "FixPolkVehicleDiscrepancy_Load";
            dr.AddGuid("FixPolkVehicleDiscrepancyId", this.Id);
        }

        /// <summary>
        /// Sets the properties of this object from the fields in the recordset located in the supplied <see cref="SqlDataReaderWrapper"/>.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> containing the recordset and fields used to set the properties of this object (this layer only).</param>
        protected new void SetPropertiesFromDataReader(SqlDataReaderWrapper dr)
        {
            this.fix = (Fix)dr.GetBusinessObjectBase(this.Registry, typeof(Fix), "FixId");
            this.polkVehicleYMME = (PolkVehicleYMME)dr.GetBusinessObjectBase(this.Registry, typeof(PolkVehicleYMME), "PolkVehicleYMMEId");
            this.occurrencesOfPolkMissing = dr.GetInt32("OccurrencesOfPolkMissing");
            this.occurrencesOfVinPowerMissing = dr.GetInt32("OccurrencesOfVinPowerMissing");
            this.isIgnored = dr.GetBoolean("IsIgnored");
            this.isDeleted = dr.GetBoolean("IsDeleted");
            this.adminUserSetIgnoredBy = (AdminUser)dr.GetBusinessObjectBase(this.Registry, typeof(AdminUser), "AdminUserIdSetIgnoredBy");
            this.adminUserDeletedBy = (AdminUser)dr.GetBusinessObjectBase(this.Registry, typeof(AdminUser), "AdminUserIdDeletedBy");
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
                        dr.ProcedureName = "FixPolkVehicleDiscrepancy_Create";
                        this.CreatedDateTimeUTC = DateTime.UtcNow;
                        this.UpdatedDateTimeUTC = this.CreatedDateTimeUTC;
                    }
                    else
                    {
                        dr.UpdateFields("FixPolkVehicleDiscrepancy", "FixPolkVehicleDiscrepancyId", this.updatedFields);
                    }

                    dr.AddGuid("FixPolkVehicleDiscrepancyId", this.Id);
                    dr.AddBusinessObject("FixId", this.Fix);
                    dr.AddBusinessObject("PolkVehicleYMMEId", this.PolkVehicleYMME);
                    dr.AddInt32("OccurrencesOfPolkMissing", this.OccurrencesOfPolkMissing);
                    dr.AddInt32("OccurrencesOfVinPowerMissing", this.OccurrencesOfVinPowerMissing);
                    dr.AddBoolean("IsIgnored", this.IsIgnored);
                    dr.AddBoolean("IsDeleted", this.IsDeleted);
                    dr.AddBusinessObject("AdminUserIdSetIgnoredBy", this.AdminUserSetIgnoredBy);
                    dr.AddBusinessObject("AdminUserIdDeletedBy", this.AdminUserDeletedBy);
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
                dr.ProcedureName = "FixPolkVehicleDiscrepancy_Delete";
                dr.AddGuid("FixPolkVehicleDiscrepancyId", this.Id);

                dr.ExecuteNonQuery(transaction);
            }

            transaction = base.Delete(connection, transaction);

            return transaction;
        }

        #endregion Required Methods (Load, Save, Delete, Etc)
    }
}
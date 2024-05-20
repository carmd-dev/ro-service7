﻿using Innova.Users;
using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using System;
using System.Collections.Specialized;
using System.Data.SqlClient;

namespace Innova.Parts
{
    /// <summary>
    /// The PartName object handles the business logic and data access for the specialized business object, PartName.
    /// The class inherits from <see cref="BusinessObjectBase"/> class.
    /// </summary>
    /// <example>
    /// See the examples of how to construct the PartName object.
    ///
    /// To create a new instance of a new of PartName.
    /// <code>PartName o = (PartName)this.Registry.CreateInstance(typeof(PartName));</code>
    ///
    /// To create an new instance of an existing PartName.
    /// <code>PartName o = (PartName)this.Registry.CreateInstance(typeof(PartName), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of PartName, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectBase.ConnectionString"/> to the <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable()]
    public class PartName : InnovaBusinessObjectBase
    {
        private string acesId = "";
        private string name = "";
        private string name_es = "";
        private string name_fr = "";
        private string name_zh = "";
        private AdminUser adminUserCreated;
        private AdminUser adminUserUpdated;
        private bool isActive;
        private DateTime createdDateTimeUTC = DateTime.MinValue;
        private DateTime updatedDateTimeUTC = DateTime.MinValue;

        #region Contructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new (one that does not exist, assigning a new <see cref="Guid"/> Id). PartName object.
        /// In order to create a new PartName which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <example>
        /// <code>
        /// PartName o = (PartName)Registry.CreateInstance(typeof(PartName));
        /// </code>
        /// </example>
        protected internal PartName()
            : base()
        {
            this.IsObjectCreated = true;
            this.IsObjectLoaded = true;
            this.IsObjectDirty = true;

            this.CreatedDateTimeUTC = DateTime.UtcNow;
            this.UpdatedDateTimeUTC = this.CreatedDateTimeUTC;
        }

        /// <summary>
        /// Constructor creates an existing (one that exists, and already has a <see cref="Guid"/> Id).  PartName object.
        /// In order to create an existing PartName object, which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <param name="id">The <see cref="Guid"/> or SQL uniqueidentifier indentification "Id" of the current object.</param>
        /// <example>
        /// <code>
        /// PartName o = (PartName)Registry.CreateInstance(typeof(PartName), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));
        /// </code>
        /// </example>
        protected internal PartName(Guid id)
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
        /// Gets or sets the <see cref="string"/> ACES ID of the part.
        /// </summary>
        public string ACESId
        {
            get
            {
                this.EnsureLoaded();
                return this.acesId;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.acesId)
                {
                    this.IsObjectDirty = true;
                    this.acesId = value;
                    this.UpdatedField("ACESId");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> name of the part.
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
                if (value != this.name)
                {
                    this.IsObjectDirty = true;
                    this.name = value;
                    this.UpdatedField("Name");
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> name of the part in lowercase. Used for FindBy.. lookup in collections.
        /// </summary>
        public string NameLowerCase
        {
            get
            {
                return this.Name.ToLower().Trim();
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> name of the part in Spanish.
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
                if (value != this.name_es)
                {
                    this.IsObjectDirty = true;
                    this.name_es = value;
                    this.UpdatedField("Name_es");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> name of the part in French.
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
                if (value != this.name_fr)
                {
                    this.IsObjectDirty = true;
                    this.name_fr = value;
                    this.UpdatedField("Name_fr");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> name of the part in Mandarin .
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
                if (value != this.name_zh)
                {
                    this.IsObjectDirty = true;
                    this.name_zh = value;
                    this.UpdatedField("Name_zh");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> name in the language specified in the <see cref="RuntimeInfo"/>.
        /// </summary>
        public string Name_Translated
        {
            get
            {
                this.EnsureLoaded();
                return this.RuntimeInfo.GetTranslatedValue(this.Name, this.Name_es, this.Name_fr, this.Name_zh);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="AdminUser"/> who created the record.
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
                    this.UpdatedField("AdminUserIdCreated");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="AdminUser"/> who last updated the record.
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
                    this.UpdatedField("AdminUserIdUpdated");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/>  flag indicating whether or not the part name is active.
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
        /// Gets or sets the <see cref="DateTime"/> the part was created.
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
        /// Gets or sets the <see cref="DateTime"/> the part was updated.
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
            dr.ProcedureName = "PartName_Load";
            dr.AddGuid("PartNameId", this.Id);
        }

        /// <summary>
        /// Sets the properties of this object from the fields in the recordset located in the supplied <see cref="SqlDataReaderWrapper"/>.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> containing the recordset and fields used to set the properties of this object (this layer only).</param>
        protected new void SetPropertiesFromDataReader(SqlDataReaderWrapper dr)
        {
            this.acesId = dr.GetString("ACESId");
            this.name = dr.GetString("Name");
            this.name_es = dr.GetString("Name_es");
            this.name_fr = dr.GetString("Name_fr");
            this.name_zh = dr.GetString("Name_zh");
            this.adminUserCreated = (AdminUser)dr.GetBusinessObjectBase(this.Registry, typeof(AdminUser), "AdminUserIdCreated");
            this.adminUserUpdated = (AdminUser)dr.GetBusinessObjectBase(this.Registry, typeof(AdminUser), "AdminUserIdUpdated");
            this.isActive = dr.GetBoolean("IsActive");
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
            //##Security
            bool needToEncryptName = false;
            //##Security

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
                        dr.ProcedureName = "PartName_Create";
                    }
                    else
                    {
                        dr.UpdateFields("PartName", "PartNameId", this.updatedFields);
                    }

                    dr.AddGuid("PartNameId", this.Id);
                    dr.AddNVarChar("ACESId", this.ACESId.Trim());

                    dr.AddNVarChar("Name", this.Name.Trim());
                    dr.AddNVarChar("Name_es", this.Name_es.Trim());
                    dr.AddNVarChar("Name_fr", this.Name_fr.Trim());
                    dr.AddNVarChar("Name_zh", this.Name_zh.Trim());
                    dr.AddBusinessObject("AdminUserIdCreated", this.AdminUserCreated);
                    dr.AddBusinessObject("AdminUserIdUpdated", this.AdminUserUpdated);
                    dr.AddBoolean("IsActive", this.IsActive);
                    dr.AddDateTime("CreatedDateTimeUTC", this.CreatedDateTimeUTC);
                    dr.AddDateTime("UpdatedDateTimeUTC", this.UpdatedDateTimeUTC);

                    dr.Execute(transaction);
                }

                this.IsObjectDirty = false;
            }

            //##Security
            if (needToEncryptName)
            {
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    dr.ProcedureName = "PartName_EncryptName";
                    dr.AddGuid("PartNameId", this.Id);
                    dr.AddNVarChar("Name", this.Name.Trim());
                    dr.Execute(transaction);
                }
            }
            //##Security

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
                dr.ProcedureName = "PartName_Delete";
                dr.AddGuid("PartNameId", this.Id);

                dr.ExecuteNonQuery(transaction);
            }

            transaction = base.Delete(connection, transaction);

            return transaction;
        }

        #endregion Required Methods (Load, Save, Delete, Etc)
    }
}
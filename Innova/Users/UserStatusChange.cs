using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using System;
using System.Collections.Specialized;
using System.Data.SqlClient;

namespace Innova.Users
{
    /// <summary>
    /// The UserStatusChange object handles the business logic and data access for the specialized business object, UserStatusChange.
    /// The class inherits from <see cref="BusinessObjectBase"/> class.
    /// </summary>
    /// <example>
    /// See the examples of how to construct the UserStatusChange object.
    ///
    /// To create a new instance of a new of UserStatusChange.
    /// <code>UserStatusChange o = (UserStatusChange)this.Registry.CreateInstance(typeof(UserStatusChange));</code>
    ///
    /// To create an new instance of an existing UserStatusChange.
    /// <code>UserStatusChange o = (UserStatusChange)this.Registry.CreateInstance(typeof(UserStatusChange), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of UserStatusChange, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectBase.ConnectionString"/> to the <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable()]
    public class UserStatusChange : BusinessObjectBase
    {
        private User user;
        private AdminUser adminUser;
        private UserAccountStatus userAccountStatus;
        private string comments = "";
        private DateTime createdDateTimeUTC = DateTime.MinValue;

        #region Contructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new (one that does not exist, assigning a new <see cref="Guid"/> Id). UserStatusChange object.
        /// In order to create a new UserStatusChange which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <example>
        /// <code>
        /// UserStatusChange o = (UserStatusChange)Registry.CreateInstance(typeof(UserStatusChange));
        /// </code>
        /// </example>
        protected internal UserStatusChange()
            : base()
        {
            this.IsObjectCreated = true;
            this.IsObjectLoaded = true;
            this.IsObjectDirty = true;
        }

        /// <summary>
        /// Constructor creates an existing (one that exists, and already has a <see cref="Guid"/> Id).  UserStatusChange object.
        /// In order to create an existing UserStatusChange object, which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <param name="id">The <see cref="Guid"/> or SQL uniqueidentifier indentification "Id" of the current object.</param>
        /// <example>
        /// <code>
        /// UserStatusChange o = (UserStatusChange)Registry.CreateInstance(typeof(UserStatusChange), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));
        /// </code>
        /// </example>
        protected internal UserStatusChange(Guid id)
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
        /// Gets or sets the <see cref="User"/> .
        /// </summary>
        [PropertyDefinition("User", "Account user.")]
        public User User
        {
            get
            {
                this.EnsureLoaded();
                return this.user;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.user)
                {
                    this.IsObjectDirty = true;
                    this.user = value;
                    this.UpdatedField("User");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="AdminUser"/> .
        /// </summary>
        [PropertyDefinition("AdminUser", "Admin", "Admin", "Changed By", "Admin user.")]
        public AdminUser AdminUser
        {
            get
            {
                this.EnsureLoaded();
                return this.adminUser;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.adminUser)
                {
                    this.IsObjectDirty = true;
                    this.adminUser = value;
                    this.UpdatedField("AdminUser");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="UserAccountStatus"/> .
        /// </summary>
        [PropertyDefinition("UserAccountStatus", "Status", "Status", "Status", "Account status of user.")]
        public UserAccountStatus UserAccountStatus
        {
            get
            {
                this.EnsureLoaded();
                return this.userAccountStatus;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.userAccountStatus)
                {
                    this.IsObjectDirty = true;
                    this.userAccountStatus = value;
                    this.UpdatedField("UserAccountStatus");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> .
        /// </summary>
        [PropertyDefinition("Comments", "Comments for user account.")]
        public string Comments
        {
            get
            {
                this.EnsureLoaded();
                return this.comments;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.comments)
                {
                    this.IsObjectDirty = true;
                    this.comments = value;
                    this.UpdatedField("Comments");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/> .
        /// </summary>
        [PropertyDefinition("CreatedDateTimeUTC", "Date", "Date", "Date", "Create date for user acccount.")]
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

        #endregion Public Properties

        #region Object Properties (Related Objects)

        /*****************************************************************************************
		 *
		 * Object Relationships: Add correlated Object Collections
		 *
		*******************************************************************************************/

        /* Example of collection of related child objects
		public ItemChildCollection ItemChilds
		{
			get
			{
				if(itemChilds == null)
				{
					itemChilds = new ItemChildCollection(Registry);

					//load if not a user created element
					if(!isObjectCreated)
					{
						SqlDataReaderWrappercall = new SqlProcedureCommand();
						EnsureValidId();

						call.ProcedureName = "Item_LoadItemChilds";
						call.AddGuid("ItemId", Id);

						itemChilds.Load(call, "ItemChildId", true, true);
					}
				}

				return itemChilds;
			}
		}

		//example of joined business object records in a two table scenario
		public OtherCollection Others
		{
			get
			{
				if(others == null)
				{
					others = new OtherCollection(Registry);

					//load if not a user created element
					if(!isCreated)
					{
						MfiSqlProcedureCall call = new MfiSqlProcedureCall();
						EnsureValidId();

						call.ProcedureName = "ItemsOthers_LoadCollection";
						call.AddGuid("ItemId", Id);

						others.Load(call, "OtherId");
					}
				}

				return others;
			}
		}
		*/

        #endregion Object Properties (Related Objects)

        #region Business Logic Methods

        /***********************************************************************************************
		 *
		 * Custom Business Logic
		 *
		 * **********************************************************************************************/

        /* Example of adding related items.
		// business logic add item child.
		public void AddItemChild(ItemChild itemChild)
		{
			itemChild.Item = this;
			this.ItemChilds.Add(itemChild);
		}
		// business logic remove item child
		public void RemoveItemChild(ItemChild itemChild)
		{
			this.ItemChilds.Remove(itemChild);
		}

		// business logic add item child.
		public void AddOther(Other other)
		{
			this.Others.Add(other);
		}
		// business logic remove item child
		public void RemoveOther(Other other)
		{
			this.Others.Remove(other);
		}
		*/

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
            dr.ProcedureName = "UserStatusChange_Load";
            dr.AddGuid("UserStatusChangeId", this.Id);
        }

        /// <summary>
        /// Sets the properties of this object from the fields in the recordset located in the supplied <see cref="SqlDataReaderWrapper"/>.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> containing the recordset and fields used to set the properties of this object (this layer only).</param>
        protected new void SetPropertiesFromDataReader(SqlDataReaderWrapper dr)
        {
            this.user = (User)dr.GetBusinessObjectBase(this.Registry, typeof(User), "UserId");
            if (!dr.IsDBNull("AdminUserId"))
            {
                this.adminUser = (AdminUser)dr.GetBusinessObjectBase(this.Registry, typeof(AdminUser), "AdminUserId");
            }
            this.userAccountStatus = (UserAccountStatus)dr.GetInt32("UserStatus");
            this.comments = dr.GetString("Comments");
            this.createdDateTimeUTC = dr.GetDateTime("CreatedDateTimeUTC");

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
                        dr.ProcedureName = "UserStatusChange_Create";
                        this.CreatedDateTimeUTC = DateTime.UtcNow;
                    }
                    else
                    {
                        dr.UpdateFields("UserStatusChange", "UserStatusChangeId", this.updatedFields);
                    }

                    dr.AddGuid("UserStatusChangeId", this.Id);
                    dr.AddBusinessObject("UserId", this.User);
                    dr.AddBusinessObject("AdminUserId", this.AdminUser);
                    dr.AddInt32("UserStatus", (int)this.UserAccountStatus);
                    dr.AddNText("Comments", this.Comments);
                    dr.AddDateTime("CreatedDateTimeUTC", this.CreatedDateTimeUTC);

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

            /*
			 * Example of related business objects
			 *
			// remove the removed objects
			if(itemChilds != null)
			{
				foreach(ItemChild itemChild in ItemChilds.Removed)
				{
					transaction = itemChild.Delete(connection, transaction);
				}
				// save each object in the related object
				foreach(ItemChild itemChild in ItemChilds)
				{
					transaction = itemChild.Save(connection, transaction);
				}
			}

			Example of related by a join of two tables

			// save the others object join relationship
			if(others != null && Others.IsObjectDirty)
			{
				transaction = EnsureDatabasePrepared(connection, transaction);

				//for id/joinId (2 column table) relationships
				SqlDataReaderWrapperdeleteItemsOthers = new SqlProcedureCommand();
				deleteItemsOthers.ProcedureName = "ItemsOthers_Delete";
				deleteItemsOthers.AddGuid("ItemId", this.Id);

				// create procedure call to add this Id then call the collection save
				// method, which adds the Id for itself.
				SqlDataReaderWrappersaveItemsOthers = new SqlProcedureCommand();

				saveItemsOthers.ProcedureName = "ItemsOthers_Create";
				saveItemsOthers.AddGuid("ItemId", this.Id);

				// perform the save on the related object
				transaction = Others.Save(connection, transaction, deleteItemsOthers, saveItemsOthers , "OtherId");
			}
			*/

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

            /* Example of deleting the items
			 * (This could potentially cause a lock since the property calls the lookup
			 *  with a new connection, implement a load method for the property in that case,
			 *  or created a specialied delete call which won't load the collection to loop and
			 *  delete, but in that case you won't be automatically calling the delete of the
			 *  child related items. See example below).
			 *
			// delete the child related objects
			foreach (ItemChild itemChild in ItemChilds)
			{
				transaction = itemChild.Delete(connection, transaction);
			}
			*/

            // delete this current object
            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                //remove related objects with a specialized delete.
                /*
				dr.ProcedureName = "ItemsOthers_Delete";
				dr.AddGuid("ItemId", Id);

				dr.ExecuteNonQuery(transaction);
				dr.ClearParameters();
				*/

                //delete the item
                dr.ProcedureName = "UserStatusChange_Delete";
                dr.AddGuid("UserStatusChangeId", this.Id);

                dr.ExecuteNonQuery(transaction);
            }

            transaction = base.Delete(connection, transaction);

            return transaction;
        }

        #endregion Required Methods (Load, Save, Delete, Etc)
    }
}
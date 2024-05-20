using Innova.Users;
using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using Metafuse3.NullableTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;

namespace Innova.Parts
{
    /// <summary>
    /// The Part object handles the business logic and data access for the specialized business object, Part.
    /// The class inherits from <see cref="BusinessObjectBase"/> class.
    /// </summary>
    /// <example>
    /// See the examples of how to construct the Part object.
    ///
    /// To create a new instance of a new of Part.
    /// <code>Part o = (Part)Registry.CreateInstance(typeof(Part));</code>
    ///
    /// To create an new instance of an existing Part.
    /// <code>Part o = (Part)Registry.CreateInstance(typeof(Part), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of Part, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectBase.ConnectionString"/> to the <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable()]
    [ClassDefinition("Part", "Parts", "Part", "PartId")]
    public class Part : InnovaBusinessObjectBase
    {
        // data object variables
        private PartName partName;

        private string makesString = "";
        private bool isMakesDirty = false;
        private List<string> makes;

        private string partNumber = "";
        private AdminUser adminUserCreated;
        private AdminUser adminUserUpdated;
        private AdminUser adminUserApproved;
        private bool isApproved;
        private NullableDateTime approvedDateTimeUTC = NullableDateTime.Null;
        private decimal price;
        private DateTime createdDateTimeUTC;
        private DateTime updatedDateTimeUTC;

        private NullableDecimal priceInLocalCurrency = NullableDecimal.Null;

        #region System Constructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new (one that does not exist, assigning a new <see cref="Guid"/> Id). Part object.
        /// In order to create a new Part which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <example>
        /// <code>
        /// Part o = (Part)Registry.CreateInstance(typeof(Part));
        /// </code>
        /// </example>
        protected internal Part() : base()
        {
            IsObjectCreated = true;
            IsObjectLoaded = true;
            this.IsObjectDirty = true;
        }

        /// <summary>
        /// Constructor creates an existing (one that exists, and already has a <see cref="Guid"/> Id).  Part object.
        /// In order to create an existing Part object, which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <param name="id">The <see cref="Guid"/> or SQL uniqueidentifier indentification "Id" of the current object.</param>
        /// <example>
        /// <code>
        /// Part o = (Part)Registry.CreateInstance(typeof(Part), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));
        /// </code>
        /// </example>
        protected internal Part(Guid id) : base(id)
        {
            this.id = id;
        }

        #endregion System Constructors

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

        /// <summary>
        /// Gets or sets the <see cref="PartName"/> of the part.
        /// </summary>
        [PropertyDefinition("Part Name", "Name or Description of the part.")]
        public PartName PartName
        {
            get
            {
                this.EnsureLoaded();
                return this.partName;
            }
            set
            {
                this.EnsureLoaded();
                if (this.partName != value)
                {
                    this.IsObjectDirty = true;
                    this.partName = value;

                    this.UpdatedField("PartNameId");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> part number of the part for the manufacturer.
        /// </summary>
        [PropertyDefinition("Part Number", "The manufacturer specific number that identifies the part.")]
        public string PartNumber
        {
            get
            {
                this.EnsureLoaded();
                return partNumber;
            }
            set
            {
                this.EnsureLoaded();
                if (this.partNumber != value)
                {
                    this.IsObjectDirty = true;
                    this.partNumber = value;
                    this.UpdatedField("PartNumber");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="AdminUser"/> who created the part.
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
                if (this.adminUserCreated != value)
                {
                    this.IsObjectDirty = true;
                    this.adminUserCreated = value;
                    this.UpdatedField("AdminUserIdCreated");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="AdminUser"/> who updated the part.
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
                if (this.adminUserUpdated != value)
                {
                    this.IsObjectDirty = true;
                    this.adminUserUpdated = value;
                    this.UpdatedField("AdminUserIdUpdated");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="AdminUser"/> the part was last approved or re-approved by.
        /// </summary>
        public AdminUser AdminUserApproved
        {
            get
            {
                this.EnsureLoaded();
                return this.adminUserApproved;
            }
            set
            {
                this.EnsureLoaded();
                if (this.adminUserApproved != value)
                {
                    this.IsObjectDirty = true;
                    this.adminUserApproved = value;
                    this.UpdatedField("AdminUserIdApproved");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/> flag indicating whether or not the part has been approved.
        /// </summary>
        [PropertyDefinition("Approved", "Indicates that the part has been approved.")]
        public bool IsApproved
        {
            get
            {
                this.EnsureLoaded();
                return this.isApproved;
            }
            set
            {
                this.EnsureLoaded();
                if (this.isApproved != value)
                {
                    this.IsObjectDirty = true;
                    this.isApproved = value;
                    this.UpdatedField("IsApproved");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableDateTime"/> the part was approved on or re-approved on.
        /// </summary>
        [PropertyDefinition("Approved Date", "Date and time part was approved.")]
        public NullableDateTime ApprovedDateTimeUTC
        {
            get
            {
                this.EnsureLoaded();
                return this.approvedDateTimeUTC;
            }
            set
            {
                this.EnsureLoaded();
                if (this.approvedDateTimeUTC != value)
                {
                    this.IsObjectDirty = true;
                    this.approvedDateTimeUTC = value;
                    this.UpdatedField("ApprovedDateTimeUTC");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="decimal"/>
        /// </summary>
        [PropertyDefinition("Price", "Monitary value of the part.")]
        public decimal Price
        {
            get
            {
                this.EnsureLoaded();
                return this.price;
            }
            set
            {
                this.EnsureLoaded();
                if (this.price != value)
                {
                    this.IsObjectDirty = true;
                    this.price = value;
                    this.UpdatedField("Price");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/> the part was created.
        /// </summary>
        [PropertyDefinition("Created", "Date and time the part was created.")]
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
                if (this.createdDateTimeUTC != value)
                {
                    this.IsObjectDirty = true;
                    this.createdDateTimeUTC = value;
                    this.UpdatedField("CreatedDateTimeUTC");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/> the part was last updated.
        /// </summary>
        [PropertyDefinition("Updated", "Date and time the part was last updated.")]
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
                if (this.updatedDateTimeUTC != value)
                {
                    this.IsObjectDirty = true;
                    this.updatedDateTimeUTC = value;
                    this.UpdatedField("UpdatedDateTimeUTC");
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="NullableDecimal"/> price of the part in the local currency as specified in the <see cref="RuntimeInfo"/>.
        /// </summary>
        public NullableDecimal PriceInLocalCurrency
        {
            get
            {
                // Now calculate the local curreny value if necessary.
                if (this.RuntimeInfo.CurrentCurrency != Currency.USD)
                {
                    this.priceInLocalCurrency = this.RuntimeInfo.GetLocalCurrencyValueFromUSDollars(this.Price);
                }

                return this.priceInLocalCurrency;
            }
        }

        /// <summary>
        /// Gets the <see cref="decimal"/> monetary cost of the part in the local currency if other than US, otherwise in US dollars.
        /// </summary>
        public decimal Price_Presented
        {
            get
            {
                return this.PriceInLocalCurrency.HasValue ? this.PriceInLocalCurrency.Value : this.Price;
            }
        }

        //		protected string MakesString

        #endregion Object Properties

        #region Object Properties (Related Objects)

        /*****************************************************************************************
		 *
		 * Object Relationships: Add correlated Object Collections
		 *
		*******************************************************************************************/

        /// <summary>
        /// Get the <see cref="List{T}"/> of vehicle makes that apply to this fix.
        /// NOTE: DO NOT add to this collection directly. Use the AddMake() method.
        /// </summary>
        [PropertyDefinition("Makes", "Makes of vehicles that apply to this fix.")]
        public List<string> Makes
        {
            get
            {
                if (this.makes == null)
                {
                    this.EnsureLoaded();

                    this.makes = new List<string>();

                    //load if not a user created element

                    if (!this.isObjectCreated && this.makesString != "")
                    {
                        foreach (string s in this.makesString.Split("|".ToCharArray()))
                        {
                            if (s != null && s != "")
                            {
                                this.makes.Add(s);
                            }
                        }
                    }
                }

                return this.makes;
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> makes as a comma separated string sorted alphabetically.
        /// </summary>
        public string MakesAsCommaDelimittedString
        {
            get
            {
                this.Makes.Sort();
                return String.Join(",", this.Makes);
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
        /// Builds a string list from the enumerable list spearated by a pipe "|" symbol.
        /// </summary>
        /// <param name="list"><see cref="IEnumerable"/> list.</param>
        /// <returns><see cref="string"/> of the list separated by the pipe symbol.</returns>
        private string BuildStringList(IEnumerable list)
        {
            string s = "";

            foreach (object o in list)
            {
                string os = o.ToString();

                if (!String.IsNullOrEmpty(os))
                {
                    if (s.Length > 0)
                    {
                        s += "|";
                    }
                    s += os.Trim();
                }
            }
            return s;
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
                //load the base part if user selected it.
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
            dr.ProcedureName = "Part_Load";
            dr.AddGuid("PartId", Id);
        }

        /// <summary>
        /// Sets the properties of this object from the fields in the recordset located in the supplied <see cref="SqlDataReaderWrapper"/>.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> containing the recordset and fields used to set the properties of this object (this layer only).</param>
        protected new void SetPropertiesFromDataReader(SqlDataReaderWrapper dr)
        {
            this.partName = (PartName)dr.GetBusinessObjectBase(this.Registry, typeof(PartName), "PartNameId");
            this.makesString = dr.GetString("MakesString");
            this.partNumber = dr.GetString("PartNumber").Trim();
            this.adminUserCreated = (AdminUser)dr.GetBusinessObjectBase(this.Registry, typeof(AdminUser), "AdminUserIdCreated");
            this.adminUserUpdated = (AdminUser)dr.GetBusinessObjectBase(this.Registry, typeof(AdminUser), "AdminUserIdUpdated");
            this.adminUserApproved = (AdminUser)dr.GetBusinessObjectBase(this.Registry, typeof(AdminUser), "AdminUserIdApproved");

            this.isApproved = dr.GetBoolean("IsApproved");
            this.approvedDateTimeUTC = dr.GetNullableDateTime("ApprovedDateTimeUTC");

            this.price = dr.GetDecimal("Price");
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
                    if (IsObjectCreated)
                    {
                        dr.ProcedureName = "Part_Create";
                        this.CreatedDateTimeUTC = DateTime.UtcNow;
                        this.UpdatedDateTimeUTC = this.CreatedDateTimeUTC;
                    }
                    else
                    {
                        this.UpdatedDateTimeUTC = DateTime.UtcNow;
                        dr.UpdateFields("Part", "PartId", this.updatedFields);
                    }

                    dr.AddGuid("PartId", this.Id);
                    dr.AddBusinessObject("PartNameId", this.PartName);
                    if (this.isMakesDirty)
                    {
                        this.makesString = this.BuildStringList(this.Makes);
                    }
                    dr.AddNVarChar("MakesString", this.makesString);
                    dr.AddNVarChar("PartNumber", this.PartNumber.ToUpper().Trim());
                    dr.AddBusinessObject("AdminUserIdCreated", this.AdminUserCreated);
                    dr.AddBusinessObject("AdminUserIdUpdated", this.AdminUserUpdated);

                    dr.AddBusinessObject("AdminUserIdApproved", this.AdminUserApproved);

                    dr.AddBoolean("IsApproved", this.IsApproved);
                    dr.AddDateTime("ApprovedDateTimeUTC", this.ApprovedDateTimeUTC);
                    dr.AddDecimal("Price", Math.Round(Price, 2));
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
            if (this.isMakesDirty)
            {
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    dr.ProcedureName = "Part_SaveMakes";
                    dr.AddGuid("PartId", Id);
                    dr.AddNText("MakesXmlList", Metafuse3.Xml.XmlList.ToXml(this.Makes));
                    dr.ExecuteNonQuery(transaction);
                }
                this.isMakesDirty = false;
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

            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                dr.ProcedureName = "PartMake_DeleteByPart";
                dr.AddGuid("PartId", Id);
                dr.ExecuteNonQuery(transaction);
            }

            // delete this current object
            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                //delete the part
                dr.ProcedureName = "Part_Delete";
                dr.AddGuid("PartId", Id);

                dr.ExecuteNonQuery(transaction);
            }

            transaction = base.Delete(connection, transaction);

            return transaction;
        }

        #endregion Required Object Methods (Load, Save, Delete, Etc)
    }
}
using Innova.Markets;
using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using Metafuse3.NullableTypes;
using System;
using System.Data.SqlClient;

namespace Innova.Users
{
    /// <summary>
    /// The AdminUser object handles the business logic and data access for the specialized business object, AdminUser.
    /// The class inherits from <see cref="BusinessObjectBase"/> class.
    /// </summary>
    /// <example>
    /// See the examples of how to construct the AdminUser object.
    ///
    /// To create a new instance of a new of AdminUser.
    /// <code>AdminUser o = (AdminUser)Registry.CreateInstance(typeof(AdminUser));</code>
    ///
    /// To create an new instance of an existing AdminUser.
    /// <code>AdminUser o = (AdminUser)Registry.CreateInstance(typeof(AdminUser), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of AdminUser, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectBase.ConnectionString"/> to the <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable()]
    [ClassDefinition("Admin User", "Admin Users", "Admin User", "AdminUserId")]
    public class AdminUser : InnovaBusinessObjectBase, IUser
    {
        // data object variables
        private string firstName = "";

        private string lastName = "";
        private string emailAddress = "";
        private string phoneNumber = "";
        private string password = "";
        private string address1 = "";
        private string address2 = "";
        private string city = "";
        private string state = "";
        private string postalCode = "";
        private string shippingAddress1 = "";
        private string shippingAddress2 = "";
        private string shippingCity = "";
        private string shippingState = "";
        private string shippingPostalCode = "";
        private string areasOfExpertise = "";
        private AdminUserType adminUserType;
        private bool isSystemAdministrator;
        private bool isReceivesPastDueNoFixNotifications;
        private Market market;
        private string permissions;
        private NullableDecimal validationPayRateDollarsPerHour = NullableDecimal.Null;
        private bool isActive;
        private bool isDeleted;
        private DateTime updatedDateTimeUTC;
        private DateTime createdDateTimeUTC;
        private NullableDateTime lastLoginDateTimeUTC = NullableDateTime.Null;
        private PropertyDefinitionDataSet propertyDefinitionDataSet;
        private bool enablePropertyDefinitionEditingOnForms = false;

        // User for reporting and sorting
        private int validationTestResultsTotalMinutesToComplete;

        private decimal validationTestResultsPaymentAmount;

        #region System Constructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new (one that does not exist, assigning a new <see cref="Guid"/> Id). AdminUser object.
        /// In order to create a new AdminUser which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <example>
        /// <code>
        /// AdminUser o = (AdminUser)Registry.CreateInstance(typeof(AdminUser));
        /// </code>
        /// </example>
        protected internal AdminUser() : base()
        {
            IsObjectCreated = true;
            IsObjectLoaded = true;
            this.IsObjectDirty = true;
        }

        /// <summary>
        /// Constructor creates an existing (one that exists, and already has a <see cref="Guid"/> Id).  AdminUser object.
        /// In order to create an existing AdminUser object, which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <param name="id">The <see cref="Guid"/> or SQL uniqueidentifier indentification "Id" of the current object.</param>
        /// <example>
        /// <code>
        /// AdminUser o = (AdminUser)Registry.CreateInstance(typeof(AdminUser), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));
        /// </code>
        /// </example>
        protected internal AdminUser(Guid id) : base(id)
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
                this.isObjectDirty = value;

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
        /// Gets or sets the <see cref="string"/> first name.
        /// </summary>
        [PropertyDefinition("First Name", "Admin's first name.")]
        public string FirstName
        {
            get
            {
                this.EnsureLoaded();
                return firstName;
            }
            set
            {
                this.EnsureLoaded();
                if (this.firstName != value)
                {
                    this.IsObjectDirty = true;
                    this.firstName = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> last name.
        /// </summary>
        [PropertyDefinition("Last Name", "Admin's last name.")]
        public string LastName
        {
            get
            {
                this.EnsureLoaded();
                return this.lastName;
            }
            set
            {
                this.EnsureLoaded();
                if (this.lastName != value)
                {
                    this.IsObjectDirty = true;
                    this.lastName = value;
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> name of the admin user.
        /// </summary>
        [PropertyDefinition("Name", "Admin's full first and last name.")]
        public string Name
        {
            get
            {
                return this.FirstName + " " + this.LastName;
            }
            set
            {
                string[] name = value.Split(new char[] { ' ' });
                if (name.Length > 0)
                {
                    this.FirstName = name[0];

                    if (name.Length > 1)
                    {
                        this.LastName = name[1];
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> email address.
        /// </summary>
        [PropertyDefinition("Email Address", "Admin's contact email address")]
        public string EmailAddress
        {
            get
            {
                this.EnsureLoaded();
                return this.emailAddress;
            }
            set
            {
                this.EnsureLoaded();
                if (this.emailAddress != value)
                {
                    this.IsObjectDirty = true;
                    this.emailAddress = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> phone number.
        /// </summary>
        [PropertyDefinition("Phone Number", "Admin's contact telephone number")]
        public string PhoneNumber
        {
            get
            {
                this.EnsureLoaded();
                return this.phoneNumber;
            }
            set
            {
                this.EnsureLoaded();
                if (this.phoneNumber != value)
                {
                    this.IsObjectDirty = true;
                    this.phoneNumber = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> password.
        /// </summary>
        [PropertyDefinition("Password", "Admin's password, leave blank when editing to keep password the same.")]
        public string Password
        {
            get
            {
                this.EnsureLoaded();
                return this.password;
            }
            set
            {
                this.EnsureLoaded();
                if (this.password != value)
                {
                    this.IsObjectDirty = true;
                    this.password = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        [PropertyDefinition("Address 1", "First line of admin's address.")]
        public string Address1
        {
            get
            {
                this.EnsureLoaded();
                return this.address1;
            }
            set
            {
                this.EnsureLoaded();
                if (this.address1 != value)
                {
                    this.IsObjectDirty = true;
                    this.address1 = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        [PropertyDefinition("Address 2", "Second line of admin's address")]
        public string Address2
        {
            get
            {
                this.EnsureLoaded();
                return this.address2;
            }
            set
            {
                this.EnsureLoaded();
                if (this.address2 != value)
                {
                    this.IsObjectDirty = true;
                    this.address2 = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        [PropertyDefinition("City", "City where admin is located")]
        public string City
        {
            get
            {
                this.EnsureLoaded();
                return this.city;
            }
            set
            {
                this.EnsureLoaded();
                if (this.city != value)
                {
                    this.IsObjectDirty = true;
                    this.city = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        [PropertyDefinition("State", "State where the admin is located.")]
        public string State
        {
            get
            {
                this.EnsureLoaded();
                return this.state;
            }
            set
            {
                this.EnsureLoaded();
                if (this.state != value)
                {
                    this.IsObjectDirty = true;
                    this.state = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        [PropertyDefinition("Zip/Postal Code", "The Zip code or Postal Code of the admin's location.")]
        public string PostalCode
        {
            get
            {
                this.EnsureLoaded();
                return this.postalCode;
            }
            set
            {
                this.EnsureLoaded();
                if (this.postalCode != value)
                {
                    this.IsObjectDirty = true;
                    this.postalCode = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        [PropertyDefinition("Shipping Address 1", "First line of the admin's shipping address.")]
        public string ShippingAddress1
        {
            get
            {
                this.EnsureLoaded();
                return this.shippingAddress1;
            }
            set
            {
                this.EnsureLoaded();
                if (this.shippingAddress1 != value)
                {
                    this.IsObjectDirty = true;
                    this.shippingAddress1 = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        [PropertyDefinition("Shipping Address 2", "Second line of the admin's shipping address.")]
        public string ShippingAddress2
        {
            get
            {
                this.EnsureLoaded();
                return this.shippingAddress2;
            }
            set
            {
                this.EnsureLoaded();
                if (this.shippingAddress2 != value)
                {
                    this.IsObjectDirty = true;
                    this.shippingAddress2 = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        [PropertyDefinition("City", "City of the admin's shipping address.")]
        public string ShippingCity
        {
            get
            {
                this.EnsureLoaded();
                return this.shippingCity;
            }
            set
            {
                this.EnsureLoaded();
                if (this.shippingCity != value)
                {
                    this.IsObjectDirty = true;
                    this.shippingCity = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        [PropertyDefinition("State", "State for the admin's shipping address.")]
        public string ShippingState
        {
            get
            {
                this.EnsureLoaded();
                return this.shippingState;
            }
            set
            {
                this.EnsureLoaded();
                if (this.shippingState != value)
                {
                    this.IsObjectDirty = true;
                    this.shippingState = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        [PropertyDefinition("Zip/Postal Code", "the Zip code or Postal Code of the admin's Shipping Address.")]
        public string ShippingPostalCode
        {
            get
            {
                this.EnsureLoaded();
                return this.shippingPostalCode;
            }
            set
            {
                this.EnsureLoaded();
                if (this.shippingPostalCode != value)
                {
                    this.IsObjectDirty = true;
                    this.shippingPostalCode = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        [PropertyDefinition("Areas of Expertise", "Admin's background or knowledge base.")]
        public string AreasOfExpertise
        {
            get
            {
                this.EnsureLoaded();
                return this.areasOfExpertise;
            }
            set
            {
                this.EnsureLoaded();
                if (this.areasOfExpertise != value)
                {
                    this.IsObjectDirty = true;
                    this.areasOfExpertise = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="AdminUserType"/>.
        /// </summary>
        [PropertyDefinition("Account Type", "What type of account should the admin be considered.")]
        public AdminUserType AdminUserType
        {
            get
            {
                this.EnsureLoaded();
                return this.adminUserType;
            }
            set
            {
                this.EnsureLoaded();
                if (this.adminUserType != value)
                {
                    this.IsObjectDirty = true;
                    this.adminUserType = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/> indicating if the user is a system admin.
        /// </summary>
        [PropertyDefinition("System Administrator", "Is this admin a system administrator")]
        public bool IsSystemAdministrator
        {
            get
            {
                this.EnsureLoaded();
                return this.isSystemAdministrator;
            }
            set
            {
                this.EnsureLoaded();
                if (this.isSystemAdministrator != value)
                {
                    this.IsObjectDirty = true;
                    this.isSystemAdministrator = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/> indicating if the user should receive past due notifications for no fix reports.
        /// </summary>
        [PropertyDefinition("Receive Past Due Notifications For No-Fix Reports", "Should the user receive past due notifications for no fix reports")]
        public bool IsReceivesPastDueNoFixNotifications
        {
            get
            {
                this.EnsureLoaded();
                return this.isReceivesPastDueNoFixNotifications;
            }
            set
            {
                this.EnsureLoaded();
                if (this.isReceivesPastDueNoFixNotifications != value)
                {
                    this.IsObjectDirty = true;
                    this.isReceivesPastDueNoFixNotifications = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Market"/>.
        /// </summary>
        [PropertyDefinition("Market", "The user's market.")]
        public Market Market
        {
            get
            {
                this.EnsureLoaded();
                return this.market;
            }
            set
            {
                this.EnsureLoaded();
                if (this.market != value)
                {
                    this.IsObjectDirty = true;
                    this.market = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableDecimal"/> hourly rate for completing validation tests.
        /// </summary>
        [PropertyDefinition("Validation Hourly Pay Rate", "The hourly rate for completing validation tests.")]
        public NullableDecimal ValidationPayRateDollarsPerHour
        {
            get
            {
                this.EnsureLoaded();
                return this.validationPayRateDollarsPerHour;
            }
            set
            {
                this.EnsureLoaded();
                if (this.validationPayRateDollarsPerHour != value)
                {
                    this.IsObjectDirty = true;
                    this.validationPayRateDollarsPerHour = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/> indicating if the user account is active.
        /// </summary>'
        [PropertyDefinition("Active", "Is admin currently active.")]
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
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/> indicating if the user account is deleted.
        /// </summary>
        [PropertyDefinition("Deleted", "Has the admin been removed from the system.")]
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
                if (this.isDeleted != value)
                {
                    this.IsObjectDirty = true;
                    this.isDeleted = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/> when the account was last updated.
        /// </summary>
        [PropertyDefinition("Last Updated", "When the admin was last updated.")]
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
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/> when the account was created.
        /// </summary>
        [PropertyDefinition("Created Date", "Date the admin was created.")]
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
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableDateTime"/> when the user last logged in.
        /// </summary>
        [PropertyDefinition("Last Logged In", "Date the admin last logged in.")]
        public NullableDateTime LastLoginDateTimeUTC
        {
            get
            {
                this.EnsureLoaded();
                return this.lastLoginDateTimeUTC;
            }
            set
            {
                this.EnsureLoaded();
                if (this.lastLoginDateTimeUTC != value)
                {
                    this.IsObjectDirty = true;
                    this.lastLoginDateTimeUTC = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="PropertyDefinitionDataSet"/> currently in use for this user.
        /// </summary>
        [PropertyDefinition("Property Definition Data Set", "The property definition data set currently in use for this user.")]
        public PropertyDefinitionDataSet PropertyDefinitionDataSet
        {
            get
            {
                this.EnsureLoaded();
                return this.propertyDefinitionDataSet;
            }
            set
            {
                this.EnsureLoaded();
                if (this.propertyDefinitionDataSet != value)
                {
                    this.IsObjectDirty = true;
                    this.propertyDefinitionDataSet = value;
                    this.UpdatedField("PropertyDefinitionDataSetId");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/> indicating whether or not this user user should be able to edit the property definitions while browsing the site.
        /// </summary>
        [PropertyDefinition("Enable Property Definition Editing On Forms", "Indicator that user should be able to edit the property definitions while browsing the site.")]
        public bool EnablePropertyDefinitionEditingOnForms
        {
            get
            {
                this.EnsureLoaded();
                return this.enablePropertyDefinitionEditingOnForms;
            }
            set
            {
                this.EnsureLoaded();
                if (this.enablePropertyDefinitionEditingOnForms != value)
                {
                    this.IsObjectDirty = true;
                    this.enablePropertyDefinitionEditingOnForms = value;
                    this.UpdatedField("EnablePropertyDefinitionEditingOnForms");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/> minutes of work performed completeing tests.
        /// </summary>
        public int ValidationTestResultsTotalMinutesToComplete
        {
            get
            {
                return this.validationTestResultsTotalMinutesToComplete;
            }
            set
            {
                this.validationTestResultsTotalMinutesToComplete = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="decimal"/> amount to pay the user for submitted report results.
        /// </summary>
        public decimal ValidationTestResultsPaymentAmount
        {
            get
            {
                return this.validationTestResultsPaymentAmount;
            }
            set
            {
                this.validationTestResultsPaymentAmount = value;
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

        private bool adminUserRolesUpdated = false;

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
                //load the base adminUser if user selected it.
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
            dr.ProcedureName = "AdminUser_Load";
            dr.AddGuid("AdminUserId", Id);
        }

        /// <summary>
        /// Sets the properties of this object from the fields in the recordset located in the supplied <see cref="SqlDataReaderWrapper"/>.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> containing the recordset and fields used to set the properties of this object (this layer only).</param>
        protected new void SetPropertiesFromDataReader(SqlDataReaderWrapper dr)
        {
            this.firstName = dr.GetString("FirstName");
            this.lastName = dr.GetString("LastName");
            this.emailAddress = dr.GetString("EmailAddress");
            this.phoneNumber = dr.GetString("PhoneNumber");
            this.password = dr.GetString("Password");
            this.address1 = dr.GetString("Address1");
            this.address2 = dr.GetString("Address2");
            this.city = dr.GetString("City");
            this.state = dr.GetString("State");
            this.postalCode = dr.GetString("PostalCode");
            this.shippingAddress1 = dr.GetString("ShippingAddress1");
            this.shippingAddress2 = dr.GetString("ShippingAddress2");
            this.shippingCity = dr.GetString("ShippingCity");
            this.shippingState = dr.GetString("ShippingState");
            this.shippingPostalCode = dr.GetString("ShippingPostalCode");
            this.areasOfExpertise = dr.GetString("AreasOfExpertise");
            this.adminUserType = (AdminUserType)dr.GetInt32("AdminUserType");
            this.isSystemAdministrator = dr.GetBoolean("IsSystemAdministrator");
            this.isReceivesPastDueNoFixNotifications = dr.GetBoolean("IsReceivesPastDueNoFixNotifications");
            this.market = (Market)dr.GetInt32("Market");
            this.permissions = dr.GetString("Permissions");
            this.validationPayRateDollarsPerHour = dr.GetNullableDecimal("ValidationPayRateDollarsPerHour");
            this.isActive = dr.GetBoolean("IsActive");
            this.isDeleted = dr.GetBoolean("IsDeleted");
            this.updatedDateTimeUTC = dr.GetDateTime("UpdatedDateTimeUTC");
            this.createdDateTimeUTC = dr.GetDateTime("CreatedDateTimeUTC");
            this.lastLoginDateTimeUTC = dr.GetNullableDateTime("LastLoginDateTimeUTC");
            this.propertyDefinitionDataSet = (PropertyDefinitionDataSet)dr.GetBusinessObjectBase(this.Registry, typeof(PropertyDefinitionDataSet), "PropertyDefinitionDataSetId");
            this.enablePropertyDefinitionEditingOnForms = dr.GetBoolean("EnablePropertyDefinitionEditingOnForms");

            if (!dr.IsDBNull("ValidationTestResultsTotalMinutesToComplete"))
            {
                this.validationTestResultsTotalMinutesToComplete = dr.GetInt32("ValidationTestResultsTotalMinutesToComplete");
            }

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
            if (IsObjectDirty)
            {
                transaction = EnsureDatabasePrepared(connection, transaction);

                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    if (IsObjectCreated)
                    {
                        dr.ProcedureName = "AdminUser_Create";
                        this.CreatedDateTimeUTC = DateTime.UtcNow;
                        this.UpdatedDateTimeUTC = this.CreatedDateTimeUTC;
                    }
                    else
                    {
                        dr.ProcedureName = "AdminUser_Save";
                    }

                    dr.AddGuid("AdminUserId", Id);
                    dr.AddNVarChar("FirstName", FirstName);
                    dr.AddNVarChar("LastName", LastName);
                    dr.AddNVarChar("EmailAddress", EmailAddress);
                    dr.AddNVarChar("PhoneNumber", InnovaFormatting.CleanPhoneNumber(PhoneNumber));
                    dr.AddNVarChar("Password", Password);
                    dr.AddNVarChar("Address1", this.Address1);
                    dr.AddNVarChar("Address2", this.Address2);
                    dr.AddNVarChar("City", this.City);
                    dr.AddNVarChar("State", this.State);
                    dr.AddNVarChar("PostalCode", this.PostalCode);
                    dr.AddNVarChar("ShippingAddress1", this.ShippingAddress1);
                    dr.AddNVarChar("ShippingAddress2", this.ShippingAddress2);
                    dr.AddNVarChar("ShippingCity", this.ShippingCity);
                    dr.AddNVarChar("ShippingState", this.ShippingState);
                    dr.AddNVarChar("ShippingPostalCode", this.ShippingPostalCode);
                    dr.AddNVarChar("AreasOfExpertise", this.AreasOfExpertise);
                    dr.AddInt32("AdminUserType", (int)this.AdminUserType);
                    dr.AddBoolean("IsSystemAdministrator", IsSystemAdministrator);
                    dr.AddBoolean("IsReceivesPastDueNoFixNotifications", this.IsReceivesPastDueNoFixNotifications);
                    dr.AddInt32("Market", (int)this.Market);

                    dr.AddDecimal("ValidationPayRateDollarsPerHour", this.ValidationPayRateDollarsPerHour);
                    dr.AddBoolean("IsActive", this.IsActive);
                    dr.AddBoolean("IsDeleted", this.IsDeleted);
                    dr.AddDateTime("UpdatedDateTimeUTC", this.UpdatedDateTimeUTC);
                    dr.AddDateTime("CreatedDateTimeUTC", this.CreatedDateTimeUTC);
                    dr.AddDateTime("LastLoginDateTimeUTC", this.LastLoginDateTimeUTC);
                    dr.AddBusinessObject("PropertyDefinitionDataSetId", this.PropertyDefinitionDataSet);
                    dr.AddBoolean("EnablePropertyDefinitionEditingOnForms", this.EnablePropertyDefinitionEditingOnForms);

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

            transaction = EnsureDatabasePrepared(connection, transaction);

            // delete this current object
            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                //delete the adminUser
                dr.ProcedureName = "AdminUser_Delete";
                dr.AddGuid("AdminUserId", Id);

                dr.ExecuteNonQuery(transaction);
            }

            transaction = base.Delete(connection, transaction);

            return transaction;
        }

        #endregion Required Object Methods (Load, Save, Delete, Etc)
    }
}
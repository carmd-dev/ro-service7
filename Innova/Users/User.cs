using Innova.Devices;
using Innova.DiagnosticReports;
using Innova.Markets;
using Innova.Vehicles;
using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using Metafuse3.NullableTypes;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data.SqlClient;

namespace Innova.Users
{
    /// <summary>
    /// The User object handles the business logic and data access for the specialized business object, User.
    /// The class inherits from <see cref="BusinessObjectBase"/> class.
    /// </summary>
    /// <example>
    /// See the examples of how to construct the User object.
    ///
    /// To create a new instance of a new of User.
    /// <code>User o = (User)Registry.CreateInstance(typeof(User));</code>
    ///
    /// To create an new instance of an existing User.
    /// <code>User o = (User)Registry.CreateInstance(typeof(User), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of User, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectBase.ConnectionString"/> to the <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable()]
    [ClassDefinition("User", "Users", "User", "UserId")]
    public class User : InnovaBusinessObjectBase, IUser
    {
        // data object variables
        private string salutation = "";

        private string firstName = "";
        private string lastName = "";
        private string address1 = "";
        private string address2 = "";
        private string city = "";
        private string region = "";
        private string postCode = "";
        private string country = "";
        private string emailAddress = "";
        private string password = "";
        private Market market;
        private string phoneNumber = "";
        private int additionalReports = 0;
        private int additionalVehicles = 0;
        private int additionalDTCSearches = 0;
        private int additionalVINLookups = 0;
        private int additionalOpenReports = 0;
        private int additionalDailyReports = 0;
        private UserType userType;

        //STW 2010-02-21, we need to add the property which identifies the "CarMD" user type, will need to change to the type...and the validation routine will need to be sure that we log this information
        private ExternalSystem externalSystem;

        private UserAccountStatus userAccountStatus = UserAccountStatus.Approved;
        private AdminUser userAccountStatusSetByAdminUser;
        private NullableDateTime userAccountStatusSetDateTimeUTC = NullableDateTime.Null;
        private string declineReason = "";

        private string signupPromoCode = "";

        private string emailValidationCode = "";
        private bool isEmailValidated = false;
        private NullableDateTime emailValidatedDateTimeUtc = NullableDateTime.Null;

        private string aseCertificationsString = "";
        private bool isAseCertificationsDirty = false;

        private string jobPositionsString = "";
        private bool isJobPositionsDirty = false;

        private string automobileMakesString = "";
        private bool isAutomobileMakesDirty = false;

        private string areasOfExpertiseString = "";
        private bool isAreasOfExpertiseDirty = false;

        private string aseCertificationsOther = "";
        private string shopName = "";
        private string shopAddress1 = "";
        private string shopAddress2 = "";
        private string shopCity = "";
        private string shopRegion = "";
        private string shopPostCode = "";
        private string shopEmailAddress = "";
        private string shopPhoneNumber = "";
        private string companyDescription = "";
        private string businessType = "";
        private string businessTypeOther = "";
        private string areasOfExpertiseOther = "";
        private bool isProvidesRepairServicesToPublic = false;
        private bool isHadPreviousAccount = false;
        private string automobileMakeOther = "";
        private string jobPositionOther = "";
        private int yearsExperience = 0;
        private string howHearedAboutOBDFix = "";
        private string howHearedAboutOBDFixOther = "";
        private string aboutMemberWhoReferredYou = "";
        private string whereTesterWasPurchased = "";
        private string comments = "";

        private bool showIdentityOnPostedComments = false;

        private bool isMasterTech = false;
        private string masterTechMakesString = "";
        private NullableInt32 masterTechNoFixReportsCap = NullableInt32.Null;
        private bool isMasterTechMakesDirty = false;
        private NullableDateTime masterTechNoFixReportLastAssignedDateTimeUTC = NullableDateTime.Null;

        private int dtcSearchCount = 0;
        private NullableDateTime dtcLastSearchDateTimeUTC = NullableDateTime.Null;
        private int vinLookupCount = 0;
        private NullableDateTime vinLastLookupDateTimeUTC = NullableDateTime.Null;
        private int reportCount = 0;
        private NullableDateTime reportLastCreatedDateTimeUTC = NullableDateTime.Null;

        private bool isPaymentProgramAllowed;
        private bool isPaymentProgramMember;
        private NullableDateTime paymentProgramStartDateTimeUTC = NullableDateTime.Null;

        private int score;
        private bool isActive = true;
        private DateTime createdDateTimeUTC;
        private DateTime updatedDateTimeUTC;

        private StringCollection aseCertifications;
        private StringCollection jobPositions;
        private StringCollection automobileMakes;
        private StringCollection areasOfExpertise;
        private StringCollection masterTechMakes;

        private VehicleCollection vehicles;
        private DeviceCollection devices;

        private int obdFixPaymentFixCount;
        private decimal obdFixPaymentTotal;

        private bool isInOBDPromotion;

        private bool isInternal = false;
        private PropertyDefinitionDataSet propertyDefinitionDataSet;
        private bool enablePropertyDefinitionEditingOnForms = false;

        private UserStatusChangeCollection userAccountStatusLog;
        private DiagnosticReportCollection noFixReportsAssigned;

        //#ABSStattus
        private decimal earningsLimitPerMonth;

        #region System Constructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new (one that does not exist, assigning a new <see cref="Guid"/> Id). User object.
        /// In order to create a new User which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <example>
        /// <code>
        /// User o = (User)Registry.CreateInstance(typeof(User));
        /// </code>
        /// </example>
        protected internal User() : base()
        {
            IsObjectCreated = true;
            IsObjectLoaded = true;
            this.IsObjectDirty = true;
        }

        /// <summary>
        /// Constructor creates an existing (one that exists, and already has a <see cref="Guid"/> Id).  User object.
        /// In order to create an existing User object, which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <param name="id">The <see cref="Guid"/> or SQL uniqueidentifier indentification "Id" of the current object.</param>
        /// <example>
        /// <code>
        /// User o = (User)Registry.CreateInstance(typeof(User), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));
        /// </code>
        /// </example>
        protected internal User(Guid id) : base(id)
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
        /// Gets or sets the <see cref="string"/> salutation of the user.
        /// </summary>
        [PropertyDefinition("Salutation", "Salutation of the user")]
        public string Salutation
        {
            get
            {
                this.EnsureLoaded();
                return salutation;
            }
            set
            {
                this.EnsureLoaded();
                if (salutation != value)
                {
                    this.IsObjectDirty = true;
                    salutation = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> first name of the user.
        /// </summary>
        [PropertyDefinition("First Name", "First Name", "First Name", "First", "User's First Name")]
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
                if (firstName != value)
                {
                    this.IsObjectDirty = true;
                    firstName = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> last name of the user.
        /// </summary>
        [PropertyDefinition("Last Name", "Last Name", "Last Name", "Last", "User's Last Name")]
        public string LastName
        {
            get
            {
                this.EnsureLoaded();
                return lastName;
            }
            set
            {
                this.EnsureLoaded();
                if (lastName != value)
                {
                    this.IsObjectDirty = true;
                    lastName = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> first line of the address of the user.
        /// </summary>
        [PropertyDefinition("Address 1", "First line of the user's address.")]
        public string Address1
        {
            get
            {
                this.EnsureLoaded();
                return address1;
            }
            set
            {
                this.EnsureLoaded();
                if (address1 != value)
                {
                    this.IsObjectDirty = true;
                    address1 = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> second line of the address of the user.
        /// </summary>
        [PropertyDefinition("Address 2", "Second line of the users address.")]
        public string Address2
        {
            get
            {
                this.EnsureLoaded();
                return address2;
            }
            set
            {
                this.EnsureLoaded();
                if (address2 != value)
                {
                    this.IsObjectDirty = true;
                    address2 = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> city of the user.
        /// </summary>
        [PropertyDefinition("City", "City where the user is located.")]
        public string City
        {
            get
            {
                this.EnsureLoaded();
                return city;
            }
            set
            {
                this.EnsureLoaded();
                if (city != value)
                {
                    this.IsObjectDirty = true;
                    city = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> region of the user.
        /// </summary>
        [PropertyDefinition("Region", "Region", "Region", "Region", "Region where the user is located.")]
        public string Region
        {
            get
            {
                this.EnsureLoaded();
                return region;
            }
            set
            {
                this.EnsureLoaded();
                if (region != value)
                {
                    this.IsObjectDirty = true;
                    region = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> post code of the user.
        /// </summary>
        [PropertyDefinition("Postal Code", "Postal Code where the user is located.")]
        public string PostCode
        {
            get
            {
                this.EnsureLoaded();
                return postCode;
            }
            set
            {
                this.EnsureLoaded();
                if (postCode != value)
                {
                    this.IsObjectDirty = true;
                    postCode = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> country of the user.
        /// </summary>
        [PropertyDefinition("Country", "Country where the user is located.")]
        public string Country
        {
            get
            {
                this.EnsureLoaded();
                return country;
            }
            set
            {
                this.EnsureLoaded();
                if (country != value)
                {
                    this.IsObjectDirty = true;
                    country = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> email address of the user.
        /// </summary>
        [PropertyDefinition("Email Address", "Email Address", "Email Address", "Email", "User's email address.")]
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
                    this.UpdatedField("EmailAddress");

                    if (this.emailAddress != null)
                    {
                        //set the is internal flag
                        if (this.emailAddress.ToLower().Contains("innova.com")
                            || this.emailAddress.ToLower().Contains("equus.com")
                            || this.emailAddress.ToLower().Contains("iequus.com")
                            || this.emailAddress.ToLower().Contains("carmd.com")
                            || this.emailAddress.ToLower().Contains("metafuse.com")
                            || this.emailAddress.ToLower().Contains("projectinsight.com")
                            || this.emailAddress.ToLower().Contains("projectinsight.net")
                            || this.emailAddress.ToLower().Contains("innovavietnam.com"))
                        {
                            this.IsInternal = true;
                        }
                        else
                        {
                            this.IsInternal = false;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> password of the user.
        /// </summary>
        [PropertyDefinition("Password", "Password of the user.")]
        public string Password
        {
            get
            {
                this.EnsureLoaded();
                return password;
            }
            set
            {
                this.EnsureLoaded();
                if (password != value)
                {
                    this.IsObjectDirty = true;
                    password = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Market"/>.
        /// </summary>
        [PropertyDefinition("Market", "Market", "Market", "Market", "The user's market.")]
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
        /// Gets or sets the <see cref="string"/> phone number of the user.
        /// </summary>
        [PropertyDefinition("Phone Number", "Phone Number", "Phone Number", "Phone", "User's phone number.")]
        public string PhoneNumber
        {
            get
            {
                this.EnsureLoaded();
                return phoneNumber;
            }
            set
            {
                this.EnsureLoaded();
                if (phoneNumber != value)
                {
                    this.IsObjectDirty = true;
                    phoneNumber = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/> number of additional reports the user is allowed to have.
        /// </summary>
        [PropertyDefinition("Additional Reports", "Number of additional reports the user is allowed to have.")]
        public int AdditionalReports
        {
            get
            {
                this.EnsureLoaded();
                return additionalReports;
            }
            set
            {
                this.EnsureLoaded();
                if (additionalReports != value)
                {
                    this.IsObjectDirty = true;
                    additionalReports = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/> number of additional vehicles the user is allowed to have.
        /// </summary>
        [PropertyDefinition("Additional Vehicles", "Number of additional vehicles the user is allowed to have.")]
        public int AdditionalVehicles
        {
            get
            {
                this.EnsureLoaded();
                return additionalVehicles;
            }
            set
            {
                this.EnsureLoaded();
                if (additionalVehicles != value)
                {
                    this.IsObjectDirty = true;
                    additionalVehicles = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/>
        /// </summary>
        [PropertyDefinition("Additional DTC Searches", "Number of Additional DTC Searches user is allowed to have")]
        public int AdditionalDTCSearches
        {
            get
            {
                this.EnsureLoaded();
                return additionalDTCSearches;
            }
            set
            {
                this.EnsureLoaded();
                if (additionalDTCSearches != value)
                {
                    this.IsObjectDirty = true;
                    additionalDTCSearches = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/>
        /// </summary>
        [PropertyDefinition("Additional VIN Lookups", "Number of Additional VIN lookups the user is allowed to have.")]
        public int AdditionalVINLookups
        {
            get
            {
                this.EnsureLoaded();
                return additionalVINLookups;
            }
            set
            {
                this.EnsureLoaded();
                if (additionalVINLookups != value)
                {
                    this.IsObjectDirty = true;
                    additionalVINLookups = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/>
        /// </summary>
        [PropertyDefinition("Additional Open Reports", "Number of additional open reports the user is allowed to have.")]
        public int AdditionalOpenReports
        {
            get
            {
                this.EnsureLoaded();
                return additionalOpenReports;
            }
            set
            {
                this.EnsureLoaded();
                if (additionalOpenReports != value)
                {
                    this.IsObjectDirty = true;
                    additionalOpenReports = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/>
        /// </summary>
        [PropertyDefinition("Additional Daily Reports", "Number of additional Daily Reports the user is allowed to have.")]
        public int AdditionalDailyReports
        {
            get
            {
                this.EnsureLoaded();
                return additionalDailyReports;
            }
            set
            {
                this.EnsureLoaded();
                if (additionalDailyReports != value)
                {
                    this.IsObjectDirty = true;
                    additionalDailyReports = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="UserType"/>
        /// </summary>
        [PropertyDefinition("User Type", "Type of user.")]
        public UserType UserType
        {
            get
            {
                this.EnsureLoaded();
                return this.userType;
            }
            set
            {
                this.EnsureLoaded();
                if (this.userType != value)
                {
                    this.IsObjectDirty = true;
                    this.userType = value;

                    if (this.userType == UserType.OBDFixConsultant || this.userType == UserType.OBDFixTechnicianUser)
                    {
                        this.ExternalSystem = (ExternalSystem)this.Registry.CreateInstance(typeof(ExternalSystem), ExternalSystem.EXTERNAL_SYSTEM_ID_OBDFIX);
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="ExternalSystem"/> of the external system the user type is setup for.  (If UserType equal ExternalSystem (or CarMD), then this value should be set).
        /// </summary>
        [PropertyDefinition("External System", "External System the user is originating from.")]
        public ExternalSystem ExternalSystem
        {
            get
            {
                this.EnsureLoaded();
                return this.externalSystem;
            }
            set
            {
                this.EnsureLoaded();
                if (this.externalSystem != value)
                {
                    this.IsObjectDirty = true;
                    this.externalSystem = value;
                    this.UpdatedField("UserTypeExternalId");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="UserAccountStatus"/>
        /// </summary>
        [PropertyDefinition("Account Status", "Account Status", "Account Status", "Status", "Current Status of the user's Account.")]
        public UserAccountStatus UserAccountStatus
        {
            get
            {
                this.EnsureLoaded();
                return userAccountStatus;
            }
            set
            {
                this.EnsureLoaded();
                if (userAccountStatus != value)
                {
                    this.IsObjectDirty = true;
                    userAccountStatus = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="AdminUser"/>
        /// </summary>
        [PropertyDefinition("Account Status Set By Administrator", "Current Status of the user's account set by the Administrator.")]
        public AdminUser UserAccountStatusSetByAdminUser
        {
            get
            {
                this.EnsureLoaded();
                return userAccountStatusSetByAdminUser;
            }
            set
            {
                this.EnsureLoaded();
                if (userAccountStatusSetByAdminUser != value)
                {
                    this.IsObjectDirty = true;
                    userAccountStatusSetByAdminUser = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableDateTime"/>
        /// </summary>
        [PropertyDefinition("Account Status Set Date", "When the user's account status was set.")]
        public NullableDateTime UserAccountStatusSetDateTimeUTC
        {
            get
            {
                this.EnsureLoaded();
                return userAccountStatusSetDateTimeUTC;
            }
            set
            {
                this.EnsureLoaded();
                if (userAccountStatusSetDateTimeUTC != value)
                {
                    this.IsObjectDirty = true;
                    userAccountStatusSetDateTimeUTC = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> reason for declining the account.
        /// </summary>
        [PropertyDefinition("Decline Reason", "Reason for declining the user's account.")]
        public string DeclineReason
        {
            get
            {
                this.EnsureLoaded();
                return declineReason;
            }
            set
            {
                this.EnsureLoaded();
                if (declineReason != value)
                {
                    this.IsObjectDirty = true;
                    declineReason = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> promo code that was entered during the signup process.
        /// </summary>
        [PropertyDefinition("Promo Code", "Promotional code used when user signed up.")]
        public string SignupPromoCode
        {
            get
            {
                this.EnsureLoaded();
                return this.signupPromoCode;
            }
            set
            {
                this.EnsureLoaded();
                if (this.signupPromoCode != value)
                {
                    this.IsObjectDirty = true;
                    this.signupPromoCode = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> email validation code.
        /// </summary>
        [PropertyDefinition("Email Validation Code", "Email validation code")]
        public string EmailValidationCode
        {
            get
            {
                this.EnsureLoaded();
                return emailValidationCode;
            }
            set
            {
                this.EnsureLoaded();
                if (emailValidationCode != value)
                {
                    this.IsObjectDirty = true;
                    emailValidationCode = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Boolean"/> indicating if the email address has been validated.
        /// </summary>
        [PropertyDefinition("Email Validated", "Indicator if the email address has been validated.")]
        public bool IsEmailValidated
        {
            get
            {
                this.EnsureLoaded();
                return isEmailValidated;
            }
            set
            {
                this.EnsureLoaded();
                if (isEmailValidated != value)
                {
                    this.IsObjectDirty = true;
                    isEmailValidated = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableDateTime"/> when the email address was last validated.
        /// </summary>
        [PropertyDefinition("Email Validation Time", "Date and time email was validated.")]
        public NullableDateTime EmailValidatedDateTimeUtc
        {
            get
            {
                this.EnsureLoaded();
                return emailValidatedDateTimeUtc;
            }
            set
            {
                this.EnsureLoaded();
                if (emailValidatedDateTimeUtc != value)
                {
                    this.IsObjectDirty = true;
                    emailValidatedDateTimeUtc = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        [PropertyDefinition("ASE Certifcations", "ASE Certifications")]
        public string AseCertificationsOther
        {
            get
            {
                this.EnsureLoaded();
                return aseCertificationsOther;
            }
            set
            {
                this.EnsureLoaded();
                if (aseCertificationsOther != value)
                {
                    this.IsObjectDirty = true;
                    aseCertificationsOther = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        [PropertyDefinition("Shop Name", "User's shop name.")]
        public string ShopName
        {
            get
            {
                this.EnsureLoaded();
                return shopName;
            }
            set
            {
                this.EnsureLoaded();
                if (shopName != value)
                {
                    this.IsObjectDirty = true;
                    shopName = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        [PropertyDefinition("Shop Address 1", "Address 1", "Address 1", "First line of user's shop address.")]
        public string ShopAddress1
        {
            get
            {
                this.EnsureLoaded();
                return shopAddress1;
            }
            set
            {
                this.EnsureLoaded();
                if (shopAddress1 != value)
                {
                    this.IsObjectDirty = true;
                    shopAddress1 = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        [PropertyDefinition("Shop Address 2", "Address 2", "Address 2", "Second line of the user's shop address.")]
        public string ShopAddress2
        {
            get
            {
                this.EnsureLoaded();
                return shopAddress2;
            }
            set
            {
                this.EnsureLoaded();
                if (shopAddress2 != value)
                {
                    this.IsObjectDirty = true;
                    shopAddress2 = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        [PropertyDefinition("Shop City", "City", "City", "City where user's shop is located.")]
        public string ShopCity
        {
            get
            {
                this.EnsureLoaded();
                return shopCity;
            }
            set
            {
                this.EnsureLoaded();
                if (shopCity != value)
                {
                    this.IsObjectDirty = true;
                    shopCity = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        [PropertyDefinition("Shop Region", "Region", "Region", "Region where user's shop is located.")]
        public string ShopRegion
        {
            get
            {
                this.EnsureLoaded();
                return shopRegion;
            }
            set
            {
                this.EnsureLoaded();
                if (shopRegion != value)
                {
                    this.IsObjectDirty = true;
                    shopRegion = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        [PropertyDefinition("Shop Postal Code", "Postal Code", "Postal Code", "Postal Code where the user's shop is located.")]
        public string ShopPostCode
        {
            get
            {
                this.EnsureLoaded();
                return shopPostCode;
            }
            set
            {
                this.EnsureLoaded();
                if (shopPostCode != value)
                {
                    this.IsObjectDirty = true;
                    shopPostCode = value;
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> full address formatted with HTML
        /// </summary>
        [PropertyDefinition("Shop Address", "The full shop address formatted with HTML.")]
        public string ShopFullAddressHTML
        {
            get
            {
                string compositeAddress = this.ShopAddress1;
                if (!string.IsNullOrEmpty(this.ShopAddress2))
                {
                    compositeAddress += "<br/>" + this.ShopAddress2;
                }
                compositeAddress += "<br/>" + this.ShopCity + ", " + this.ShopRegion + "&nbsp;&nbsp;" + this.ShopPostCode;
                if (this.Market != Markets.Market.US)
                {
                    compositeAddress += "<br/>" + this.Registry.GetEnumDescription(this.Market);
                }

                return compositeAddress;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        [PropertyDefinition("Shop Email Address", "Email Address", "Email Address", "Shop Email Address.")]
        public string ShopEmailAddress
        {
            get
            {
                this.EnsureLoaded();
                return shopEmailAddress;
            }
            set
            {
                this.EnsureLoaded();
                if (shopEmailAddress != value)
                {
                    this.IsObjectDirty = true;
                    shopEmailAddress = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        [PropertyDefinition("Shop Phone", "Phone Number", "Phone Number", "Shop phone number.")]
        public string ShopPhoneNumber
        {
            get
            {
                this.EnsureLoaded();
                return shopPhoneNumber;
            }
            set
            {
                this.EnsureLoaded();
                if (shopPhoneNumber != value)
                {
                    this.IsObjectDirty = true;
                    shopPhoneNumber = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        [PropertyDefinition("Company Description", "Description of user's company.")]
        public string CompanyDescription
        {
            get
            {
                this.EnsureLoaded();
                return companyDescription;
            }
            set
            {
                this.EnsureLoaded();
                if (companyDescription != value)
                {
                    this.IsObjectDirty = true;
                    companyDescription = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        [PropertyDefinition("Business Type", "Business Types", "Business Types", "Type of user's business.")]
        public string BusinessType
        {
            get
            {
                this.EnsureLoaded();
                return businessType;
            }
            set
            {
                this.EnsureLoaded();
                if (businessType != value)
                {
                    this.IsObjectDirty = true;
                    businessType = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        [PropertyDefinition("Business Type Other", "Please specify", "Please specify", "If Business type is Other, specify.")]
        public string BusinessTypeOther
        {
            get
            {
                this.EnsureLoaded();
                return businessTypeOther;
            }
            set
            {
                this.EnsureLoaded();
                if (businessTypeOther != value)
                {
                    this.IsObjectDirty = true;
                    businessTypeOther = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        [PropertyDefinition("Areas of Expertise", "User's areas of expertise.")]
        public string AreasOfExpertiseOther
        {
            get
            {
                this.EnsureLoaded();
                return areasOfExpertiseOther;
            }
            set
            {
                this.EnsureLoaded();
                if (areasOfExpertiseOther != value)
                {
                    this.IsObjectDirty = true;
                    areasOfExpertiseOther = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/>
        /// </summary>
        [PropertyDefinition("Provides Repair Services to Public", "Do you or your company, provide auto repair services to the general public?", "", "Indicator that user providers repair services to public.")]
        public bool IsProvidesRepairServicesToPublic
        {
            get
            {
                this.EnsureLoaded();
                return isProvidesRepairServicesToPublic;
            }
            set
            {
                this.EnsureLoaded();
                if (isProvidesRepairServicesToPublic != value)
                {
                    this.IsObjectDirty = true;
                    isProvidesRepairServicesToPublic = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/>
        /// </summary>
        [PropertyDefinition("Had Previous Account", "Have you had a previous account?", "", "Indiator that user has had a previous account.")]
        public bool IsHadPreviousAccount
        {
            get
            {
                this.EnsureLoaded();
                return isHadPreviousAccount;
            }
            set
            {
                this.EnsureLoaded();
                if (isHadPreviousAccount != value)
                {
                    this.IsObjectDirty = true;
                    isHadPreviousAccount = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        [PropertyDefinition("Automobile Make Other", "If Automobile make is other, specify.")]
        public string AutomobileMakeOther
        {
            get
            {
                this.EnsureLoaded();
                return automobileMakeOther;
            }
            set
            {
                this.EnsureLoaded();
                if (automobileMakeOther != value)
                {
                    this.IsObjectDirty = true;
                    automobileMakeOther = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        [PropertyDefinition("Job Position Other", "If job position is other, specify.")]
        public string JobPositionOther
        {
            get
            {
                this.EnsureLoaded();
                return jobPositionOther;
            }
            set
            {
                this.EnsureLoaded();
                if (jobPositionOther != value)
                {
                    this.IsObjectDirty = true;
                    jobPositionOther = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/>
        /// </summary>
        [PropertyDefinition("Years of Experience", "Number of years user has in experience.")]
        public int YearsExperience
        {
            get
            {
                this.EnsureLoaded();
                return yearsExperience;
            }
            set
            {
                this.EnsureLoaded();
                if (yearsExperience != value)
                {
                    this.IsObjectDirty = true;
                    yearsExperience = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        [PropertyDefinition("How heard about ODBFix", "How did you hear of ODBFix?", "", "Explain how you heard of ODBFix")]
        public string HowHearedAboutOBDFix
        {
            get
            {
                this.EnsureLoaded();
                return howHearedAboutOBDFix;
            }
            set
            {
                this.EnsureLoaded();
                if (howHearedAboutOBDFix != value)
                {
                    this.IsObjectDirty = true;
                    howHearedAboutOBDFix = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        [PropertyDefinition("Other", "Please specify", "Please Specify", "If heard of ODBFix from other source, explain.")]
        public string HowHearedAboutOBDFixOther
        {
            get
            {
                this.EnsureLoaded();
                return howHearedAboutOBDFixOther;
            }
            set
            {
                this.EnsureLoaded();
                if (howHearedAboutOBDFixOther != value)
                {
                    this.IsObjectDirty = true;
                    howHearedAboutOBDFixOther = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        [PropertyDefinition("Member who referred you", "if referred, please indicator who.")]
        public string AboutMemberWhoReferredYou
        {
            get
            {
                this.EnsureLoaded();
                return aboutMemberWhoReferredYou;
            }
            set
            {
                this.EnsureLoaded();
                if (aboutMemberWhoReferredYou != value)
                {
                    this.IsObjectDirty = true;
                    aboutMemberWhoReferredYou = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        [PropertyDefinition("Where was Tester Purchased", "Location where tester was purchased.")]
        public string WhereTesterWasPurchased
        {
            get
            {
                this.EnsureLoaded();
                return whereTesterWasPurchased;
            }
            set
            {
                this.EnsureLoaded();
                if (whereTesterWasPurchased != value)
                {
                    this.IsObjectDirty = true;
                    whereTesterWasPurchased = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        [PropertyDefinition("Comments", "Comments about service.")]
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
                if (this.comments != value)
                {
                    this.IsObjectDirty = true;
                    this.comments = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/> indicating whether or not the user should be identified on comments they post.
        /// </summary>
        [PropertyDefinition("Identify on posted Comments", "Show My Name", "Show My Name", "Indicator wheather or not the user should be identified on comments they post.")]
        public bool ShowIdentityOnPostedComments
        {
            get
            {
                this.EnsureLoaded();
                return this.showIdentityOnPostedComments;
            }
            set
            {
                this.EnsureLoaded();
                if (this.showIdentityOnPostedComments != value)
                {
                    this.IsObjectDirty = true;
                    this.showIdentityOnPostedComments = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/> indicating if the user is a MasterTech Committe Member
        /// </summary>
        [PropertyDefinition("Is Master Tech", "User is a MasterTech Committe Member", "User is a MasterTech Committe Member", "Indicator that user is a MasterTech Committe member")]
        public bool IsMasterTech
        {
            get
            {
                this.EnsureLoaded();
                return this.isMasterTech;
            }
            set
            {
                this.EnsureLoaded();
                if (this.isMasterTech != value)
                {
                    this.IsObjectDirty = true;
                    this.isMasterTech = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableInt32"/> maximum number of no fix reports that can be assigned to this user.
        /// </summary>
        [PropertyDefinition("Master Tech No Fix Reports Cap", "Max No-Fix Reports", "Max No-Fix Reports", "The Maximum number of no fix reports that can be assigned to this user.")]
        public NullableInt32 MasterTechNoFixReportsCap
        {
            get
            {
                this.EnsureLoaded();
                return this.masterTechNoFixReportsCap;
            }
            set
            {
                this.EnsureLoaded();
                if (this.masterTechNoFixReportsCap != value)
                {
                    this.IsObjectDirty = true;
                    this.masterTechNoFixReportsCap = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableDateTime"/> of when a no-fix report was last assigned to this user.
        /// </summary>
        [PropertyDefinition("Master Tech No Fix Reports Last Assigned Date", "When a no-fix report was last assigned to this user.")]
        public NullableDateTime MasterTechNoFixReportLastAssignedDateTimeUTC
        {
            get
            {
                this.EnsureLoaded();
                return this.masterTechNoFixReportLastAssignedDateTimeUTC;
            }
            set
            {
                this.EnsureLoaded();
                if (this.masterTechNoFixReportLastAssignedDateTimeUTC != value)
                {
                    this.IsObjectDirty = true;
                    this.masterTechNoFixReportLastAssignedDateTimeUTC = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableDateTime"/>
        /// </summary>
        [PropertyDefinition("DTC Last Search Date", "Last date user searched for DTC")]
        public NullableDateTime DtcLastSearchDateTimeUTC
        {
            get
            {
                this.EnsureLoaded();
                return dtcLastSearchDateTimeUTC;
            }
            set
            {
                this.EnsureLoaded();
                if (dtcLastSearchDateTimeUTC != value)
                {
                    this.IsObjectDirty = true;
                    dtcLastSearchDateTimeUTC = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableDateTime"/>
        /// </summary>
        [PropertyDefinition("VIN Last Lookup Date", "Last date user performed a VIN lookup")]
        public NullableDateTime VinLastLookupDateTimeUTC
        {
            get
            {
                this.EnsureLoaded();
                return vinLastLookupDateTimeUTC;
            }
            set
            {
                this.EnsureLoaded();
                if (vinLastLookupDateTimeUTC != value)
                {
                    this.IsObjectDirty = true;
                    vinLastLookupDateTimeUTC = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableDateTime"/>
        /// </summary>
        [PropertyDefinition("Report Last Created", "Last date user created a report")]
        public NullableDateTime ReportLastCreatedDateTimeUTC
        {
            get
            {
                this.EnsureLoaded();
                return reportLastCreatedDateTimeUTC;
            }
            set
            {
                this.EnsureLoaded();
                if (reportLastCreatedDateTimeUTC != value)
                {
                    this.IsObjectDirty = true;
                    reportLastCreatedDateTimeUTC = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/> score.
        /// </summary>
        [PropertyDefinition("Score", "User's Score")]
        public int Score
        {
            get
            {
                this.EnsureLoaded();
                return score;
            }
            set
            {
                this.EnsureLoaded();
                if (score != value)
                {
                    this.IsObjectDirty = true;
                    score = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/>
        /// </summary>
        [PropertyDefinition("Is Payment Program Allowed", "Payment Program Allowed", "Payment Program", "PPA", "Indicator that user payment program is allowed.")]
        public bool IsPaymentProgramAllowed
        {
            get
            {
                this.EnsureLoaded();
                return isPaymentProgramAllowed;
            }
            set
            {
                this.EnsureLoaded();
                if (isPaymentProgramAllowed != value)
                {
                    this.IsObjectDirty = true;
                    isPaymentProgramAllowed = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/>
        /// </summary>
        [PropertyDefinition("Is Payment Program Member", "Payment Program Member", "Payment Program", "PPM", "Indicator that user is a member of the payment program.")]
        public bool IsPaymentProgramMember
        {
            get
            {
                this.EnsureLoaded();
                return isPaymentProgramMember;
            }
            set
            {
                this.EnsureLoaded();
                if (isPaymentProgramMember != value)
                {
                    this.IsObjectDirty = true;
                    isPaymentProgramMember = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableDateTime"/>
        /// </summary>
        [PropertyDefinition("Payment", "Payment Program Start Date")]
        public NullableDateTime PaymentProgramStartDateTimeUTC
        {
            get
            {
                this.EnsureLoaded();
                return paymentProgramStartDateTimeUTC;
            }
            set
            {
                this.EnsureLoaded();
                if (paymentProgramStartDateTimeUTC != value)
                {
                    this.IsObjectDirty = true;
                    paymentProgramStartDateTimeUTC = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/>
        /// </summary>
        [PropertyDefinition("Active", "Account Is Active", "Account Is Active", "Indicator that user is currently active.")]
        public bool IsActive
        {
            get
            {
                this.EnsureLoaded();
                return isActive;
            }
            set
            {
                this.EnsureLoaded();
                if (isActive != value)
                {
                    this.IsObjectDirty = true;
                    isActive = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/> date and time that the record was created.
        /// </summary>
        [PropertyDefinition("Created Date", "Date when record was created.")]
        public DateTime CreatedDateTimeUTC
        {
            get
            {
                this.EnsureLoaded();
                return createdDateTimeUTC;
            }
            set
            {
                this.EnsureLoaded();
                if (createdDateTimeUTC != value)
                {
                    this.IsObjectDirty = true;
                    createdDateTimeUTC = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/> date and time that the record was last modified.
        /// </summary>
        [PropertyDefinition("Updated Date", "Date when record was last modified")]
        public DateTime UpdatedDateTimeUTC
        {
            get
            {
                this.EnsureLoaded();
                return updatedDateTimeUTC;
            }
            set
            {
                this.EnsureLoaded();
                if (updatedDateTimeUTC != value)
                {
                    this.IsObjectDirty = true;
                    updatedDateTimeUTC = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/> indicating if this user is entered in a OBDFix promotion.
        /// </summary>
        [PropertyDefinition("In OBD Promotion", "Indicator that user is entered in a ODBFix promotion.")]
        public bool IsInOBDPromotion
        {
            get
            {
                this.EnsureLoaded();
                return this.isInOBDPromotion;
            }
            set
            {
                this.EnsureLoaded();
                if (this.isInOBDPromotion != value)
                {
                    this.IsObjectDirty = true;
                    this.isInOBDPromotion = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/> indicating whether or not this user is an internal employee or not.
        /// </summary>
        [PropertyDefinition("Internal", "Indicator that user is an internal employee.")]
        public bool IsInternal
        {
            get
            {
                this.EnsureLoaded();
                return this.isInternal;
            }
            set
            {
                this.EnsureLoaded();
                if (this.isInternal != value)
                {
                    this.IsObjectDirty = true;
                    this.isInternal = value;
                    this.UpdatedField("IsInternal");
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
        /// Gets or sets the <see cref="string"/> first and last name.
        /// </summary>
        [PropertyDefinition("Name", "User's First Name and Last Name")]
        public string Name
        {
            get
            {
                return FirstName + " " + LastName;
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
        /// Gets a <see cref="Boolean"/> indicating if the User has a manual device associated with their account.
        /// </summary>
        [PropertyDefinition("Manual Device Exists", "If user has a manual device associated with their account.")]
        public bool IsManualDeviceExists
        {
            get
            {
                bool isManualDeviceExists = false;

                foreach (Device d in DevicesActive)
                {
                    if (d.IsManualDevice)
                    {
                        isManualDeviceExists = true;
                        break;
                    }
                }

                return isManualDeviceExists;
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> full name of the user with a list of assigned makes if the user is a Master Tech. (ex. "John Smith (Ford, Honda, Toyota)")
        /// </summary>
        public string FullNameAndAssignedMakes
        {
            get
            {
                return this.Name + (!String.IsNullOrEmpty(MasterTechMakesCommaSeparatedList) ? " (" + this.MasterTechMakesCommaSeparatedList + ")" : "");
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> comma separated list of makes assigned to user as part of being a Master Tech
        /// </summary>
        public string MasterTechMakesCommaSeparatedList
        {
            get
            {
                return this.masterTechMakesString.Replace("|", ", ");
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
        /// Gets the <see cref="StringCollection"/> of ASE Certifications for this User.
        /// NOTE: DO NOT add to this collection directly. Use the AddAseCertification() method.
        /// </summary>
        [PropertyDefinition("ASE Certifications", "The ASE certifications for this technician.")]
        public StringCollection AseCertifications
        {
            get
            {
                if (aseCertifications == null)
                {
                    aseCertifications = new StringCollection();

                    if (!isObjectCreated)
                    {
                        if (this.aseCertificationsString != "")
                        {
                            foreach (string s in this.aseCertificationsString.Split("|".ToCharArray()))
                            {
                                if (s != null && s != "" && s.ToLower() != "other")
                                {
                                    aseCertifications.Add(s);
                                }
                            }
                        }

                        if (AseCertificationsOther != "")
                        {
                            aseCertifications.Add("Other - " + AseCertificationsOther);
                        }
                    }
                }
                return aseCertifications;
            }
        }

        /// <summary>
        /// Gets the <see cref="StringCollection"/> of job positions for this User.
        /// NOTE: DO NOT add to this collection directly. Use the AddJobPosition() method.
        /// </summary>
        [PropertyDefinition("Job Positions", "The job position held by this technician.")]
        public StringCollection JobPositions
        {
            get
            {
                if (jobPositions == null)
                {
                    jobPositions = new StringCollection();

                    if (!isObjectCreated)
                    {
                        if (this.jobPositionsString != "")
                        {
                            foreach (string s in this.jobPositionsString.Split("|".ToCharArray()))
                            {
                                if (s != null && s != "" && s.ToLower() != "other")
                                {
                                    jobPositions.Add(s);
                                }
                            }
                        }

                        if (JobPositionOther != "")
                        {
                            jobPositions.Add("Other - " + JobPositionOther);
                        }
                    }
                }
                return jobPositions;
            }
        }

        /// <summary>
        /// Gets the <see cref="StringCollection"/> of Automobile Makes this User has experience working with.
        /// NOTE: DO NOT add to this collection directly. Use the AddAutomobileMake() method.
        /// </summary>
        [PropertyDefinition("Automobile Makes", "The automobile makes this technician has experience working with.")]
        public StringCollection AutomobileMakes
        {
            get
            {
                if (automobileMakes == null)
                {
                    automobileMakes = new StringCollection();

                    if (!isObjectCreated)
                    {
                        if (this.automobileMakesString != "")
                        {
                            foreach (string s in this.automobileMakesString.Split("|".ToCharArray()))
                            {
                                if (s != null && s != "" && s.ToLower() != "other")
                                {
                                    automobileMakes.Add(s);
                                }
                            }
                        }

                        if (AutomobileMakeOther != "")
                        {
                            automobileMakes.Add("Other - " + AutomobileMakeOther);
                        }
                    }
                }
                return automobileMakes;
            }
        }

        /// <summary>
        /// Gets the <see cref="StringCollection"/> of Areas Of Expertise for this User.
        /// NOTE: DO NOT add to this collection directly. Use the AddAreaOfExpertise() method.
        /// </summary>
        [PropertyDefinition("Areas Of Expertise", "The areas of expertise for this technician.")]
        public StringCollection AreasOfExpertise
        {
            get
            {
                if (areasOfExpertise == null)
                {
                    areasOfExpertise = new StringCollection();

                    if (!isObjectCreated)
                    {
                        if (this.areasOfExpertiseString != "")
                        {
                            foreach (string s in this.areasOfExpertiseString.Split("|".ToCharArray()))
                            {
                                if (s != null && s != "" && s.ToLower() != "other")
                                {
                                    areasOfExpertise.Add(s);
                                }
                            }
                        }

                        if (AreasOfExpertiseOther != "")
                        {
                            areasOfExpertise.Add("Other - " + AreasOfExpertiseOther);
                        }
                    }
                }
                return areasOfExpertise;
            }
        }

        /// <summary>
        /// Gets the <see cref="StringCollection"/> of makes for which the MasterTech user will submit fixes.
        /// NOTE: DO NOT add to this collection directly. Use the AddMasterTechMake() method.
        /// </summary>
        [PropertyDefinition("MasterTech Makes", "The makes for which the MasterTech user will submit fixes.")]
        public StringCollection MasterTechMakes
        {
            get
            {
                if (this.masterTechMakes == null)
                {
                    this.masterTechMakes = new StringCollection();

                    if (!this.isObjectCreated)
                    {
                        if (this.masterTechMakesString != "")
                        {
                            foreach (string s in this.masterTechMakesString.Split("|".ToCharArray()))
                            {
                                if (s != null && s != "" && !this.masterTechMakes.Contains(s))
                                {
                                    this.masterTechMakes.Add(s);
                                }
                            }
                        }
                    }
                }
                return this.masterTechMakes;
            }
        }

        /// <summary>
        /// Gets the <see cref="StringCollection"/> of makes for which the MasterTech user will submit fixes.
        /// NOTE: DO NOT add to this collection directly. Use the AddMasterTechMake() method.
        /// </summary>
        [PropertyDefinition("MasterTech Makes", "The makes for which the MasterTech user will submit fixes.")]
        public StringCollection MasterTechMakesLowerCase
        {
            get
            {
                StringCollection masterTechMakesLowerCase = new StringCollection();

                foreach (string s in this.MasterTechMakes)
                {
                    masterTechMakesLowerCase.Add(s.ToLower());
                }

                return masterTechMakesLowerCase;
            }
        }

        /// <summary>
        /// Gets a <see cref="UserStatusChangeCollection"/> of <see cref="UserStatusChange"/> objects which is the history of status changes made to this user.
        /// </summary>
        [PropertyDefinition("User Account Status Log", "The history of status changes made to the user account.")]
        public UserStatusChangeCollection UserAccountStatusLog
        {
            get
            {
                if (this.userAccountStatusLog == null)
                {
                    this.userAccountStatusLog = new UserStatusChangeCollection(Registry);

                    //load if not a user created element
                    if (!isObjectCreated)
                    {
                        SqlProcedureCommand call = new SqlProcedureCommand();
                        EnsureValidId();

                        call.ProcedureName = "UserStatusChange_LoadByUser";
                        call.AddGuid("UserId", Id);

                        this.userAccountStatusLog.Load(call, "UserStatusChangeId", true, true);
                    }
                }

                return this.userAccountStatusLog;
            }
        }

        /// <summary>
        /// Gets a <see cref="VehicleCollection"/> of <see cref="Vehicle"/> objects associated to the current user.
        /// </summary>
        public VehicleCollection Vehicles
        {
            get
            {
                if (vehicles == null)
                {
                    vehicles = new VehicleCollection(Registry);

                    //load if not a user created element
                    if (!isObjectCreated)
                    {
                        SqlProcedureCommand call = new SqlProcedureCommand();
                        EnsureValidId();

                        call.ProcedureName = "Vehicle_LoadByUser";
                        call.AddGuid("UserId", Id);

                        vehicles.Load(call, "VehicleId", true, true);
                    }
                }

                return vehicles;
            }
        }

        /// <summary>
        /// Gets a <see cref="VehicleCollection"/> of <see cref="Vehicle"/> objects that are currently active.
        /// </summary>
        public VehicleCollection ActiveVehicles
        {
            get
            {
                VehicleCollection activeVehicles = new VehicleCollection(Registry);

                foreach (Vehicle v in Vehicles)
                {
                    if (v.IsActive)
                    {
                        activeVehicles.Add(v);
                    }
                }

                return activeVehicles;
            }
        }

        /// <summary>
        /// Gets a <see cref="DeviceCollection"/> of ALL <see cref="Device"/> objects associated with the current <see cref="User"/>. Both
        /// active and inactive devices are contained in this collection.
        /// </summary>
        public DeviceCollection Devices
        {
            get
            {
                if (devices == null)
                {
                    devices = new DeviceCollection(Registry);

                    if (!isObjectCreated)
                    {
                        SqlProcedureCommand call = new SqlProcedureCommand();

                        call.ProcedureName = "Device_LoadByUser";
                        call.AddGuid("UserId", Id);

                        devices.Load(call, "DeviceId", true, true);
                    }
                }

                return devices;
            }
        }

        /// <summary>
        /// Gets a <see cref="DeviceCollection"/> of <see cref="Device"/> objects associated with the current <see cref="User"/> that are
        /// currently pending a transfer of ownership.
        /// </summary>
        public DeviceCollection DevicesPendingTransfer
        {
            get
            {
                DeviceCollection devicesPendingTransfer = new DeviceCollection(Registry);

                foreach (Device d in DevicesActive)
                {
                    if (d.UserRequestingTransfer != null)
                    {
                        devicesPendingTransfer.Add(d);
                    }
                }

                return devicesPendingTransfer;
            }
        }

        /// <summary>
        /// Gets a <see cref="DeviceCollection"/> of <see cref="Device"/> objects associated with the current <see cref="User"/> that are
        /// currently active.
        /// </summary>
        public DeviceCollection DevicesActive
        {
            get
            {
                DeviceCollection devicesActive = new DeviceCollection(Registry);

                foreach (Device d in Devices)
                {
                    if (d.IsActive && !d.IsManualDevice)
                    {
                        devicesActive.Add(d);
                    }
                }

                return devicesActive;
            }
        }

        /// <summary>
        /// Gets the <see cref="int"/> current DTC search count. The count will be reset to zero if the last search occurred prior to today.
        /// </summary>
        public int DtcSearchCount
        {
            get
            {
                this.EnsureLoaded();

                if (DtcLastSearchDateTimeUTC.IsNull || (!DtcLastSearchDateTimeUTC.IsNull && DateTime.Now.Date != DtcLastSearchDateTimeUTC.Value.ToLocalTime().Date))
                {
                    if (dtcSearchCount != 0)
                    {
                        dtcSearchCount = 0;
                        this.IsObjectDirty = true;
                    }
                }

                return dtcSearchCount;
            }
        }

        /// <summary>
        /// Gets the <see cref="int"/> current VIN lookup count. The count will be reset to zero if the last lookup occurred prior to today.
        /// </summary>
        public int VinLookupCount
        {
            get
            {
                this.EnsureLoaded();

                if (vinLookupCount != 0 && (VinLastLookupDateTimeUTC.IsNull || (!VinLastLookupDateTimeUTC.IsNull && DateTime.Now.Date != VinLastLookupDateTimeUTC.Value.ToLocalTime().Date)))
                {
                    vinLookupCount = 0;
                    this.IsObjectDirty = true;
                }

                return vinLookupCount;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/> current daily report creation count. The count will be reset to zero if the last report was created prior to today.
        /// </summary>
        public int ReportCount
        {
            get
            {
                this.EnsureLoaded();

                if (reportCount != 0 && (ReportLastCreatedDateTimeUTC.IsNull || (!ReportLastCreatedDateTimeUTC.IsNull && DateTime.Now.Date != ReportLastCreatedDateTimeUTC.Value.ToLocalTime().Date)))
                {
                    reportCount = 0;
                    this.IsObjectDirty = true;
                }

                return reportCount;
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
        /// Gets a collection of OBDFix users that are MasterTechs for the provided make.
        /// </summary>
        /// <param name="registry">The <see cref="Registry"/> currently in use.</param>
        /// <param name="make">OPTIONAL: The <see cref="string"/> vehicle make.</param>
        /// <returns>A <see cref="UserCollection"/> of <see cref="User"/> objects.</returns>
        public static UserCollection GetOBDFixMasterTechs(Registry registry, string make)
        {
            UserCollection users = new UserCollection(registry);

            SqlProcedureCommand call = new SqlProcedureCommand();

            if (!string.IsNullOrEmpty(make))
            {
                call.ProcedureName = "User_LoadByIsMasterTechAndMake";
                call.AddNVarChar("Make", make);
            }
            else
            {
                call.ProcedureName = "User_LoadByIsMasterTech";
            }

            users.Load(call, "UserId", true, true);

            return users;
        }

        /// <summary>
        /// Gets the manual device for this User. Will create and save a new device if a manual device does not yet exist.
        /// </summary>
        /// <returns>A <see cref="Device"/> object.</returns>
        public Device GetManualDevice()
        {
            Device device = Device.GetManualDeviceForUser(this);

            if (device == null)
            {
                device = (Device)Registry.CreateInstance(typeof(Device));
                device.ChipId = Guid.NewGuid();
                device.IsActive = true;
                device.IsManualDevice = true;
                device.IsPrimaryOwner = true;
                device.User = this;
                device.Save();
            }

            return device;
        }

        /// <summary>
        /// Gets a <see cref="Vehicle"/> with the specified VIN
        /// </summary>
        /// <param name="vin">The <see cref="string"/> VIN to search for.</param>
        /// <returns>A <see cref="Vehicle"/> with the specified VIN.</returns>
        public Vehicle GetVehicle(string vin)
        {
            Vehicle v = null;

            using (SqlDataReaderWrapper dataReader = new SqlDataReaderWrapper(this.Registry.ConnectionStringDefault))
            {
                dataReader.SqlProcedureCommand.SqlCommand.CommandTimeout = 0; //Added on 2018-01-18 12:22 PM by INNOVA Dev Team to increase the timeout wait due to a heavy load of the No-Fix Report data.

                dataReader.ProcedureName = "Vehicle_LoadByUserAndVINOrYMME";
                dataReader.AddGuid("UserId", this.Id);
                dataReader.AddNVarChar("Vin", vin);
                dataReader.Execute();

                if (dataReader.Read())
                {
                    v = (Vehicle)this.Registry.CreateInstance(typeof(Vehicle), dataReader.GetGuid("VehicleId"));
                    v.LoadPropertiesFromDataReader(dataReader, true);
                }
            }

            return v;
        }

        /// <summary>
        /// Gets a <see cref="Vehicle"/> with the specified YMME values.
        /// </summary>
        /// <param name="year">The <see cref="int"/> year of the vehicle.</param>
        /// <param name="make">The <see cref="string"/> make of the vehicle.</param>
        /// <param name="model">The <see cref="string"/> model of the vehicle.</param>
        /// <param name="engineType">The <see cref="string"/> engine type of the vehicle.</param>
        /// <returns>A <see cref="Vehicle"/> with the specified YMME values.</returns>
        public Vehicle GetVehicle(int year, string make, string model, string engineType)
        {
            Vehicle v = null;

            using (SqlDataReaderWrapper dataReader = new SqlDataReaderWrapper(this.Registry.ConnectionStringDefault))
            {
                dataReader.SqlProcedureCommand.SqlCommand.CommandTimeout = 0; //Added on 2018-01-18 12:22 PM by INNOVA Dev Team to increase the timeout wait due to a heavy load of the No-Fix Report data.

                dataReader.ProcedureName = "Vehicle_LoadByUserAndVINOrYMME";
                dataReader.AddGuid("UserId", this.Id);
                dataReader.AddInt32("Year", year);
                dataReader.AddNVarChar("Make", make);
                dataReader.AddNVarChar("Model", model);
                dataReader.AddNVarChar("EngineType", engineType);
                dataReader.Execute();

                if (dataReader.Read())
                {
                    v = (Vehicle)this.Registry.CreateInstance(typeof(Vehicle), dataReader.GetGuid("VehicleId"));
                    v.LoadPropertiesFromDataReader(dataReader, true);
                }
            }

            return v;
        }

        //Added on 2018-05-30 by Nam Lu - INNOVA Development Team
        /// <summary>
        /// Find AutoZone UserId from external reportID and AutoZone store
        /// </summary>
        public static string GetIdByAutoZoneReportId(Registry registry, string autoZoneReportId, string defaultUserId)
        {
            NullableGuid userId = null;
            using (SqlDataReaderWrapper dataReader = new SqlDataReaderWrapper(registry.ConnectionStringDefault))
            {
                dataReader.ProcedureName = "User_LoadByAutoZoneReportId";
                dataReader.AddNVarChar("AutoZoneReportId", autoZoneReportId);
                dataReader.Execute();

                if (dataReader.Read())
                {
                    userId = dataReader.GetNullableGuid("UserId");
                }
            }

            return userId == NullableGuid.Null ? defaultUserId : userId.ToString();
        }

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

                if (os != null && os != "" && os.IndexOf("Other - ") < 0)
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
            dr.ProcedureName = "User_Load";
            dr.AddGuid("UserId", Id);
        }

        /// <summary>
        /// Sets the properties of this object from the fields in the recordset located in the supplied <see cref="SqlDataReaderWrapper"/>.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> containing the recordset and fields used to set the properties of this object (this layer only).</param>
        protected new void SetPropertiesFromDataReader(SqlDataReaderWrapper dr)
        {
            this.salutation = dr.GetString("Salutation");
            this.firstName = dr.GetString("FirstName");
            this.lastName = dr.GetString("LastName");
            this.address1 = dr.GetString("Address1");
            this.address2 = dr.GetString("Address2");
            this.city = dr.GetString("City");
            this.region = dr.GetString("Region");
            this.postCode = dr.GetString("PostCode");
            this.country = dr.GetString("Country");
            this.emailAddress = dr.GetString("EmailAddress");
            this.market = (Market)dr.GetInt32("Market");
            this.password = dr.GetString("Password");
            this.phoneNumber = dr.GetString("PhoneNumber");
            this.emailValidationCode = dr.GetString("emailValidationCode");
            this.isEmailValidated = dr.GetBoolean("isEmailValidated");
            this.emailValidatedDateTimeUtc = dr.GetNullableDateTime("EmailValidatedDateTimeUTC");
            this.additionalReports = dr.GetInt32("AdditionalSessions");
            this.additionalVehicles = dr.GetInt32("AdditionalVehicles");
            this.additionalDTCSearches = dr.GetInt32("AdditionalDTCSearches");
            this.additionalVINLookups = dr.GetInt32("AdditionalVINLookups");
            this.additionalOpenReports = dr.GetInt32("AdditionalOpenReports");
            this.additionalDailyReports = dr.GetInt32("AdditionalDailyReports");
            this.userType = (UserType)int.Parse(dr.GetValue("UserType").ToString());
            this.externalSystem = (ExternalSystem)dr.GetBusinessObjectBase(this.Registry, typeof(ExternalSystem), "UserTypeExternalId");
            this.userAccountStatus = (UserAccountStatus)int.Parse(dr.GetValue("UserAccountStatus").ToString());
            if (!dr.IsDBNull("UserAccountStatusSetByAdminUserId"))
            {
                this.userAccountStatusSetByAdminUser = (AdminUser)Registry.CreateInstance(typeof(AdminUser), dr.GetGuid("UserAccountStatusSetByAdminUserId"));
            }
            userAccountStatusSetDateTimeUTC = dr.GetNullableDateTime("UserAccountStatusSetDateTimeUTC");
            declineReason = dr.GetString("DeclineReason");
            this.signupPromoCode = dr.GetString("SignupPromoCode");

            this.aseCertificationsString = dr.GetString("ASECertifications");
            this.aseCertificationsOther = dr.GetString("ASECertificationsOther");
            this.areasOfExpertiseString = dr.GetString("AreasOfExpertiseString");
            this.jobPositionsString = dr.GetString("JobPositionsString");
            this.automobileMakesString = dr.GetString("AutomobileMakesString");
            this.shopName = dr.GetString("ShopName");
            this.shopAddress1 = dr.GetString("ShopAddress1");
            this.shopAddress2 = dr.GetString("ShopAddress2");
            this.shopCity = dr.GetString("ShopCity");
            this.shopRegion = dr.GetString("ShopRegion");
            this.shopPostCode = dr.GetString("ShopPostCode");
            this.shopEmailAddress = dr.GetString("ShopEmailAddress");
            this.shopPhoneNumber = dr.GetString("ShopPhoneNumber");
            this.companyDescription = dr.GetString("CompanyDescription");
            this.businessType = dr.GetString("BusinessType");
            this.businessTypeOther = dr.GetString("BusinessTypeOther");
            this.areasOfExpertiseOther = dr.GetString("AreasOfExpertiseOther");
            this.isProvidesRepairServicesToPublic = dr.GetBoolean("IsProvidesRepairServicesToPublic");
            this.isHadPreviousAccount = dr.GetBoolean("IsHadPreviousAccount");
            this.automobileMakeOther = dr.GetString("AutomobileMakeOther");
            this.jobPositionOther = dr.GetString("JobPositionOther");
            this.yearsExperience = dr.GetInt32("YearsExperience");
            this.howHearedAboutOBDFix = dr.GetString("HowHearedAboutOBDFix");
            this.howHearedAboutOBDFixOther = dr.GetString("HowHearedAboutOBDFixOther");
            this.aboutMemberWhoReferredYou = dr.GetString("AboutMemberWhoReferredYou");
            this.whereTesterWasPurchased = dr.GetString("WhereTesterWasPurchased");
            this.comments = dr.GetString("Comments");
            this.showIdentityOnPostedComments = dr.GetBoolean("ShowIdentityOnPostedComments");
            this.isMasterTech = dr.GetBoolean("IsMasterTech");
            this.masterTechMakesString = dr.GetString("MasterTechMakesString");
            this.masterTechNoFixReportsCap = dr.GetNullableInt32("MasterTechNoFixReportsCap");
            this.masterTechNoFixReportLastAssignedDateTimeUTC = dr.GetNullableDateTime("MasterTechNoFixReportLastAssignedDateTimeUTC");
            this.dtcSearchCount = dr.GetInt32("DTCSearchCount");
            this.dtcLastSearchDateTimeUTC = dr.GetNullableDateTime("DTCLastSearchDateTimeUTC");
            this.vinLookupCount = dr.GetInt32("VINLookupCount");
            this.vinLastLookupDateTimeUTC = dr.GetNullableDateTime("VINLastLookupDateTimeUTC");
            this.reportCount = dr.GetInt32("ReportCount");
            this.reportLastCreatedDateTimeUTC = dr.GetNullableDateTime("ReportLastCreatedDateTimeUTC");
            this.score = dr.GetInt32("Score");
            this.isPaymentProgramAllowed = dr.GetBoolean("IsPaymentProgramAllowed");
            this.isPaymentProgramMember = dr.GetBoolean("IsPaymentProgramMember");
            this.paymentProgramStartDateTimeUTC = dr.GetNullableDateTime("PaymentProgramStartDateTimeUTC");
            this.isActive = dr.GetBoolean("IsActive");
            this.isInternal = dr.GetBoolean("IsInternal");
            this.createdDateTimeUTC = dr.GetDateTime("CreatedDateTimeUTC");
            this.updatedDateTimeUTC = dr.GetDateTime("UpdatedDateTimeUTC");
            this.isInOBDPromotion = dr.GetBoolean("isInOBDPromotion");
            this.propertyDefinitionDataSet = (PropertyDefinitionDataSet)dr.GetBusinessObjectBase(this.Registry, typeof(PropertyDefinitionDataSet), "PropertyDefinitionDataSetId");
            this.enablePropertyDefinitionEditingOnForms = dr.GetBoolean("EnablePropertyDefinitionEditingOnForms");

            // Attempt to set the payment values. Theses values are only output by the User_LoadBySearchAndPaymentAmounts
            // stored procedure so they are wrapped in a try/catch block for all other load calls.
            try
            {
                obdFixPaymentFixCount = dr.GetInt32("FixCount");
                obdFixPaymentTotal = dr.GetDecimal("PaymentTotal");
            }
            catch
            {
                // Do nothing.
            }

            //#ABSStatus
            if (dr.ColumnExists("EarningsLimitPerMonth"))
            {
                this.earningsLimitPerMonth = dr.GetDecimal("EarningsLimitPerMonth");
            }

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
                        dr.ProcedureName = "User_Create";
                        CreatedDateTimeUTC = DateTime.Now.ToUniversalTime();
                        UpdatedDateTimeUTC = CreatedDateTimeUTC;
                    }
                    else
                    {
                        UpdatedDateTimeUTC = DateTime.UtcNow;
                        dr.ProcedureName = "User_Save";
                    }

                    dr.AddGuid("UserId", this.Id);
                    dr.AddNVarChar("Salutation", this.Salutation);
                    dr.AddNVarChar("FirstName", this.FirstName);
                    dr.AddNVarChar("LastName", this.LastName);
                    dr.AddNVarChar("Address1", this.Address1);
                    dr.AddNVarChar("Address2", this.Address2);
                    dr.AddNVarChar("City", this.City);
                    dr.AddNVarChar("Region", this.Region);
                    dr.AddNVarChar("PostCode", this.PostCode);
                    dr.AddNVarChar("Country", this.Country);
                    dr.AddNVarChar("EmailAddress", this.EmailAddress);
                    dr.AddNVarChar("Password", this.Password);
                    dr.AddInt32("Market", (int)this.Market);
                    dr.AddNVarChar("PhoneNumber", InnovaFormatting.CleanPhoneNumber(this.PhoneNumber));
                    dr.AddNVarChar("EmailValidationCode", this.EmailValidationCode);
                    dr.AddBoolean("IsEmailValidated", this.IsEmailValidated);
                    if (!this.EmailValidatedDateTimeUtc.IsNull)
                    {
                        dr.AddDateTime("EmailValidatedDateTimeUTC", this.EmailValidatedDateTimeUtc.Value);
                    }
                    dr.AddInt32("AdditionalSessions", this.AdditionalReports);
                    dr.AddInt32("AdditionalVehicles", this.AdditionalVehicles);
                    dr.AddInt32("AdditionalDTCSearches", this.AdditionalDTCSearches);
                    dr.AddInt32("AdditionalVINLookups", this.AdditionalVINLookups);
                    dr.AddInt32("AdditionalOpenReports", this.AdditionalOpenReports);
                    dr.AddInt32("AdditionalDailyReports", this.AdditionalDailyReports);
                    dr.AddParameter(System.Data.SqlDbType.TinyInt, "UserType", (int)this.UserType);
                    dr.AddBusinessObject("UserTypeExternalId", this.ExternalSystem);
                    dr.AddParameter(System.Data.SqlDbType.TinyInt, "UserAccountStatus", (int)this.UserAccountStatus);
                    if (this.UserAccountStatusSetByAdminUser != null)
                    {
                        dr.AddGuid("UserAccountStatusSetByAdminUserId", UserAccountStatusSetByAdminUser.Id);
                    }
                    if (!this.UserAccountStatusSetDateTimeUTC.IsNull)
                    {
                        dr.AddDateTime("UserAccountStatusSetDateTimeUTC", this.UserAccountStatusSetDateTimeUTC.Value);
                    }

                    dr.AddNText("DeclineReason", this.DeclineReason);

                    dr.AddNVarChar("SignupPromoCode", this.SignupPromoCode);

                    if (this.isAseCertificationsDirty)
                    {
                        this.aseCertificationsString = BuildStringList(this.AseCertifications);
                    }
                    dr.AddNVarChar("ASECertifications", this.aseCertificationsString);

                    if (this.isAreasOfExpertiseDirty)
                    {
                        this.areasOfExpertiseString = this.BuildStringList(this.AreasOfExpertise);
                    }
                    dr.AddNVarChar("AreasOfExpertiseString", this.areasOfExpertiseString);

                    if (this.isJobPositionsDirty)
                    {
                        this.jobPositionsString = this.BuildStringList(this.JobPositions);
                    }
                    dr.AddNVarChar("JobPositionsString", this.jobPositionsString);

                    if (this.isAutomobileMakesDirty)
                    {
                        this.automobileMakesString = this.BuildStringList(this.AutomobileMakes);
                    }
                    dr.AddNVarChar("AutomobileMakesString", this.automobileMakesString);

                    dr.AddNVarChar("ASECertificationsOther", this.AseCertificationsOther);
                    dr.AddNVarChar("ShopName", this.ShopName);
                    dr.AddNVarChar("ShopAddress1", this.ShopAddress1);
                    dr.AddNVarChar("ShopAddress2", this.ShopAddress2);
                    dr.AddNVarChar("ShopCity", this.ShopCity);
                    dr.AddNVarChar("ShopRegion", this.ShopRegion);
                    dr.AddNVarChar("ShopPostCode", this.ShopPostCode);
                    dr.AddNVarChar("ShopEmailAddress", this.ShopEmailAddress);
                    dr.AddNVarChar("ShopPhoneNumber", this.ShopPhoneNumber);
                    dr.AddNVarChar("CompanyDescription", this.CompanyDescription);
                    dr.AddNVarChar("BusinessType", this.BusinessType);
                    dr.AddNVarChar("BusinessTypeOther", this.BusinessTypeOther);
                    dr.AddNVarChar("AreasOfExpertiseOther", this.AreasOfExpertiseOther);
                    dr.AddBoolean("IsProvidesRepairServicesToPublic", this.IsProvidesRepairServicesToPublic);
                    dr.AddBoolean("IsHadPreviousAccount", this.IsHadPreviousAccount);
                    dr.AddNVarChar("AutomobileMakeOther", this.AutomobileMakeOther);
                    dr.AddNVarChar("JobPositionOther", this.JobPositionOther);
                    dr.AddInt32("YearsExperience", this.YearsExperience);
                    dr.AddNVarChar("HowHearedAboutOBDFix", this.HowHearedAboutOBDFix);
                    dr.AddNVarChar("HowHearedAboutOBDFixOther", this.HowHearedAboutOBDFixOther);
                    dr.AddNVarChar("AboutMemberWhoReferredYou", this.AboutMemberWhoReferredYou);
                    dr.AddNVarChar("WhereTesterWasPurchased", this.WhereTesterWasPurchased);
                    dr.AddNVarChar("Comments", this.Comments);
                    dr.AddBoolean("ShowIdentityOnPostedComments", this.ShowIdentityOnPostedComments);

                    dr.AddBoolean("IsMasterTech", this.IsMasterTech);
                    if (this.isMasterTechMakesDirty)
                    {
                        this.masterTechMakesString = BuildStringList(this.MasterTechMakes);
                    }
                    dr.AddNVarChar("MasterTechMakesString", this.masterTechMakesString);
                    dr.AddInt32("MasterTechNoFixReportsCap", this.MasterTechNoFixReportsCap);
                    dr.AddDateTime("MasterTechNoFixReportLastAssignedDateTimeUTC", this.MasterTechNoFixReportLastAssignedDateTimeUTC);

                    dr.AddInt32("DTCSearchCount", this.DtcSearchCount);
                    dr.AddDateTime("DTCLastSearchDateTimeUTC", this.DtcLastSearchDateTimeUTC);
                    dr.AddInt32("VINLookupCount", this.VinLookupCount);
                    dr.AddDateTime("VINLastLookupDateTimeUTC", this.VinLastLookupDateTimeUTC);
                    dr.AddInt32("ReportCount", this.ReportCount);
                    dr.AddDateTime("ReportLastCreatedDateTimeUTC", this.ReportLastCreatedDateTimeUTC);
                    dr.AddInt32("Score", this.Score);
                    dr.AddBoolean("IsPaymentProgramAllowed", this.IsPaymentProgramAllowed);
                    dr.AddBoolean("IsPaymentProgramMember", this.IsPaymentProgramMember);
                    if (!this.PaymentProgramStartDateTimeUTC.IsNull)
                    {
                        dr.AddDateTime("PaymentProgramStartDateTimeUTC", this.PaymentProgramStartDateTimeUTC.Value);
                    }
                    dr.AddBoolean("IsActive", this.IsActive);
                    dr.AddBoolean("IsInternal", this.IsInternal);
                    dr.AddDateTime("CreatedDateTimeUTC", this.CreatedDateTimeUTC);
                    dr.AddDateTime("UpdatedDateTimeUTC", this.UpdatedDateTimeUTC);
                    dr.AddBoolean("IsInOBDPromotion", this.IsInOBDPromotion);
                    dr.AddBusinessObject("PropertyDefinitionDataSetId", this.PropertyDefinitionDataSet);
                    dr.AddBoolean("EnablePropertyDefinitionEditingOnForms", this.EnablePropertyDefinitionEditingOnForms);

                    dr.AddNText("FullTextSearch", FullTextSearchUtilities.GetDataPhrase(this.FirstName, this.LastName, this.Address1, this.Address2, this.City, this.Region, this.PostCode, this.Country, this.EmailAddress));

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

            if (this.devices != null)
            {
                for (int i = 0; i < this.Devices.Removed.Count; i++)
                {
                    transaction = ((Device)this.Devices.Removed[i]).Delete(connection, transaction);
                }
                for (int i = 0; i < this.Devices.Count; i++)
                {
                    transaction = this.Devices[i].Save(connection, transaction);
                }
            }

            if (this.vehicles != null)
            {
                for (int i = 0; i < this.Vehicles.Removed.Count; i++)
                {
                    transaction = ((Vehicle)this.Vehicles.Removed[i]).Delete(connection, transaction);
                }
                for (int i = 0; i < this.Vehicles.Count; i++)
                {
                    transaction = this.Vehicles[i].Save(connection, transaction);
                }
            }

            if (this.userAccountStatusLog != null)
            {
                for (int i = 0; i < this.UserAccountStatusLog.Removed.Count; i++)
                {
                    transaction = ((UserStatusChange)this.UserAccountStatusLog.Removed[i]).Delete(connection, transaction);
                }
                for (int i = 0; i < this.UserAccountStatusLog.Count; i++)
                {
                    transaction = this.UserAccountStatusLog[i].Save(connection, transaction);
                }
            }

            return transaction;
        }

        #endregion Required Object Methods (Load, Save, Delete, Etc)
    }
}
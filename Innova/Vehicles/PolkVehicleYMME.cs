using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using System;
using System.Collections.Specialized;
using System.Data.SqlClient;

namespace Innova.Vehicles
{
    /// <summary>
    /// The PolkVehicleYMME object handles the business logic and data access for the specialized business object, PolkVehicleYMME.
    /// The class inherits from <see cref="BusinessObjectBase"/> class.
    /// </summary>
    /// <example>
    /// See the examples of how to construct the PolkVehicleYMME object.
    ///
    /// To create a new instance of a new of PolkVehicleYMME.
    /// <code>PolkVehicleYMME o = (PolkVehicleYMME)this.Registry.CreateInstance(typeof(PolkVehicleYMME));</code>
    ///
    /// To create an new instance of an existing PolkVehicleYMME.
    /// <code>PolkVehicleYMME o = (PolkVehicleYMME)this.Registry.CreateInstance(typeof(PolkVehicleYMME), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of PolkVehicleYMME, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectBase.ConnectionString"/> to the <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable()]
    public class PolkVehicleYMME : BusinessObjectBase
    {
        private string vinPatternMask = "";
        private DlcLocation dlcLocation;
        private string vehicleClass = "";
        private string manufacturer = "";
        private string make = "";
        private string model = "";
        private int year;
        private string trim = "";
        private string engineVinCode = "";
        private string engineType = "";
        private string aAIA = "";
        private string transmission = "";
        private string modelImageName = "";
        private string trimImageName = "";
        private int baseMSRP;
        private string crashRating = "";
        private decimal? currentMarketValue;
        private decimal? fuelMPGCity;
        private decimal? fuelMPGCombined;
        private decimal? fuelMPGHighway;
        private int? population;
        private int? acesBaseVehicleID;
        private int? acesBodyStyleConfigID;
        private int? acesBodyTypeID;
        private string acesBodyTypeName = "";
        private int? acesEngineBaseID;
        private int? acesEngineConfigID;
        private int? acesEngineDestinationID;
        private int? acesEngineVinID;
        private int? acesMakeID;
        private int? acesModelID;
        private int? acesSubModelID;
        private int? acesVehicleEngineConfigID;
        private string acesVehicleTypeName = "";
        private int? acesVehicleTypeID;
        private int? acesYearID;
        private DateTime updatedDateTimeUTC;
        private DateTime createdDateTimeUTC;

        //For sorting collections
        private int missingPartsAndLaborCount;

        //#NewFixLogic
        /// <summary>
        /// MX/CA/PR...
        /// </summary>
        private string countryId = "";

        #region Contructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new (one that does not exist, assigning a new <see cref="Guid"/> Id). PolkVehicleYMME object.
        /// In order to create a new PolkVehicleYMME which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <example>
        /// <code>
        /// PolkVehicleYMME o = (PolkVehicleYMME)Registry.CreateInstance(typeof(PolkVehicleYMME));
        /// </code>
        /// </example>
        protected internal PolkVehicleYMME()
            : base()
        {
            this.IsObjectCreated = true;
            this.IsObjectLoaded = true;
            this.IsObjectDirty = true;
        }

        /// <summary>
        /// Constructor creates an existing (one that exists, and already has a <see cref="Guid"/> Id).  PolkVehicleYMME object.
        /// In order to create an existing PolkVehicleYMME object, which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <param name="id">The <see cref="Guid"/> or SQL uniqueidentifier indentification "Id" of the current object.</param>
        /// <example>
        /// <code>
        /// PolkVehicleYMME o = (PolkVehicleYMME)Registry.CreateInstance(typeof(PolkVehicleYMME), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));
        /// </code>
        /// </example>
        protected internal PolkVehicleYMME(Guid id)
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

        //#NewFixLogic
        /// <summary>
        /// Gets or sets the <see cref="string"/> countryId.
        /// </summary>
        [PropertyDefinition("Country ID", "Country ID.")]
        public string CountryID
        {
            get
            {
                this.EnsureLoaded();
                return this.countryId;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.countryId)
                {
                    this.IsObjectDirty = true;
                    this.countryId = value;
                    this.UpdatedField("CountryId");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> Polk VIN pattern mask.
        /// </summary>
        [PropertyDefinition("Vin Pattern Mask", "The Polk VIN pattern mask.")]
        public string VinPatternMask
        {
            get
            {
                this.EnsureLoaded();
                return this.vinPatternMask;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.vinPatternMask)
                {
                    this.IsObjectDirty = true;
                    this.vinPatternMask = value;
                    this.UpdatedField("VinPatternMask");
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> Polk VIN pattern mask lowercased.
        /// </summary>
        [PropertyDefinition("Vin Pattern Mask (lowercase)", "The Polk VIN pattern mask in lowercase.")]
        public string VinPatternMaskLowercase
        {
            get
            {
                return VinPatternMask.ToLower();
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DlcLocation"/> DLC location.
        /// </summary>
        [PropertyDefinition("DLC Location", "The DLC location.")]
        public DlcLocation DlcLocation
        {
            get
            {
                this.EnsureLoaded();
                return this.dlcLocation;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.dlcLocation)
                {
                    this.IsObjectDirty = true;
                    this.dlcLocation = value;
                    this.UpdatedField("DlcLocationId");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> vehicle class.
        /// </summary>
        [PropertyDefinition("Vehicle Class", "The vehicle class.")]
        public string VehicleClass
        {
            get
            {
                this.EnsureLoaded();
                return this.vehicleClass;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.vehicleClass)
                {
                    this.IsObjectDirty = true;
                    this.vehicleClass = value;
                    this.UpdatedField("VehicleClass");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> vehicle manufacturer.
        /// </summary>
        [PropertyDefinition("Manufacturer", "The vehicle manufacturer.")]
        public string Manufacturer
        {
            get
            {
                this.EnsureLoaded();
                return this.manufacturer;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.manufacturer)
                {
                    this.IsObjectDirty = true;
                    this.manufacturer = value;
                    this.UpdatedField("Manufacturer");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> vehicle make.
        /// </summary>
        [PropertyDefinition("Make", "The vehicle make.")]
        public string Make
        {
            get
            {
                this.EnsureLoaded();
                return this.make;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.make)
                {
                    this.IsObjectDirty = true;
                    this.make = value;
                    this.UpdatedField("Make");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> vehicle model.
        /// </summary>
        [PropertyDefinition("Model", "The vehicle model.")]
        public string Model
        {
            get
            {
                this.EnsureLoaded();
                return this.model;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.model)
                {
                    this.IsObjectDirty = true;
                    this.model = value;
                    this.UpdatedField("Model");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/> vehicle year.
        /// </summary>
        [PropertyDefinition("Year", "The vehicle year.")]
        public int Year
        {
            get
            {
                this.EnsureLoaded();
                return this.year;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.year)
                {
                    this.IsObjectDirty = true;
                    this.year = value;
                    this.UpdatedField("Year");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> vehicle trim.
        /// </summary>
        [PropertyDefinition("Trim", "The vehicle trim.")]
        public string Trim
        {
            get
            {
                this.EnsureLoaded();
                return this.trim;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.trim)
                {
                    this.IsObjectDirty = true;
                    this.trim = value;
                    this.UpdatedField("Trim");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> vehicle engine VIN code.
        /// </summary>
        [PropertyDefinition("Engine VIN Code", "The vehicle engine VIN code.")]
        public string EngineVinCode
        {
            get
            {
                this.EnsureLoaded();
                return this.engineVinCode;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.engineVinCode)
                {
                    this.IsObjectDirty = true;
                    this.engineVinCode = value;
                    this.UpdatedField("EngineVinCode");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> vehicle engine type.
        /// </summary>
        [PropertyDefinition("Engine Type", "The vehicle engine type.")]
        public string EngineType
        {
            get
            {
                this.EnsureLoaded();
                return this.engineType;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.engineType)
                {
                    this.IsObjectDirty = true;
                    this.engineType = value;
                    this.UpdatedField("EngineType");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> vehicle AAIA.
        /// </summary>
        [PropertyDefinition("AAIA", "The vehicle AAIA.")]
        public string AAIA
        {
            get
            {
                this.EnsureLoaded();
                return this.aAIA;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.aAIA)
                {
                    this.IsObjectDirty = true;
                    this.aAIA = value;
                    this.UpdatedField("AAIA");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> vehicle transmission.
        /// </summary>
        [PropertyDefinition("Transmission", "The vehicle transmission.")]
        public string Transmission
        {
            get
            {
                this.EnsureLoaded();
                return this.transmission;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.transmission)
                {
                    this.IsObjectDirty = true;
                    this.transmission = value;
                    this.UpdatedField("Transmission");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> name of the model-specific image.
        /// </summary>
        [PropertyDefinition("Model Image", "The name of the model-specific image.")]
        public string ModelImageName
        {
            get
            {
                this.EnsureLoaded();
                return this.modelImageName;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.modelImageName)
                {
                    this.IsObjectDirty = true;
                    this.modelImageName = value;
                    this.UpdatedField("ModelImageName");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> name of the trim-specific image.
        /// </summary>
        [PropertyDefinition("Trim Image", "The name of the trim-specific image.")]
        public string TrimImageName
        {
            get
            {
                this.EnsureLoaded();
                return this.trimImageName;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.trimImageName)
                {
                    this.IsObjectDirty = true;
                    this.trimImageName = value;
                    this.UpdatedField("TrimImageName");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/> base MSRP.
        /// </summary>
        [PropertyDefinition("Base MSRP", "The base MSRP.")]
        public int BaseMSRP
        {
            get
            {
                this.EnsureLoaded();
                return this.baseMSRP;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.baseMSRP)
                {
                    this.IsObjectDirty = true;
                    this.baseMSRP = value;
                    this.UpdatedField("BaseMSRP");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> crash rating.
        /// </summary>
        [PropertyDefinition("Crash Rating", "The crash rating.")]
        public string CrashRating
        {
            get
            {
                this.EnsureLoaded();
                return this.crashRating;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.crashRating)
                {
                    this.IsObjectDirty = true;
                    this.crashRating = value;
                    this.UpdatedField("CrashRating");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="decimal"/> current market value.
        /// </summary>
        [PropertyDefinition("Crash Rating", "The current market value.")]
        public decimal? CurrentMarketValue
        {
            get
            {
                this.EnsureLoaded();
                return this.currentMarketValue;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.currentMarketValue)
                {
                    this.IsObjectDirty = true;
                    this.currentMarketValue = value;
                    this.UpdatedField("CurrentMarketValue");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="decimal"/> MPG rating for city driving.
        /// </summary>
        [PropertyDefinition("MPG (City)", "The MPG rating for city driving.")]
        public decimal? FuelMPGCity
        {
            get
            {
                this.EnsureLoaded();
                return this.fuelMPGCity;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.fuelMPGCity)
                {
                    this.IsObjectDirty = true;
                    this.fuelMPGCity = value;
                    this.UpdatedField("FuelMPGCity");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="decimal"/> MPG rating for city and highway driving combined.
        /// </summary>
        [PropertyDefinition("MPG (Combined)", "The MPG rating for city and highway driving combined.")]
        public decimal? FuelMPGCombined
        {
            get
            {
                this.EnsureLoaded();
                return this.fuelMPGCombined;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.fuelMPGCombined)
                {
                    this.IsObjectDirty = true;
                    this.fuelMPGCombined = value;
                    this.UpdatedField("FuelMPGCombined");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="decimal"/> MPG rating for highway driving.
        /// </summary>
        [PropertyDefinition("MPG (Highway)", "The MPG rating for highway driving.")]
        public decimal? FuelMPGHighway
        {
            get
            {
                this.EnsureLoaded();
                return this.fuelMPGHighway;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.fuelMPGHighway)
                {
                    this.IsObjectDirty = true;
                    this.fuelMPGHighway = value;
                    this.UpdatedField("FuelMPGHighway");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/> population.
        /// </summary>
        [PropertyDefinition("Population", "The population.")]
        public int? Population
        {
            get
            {
                this.EnsureLoaded();
                return this.population;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.population)
                {
                    this.IsObjectDirty = true;
                    this.population = value;
                    this.UpdatedField("Population");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/> ACES Base Vehicle ID
        /// </summary>
        [PropertyDefinition("ACES Base Vehicle ID", "The ACES Base Vehicle ID.")]
        public int? ACESBaseVehicleID
        {
            get
            {
                this.EnsureLoaded();
                return this.acesBaseVehicleID;
            }
            set
            {
                this.EnsureLoaded();
                if (this.acesBaseVehicleID != value)
                {
                    this.IsObjectDirty = true;
                    this.acesBaseVehicleID = value;
                    this.UpdatedField("ACESBaseVehicleID");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/> ACES Body Style Config ID
        /// </summary>
        [PropertyDefinition("ACES Body Style Config ID", "The ACES Body Style Config ID.")]
        public int? ACESBodyStyleConfigID
        {
            get
            {
                this.EnsureLoaded();
                return this.acesBodyStyleConfigID;
            }
            set
            {
                this.EnsureLoaded();
                if (this.acesBodyStyleConfigID != value)
                {
                    this.IsObjectDirty = true;
                    this.acesBodyStyleConfigID = value;
                    this.UpdatedField("ACESBodyStyleConfigID");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/> ACES Body Type ID
        /// </summary>
        [PropertyDefinition("ACES Body Type ID", "The ACES Body Type ID.")]
        public int? ACESBodyTypeID
        {
            get
            {
                this.EnsureLoaded();
                return this.acesBodyTypeID;
            }
            set
            {
                this.EnsureLoaded();
                if (this.acesBodyTypeID != value)
                {
                    this.IsObjectDirty = true;
                    this.acesBodyTypeID = value;
                    this.UpdatedField("ACESBodyTypeID");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> ACES Body Type Name
        /// </summary>
        [PropertyDefinition("ACES Body Type Name", "The ACES Body Type Name.")]
        public string ACESBodyTypeName
        {
            get
            {
                this.EnsureLoaded();
                return this.acesBodyTypeName;
            }
            set
            {
                this.EnsureLoaded();
                if (this.acesBodyTypeName != value)
                {
                    this.IsObjectDirty = true;
                    this.acesBodyTypeName = value;
                    this.UpdatedField("ACESBodyTypeName");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/> ACES Engine Base ID
        /// </summary>
        [PropertyDefinition("ACES Engine Base ID", "The ACES Engine Base ID.")]
        public int? ACESEngineBaseID
        {
            get
            {
                this.EnsureLoaded();
                return this.acesEngineBaseID;
            }
            set
            {
                this.EnsureLoaded();
                if (this.acesEngineBaseID != value)
                {
                    this.IsObjectDirty = true;
                    this.acesEngineBaseID = value;
                    this.UpdatedField("ACESEngineBaseID");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/> ACES Engine Config ID
        /// </summary>
        [PropertyDefinition("ACES Engine Config ID", "The ACES Engine Config ID.")]
        public int? ACESEngineConfigID
        {
            get
            {
                this.EnsureLoaded();
                return this.acesEngineConfigID;
            }
            set
            {
                this.EnsureLoaded();
                if (this.acesEngineConfigID != value)
                {
                    this.IsObjectDirty = true;
                    this.acesEngineConfigID = value;
                    this.UpdatedField("ACESEngineConfigID");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/> ACES Engine Destination ID
        /// </summary>
        [PropertyDefinition("ACES Engine Destination ID", "The ACES Engine Destination ID.")]
        public int? ACESEngineDestinationID
        {
            get
            {
                this.EnsureLoaded();
                return this.acesEngineDestinationID;
            }
            set
            {
                this.EnsureLoaded();
                if (this.acesEngineDestinationID != value)
                {
                    this.IsObjectDirty = true;
                    this.acesEngineDestinationID = value;
                    this.UpdatedField("ACESEngineDestinationID");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/> ACES Engine VIN ID
        /// </summary>
        [PropertyDefinition("ACES Engine VIN ID", "The ACES Engine VIN ID.")]
        public int? ACESEngineVinID
        {
            get
            {
                this.EnsureLoaded();
                return this.acesEngineVinID;
            }
            set
            {
                this.EnsureLoaded();
                if (this.acesEngineVinID != value)
                {
                    this.IsObjectDirty = true;
                    this.acesEngineVinID = value;
                    this.UpdatedField("ACESEngineVinID");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/> ACES Make ID
        /// </summary>
        [PropertyDefinition("ACES Make ID", "The ACES Make ID.")]
        public int? ACESMakeID
        {
            get
            {
                this.EnsureLoaded();
                return this.acesMakeID;
            }
            set
            {
                this.EnsureLoaded();
                if (this.acesMakeID != value)
                {
                    this.IsObjectDirty = true;
                    this.acesMakeID = value;
                    this.UpdatedField("ACESMakeID");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/> ACES Model ID
        /// </summary>
        [PropertyDefinition("ACES Model ID", "The ACES Model ID.")]
        public int? ACESModelID
        {
            get
            {
                this.EnsureLoaded();
                return this.acesModelID;
            }
            set
            {
                this.EnsureLoaded();
                if (this.acesModelID != value)
                {
                    this.IsObjectDirty = true;
                    this.acesModelID = value;
                    this.UpdatedField("ACESModelID");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/> ACES SubModel ID
        /// </summary>
        [PropertyDefinition("ACES SubModel ID", "The ACES SubModel ID.")]
        public int? ACESSubModelID
        {
            get
            {
                this.EnsureLoaded();
                return this.acesSubModelID;
            }
            set
            {
                this.EnsureLoaded();
                if (this.acesSubModelID != value)
                {
                    this.IsObjectDirty = true;
                    this.acesSubModelID = value;
                    this.UpdatedField("ACESSubModelID");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/> ACES Vehicle Engine Config ID
        /// </summary>
        [PropertyDefinition("ACES Vehicle Engine Config ID", "The ACES Vehicle Engine Config ID.")]
        public int? ACESVehicleEngineConfigID
        {
            get
            {
                this.EnsureLoaded();
                return this.acesVehicleEngineConfigID;
            }
            set
            {
                this.EnsureLoaded();
                if (this.acesVehicleEngineConfigID != value)
                {
                    this.IsObjectDirty = true;
                    this.acesVehicleEngineConfigID = value;
                    this.UpdatedField("ACESVehicleEngineConfigID");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> ACES Vehicle Type Name
        /// </summary>
        [PropertyDefinition("ACES Vehicle Type Name", "The ACES Vehicle Type Name.")]
        public string ACESVehicleTypeName
        {
            get
            {
                this.EnsureLoaded();
                return this.acesVehicleTypeName;
            }
            set
            {
                this.EnsureLoaded();
                if (this.acesVehicleTypeName != value)
                {
                    this.IsObjectDirty = true;
                    this.acesVehicleTypeName = value;
                    this.UpdatedField("ACESVehicleTypeName");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/> ACES Vehicle Type ID
        /// </summary>
        [PropertyDefinition("ACES Vehicle Type ID", "The ACES Vehicle Type ID.")]
        public int? ACESVehicleTypeID
        {
            get
            {
                this.EnsureLoaded();
                return this.acesVehicleTypeID;
            }
            set
            {
                this.EnsureLoaded();
                if (this.acesVehicleTypeID != value)
                {
                    this.IsObjectDirty = true;
                    this.acesVehicleTypeID = value;
                    this.UpdatedField("ACESVehicleTypeID");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/> ACES Year ID
        /// </summary>
        [PropertyDefinition("ACES Year ID", "The ACES Year ID.")]
        public int? ACESYearID
        {
            get
            {
                this.EnsureLoaded();
                return this.acesYearID;
            }
            set
            {
                this.EnsureLoaded();
                if (this.acesYearID != value)
                {
                    this.IsObjectDirty = true;
                    this.acesYearID = value;
                    this.UpdatedField("ACESYearID");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/> created date/time.
        /// </summary>
        [PropertyDefinition("Created date/time.", "The created date/time.")]
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
        /// Gets or sets the <see cref="DateTime"/> last updated date/time..
        /// </summary>
        [PropertyDefinition("Updated date/time.", "The last updated date/time.")]
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
        /// Gets or sets the <see cref="int"/> missing parts and labor count.
        /// READ ONLY. Used for sorting.
        /// </summary>
        [PropertyDefinition("Missing Part/Labor Count", "The number of fixes for this vehicle that are missing parts and labor.")]
        public int MissingPartsAndLaborCount
        {
            get
            {
                return this.missingPartsAndLaborCount;
            }
            set
            {
                this.missingPartsAndLaborCount = value;
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
        /// Gets the URL to the model image.
        /// </summary>
        /// <param name="urlRoot">The URL root.</param>
        /// <returns>The URL to the model image.</returns>
        public string GetModelImageUrl(string urlRoot)
        {
            if (String.IsNullOrWhiteSpace(this.ModelImageName))
            {
                return "";
            }
            else
            {
                if (!urlRoot.EndsWith("/"))
                {
                    urlRoot += "/";
                }

                string makeName = this.Make.ToLower();
                makeName = makeName.Replace(" ", "_");
                makeName = makeName.Replace("-", "_");

                string modelName = this.Model.ToLower();
                modelName = modelName.Replace(" ", "_");

                modelName = modelName.Replace("/", "_");

                return urlRoot + this.ModelImageName.ToUpper() + ".jpg";
            }
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
            dr.ProcedureName = "PolkVehicleYMME_Load";
            dr.AddGuid("PolkVehicleYMMEId", this.Id);
        }

        /// <summary>
        /// Sets the properties of this object from the fields in the recordset located in the supplied <see cref="SqlDataReaderWrapper"/>.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> containing the recordset and fields used to set the properties of this object (this layer only).</param>
        protected new void SetPropertiesFromDataReader(SqlDataReaderWrapper dr)
        {
            this.vinPatternMask = dr.GetString("VinPatternMask");
            this.dlcLocation = (DlcLocation)dr.GetBusinessObjectBase(this.Registry, typeof(DlcLocation), "DlcLocationId");
            this.vehicleClass = dr.GetString("VehicleClass");
            this.manufacturer = dr.GetString("Manufacturer");
            this.make = dr.GetString("Make");
            this.model = dr.GetString("Model");
            this.year = dr.GetInt32("Year");
            this.trim = dr.GetString("Trim");
            this.engineVinCode = dr.GetString("EngineVinCode");
            this.engineType = dr.GetString("EngineType");
            this.aAIA = dr.GetString("AAIA");
            this.transmission = dr.GetString("Transmission");
            this.modelImageName = dr.GetString("ModelImageName");
            this.trimImageName = dr.GetString("TrimImageName");

            if (!dr.IsDBNull("BaseMSRP"))
            {
                this.baseMSRP = dr.GetInt32("BaseMSRP");
            }
            if (!dr.IsDBNull("CrashRating"))
            {
                this.crashRating = dr.GetString("CrashRating");
            }
            if (!dr.IsDBNull("CurrentMarketValue"))
            {
                this.currentMarketValue = dr.GetDecimal("CurrentMarketValue");
            }
            if (!dr.IsDBNull("FuelMPGCity"))
            {
                this.fuelMPGCity = dr.GetDecimal("FuelMPGCity");
            }
            if (!dr.IsDBNull("FuelMPGCombined"))
            {
                this.fuelMPGCombined = dr.GetDecimal("FuelMPGCombined");
            }
            if (!dr.IsDBNull("FuelMPGHighway"))
            {
                this.fuelMPGHighway = dr.GetDecimal("FuelMPGHighway");
            }
            if (!dr.IsDBNull("Population"))
            {
                this.population = dr.GetInt32("Population");
            }
            if (!dr.IsDBNull("ACESBaseVehicleID"))
            {
                this.acesBaseVehicleID = dr.GetInt32("ACESBaseVehicleID");
            }
            if (!dr.IsDBNull("ACESBodyStyleConfigID"))
            {
                this.acesBodyStyleConfigID = dr.GetInt32("ACESBodyStyleConfigID");
            }
            if (!dr.IsDBNull("ACESBodyTypeID"))
            {
                this.acesBodyTypeID = dr.GetInt32("ACESBodyTypeID");
            }
            if (!dr.IsDBNull("ACESBodyTypeName"))
            {
                this.acesBodyTypeName = dr.GetString("ACESBodyTypeName");
            }
            if (!dr.IsDBNull("ACESEngineBaseID"))
            {
                this.acesEngineBaseID = dr.GetInt32("ACESEngineBaseID");
            }
            if (!dr.IsDBNull("ACESEngineConfigID"))
            {
                this.acesEngineConfigID = dr.GetInt32("ACESEngineConfigID");
            }
            if (!dr.IsDBNull("ACESEngineDestinationID"))
            {
                this.acesEngineDestinationID = dr.GetInt32("ACESEngineDestinationID");
            }
            if (!dr.IsDBNull("ACESEngineVinID"))
            {
                this.acesEngineVinID = dr.GetInt32("ACESEngineVinID");
            }
            if (!dr.IsDBNull("ACESMakeID"))
            {
                this.acesMakeID = dr.GetInt32("ACESMakeID");
            }
            if (!dr.IsDBNull("ACESModelID"))
            {
                this.acesModelID = dr.GetInt32("ACESModelID");
            }
            if (!dr.IsDBNull("ACESSubModelID"))
            {
                this.acesSubModelID = dr.GetInt32("ACESSubModelID");
            }
            if (!dr.IsDBNull("ACESVehicleEngineConfigID"))
            {
                this.acesVehicleEngineConfigID = dr.GetInt32("ACESVehicleEngineConfigID");
            }
            if (!dr.IsDBNull("ACESVehicleTypeName"))
            {
                this.acesVehicleTypeName = dr.GetString("ACESVehicleTypeName");
            }
            if (!dr.IsDBNull("ACESVehicleTypeID"))
            {
                this.acesVehicleTypeID = dr.GetInt32("ACESVehicleTypeID");
            }
            if (!dr.IsDBNull("ACESYearID"))
            {
                this.acesYearID = dr.GetInt32("ACESYearID");
            }
            this.updatedDateTimeUTC = dr.GetDateTime("UpdatedDateTimeUTC");
            this.createdDateTimeUTC = dr.GetDateTime("CreatedDateTimeUTC");
            if (dr.ColumnExists("CountryId")) //#NewFixLogic
            {
                this.countryId = dr.GetString("CountryID");
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
            if (this.IsObjectDirty)
            {
                transaction = this.EnsureDatabasePrepared(connection, transaction);

                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    if (this.IsObjectCreated)
                    {
                        dr.ProcedureName = "PolkVehicleYMME_Create";
                    }
                    else
                    {
                        dr.UpdateFields("PolkVehicleYMME", "PolkVehicleYMMEId", this.updatedFields);
                    }

                    dr.AddGuid("PolkVehicleYMMEId", this.Id);
                    dr.AddNVarChar("VinPatternMask", this.VinPatternMask);
                    dr.AddBusinessObject("DlcLocationId", this.DlcLocation);
                    dr.AddNVarChar("VehicleClass", this.VehicleClass);
                    dr.AddNVarChar("Manufacturer", this.Manufacturer);
                    dr.AddNVarChar("Make", this.Make);
                    dr.AddNVarChar("Model", this.Model);
                    dr.AddInt32("Year", this.Year);
                    dr.AddNVarChar("Trim", this.Trim);
                    dr.AddNVarChar("EngineVinCode", this.EngineVinCode);
                    dr.AddNVarChar("EngineType", this.EngineType);
                    dr.AddNVarChar("AAIA", this.AAIA);
                    dr.AddNVarChar("Transmission", this.Transmission);
                    dr.AddNVarChar("ModelImageName", this.ModelImageName);
                    dr.AddNVarChar("TrimImageName", this.TrimImageName);
                    dr.AddInt32("BaseMSRP", this.BaseMSRP);
                    dr.AddNVarChar("CrashRating", this.crashRating);
                    dr.AddDecimal("CurrentMarketValue", this.currentMarketValue);
                    dr.AddDecimal("FuelMPGCity", this.fuelMPGCity);
                    dr.AddDecimal("FuelMPGCombined", this.fuelMPGCombined);
                    dr.AddDecimal("FuelMPGHighway", this.fuelMPGHighway);
                    dr.AddInt32("Population", this.population);
                    dr.AddInt32("ACESBaseVehicleID", this.ACESBaseVehicleID);
                    dr.AddInt32("ACESBodyStyleConfigID", this.ACESBodyStyleConfigID);
                    dr.AddInt32("ACESBodyTypeID", this.ACESBodyTypeID);
                    dr.AddNVarChar("ACESBodyTypeName", this.ACESBodyTypeName);
                    dr.AddInt32("ACESEngineBaseID", this.ACESEngineBaseID);
                    dr.AddInt32("ACESEngineConfigID", this.ACESEngineConfigID);
                    dr.AddInt32("ACESEngineDestinationID", this.ACESEngineDestinationID);
                    dr.AddInt32("ACESEngineVinID", this.ACESEngineVinID);
                    dr.AddInt32("ACESMakeID", this.ACESMakeID);
                    dr.AddInt32("ACESModelID", this.ACESModelID);
                    dr.AddInt32("ACESSubModelID", this.ACESSubModelID);
                    dr.AddInt32("ACESVehicleEngineConfigID", this.ACESVehicleEngineConfigID);
                    dr.AddNVarChar("ACESVehicleTypeName", this.ACESVehicleTypeName);
                    dr.AddInt32("ACESVehicleTypeID", this.ACESVehicleTypeID);
                    dr.AddInt32("ACESYearID", this.ACESYearID);
                    dr.AddDateTime("UpdatedDateTimeUTC", this.updatedDateTimeUTC);
                    dr.AddDateTime("CreatedDateTimeUTC", this.createdDateTimeUTC);

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

        #endregion Required Methods (Load, Save, Delete, Etc)
    }
}
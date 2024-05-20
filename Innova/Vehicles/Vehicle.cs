using Innova.ScheduleMaintenance;
using Innova.Users;
using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using Metafuse3.NullableTypes;
using System;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;

namespace Innova.Vehicles
{
    /// <summary>
    /// The Vehicle object handles the business logic and data access for the specialized business object, Vehicle.
    /// The class inherits from <see cref="BusinessObjectBase"/> class.
    /// </summary>
    /// <example>
    /// See the examples of how to construct the Vehicle object.
    ///
    /// To create a new instance of a new of Vehicle.
    /// <code>Vehicle o = (Vehicle)Registry.CreateInstance(typeof(Vehicle));</code>
    ///
    /// To create an new instance of an existing Vehicle.
    /// <code>Vehicle o = (Vehicle)Registry.CreateInstance(typeof(Vehicle), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of Vehicle, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectBase.ConnectionString"/> to the <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable()]
    [ClassDefinition("Vehicle", "Vehicles", "Vehicle", "VehicleId")]
    public class Vehicle : BusinessObjectBase
    {
        // data object variables
        private User user;

        private ScheduleMaintenancePlan scheduleMaintenancePlan;
        private PolkVehicleYMME polkVehicleYMME;
        private string nickname = "";
        private string vin = "";
        private string licensePlateNumber = "";
        private string manufacturerName = "";
        private string manufacturerNameAlt = "";
        private NullableInt32 year = NullableInt32.Null;
        private string make = "";
        private string model = "";
        private string aaia = "";
        private string trimLevel = "";
        private string engineVINCode = "";
        private string engineType = "";
        private string fuelType = "";
        private string transmissionControlType = "";
        private string bodyCode = null;
        private string vpManufacturer = "";
        private NullableInt32 vpYear = NullableInt32.Null;
        private string vpMake = "";
        private string vpModel = "";
        private string vpAAIA = "";
        private string vpTrimLevel = "";
        private string vpEngineVINCode = "";
        private string vpEngineType = "";
        private string vpFuelType = "";
        private string vinPowerDatabaseVersion = "";
        private NullableInt32 vinStatus = NullableInt32.Null;
        private string vinStatusDescription = "";
        private string detailedDecodingProcessDescription = "";
        private NullableInt32 mileage = NullableInt32.Null;
        private NullableDateTime mileageLastRecordedDateTimeUTC = NullableDateTime.Null;
        private bool sendScheduledMaintenanceAlerts = false;
        private NullableInt32 scheduleMaintenanceMileageIntervalLastFound = NullableInt32.Null;
        private NullableDateTime scheduleMaintenanceAlertLastSentDateTimeUTC = NullableDateTime.Null;
        private bool sendNewRecallAlerts = false;
        private NullableDateTime newRecallAlertLastSentDateTimeUTC = NullableDateTime.Null;
        private bool sendNewTSBAlerts = false;
        private NullableDateTime newTSBAlertLastSentDateTimeUTC = NullableDateTime.Null;

        private bool isActive = true;
        private bool isDeleted = false;

        private string purchasedTsbsString = "";
        private TsbPurchasedCollection purchasedTsbs;

        #region System Constructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new (one that does not exist, assigning a new <see cref="Guid"/> Id). Vehicle object.
        /// In order to create a new Vehicle which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <example>
        /// <code>
        /// Vehicle o = (Vehicle)Registry.CreateInstance(typeof(Vehicle));
        /// </code>
        /// </example>
        protected internal Vehicle() : base()
        {
            IsObjectCreated = true;
            IsObjectLoaded = true;
            IsObjectDirty = true;
        }

        /// <summary>
        /// Constructor creates an existing (one that exists, and already has a <see cref="Guid"/> Id).  Vehicle object.
        /// In order to create an existing Vehicle object, which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <param name="id">The <see cref="Guid"/> or SQL uniqueidentifier indentification "Id" of the current object.</param>
        /// <example>
        /// <code>
        /// Vehicle o = (Vehicle)Registry.CreateInstance(typeof(Vehicle), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));
        /// </code>
        /// </example>
        protected internal Vehicle(Guid id) : base(id)
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
        /// Gets or sets the <see cref="User"/> the vehicle is associated with.
        /// </summary>
        [PropertyDefinition("User", "The user to whom this vehicle belongs.")]
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
                if (this.user != value)
                {
                    this.IsObjectDirty = true;
                    this.user = value;
                    this.UpdatedField("UserId");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="ScheduleMaintenancePlan"/> for the vehicle.
        /// </summary>
        [PropertyDefinition("Scheduled Maintenance Plan", "The scheduled maintenance plan for this vehicle.")]
        public ScheduleMaintenancePlan ScheduleMaintenancePlan
        {
            get
            {
                this.EnsureLoaded();
                return this.scheduleMaintenancePlan;
            }
            set
            {
                this.EnsureLoaded();
                if (this.scheduleMaintenancePlan != value)
                {
                    this.IsObjectDirty = true;
                    this.scheduleMaintenancePlan = value;
                    this.UpdatedField("ScheduleMaintenancePlanId");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="PolkVehicleYMME"/> associated with the VIN of the vehicle.
        /// </summary>
        [PropertyDefinition("Polk Vehicle", "The Polk vehicle assiciated with the VIN.")]
        public PolkVehicleYMME PolkVehicleYMME
        {
            get
            {
                this.EnsureLoaded();
                //this.EnsurePolkVehicleDecoded();
                return this.polkVehicleYMME;
            }
            set
            {
                this.EnsureLoaded();
                if (this.polkVehicleYMME != value)
                {
                    this.IsObjectDirty = true;
                    this.polkVehicleYMME = value;
                    this.UpdatedField("PolkVehicleYMMEId");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> nickname of the vehicle.
        /// </summary>
        [PropertyDefinition("Nickname", "The nickname for this vehicle.")]
        public string Nickname
        {
            get
            {
                this.EnsureLoaded();
                return this.nickname;
            }
            set
            {
                this.EnsureLoaded();
                if (this.nickname != value)
                {
                    this.IsObjectDirty = true;
                    this.nickname = value;
                    this.UpdatedField("Nickname");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> VIN of the vehicle.
        /// </summary>
        [PropertyDefinition("VIN", "The VIN for this vehicle.")]
        public string Vin
        {
            get
            {
                this.EnsureLoaded();
                return this.vin;
            }
            set
            {
                this.EnsureLoaded();
                if (this.vin != value)
                {
                    this.IsObjectDirty = true;
                    this.vin = value;
                    this.UpdatedField("VIN");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> license plate number of the vehicle.
        /// </summary>
        [PropertyDefinition("License Plate Number", "The license plate number for this vehicle.")]
        public string LicensePlateNumber
        {
            get
            {
                this.EnsureLoaded();
                return this.licensePlateNumber;
            }
            set
            {
                this.EnsureLoaded();
                if (this.licensePlateNumber != value)
                {
                    this.IsObjectDirty = true;
                    this.licensePlateNumber = value;
                    this.UpdatedField("LicensePlateNumber");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> manufacturer name of the vehicle.
        /// </summary>
        [PropertyDefinition("Manufacturer Name", "The manufacturer of this vehicle.")]
        public string ManufacturerName
        {
            get
            {
                this.EnsureLoaded();
                if (this.PolkVehicleYMME != null)
                {
                    return this.PolkVehicleYMME.Manufacturer;
                }
                else
                {
                    return this.VPManufacturer;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> alternate manufacturer name of the vehicle.
        /// </summary>
        [PropertyDefinition("Alternate Manufacturer Name", "The alternate name of the manufacturer of this vehicle.")]
        public string ManufacturerNameAlt
        {
            get
            {
                this.EnsureLoaded();
                return this.manufacturerNameAlt;
            }
            set
            {
                this.EnsureLoaded();
                if (this.manufacturerNameAlt != value)
                {
                    this.IsObjectDirty = true;
                    this.manufacturerNameAlt = value;
                    this.UpdatedField("ManufacturerNameAlt");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableInt32"/> year of the vehicle.
        /// </summary>
        [PropertyDefinition("Year", "The year of this vehicle.")]
        public int Year
        {
            get
            {
                this.EnsureLoaded();
                if (this.PolkVehicleYMME != null)
                {
                    return this.PolkVehicleYMME.Year;
                }
                else
                {
                    return this.VPYear.HasValue ? this.VPYear.Value : -1;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> make of the vehicle.
        /// </summary>
        [PropertyDefinition("Make", "The make of this vehicle.")]
        public string Make
        {
            get
            {
                this.EnsureLoaded();
                if (this.PolkVehicleYMME != null)
                {
                    return this.PolkVehicleYMME.Make;
                }
                else
                {
                    return this.VPMake;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> model of the vehicle.
        /// </summary>
        [PropertyDefinition("Model", "The model of this vehicle.")]
        public string Model
        {
            get
            {
                this.EnsureLoaded();
                if (this.PolkVehicleYMME != null)
                {
                    return this.PolkVehicleYMME.Model;
                }
                else
                {
                    return this.VPModel;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> AAIA of the vehicle.
        /// </summary>
        [PropertyDefinition("AAIA", "The AAIA (Automotive Aftermarket Industry Association) assigned identifier(s) for this vehicle.")]
        public string AAIA
        {
            get
            {
                this.EnsureLoaded();
                if (this.PolkVehicleYMME != null)
                {
                    return this.PolkVehicleYMME.AAIA;
                }
                else
                {
                    return this.VPAAIA;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> trim level of the vehicle.
        /// </summary>
        [PropertyDefinition("Trime Level", "Trim", "Trim", "The trim level of this vehicle.")]
        public string TrimLevel
        {
            get
            {
                this.EnsureLoaded();
                if (this.PolkVehicleYMME != null)
                {
                    return this.PolkVehicleYMME.Trim;
                }
                else
                {
                    return this.VPTrimLevel;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> enigine VIN code of the vehicle.
        /// </summary>
        [PropertyDefinition("Engine VIN Code", "The engine VIN code of this vehicle.")]
        public string EngineVINCode
        {
            get
            {
                this.EnsureLoaded();
                if (this.PolkVehicleYMME != null)
                {
                    return this.PolkVehicleYMME.EngineVinCode;
                }
                else
                {
                    return this.VPEngineVINCode;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> engine type of this vehicle.
        /// </summary>
        [PropertyDefinition("Engine Type", "Engine", "Engine", "Engine", "The engine type of this vehicle.")]
        public string EngineType
        {
            get
            {
                this.EnsureLoaded();
                if (this.PolkVehicleYMME != null)
                {
                    return this.PolkVehicleYMME.EngineType;
                }
                else
                {
                    return this.VPEngineType;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> fuel type of this vehicle.
        /// </summary>
        [PropertyDefinition("Engine Type", "The fuel type of this vehicle.")]
        public string FuelType
        {
            get
            {
                this.EnsureLoaded();
                return this.fuelType;
            }
            set
            {
                this.EnsureLoaded();
                if (this.fuelType != value)
                {
                    this.IsObjectDirty = true;
                    this.fuelType = value;
                    this.UpdatedField("FuelType");
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> transmission type (automatic or manual) for this vehicle.
        /// </summary>
        [PropertyDefinition("Transmission", "The transmission type (automatic or manual) for this vehicle.")]
        public string TransmissionControlType
        {
            get
            {
                this.EnsureLoaded();
                if (this.PolkVehicleYMME != null && !String.IsNullOrEmpty(this.PolkVehicleYMME.Transmission))
                {
                    return this.PolkVehicleYMME.Transmission;
                }
                else
                {
                    return this.transmissionControlType;
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> body code for the vehicle.
        /// </summary>
        [PropertyDefinition("Body Code", "The body code of this vehicle.")]
        public string BodyCode
        {
            get
            {
                if (this.ManufacturerName.IndexOf("General") >= 0)
                {
                    Regex regex = new Regex(@"\d");
                    if (!regex.IsMatch(this.Vin.Substring(2, 1)))
                    {
                        bodyCode = this.Vin.Substring(4, 1);
                    }
                }

                return bodyCode;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> VinPOWER manufacturer.
        /// </summary>
        [PropertyDefinition("VinPOWER Manufacturer", "The VinPOWER manufacturer.")]
        public string VPManufacturer
        {
            get
            {
                this.EnsureLoaded();
                return this.vpManufacturer;
            }
            set
            {
                this.EnsureLoaded();
                if (this.vpManufacturer != value)
                {
                    this.IsObjectDirty = true;
                    this.vpManufacturer = value;
                    this.UpdatedField("VPManufacturer");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> VinPOWER year.
        /// </summary>
        [PropertyDefinition("VinPOWER Year", "The VinPOWER year.")]
        public NullableInt32 VPYear
        {
            get
            {
                this.EnsureLoaded();
                return this.vpYear;
            }
            set
            {
                this.EnsureLoaded();
                if (this.vpYear != value)
                {
                    this.IsObjectDirty = true;
                    this.vpYear = value;
                    this.UpdatedField("VPYear");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> VinPOWER make.
        /// </summary>
        [PropertyDefinition("VinPOWER Make", "The VinPOWER make.")]
        public string VPMake
        {
            get
            {
                this.EnsureLoaded();
                return this.vpMake;
            }
            set
            {
                this.EnsureLoaded();
                if (this.vpMake != value)
                {
                    this.IsObjectDirty = true;
                    this.vpMake = value;
                    this.UpdatedField("VPMake");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> VinPOWER Model.
        /// </summary>
        [PropertyDefinition("VinPOWER Model", "The VinPOWER Model.")]
        public string VPModel
        {
            get
            {
                this.EnsureLoaded();
                return this.vpModel;
            }
            set
            {
                this.EnsureLoaded();
                if (this.vpModel != value)
                {
                    this.IsObjectDirty = true;
                    this.vpModel = value;
                    this.UpdatedField("VPModel");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> VinPOWER AAIA.
        /// </summary>
        [PropertyDefinition("VinPOWER AAIA", "The VinPOWER AAIA.")]
        public string VPAAIA
        {
            get
            {
                this.EnsureLoaded();
                return this.vpAAIA;
            }
            set
            {
                this.EnsureLoaded();
                if (this.vpAAIA != value)
                {
                    this.IsObjectDirty = true;
                    this.vpAAIA = value;
                    this.UpdatedField("VPAAIA");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> VinPOWER Trim Level.
        /// </summary>
        [PropertyDefinition("VinPOWER Trim Level", "The VinPOWER Trim Level.")]
        public string VPTrimLevel
        {
            get
            {
                this.EnsureLoaded();
                return this.vpTrimLevel;
            }
            set
            {
                this.EnsureLoaded();
                if (this.vpTrimLevel != value)
                {
                    this.IsObjectDirty = true;
                    this.vpTrimLevel = value;
                    this.UpdatedField("VPTrimLevel");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> VinPOWER Engine VIN Code.
        /// </summary>
        [PropertyDefinition("VinPOWER Engine VIN Code", "The VinPOWER Engine VIN Code.")]
        public string VPEngineVINCode
        {
            get
            {
                this.EnsureLoaded();
                return this.vpEngineVINCode;
            }
            set
            {
                this.EnsureLoaded();
                if (this.vpEngineVINCode != value)
                {
                    this.IsObjectDirty = true;
                    this.vpEngineVINCode = value;
                    this.UpdatedField("VPEngineVINCode");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> VinPOWER Engine Type.
        /// </summary>
        [PropertyDefinition("VinPOWER Engine Type", "The VinPOWER Engine Type.")]
        public string VPEngineType
        {
            get
            {
                this.EnsureLoaded();
                return this.vpEngineType;
            }
            set
            {
                this.EnsureLoaded();
                if (this.vpEngineType != value)
                {
                    this.IsObjectDirty = true;
                    this.vpEngineType = value;
                    this.UpdatedField("VPEngineType");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> VinPOWER Fuel Type.
        /// </summary>
        [PropertyDefinition("VinPOWER Fuel Type", "The VinPOWER Fuel Type.")]
        public string VPFuelType
        {
            get
            {
                this.EnsureLoaded();
                return this.vpFuelType;
            }
            set
            {
                this.EnsureLoaded();
                if (this.vpFuelType != value)
                {
                    this.IsObjectDirty = true;
                    this.vpFuelType = value;
                    this.UpdatedField("VPFuelType");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> VinPOWER database version from which the VIN was decoded.
        /// </summary>
        [PropertyDefinition("VinPOWER Database Version", "The VinPOWER database version from which the VIN was decoded.")]
        public string VinPowerDatabaseVersion
        {
            get
            {
                this.EnsureLoaded();
                return this.vinPowerDatabaseVersion;
            }
            set
            {
                this.EnsureLoaded();
                if (this.vinPowerDatabaseVersion != value)
                {
                    this.IsObjectDirty = true;
                    this.vinPowerDatabaseVersion = value;
                    this.UpdatedField("VinPowerDatabaseVersion");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableInt32"/> VIN decode status.
        /// </summary>
        [PropertyDefinition("VIN Status", "The VIN decode status.")]
        public NullableInt32 VinStatus
        {
            get
            {
                this.EnsureLoaded();
                return this.vinStatus;
            }
            set
            {
                this.EnsureLoaded();
                if (this.vinStatus != value)
                {
                    this.IsObjectDirty = true;
                    this.vinStatus = value;
                    this.UpdatedField("VinStatus");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> VIN decode status description.
        /// </summary>
        [PropertyDefinition("VIN Status Description", "The VIN decode status description.")]
        public string VinStatusDescription
        {
            get
            {
                this.EnsureLoaded();
                return this.vinStatusDescription;
            }
            set
            {
                this.EnsureLoaded();
                if (this.vinStatusDescription != value)
                {
                    this.IsObjectDirty = true;
                    this.vinStatusDescription = value;
                    this.UpdatedField("VinStatusDescription");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> detailed description of the VIN decoding process.
        /// </summary>
        [PropertyDefinition("Detailed Decoding Process Description", "The detailed description of the VIN decoding process.")]
        public string DetailedDecodingProcessDescription
        {
            get
            {
                this.EnsureLoaded();
                return this.detailedDecodingProcessDescription;
            }
            set
            {
                this.EnsureLoaded();
                if (this.detailedDecodingProcessDescription != value)
                {
                    this.IsObjectDirty = true;
                    this.detailedDecodingProcessDescription = value;
                    this.UpdatedField("DetailedDecodingProcessDescription");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableInt32"/> mileage of the vehicle.
        /// </summary>
        [PropertyDefinition("Mileage", "The number of miles this vehicle has been driven.")]
        public NullableInt32 Mileage
        {
            get
            {
                this.EnsureLoaded();
                return this.mileage;
            }
            set
            {
                this.EnsureLoaded();
                if (this.mileage != value)
                {
                    this.IsObjectDirty = true;
                    this.mileage = value;
                    this.UpdatedField("Mileage");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableDateTime"/> date that the mileage was last updated.
        /// </summary>
        [PropertyDefinition("Mileage Last Recorded DateTime UTC", "The last time the mileage for this vehicle was updated.")]
        public NullableDateTime MileageLastRecordedDateTimeUTC
        {
            get
            {
                this.EnsureLoaded();
                return this.mileageLastRecordedDateTimeUTC;
            }
            set
            {
                this.EnsureLoaded();
                if (this.mileageLastRecordedDateTimeUTC != value)
                {
                    this.IsObjectDirty = true;
                    this.mileageLastRecordedDateTimeUTC = value;
                    this.UpdatedField("MileageLastRecordedDateTimeUTC");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Boolean"/> indicating if scheduled maintenance alerts should be sent to the user for this vehicle.
        /// </summary>
        [PropertyDefinition("Send Scheduled Maintenance Alerts", "Flag that determins if alerts for upcoming scheduled maintenance should be sent.")]
        public bool SendScheduledMaintenanceAlerts
        {
            get
            {
                this.EnsureLoaded();
                return this.sendScheduledMaintenanceAlerts;
            }
            set
            {
                this.EnsureLoaded();
                if (this.sendScheduledMaintenanceAlerts != value)
                {
                    this.IsObjectDirty = true;
                    this.sendScheduledMaintenanceAlerts = value;
                    this.UpdatedField("SendScheduledMaintenanceAlerts");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableInt32"/>
        /// </summary>
        [PropertyDefinition("Scheduled Maintenance Mileage Interval Last Found", "The the last found mileage interval for the vehicle's maintenance plan.")]
        public NullableInt32 ScheduleMaintenanceMileageIntervalLastFound
        {
            get
            {
                this.EnsureLoaded();
                return this.scheduleMaintenanceMileageIntervalLastFound;
            }
            set
            {
                this.EnsureLoaded();
                if (this.scheduleMaintenanceMileageIntervalLastFound != value)
                {
                    this.IsObjectDirty = true;
                    this.scheduleMaintenanceMileageIntervalLastFound = value;
                    this.UpdatedField("ScheduleMaintenanceMileageIntervalLastFound");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableDateTime"/>
        /// </summary>
        [PropertyDefinition("Scheduled Maintenance Alert Last Sent DateTime (UTC)", "The last time a maintenance alert was sent.")]
        public NullableDateTime ScheduleMaintenanceAlertLastSentDateTimeUTC
        {
            get
            {
                this.EnsureLoaded();
                return this.scheduleMaintenanceAlertLastSentDateTimeUTC;
            }
            set
            {
                this.EnsureLoaded();
                if (this.scheduleMaintenanceAlertLastSentDateTimeUTC != value)
                {
                    this.IsObjectDirty = true;
                    this.scheduleMaintenanceAlertLastSentDateTimeUTC = value;
                    this.UpdatedField("ScheduleMaintenanceAlertLastSentDateTimeUTC");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/> flag indicating whether or not the vehicle is signed up to receive new recall alerts
        /// </summary>
        [PropertyDefinition("Send New Recall Alerts", "Flag that determins if alerts for new recalls should be sent.")]
        public bool SendNewRecallAlerts
        {
            get
            {
                this.EnsureLoaded();
                return this.sendNewRecallAlerts;
            }
            set
            {
                this.EnsureLoaded();
                if (this.sendNewRecallAlerts != value)
                {
                    this.IsObjectDirty = true;
                    this.sendNewRecallAlerts = value;
                    this.UpdatedField("SendNewRecallAlerts");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableDateTime"/> date time the new recall alert was last sent.
        /// </summary>
        [PropertyDefinition("New Recall Alert Last Sent DateTime (UTC)", "The last time a recall alert was sent.")]
        public NullableDateTime NewRecallAlertLastSentDateTimeUTC
        {
            get
            {
                this.EnsureLoaded();
                return this.newRecallAlertLastSentDateTimeUTC;
            }
            set
            {
                this.EnsureLoaded();
                if (this.newRecallAlertLastSentDateTimeUTC != value)
                {
                    this.IsObjectDirty = true;
                    this.newRecallAlertLastSentDateTimeUTC = value;
                    this.UpdatedField("NewRecallAlertLastSentDateTimeUTC");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/> flag indicating whether or not the vehicle is signed up to receive new TSB alerts
        /// </summary>
        [PropertyDefinition("Send New TSB Alerts", "Flag that determins if alerts for new TSBs should be sent.")]
        public bool SendNewTSBAlerts
        {
            get
            {
                this.EnsureLoaded();
                return this.sendNewTSBAlerts;
            }
            set
            {
                this.EnsureLoaded();
                if (this.sendNewTSBAlerts != value)
                {
                    this.IsObjectDirty = true;
                    this.sendNewTSBAlerts = value;
                    this.UpdatedField("SendNewTSBAlerts");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableDateTime"/> date time the new tsb alert was last sent.
        /// </summary>
        [PropertyDefinition("TSB Alert Last Sent DateTime (UTC)", "The last time a TSB alert was sent.")]
        public NullableDateTime NewTSBAlertLastSentDateTimeUTC
        {
            get
            {
                this.EnsureLoaded();
                return this.newTSBAlertLastSentDateTimeUTC;
            }
            set
            {
                this.EnsureLoaded();
                if (this.newTSBAlertLastSentDateTimeUTC != value)
                {
                    this.IsObjectDirty = true;
                    this.newTSBAlertLastSentDateTimeUTC = value;
                    this.UpdatedField("NewTSBAlertLastSentDateTimeUTC");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Boolean"/> indicating if the vehicle is active.
        /// </summary>
        [PropertyDefinition("Active", "Flag indicating if the vehicle is active.")]
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
                    this.UpdatedField("IsActive");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Boolean"/> indicating if the vehicle is deleted.
        /// </summary>
        [PropertyDefinition("Deleted", "Flag indicating if the vehicle has been deleted.")]
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
                    this.UpdatedField("IsDeleted");
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

        /// <summary>
        /// Gets the collection of TSBs that have been purchased.
        /// </summary>
        public TsbPurchasedCollection PurchasedTsbs
        {
            get
            {
                this.EnsureLoaded();

                if (this.purchasedTsbs == null)
                {
                    this.purchasedTsbs = this.GetPurchasedTsbs(this.purchasedTsbsString);
                }

                return this.purchasedTsbs;
            }
        }

        /// <summary>
        /// Gets a collection of purchased TSBs from a supplied delimited string.
        /// </summary>
        /// <param name="delimitedString">The delimited <see cref="string"/> in the format of "[date1],[TsbId1a],[TsbId1b]|[date2],[TsbId2a],[TsbId2b],[TsbId2c]".</param>
        /// <returns>A <see cref="TsbPurchasedCollection"/> of <see cref="TsbPurchased"/> objects.</returns>
        private TsbPurchasedCollection GetPurchasedTsbs(string delimitedString)
        {
            // Create a collection to be returned
            TsbPurchasedCollection tsbs = new TsbPurchasedCollection();

            if (delimitedString != "")
            {
                // Get an array of delimited date and TSB ids
                string[] dateTsbValues = delimitedString.Split(new char[] { '|' });

                // Loop through the array
                for (int i = 0; i < dateTsbValues.Length; i++)
                {
                    // Create a variable to store the purchased date and set it to MinValue.
                    DateTime purchasedDate = DateTime.MinValue;
                    // Get an array of the purchase date and individual TSB ids.
                    string[] dateTsbs = dateTsbValues[i].Split(new char[] { ',' });

                    // If the array contains at least 2 values, meaning the purchase date and at least one TSB id, then get the purchase date.
                    if (dateTsbs.Length > 1)
                    {
                        try
                        {
                            purchasedDate = DateTime.Parse(dateTsbs[0]);
                        }
                        catch
                        {
                        }
                    }

                    // If we got a valid purchase date then proceed.
                    if (purchasedDate != DateTime.MinValue)
                    {
                        // Since the first value was the purchase date, loop through the remaining values to get the TSB ids.
                        for (int x = 1; x < dateTsbs.Length; x++)
                        {
                            // Create a TsbPurchased object
                            TsbPurchased tsb = new TsbPurchased();
                            // Set the TSBId to MinValue so we can test for it later.
                            tsb.TsbId = int.MinValue;

                            try
                            {
                                // Parse out the TSBId
                                tsb.TsbId = int.Parse(dateTsbs[x]);
                            }
                            catch
                            {
                            }

                            // If we got a valid TSBId then set the rest of the values and add the object to the collection.
                            if (tsb.TsbId != int.MinValue)
                            {
                                tsb.PurchaseDate = purchasedDate;
                                tsb.Vehicle = this;
                                tsbs.Add(tsb);
                            }
                        }
                    }
                }
            }

            return tsbs;
        }

        /// <summary>
        /// Gets a delimited string of a purchased TSB collection.
        /// </summary>
        /// <param name="tsbs">The <see cref="TsbPurchasedCollection"/> to translate into a delimited string.</param>
        /// <returns>A delimited <see cref="string"/> in the format of "[date1],[TsbId1]|[date2],[TsbId2]".</returns>
        private string GetPurchasedTsbsString(TsbPurchasedCollection tsbs)
        {
            StringBuilder sb = new StringBuilder();

            foreach (TsbPurchased tsb in tsbs)
            {
                if (sb.Length > 0)
                {
                    sb.Append("|");
                }

                sb.Append(tsb.PurchaseDate.ToShortDateString());
                sb.Append(",");
                sb.Append(tsb.TsbId.ToString());
            }

            return sb.ToString();
        }

        #endregion Object Properties (Related Objects)

        #region Business Logic Methods

        /***********************************************************************************************
		 *
		 * Custom Business Logic
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Check to make sure we've looked up a Polk vehicle if we have a VIN
        /// </summary>
        private void EnsurePolkVehicleDecoded()
        {
            if (this.polkVehicleYMME == null && !String.IsNullOrEmpty(this.vin))
            {
                PolkVinDecoder pvd = new PolkVinDecoder(this.Registry);
                this.polkVehicleYMME = pvd.DecodeVIN(this.vin);
            }
        }

        /// <summary>
        /// Gets the estimated mileage for a given date based on the value provided for miles driven per day.
        /// </summary>
        /// <param name="date">The <see cref="DateTime"/> for which to calculate the mileage.</param>
        /// <param name="milesDrivenPerDay">The <see cref="int"/> estimated miles driven per day.</param>
        /// <returns>A <see cref="int"/> estiamted mileage value for the date provided.</returns>
        public int GetEstimatedMileage(DateTime date, int milesDrivenPerDay)
        {
            if (this.Mileage.IsNull)
            {
                this.Mileage = this.GetLastVehicleMileage();
                this.MileageLastRecordedDateTimeUTC = DateTime.UtcNow;
                this.Save();
            }

            if (this.MileageLastRecordedDateTimeUTC.IsNull)
            {
                return milesDrivenPerDay;
            }

            TimeSpan ts = date.Subtract(this.MileageLastRecordedDateTimeUTC.Value);
            int daysSinceLastMileage = (int)ts.TotalDays;

            return this.Mileage.Value + (daysSinceLastMileage * milesDrivenPerDay);
        }

        /// <summary>
        /// Get Estimated Kilometers
        /// </summary>
        /// <returns></returns>
        public int GetEstimatedKilometers(DateTime date, int currentKilometer, int milesDrivenPerDay)
        {
            var kmsDrivenPerDay = (int)Math.Round(milesDrivenPerDay * 1.64, 0);
            if (this.MileageLastRecordedDateTimeUTC.IsNull)
            {
                return kmsDrivenPerDay;
            }

            TimeSpan ts = date.Subtract(this.MileageLastRecordedDateTimeUTC.Value);
            int daysSinceLastMileage = (int)ts.TotalDays;

            return currentKilometer + (daysSinceLastMileage * kmsDrivenPerDay);
        }

        /// <summary>
        /// Get the last vehicle mileage used for a diagnostic report.
        /// </summary>
        /// <returns>The <see cref="int"/> vehicle mileage.</returns>
        public int GetLastVehicleMileage()
        {
            int mileage = 0;

            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(ConnectionString))
            {
                dr.ProcedureName = "DiagnosticReport_GetLastVehicleMileage";
                dr.AddGuid("UserId", this.User.Id);
                dr.AddGuid("VehicleId", this.Id);
                dr.Execute();

                if (dr.Read())
                {
                    mileage = dr.GetInt32("VehicleMileage");
                }
            }

            return mileage;
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
            dr.ProcedureName = "Vehicle_Load";
            dr.AddGuid("VehicleId", Id);
        }

        /// <summary>
        /// Sets the properties of this object from the fields in the recordset located in the supplied <see cref="SqlDataReaderWrapper"/>.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> containing the recordset and fields used to set the properties of this object (this layer only).</param>
        protected new void SetPropertiesFromDataReader(SqlDataReaderWrapper dr)
        {
            this.user = (User)Registry.CreateInstance(typeof(User), dr.GetGuid("UserId"));
            if (!dr.IsDBNull("ScheduleMaintenancePlanId"))
            {
                this.scheduleMaintenancePlan = (ScheduleMaintenancePlan)Registry.CreateInstance(typeof(ScheduleMaintenancePlan), dr.GetGuid("ScheduleMaintenancePlanId"));
            }
            if (!dr.IsDBNull("PolkVehicleYMMEId"))
            {
                this.polkVehicleYMME = (PolkVehicleYMME)Registry.CreateInstance(typeof(PolkVehicleYMME), dr.GetGuid("PolkVehicleYMMEId"));
            }
            this.nickname = dr.GetString("Nickname");
            this.vin = dr.GetString("VIN");
            this.licensePlateNumber = dr.GetString("LicensePlateNumber");
            this.manufacturerName = dr.GetString("ManufacturerName");
            this.manufacturerNameAlt = dr.GetString("ManufacturerNameAlt");
            this.year = dr.GetNullableInt32("Year");
            this.make = dr.GetString("Make");
            this.model = dr.GetString("Model");
            this.aaia = dr.GetString("AAIA");
            this.trimLevel = dr.GetString("TrimLevel");
            this.engineVINCode = dr.GetString("EngineVINCode");
            this.engineType = dr.GetString("EngineType");
            this.fuelType = dr.GetString("FuelType");
            this.transmissionControlType = dr.GetString("TransmissionControlType");
            this.vpManufacturer = dr.GetString("VPManufacturer");
            this.vpYear = dr.GetNullableInt32("VPYear");
            this.vpMake = dr.GetString("VPMake");
            this.vpModel = dr.GetString("VPModel");
            this.vpAAIA = dr.GetString("VPAAIA");
            this.vpTrimLevel = dr.GetString("VPTrimLevel");
            this.vpEngineVINCode = dr.GetString("VPEngineVINCode");
            this.vpEngineType = dr.GetString("VPEngineType");
            this.vpFuelType = dr.GetString("VPFuelType");
            this.vinPowerDatabaseVersion = dr.GetString("VinPowerDatabaseVersion");
            this.vinStatus = dr.GetNullableInt32("VinStatus");
            this.vinStatusDescription = dr.GetString("VinStatusDescription");
            this.detailedDecodingProcessDescription = dr.GetString("DetailedDecodingProcessDescription");
            this.purchasedTsbsString = dr.GetString("PurchasedTsbsString");
            this.mileage = dr.GetNullableInt32("Mileage");
            this.mileageLastRecordedDateTimeUTC = dr.GetNullableDateTime("MileageLastRecordedDateTimeUTC");
            this.sendScheduledMaintenanceAlerts = dr.GetBoolean("SendScheduledMaintenanceAlerts");
            this.scheduleMaintenanceMileageIntervalLastFound = dr.GetNullableInt32("ScheduleMaintenanceMileageIntervalLastFound");
            this.scheduleMaintenanceAlertLastSentDateTimeUTC = dr.GetNullableDateTime("ScheduleMaintenanceAlertLastSentDateTimeUTC");

            this.sendNewRecallAlerts = dr.GetBoolean("SendNewRecallAlerts");
            this.newRecallAlertLastSentDateTimeUTC = dr.GetNullableDateTime("NewRecallAlertLastSentDateTimeUTC");
            this.sendNewTSBAlerts = dr.GetBoolean("SendNewTSBAlerts");
            this.newTSBAlertLastSentDateTimeUTC = dr.GetNullableDateTime("NewTSBAlertLastSentDateTimeUTC");

            this.isActive = dr.GetBoolean("IsActive");
            this.isDeleted = dr.GetBoolean("IsDeleted");

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
                    dr.CommandTimeout = 120; //Added on 2018-07-23 1:52 PM by INNOVA Dev Team: Set the Timeout value tp 120 seconds (2 minutes)

                    if (IsObjectCreated)
                    {
                        dr.ProcedureName = "Vehicle_Create";
                    }
                    else
                    {
                        dr.UpdateFields("Vehicle", "VehicleId", this.updatedFields);
                    }

                    this.EnsurePolkVehicleDecoded();

                    dr.AddGuid("VehicleId", this.Id);
                    dr.AddBusinessObject("UserId", this.User);
                    dr.AddBusinessObject("ScheduleMaintenancePlanId", this.ScheduleMaintenancePlan);
                    dr.AddBusinessObject("PolkVehicleYMMEId", this.PolkVehicleYMME);
                    dr.AddNVarChar("Nickname", this.Nickname);
                    dr.AddNVarChar("VIN", this.Vin);
                    dr.AddNVarChar("LicensePlateNumber", this.LicensePlateNumber);
                    dr.AddNVarChar("ManufacturerName", this.ManufacturerName);
                    dr.AddNVarChar("ManufacturerNameAlt", this.ManufacturerNameAlt);
                    dr.AddInt32("Year", this.Year);
                    dr.AddNVarChar("Make", this.Make);
                    dr.AddNVarChar("Model", this.Model);
                    dr.AddNVarChar("AAIA", this.AAIA);
                    dr.AddNVarChar("TrimLevel", this.TrimLevel);
                    dr.AddNVarChar("EngineVINCode", this.EngineVINCode);
                    dr.AddNVarChar("EngineType", this.EngineType);
                    dr.AddNVarChar("FuelType", this.FuelType);
                    dr.AddNVarChar("TransmissionControlType", this.TransmissionControlType);
                    dr.AddNVarChar("VPManufacturer", this.VPManufacturer);
                    dr.AddInt32("VPYear", this.VPYear);
                    dr.AddNVarChar("VPMake", this.VPMake);
                    dr.AddNVarChar("VPModel", this.VPModel);
                    dr.AddNVarChar("VPAAIA", this.VPAAIA);
                    dr.AddNVarChar("VPTrimLevel", this.VPTrimLevel);
                    dr.AddNVarChar("VPEngineVINCode", this.VPEngineVINCode);
                    dr.AddNVarChar("VPEngineType", this.VPEngineType);
                    dr.AddNVarChar("VPFuelType", this.VPFuelType);
                    dr.AddNVarChar("VinPowerDatabaseVersion", this.VinPowerDatabaseVersion);
                    dr.AddInt32("VinStatus", this.VinStatus);
                    dr.AddNVarChar("VinStatusDescription", this.VinStatusDescription);
                    dr.AddNVarChar("DetailedDecodingProcessDescription", this.DetailedDecodingProcessDescription);
                    dr.AddNText("PurchasedTsbsString", this.GetPurchasedTsbsString(this.PurchasedTsbs));
                    dr.AddInt32("Mileage", this.Mileage);
                    dr.AddDateTime("MileageLastRecordedDateTimeUTC", this.MileageLastRecordedDateTimeUTC);
                    dr.AddBoolean("SendScheduledMaintenanceAlerts", this.SendScheduledMaintenanceAlerts);
                    dr.AddInt32("ScheduleMaintenanceMileageIntervalLastFound", this.ScheduleMaintenanceMileageIntervalLastFound);
                    dr.AddDateTime("ScheduleMaintenanceAlertLastSentDateTimeUTC", this.ScheduleMaintenanceAlertLastSentDateTimeUTC);

                    dr.AddBoolean("SendNewRecallAlerts", this.SendNewRecallAlerts);
                    dr.AddDateTime("NewRecallAlertLastSentDateTimeUTC", this.NewRecallAlertLastSentDateTimeUTC);
                    dr.AddBoolean("SendNewTSBAlerts", this.SendNewTSBAlerts);
                    dr.AddDateTime("NewTSBAlertLastSentDateTimeUTC", this.NewTSBAlertLastSentDateTimeUTC);

                    dr.AddBoolean("IsActive", this.IsActive);
                    dr.AddBoolean("IsDeleted", this.IsDeleted);

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
            throw new ApplicationException("Vehicles cannot be deleted.");
        }

        #endregion Required Object Methods (Load, Save, Delete, Etc)
    }
}
using Innova.Users;
using Innova.Vehicles;
using Metafuse3;
using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Text;

namespace Innova.ScheduleMaintenance
{
    /// <summary>
    /// The ScheduleMaintenancePlan object handles the business logic and data access for the specialized business object, ScheduleMaintenancePlan.
    /// The class inherits from <see cref="BusinessObjectBase"/> class.
    /// </summary>
    /// <example>
    /// See the examples of how to construct the ScheduleMaintenancePlan object.
    ///
    /// To create a new instance of a new of ScheduleMaintenancePlan.
    /// <code>ScheduleMaintenancePlan o = (ScheduleMaintenancePlan)Registry.CreateInstance(typeof(ScheduleMaintenancePlan));</code>
    ///
    /// To create an new instance of an existing ScheduleMaintenancePlan.
    /// <code>ScheduleMaintenancePlan o = (ScheduleMaintenancePlan)Registry.CreateInstance(typeof(ScheduleMaintenancePlan), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of ScheduleMaintenancePlan, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectBase.ConnectionString"/> to the <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable()]
    [ClassDefinition("Scheduled Maintenance Plan", "Scheduled Maintenance Plans", "Maintenance Plan", "ScheduleMaintenancePlanId")]
    public class ScheduleMaintenancePlan : BusinessObjectBase
    {
        // data object variables
        private ScheduleMaintenanceType type = ScheduleMaintenanceType.ScheduledMaintenance;

        private string manufacturerName = "";
        private string name = "";
        private int maxMileage = 0;

        private AdminUser createdByAdminUser;
        private AdminUser updatedByAdminUser;

        private bool hasEngineTypeDefined = false;
        private string engineTypesString = "";
        private bool isEngineTypesDirty = false;

        private bool hasEngineVINCodeDefined;
        private string engineVINCodesString = "";
        private bool isEngineVINCodesDirty = false;

        private bool hasYearDefined = false;
        private string yearsString = "";
        private bool isYearsDirty = false;

        private bool hasMakeDefined = false;
        private string makesString = "";
        private bool isMakesDirty = false;

        private bool hasModelDefined = false;
        private string modelsString = "";
        private bool isModelsDirty = false;

        private bool hastrimLevelDefined = false;
        private string trimLevelsString = "";
        private bool isTrimLevelsDirty = false;

        private bool hasTransmissionDefined = false;
        private string transmissionsString = "";
        private bool isTransmissionsDirty = false;

        private StringCollection engineTypes;
        private StringCollection engineVINCodes;
        private StringCollection makes;
        private StringCollection models;
        private StringCollection trimLevels;
        private StringCollection transmissionControlTypes;
        private ArrayList years;
        private bool isForHistoricalVehicles;

        private DateTime createdDateTimeUTC;
        private DateTime updatedDateTimeUTC;

        private ScheduleMaintenancePlanDetailCollection planDetails;

        #region System Constructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new (one that does not exist, assigning a new <see cref="Guid"/> Id). ScheduleMaintenancePlan object.
        /// In order to create a new ScheduleMaintenancePlan which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <example>
        /// <code>
        /// ScheduleMaintenancePlan o = (ScheduleMaintenancePlan)Registry.CreateInstance(typeof(ScheduleMaintenancePlan));
        /// </code>
        /// </example>
        protected internal ScheduleMaintenancePlan() : base()
        {
            IsObjectCreated = true;
            IsObjectLoaded = true;
            IsObjectDirty = true;
        }

        /// <summary>
        /// Constructor creates an existing (one that exists, and already has a <see cref="Guid"/> Id).  ScheduleMaintenancePlan object.
        /// In order to create an existing ScheduleMaintenancePlan object, which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <param name="id">The <see cref="Guid"/> or SQL uniqueidentifier indentification "Id" of the current object.</param>
        /// <example>
        /// <code>
        /// ScheduleMaintenancePlan o = (ScheduleMaintenancePlan)Registry.CreateInstance(typeof(ScheduleMaintenancePlan), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));
        /// </code>
        /// </example>
        protected internal ScheduleMaintenancePlan(Guid id) : base(id)
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
        /// Gets or sets the <see cref="ScheduleMaintenanceType"/> type of scheduled maintenance plans
        /// </summary>
        [PropertyDefinition("Type", "The type of plan, scheduled or unscheduled maintenance")]
        public ScheduleMaintenanceType Type
        {
            get
            {
                this.EnsureLoaded();
                return this.type;
            }
            set
            {
                this.EnsureLoaded();
                if (this.type != value)
                {
                    IsObjectDirty = true;
                    this.type = value;
                    this.UpdatedField("Type");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        [PropertyDefinition("Manufacturer", "Manufacturer associated with this maintenance plan")]
        public string ManufacturerName
        {
            get
            {
                EnsureLoaded();
                return manufacturerName;
            }
            set
            {
                EnsureLoaded();
                if (manufacturerName != value)
                {
                    IsObjectDirty = true;
                    manufacturerName = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        [PropertyDefinition("Name", "Name of maintenance plan.")]
        public string Name
        {
            get
            {
                EnsureLoaded();
                return name;
            }
            set
            {
                EnsureLoaded();
                if (name != value)
                {
                    IsObjectDirty = true;
                    name = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/> max mileage interval on the plan.
        /// </summary>
        [PropertyDefinition("Max Mileage", "The maximum mileage interval on the plan.")]
        public int MaxMileage
        {
            get
            {
                this.EnsureLoaded();
                return this.maxMileage;
            }
            set
            {
                this.EnsureLoaded();
                if (this.maxMileage != value)
                {
                    this.IsObjectDirty = true;
                    this.maxMileage = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="AdminUser"/>
        /// </summary>
        [PropertyDefinition("Created By", "The user who created the maintenance plan")]
        public AdminUser CreatedByAdminUser
        {
            get
            {
                EnsureLoaded();
                return createdByAdminUser;
            }
            set
            {
                EnsureLoaded();
                if (createdByAdminUser != value)
                {
                    IsObjectDirty = true;
                    createdByAdminUser = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="UpdatedByAdminUser"/>
        /// </summary>
        [PropertyDefinition("Updated By", "The user who last updated the maintenance plan")]
        public AdminUser UpdatedByAdminUser
        {
            get
            {
                EnsureLoaded();
                return updatedByAdminUser;
            }
            set
            {
                EnsureLoaded();
                if (updatedByAdminUser != value)
                {
                    IsObjectDirty = true;
                    updatedByAdminUser = value;
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="bool"/> flag indicating whether or not the engine type is defined.
        /// </summary>
        public bool HasEngineTypeDefined
        {
            get
            {
                EnsureLoaded();
                return hasEngineTypeDefined;
            }
        }

        /// <summary>
        /// Gets the <see cref="bool"/> flag indicating whether or not the engine type is defined.
        /// </summary>
        public bool HasEngineVINCodeDefined
        {
            get
            {
                EnsureLoaded();
                return hasEngineVINCodeDefined;
            }
        }

        /// <summary>
        /// Gets the <see cref="bool"/> flag indicating whether or not the year is defined.
        /// </summary>
        public bool HasYearDefined
        {
            get
            {
                EnsureLoaded();
                return hasYearDefined;
            }
        }

        /// <summary>
        /// Gets the <see cref="bool"/> flag indicating whether or not the make is defined.
        /// </summary>
        public bool HasMakeDefined
        {
            get
            {
                EnsureLoaded();
                return hasMakeDefined;
            }
        }

        /// <summary>
        /// Gets the <see cref="bool"/> flag indicating whether or not the model is defined.
        /// </summary>
        public bool HasModelDefined
        {
            get
            {
                EnsureLoaded();
                return hasModelDefined;
            }
        }

        /// <summary>
        /// Gets the <see cref="bool"/> flag indicating whether or not the trim level is defined.
        /// </summary>
        public bool HasTrimLevelDefined
        {
            get
            {
                EnsureLoaded();
                return hastrimLevelDefined;
            }
        }

        /// <summary>
        /// Gets the <see cref="bool"/> flag indicating whether or not the transmission control type is defined.
        /// </summary>
        public bool HasTransmissionDefined
        {
            get
            {
                EnsureLoaded();
                return hasTransmissionDefined;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/> indicating if this plan applies to historical vehicles.
        /// </summary>
        [PropertyDefinition("Is For Historical Vehicles", "Mark As Historical Vehicle", "Date and time the maintenance plan was created.")]
        public bool IsForHistoricalVehicles
        {
            get
            {
                this.EnsureLoaded();
                return this.isForHistoricalVehicles;
            }
            set
            {
                this.EnsureLoaded();
                if (this.isForHistoricalVehicles != value)
                {
                    this.IsObjectDirty = true;
                    this.isForHistoricalVehicles = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/>
        /// </summary>
        [PropertyDefinition("Created", "Date and time the maintenance plan was created.")]
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
        /// Gets or sets the <see cref="DateTime"/>
        /// </summary>
        [PropertyDefinition("Updated", "Date and time the maineance plan was last updated.")]
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

        #endregion Object Properties

        #region Object Properties (Related Objects)

        /*****************************************************************************************
		 *
		 * Object Relationships: Add correlated Object Collections
		 *
		*******************************************************************************************/

        /// <summary>
        /// Gets a <see cref="ScheduleMaintenancePlanDetailCollection"/> related to this plan.
        /// </summary>
        public ScheduleMaintenancePlanDetailCollection PlanDetails
        {
            get
            {
                if (this.planDetails == null)
                {
                    this.planDetails = new ScheduleMaintenancePlanDetailCollection(Registry);

                    //load if not a user created element
                    if (!this.IsObjectCreated)
                    {
                        SqlProcedureCommand call = new SqlProcedureCommand();
                        EnsureValidId();

                        call.ProcedureName = "ScheduleMaintenancePlanDetail_LoadByScheduleMaintenancePlan";
                        call.AddGuid("ScheduleMaintenancePlanId", Id);

                        this.planDetails.Load(call, "ScheduleMaintenancePlanDetailId", true, true);

                        SortDictionaryCollection sorts = new SortDictionaryCollection();
                        sorts.Add(new SortDictionary("ScheduleMaintenanceService.FixName.Description_Translated", SortDirection.Ascending));
                        this.planDetails.Sort(sorts);
                    }
                }

                return this.planDetails;
            }
        }

        /// <summary>
        /// Gets the <see cref="StringCollection"/> of vehicle engine types that apply to this fix.
        /// NOTE: DO NOT add to this collection directly. Use the AddEngineType() method.
        /// </summary>
        [PropertyDefinition("Engines", "Types of engines associated with this maintenance plan")]
        public StringCollection EngineTypes
        {
            get
            {
                if (engineTypes == null)
                {
                    engineTypes = new StringCollection();

                    if (!isObjectCreated && this.engineTypesString != "")
                    {
                        foreach (string s in this.engineTypesString.Split("|".ToCharArray()))
                        {
                            if (s != null && s != "")
                            {
                                engineTypes.Add(s);
                            }
                        }
                    }
                }
                return engineTypes;
            }
        }

        /// <summary>
        /// Gets the <see cref="StringCollection"/> of vehicle engine VIN codes that apply to this fix.
        /// NOTE: DO NOT add to this collection directly. Use the AddEngineVINCode() method.
        /// </summary>
        [PropertyDefinition("Engine Codes", "VIN Code the engine is assocated with, may have mulitple values separated by a comma")]
        public StringCollection EngineVINCodes
        {
            get
            {
                this.EnsureLoaded();

                if (engineVINCodes == null)
                {
                    engineVINCodes = new StringCollection();

                    if (!isObjectCreated && this.engineVINCodesString != "")
                    {
                        foreach (string s in this.engineVINCodesString.Split("|".ToCharArray()))
                        {
                            if (s != null && s != "")
                            {
                                engineVINCodes.Add(s);
                            }
                        }
                    }
                }
                return engineVINCodes;
            }
        }

        /// <summary>
        /// Get the <see cref="StringCollection"/> of vehicle makes that apply to this fix.
        /// NOTE: DO NOT add to this collection directly. Use the AddMake() method.
        /// </summary>
        [PropertyDefinition("Makes", "Makes associated with this maintenance plan.")]
        public StringCollection Makes
        {
            get
            {
                this.EnsureLoaded();

                if (makes == null)
                {
                    makes = new StringCollection();

                    //load if not a user created element

                    if (!isObjectCreated && this.makesString != "")
                    {
                        foreach (string s in this.makesString.Split("|".ToCharArray()))
                        {
                            if (s != null && s != "")
                            {
                                makes.Add(s);
                            }
                        }
                    }
                }

                return makes;
            }
        }

        /// <summary>
        /// Get the <see cref="StringCollection"/> of vehicle models that apply to this fix.
        /// NOTE: DO NOT add to this collection directly. Use the AddModel() method.
        /// </summary>
        [PropertyDefinition("Models", "Models associated with this maintenance plan")]
        public StringCollection Models
        {
            get
            {
                this.EnsureLoaded();

                if (models == null)
                {
                    models = new StringCollection();

                    if (!isObjectCreated && this.modelsString != "")
                    {
                        foreach (string s in this.modelsString.Split("|".ToCharArray()))
                        {
                            if (s != null && s != "")
                            {
                                models.Add(s);
                            }
                        }
                    }
                }

                return models;
            }
        }

        /// <summary>
        /// Get the <see cref="StringCollection"/> of vehicle trim levels that apply to this fix.
        /// NOTE: DO NOT add to this collection directly. Use the AddTrimLevel() method.
        /// </summary>
        [PropertyDefinition("Trim Levels", "Trim Levels associated with this maintenance plan")]
        public StringCollection TrimLevels
        {
            get
            {
                this.EnsureLoaded();

                if (trimLevels == null)
                {
                    trimLevels = new StringCollection();

                    if (!isObjectCreated && this.trimLevelsString != "")
                    {
                        foreach (string s in this.trimLevelsString.Split("|".ToCharArray()))
                        {
                            if (s != null && s != "")
                            {
                                trimLevels.Add(s);
                            }
                        }
                    }
                }

                return trimLevels;
            }
        }

        /// <summary>
        /// Get the <see cref="StringCollection"/> of transmission control types that apply to this fix.
        /// NOTE: DO NOT add to this collection directly. Use the AddTransmissionControlType() method.
        /// </summary>
        [PropertyDefinition("Transmission", "Types of transmissions associated with this maintenance")]
        public StringCollection TransmissionControlTypes
        {
            get
            {
                this.EnsureLoaded();

                if (transmissionControlTypes == null)
                {
                    transmissionControlTypes = new StringCollection();

                    if (!isObjectCreated && this.transmissionsString != "")
                    {
                        foreach (string s in this.transmissionsString.Split("|".ToCharArray()))
                        {
                            if (s != null && s != "")
                            {
                                transmissionControlTypes.Add(s);
                            }
                        }
                    }
                }

                return transmissionControlTypes;
            }
        }

        /// <summary>
        /// Get an <see cref="ArrayList"/> of <see cref="int"/> vehicle years that apply to this fix.
        /// NOTE: DO NOT add to this collection directly. Use the AddYear() method.
        /// </summary>
        [PropertyDefinition("Years", "Years associated with this maintenance")]
        public ArrayList Years
        {
            get
            {
                this.EnsureLoaded();

                if (years == null)
                {
                    years = new ArrayList();

                    if (!isObjectCreated && this.yearsString != "")
                    {
                        foreach (string s in this.yearsString.Split("|".ToCharArray()))
                        {
                            if (s != null && s != "")
                            {
                                years.Add(Int32.Parse(s));
                            }
                        }
                    }
                }

                return years;
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
        /// Lookup a maintenance plans for a specific vehicle.
        /// </summary>
        /// <param name="registry">The <see cref="Registry"/> currently in use.</param>
        /// <param name="vehicle">The <see cref="Vehicle"/> for which to find the plan.</param>
        /// <param name="type">The <see cref="ScheduleMaintenanceType"/> type of schedule maintenance to lookup</param>
        /// <returns>A <see cref="ScheduleMaintenancePlan"/> object.</returns>
        public static ScheduleMaintenancePlan LookupScheduleMaintenancePlan(Registry registry, Vehicle vehicle, ScheduleMaintenanceType type)
        {
            return GetByVehicle(registry, type, vehicle.Year, vehicle.Make, vehicle.Model, vehicle.TrimLevel, vehicle.TransmissionControlType, vehicle.EngineVINCode, vehicle.EngineType);
        }

        /// <summary>
        /// Gets a schedule maintenance plan that match the vehicle info supplied.
        /// </summary>
        /// <param name="registry">The <see cref="Registry"/> curently in use.</param>
        /// <param name="type"><see cref="ScheduleMaintenanceType"/> type of scheduled maintenance</param>
        /// <param name="year">The <see cref="int"/> vehicle year.</param>
        /// <param name="make">The <see cref="string"/> vehicle make.</param>
        /// <param name="model">The <see cref="string"/> vehicle model.</param>
        /// <param name="trimLevel">The <see cref="string"/> vehicle trim level.</param>
        /// <param name="transmission">The <see cref="string"/> vehicle transmission.</param>
        /// <param name="engineVINCode">The <see cref="string"/> vehicle engine VIN code.</param>
        /// <param name="engineType">The <see cref="string"/> vehicle engine type.</param>
        /// <returns>A <see cref="ScheduleMaintenancePlan"/> object or NULL if none found.</returns>
        public static ScheduleMaintenancePlan GetByVehicle(Registry registry, ScheduleMaintenanceType type, int? year, string make, string model, string trimLevel, string transmission, string engineVINCode, string engineType)
        {
            ScheduleMaintenancePlan plan = null;
            ScheduleMaintenancePlanCollection plans = new ScheduleMaintenancePlanCollection(registry);

            SqlProcedureCommand call = new SqlProcedureCommand();
            call.ProcedureName = "ScheduleMaintenancePlan_LoadByVehicleInfo";

            call.AddInt32("Type", (int)type);
            if (year.HasValue)
            {
                call.AddInt32("Year", year.Value);
            }

            call.AddNVarChar("Make", make);
            call.AddNVarChar("Model", model);
            call.AddNVarChar("TrimLevel", trimLevel);
            call.AddNVarChar("Transmission", transmission);
            call.AddNVarChar("EngineVINCode", engineVINCode);

            //Added on 2019-05-22 12:32 PM by INNOVA Dev Team
            if (!string.IsNullOrEmpty(engineType))
                call.AddNVarChar("EngineType", engineType);

            //call.AddNVarChar("EngineType", engineType); //Commented out on 2019-05-22 12:30 PM by INNOVA Dev Team

            plans.Load(call, "ScheduleMaintenancePlanId", true, true);

            if (plans.Count > 0)
            {
                plan = plans[0];
            }

            return plan;
        }

        /// <summary>
        /// Gets a collection of the next scheduled services based on the mileage provided.
        /// </summary>
        /// <param name="vehicle">The <see cref="Vehicle"/> to get the scheduled maintenance service for.</param>
        /// <param name="lookupFixesPartsAndLabor"><see cref="bool"/> flag indicating whether or not to lookup services parts and labor</param>
        /// <param name="date">The <see cref="DateTime"/> for which to calculate the mileage.</param>
        /// <param name="milesDrivenPerDay">The <see cref="int"/> estimated miles driven per day.</param>
        /// <param name="useSingleDayMileageWindow">A <see cref="bool"/> that indicates if only a single day's mileage window should be looked for.</param>
        /// <returns>A <see cref="ScheduleMaintenancePlanDetailCollection"/> of <see cref="ScheduleMaintenancePlanDetail"/> objects.</returns>
        public ScheduleMaintenancePlanDetailCollection GetNextServices(Vehicle vehicle, bool lookupFixesPartsAndLabor, DateTime date, int milesDrivenPerDay, bool useSingleDayMileageWindow)
        {
            //determine the vehicle's current mileage based on the date and the miles driven per day
            int mileage = vehicle.GetEstimatedMileage(date, milesDrivenPerDay);

            int maxMiles = 9999999;
            //if using the single day window, then the end of the range is the mileage plus the miles driven per day
            if (useSingleDayMileageWindow)
            {
                maxMiles = mileage + milesDrivenPerDay;
            }
            //get the next services by the vehicle info
            ScheduleMaintenancePlanDetailCollection planDetails = this.GetNextServicesByVehicleInfo(mileage, maxMiles, lookupFixesPartsAndLabor, vehicle.ManufacturerName, vehicle.Make, vehicle.Model, vehicle.Year, vehicle.TrimLevel, vehicle.EngineType, vehicle.EngineVINCode, vehicle.TransmissionControlType);
            ScheduleMaintenancePlanDetailCollection nextPlanDetails = new ScheduleMaintenancePlanDetailCollection(this.Registry);

            if (useSingleDayMileageWindow)
            {
                nextPlanDetails = planDetails;
            }
            else
            {
                int nextMileageInterval = 0;
                foreach (ScheduleMaintenancePlanDetail plan in planDetails)
                {
                    int planNextMileage = plan.GetNextServiceMileageInterval(mileage);
                    if (nextMileageInterval == 0 || planNextMileage < nextMileageInterval)
                    {
                        nextMileageInterval = planNextMileage;
                    }
                }

                foreach (ScheduleMaintenancePlanDetail plan in planDetails)
                {
                    if (plan.GetNextServiceMileageInterval(mileage) == nextMileageInterval)
                    {
                        nextPlanDetails.Add(plan);
                    }
                }
            }

            return nextPlanDetails;
        }

        /// <summary>
        /// Gets a collection of the next scheduled services based on the mileage provided.
        /// </summary>
        /// <param name="currentVehicleMileage"><see cref="int"/> minimum mileage for the next service, the service will occur inside this mileage (this is usually the current vehicle projected mileage)</param>
        /// <param name="mileageMax"><see cref="int"/> maximum mileage for the nex service (enter 9,999,999 for no end range)</param>
        /// <param name="lookupFixesPartsAndLabor"><see cref="bool"/> flag indicating whether or not to lookup services parts and labor</param>
        /// <param name="manufacturer"><see cref="string"/> manufacturer of the vehicle</param>
        /// <param name="make"><see cref="string"/> make of the vehicle</param>
        /// <param name="model"><see cref="string"/> model of the vehicle</param>
        /// <param name="year"><see cref="int"/> (nullable) year of the vehicle</param>
        /// <param name="trimLevel"><see cref="string"/> trim level of the vehicle</param>
        /// <param name="engineType"><see cref="string"/> engine type</param>
        /// <param name="engineVINCode"><see cref="string"/> engine VIN Code</param>
        /// <param name="transmission"><see cref="string"/> transmission</param>
        /// <returns>A <see cref="ScheduleMaintenancePlanDetailCollection"/> of <see cref="ScheduleMaintenancePlanDetail"/> objects.</returns>
        public ScheduleMaintenancePlanDetailCollection GetNextServicesByVehicleInfo(int currentVehicleMileage, int mileageMax, bool lookupFixesPartsAndLabor, string manufacturer, string make, string model, int? year, string trimLevel, string engineType, string engineVINCode, string transmission)
        {
            ScheduleMaintenancePlanDetailCollection nextServicesPlanDetails = new ScheduleMaintenancePlanDetailCollection(this.Registry);

            foreach (ScheduleMaintenancePlanDetail detail in this.PlanDetails)
            {
                int nextMileageInterval = detail.GetNextServiceMileageInterval(currentVehicleMileage);

                //if the service occurs between the range, then this is part of the services required
                //the next mileage interval is greater than the current vehicle mileage and the next service is less than the maximum
                //or the next mileage interval is greater than the current vehicle mileage and the maximum is not limited

                if (nextMileageInterval >= currentVehicleMileage && nextMileageInterval < mileageMax
                    || nextMileageInterval >= currentVehicleMileage && mileageMax == 0)
                {
                    //if scheduled maintenance we're fine, if unscheduled, the next interval has to be within 15,000 miles of the current vehicle mileage
                    if (this.Type == ScheduleMaintenanceType.ScheduledMaintenance || (currentVehicleMileage + 15000 >= nextMileageInterval))
                    {
                        nextServicesPlanDetails.Add(detail);
                    }
                }
            }

            //populate the fixes for these services (if they exist)
            if (lookupFixesPartsAndLabor)
            {
                nextServicesPlanDetails.RelationPopulateFixes(make, model, year, trimLevel, engineType, engineVINCode, transmission);
            }

            //sort them by the name (translated)
            SortDictionaryCollection sorts = new SortDictionaryCollection();
            sorts.Add(new SortDictionary("ScheduleMaintenanceService.FixName.Description_Translated", SortDirection.Ascending));
            nextServicesPlanDetails.Sort(sorts);

            return nextServicesPlanDetails;
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

                if (os != null && os != "")
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

        /// <summary>
        /// Gets a description of the plan.
        /// </summary>
        /// <returns>A <see cref="string"/> description of the plan.</returns>
        public override string ToString()
        {
            // A StringBuilder with which to build the description.
            StringBuilder sb = new StringBuilder();

            // Sort the years
            this.Years.Sort();

            // A variable to store the 1st year in a potential span.
            int firstYearInSpan = 0;

            // A variable to store the last year examined in the loop.
            int lastYear = 0;

            // A bool to keep track of whether we are in the middle of a consecutive year span.
            bool inSpan = false;

            // Loop through the years
            foreach (int i in this.Years)
            {
                // Set the 1st year in span if it's the 1st time in the loop.
                if (firstYearInSpan == 0)
                {
                    firstYearInSpan = i;
                }

                // If this is not the 1st time in the loop . . .
                if (lastYear != 0)
                {
                    // If the difference is equal to 1 then we are in a consecutive year span.
                    if (i - lastYear == 1)
                    {
                        inSpan = true;
                    }
                    else // Append the next year value(s) to the description.
                    {
                        // Get the year value
                        sb.Append(this.GetYearValue(inSpan, firstYearInSpan, lastYear, (sb.Length > 0)));

                        // We are no longer in a span
                        inSpan = false;

                        // We are potentially starting a new span
                        firstYearInSpan = i;
                    }
                }

                // Set the lastYear to the current value.
                lastYear = i;
            }

            // Get the final year value
            sb.Append(this.GetYearValue(inSpan, firstYearInSpan, lastYear, (sb.Length > 0)));

            // Append the makes
            if (this.makesString != "")
            {
                sb.Append(" " + this.makesString.Replace("|", ","));
            }

            // Append the models
            if (this.modelsString != "")
            {
                sb.Append(" " + this.modelsString.Replace("|", ","));
            }

            // Append the engines
            if (this.engineTypesString != "")
            {
                sb.Append(" " + this.engineTypesString.Replace("|", ","));
            }

            return sb.ToString();
        }

        private string GetYearValue(bool inSpan, int firstYearInSpan, int lastYear, bool prependComma)
        {
            string yearValue = "";

            if (prependComma)
            {
                yearValue = ",";
            }

            if (inSpan)
            {
                yearValue += firstYearInSpan.ToString() + "-" + lastYear.ToString();
            }
            else
            {
                yearValue += lastYear.ToString();
            }

            return yearValue;
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
                //load the base scheduleMaintenancePlan if user selected it.
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
            dr.ProcedureName = "ScheduleMaintenancePlan_Load";
            dr.AddGuid("ScheduleMaintenancePlanId", this.Id);
        }

        /// <summary>
        /// Sets the properties of this object from the fields in the recordset located in the supplied <see cref="SqlDataReaderWrapper"/>.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> containing the recordset and fields used to set the properties of this object (this layer only).</param>
        protected new void SetPropertiesFromDataReader(SqlDataReaderWrapper dr)
        {
            this.type = (ScheduleMaintenanceType)dr.GetInt32("Type");
            this.manufacturerName = dr.GetString("ManufacturerName");
            this.name = dr.GetString("Name");
            this.maxMileage = dr.GetInt32("MaxMileage");
            if (!dr.IsDBNull("AdminUserIdCreatedBy"))
            {
                this.createdByAdminUser = (AdminUser)Registry.CreateInstance(typeof(AdminUser), dr.GetGuid("AdminUserIdCreatedBy"));
            }
            if (!dr.IsDBNull("AdminUserIdUpdatedBy"))
            {
                this.updatedByAdminUser = (AdminUser)Registry.CreateInstance(typeof(AdminUser), dr.GetGuid("AdminUserIdUpdatedBy"));
            }
            this.hasEngineTypeDefined = dr.GetBoolean("HasEngineTypeDefined");
            this.engineTypesString = dr.GetString("EngineTypesString");
            this.hasEngineVINCodeDefined = dr.GetBoolean("HasEngineVINCodeDefined");
            this.engineVINCodesString = dr.GetString("EngineVINCodesString");
            this.hasYearDefined = dr.GetBoolean("HasYearDefined");
            this.yearsString = dr.GetString("YearsString");
            this.hasMakeDefined = dr.GetBoolean("HasMakeDefined");
            this.makesString = dr.GetString("MakesString");
            this.hasModelDefined = dr.GetBoolean("HasModelDefined");
            this.modelsString = dr.GetString("ModelsString");
            this.hastrimLevelDefined = dr.GetBoolean("HastrimLevelDefined");
            this.trimLevelsString = dr.GetString("TrimLevelsString");
            this.hasTransmissionDefined = dr.GetBoolean("HasTransmissionDefined");
            this.transmissionsString = dr.GetString("TransmissionsString");
            this.isForHistoricalVehicles = dr.GetBoolean("IsForHistoricalVehicles");
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
            if (IsObjectDirty)
            {
                transaction = EnsureDatabasePrepared(connection, transaction);

                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    if (IsObjectCreated)
                    {
                        dr.ProcedureName = "ScheduleMaintenancePlan_Create";
                        this.CreatedDateTimeUTC = DateTime.Now.ToUniversalTime();
                        this.UpdatedDateTimeUTC = this.CreatedDateTimeUTC;
                    }
                    else
                    {
                        dr.ProcedureName = "ScheduleMaintenancePlan_Save";
                    }

                    dr.AddGuid("ScheduleMaintenancePlanId", this.Id);

                    dr.AddInt32("Type", (int)this.Type);

                    dr.AddNVarChar("ManufacturerName", this.ManufacturerName);
                    dr.AddNVarChar("Name", this.Name);
                    dr.AddInt32("MaxMileage", this.MaxMileage);
                    if (this.CreatedByAdminUser != null)
                    {
                        dr.AddGuid("AdminUserIdCreatedBy", this.CreatedByAdminUser.Id);
                    }
                    if (this.UpdatedByAdminUser != null)
                    {
                        dr.AddGuid("AdminUserIdUpdatedBy", this.UpdatedByAdminUser.Id);
                    }

                    dr.AddBoolean("HasEngineTypeDefined", this.HasEngineTypeDefined);
                    if (this.isEngineTypesDirty)
                    {
                        this.engineTypesString = this.BuildStringList(this.EngineTypes);
                    }
                    dr.AddNVarChar("EngineTypesString", this.engineTypesString);

                    dr.AddBoolean("HasEngineVINCodeDefined", this.HasEngineVINCodeDefined);
                    if (this.isEngineVINCodesDirty)
                    {
                        this.engineVINCodesString = this.BuildStringList(this.EngineVINCodes);
                    }
                    dr.AddNVarChar("EngineVINCodesString", this.engineVINCodesString);

                    dr.AddBoolean("HasYearDefined", this.HasYearDefined);
                    if (this.isYearsDirty)
                    {
                        this.yearsString = this.BuildStringList(this.Years);
                    }
                    dr.AddNVarChar("YearsString", this.yearsString);

                    dr.AddBoolean("HasMakeDefined", this.HasMakeDefined);
                    if (this.isMakesDirty)
                    {
                        this.makesString = this.BuildStringList(this.Makes);
                    }
                    dr.AddNVarChar("MakesString", this.makesString);

                    dr.AddBoolean("HasModelDefined", this.HasModelDefined);
                    if (this.isModelsDirty)
                    {
                        this.modelsString = this.BuildStringList(this.Models);
                    }
                    dr.AddNVarChar("ModelsString", this.modelsString);

                    dr.AddBoolean("HasTrimLevelDefined", this.HasTrimLevelDefined);
                    if (this.isTrimLevelsDirty)
                    {
                        this.trimLevelsString = this.BuildStringList(this.TrimLevels);
                    }
                    dr.AddNVarChar("TrimLevelsString", this.trimLevelsString);

                    dr.AddBoolean("HasTransmissionDefined", this.HasTransmissionDefined);
                    if (this.isTransmissionsDirty)
                    {
                        this.transmissionsString = this.BuildStringList(this.TransmissionControlTypes);
                    }
                    dr.AddNVarChar("TransmissionsString", this.transmissionsString);

                    dr.AddBoolean("IsForHistoricalVehicles", this.isForHistoricalVehicles);
                    dr.AddDateTime("CreatedDateTimeUTC", this.CreatedDateTimeUTC);
                    dr.AddDateTime("UpdatedDateTimeUTC", this.UpdatedDateTimeUTC);

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
            if (this.planDetails != null)
            {

                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    dr.ProcedureName = "ScheduleMaintenancePlanDetail_DeleteByScheduleMaintenancePlan";
                    dr.AddGuid("ScheduleMaintenancePlanId", this.Id);
                    dr.ExecuteNonQuery(transaction);
                }

                // save each object in the related object
                foreach (ScheduleMaintenancePlanDetail pd in this.PlanDetails)
                {
                    transaction = pd.Save(connection, transaction);
                }
            }

            if (this.isTrimLevelsDirty)
            {
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    dr.ProcedureName = "ScheduleMaintenancePlan_SaveTrimLevels";
                    dr.AddGuid("ScheduleMaintenancePlanId", Id);
                    dr.AddNText("TrimLevelsXmlList", Metafuse3.Xml.XmlList.ToXml(this.TrimLevels));
                    dr.ExecuteNonQuery(transaction);
                }
                this.isTrimLevelsDirty = false;
            }

            if (this.isEngineVINCodesDirty)
            {
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    dr.ProcedureName = "ScheduleMaintenancePlan_SaveEngineVINCode";
                    dr.AddGuid("ScheduleMaintenancePlanId", Id);
                    dr.AddNText("XmlList", Metafuse3.Xml.XmlList.ToXml(this.EngineVINCodes));
                    dr.ExecuteNonQuery(transaction);
                }
                this.isEngineVINCodesDirty = false;
            }

            if (this.isEngineTypesDirty)
            {
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    dr.ProcedureName = "ScheduleMaintenancePlan_SaveEngineType";
                    dr.AddGuid("ScheduleMaintenancePlanId", Id);
                    dr.AddNText("XmlList", Metafuse3.Xml.XmlList.ToXml(this.EngineTypes));
                    dr.ExecuteNonQuery(transaction);
                }
                this.isEngineTypesDirty = false;
            }

            if (this.isMakesDirty)
            {
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    dr.ProcedureName = "ScheduleMaintenancePlan_SaveMakes";
                    dr.AddGuid("ScheduleMaintenancePlanId", Id);
                    dr.AddNText("MakesXmlList", Metafuse3.Xml.XmlList.ToXml(this.Makes));
                    dr.ExecuteNonQuery(transaction);
                }
                this.isMakesDirty = false;
            }
            if (this.isModelsDirty)
            {
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    dr.ProcedureName = "ScheduleMaintenancePlan_SaveModels";
                    dr.AddGuid("ScheduleMaintenancePlanId", Id);
                    dr.AddNText("ModelsXmlList", Metafuse3.Xml.XmlList.ToXml(this.Models).Replace("&amp;", "&").Replace("&", "&amp;"));
                    dr.ExecuteNonQuery(transaction);
                }
                this.isModelsDirty = false;
            }
            if (this.isYearsDirty)
            {
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    dr.ProcedureName = "ScheduleMaintenancePlan_SaveYears";
                    dr.AddGuid("ScheduleMaintenancePlanId", Id);
                    dr.AddNText("YearsXmlList", Metafuse3.Xml.XmlList.ToXml(this.Years));
                    dr.ExecuteNonQuery(transaction);
                }
                this.isYearsDirty = false;
            }
            if (this.isTransmissionsDirty)
            {
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    dr.ProcedureName = "ScheduleMaintenancePlan_SaveTransmissionControlTypes";
                    dr.AddGuid("ScheduleMaintenancePlanId", Id);
                    dr.AddNText("TransmissionControlTypesXmlList", Metafuse3.Xml.XmlList.ToXml(TransmissionControlTypes));
                    dr.ExecuteNonQuery(transaction);
                }
                this.isTransmissionsDirty = false;
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
            EnsureValidId();

            transaction = EnsureDatabasePrepared(connection, transaction);

            //Copy deleted ScheduleMaintenancePlan to Audit DB first
            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                dr.ProcedureName = "Audit_CopySM";
                dr.AddGuid("ScheduleMaintenancePlanId", Id);
                dr.ExecuteNonQuery(transaction);
            }

            //Copy deleted ScheduleMaintenancePlan to Audit DB first

            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                dr.ProcedureName = "ScheduleMaintenancePlanTrimLevel_DeleteByScheduleMaintenancePlan";
                dr.AddGuid("ScheduleMaintenancePlanId", Id);
                dr.ExecuteNonQuery(transaction);
            }

            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                dr.ProcedureName = "ScheduleMaintenancePlanEngineType_DeleteByScheduleMaintenancePlan";
                dr.AddGuid("ScheduleMaintenancePlanId", Id);
                dr.ExecuteNonQuery(transaction);
            }

            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                dr.ProcedureName = "ScheduleMaintenancePlanEngineVINCode_DeleteByScheduleMaintenancePlan";
                dr.AddGuid("ScheduleMaintenancePlanId", Id);
                dr.ExecuteNonQuery(transaction);
            }

            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                dr.ProcedureName = "ScheduleMaintenancePlanMake_DeleteByScheduleMaintenancePlan";
                dr.AddGuid("ScheduleMaintenancePlanId", Id);
                dr.ExecuteNonQuery(transaction);
            }

            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                dr.ProcedureName = "ScheduleMaintenancePlanModel_DeleteByScheduleMaintenancePlan";
                dr.AddGuid("ScheduleMaintenancePlanId", Id);
                dr.ExecuteNonQuery(transaction);
            }

            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                dr.ProcedureName = "ScheduleMaintenancePlanTransmission_DeleteByScheduleMaintenancePlan";
                dr.AddGuid("ScheduleMaintenancePlanId", Id);
                dr.ExecuteNonQuery(transaction);
            }

            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                dr.ProcedureName = "ScheduleMaintenancePlanYear_DeleteByScheduleMaintenancePlan";
                dr.AddGuid("ScheduleMaintenancePlanId", Id);
                dr.ExecuteNonQuery(transaction);
            }

            // delete this current object
            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                // Delete the details
                dr.ProcedureName = "ScheduleMaintenancePlanDetail_DeleteByScheduleMaintenancePlan";
                dr.AddGuid("ScheduleMaintenancePlanId", Id);

                dr.ExecuteNonQuery(transaction);
                dr.ClearParameters();

                //delete the ScheduleMaintenancePlan
                dr.ProcedureName = "ScheduleMaintenancePlan_Delete";
                dr.AddGuid("ScheduleMaintenancePlanId", Id);

                dr.ExecuteNonQuery(transaction);
            }

            transaction = base.Delete(connection, transaction);

            return transaction;
        }

        #endregion Required Object Methods (Load, Save, Delete, Etc)
    }
}
using Innova.Users;
using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using Metafuse3.NullableTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;

namespace Innova.Vehicles
{
    /// <summary>
    /// The VehicleWarranty object handles the business logic and data access for the specialized business object, VehicleWarranty.
    /// The class inherits from <see cref="BusinessObjectBase"/> class.
    /// </summary>
    /// <example>
    /// See the examples of how to construct the VehicleWarranty object.
    ///
    /// To create a new instance of a new of VehicleWarranty.
    /// <code>VehicleWarranty o = (VehicleWarranty)this.Registry.CreateInstance(typeof(VehicleWarranty));</code>
    ///
    /// To create an new instance of an existing VehicleWarranty.
    /// <code>VehicleWarranty o = (VehicleWarranty)this.Registry.CreateInstance(typeof(VehicleWarranty), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of VehicleWarranty, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectBase.ConnectionString"/> to the <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable()]
    public class VehicleWarranty : InnovaBusinessObjectBase
    {
        private string name = "";
        private string name_es = "";
        private string name_fr = "";
        private string name_zh = "";
        private bool isTransferable;
        private AdminUser adminUserCreatedBy;
        private AdminUser adminUserUpdatedBy;
        private NullableInt32 minYear = NullableInt32.Null;
        private NullableInt32 maxYear = NullableInt32.Null;

        private bool hasEngineTypeDefined = false;
        private string engineTypesString = "";
        private bool isEngineTypesDirty = false;

        private bool hasEngineVINCodeDefined;
        private string engineVINCodesString = "";
        private bool isEngineVINCodesDirty = false;

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

        private List<string> engineTypes;
        private List<string> engineVINCodes;
        private List<string> makes;
        private List<string> models;
        private List<string> trimLevels;
        private List<string> transmissionControlTypes;

        private DateTime createdDateTimeUTC = DateTime.MinValue;
        private DateTime updatedDateTimeUTC = DateTime.MinValue;

        private VehicleWarrantyDetailCollection vehicleWarrantyDetails;

        #region Contructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new (one that does not exist, assigning a new <see cref="Guid"/> Id). VehicleWarranty object.
        /// In order to create a new VehicleWarranty which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <example>
        /// <code>
        /// VehicleWarranty o = (VehicleWarranty)Registry.CreateInstance(typeof(VehicleWarranty));
        /// </code>
        /// </example>
        protected internal VehicleWarranty()
            : base()
        {
            this.IsObjectCreated = true;
            this.IsObjectLoaded = true;
            this.IsObjectDirty = true;
        }

        /// <summary>
        /// Constructor creates an existing (one that exists, and already has a <see cref="Guid"/> Id).  VehicleWarranty object.
        /// In order to create an existing VehicleWarranty object, which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <param name="id">The <see cref="Guid"/> or SQL uniqueidentifier indentification "Id" of the current object.</param>
        /// <example>
        /// <code>
        /// VehicleWarranty o = (VehicleWarranty)Registry.CreateInstance(typeof(VehicleWarranty), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));
        /// </code>
        /// </example>
        protected internal VehicleWarranty(Guid id)
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
        /// Gets or sets the <see cref="string"/> .
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
        /// Gets or sets the <see cref="string"/> name in Spanish.
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
                if (this.name_es != value)
                {
                    this.IsObjectDirty = true;
                    this.name_es = value;
                    this.UpdatedField("Name_es");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> name in French.
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
                if (this.name_fr != value)
                {
                    this.IsObjectDirty = true;
                    this.name_fr = value;
                    this.UpdatedField("Name_fr");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> name in Mandarin Chinese.
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
                if (this.name_zh != value)
                {
                    this.IsObjectDirty = true;
                    this.name_zh = value;
                    this.UpdatedField("Name_zh");
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> name in the language specified in the Registry.
        /// </summary>
        public string Name_Translated
        {
            get
            {
                return this.RuntimeInfo.GetTranslatedValue(this.Name, this.Name_es, this.Name_fr, this.Name_zh);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/> .
        /// </summary>
        public bool IsTransferable
        {
            get
            {
                this.EnsureLoaded();
                return this.isTransferable;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.isTransferable)
                {
                    this.IsObjectDirty = true;
                    this.isTransferable = value;
                    this.UpdatedField("IsTransferable");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableInt32"/> .
        /// </summary>
        public NullableInt32 MinYear
        {
            get
            {
                this.EnsureLoaded();
                return this.minYear;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.minYear)
                {
                    this.IsObjectDirty = true;
                    this.minYear = value;
                    this.UpdatedField("MinYear");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableInt32"/> .
        /// </summary>
        public NullableInt32 MaxYear
        {
            get
            {
                this.EnsureLoaded();
                return this.maxYear;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.maxYear)
                {
                    this.IsObjectDirty = true;
                    this.maxYear = value;
                    this.UpdatedField("MaxYear");
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
        /// Gets or sets the <see cref="AdminUserCreatedBy"/> .
        /// </summary>
        public AdminUser AdminUserCreatedBy
        {
            get
            {
                this.EnsureLoaded();
                return this.adminUserCreatedBy;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.adminUserCreatedBy)
                {
                    this.IsObjectDirty = true;
                    this.adminUserCreatedBy = value;
                    this.UpdatedField("AdminUserIdCreatedBy");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="AdminUserUpdatedBy"/> .
        /// </summary>
        public AdminUser AdminUserUpdatedBy
        {
            get
            {
                this.EnsureLoaded();
                return this.adminUserUpdatedBy;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.adminUserUpdatedBy)
                {
                    this.IsObjectDirty = true;
                    this.adminUserUpdatedBy = value;
                    this.UpdatedField("AdminUserIdUpdatedBy");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/> .
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
        /// Gets or sets the <see cref="DateTime"/> .
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

        /// <summary>
        /// Gets the <see cref="VehicleWarrantyDetailCollection"/> associated with this <see cref="VehicleWarranty"/>.
        /// </summary>
        public VehicleWarrantyDetailCollection VehicleWarrantyDetails
        {
            get
            {
                if (this.vehicleWarrantyDetails == null)
                {
                    this.vehicleWarrantyDetails = new VehicleWarrantyDetailCollection(Registry);

                    //load if not a user created element
                    if (!this.isObjectCreated)
                    {
                        SqlProcedureCommand call = new SqlProcedureCommand();
                        this.EnsureValidId();

                        call.ProcedureName = "VehicleWarrantyDetail_LoadByVehicleWarranty";
                        call.AddGuid("VehicleWarrantyId", this.Id);

                        this.vehicleWarrantyDetails.Load(call, "VehicleWarrantyDetailId", true, true);
                    }
                }

                return this.vehicleWarrantyDetails;
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> of vehicle engine types that apply to this fix.
        /// NOTE: DO NOT add to this collection directly. Use the AddEngineType() method.
        /// </summary>
        public List<string> EngineTypes
        {
            get
            {
                if (engineTypes == null)
                {
                    engineTypes = new List<string>();

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
        /// Gets the <see cref="string"/> of vehicle engine VIN codes that apply to this fix.
        /// NOTE: DO NOT add to this collection directly. Use the AddEngineVINCode() method.
        /// </summary>
        public List<string> EngineVINCodes
        {
            get
            {
                if (engineVINCodes == null)
                {
                    engineVINCodes = new List<string>();

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
        /// Get the <see cref="string"/> of vehicle makes that apply to this fix.
        /// NOTE: DO NOT add to this collection directly. Use the AddMake() method.
        /// </summary>
        public List<string> Makes
        {
            get
            {
                if (makes == null)
                {
                    makes = new List<string>();

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
        /// Get the <see cref="string"/> of vehicle models that apply to this fix.
        /// NOTE: DO NOT add to this collection directly. Use the AddModel() method.
        /// </summary>
        public List<string> Models
        {
            get
            {
                if (models == null)
                {
                    models = new List<string>();

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
        /// Get the <see cref="string"/> of vehicle trim levels that apply to this fix.
        /// NOTE: DO NOT add to this collection directly. Use the AddTrimLevel() method.
        /// </summary>
        public List<string> TrimLevels
        {
            get
            {
                if (trimLevels == null)
                {
                    trimLevels = new List<string>();

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
        /// Get the <see cref="string"/> of transmission control types that apply to this fix.
        /// NOTE: DO NOT add to this collection directly. Use the AddTransmissionControlType() method.
        /// </summary>
        public List<string> TransmissionControlTypes
        {
            get
            {
                if (transmissionControlTypes == null)
                {
                    transmissionControlTypes = new List<string>();

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

        #endregion Object Properties (Related Objects)

        #region Business Logic Methods

        /***********************************************************************************************
		 *
		 * Custom Business Logic
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Gets a currently valid warranty for the vehicle provided.
        /// </summary>
        /// <param name="registry">The <see cref="Registry"/> currently in use.</param>
        /// <param name="vehicle">The <see cref="Vehicle"/> the warranty should apply to.</param>
        /// <param name="averageMilesDrivenPerDay">The <see cref="int"/> average miles driven per day.</param>
        /// <returns>A <see cref="VehicleWarranty"/> object.</returns>
        public static VehicleWarranty GetCurrentlyValidWarranty(Registry registry, Vehicle vehicle, int averageMilesDrivenPerDay)
        {
            return VehicleWarranty.GetCurrentlyValidWarranty(registry, vehicle.Year, vehicle.Make, vehicle.Model, vehicle.TrimLevel, vehicle.TransmissionControlType, vehicle.EngineVINCode, vehicle.EngineType, vehicle.GetEstimatedMileage(DateTime.Now, averageMilesDrivenPerDay));
        }

        /// <summary>
        /// Gets a currently valid warranty for the vehicle provided.
        /// </summary>
        /// <param name="registry">The <see cref="Registry"/> currently in use.</param>
        /// <param name="year">The <see cref="int"/> vehicle year.</param>
        /// <param name="make">The <see cref="string"/> vehicle make.</param>
        /// <param name="model">The <see cref="string"/> vehicle model.</param>
        /// <param name="trimLevel">The <see cref="string"/> vehicle trim.</param>
        /// <param name="transmission">The <see cref="string"/> vehicle transmission.</param>
        /// <param name="engineVINCode">The <see cref="string"/> vehicle engine VIN code.</param>
        /// <param name="engineType">The <see cref="string"/> vehicle engine type.</param>
        /// <param name="currentMileage">The <see cref="int"/> vehicle's mileage.</param>
        /// <returns>A <see cref="VehicleWarranty"/> object.</returns>
        public static VehicleWarranty GetCurrentlyValidWarranty(Registry registry, int? year, string make, string model, string trimLevel, string transmission, string engineVINCode, string engineType, int currentMileage)
        {
            VehicleWarrantyCollection warranties = new VehicleWarrantyCollection(registry);
            VehicleWarranty validWarranty = null;

            SqlProcedureCommand call = new SqlProcedureCommand();
            call.ProcedureName = "VehicleWarranty_LoadByVehicleInfo";
            call.AddInt32("Year", year);
            call.AddNVarChar("Make", make);
            call.AddNVarChar("Model", model);
            call.AddNVarChar("TrimLevel", trimLevel);
            call.AddNVarChar("Transmission", transmission);
            call.AddNVarChar("EngineVINCode", engineVINCode);
            call.AddNVarChar("EngineType", engineType);
            call.AddInt32("CurrentMileage", currentMileage);

            warranties.Load(call, "VehicleWarrantyId", true, true);

            if (warranties.Count > 0)
            {
                validWarranty = warranties[0];
            }

            return validWarranty;
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
            dr.ProcedureName = "VehicleWarranty_Load";
            dr.AddGuid("VehicleWarrantyId", this.Id);
        }

        /// <summary>
        /// Sets the properties of this object from the fields in the recordset located in the supplied <see cref="SqlDataReaderWrapper"/>.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> containing the recordset and fields used to set the properties of this object (this layer only).</param>
        protected new void SetPropertiesFromDataReader(SqlDataReaderWrapper dr)
        {
            this.name = dr.GetString("Name");
            this.name_es = dr.GetString("Name_es");
            this.name_fr = dr.GetString("Name_fr");
            this.name_zh = dr.GetString("Name_zh");
            this.isTransferable = dr.GetBoolean("IsTransferable");
            this.minYear = dr.GetNullableInt32("MinYear");
            this.maxYear = dr.GetNullableInt32("MaxYear");
            this.hasEngineTypeDefined = dr.GetBoolean("HasEngineTypeDefined");
            this.engineTypesString = dr.GetString("EngineTypesString");
            this.hasEngineVINCodeDefined = dr.GetBoolean("HasEngineVINCodeDefined");
            this.engineVINCodesString = dr.GetString("EngineVINCodesString");
            this.hasMakeDefined = dr.GetBoolean("HasMakeDefined");
            this.makesString = dr.GetString("MakesString");
            this.hasModelDefined = dr.GetBoolean("HasModelDefined");
            this.modelsString = dr.GetString("ModelsString");
            this.hastrimLevelDefined = dr.GetBoolean("HastrimLevelDefined");
            this.trimLevelsString = dr.GetString("TrimLevelsString");
            this.hasTransmissionDefined = dr.GetBoolean("HasTransmissionDefined");
            this.transmissionsString = dr.GetString("TransmissionsString");

            this.adminUserCreatedBy = (AdminUser)dr.GetBusinessObjectBase(this.Registry, typeof(AdminUser), "AdminUserIdCreatedBy");
            this.adminUserUpdatedBy = (AdminUser)dr.GetBusinessObjectBase(this.Registry, typeof(AdminUser), "AdminUserIdUpdatedBy");
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
                        dr.ProcedureName = "VehicleWarranty_Create";
                        //Remove the lines below if they are not needed.
                        this.CreatedDateTimeUTC = DateTime.UtcNow;
                        this.UpdatedDateTimeUTC = this.CreatedDateTimeUTC;
                    }
                    else
                    {
                        dr.UpdateFields("VehicleWarranty", "VehicleWarrantyId", this.updatedFields);
                    }

                    dr.AddGuid("VehicleWarrantyId", this.Id);
                    dr.AddNVarChar("Name", this.Name);
                    dr.AddNVarChar("Name_es", this.Name_es);
                    dr.AddNVarChar("Name_fr", this.Name_fr);
                    dr.AddNVarChar("Name_zh", this.Name_zh);
                    dr.AddBoolean("IsTransferable", this.IsTransferable);
                    dr.AddInt32("MinYear", this.MinYear);
                    dr.AddInt32("MaxYear", this.MaxYear);

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

                    dr.AddBoolean("HasMakeDefined", HasMakeDefined);
                    if (this.isMakesDirty)
                    {
                        this.makesString = this.BuildStringList(this.Makes);
                    }
                    dr.AddNVarChar("MakesString", this.makesString);

                    dr.AddBoolean("HasModelDefined", HasModelDefined);
                    if (this.isModelsDirty)
                    {
                        this.modelsString = this.BuildStringList(this.Models);
                    }
                    dr.AddNVarChar("ModelsString", this.modelsString);

                    dr.AddBoolean("HasTrimLevelDefined", HasTrimLevelDefined);
                    if (this.isTrimLevelsDirty)
                    {
                        this.trimLevelsString = this.BuildStringList(this.TrimLevels);
                    }
                    dr.AddNVarChar("TrimLevelsString", this.trimLevelsString);

                    dr.AddBoolean("HasTransmissionDefined", HasTransmissionDefined);
                    if (this.isTransmissionsDirty)
                    {
                        this.transmissionsString = this.BuildStringList(this.TransmissionControlTypes);
                    }
                    dr.AddNVarChar("TransmissionsString", this.transmissionsString);

                    dr.AddBusinessObject("AdminUserIdCreatedBy", this.AdminUserCreatedBy);
                    dr.AddBusinessObject("AdminUserIdUpdatedBy", this.AdminUserUpdatedBy);
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
            if (this.vehicleWarrantyDetails != null)
            {
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    dr.ProcedureName = "VehicleWarrantyDetail_DeleteByVehicleWarranty";
                    dr.AddGuid("VehicleWarrantyId", this.Id);
                    dr.ExecuteNonQuery(transaction);
                }

                // save each object in the related object
                foreach (VehicleWarrantyDetail vwd in this.VehicleWarrantyDetails)
                {
                    transaction = vwd.Save(connection, transaction);
                }
            }

            if (this.isTrimLevelsDirty)
            {
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    dr.ProcedureName = "VehicleWarranty_SaveTrimLevels";
                    dr.AddGuid("VehicleWarrantyId", Id);
                    dr.AddNText("TrimLevelsXmlList", Metafuse3.Xml.XmlList.ToXml(this.TrimLevels));
                    dr.ExecuteNonQuery(transaction);
                }
                this.isTrimLevelsDirty = false;
            }

            if (this.isEngineVINCodesDirty)
            {
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    dr.ProcedureName = "VehicleWarranty_SaveEngineVINCode";
                    dr.AddGuid("VehicleWarrantyId", Id);
                    dr.AddNText("XmlList", Metafuse3.Xml.XmlList.ToXml(this.EngineVINCodes));
                    dr.ExecuteNonQuery(transaction);
                }
                this.isEngineVINCodesDirty = false;
            }

            if (this.isEngineTypesDirty)
            {
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    dr.ProcedureName = "VehicleWarranty_SaveEngineType";
                    dr.AddGuid("VehicleWarrantyId", Id);
                    dr.AddNText("XmlList", Metafuse3.Xml.XmlList.ToXml(this.EngineTypes));
                    dr.ExecuteNonQuery(transaction);
                }
                this.isEngineTypesDirty = false;
            }

            if (this.isMakesDirty)
            {
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    dr.ProcedureName = "VehicleWarranty_SaveMakes";
                    dr.AddGuid("VehicleWarrantyId", Id);
                    dr.AddNText("MakesXmlList", Metafuse3.Xml.XmlList.ToXml(this.Makes));
                    dr.ExecuteNonQuery(transaction);
                }
                this.isMakesDirty = false;
            }
            if (this.isModelsDirty)
            {
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    dr.ProcedureName = "VehicleWarranty_SaveModels";
                    dr.AddGuid("VehicleWarrantyId", Id);
                    dr.AddNText("ModelsXmlList", Metafuse3.Xml.XmlList.ToXml(this.Models).Replace("&amp;", "&").Replace("&", "&amp;"));
                    dr.ExecuteNonQuery(transaction);
                }
                this.isModelsDirty = false;
            }
            if (this.isTransmissionsDirty)
            {
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    dr.ProcedureName = "VehicleWarranty_SaveTransmissionControlTypes";
                    dr.AddGuid("VehicleWarrantyId", Id);
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
            this.EnsureValidId();

            transaction = this.EnsureDatabasePrepared(connection, transaction);

            // delete this current object
            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                dr.ProcedureName = "VehicleWarrantyTrimLevel_DeleteByVehicleWarranty";
                dr.AddGuid("VehicleWarrantyId", this.Id);
                dr.ExecuteNonQuery(transaction);
                dr.ClearParameters();

                dr.ProcedureName = "VehicleWarrantyEngineType_DeleteByVehicleWarranty";
                dr.AddGuid("VehicleWarrantyId", this.Id);
                dr.ExecuteNonQuery(transaction);
                dr.ClearParameters();

                dr.ProcedureName = "VehicleWarrantyEngineVINCode_DeleteByVehicleWarranty";
                dr.AddGuid("VehicleWarrantyId", this.Id);
                dr.ExecuteNonQuery(transaction);
                dr.ClearParameters();

                dr.ProcedureName = "VehicleWarrantyMake_DeleteByVehicleWarranty";
                dr.AddGuid("VehicleWarrantyId", this.Id);
                dr.ExecuteNonQuery(transaction);
                dr.ClearParameters();

                dr.ProcedureName = "VehicleWarrantyModel_DeleteByVehicleWarranty";
                dr.AddGuid("VehicleWarrantyId", this.Id);
                dr.ExecuteNonQuery(transaction);
                dr.ClearParameters();

                dr.ProcedureName = "VehicleWarrantyTransmission_DeleteByVehicleWarranty";
                dr.AddGuid("VehicleWarrantyId", this.Id);
                dr.ExecuteNonQuery(transaction);
                dr.ClearParameters();

                dr.ProcedureName = "VehicleWarrantyDetail_DeleteByVehicleWarranty";
                dr.AddGuid("VehicleWarrantyId", this.Id);
                dr.ExecuteNonQuery(transaction);
                dr.ClearParameters();

                //delete the item
                dr.ProcedureName = "VehicleWarranty_Delete";
                dr.AddGuid("VehicleWarrantyId", this.Id);
                dr.ExecuteNonQuery(transaction);
            }

            transaction = base.Delete(connection, transaction);

            return transaction;
        }

        #endregion Required Methods (Load, Save, Delete, Etc)
    }
}
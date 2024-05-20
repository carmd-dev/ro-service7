using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using System;
using System.Collections.Specialized;
using System.Data.SqlClient;

namespace Innova.ScheduleMaintenance
{
    /// <summary>
    /// The ScheduleMaintenancePlanDetail object handles the business logic and data access for the specialized business object, ScheduleMaintenancePlanDetail.
    /// The class inherits from <see cref="BusinessObjectBase"/> class.
    /// </summary>
    /// <example>
    /// See the examples of how to construct the ScheduleMaintenancePlanDetail object.
    ///
    /// To create a new instance of a new of ScheduleMaintenancePlanDetail.
    /// <code>ScheduleMaintenancePlanDetail o = (ScheduleMaintenancePlanDetail)Registry.CreateInstance(typeof(ScheduleMaintenancePlanDetail));</code>
    ///
    /// To create an new instance of an existing ScheduleMaintenancePlanDetail.
    /// <code>ScheduleMaintenancePlanDetail o = (ScheduleMaintenancePlanDetail)Registry.CreateInstance(typeof(ScheduleMaintenancePlanDetail), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of ScheduleMaintenancePlanDetail, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectBase.ConnectionString"/> to the <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable()]
    public class ScheduleMaintenancePlanDetail : BusinessObjectBase
    {
        // data object variables
        private ScheduleMaintenancePlan scheduleMaintenancePlan;

        private ScheduleMaintenanceService scheduleMaintenanceService;
        private int mileage;
        private int mileageRepeat;
        private int odometer;
        private int odometerRepeat;

        //#Sprint26
        private int mileageSevere;

        private int mileageRepeatSevere;
        private int odometerSevere;
        private int odometerRepeatSevere;

        #region System Constructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new (one that does not exist, assigning a new <see cref="Guid"/> Id). ScheduleMaintenancePlanDetail object.
        /// In order to create a new ScheduleMaintenancePlanDetail which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <example>
        /// <code>
        /// ScheduleMaintenancePlanDetail o = (ScheduleMaintenancePlanDetail)Registry.CreateInstance(typeof(ScheduleMaintenancePlanDetail));
        /// </code>
        /// </example>
        protected internal ScheduleMaintenancePlanDetail() : base()
        {
            IsObjectCreated = true;
            IsObjectLoaded = true;
            IsObjectDirty = true;
        }

        /// <summary>
        /// Constructor creates an existing (one that exists, and already has a <see cref="Guid"/> Id).  ScheduleMaintenancePlanDetail object.
        /// In order to create an existing ScheduleMaintenancePlanDetail object, which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <param name="id">The <see cref="Guid"/> or SQL uniqueidentifier indentification "Id" of the current object.</param>
        /// <example>
        /// <code>
        /// ScheduleMaintenancePlanDetail o = (ScheduleMaintenancePlanDetail)Registry.CreateInstance(typeof(ScheduleMaintenancePlanDetail), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));
        /// </code>
        /// </example>
        protected internal ScheduleMaintenancePlanDetail(Guid id) : base(id)
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

        private StringCollection updatedFields = null;

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
        /// Gets or sets the <see cref="ScheduleMaintenancePlan"/> this detail record is associated to.
        /// </summary>
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
        /// Gets or sets the <see cref="ScheduleMaintenanceService"/> associated to this detail record
        /// </summary>
        public ScheduleMaintenanceService ScheduleMaintenanceService
        {
            get
            {
                this.EnsureLoaded();
                return this.scheduleMaintenanceService;
            }
            set
            {
                this.EnsureLoaded();
                if (this.scheduleMaintenanceService != value)
                {
                    this.IsObjectDirty = true;
                    this.scheduleMaintenanceService = value;
                    this.UpdatedField("ScheduleMaintenanceServiceId");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/> mileage this detail begins at
        /// </summary>
        public int Mileage
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
        /// Gets or sets the <see cref="int"/> mileage repeat interval
        /// </summary>
        public int MileageRepeat
        {
            get
            {
                this.EnsureLoaded();
                return this.mileageRepeat;
            }
            set
            {
                this.EnsureLoaded();
                if (this.mileageRepeat != value)
                {
                    this.IsObjectDirty = true;
                    this.mileageRepeat = value;
                    this.UpdatedField("MileageRepeat");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/> odometer this detail begins at
        /// </summary>
        public int Odometer
        {
            get
            {
                this.EnsureLoaded();
                return this.odometer;
            }
            set
            {
                this.EnsureLoaded();
                if (this.odometer != value)
                {
                    this.IsObjectDirty = true;
                    this.odometer = value;
                    this.UpdatedField("Odometer");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/> odometer repeat interval
        /// </summary>
        public int OdometerRepeat
        {
            get
            {
                this.EnsureLoaded();
                return this.odometerRepeat;
            }
            set
            {
                this.EnsureLoaded();
                if (this.odometerRepeat != value)
                {
                    this.IsObjectDirty = true;
                    this.odometerRepeat = value;
                    this.UpdatedField("OdometerRepeat");
                }
            }
        }

        //#Sprint26
        /// <summary>
        /// Gets or sets the <see cref="int"/> mileageSevere this detail begins at
        /// </summary>
        public int MileageSevere
        {
            get
            {
                this.EnsureLoaded();
                return this.mileageSevere;
            }
            set
            {
                this.EnsureLoaded();
                if (this.mileageSevere != value)
                {
                    this.IsObjectDirty = true;
                    this.mileageSevere = value;
                    this.UpdatedField("MileageSevere");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/> mileage repeat Severe interval
        /// </summary>
        public int MileageRepeatSevere
        {
            get
            {
                this.EnsureLoaded();
                return this.mileageRepeatSevere;
            }
            set
            {
                this.EnsureLoaded();
                if (this.mileageRepeatSevere != value)
                {
                    this.IsObjectDirty = true;
                    this.mileageRepeatSevere = value;
                    this.UpdatedField("MileageRepeatSevere");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/> odometerSevere this detail begins at
        /// </summary>
        public int OdometerSevere
        {
            get
            {
                this.EnsureLoaded();
                return this.odometerSevere;
            }
            set
            {
                this.EnsureLoaded();
                if (this.odometerSevere != value)
                {
                    this.IsObjectDirty = true;
                    this.odometerSevere = value;
                    this.UpdatedField("OdometerSevere");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/> odometer repeatSevere interval
        /// </summary>
        public int OdometerRepeatSevere
        {
            get
            {
                this.EnsureLoaded();
                return this.odometerRepeatSevere;
            }
            set
            {
                this.EnsureLoaded();
                if (this.odometerRepeatSevere != value)
                {
                    this.IsObjectDirty = true;
                    this.odometerRepeatSevere = value;
                    this.UpdatedField("OdometerRepeatSevere");
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

        #endregion Object Properties (Related Objects)

        #region Business Logic Methods

        /***********************************************************************************************
		 *
		 * Custom Business Logic
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Gets the <see cref="int"/> (nullable) next service mileage interval that was last calculated by the method GetNextServiceMileageInterval();
        /// </summary>
        public int? NextServiceMileageInterval { get; private set; }

        /// <summary>
        /// Gets the <see cref="int"/> (nullable) next service KM interval that was last calculated by the method GetNextServiceKMInterval();
        /// </summary>
        public int? NextServiceODOInterval { get; private set; }

        /// <summary>
        /// Gets the <see cref="int"/> next service mileage for this service.
        /// </summary>
        /// <param name="currentMileage"><see cref="int"/> current vehicle mileage to calculate from.</param>
        /// <returns><see cref="int"/> next service mileage interval.</returns>
        public int GetNextServiceMileageInterval(int currentMileage)
        {
            //if the current mileage is before the first instance of this service mileage then return the mileage there
            if (currentMileage <= this.Mileage)
            {
                this.NextServiceMileageInterval = this.Mileage;
                return this.Mileage;
            }

            //this shouldn't happen, but avoid a divide by zero error here
            if (this.MileageRepeat <= 0)
            {
                this.NextServiceMileageInterval = 0;
                return 0;
            }

            //otherwise the current mileage is greater than the minimum mileage so we need to subtract the first instance from the main mileage
            currentMileage = currentMileage - this.Mileage;

            //now we need to divide the interval into the current mileage
            decimal intervals = (decimal)((decimal)currentMileage / (decimal)this.MileageRepeat);

            //if the interval is greater than the floor, then that means that the interval will be less than the current mileage
            if (intervals > decimal.Floor(intervals))
            {
                intervals = decimal.Floor(intervals) + 1;
            }

            //return the next interval here
            this.NextServiceMileageInterval = (this.MileageRepeat * (int)intervals) + this.Mileage;

            return this.NextServiceMileageInterval.Value;
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
                //load the base scheduleMaintenancePlanDetail if user selected it.
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
            dr.ProcedureName = "ScheduleMaintenancePlanDetail_Load";
            dr.AddGuid("ScheduleMaintenancePlanDetailId", this.Id);
        }

        /// <summary>
        /// Sets the properties of this object from the fields in the recordset located in the supplied <see cref="SqlDataReaderWrapper"/>.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> containing the recordset and fields used to set the properties of this object (this layer only).</param>
        protected new void SetPropertiesFromDataReader(SqlDataReaderWrapper dr)
        {
            this.scheduleMaintenancePlan = (ScheduleMaintenancePlan)Registry.CreateInstance(typeof(ScheduleMaintenancePlan), dr.GetGuid("ScheduleMaintenancePlanId"));
            this.scheduleMaintenanceService = (ScheduleMaintenanceService)Registry.CreateInstance(typeof(ScheduleMaintenanceService), dr.GetGuid("ScheduleMaintenanceServiceId"));
            this.mileage = dr.GetInt32("Mileage");
            this.mileageRepeat = dr.GetInt32("MileageRepeat");
            this.odometer = dr.GetInt32("Odometer");
            this.odometerRepeat = dr.GetInt32("OdometerRepeat");
            this.IsObjectLoaded = true;
            //#Sprint26
            this.mileageSevere = dr.GetInt32("MileageSevere");
            this.mileageRepeatSevere = dr.GetInt32("MileageRepeatSevere");
            this.odometerSevere = dr.GetInt32("OdometerSevere");
            this.odometerRepeatSevere = dr.GetInt32("OdometerRepeatSevere");
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
                        dr.ProcedureName = "ScheduleMaintenancePlanDetail_Create";
                    }
                    else
                    {
                        dr.UpdateFields("ScheduleMaintenancePlanDetail", "ScheduleMaintenancePlanDetailId", this.updatedFields);
                    }

                    dr.AddGuid("ScheduleMaintenancePlanDetailId", this.Id);
                    dr.AddBusinessObject("ScheduleMaintenancePlanId", this.ScheduleMaintenancePlan);
                    dr.AddBusinessObject("ScheduleMaintenanceServiceId", this.ScheduleMaintenanceService);
                    dr.AddInt32("Mileage", this.Mileage);
                    dr.AddInt32("MileageRepeat", this.MileageRepeat);

                    dr.AddInt32("Odometer", this.Odometer);
                    dr.AddInt32("OdometerRepeat", this.OdometerRepeat);

                    //#Sprint26
                    dr.AddInt32("MileageSevere", this.MileageSevere);
                    dr.AddInt32("MileageRepeatSevere", this.MileageRepeatSevere);
                    dr.AddInt32("OdometerSevere", this.OdometerSevere);
                    dr.AddInt32("OdometerRepeatSevere", this.OdometerRepeatSevere);

                    dr.Execute(transaction);
                }

                this.IsObjectDirty = false;
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
                //remove related objects with a specialized delete.
                /*
				dr.ProcedureName = "ScheduleMaintenancePlanDetailsOthers_Delete";
				dr.AddGuid("ScheduleMaintenancePlanDetailId", Id);

				dr.ExecuteNonQuery(transaction);
				dr.ClearParameters();
				*/

                //delete the scheduleMaintenancePlanDetail
                dr.ProcedureName = "ScheduleMaintenancePlanDetail_Delete";
                dr.AddGuid("ScheduleMaintenancePlanDetailId", Id);

                dr.ExecuteNonQuery(transaction);
            }

            transaction = base.Delete(connection, transaction);

            return transaction;
        }

        #endregion Required Object Methods (Load, Save, Delete, Etc)
    }
}
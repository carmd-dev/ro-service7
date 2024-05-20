using Innova.Users;
using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using System;
using System.Data.SqlClient;

namespace Innova.ObdFixes
{
    /// <summary>
    /// The ObdFixPaymentSchedule object handles the business logic and data access for the specialized business object, ObdFixPaymentSchedule.
    /// The class inherits from <see cref="BusinessObjectBase"/> class.
    /// </summary>
    /// <example>
    /// See the examples of how to construct the ObdFixPaymentSchedule object.
    ///
    /// To create a new instance of a new of ObdFixPaymentSchedule.
    /// <code>ObdFixPaymentSchedule o = (ObdFixPaymentSchedule)Registry.CreateInstance(typeof(ObdFixPaymentSchedule));</code>
    ///
    /// To create an new instance of an existing ObdFixPaymentSchedule.
    /// <code>ObdFixPaymentSchedule o = (ObdFixPaymentSchedule)Registry.CreateInstance(typeof(ObdFixPaymentSchedule), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of ObdFixPaymentSchedule, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectBase.ConnectionString"/> to the <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable()]
    public class ObdFixPaymentSchedule : BusinessObjectBase
    {
        private User user;
        private ObdFixPaymentType obdFixPaymentType;
        private decimal paymentAmount;

        #region System Constructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new (one that does not exist, assigning a new <see cref="Guid"/> Id). ObdFixPaymentSchedule object.
        /// In order to create a new ObdFixPaymentSchedule which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <example>
        /// <code>
        /// ObdFixPaymentSchedule o = (ObdFixPaymentSchedule)Registry.CreateInstance(typeof(ObdFixPaymentSchedule));
        /// </code>
        /// </example>
        protected internal ObdFixPaymentSchedule() : base()
        {
            IsObjectCreated = true;
            IsObjectLoaded = true;
            IsObjectDirty = true;
        }

        /// <summary>
        /// Constructor creates an existing (one that exists, and already has a <see cref="Guid"/> Id).  ObdFixPaymentSchedule object.
        /// In order to create an existing ObdFixPaymentSchedule object, which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <param name="id">The <see cref="Guid"/> or SQL uniqueidentifier indentification "Id" of the current object.</param>
        /// <example>
        /// <code>
        /// ObdFixPaymentSchedule o = (ObdFixPaymentSchedule)Registry.CreateInstance(typeof(ObdFixPaymentSchedule), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));
        /// </code>
        /// </example>
        protected internal ObdFixPaymentSchedule(Guid id) : base(id)
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
        /// Gets or sets the <see cref="User"/>
        /// </summary>
        [PropertyDefinition("User", "The user to which the payment schedule is assigned.")]
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
                    IsObjectDirty = true;
                    this.user = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="ObdFixPaymentType"/>
        /// </summary>
        [PropertyDefinition("Payment Type", "The payment type.")]
        public ObdFixPaymentType ObdFixPaymentType
        {
            get
            {
                this.EnsureLoaded();
                return this.obdFixPaymentType;
            }
            set
            {
                this.EnsureLoaded();
                if (this.obdFixPaymentType != value)
                {
                    this.IsObjectDirty = true;
                    this.obdFixPaymentType = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="decimal"/>
        /// </summary>
        [PropertyDefinition("Payment Amount", "The payment amount in USD.")]
        public decimal PaymentAmount
        {
            get
            {
                this.EnsureLoaded();
                return this.paymentAmount;
            }
            set
            {
                this.EnsureLoaded();
                if (this.paymentAmount != value)
                {
                    this.IsObjectDirty = true;
                    this.paymentAmount = value;
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
        /// Gets the payment schedule for a given user and payment type.
        /// </summary>
        /// <param name="registry">The <see cref="Registry"/> in use.</param>
        /// <param name="user">The <see cref="User"/> to which the payment schedule is assigned.</param>
        /// <param name="obdFixPaymentType">The <see cref="ObdFixPaymentType"/> payment type.</param>
        /// <returns>A <see cref="ObdFixPaymentSchedule"/> object.</returns>
        public static ObdFixPaymentSchedule GetByUserAndPaymentType(Registry registry, User user, ObdFixPaymentType obdFixPaymentType)
        {
            ObdFixPaymentSchedule ps = null;

            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(registry.ConnectionStringDefault))
            {
                dr.ProcedureName = "ObdFixPaymentSchedule_LoadByUserAndPaymentType";
                dr.AddGuid("UserId", user.Id);
                dr.AddInt32("ObdFixPaymentType", (int)obdFixPaymentType);

                dr.Execute();

                if (dr.Read())
                {
                    ps = (ObdFixPaymentSchedule)registry.CreateInstance(typeof(ObdFixPaymentSchedule), dr.GetGuid("ObdFixPaymentScheduleId"));
                    ps.LoadPropertiesFromDataReader(dr, true);
                }
            }

            return ps;
        }

        /// <summary>
        /// Gets the payment schedule for a given user and DTC prefix.
        /// </summary>
        /// <param name="registry">The <see cref="Registry"/> in use.</param>
        /// <param name="user">The <see cref="User"/> to which the payment schedule is assigned.</param>
        /// <param name="dtcPrefix">The <see cref="string"/> DTC prefix.</param>
        /// <returns>A <see cref="ObdFixPaymentSchedule"/> object.</returns>
        public static ObdFixPaymentSchedule GetByUserAndDTCPrefix(Registry registry, User user, string dtcPrefix)
        {
            ObdFixPaymentSchedule ps = null;

            switch (dtcPrefix)
            {
                case "|":
                    ps = GetByUserAndPaymentType(registry, user, ObdFixPaymentType.OBD1Code);
                    break;

                case "B":
                    ps = GetByUserAndPaymentType(registry, user, ObdFixPaymentType.OBD2BCode);
                    break;

                case "C":
                    ps = GetByUserAndPaymentType(registry, user, ObdFixPaymentType.OBD2CCode);
                    break;

                case "P":
                    ps = GetByUserAndPaymentType(registry, user, ObdFixPaymentType.OBD2PCode);
                    break;

                case "U":
                    ps = GetByUserAndPaymentType(registry, user, ObdFixPaymentType.OBD2UCode);
                    break;

                //Add on 2018-04-10 1:40 PM by Nam Lu - INNOVA Dev Team
                //Todo: Add new prefix named S for Symptom Type, this will be reviewed by supervisor...
                case "S":
                    ps = GetByUserAndPaymentType(registry, user, ObdFixPaymentType.Symptom);
                    break;
            }

            return ps;
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
                //load the base obdFixPaymentSchedule if user selected it.
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
            dr.ProcedureName = "ObdFixPaymentSchedule_Load";
            dr.AddGuid("ObdFixPaymentScheduleId", Id);
        }

        /// <summary>
        /// Sets the properties of this object from the fields in the recordset located in the supplied <see cref="SqlDataReaderWrapper"/>.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> containing the recordset and fields used to set the properties of this object (this layer only).</param>
        protected new void SetPropertiesFromDataReader(SqlDataReaderWrapper dr)
        {
            this.user = (User)Registry.CreateInstance(typeof(User), dr.GetGuid("UserId"));
            this.obdFixPaymentType = (ObdFixPaymentType)dr.GetInt32("ObdFixPaymentType");
            this.paymentAmount = dr.GetDecimal("PaymentAmount");

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
                        dr.ProcedureName = "ObdFixPaymentSchedule_Create";
                    }
                    else
                    {
                        dr.ProcedureName = "ObdFixPaymentSchedule_Save";
                    }

                    dr.AddGuid("ObdFixPaymentScheduleId", Id);
                    dr.AddGuid("UserId", User.Id);
                    dr.AddInt32("ObdFixPaymentType", (int)ObdFixPaymentType);
                    dr.AddDecimal("PaymentAmount", Math.Round(PaymentAmount, 2));

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

            // Custom delete business logic here.

            /* Example of deleting the obdFixPaymentSchedules
			 * (This could potentially cause a lock since the property calls the lookup
			 *  with a new connection, implement a load method for the property in that case,
			 *  or created a specialied delete call which won't load the collection to loop and
			 *  delete, but in that case you won't be automatically calling the delete of the
			 *  child related obdFixPaymentSchedules. See example below).
			 *
			// delete the child related objects
			foreach (ObdFixPaymentScheduleChild obdFixPaymentScheduleChild in ObdFixPaymentScheduleChilds)
			{
				transaction = obdFixPaymentScheduleChild.Delete(connection, transaction);
			}
			*/

            // delete this current object
            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                //remove related objects with a specialized delete.
                /*
				dr.ProcedureName = "ObdFixPaymentSchedulesOthers_Delete";
				dr.AddGuid("ObdFixPaymentScheduleId", Id);

				dr.ExecuteNonQuery(transaction);
				dr.ClearParameters();
				*/

                //delete the obdFixPaymentSchedule
                dr.ProcedureName = "ObdFixPaymentSchedule_Delete";
                dr.AddGuid("ObdFixPaymentScheduleId", Id);

                dr.ExecuteNonQuery(transaction);
            }

            transaction = base.Delete(connection, transaction);

            return transaction;
        }

        #endregion Required Object Methods (Load, Save, Delete, Etc)
    }
}
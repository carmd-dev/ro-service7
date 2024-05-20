using Innova.DiagnosticReports;
using Innova.Parts;
using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using Metafuse3.NullableTypes;
using System;
using System.Collections.Specialized;
using System.Data.SqlClient;

namespace Innova.Fixes
{
    /// <summary>
    /// The FixPart object handles the business logic and data access for the specialized business object, FixPart.
    /// The class inherits from <see cref="BusinessObjectBase"/> class.
    /// </summary>
    /// <example>
    /// See the examples of how to construct the FixPart object.
    ///
    /// To create a new instance of a new of FixPart.
    /// <code>FixPart o = (FixPart)Registry.CreateInstance(typeof(FixPart));</code>
    ///
    /// To create an new instance of an existing FixPart.
    /// <code>FixPart o = (FixPart)Registry.CreateInstance(typeof(FixPart), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of FixPart, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectBase.ConnectionString"/> to the <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable()]
    public class FixPart : InnovaBusinessObjectBase
    {
        // data object variables
        private Fix fix;

        private Part part;

        private int quantity;

        private string codemasterID = "";

        private NullableDecimal pricePending = NullableDecimal.Null;
        private string acesId = ""; //#ValveTask
        private PartName partName;//#ValveTask

        #region System Constructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new (one that does not exist, assigning a new <see cref="Guid"/> Id). FixPart object.
        /// In order to create a new FixPart which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <example>
        /// <code>
        /// FixPart o = (FixPart)Registry.CreateInstance(typeof(FixPart));
        /// </code>
        /// </example>
        protected internal FixPart() : base()
        {
            IsObjectCreated = true;
            IsObjectLoaded = true;
            IsObjectDirty = true;
        }

        /// <summary>
        /// Constructor creates an existing (one that exists, and already has a <see cref="Guid"/> Id).  FixPart object.
        /// In order to create an existing FixPart object, which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <param name="id">The <see cref="Guid"/> or SQL uniqueidentifier indentification "Id" of the current object.</param>
        /// <example>
        /// <code>
        /// FixPart o = (FixPart)Registry.CreateInstance(typeof(FixPart), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));
        /// </code>
        /// </example>
        protected internal FixPart(Guid id) : base(id)
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

                if (!isObjectDirty)
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
        /// Gets or sets the <see cref="Fix"/> this fix part is associated with
        /// </summary>
        public Fix Fix
        {
            get
            {
                this.EnsureLoaded();
                return this.fix;
            }
            set
            {
                EnsureLoaded();
                if (this.fix != value)
                {
                    this.IsObjectDirty = true;
                    this.fix = value;
                    this.UpdatedField("FixId");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Part"/> that is connected to this fix part
        /// </summary>
        public Part Part
        {
            get
            {
                this.EnsureLoaded();
                return this.part;
            }
            set
            {
                this.EnsureLoaded();
                if (this.part != value)
                {
                    this.IsObjectDirty = true;
                    this.part = value;
                    this.UpdatedField("PartId");
                }
            }
        }

        //#ValveTask
        /// <summary>
        /// Gets or sets the <see cref="PartName"/> that is connected to this fix part
        /// </summary>
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
        /// Gets or sets the <see cref="int"/>
        /// </summary>
        public int Quantity
        {
            get
            {
                this.EnsureLoaded();
                return this.quantity;
            }
            set
            {
                this.EnsureLoaded();
                if (this.quantity != value)
                {
                    this.IsObjectDirty = true;
                    this.quantity = value;
                    this.UpdatedField("Quantity");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> Codemaster ID
        /// </summary>
        public string CodemasterID
        {
            get
            {
                this.EnsureLoaded();
                return this.codemasterID;
            }
            set
            {
                this.EnsureLoaded();
                if (this.codemasterID != value)
                {
                    this.IsObjectDirty = true;
                    this.codemasterID = value;
                    this.UpdatedField("CodemasterID");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> Codemaster ID
        /// </summary>
        public string ACESId
        {
            get
            {
                this.EnsureLoaded();
                return this.acesId;
            }
            set
            {
                this.EnsureLoaded();
                if (this.acesId != value)
                {
                    this.IsObjectDirty = true;
                    this.acesId = value;
                    this.UpdatedField("ACESId");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableDecimal"/>
        /// </summary>
        public NullableDecimal PricePending
        {
            get
            {
                this.EnsureLoaded();
                return this.pricePending;
            }
            set
            {
                this.EnsureLoaded();
                if (this.pricePending != value)
                {
                    this.IsObjectDirty = true;
                    if (value.IsNull)
                    {
                        this.pricePending = value;
                    }
                    else
                    {
                        this.pricePending = Math.Round(value.Value, 2);
                    }
                    this.UpdatedField("PricePending");
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
        /// Returns a populated DiagnosticReportResultFixPart object.
        /// </summary>
        /// <returns>A populated <see cref="DiagnosticReportResultFixPart"/> object.</returns>
        public DiagnosticReportResultFixPart ToDiagnosticReportResultFixPart(DiagnosticReportResultFix drrFix)
        {
            DiagnosticReportResultFixPart drrFixPart = (DiagnosticReportResultFixPart)Registry.CreateInstance(typeof(DiagnosticReportResultFixPart));

            //2011-09-14 STW remove
            drrFixPart.Description = this.Part.PartName.Name;

            drrFixPart.DiagnosticReportResultFix = drrFix;
            drrFixPart.MakesList = this.Part.MakesAsCommaDelimittedString;
            drrFixPart.Name = this.PartName.Name;//#ValveTask
            drrFixPart.Name_es = this.PartName.Name_es;
            drrFixPart.Name_fr = this.PartName.Name_fr;
            drrFixPart.Name_zh = this.PartName.Name_zh;
            drrFixPart.Part = this.Part;
            drrFixPart.PartNumber = this.Part.PartNumber;
            drrFixPart.Price = this.Part.Price;
            drrFixPart.PriceInLocalCurrency = this.Part.PriceInLocalCurrency;
            drrFixPart.Quantity = this.Quantity;

            drrFixPart.CodemasterID = this.CodemasterID;
            drrFixPart.ACESId = this.ACESId;  //#ValveTask
            drrFixPart.PartName = this.PartName;    //#ValveTask

            PartOEMCollection partOEMs = new PartOEMCollection(this.Registry);
            int ACESBaseVehicleID = -1;
            int ACESEngineBaseID = -1;

            if (drrFix.DiagnosticReportResult.DiagnosticReport.Vehicle.PolkVehicleYMME.ACESBaseVehicleID.HasValue)
            {
                ACESBaseVehicleID = drrFix.DiagnosticReportResult.DiagnosticReport.Vehicle.PolkVehicleYMME.ACESBaseVehicleID.Value;
            }
            if (drrFix.DiagnosticReportResult.DiagnosticReport.Vehicle.PolkVehicleYMME.ACESEngineBaseID.HasValue)
            {
                ACESEngineBaseID = drrFix.DiagnosticReportResult.DiagnosticReport.Vehicle.PolkVehicleYMME.ACESEngineBaseID.Value;
            }

            //#ValveTask
            int.TryParse(this.Part.PartName.ACESId, out int acesIdInt);
            if (acesIdInt <= 0)
                return drrFixPart;

            if (ACESBaseVehicleID != -1 && ACESBaseVehicleID != -1)
            {
                partOEMs.LoadByAcesIds(ACESBaseVehicleID, ACESEngineBaseID, acesIdInt);
                foreach (PartOEM fixPartOEM in partOEMs)
                {
                    DiagnosticReportResultFixPartPartOem drrFixPartOEM = fixPartOEM.ToDiagnosticReportResultFixPartPartOEM();
                    drrFixPartOEM.DiagnosticReportResultFixPart = drrFixPart;

                    drrFixPart.DiagnosticReportResultFixPartPartOems.Add(drrFixPartOEM);
                }
            }

            return drrFixPart;
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
                //load the base fixPart if user selected it.
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
            dr.ProcedureName = "FixPart_Load";
            dr.AddGuid("FixPartId", Id);
        }

        /// <summary>
        /// Sets the properties of this object from the fields in the recordset located in the supplied <see cref="SqlDataReaderWrapper"/>.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> containing the recordset and fields used to set the properties of this object (this layer only).</param>
        protected new void SetPropertiesFromDataReader(SqlDataReaderWrapper dr)
        {
            this.fix = (Fix)Registry.CreateInstance(typeof(Fix), dr.GetGuid("FixId"));
            this.part = (Part)Registry.CreateInstance(typeof(Part), dr.GetGuid("PartId"));
            this.quantity = dr.GetInt32("Quantity");

            this.codemasterID = dr.GetString("CodemasterID");

            this.pricePending = dr.GetNullableDecimal("PricePending");

            this.acesId = dr.GetString("ACESId"); //#ValveTask
            this.partName = (PartName)Registry.CreateInstance(typeof(PartName), dr.GetGuid("PartNameId"));//#ValveTask

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
                        dr.ProcedureName = "FixPart_Create";
                    }
                    else
                    {
                        dr.UpdateFields("FixPart", "FixPartId", this.updatedFields);
                    }

                    dr.AddGuid("FixPartId", this.Id);
                    dr.AddGuid("FixId", this.Fix.Id);
                    dr.AddGuid("PartId", this.Part.Id);
                    dr.AddInt32("Quantity", this.Quantity);

                    dr.AddNVarChar("CodemasterID", this.CodemasterID);

                    dr.AddDecimal("PricePending", this.PricePending);

                    dr.AddNVarChar("ACESId", this.ACESId);//#ValveTask
                    dr.AddGuid("PartNameId", this.PartName.Id);//#ValveTask

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

            /* Example of deleting the fixParts
			 * (This could potentially cause a lock since the property calls the lookup
			 *  with a new connection, implement a load method for the property in that case,
			 *  or created a specialied delete call which won't load the collection to loop and
			 *  delete, but in that case you won't be automatically calling the delete of the
			 *  child related fixParts. See example below).
			 *
			// delete the child related objects
			foreach (FixPartChild fixPartChild in FixPartChilds)
			{
				transaction = fixPartChild.Delete(connection, transaction);
			}
			*/

            // delete this current object
            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                //remove related objects with a specialized delete.
                /*
				dr.ProcedureName = "FixPartsOthers_Delete";
				dr.AddGuid("FixPartId", Id);

				dr.ExecuteNonQuery(transaction);
				dr.ClearParameters();
				*/

                //delete the fixPart
                dr.ProcedureName = "FixPart_Delete";
                dr.AddGuid("FixPartId", Id);

                dr.ExecuteNonQuery(transaction);
            }

            transaction = base.Delete(connection, transaction);

            return transaction;
        }

        #endregion Required Object Methods (Load, Save, Delete, Etc)
    }
}
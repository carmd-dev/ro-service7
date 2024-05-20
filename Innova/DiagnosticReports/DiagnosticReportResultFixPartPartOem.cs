using Innova.Parts;
using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using System;
using System.Collections.Specialized;
using System.Data.SqlClient;

namespace Innova.DiagnosticReports
{
    /// <summary>
    /// The DiagnosticReportResultFixPartPartOem object handles the business logic and data access for the specialized business object, DiagnosticReportResultFixPartPartOem.
    /// The class inherits from <see cref="BusinessObjectBase"/> class.
    /// </summary>
    /// <example>
    /// See the examples of how to construct the DiagnosticReportResultFixPartPartOem object.
    ///
    /// To create a new instance of a new of DiagnosticReportResultFixPartPartOem.
    /// <code>DiagnosticReportResultFixPartPartOem o = (DiagnosticReportResultFixPartPartOem)this.Registry.CreateInstance(typeof(DiagnosticReportResultFixPartPartOem));</code>
    ///
    /// To create an new instance of an existing DiagnosticReportResultFixPartPartOem.
    /// <code>DiagnosticReportResultFixPartPartOem o = (DiagnosticReportResultFixPartPartOem)this.Registry.CreateInstance(typeof(DiagnosticReportResultFixPartPartOem), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of DiagnosticReportResultFixPartPartOem, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectBase.ConnectionString"/> to the <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable()]
    public class DiagnosticReportResultFixPartPartOem : BusinessObjectBase
    {
        private DiagnosticReportResultFixPart diagnosticReportResultFixPart;
        private PartOEM partOem;
        private string retailer = "";
        private string manufacturer = "";
        private string oemPartNumber = "";

        #region Contructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new (one that does not exist, assigning a new <see cref="Guid"/> Id). DiagnosticReportResultFixPartPartOem object.
        /// In order to create a new DiagnosticReportResultFixPartPartOem which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <example>
        /// <code>
        /// DiagnosticReportResultFixPartPartOem o = (DiagnosticReportResultFixPartPartOem)Registry.CreateInstance(typeof(DiagnosticReportResultFixPartPartOem));
        /// </code>
        /// </example>
        protected internal DiagnosticReportResultFixPartPartOem() : base()
        {
            this.IsObjectCreated = true;
            this.IsObjectLoaded = true;
            this.IsObjectDirty = true;
        }

        /// <summary>
        /// Constructor creates an existing (one that exists, and already has a <see cref="Guid"/> Id).  DiagnosticReportResultFixPartPartOem object.
        /// In order to create an existing DiagnosticReportResultFixPartPartOem object, which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <param name="id">The <see cref="Guid"/> or SQL uniqueidentifier indentification "Id" of the current object.</param>
        /// <example>
        /// <code>
        /// DiagnosticReportResultFixPartPartOem o = (DiagnosticReportResultFixPartPartOem)Registry.CreateInstance(typeof(DiagnosticReportResultFixPartPartOem), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));
        /// </code>
        /// </example>
        protected internal DiagnosticReportResultFixPartPartOem(Guid id) : base(id)
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
        /// Gets or sets the <see cref="DiagnosticReportResultFixPart"/> .
        /// </summary>
        public DiagnosticReportResultFixPart DiagnosticReportResultFixPart
        {
            get
            {
                this.EnsureLoaded();
                return this.diagnosticReportResultFixPart;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.diagnosticReportResultFixPart)
                {
                    this.IsObjectDirty = true;
                    this.diagnosticReportResultFixPart = value;
                    this.UpdatedField("DiagnosticReportResultFixPartId");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="PartOEM"/> .
        /// </summary>
        public PartOEM PartOem
        {
            get
            {
                this.EnsureLoaded();
                return this.partOem;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.partOem)
                {
                    this.IsObjectDirty = true;
                    this.partOem = value;
                    this.UpdatedField("PartOemId");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> .
        /// </summary>
        public string Retailer
        {
            get
            {
                this.EnsureLoaded();
                return this.retailer;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.retailer)
                {
                    this.IsObjectDirty = true;
                    this.retailer = value;
                    this.UpdatedField("Retailer");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> .
        /// </summary>
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
        /// Gets or sets the <see cref="string"/> .
        /// </summary>
        public string OemPartNumber
        {
            get
            {
                this.EnsureLoaded();
                return this.oemPartNumber;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.oemPartNumber)
                {
                    this.IsObjectDirty = true;
                    this.oemPartNumber = value;
                    this.UpdatedField("OemPartNumber");
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

        #endregion Object Properties (Related Objects)

        #region Business Logic Methods

        /***********************************************************************************************
	 *
	 * Custom Business Logic
	 *
	 * **********************************************************************************************/

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
            dr.ProcedureName = "DiagnosticReportResultFixPartPartOem_Load";
            dr.AddGuid("DiagnosticReportResultFixPartPartOemId", this.Id);
        }

        /// <summary>
        /// Sets the properties of this object from the fields in the recordset located in the supplied <see cref="SqlDataReaderWrapper"/>.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> containing the recordset and fields used to set the properties of this object (this layer only).</param>
        protected new void SetPropertiesFromDataReader(SqlDataReaderWrapper dr)
        {
            this.diagnosticReportResultFixPart = (DiagnosticReportResultFixPart)dr.GetBusinessObjectBase(this.Registry, typeof(DiagnosticReportResultFixPart), "DiagnosticReportResultFixPartId");
            this.partOem = (PartOEM)dr.GetBusinessObjectBase(this.Registry, typeof(PartOEM), "PartOemId");
            this.retailer = dr.GetString("Retailer");
            this.manufacturer = dr.GetString("Manufacturer");
            this.oemPartNumber = dr.GetString("OemPartNumber");

            this.IsObjectLoaded = true;
        }

        /// <summary>
        /// Sets the data in this object to the supplied <see cref="SqlDataReaderWrapper"/>, data reader wrapper.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> to set the data to</param>
        /// <param name="isCreate"><see cref="bool"/> flag indicating whether or not the command should be a create command or update</param>
        public new void SetPropertiesToDataReader(SqlDataReaderWrapper dr, bool isCreate)
        {
            if (isCreate)
            {
                dr.ProcedureName = "DiagnosticReportResultFixPartPartOem_Create";
            }
            else
            {
                dr.UpdateFields("DiagnosticReportResultFixPartPartOem", "DiagnosticReportResultFixPartPartOemId", this.updatedFields);
            }

            dr.AddGuid("DiagnosticReportResultFixPartPartOemId", this.Id);
            dr.AddBusinessObject("DiagnosticReportResultFixPartId", this.DiagnosticReportResultFixPart);
            dr.AddBusinessObject("PartOemId", this.PartOem);
            dr.AddNChar("Retailer", this.Retailer);
            dr.AddNChar("Manufacturer", this.Manufacturer);
            dr.AddNChar("OemPartNumber", this.OemPartNumber);
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
                    this.SetPropertiesToDataReader(dr, this.IsObjectCreated);

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

            // Custom delete business logic here.

            // delete this current object
            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                //delete the item
                dr.ProcedureName = "DiagnosticReportResultFixPartPartOem_Delete";
                dr.AddGuid("DiagnosticReportResultFixPartPartOemId", this.Id);

                dr.ExecuteNonQuery(transaction);
            }

            transaction = base.Delete(connection, transaction);

            return transaction;
        }

        #endregion Required Methods (Load, Save, Delete, Etc)
    }
}
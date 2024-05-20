using Innova.Parts;
using Innova.RetailerTools;
using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using System;
using System.Data.SqlClient;

namespace Innova.DiagnosticReports
{
    /// <summary>
    /// The DiagnosticReportResultFixPart object handles the business logic and data access for the specialized business object, DiagnosticReportResultFixPart.
    /// The class inherits from <see cref="BusinessObjectBase"/> class.
    /// </summary>
    /// <example>
    /// See the examples of how to construct the DiagnosticReportResultFixPart object.
    ///
    /// To create a new instance of a new of DiagnosticReportResultFixPart.
    /// <code>DiagnosticReportResultFixPart o = (DiagnosticReportResultFixPart)Registry.CreateInstance(typeof(DiagnosticReportResultFixPart));</code>
    ///
    /// To create an new instance of an existing DiagnosticReportResultFixPart.
    /// <code>DiagnosticReportResultFixPart o = (DiagnosticReportResultFixPart)Registry.CreateInstance(typeof(DiagnosticReportResultFixPart), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of DiagnosticReportResultFixPart, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectBase.ConnectionString"/> to the <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable()]
    public class DiagnosticReportResultFixTool : InnovaBusinessObjectBase
    {
        private DiagnosticReportResultFix diagnosticReportResultFix;
        private Tool tool;
        private string manufacturerName = "";
        private string makesList = "";
        private string retailersString = "";
        private string toolNumber = "";
        private string name = "";
        private string name_es = "";
        private string name_fr = "";
        private string name_zh = "";
        private string description = "";
        private string description_es = "";
        private string description_fr = "";
        private string description_zh = "";
        private decimal price;
        private int quantity;

        #region System Constructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new (one that does not exist, assigning a new <see cref="Guid"/> Id). DiagnosticReportResultFixPart object.
        /// In order to create a new DiagnosticReportResultFixPart which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <example>
        /// <code>
        /// DiagnosticReportResultFixPart o = (DiagnosticReportResultFixPart)Registry.CreateInstance(typeof(DiagnosticReportResultFixPart));
        /// </code>
        /// </example>
        protected internal DiagnosticReportResultFixTool() : base()
        {
            IsObjectCreated = true;
            IsObjectLoaded = true;
            IsObjectDirty = true;
        }

        /// <summary>
        /// Constructor creates an existing (one that exists, and already has a <see cref="Guid"/> Id).  DiagnosticReportResultFixPart object.
        /// In order to create an existing DiagnosticReportResultFixPart object, which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <param name="id">The <see cref="Guid"/> or SQL uniqueidentifier indentification "Id" of the current object.</param>
        /// <example>
        /// <code>
        /// DiagnosticReportResultFixPart o = (DiagnosticReportResultFixPart)Registry.CreateInstance(typeof(DiagnosticReportResultFixPart), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));
        /// </code>
        /// </example>
        protected internal DiagnosticReportResultFixTool(Guid id) : base(id)
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
        /// Gets or sets the <see cref="DiagnosticReportResultFix"/>
        /// </summary>
        public DiagnosticReportResultFix DiagnosticReportResultFix
        {
            get
            {
                this.EnsureLoaded();
                return this.diagnosticReportResultFix;
            }
            set
            {
                this.EnsureLoaded();
                if (this.diagnosticReportResultFix != value)
                {
                    this.IsObjectDirty = true;
                    this.diagnosticReportResultFix = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Part"/>
        /// </summary>
        public Tool Tool
        {
            get
            {
                this.EnsureLoaded();
                return this.tool;
            }
            set
            {
                this.EnsureLoaded();
                if (this.tool != value)
                {
                    this.IsObjectDirty = true;
                    this.tool = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> list of makes.
        /// </summary>
        public string MakesList
        {
            get
            {
                this.EnsureLoaded();
                return this.makesList;
            }
            set
            {
                this.EnsureLoaded();
                if (this.makesList != value)
                {
                    this.IsObjectDirty = true;
                    this.makesList = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        public string ToolNumber
        {
            get
            {
                this.EnsureLoaded();
                return this.toolNumber;
            }
            set
            {
                this.EnsureLoaded();
                if (this.toolNumber != value)
                {
                    this.IsObjectDirty = true;
                    this.toolNumber = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
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
                if (this.name != value)
                {
                    this.IsObjectDirty = true;
                    this.name = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
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
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
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
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
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
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> name in the language specified in the Registry.
        /// </summary>
        public string Name_Translated => this.RuntimeInfo.GetTranslatedValue(this.Name, this.Name_es, this.Name_fr, this.Name_zh);

        /// <summary>
		/// Gets or sets the <see cref="string"/>
		/// </summary>
		public string Description
        {
            get
            {
                this.EnsureLoaded();
                return this.description;
            }
            set
            {
                this.EnsureLoaded();
                if (this.description != value)
                {
                    this.IsObjectDirty = true;
                    this.description = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        public string Description_es
        {
            get
            {
                this.EnsureLoaded();
                return this.description_es;
            }
            set
            {
                this.EnsureLoaded();
                if (this.description_es != value)
                {
                    this.IsObjectDirty = true;
                    this.description_es = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        public string Description_fr
        {
            get
            {
                this.EnsureLoaded();
                return this.description_fr;
            }
            set
            {
                this.EnsureLoaded();
                if (this.description_fr != value)
                {
                    this.IsObjectDirty = true;
                    this.description_fr = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        public string Description_zh
        {
            get
            {
                this.EnsureLoaded();
                return this.description_zh;
            }
            set
            {
                this.EnsureLoaded();
                if (this.description_zh != value)
                {
                    this.IsObjectDirty = true;
                    this.description_zh = value;
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> description in the language specified in the Registry.
        /// </summary>
        public string Description_Translated => this.RuntimeInfo.GetTranslatedValue(this.Description, this.Description_es, this.Description_fr, this.Description_zh);

        /// <summary>
		/// Gets or sets the <see cref="decimal"/>
		/// </summary>
		public decimal Price
        {
            get
            {
                this.EnsureLoaded();
                return this.price;
            }
            set
            {
                this.EnsureLoaded();
                if (this.price != value)
                {
                    this.IsObjectDirty = true;
                    this.price = value;
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
                }
            }
        }

        //ToolDB_
        /// <summary>
        ///
        /// </summary>
        public string RetailersString
        {
            get
            {
                this.EnsureLoaded();
                return this.retailersString;
            }
            set
            {
                this.EnsureLoaded();
                if (this.retailersString != value)
                {
                    this.IsObjectDirty = true;
                    this.retailersString = value;
                }
            }
        }

        //ToolDB_

        /// <summary>
        ///
        /// </summary>
        public string ManufacturerName
        {
            get
            {
                this.EnsureLoaded();
                return manufacturerName;
            }
            set
            {
                this.EnsureLoaded();
                if (this.manufacturerName != value)
                {
                    this.IsObjectDirty = true;
                    this.manufacturerName = value;
                    this.UpdatedField("ManufacturerName");
                }
            }
        }

        private DiagnosticReport DiagnosticReport => this.DiagnosticReportResultFix.DiagnosticReportResult.DiagnosticReport;

        //ToolDB_
        /// <summary>
        /// Gets the <see cref="decimal"/> price to be presented based on the current currency in the <see cref="RuntimeInfo"/>.
        /// </summary>
        public decimal Price_Presented
        {
            get
            {
                if (this.RuntimeInfo.CurrentCurrency != Currency.USD)
                {
                    return this.RuntimeInfo.GetLocalCurrencyValueFromUSDollars(this.Price);
                }

                return this.price;
            }
        }

        //ToolDB_

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
                //load the base diagnosticReportResultFixPart if user selected it.
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
            dr.ProcedureName = "DiagnosticReportResultFixTool_Load";
            dr.AddGuid("DiagnosticReportResultFixToolId", Id);
        }

        /// <summary>
        /// Sets the properties of this object from the fields in the recordset located in the supplied <see cref="SqlDataReaderWrapper"/>.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> containing the recordset and fields used to set the properties of this object (this layer only).</param>
        protected new void SetPropertiesFromDataReader(SqlDataReaderWrapper dr)
        {
            this.diagnosticReportResultFix = (DiagnosticReportResultFix)this.Registry.CreateInstance(typeof(DiagnosticReportResultFix), dr.GetGuid("DiagnosticReportResultFixId"));
            if (!dr.IsDBNull("ToolId"))
            {
                this.tool = (Tool)this.Registry.CreateInstance(typeof(Tool), dr.GetGuid("ToolId"));
            }
            this.makesList = dr.GetString("MakesList");
            this.toolNumber = dr.GetSysNullableString("ToolNumber"); //#Null Issue Fix
            this.manufacturerName = dr.GetString("ManufacturerName");
            this.retailersString = dr.GetSysNullableString("RetailersString"); //#Null Issue Fix
            this.name = dr.GetString("Name");
            this.name_es = dr.GetString("Name_es");
            this.name_fr = dr.GetString("Name_fr");
            this.name_zh = dr.GetString("Name_zh");
            this.description = dr.GetString("Description");
            this.description_es = dr.GetString("Description_es");
            this.description_fr = dr.GetString("Description_fr");
            this.description_zh = dr.GetString("Description_zh");
            this.price = dr.GetDecimal("Price");
            this.quantity = dr.GetInt32("Quantity");

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
                    if (this.IsObjectCreated)
                    {
                        dr.ProcedureName = "DiagnosticReportResultFixTool_Create";
                    }
                    else
                    {
                        dr.ProcedureName = "DiagnosticReportResultFixTool_Save";
                    }

                    dr.AddGuid("DiagnosticReportResultFixToolId", this.Id);
                    dr.AddGuid("DiagnosticReportResultFixId", this.DiagnosticReportResultFix.Id);
                    if (this.Tool != null)
                    {
                        dr.AddGuid("ToolId", this.Tool.Id);
                    }
                    dr.AddNVarChar("MakesList", this.MakesList);
                    dr.AddNVarChar("RetailersString", this.RetailersString); //ToolDB_
                    dr.AddNVarChar("ToolNumber", this.ToolNumber);
                    dr.AddNVarChar("ManufacturerName", this.ManufacturerName);
                    dr.AddNVarChar("Name", this.Name);
                    dr.AddNVarChar("Name_es", this.Name_es);
                    dr.AddNVarChar("Name_fr", this.Name_fr);
                    dr.AddNVarChar("Name_zh", this.Name_zh);
                    dr.AddNVarChar("Description", this.Description);
                    dr.AddNVarChar("Description_es", this.Description_es);
                    dr.AddNVarChar("Description_fr", this.Description_fr);
                    dr.AddNVarChar("Description_zh", this.Description_zh);
                    dr.AddDecimal("Price", Math.Round(this.Price, 2));
                    dr.AddInt32("Quantity", this.Quantity);

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

            transaction = this.EnsureDatabasePrepared(connection, transaction);

            // Custom delete business logic here.

            /* Example of deleting the diagnosticReportResultFixParts
			 * (This could potentially cause a lock since the property calls the lookup
			 *  with a new connection, implement a load method for the property in that case,
			 *  or created a specialied delete call which won't load the collection to loop and
			 *  delete, but in that case you won't be automatically calling the delete of the
			 *  child related diagnosticReportResultFixParts. See example below).
			 *
			// delete the child related objects
			foreach (DiagnosticReportResultFixPartChild diagnosticReportResultFixPartChild in DiagnosticReportResultFixPartChilds)
			{
				transaction = diagnosticReportResultFixPartChild.Delete(connection, transaction);
			}
			*/

            // delete this current object
            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                //remove related objects with a specialized delete.
                /*
				dr.ProcedureName = "DiagnosticReportResultFixPartsOthers_Delete";
				dr.AddGuid("DiagnosticReportResultFixPartId", Id);

				dr.ExecuteNonQuery(transaction);
				dr.ClearParameters();
				*/

                //delete the diagnosticReportResultFixPart
                dr.ProcedureName = "DiagnosticReportResultFixTool_Delete";
                dr.AddGuid("DiagnosticReportResultFixToolId", this.Id);

                dr.ExecuteNonQuery(transaction);
            }

            transaction = base.Delete(connection, transaction);

            return transaction;
        }

        #endregion Required Object Methods (Load, Save, Delete, Etc)
    }
}
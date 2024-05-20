using Innova.DiagnosticReports;
using Innova.ObdFixes;
using Innova.Parts;
using Innova.RetailerTools;
using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using System;
using System.Collections.Specialized;
using System.Data.SqlClient;

namespace Innova.Fixes
{
    /// <summary>
    /// The FixTool object handles the business logic and data access for the specialized business object, FixTool.
    /// The class inherits from <see cref="BusinessObjectBase"/> class.
    /// </summary>
    /// <example>
    /// See the examples of how to construct the FixTool object.
    ///
    /// To create a new instance of a new of FixTool.
    /// <code>FixTool o = (FixTool)Registry.CreateInstance(typeof(FixTool));</code>
    ///
    /// To create an new instance of an existing FixTool.
    /// <code>FixTool o = (FixTool)Registry.CreateInstance(typeof(FixTool), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of FixTool, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectBase.ConnectionString"/> to the <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable()]
    public class FixTool : InnovaBusinessObjectBase
    {
        // data object variables
        private Fix fix;

        private Tool tool;
        private ObdFix obdfix;
        private ProRSFix prorsfix;
        private int quantity;

        #region System Constructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new (one that does not exist, assigning a new <see cref="Guid"/> Id). FixTool object.
        /// In order to create a new FixTool which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <example>
        /// <code>
        /// FixTool o = (FixTool)Registry.CreateInstance(typeof(FixTool));
        /// </code>
        /// </example>
        protected internal FixTool() : base()
        {
            IsObjectCreated = true;
            IsObjectLoaded = true;
            IsObjectDirty = true;
        }

        /// <summary>
        /// Constructor creates an existing (one that exists, and already has a <see cref="Guid"/> Id).  FixTool object.
        /// In order to create an existing FixTool object, which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <param name="id">The <see cref="Guid"/> or SQL uniqueidentifier indentification "Id" of the current object.</param>
        /// <example>
        /// <code>
        /// FixTool o = (FixTool)Registry.CreateInstance(typeof(FixTool), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));
        /// </code>
        /// </example>
        protected internal FixTool(Guid id) : base(id)
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
        ///
        /// </summary>
        public ObdFix ObdFix
        {
            get
            {
                this.EnsureLoaded();
                return this.obdfix;
            }
            set
            {
                EnsureLoaded();
                if (this.obdfix != value)
                {
                    this.IsObjectDirty = true;
                    this.obdfix = value;
                    this.UpdatedField("ObdFixId");
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public ProRSFix ProRSFix
        {
            get
            {
                this.EnsureLoaded();
                return this.prorsfix;
            }
            set
            {
                EnsureLoaded();
                if (this.prorsfix != value)
                {
                    this.IsObjectDirty = true;
                    this.prorsfix = value;
                    this.UpdatedField("ProRSFixId");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Part"/> that is connected to this fix part
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
                    this.UpdatedField("ToolId");
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

        //ToolDB_
        /// <summary>
        /// Returns a populated DiagnosticReportResultFixPart object.
        /// </summary>
        /// <returns>A populated <see cref="DiagnosticReportResultFixTool"/> object.</returns>
        public DiagnosticReportResultFixTool ToDiagnosticReportResultFixTool(DiagnosticReportResultFix drrFix)
        {
            DiagnosticReportResultFixTool drrFixTool = (DiagnosticReportResultFixTool)Registry.CreateInstance(typeof(DiagnosticReportResultFixTool));
            drrFixTool.DiagnosticReportResultFix = drrFix;
            drrFixTool.Tool = this.Tool;
            drrFixTool.MakesList = this.Tool.MakesAsCommaDelimittedString;
            drrFixTool.RetailersString = string.Join(", ", this.Tool.Retailers);
            drrFixTool.Name = this.Tool.ToolName.Description;
            drrFixTool.Name_es = this.Tool.ToolName.Description_es;
            drrFixTool.Name_fr = this.Tool.ToolName.Description_fr;
            drrFixTool.Name_zh = this.Tool.ToolName.Description_zh;
            drrFixTool.Description = this.Tool.ToolName.Description;
            drrFixTool.Description_es = this.Tool.ToolName.Description_es;
            drrFixTool.Description_fr = this.Tool.ToolName.Description_fr;
            drrFixTool.Description_zh = this.Tool.ToolName.Description_zh;
            drrFixTool.ToolNumber = this.Tool.ToolNumber;
            drrFixTool.Price = this.Tool.Price;
            drrFixTool.Quantity = this.Quantity;

            return drrFixTool;
        }

        //ToolDB_

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
                //load the base FixTool if user selected it.
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
            dr.ProcedureName = "FixTool_Load";
            dr.AddGuid("FixToolId", Id);
        }

        /// <summary>
        /// Sets the properties of this object from the fields in the recordset located in the supplied <see cref="SqlDataReaderWrapper"/>.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> containing the recordset and fields used to set the properties of this object (this layer only).</param>
        protected new void SetPropertiesFromDataReader(SqlDataReaderWrapper dr)
        {
            this.fix = (Fix)Registry.CreateInstance(typeof(Fix), dr.GetGuid("FixId"));
            this.tool = (Tool)Registry.CreateInstance(typeof(Tool), dr.GetGuid("ToolId"));
            if (!dr.IsDBNull("ProRSFixId"))
                this.prorsfix = (ProRSFix)Registry.CreateInstance(typeof(ProRSFix), dr.GetGuid("ProRSFixId"));

            if (!dr.IsDBNull("ObdFixId"))
                this.obdfix = (ObdFix)Registry.CreateInstance(typeof(ObdFix), dr.GetGuid("ObdFixId"));

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
                    if (IsObjectCreated)
                    {
                        dr.ProcedureName = "FixTool_Create";
                    }
                    else
                    {
                        dr.UpdateFields("FixTool", "FixToolId", this.updatedFields);
                    }

                    dr.AddGuid("FixToolId", this.Id);
                    dr.AddGuid("FixId", this.Fix.Id);
                    dr.AddGuid("ToolId", this.Tool.Id);
                    if (this.ObdFix != null)
                        dr.AddGuid("ObdFixId", this.ObdFix.Id);
                    if (this.ProRSFix != null)
                        dr.AddGuid("ProRSFixId", this.ProRSFix.Id);

                    dr.AddInt32("Quantity", this.Quantity);

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

            /*
			 * Example of related business objects
			 *
			// remove the removed objects
			if(FixToolChilds != null)
			{
				foreach(FixToolChild FixToolChild in FixToolChilds.Removed)
				{
					transaction = FixToolChild.Delete(connection, transaction);
				}
				// save each object in the related object
				foreach(FixToolChild FixToolChild in FixToolChilds)
				{
					transaction = FixToolChild.Save(connection, transaction);
				}
			}

			Example of related by a join of two tables

			// save the others object join relationship
			if(others != null && Others.IsObjectDirty)
			{
				transaction = EnsureDatabasePrepared(connection, transaction);

				//for id/joinId (2 column table) relationships
				SqlDataReaderWrapperdeleteFixToolsOthers = new SqlProcedureCommand();
				deleteFixToolsOthers.ProcedureName = "FixToolsOthers_Delete";
				deleteFixToolsOthers.AddGuid("FixToolId", Id);

				// create procedure call to add this Id then call the collection save
				// method, which adds the Id for itself.
				SqlDataReaderWrappersaveFixToolsOthers = new SqlProcedureCommand();

				saveFixToolsOthers.ProcedureName = "FixToolsOthers_Create";
				saveFixToolsOthers.AddGuid("FixToolId", Id);

				// perform the save on the related object
				transaction = Others.Save(connection, transaction, deleteFixToolsOthers, saveFixToolsOthers , "OtherId");
			}
			*/

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

            /* Example of deleting the FixTools
			 * (This could potentially cause a lock since the property calls the lookup
			 *  with a new connection, implement a load method for the property in that case,
			 *  or created a specialied delete call which won't load the collection to loop and
			 *  delete, but in that case you won't be automatically calling the delete of the
			 *  child related FixTools. See example below).
			 *
			// delete the child related objects
			foreach (FixToolChild FixToolChild in FixToolChilds)
			{
				transaction = FixToolChild.Delete(connection, transaction);
			}
			*/

            // delete this current object
            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                //remove related objects with a specialized delete.
                /*
				dr.ProcedureName = "FixToolsOthers_Delete";
				dr.AddGuid("FixToolId", Id);

				dr.ExecuteNonQuery(transaction);
				dr.ClearParameters();
				*/

                //delete the FixTool
                dr.ProcedureName = "FixTool_Delete";
                dr.AddGuid("FixToolId", Id);

                dr.ExecuteNonQuery(transaction);
            }

            transaction = base.Delete(connection, transaction);

            return transaction;
        }

        #endregion Required Object Methods (Load, Save, Delete, Etc)
    }
}
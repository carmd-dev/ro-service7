using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using System;
using System.Collections;
using System.Data.SqlClient;

namespace Innova.DiagnosticReports
{
    /// <summary>
    /// The DiagnosticReportResult object handles the business logic and data access for the specialized business object, DiagnosticReportResult.
    /// The class inherits from <see cref="BusinessObjectBase"/> class.
    /// </summary>
    /// <example>
    /// See the examples of how to construct the DiagnosticReportResult object.
    ///
    /// To create a new instance of a new of DiagnosticReportResult.
    /// <code>DiagnosticReportResult o = (DiagnosticReportResult)Registry.CreateInstance(typeof(DiagnosticReportResult));</code>
    ///
    /// To create an new instance of an existing DiagnosticReportResult.
    /// <code>DiagnosticReportResult o = (DiagnosticReportResult)Registry.CreateInstance(typeof(DiagnosticReportResult), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of DiagnosticReportResult, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectBase.ConnectionString"/> to the <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable()]
    public class DiagnosticReportResult : BusinessObjectBase
    {
        private DiagnosticReport diagnosticReport;
        private DateTime createdDateTimeUTC;

        private DiagnosticReportResultErrorCodeCollection diagnosticReportResultErrorCodes;
        private DiagnosticReportResultFixCollection diagnosticReportResultFixes;
        private DiagnosticReportResultFixCollection absFixes;

        #region System Constructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new (one that does not exist, assigning a new <see cref="Guid"/> Id). DiagnosticReportResult object.
        /// In order to create a new DiagnosticReportResult which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <example>
        /// <code>
        /// DiagnosticReportResult o = (DiagnosticReportResult)Registry.CreateInstance(typeof(DiagnosticReportResult));
        /// </code>
        /// </example>
        protected internal DiagnosticReportResult() : base()
        {
            IsObjectCreated = true;
            IsObjectLoaded = true;
            IsObjectDirty = true;
        }

        /// <summary>
        /// Constructor creates an existing (one that exists, and already has a <see cref="Guid"/> Id).  DiagnosticReportResult object.
        /// In order to create an existing DiagnosticReportResult object, which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <param name="id">The <see cref="Guid"/> or SQL uniqueidentifier indentification "Id" of the current object.</param>
        /// <example>
        /// <code>
        /// DiagnosticReportResult o = (DiagnosticReportResult)Registry.CreateInstance(typeof(DiagnosticReportResult), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));
        /// </code>
        /// </example>
        protected internal DiagnosticReportResult(Guid id) : base(id)
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
        /// Gets or sets the <see cref="DiagnosticReport"/>
        /// </summary>
        public DiagnosticReport DiagnosticReport
        {
            get
            {
                this.EnsureLoaded();
                return this.diagnosticReport;
            }
            set
            {
                this.EnsureLoaded();
                if (this.diagnosticReport != value)
                {
                    this.IsObjectDirty = true;
                    this.diagnosticReport = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/>
        /// </summary>
        [PropertyDefinition("Created Date Time", "The date the diagnostic report was created.")]
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

        #endregion Object Properties

        #region Object Properties (Related Objects)

        /*****************************************************************************************
		 *
		 * Object Relationships: Add correlated Object Collections
		 *
		*******************************************************************************************/
        /// <summary>
        /// Get the <see cref="Symptoms.Symptom"/> associated with the diagnostic report result.
        /// </summary>
        //public Symptoms.Symptom SymptomResult
        //{
        //	get
        //	{
        //		if (!IsObjectCreated)
        //		{
        //			SqlProcedureCommand call = new SqlProcedureCommand();
        //			EnsureValidId();

        //			call.ProcedureName = "DiagnosticReportResultErrorCode_LoadByDiagnosticReportResult";
        //			call.AddGuid("DiagnosticReportResultId", Id);
        //		}
        //	}
        //}

        /// <summary>
        /// Get the <see cref="DiagnosticReportResultErrorCodeCollection"/> of error codes that apply to this diagnostic report result.
        /// </summary>
        public DiagnosticReportResultErrorCodeCollection DiagnosticReportResultErrorCodes
        {
            get
            {
                if (diagnosticReportResultErrorCodes == null)
                {
                    diagnosticReportResultErrorCodes = new DiagnosticReportResultErrorCodeCollection(Registry);

                    //load if not a user created element
                    if (!isObjectCreated)
                    {
                        SqlProcedureCommand call = new SqlProcedureCommand();
                        EnsureValidId();

                        call.ProcedureName = "DiagnosticReportResultErrorCode_LoadByDiagnosticReportResult";
                        call.AddGuid("DiagnosticReportResultId", Id);

                        diagnosticReportResultErrorCodes.Load(call, "DiagnosticReportResultErrorCodeId", true, true);
                    }
                }

                return diagnosticReportResultErrorCodes;
            }
        }

        /// <summary>
        /// Gets a <see cref="DiagnosticReportResultErrorCodeCollection"/> of <see cref="DiagnosticReportResultErrorCode"/> powertrain OBD2 code objctes.
        /// </summary>
        public DiagnosticReportResultErrorCodeCollection PwrObd2DiagnosticReportResultErrorCodes
        {
            get
            {
                return this.GetErrorCodesBySystemType(DiagnosticReportErrorCodeSystemType.PowertrainObd2);
            }
        }

        /// <summary>
        /// Gets a <see cref="DiagnosticReportResultErrorCodeCollection"/> of <see cref="DiagnosticReportResultErrorCode"/> powertrain OBD1 code objctes.
        /// </summary>
        public DiagnosticReportResultErrorCodeCollection PwrObd1DiagnosticReportResultErrorCodes
        {
            get
            {
                return this.GetErrorCodesBySystemType(DiagnosticReportErrorCodeSystemType.PowertrainOBD1);
            }
        }

        /// <summary>
        /// Gets a <see cref="DiagnosticReportResultErrorCodeCollection"/> of <see cref="DiagnosticReportResultErrorCode"/> ABS code objctes.
        /// </summary>
        public DiagnosticReportResultErrorCodeCollection AbsDiagnosticReportResultErrorCodes
        {
            get
            {
                return this.GetErrorCodesBySystemType(DiagnosticReportErrorCodeSystemType.ABS);
            }
        }

        /// <summary>
        /// Gets a <see cref="DiagnosticReportResultErrorCodeCollection"/> of <see cref="DiagnosticReportResultErrorCode"/> SRS code objctes.
        /// </summary>
        public DiagnosticReportResultErrorCodeCollection SrsDiagnosticReportResultErrorCodes
        {
            get
            {
                return this.GetErrorCodesBySystemType(DiagnosticReportErrorCodeSystemType.SRS);
            }
        }

        /// <summary>
        /// Get the <see cref="DiagnosticReportResultFixCollection"/> of fixes that apply to this diagnostic report result.
        /// </summary>
        public DiagnosticReportResultFixCollection DiagnosticReportResultFixes
        {
            get
            {
                if (diagnosticReportResultFixes == null)
                {
                    diagnosticReportResultFixes = new DiagnosticReportResultFixCollection(Registry);

                    //load if not a user created element
                    if (!isObjectCreated)
                    {
                        SqlProcedureCommand call = new SqlProcedureCommand();
                        EnsureValidId();

                        call.ProcedureName = "DiagnosticReportResultFix_LoadByDiagnosticReportResult";
                        call.SqlCommand.CommandTimeout = 120; //Added 2017-10-04 by INNOVA Dev Team to increase the Command Timeout

                        call.AddGuid("DiagnosticReportResultId", Id);

                        diagnosticReportResultFixes.Load(call, "DiagnosticReportResultFixId", true, true);
                    }
                }

                return diagnosticReportResultFixes;
            }
        }

        /// <summary>
        /// Gets a <see cref="DiagnosticReportResultFixCollection"/> of <see cref="DiagnosticReportResultFix"/> powertrain OBD2 fix objctes.
        /// </summary>
        public DiagnosticReportResultFixCollection PwrObd2DiagnosticReportResultFixes
        {
            get
            {
                DiagnosticReportResultFixCollection fixes = new DiagnosticReportResultFixCollection(this.Registry);
                ArrayList tempFixes = this.DiagnosticReportResultFixes.GetAllByProperty("DiagnosticReportErrorCodeSystemType", DiagnosticReportErrorCodeSystemType.PowertrainObd2);
                if (tempFixes != null && tempFixes.Count > 0)
                {
                    foreach (DiagnosticReportResultFix drrFix in tempFixes)
                    {
                        fixes.Add(drrFix);
                    }
                }

                return fixes;
            }
        }

        /// <summary>
        /// Gets a <see cref="DiagnosticReportResultFixCollection"/> of <see cref="DiagnosticReportResultFix"/> powertrain OBD1 fix objctes.
        /// </summary>
        public DiagnosticReportResultFixCollection PwrObd1DiagnosticReportResultFixes
        {
            get
            {
                DiagnosticReportResultFixCollection fixes = new DiagnosticReportResultFixCollection(this.Registry);
                ArrayList tempFixes = this.DiagnosticReportResultFixes.GetAllByProperty("DiagnosticReportErrorCodeSystemType", DiagnosticReportErrorCodeSystemType.PowertrainOBD1);
                if (tempFixes != null && tempFixes.Count > 0)
                {
                    foreach (DiagnosticReportResultFix drrFix in tempFixes)
                    {
                        fixes.Add(drrFix);
                    }
                }

                return fixes;
            }
        }

        /// <summary>
        /// Gets a <see cref="DiagnosticReportResultFixCollection"/> of <see cref="DiagnosticReportResultFix"/> ABS fix objctes.
        /// </summary>
        public DiagnosticReportResultFixCollection AbsDiagnosticReportResultFixes
        {
            get
            {
                DiagnosticReportResultFixCollection fixes = new DiagnosticReportResultFixCollection(this.Registry);
                ArrayList tempFixes = this.DiagnosticReportResultFixes.GetAllByProperty("DiagnosticReportErrorCodeSystemType", DiagnosticReportErrorCodeSystemType.ABS);
                if (tempFixes != null && tempFixes.Count > 0)
                {
                    foreach (DiagnosticReportResultFix drrFix in tempFixes)
                    {
                        fixes.Add(drrFix);
                    }
                }

                return fixes;
            }
        }

        /// <summary>
        /// Gets a <see cref="DiagnosticReportResultFixCollection"/> of <see cref="DiagnosticReportResultFix"/> SRS fix objctes.
        /// </summary>
        public DiagnosticReportResultFixCollection SrsDiagnosticReportResultFixes
        {
            get
            {
                DiagnosticReportResultFixCollection fixes = new DiagnosticReportResultFixCollection(this.Registry);
                ArrayList tempFixes = this.DiagnosticReportResultFixes.GetAllByProperty("DiagnosticReportErrorCodeSystemType", DiagnosticReportErrorCodeSystemType.SRS);
                if (tempFixes != null && tempFixes.Count > 0)
                {
                    foreach (DiagnosticReportResultFix drrFix in tempFixes)
                    {
                        fixes.Add(drrFix);
                    }
                }

                return fixes;
            }
        }

        /// <summary>
        /// Get the <see cref="DiagnosticReportResultFixCollection"/> of ABS fixes that apply to this diagnostic report result.
        /// </summary>
        public DiagnosticReportResultFixCollection AbsFixes
        {
            get
            {
                if (this.absFixes == null)
                {
                    this.absFixes = new DiagnosticReportResultFixCollection(Registry);
                }

                return diagnosticReportResultFixes;
            }
        }

        #endregion Object Properties (Related Objects)

        #region Business Logic Methods

        /// <summary>
        /// Gets a <see cref="DiagnosticReportResultErrorCodeCollection"/> of <see cref="DiagnosticReportResultErrorCode"/> error code objects.
        /// </summary>
        /// <param name="diagnosticReportErrorCodeSystemType">The <see cref="DiagnosticReportErrorCodeSystemType"/> to look for.</param>
        /// <returns>A <see cref="DiagnosticReportResultErrorCodeCollection"/> of <see cref="DiagnosticReportResultErrorCode"/> error code objects.</returns>
        public DiagnosticReportResultErrorCodeCollection GetErrorCodesBySystemType(DiagnosticReportErrorCodeSystemType diagnosticReportErrorCodeSystemType)
        {
            DiagnosticReportResultErrorCodeCollection codes = new DiagnosticReportResultErrorCodeCollection(this.Registry);
            ArrayList codesByType = this.DiagnosticReportResultErrorCodes.GetAllByProperty("DiagnosticReportErrorCodeSystemType", diagnosticReportErrorCodeSystemType);
            if (codesByType != null && codesByType.Count > 0)
            {
                foreach (DiagnosticReportResultErrorCode drrErrorCode in codesByType)
                {
                    codes.Add(drrErrorCode);
                }
            }

            return codes;
        }

        /// <summary>
        /// Gets a <see cref="DiagnosticReportResultFixCollection"/> of <see cref="DiagnosticReportResultFix"/> fix objects for the specified system type.
        /// </summary>
        /// <param name="diagnosticReportErrorCodeSystemType">The <see cref="DiagnosticReportErrorCodeSystemType"/> to look for.</param>
        /// <returns>A <see cref="DiagnosticReportResultFixCollection"/> of <see cref="DiagnosticReportResultFix"/> fix objects.</returns>
        public DiagnosticReportResultFixCollection GetFixesBySystemType(DiagnosticReportErrorCodeSystemType diagnosticReportErrorCodeSystemType)
        {
            DiagnosticReportResultFixCollection drFixes = new DiagnosticReportResultFixCollection(this.Registry);

            switch (diagnosticReportErrorCodeSystemType)
            {
                case DiagnosticReportErrorCodeSystemType.ABS:
                    drFixes = this.AbsDiagnosticReportResultFixes;
                    break;

                case DiagnosticReportErrorCodeSystemType.PowertrainOBD1:
                    drFixes = this.PwrObd1DiagnosticReportResultFixes;
                    break;

                case DiagnosticReportErrorCodeSystemType.PowertrainObd2:
                    drFixes = this.PwrObd2DiagnosticReportResultFixes;
                    break;

                case DiagnosticReportErrorCodeSystemType.SRS:
                    drFixes = this.SrsDiagnosticReportResultFixes;
                    break;
            }

            return drFixes;
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
                //load the base diagnosticReportResult if user selected it.
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
            dr.ProcedureName = "DiagnosticReportResult_Load";
            dr.AddGuid("DiagnosticReportResultId", Id);
        }

        /// <summary>
        /// Sets the properties of this object from the fields in the recordset located in the supplied <see cref="SqlDataReaderWrapper"/>.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> containing the recordset and fields used to set the properties of this object (this layer only).</param>
        protected new void SetPropertiesFromDataReader(SqlDataReaderWrapper dr)
        {
            this.diagnosticReport = (DiagnosticReport)this.Registry.CreateInstance(typeof(DiagnosticReport), dr.GetGuid("DiagnosticReportId"));
            this.createdDateTimeUTC = dr.GetDateTime("CreatedDateTimeUTC");

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
                        dr.ProcedureName = "DiagnosticReportResult_Create";
                        CreatedDateTimeUTC = DateTime.UtcNow;
                    }
                    else
                    {
                        dr.ProcedureName = "DiagnosticReportResult_Save";
                    }

                    dr.AddGuid("DiagnosticReportResultId", this.Id);
                    dr.AddBusinessObject("DiagnosticReportId", this.DiagnosticReport);
                    dr.AddDateTime("CreatedDateTimeUTC", this.CreatedDateTimeUTC);

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
            if (diagnosticReportResultErrorCodes != null)
            {
                for (int i = 0; i < DiagnosticReportResultErrorCodes.Removed.Count; i++)
                {
                    transaction = ((DiagnosticReportResultErrorCode)DiagnosticReportResultErrorCodes.Removed[i]).Delete(connection, transaction);
                }
                for (int i = 0; i < DiagnosticReportResultErrorCodes.Count; i++)
                {
                    transaction = DiagnosticReportResultErrorCodes[i].Save(connection, transaction);
                }
            }

            if (diagnosticReportResultFixes != null)
            {
                for (int i = 0; i < DiagnosticReportResultFixes.Removed.Count; i++)
                {
                    transaction = ((DiagnosticReportResultFix)DiagnosticReportResultFixes.Removed[i]).Delete(connection, transaction);
                }
                for (int i = 0; i < DiagnosticReportResultFixes.Count; i++)
                {
                    transaction = DiagnosticReportResultFixes[i].Save(connection, transaction);
                }
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

            foreach (DiagnosticReportResultErrorCode code in DiagnosticReportResultErrorCodes)
            {
                transaction = code.Delete(connection, transaction);
            }

            foreach (DiagnosticReportResultFix fix in DiagnosticReportResultFixes)
            {
                transaction = fix.Delete(connection, transaction);
            }

            // delete this current object
            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                //delete the diagnosticReportResult
                dr.ProcedureName = "DiagnosticReportResult_Delete";
                dr.AddGuid("DiagnosticReportResultId", Id);

                dr.ExecuteNonQuery(transaction);
            }

            transaction = base.Delete(connection, transaction);

            return transaction;
        }

        #endregion Required Object Methods (Load, Save, Delete, Etc)
    }
}
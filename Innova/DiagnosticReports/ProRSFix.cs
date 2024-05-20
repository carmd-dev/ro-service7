using Innova.Fixes;
using Innova.Users;
using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using Metafuse3.NullableTypes;
using System;
using System.Collections.Specialized;
using System.Data.SqlClient;

namespace Innova.DiagnosticReports
{
    /// <summary>
    /// </summary>
    public class ProRSFix : BusinessObjectBase
    {
        private string milDtc;
        private ProRSFixStatus proRSFixStatus;
        private User user;
        private DiagnosticReportFixFeedback diagnosticReportFixFeedback;
        private Fix fix;
        private FixName fixName;
        private string name = "";
        private string name_es = "";
        private string name_fr = "";
        private string name_zh = "";
        private DateTime createdDateTimeUTC = DateTime.MinValue;

        //Added on 2017-09-08 by Nam Lu - INNOVA Dev Team
        private NullableDateTime proposedFixIdAcceptedDateTimeUTC = NullableDateTime.Null;

        private AdminUser proposedFixIdAcceptedBy;
        private NullableBoolean proposedFixIdAccepted = NullableBoolean.Null;
        private NullableGuid proposedFixId = NullableGuid.Null;
        //Added on 2017-09-08 by Nam Lu - INNOVA Dev Team

        #region Contructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new (one that does not exist, assigning a new <see cref="Guid"/> Id). DiagnosticReportFixFeedback object.
        /// In order to create a new DiagnosticReportFixFeedback which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <example>
        /// <code>
        /// DiagnosticReportFixFeedback o = (DiagnosticReportFixFeedback)Registry.CreateInstance(typeof(DiagnosticReportFixFeedback));
        /// </code>
        /// </example>
        protected internal ProRSFix()
            : base()
        {
            this.IsObjectCreated = true;
            this.IsObjectLoaded = true;
            this.IsObjectDirty = true;
        }

        /// <summary>
        /// Constructor creates an existing (one that exists, and already has a <see cref="Guid"/> Id).  DiagnosticReportFixFeedback object.
        /// In order to create an existing DiagnosticReportFixFeedback object, which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <param name="id">The <see cref="Guid"/> or SQL uniqueidentifier indentification "Id" of the current object.</param>
        /// <example>
        /// <code>
        /// DiagnosticReportFixFeedback o = (DiagnosticReportFixFeedback)Registry.CreateInstance(typeof(DiagnosticReportFixFeedback), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));
        /// </code>
        /// </example>
        protected internal ProRSFix(Guid id)
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

        //Added on 2017-09-08 by Nam Lu - INNOVA Dev Team
        /// <summary>
        /// ProposedFixIdAcceptedDateTimeUTC
        /// </summary>
        public NullableDateTime ProposedFixIdAcceptedDateTimeUTC
        {
            get
            {
                this.EnsureLoaded();
                return this.proposedFixIdAcceptedDateTimeUTC;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.proposedFixIdAcceptedDateTimeUTC)
                {
                    this.IsObjectDirty = true;
                    this.proposedFixIdAcceptedDateTimeUTC = value;
                    this.UpdatedField("ProposedFixIdAcceptedDateTimeUTC");
                }
            }
        }

        /// <summary>
        /// ProposedFixId
        /// </summary>
        public NullableGuid ProposedFixId
        {
            get
            {
                this.EnsureLoaded();
                return this.proposedFixId;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.proposedFixId)
                {
                    this.IsObjectDirty = true;
                    this.proposedFixId = value;
                    this.UpdatedField("ProposedFixId");
                }
            }
        }

        /// <summary>
        /// ProposedFixIdAccepted
        /// </summary>
        public NullableBoolean ProposedFixIdAccepted
        {
            get
            {
                this.EnsureLoaded();
                return this.proposedFixIdAccepted;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.proposedFixIdAccepted)
                {
                    this.IsObjectDirty = true;
                    this.proposedFixIdAccepted = value;
                    this.UpdatedField("ProposedFixIdAccepted");
                }
            }
        }

        /// <summary>
        /// ProposedFixIdAcceptedBy
        /// </summary>
        public AdminUser ProposedFixIdAcceptedBy
        {
            get
            {
                this.EnsureLoaded();
                return this.proposedFixIdAcceptedBy;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.proposedFixIdAcceptedBy)
                {
                    this.IsObjectDirty = true;
                    this.proposedFixIdAcceptedBy = value;
                    this.UpdatedField("AdminUserId_ProposedFixIdAcceptedBy");
                }
            }
        }

        //Added on 2017-09-08 by Nam Lu - INNOVA Dev Team

        /// <summary>
        /// Gets or sets the <see cref="DiagnosticReportFixFeedback"/> DiagnosticReportFixFeedback report.
        /// </summary>
        public DiagnosticReportFixFeedback DiagnosticReportFixFeedback
        {
            get
            {
                this.EnsureLoaded();
                return this.diagnosticReportFixFeedback;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.diagnosticReportFixFeedback)
                {
                    this.IsObjectDirty = true;
                    this.diagnosticReportFixFeedback = value;
                    this.UpdatedField("DiagnosticReportFixFeedbackId");
                }
            }
        }

        /// <summary>
        /// Gets or set the <see cref="string"/> primary error code for the fix.
        /// </summary>
        public string MilDtc
        {
            get
            {
                this.EnsureLoaded();
                return this.milDtc;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.milDtc)
                {
                    this.IsObjectDirty = true;
                    this.milDtc = value;
                    this.UpdatedField("MilDtc");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DiagnosticReportFixFeedbackStatus"/> diagnostic report fix feedback status.
        /// </summary>
        [PropertyDefinition("Status", "The feedback status of the report.")]
        public ProRSFixStatus ProRSFixStatus
        {
            get
            {
                this.EnsureLoaded();
                return this.proRSFixStatus;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.proRSFixStatus)
                {
                    this.IsObjectDirty = true;
                    this.proRSFixStatus = value;
                    this.UpdatedField("ProRSFixStatus");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="User"/> technician who is providing the fix feedback.
        /// </summary>
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
                if (value != this.user)
                {
                    this.IsObjectDirty = true;
                    this.user = value;
                    this.UpdatedField("UserId");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Fix"/> fix that the technician selects.
        /// </summary>
        [PropertyDefinition("Fix", "The fix.")]
        public Fix Fix
        {
            get
            {
                this.EnsureLoaded();
                return this.fix;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.fix)
                {
                    this.IsObjectDirty = true;
                    this.fix = value;
                    this.UpdatedField("FixId");
                }
            }
        }

        /// <summary>
		/// Gets or sets the <see cref="FixName"/> associated with the fix.
		/// </summary>
		[PropertyDefinition("Fix Name", "The name of the fix required.")]
        public FixName FixName
        {
            get
            {
                this.EnsureLoaded();
                return this.fixName;
            }
            set
            {
                this.EnsureLoaded();
                if (this.fixName != value)
                {
                    this.IsObjectDirty = true;
                    this.fixName = value;
                    this.UpdatedField("FixNameId");
                }
            }
        }

        /// <summary>
        /// </summary>
        [PropertyDefinition("Name", "Name.")]
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
        /// </summary>
        [PropertyDefinition("Name_es", "Name_es.")]
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
                if (value != this.name_es)
                {
                    this.IsObjectDirty = true;
                    this.name_es = value;
                    this.UpdatedField("Name_es");
                }
            }
        }

        /// <summary>
        /// </summary>
        [PropertyDefinition("Name_zh", "Name_zh.")]
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
                if (value != this.name_zh)
                {
                    this.IsObjectDirty = true;
                    this.name_zh = value;
                    this.UpdatedField("Name_zh");
                }
            }
        }

        /// <summary>
        /// </summary>
        [PropertyDefinition("Name_fr", "Name_fr.")]
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
                if (value != this.name_fr)
                {
                    this.IsObjectDirty = true;
                    this.name_fr = value;
                    this.UpdatedField("Name_fr");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/> date time the fix feedback report is created.
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

        #endregion Public Properties

        #region Object Properties (Related Objects)

        /*****************************************************************************************
		 *
		 * Object Relationships: Add correlated Object Collections
		 *
		*******************************************************************************************/

        #endregion Object Properties (Related Objects)

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
                        throw (new ApplicationException(
                            $"Load Failed for type {this.GetType()} using Procedure: {dr.ProcedureCall}"));
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
            dr.ProcedureName = "ProRSFix_Load";
            dr.AddGuid("ProRSFixId", this.Id);
        }

        /// <summary>
        /// Sets the properties of this object from the fields in the recordset located in the supplied <see cref="SqlDataReaderWrapper"/>.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> containing the recordset and fields used to set the properties of this object (this layer only).</param>
        protected new void SetPropertiesFromDataReader(SqlDataReaderWrapper dr)
        {
            this.diagnosticReportFixFeedback = (DiagnosticReportFixFeedback)dr.GetBusinessObjectBase(this.Registry, typeof(DiagnosticReportFixFeedback), "DiagnosticReportFixFeedbackId");
            this.milDtc = dr.GetString("MilDtc");
            this.proRSFixStatus = (ProRSFixStatus)dr.GetInt32("ProRSFixStatus");
            this.user = (User)dr.GetBusinessObjectBase(this.Registry, typeof(User), "UserId");
            this.user = (User)dr.GetBusinessObjectBase(this.Registry, typeof(User), "UserId");
            this.fix = (Fix)dr.GetBusinessObjectBase(this.Registry, typeof(Fix), "FixId");
            this.fixName = (FixName)dr.GetBusinessObjectBase(this.Registry, typeof(FixName), "FixNameId");
            this.name = dr.GetString("Name");
            this.name_es = dr.GetString("Name_es");
            this.name_zh = dr.GetString("Name_zh");
            this.name_fr = dr.GetString("Name_fr");
            this.createdDateTimeUTC = dr.GetDateTime("CreatedDateTimeUTC");

            //Added on 2017-09-08 by Nam Lu - INNOVA Dev Team
            this.proposedFixId = dr.GetNullableGuid("ProposedFixId");
            this.proposedFixIdAccepted = dr.GetNullableBoolean("ProposedFixIdAccepted");
            this.proposedFixIdAcceptedDateTimeUTC = dr.GetNullableDateTime("ProposedFixIdAcceptedDateTimeUTC");
            this.proposedFixIdAcceptedBy = (AdminUser)dr.GetBusinessObjectBase(this.Registry, typeof(AdminUser), "AdminUserId_ProposedFixIdAcceptedBy");
            //Added on 2017-09-08 by Nam Lu - INNOVA Dev Team

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
                        dr.ProcedureName = "ProRSFix_Create";
                        if (this.CreatedDateTimeUTC == DateTime.MinValue)
                            this.CreatedDateTimeUTC = DateTime.UtcNow;
                    }
                    else
                    {
                        dr.UpdateFields("ProRSFix", "ProRSFixId", this.updatedFields);
                    }

                    dr.AddGuid("ProRSFixId", this.Id);
                    dr.AddInt32("ProRSFixStatus", (int)this.ProRSFixStatus);
                    dr.AddBusinessObject("DiagnosticReportFixFeedbackId", this.DiagnosticReportFixFeedback);

                    dr.AddBusinessObject("UserId", this.User);
                    dr.AddBusinessObject("FixNameId", this.FixName);
                    dr.AddBusinessObject("FixId", this.Fix);
                    dr.AddNVarChar("MilDtc", this.MilDtc);

                    dr.AddNVarChar("Name", this.Name);
                    dr.AddNVarChar("Name_es", this.Name_es);
                    dr.AddNVarChar("Name_zh", this.Name_zh);
                    dr.AddNVarChar("Name_fr", this.Name_fr);

                    dr.AddDateTime("CreatedDateTimeUTC", this.CreatedDateTimeUTC);

                    //Added on 2017-09-08 by Nam Lu - INNOVA Dev Team
                    dr.AddGuid("ProposedFixId", this.ProposedFixId);
                    dr.AddBoolean("ProposedFixIdAccepted", this.ProposedFixIdAccepted);
                    dr.AddDateTime("ProposedFixIdAcceptedDateTimeUTC", this.ProposedFixIdAcceptedDateTimeUTC);
                    dr.AddBusinessObject("AdminUserId_ProposedFixIdAcceptedBy", this.ProposedFixIdAcceptedBy);
                    //Added on 2017-09-08 by Nam Lu - INNOVA Dev Team

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

            /* Example of deleting the items
			 * (This could potentially cause a lock since the property calls the lookup
			 *  with a new connection, implement a load method for the property in that case,
			 *  or created a specialied delete call which won't load the collection to loop and
			 *  delete, but in that case you won't be automatically calling the delete of the
			 *  child related items. See example below).
			 *
			// delete the child related objects
			foreach (ItemChild itemChild in ItemChilds)
			{
				transaction = itemChild.Delete(connection, transaction);
			}
			*/

            // delete this current object
            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                //remove related objects with a specialized delete.
                /*
				dr.ProcedureName = "ItemsOthers_Delete";
				dr.AddGuid("ItemId", Id);

				dr.ExecuteNonQuery(transaction);
				dr.ClearParameters();
				*/

                //delete the item
                dr.ProcedureName = "ProRSFix_Delete";
                dr.AddGuid("ProRSFixId", this.Id);

                dr.ExecuteNonQuery(transaction);
            }

            transaction = base.Delete(connection, transaction);

            return transaction;
        }

        #endregion Required Methods (Load, Save, Delete, Etc)
    }
}
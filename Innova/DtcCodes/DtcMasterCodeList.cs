using Metafuse3;
using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data.SqlClient;

namespace Innova.DtcCodes
{
    /// <summary>
    /// The DtcMasterCodeList object handles the business logic and data access for the specialized business object, DtcMasterCodeList.
    /// The class inherits from <see cref="BusinessObjectBase"/> class.
    /// </summary>
    /// <example>
    /// See the examples of how to construct the DtcMasterCodeList object.
    ///
    /// To create a new instance of a new of DtcMasterCodeList.
    /// <code>DtcMasterCodeList o = (DtcMasterCodeList)Registry.CreateInstance(typeof(DtcMasterCodeList));</code>
    ///
    /// To create an new instance of an existing DtcMasterCodeList.
    /// <code>DtcMasterCodeList o = (DtcMasterCodeList)Registry.CreateInstance(typeof(DtcMasterCodeList), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of DtcMasterCodeList, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectBase.ConnectionString"/> to the <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable()]
    public class DtcMasterCodeList : InnovaBusinessObjectBase
    {
        private string manufacturerName = "";
        private string errorCode = "";
        private string title = "";
        private string title_Spanish = "";
        private string title_French = "";

        // TODO The Title_zh property needs to be added to the db.
        private string title_zh = "";

        private bool hasMakeDefined = false;
        private string makesString = "";
        private bool isMakesDirty = false;

        private StringCollection makes;

        #region System Constructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new (one that does not exist, assigning a new <see cref="Guid"/> Id). DtcMasterCodeList object.
        /// In order to create a new DtcMasterCodeList which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <example>
        /// <code>
        /// DtcMasterCodeList o = (DtcMasterCodeList)Registry.CreateInstance(typeof(DtcMasterCodeList));
        /// </code>
        /// </example>
        protected internal DtcMasterCodeList() : base()
        {
            IsObjectCreated = true;
            IsObjectLoaded = true;
            IsObjectDirty = true;
        }

        /// <summary>
        /// Constructor creates an existing (one that exists, and already has a <see cref="Guid"/> Id).  DtcMasterCodeList object.
        /// In order to create an existing DtcMasterCodeList object, which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <param name="id">The <see cref="Guid"/> or SQL uniqueidentifier indentification "Id" of the current object.</param>
        /// <example>
        /// <code>
        /// DtcMasterCodeList o = (DtcMasterCodeList)Registry.CreateInstance(typeof(DtcMasterCodeList), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));
        /// </code>
        /// </example>
        protected internal DtcMasterCodeList(Guid id) : base(id)
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
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        public string ManufacturerName
        {
            get
            {
                this.EnsureLoaded();
                return this.manufacturerName;
            }
            set
            {
                this.EnsureLoaded();
                if (this.manufacturerName != value)
                {
                    this.IsObjectDirty = true;
                    this.manufacturerName = value;
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="bool"/> flag indicating whether or not the make is defined.
        /// </summary>
        public bool HasMakeDefined
        {
            get
            {
                this.EnsureLoaded();
                return this.hasMakeDefined;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> error code.
        /// </summary>
        public string ErrorCode
        {
            get
            {
                this.EnsureLoaded();
                return this.errorCode;
            }
            set
            {
                this.EnsureLoaded();
                if (this.errorCode != value)
                {
                    this.IsObjectDirty = true;
                    this.errorCode = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> title.
        /// </summary>
        public string Title
        {
            get
            {
                this.EnsureLoaded();
                return this.title;
            }
            set
            {
                this.EnsureLoaded();
                if (this.title != value)
                {
                    this.IsObjectDirty = true;
                    this.title = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> title in Spanish.
        /// </summary>
        public string Title_es
        {
            get
            {
                this.EnsureLoaded();
                return this.title_Spanish;
            }
            set
            {
                this.EnsureLoaded();
                if (this.title_Spanish != value)
                {
                    this.IsObjectDirty = true;
                    this.title_Spanish = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> title in French.
        /// </summary>
        public string Title_fr
        {
            get
            {
                this.EnsureLoaded();
                return this.title_French;
            }
            set
            {
                this.EnsureLoaded();
                if (this.title_French != value)
                {
                    this.IsObjectDirty = true;
                    this.title_French = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> title in Mandarin Chinese.
        /// </summary>
        public string Title_zh
        {
            get
            {
                this.EnsureLoaded();
                return this.title_zh;
            }
            set
            {
                this.EnsureLoaded();
                if (this.title_zh != value)
                {
                    this.IsObjectDirty = true;
                    this.title_zh = value;
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> title in the language specified by the RuntimeInfo.
        /// </summary>
        public string Title_Translated
        {
            get
            {
                this.EnsureLoaded();
                return this.RuntimeInfo.GetTranslatedValue(this.Title, this.Title_es, this.Title_fr, this.Title_zh);
            }
        }

        /// <summary>
        /// Gets the <see cref="int"/> count number of elements that have a specific item called out on the error code.
        /// </summary>
        public int HasDefinedCount
        {
            get
            {
                int count = 0;

                if (this.HasMakeDefined)
                {
                    count++;
                }

                return count;
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
        /// Get the <see cref="StringCollection"/> of vehicle makes that apply to this fix.
        /// NOTE: DO NOT add to this collection directly. Use the AddMake() method.
        /// </summary>
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

        #endregion Object Properties (Related Objects)

        #region Business Logic Methods

        /***********************************************************************************************
		 *
		 * Custom Business Logic
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Searchs for a collection of DtcCodes
        /// </summary>
        /// <param name="registry">The <see cref="Registry"/> object currently being used.</param>
        /// <param name="startCharacter">A <see cref="string"/> letter to be searched for as the first character in the title of the solution</param>
        /// <param name="orderBy">The <see cref="string"/> column to sort the results by.</param>
        /// <param name="sortDirection">The <see cref="SortDirection"/> direction in which to sort the results.</param>
        /// <param name="currentPage">The <see cref="int"/> desired page of results to return.</param>
        /// <param name="pageSize">The <see cref="int"/> number of results per page.</param>
        /// <param name="title">The <see cref="string"/> full or portion of the title of the DTC Master code. (To ignore, pass in String.Empty)</param>
        /// <param name="make">The <see cref="string"/> make of the DTC Master code. (To ignore, pass in String.Empty)</param>
        /// <param name="errorCode">The <see cref="string"/> error code for the desired DTC Master code. (To ignore, pass in String.Empty)</param>
        /// <returns>A <see cref="DtcMasterCodeListCollection"/> collection of matching <see cref="DtcMasterCodeList"/> objects.</returns>
        public static DtcMasterCodeListCollection Search(Registry registry, string startCharacter, string orderBy, SortDirection sortDirection, int currentPage, int pageSize, string title, string make, string errorCode)
        {
            return Search(registry, startCharacter, orderBy, sortDirection, currentPage, pageSize, title, make, errorCode, null);
        }

        /// <summary>
        /// Searchs for a collection of DtcCodes
        /// </summary>
        /// <param name="registry">The <see cref="Registry"/> object currently being used.</param>
        /// <param name="startCharacter">A <see cref="string"/> letter to be searched for as the first character in the title of the solution</param>
        /// <param name="orderBy">The <see cref="string"/> column to sort the results by.</param>
        /// <param name="sortDirection">The <see cref="SortDirection"/> direction in which to sort the results.</param>
        /// <param name="currentPage">The <see cref="int"/> desired page of results to return.</param>
        /// <param name="pageSize">The <see cref="int"/> number of results per page.</param>
        /// <param name="title">The <see cref="string"/> full or portion of the title of the DTC Master code. (To ignore, pass in String.Empty)</param>
        /// <param name="make">The <see cref="string"/> make of the DTC Master code. (To ignore, pass in String.Empty)</param>
        /// <param name="errorCode">The <see cref="string"/> error code for the desired DTC Master code. (To ignore, pass in String.Empty)</param>
        /// <param name="missingLanguage">The missing <see cref="Language"/> to find results for.</param>
        /// <returns>A <see cref="DtcMasterCodeListCollection"/> collection of matching <see cref="DtcMasterCodeList"/> objects.</returns>
        public static DtcMasterCodeListCollection Search(Registry registry, string startCharacter, string orderBy, SortDirection sortDirection, int currentPage, int pageSize, string title, string make, string errorCode, Language? missingLanguage)
        {
            DtcMasterCodeListCollection dtcCodes = new DtcMasterCodeListCollection(registry);

            SqlProcedureCommand call = new SqlProcedureCommand();

            call.ProcedureName = "DtcMasterCodeList_LoadBySearch";
            call.AddNVarChar("StartCharacter", startCharacter);
            call.AddNVarChar("OrderBy", orderBy);
            call.AddInt32("SortDirection", (int)sortDirection);
            call.AddInt32("CurrentPage", currentPage);
            call.AddInt32("PageSize", pageSize);
            call.AddNVarChar("Title", title);
            call.AddNVarChar("Make", make);
            call.AddNVarChar("ErrorCode", errorCode);
            if (missingLanguage.HasValue)
            {
                call.AddInt32("MissingLanguage", (int)missingLanguage.Value);
            }

            dtcCodes.Load(call, "DtcMasterCodeId", true, true, true);

            return dtcCodes;
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
                //load the base dtcMasterCodeList if user selected it.
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
            dr.ProcedureName = "DTCMasterCodeList_Load";
            dr.AddGuid("DTCMasterCodeId", Id);
        }

        /// <summary>
        /// Sets the properties of this object from the fields in the recordset located in the supplied <see cref="SqlDataReaderWrapper"/>.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> containing the recordset and fields used to set the properties of this object (this layer only).</param>
        protected new void SetPropertiesFromDataReader(SqlDataReaderWrapper dr)
        {
            manufacturerName = dr.GetString("ManufacturerName");
            hasMakeDefined = dr.GetBoolean("HasMakeDefined");
            makesString = dr.GetString("MakeString");
            errorCode = dr.GetString("ErrorCode");
            title = dr.GetString("Title");
            title_Spanish = dr.GetString("Title_Spanish");
            title_French = dr.GetString("Title_French");

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
                        dr.ProcedureName = "DTCMasterCodeList_Create";
                    }
                    else
                    {
                        dr.ProcedureName = "DTCMasterCodeList_Save";
                    }

                    dr.AddGuid("DTCMasterCodeId", Id);
                    dr.AddNVarChar("ManufacturerName", ManufacturerName);

                    dr.AddBoolean("HasMakeDefined", HasMakeDefined);
                    if (this.isMakesDirty)
                    {
                        this.makesString = this.BuildStringList(this.Makes);
                    }

                    dr.AddNVarChar("MakeString", this.makesString);
                    dr.AddNVarChar("ErrorCode", ErrorCode);
                    dr.AddNVarChar("Title", this.Title);
                    dr.AddNVarChar("Title_Spanish", this.Title_es);
                    dr.AddNVarChar("Title_French", this.Title_fr);

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
            if (this.isMakesDirty)
            {
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    dr.ProcedureName = "DTCMasterCodeList_SaveMakes";
                    dr.AddGuid("DTCMasterCodeId", Id);
                    dr.AddNText("MakesXmlList", Metafuse3.Xml.XmlList.ToXml(this.Makes));
                    dr.ExecuteNonQuery(transaction);
                }
                this.isMakesDirty = false;
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

            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                dr.ProcedureName = "DTCMasterCodeMake_DeleteByDTCMasterCode";
                dr.AddGuid("DTCMasterCodeId", this.Id);
                dr.ExecuteNonQuery(transaction);
            }

            // delete this current object
            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                //delete the dtcMasterCodeList
                dr.ProcedureName = "DtcMasterCodeList_Delete";
                dr.AddGuid("DtcMasterCodeListId", this.Id);

                dr.ExecuteNonQuery(transaction);
            }

            transaction = base.Delete(connection, transaction);

            return transaction;
        }

        #endregion Required Object Methods (Load, Save, Delete, Etc)
    }
}
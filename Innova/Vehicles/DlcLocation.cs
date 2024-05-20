using Innova.Users;
using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using Metafuse3.NullableTypes;
using System;
using System.Data.SqlClient;

namespace Innova.Vehicles
{
    /// <summary>
    /// The DlcLocation object handles the business logic and data access for the specialized business object, DlcLocation.
    /// The class inherits from <see cref="BusinessObjectBase"/> class.
    /// </summary>
    /// <example>
    /// See the examples of how to construct the DlcLocation object.
    ///
    /// To create a new instance of a new of DlcLocation.
    /// <code>DlcLocation o = (DlcLocation)Registry.CreateInstance(typeof(DlcLocation));</code>
    ///
    /// To create an new instance of an existing DlcLocation.
    /// <code>DlcLocation o = (DlcLocation)Registry.CreateInstance(typeof(DlcLocation), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of DlcLocation, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectBase.ConnectionString"/> to the <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable()]
    public class DlcLocation : InnovaBusinessObjectBase
    {
        private string year = "";
        private string make = "";
        private string model = "";
        private NullableInt32 locationNumber = NullableInt32.Null;
        private string access = "";
        private string access_French = "";
        private string access_Spanish = "";
        private string comments = "";
        private string comments_French = "";
        private string comments_Spanish = "";
        private string imageFileName = "";

        #region System Constructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new (one that does not exist, assigning a new <see cref="Guid"/> Id). DlcLocation object.
        /// In order to create a new DlcLocation which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <example>
        /// <code>
        /// DlcLocation o = (DlcLocation)Registry.CreateInstance(typeof(DlcLocation));
        /// </code>
        /// </example>
        protected internal DlcLocation() : base()
        {
            IsObjectCreated = true;
            IsObjectLoaded = true;
            IsObjectDirty = true;
        }

        /// <summary>
        /// Constructor creates an existing (one that exists, and already has a <see cref="Guid"/> Id).  DlcLocation object.
        /// In order to create an existing DlcLocation object, which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <param name="id">The <see cref="Guid"/> or SQL uniqueidentifier indentification "Id" of the current object.</param>
        /// <example>
        /// <code>
        /// DlcLocation o = (DlcLocation)Registry.CreateInstance(typeof(DlcLocation), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));
        /// </code>
        /// </example>
        protected internal DlcLocation(Guid id) : base(id)
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
        public string Year
        {
            get
            {
                EnsureLoaded();
                return year;
            }
            set
            {
                EnsureLoaded();
                if (year != value)
                {
                    IsObjectDirty = true;
                    year = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        public string Make
        {
            get
            {
                EnsureLoaded();
                return make;
            }
            set
            {
                EnsureLoaded();
                if (make != value)
                {
                    IsObjectDirty = true;
                    make = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        public string Model
        {
            get
            {
                EnsureLoaded();
                return model;
            }
            set
            {
                EnsureLoaded();
                if (model != value)
                {
                    IsObjectDirty = true;
                    model = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableInt32"/>
        /// </summary>
        public NullableInt32 LocationNumber
        {
            get
            {
                EnsureLoaded();
                return locationNumber;
            }
            set
            {
                EnsureLoaded();
                if (locationNumber != value)
                {
                    IsObjectDirty = true;
                    locationNumber = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        public string Access
        {
            get
            {
                EnsureLoaded();
                return access;
            }
            set
            {
                EnsureLoaded();
                if (access != value)
                {
                    IsObjectDirty = true;
                    access = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        public string Access_French
        {
            get
            {
                EnsureLoaded();
                return access_French;
            }
            set
            {
                EnsureLoaded();
                if (access_French != value)
                {
                    IsObjectDirty = true;
                    access_French = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        public string Access_Spanish
        {
            get
            {
                EnsureLoaded();
                return access_Spanish;
            }
            set
            {
                EnsureLoaded();
                if (access_Spanish != value)
                {
                    IsObjectDirty = true;
                    access_Spanish = value;
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> access description in the language specified in the <see cref="RuntimeInfo"/>.
        /// </summary>
        public string Access_Translated
        {
            get
            {
                this.EnsureLoaded();
                return this.RuntimeInfo.GetTranslatedValue(this.Access, this.Access_Spanish, this.Access_French, "");
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        public string Comments
        {
            get
            {
                EnsureLoaded();
                return comments;
            }
            set
            {
                EnsureLoaded();
                if (comments != value)
                {
                    IsObjectDirty = true;
                    comments = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        public string Comments_French
        {
            get
            {
                EnsureLoaded();
                return comments_French;
            }
            set
            {
                EnsureLoaded();
                if (comments_French != value)
                {
                    IsObjectDirty = true;
                    comments_French = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        public string Comments_Spanish
        {
            get
            {
                EnsureLoaded();
                return comments_Spanish;
            }
            set
            {
                EnsureLoaded();
                if (comments_Spanish != value)
                {
                    IsObjectDirty = true;
                    comments_Spanish = value;
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> comments in the language specified in the <see cref="RuntimeInfo"/>.
        /// </summary>
        public string Comments_Translated
        {
            get
            {
                this.EnsureLoaded();
                return this.RuntimeInfo.GetTranslatedValue(this.Comments, this.Comments_Spanish, this.Comments_French, "");
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        public string ImageFileName
        {
            get
            {
                EnsureLoaded();
                return imageFileName;
            }
            set
            {
                EnsureLoaded();
                if (imageFileName != value)
                {
                    IsObjectDirty = true;
                    imageFileName = value;
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
        ///
        /// </summary>
        /// <param name="externalSystem"></param>
        /// <param name="getThumbnailUrl"></param>
        /// <returns></returns>
        public string GetImageRelativeUrl(ExternalSystem externalSystem, bool getThumbnailUrl)
        {
            if (String.IsNullOrWhiteSpace(this.ImageFileName))
            {
                return "";
            }
            else
            {
                string imageNameSuffix = "";

                if (externalSystem != null)
                {
                    imageNameSuffix = externalSystem.ImageNameSuffix;
                }

                if (getThumbnailUrl)
                {
                    imageNameSuffix += "sm";
                }

                if (imageNameSuffix.Length > 0)
                {
                    imageNameSuffix = "-" + imageNameSuffix;
                }

                string makeName = this.Make.ToLower();
                makeName = makeName.Replace(" ", "_");
                makeName = makeName.Replace("-", "_");

                string modelName = this.Model.ToLower();
                modelName = modelName.Replace(" ", "_");
                modelName = modelName.Replace("/", "_");

                return makeName + "/" + modelName + "/" + this.ImageFileName.ToLower() + imageNameSuffix + ".jpg";
            }
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
                //load the base dlcLocation if user selected it.
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
            dr.ProcedureName = "DLCLocation_Load";
            dr.AddGuid("DLCLocationId", Id);
        }

        /// <summary>
        /// Sets the properties of this object from the fields in the recordset located in the supplied <see cref="SqlDataReaderWrapper"/>.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> containing the recordset and fields used to set the properties of this object (this layer only).</param>
        protected new void SetPropertiesFromDataReader(SqlDataReaderWrapper dr)
        {
            year = dr.GetString("Year");
            make = dr.GetString("Make");
            model = dr.GetString("Model");
            locationNumber = dr.GetNullableInt32("LocationNumber");
            access = dr.GetString("Access");
            access_French = dr.GetString("Access_French");
            access_Spanish = dr.GetString("Access_Spanish");
            comments = dr.GetString("Comments");
            comments_French = dr.GetString("Comments_French");
            comments_Spanish = dr.GetString("Comments_Spanish");
            imageFileName = dr.GetString("ImageFileName");

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
                        dr.ProcedureName = "DLCLocation_Create";
                    }
                    else
                    {
                        dr.ProcedureName = "DLCLocation_Save";
                    }

                    dr.AddGuid("DlcLocationId", Id);
                    dr.AddNVarChar("Year", Year);
                    dr.AddNVarChar("Make", Make);
                    dr.AddNVarChar("Model", Model);
                    dr.AddInt32("LocationNumber", LocationNumber);
                    dr.AddNVarChar("Access", Access);
                    dr.AddNVarChar("Access_French", Access_French);
                    dr.AddNVarChar("Access_Spanish", Access_Spanish);
                    dr.AddNVarChar("Comments", Comments);
                    dr.AddNVarChar("Comments_French", Comments_French);
                    dr.AddNVarChar("Comments_Spanish", Comments_Spanish);
                    dr.AddNVarChar("ImageFileName", ImageFileName);

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

        #endregion Required Object Methods (Load, Save, Delete, Etc)
    }
}
using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using System;
using System.Data.SqlClient;

namespace Innova.Articles
{
    /// <summary>
    /// The ArticleCategory object handles the business logic and data access for the specialized business object, ArticleCategory.
    /// The class inherits from <see cref="BusinessObjectBase"/> class.
    /// </summary>
    /// <example>
    /// See the examples of how to construct the ArticleCategory object.
    ///
    /// To create a new instance of a new of ArticleCategory.
    /// <code>ArticleCategory o = (ArticleCategory)Registry.CreateInstance(typeof(ArticleCategory));</code>
    ///
    /// To create an new instance of an existing ArticleCategory.
    /// <code>ArticleCategory o = (ArticleCategory)Registry.CreateInstance(typeof(ArticleCategory), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of ArticleCategory, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectBase.ConnectionString"/> to the <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable()]
    [ClassDefinition("Article Category", "Article Categoryies", "Article Category", "ArticleCategoryId")]
    public class ArticleCategory : BusinessObjectBase
    {
        private ArticleCategory articleCategoryContainer;
        private string name = "";
        private bool isCarMD;
        private bool isCanOBD2;
        private bool isOBDFix;
        private int sortOrder;
        private DateTime updatedDateTimeUTC;
        private DateTime createdDateTimeUTC;

        #region System Constructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new (one that does not exist, assigning a new <see cref="Guid"/> Id). ArticleCategory object.
        /// In order to create a new ArticleCategory which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <example>
        /// <code>
        /// ArticleCategory o = (ArticleCategory)Registry.CreateInstance(typeof(ArticleCategory));
        /// </code>
        /// </example>
        protected internal ArticleCategory() : base()
        {
            IsObjectCreated = true;
            IsObjectLoaded = true;
            IsObjectDirty = true;
        }

        /// <summary>
        /// Constructor creates an existing (one that exists, and already has a <see cref="Guid"/> Id).  ArticleCategory object.
        /// In order to create an existing ArticleCategory object, which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <param name="id">The <see cref="Guid"/> or SQL uniqueidentifier indentification "Id" of the current object.</param>
        /// <example>
        /// <code>
        /// ArticleCategory o = (ArticleCategory)Registry.CreateInstance(typeof(ArticleCategory), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));
        /// </code>
        /// </example>
        protected internal ArticleCategory(Guid id) : base(id)
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
        /// Gets or sets the <see cref="ArticleCategory"/> container
        /// </summary>
        public ArticleCategory ArticleCategoryContainer
        {
            get
            {
                EnsureLoaded();
                return articleCategoryContainer;
            }
            set
            {
                EnsureLoaded();
                if (articleCategoryContainer != value)
                {
                    IsObjectDirty = true;
                    articleCategoryContainer = value;
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
                EnsureLoaded();
                return name;
            }
            set
            {
                EnsureLoaded();
                if (name != value)
                {
                    IsObjectDirty = true;
                    name = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/>
        /// </summary>
        public bool IsCarMD
        {
            get
            {
                EnsureLoaded();
                return isCarMD;
            }
            set
            {
                EnsureLoaded();
                if (isCarMD != value)
                {
                    IsObjectDirty = true;
                    isCarMD = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/>
        /// </summary>
        public bool IsCanOBD2
        {
            get
            {
                EnsureLoaded();
                return isCanOBD2;
            }
            set
            {
                EnsureLoaded();
                if (isCanOBD2 != value)
                {
                    IsObjectDirty = true;
                    isCanOBD2 = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/>
        /// </summary>
        public bool IsOBDFix
        {
            get
            {
                EnsureLoaded();
                return isOBDFix;
            }
            set
            {
                EnsureLoaded();
                if (isOBDFix != value)
                {
                    IsObjectDirty = true;
                    isOBDFix = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/> order in which the article category will be displayed.
        /// </summary>
        public int SortOrder
        {
            get
            {
                EnsureLoaded();
                return sortOrder;
            }
            set
            {
                EnsureLoaded();
                if (sortOrder != value)
                {
                    IsObjectDirty = true;
                    sortOrder = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/>
        /// </summary>
        public DateTime UpdatedDateTimeUTC
        {
            get
            {
                EnsureLoaded();
                return updatedDateTimeUTC;
            }
            set
            {
                EnsureLoaded();
                if (updatedDateTimeUTC != value)
                {
                    IsObjectDirty = true;
                    updatedDateTimeUTC = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/>
        /// </summary>
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

        #endregion Object Properties (Related Objects)

        #region Business Logic Methods

        /***********************************************************************************************
		 *
		 * Custom Business Logic
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Gets a collection of all categories in the system.
        /// </summary>
        /// <param name="registry">The <see cref="Registry"/> currently in use.</param>
        /// <param name="parentCategory">The <see cref="ArticleCategory"/> parent category.</param>
        /// <param name="mustContainActiveArticles">A <see cref="bool"/> that indicates if the categories must contain active articles.</param>
        /// <returns>A <see cref="ArticleCategoryCollection"/> of <see cref="ArticleCategory"/> objects.</returns>
        public static ArticleCategoryCollection GetSubCategories(Registry registry, ArticleCategory parentCategory, bool mustContainActiveArticles)
        {
            ArticleCategoryCollection cats = new ArticleCategoryCollection(registry);

            SqlProcedureCommand call = new SqlProcedureCommand();

            call.ProcedureName = "ArticleCategory_LoadByParentCategory";
            if (parentCategory != null)
            {
                call.AddGuid("ArticleCategoryIdContainer", parentCategory.Id);
            }
            call.AddBoolean("MustContainActiveArticles", mustContainActiveArticles);

            cats.Load(call, "ArticleCategoryId", true, true);

            return cats;
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
                //load the base articleCategory if user selected it.
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
            dr.ProcedureName = "ArticleCategory_Load";
            dr.AddGuid("ArticleCategoryId", Id);
        }

        /// <summary>
        /// Sets the properties of this object from the fields in the recordset located in the supplied <see cref="SqlDataReaderWrapper"/>.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> containing the recordset and fields used to set the properties of this object (this layer only).</param>
        protected new void SetPropertiesFromDataReader(SqlDataReaderWrapper dr)
        {
            if (!dr.IsDBNull("ArticleCategoryIdContainer"))
            {
                articleCategoryContainer = (ArticleCategory)Registry.CreateInstance(typeof(ArticleCategory), dr.GetGuid("ArticleCategoryIdContainer"));
            }
            name = dr.GetString("Name");
            isCarMD = dr.GetBoolean("IsCarMD");
            isCanOBD2 = dr.GetBoolean("IsCanOBD2");
            isOBDFix = dr.GetBoolean("IsOBDFix");
            sortOrder = dr.GetInt32("SortOrder");
            updatedDateTimeUTC = dr.GetDateTime("UpdatedDateTimeUTC");
            createdDateTimeUTC = dr.GetDateTime("CreatedDateTimeUTC");

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
                        dr.ProcedureName = "ArticleCategory_Create";
                        CreatedDateTimeUTC = DateTime.UtcNow;
                        UpdatedDateTimeUTC = CreatedDateTimeUTC;
                    }
                    else
                    {
                        dr.ProcedureName = "ArticleCategory_Save";
                    }

                    dr.AddGuid("ArticleCategoryId", Id);
                    if (ArticleCategoryContainer != null)
                    {
                        dr.AddGuid("ArticleCategoryIdContainer", ArticleCategoryContainer.Id);
                    }
                    dr.AddNVarChar("Name", Name);
                    dr.AddBoolean("IsCarMD", IsCarMD);
                    dr.AddBoolean("IsCanOBD2", IsCanOBD2);
                    dr.AddBoolean("IsOBDFix", IsOBDFix);
                    dr.AddInt32("SortOrder", sortOrder);
                    dr.AddDateTime("UpdatedDateTimeUTC", UpdatedDateTimeUTC);
                    dr.AddDateTime("CreatedDateTimeUTC", CreatedDateTimeUTC);

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

        #endregion Required Object Methods (Load, Save, Delete, Etc)
    }
}
using Innova.Users;
using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using Metafuse3.NullableTypes;
using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Innova.Articles
{
    /// <summary>
    /// The Article object handles the business logic and data access for the specialized business object, Article.
    /// The class inherits from <see cref="BusinessObjectBase"/> class.
    /// </summary>
    /// <example>
    /// See the examples of how to construct the Article object.
    ///
    /// To create a new instance of a new of Article.
    /// <code>Article o = (Article)Registry.CreateInstance(typeof(Article));</code>
    ///
    /// To create an new instance of an existing Article.
    /// <code>Article o = (Article)Registry.CreateInstance(typeof(Article), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of Article, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectBase.ConnectionString"/> to the <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable()]
    [ClassDefinition("Article", "Articles", "Article", "ArticleId")]
    public class Article : BusinessObjectBase
    {
        private ArticleCategory articleCategoryPrimary;
        private string urlName = "";
        private string urlNamePrevious = "";
        private AdminUser adminUserCreated;
        private AdminUser adminUserUpdated;
        private AdminUser adminUserLastReviewed;
        private ArticleType articleType;
        private string title = "";
        private string summary = "";
        private string body = "";
        private string keywords = "";
        private DateTime date;
        private string author = "";
        private DateTime startDate;
        private NullableDateTime endDate = NullableDateTime.Null;
        private int sortOrder;
        private string videoUrl = "";
        private string videoThumbnailUrl = "";
        private int videoDurationSeconds = 0;
        private int videoHeight = 0;
        private int videoWidth = 0;
        private bool isActive;
        private bool isCarMD;
        private bool isCanOBD2;
        private bool isOBDFix;
        private bool isFree;
        private DateTime lastReviewedDateTimeUTC;
        private DateTime createdDateTimeUTC;
        private DateTime updatedDateTimeUTC;

        private ArticleCategoryCollection articleCategories;

        #region System Constructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new (one that does not exist, assigning a new <see cref="Guid"/> Id). Article object.
        /// In order to create a new Article which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <example>
        /// <code>
        /// Article o = (Article)Registry.CreateInstance(typeof(Article));
        /// </code>
        /// </example>
        protected internal Article() : base()
        {
            IsObjectCreated = true;
            IsObjectLoaded = true;
            IsObjectDirty = true;
        }

        /// <summary>
        /// Constructor creates an existing (one that exists, and already has a <see cref="Guid"/> Id).  Article object.
        /// In order to create an existing Article object, which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <param name="id">The <see cref="Guid"/> or SQL uniqueidentifier indentification "Id" of the current object.</param>
        /// <example>
        /// <code>
        /// Article o = (Article)Registry.CreateInstance(typeof(Article), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));
        /// </code>
        /// </example>
        protected internal Article(Guid id) : base(id)
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
        /// Gets or sets the <see cref="ArticleCategory"/> primary category
        /// </summary>
        [PropertyDefinition("Primary Category", "The primary category of the article.")]
        [PropertyDBField("ArticleCategoryIdPrimary")]
        public ArticleCategory ArticleCategoryPrimary
        {
            get
            {
                EnsureLoaded();
                return articleCategoryPrimary;
            }
            set
            {
                EnsureLoaded();
                if (articleCategoryPrimary != value)
                {
                    IsObjectDirty = true;
                    articleCategoryPrimary = value;
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> url name of the article, set when the article is marked active.
        /// </summary>
        public string URLName
        {
            get
            {
                this.EnsureLoaded();
                return this.urlName;
            }
            set
            {
                this.EnsureLoaded();
                if (this.urlName != value)
                {
                    this.IsObjectDirty = true;
                    this.urlName = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="AdminUser"/> who created the article.
        /// </summary>
        public AdminUser AdminUserCreated
        {
            get
            {
                EnsureLoaded();
                return adminUserCreated;
            }
            set
            {
                EnsureLoaded();
                if (adminUserCreated != value)
                {
                    IsObjectDirty = true;
                    adminUserCreated = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="AdminUser"/> who last updated the article.
        /// </summary>
        public AdminUser AdminUserUpdated
        {
            get
            {
                EnsureLoaded();
                return adminUserUpdated;
            }
            set
            {
                EnsureLoaded();
                if (adminUserUpdated != value)
                {
                    IsObjectDirty = true;
                    adminUserUpdated = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="AdminUser"/> who last reviewed the article.
        /// </summary>
        public AdminUser AdminUserLastReviewed
        {
            get
            {
                EnsureLoaded();
                return adminUserLastReviewed;
            }
            set
            {
                EnsureLoaded();
                if (adminUserLastReviewed != value)
                {
                    IsObjectDirty = true;
                    adminUserLastReviewed = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="ArticleType"/> ArticleType.
        /// </summary>
        [PropertyDefinition("Type", "The type of article.")]
        [PropertyDBField("ArticleTypeTitle")]
        public ArticleType ArticleType
        {
            get
            {
                EnsureLoaded();
                return articleType;
            }
            set
            {
                EnsureLoaded();
                if (articleType != value)
                {
                    IsObjectDirty = true;
                    articleType = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> title.
        /// </summary>
        [PropertyDefinition("Title", "The title for the article.")]
        [PropertyDBField("Title")]
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
        /// Gets or sets the <see cref="string"/> summary.
        /// </summary>
        [PropertyDefinition("Summary", "The summary for the article.")]
        [PropertyDBField("Summary")]
        public string Summary
        {
            get
            {
                this.EnsureLoaded();
                return this.summary;
            }
            set
            {
                this.EnsureLoaded();
                if (this.summary != value)
                {
                    this.IsObjectDirty = true;
                    this.summary = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        [PropertyDefinition("Body", "The body for the article.")]
        [PropertyDBField("Body")]
        public string Body
        {
            get
            {
                EnsureLoaded();
                return body;
            }
            set
            {
                EnsureLoaded();
                if (body != value)
                {
                    IsObjectDirty = true;
                    body = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        [PropertyDefinition("Keywords", "The keywords assigned to the article for search engine optimization.")]
        [PropertyDBField("Keywords")]
        public string Keywords
        {
            get
            {
                EnsureLoaded();
                return keywords;
            }
            set
            {
                EnsureLoaded();
                if (keywords != value)
                {
                    IsObjectDirty = true;
                    keywords = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/>
        /// </summary>
        [PropertyDefinition("Article Date", "The assigned publish or creation date for the article.")]
        [PropertyDBField("Date")]
        public DateTime Date
        {
            get
            {
                EnsureLoaded();
                return date;
            }
            set
            {
                EnsureLoaded();
                if (date != value)
                {
                    IsObjectDirty = true;
                    date = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/>
        /// </summary>
        [PropertyDefinition("Author", "The author of the article.")]
        [PropertyDBField("Author")]
        public string Author
        {
            get
            {
                EnsureLoaded();
                return author;
            }
            set
            {
                EnsureLoaded();
                if (author != value)
                {
                    IsObjectDirty = true;
                    author = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/>
        /// </summary>
        [PropertyDefinition("Start Date", "The date when the article should become available for viewing.")]
        [PropertyDBField("StartDate")]
        public DateTime StartDate
        {
            get
            {
                EnsureLoaded();
                return startDate;
            }
            set
            {
                EnsureLoaded();
                if (startDate != value)
                {
                    IsObjectDirty = true;
                    startDate = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="NullableDateTime"/>
        /// </summary>
        [PropertyDefinition("End Date", "The date when the article should no longer be available for viewing.")]
        [PropertyDBField("EndDate")]
        public NullableDateTime EndDate
        {
            get
            {
                EnsureLoaded();
                return endDate;
            }
            set
            {
                EnsureLoaded();
                if (endDate != value)
                {
                    IsObjectDirty = true;
                    endDate = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/> sort order
        /// </summary>
        [PropertyDefinition("Sort Order", "An optional numberic value by which the article can be sorted.")]
        [PropertyDBField("SortOrder")]
        public int SortOrder
        {
            get
            {
                this.EnsureLoaded();
                return this.sortOrder;
            }
            set
            {
                this.EnsureLoaded();
                if (this.sortOrder != value)
                {
                    this.IsObjectDirty = true;
                    this.sortOrder = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> video URL.
        /// </summary>
        public string VideoUrl
        {
            get
            {
                this.EnsureLoaded();
                return this.videoUrl;
            }
            set
            {
                this.EnsureLoaded();
                if (this.videoUrl != value)
                {
                    this.IsObjectDirty = true;
                    this.videoUrl = value;
                    this.UpdatedField("VideoUrl");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> video thumbnail URL.
        /// </summary>
        public string VideoThumbnailUrl
        {
            get
            {
                this.EnsureLoaded();
                return this.videoThumbnailUrl;
            }
            set
            {
                this.EnsureLoaded();
                if (this.videoThumbnailUrl != value)
                {
                    this.IsObjectDirty = true;
                    this.videoThumbnailUrl = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/> duration of the video in seconds
        /// </summary>
        public int VideoDurationSeconds
        {
            get
            {
                this.EnsureLoaded();
                return this.videoDurationSeconds;
            }
            set
            {
                this.EnsureLoaded();
                if (this.videoDurationSeconds != value)
                {
                    this.IsObjectDirty = true;
                    this.videoDurationSeconds = value;
                    this.UpdatedField("VideoDurationSeconds");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/> height of the video
        /// </summary>
        public int VideoHeight
        {
            get
            {
                this.EnsureLoaded();
                return this.videoHeight;
            }
            set
            {
                this.EnsureLoaded();
                if (this.videoHeight != value)
                {
                    this.IsObjectDirty = true;
                    this.videoHeight = value;
                    this.UpdatedField("VideoHeight");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/> width of the video
        /// </summary>
        public int VideoWidth
        {
            get
            {
                this.EnsureLoaded();
                return this.videoWidth;
            }
            set
            {
                this.EnsureLoaded();
                if (this.videoWidth != value)
                {
                    this.IsObjectDirty = true;
                    this.videoWidth = value;
                    this.UpdatedField("VideoWidth");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/> indicating if the article is active.
        /// </summary>
        [PropertyDefinition("Active", "Declares if the article is active.")]
        [PropertyDBField("IsActive")]
        public bool IsActive
        {
            get
            {
                EnsureLoaded();
                return isActive;
            }
            set
            {
                EnsureLoaded();
                if (isActive != value)
                {
                    IsObjectDirty = true;
                    isActive = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/> indicating if the article is available for viewing on the CarMD site.
        /// </summary>
        [PropertyDefinition("CarMD", "Declares if the article is available for viewing on the CarMD site.")]
        [PropertyDBField("IsCarMD")]
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
        /// Gets or sets the <see cref="bool"/> indicating if the article is available for viewing on the CanOBD2 site.
        /// </summary>
        [PropertyDefinition("CanOBD2", "Declares if the article is available for viewing on the CanOBD2 site.")]
        [PropertyDBField("IsCanOBD2")]
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
        /// Gets or sets the <see cref="bool"/> indicating if the article is available for viewing on the OBDFix site.
        /// </summary>
        [PropertyDefinition("OBDFix", "Declares if the article is available for viewing on the OBDFix site.")]
        [PropertyDBField("IsOBDFix")]
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
        /// Gets or sets the <see cref="bool"/> indicating if the article is available for free.
        /// </summary>
        [PropertyDefinition("Free", "Indicates if the article is available for viewing on the OBDFix site.")]
        [PropertyDBField("IsFree")]
        public bool IsFree
        {
            get
            {
                this.EnsureLoaded();
                return this.isFree;
            }
            set
            {
                this.EnsureLoaded();
                if (this.isFree != value)
                {
                    this.IsObjectDirty = true;
                    this.isFree = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/>
        /// </summary>
        public DateTime LastReviewedDateTimeUTC
        {
            get
            {
                EnsureLoaded();
                return lastReviewedDateTimeUTC;
            }
            set
            {
                EnsureLoaded();
                if (lastReviewedDateTimeUTC != value)
                {
                    IsObjectDirty = true;
                    lastReviewedDateTimeUTC = value;
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

        #endregion Object Properties

        #region Object Properties (Related Objects)

        /*****************************************************************************************
		 *
		 * Object Relationships: Add correlated Object Collections
		 *
		*******************************************************************************************/

        /// <summary>
        /// Gets the <see cref="ArticleCategoryCollection"/> line items.
        /// </summary>
        [PropertyDefinition("Categories", "The categories to which the article is assigned.")]
        public ArticleCategoryCollection ArticleCategories
        {
            get
            {
                if (articleCategories == null)
                {
                    articleCategories = new ArticleCategoryCollection(Registry);

                    //load if not a user created element
                    if (!isObjectCreated)
                    {
                        SqlProcedureCommand call = new SqlProcedureCommand();
                        EnsureValidId();

                        call.ProcedureName = "ArticleCategory_LoadByArticle";
                        call.AddGuid("ArticleId", Id);

                        articleCategories.Load(call, "ArticleCategoryId", true, true);
                    }
                }

                return articleCategories;
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
        /// Gets the <see cref="string"/> search engine friendly name for the input string.  Converts the title/name into a search engine name
        /// </summary>
        /// <param name="name"><see cref="string"/>name to convert into search engine friendly URL name</param>
        /// <returns><see cref="string"/> search engine URL name</returns>
        public static string GetSearchEngineURLName(string name)
        {
            //replace & with word and
            name = name.Replace("&", "and");

            //lower case the name
            name = name.ToLower();

            //remove double spaces
            name = Regex.Replace(name, @"\ \ *", " ");  // for multiple spaces

            //remove invalid chars
            name = Regex.Replace(name, @"[^A-Za-z0-9_\-\.\ ]*", "");

            name = Regex.Replace(name, @"\.", "_");

            if (name.EndsWith("_"))
            {
                //remove the period the name ends with
                name = name.Remove(name.Length - 1);
            }

            //replace space for dashes
            name = name.Replace(" ", "-");

            //remove double dashes
            name = Regex.Replace(name, @"\-\-*", "-");

            //remove double underscores
            name = Regex.Replace(name, @"__*", "_");

            return name;
        }

        /// <summary>
        /// Gets the <see cref="string"/> URL for the video
        /// </summary>
        /// <param name="videoBaseUrl"><see cref="string"/> video base URL.</param>
        /// <returns><see cref="string"/>URL to the video.</returns>
        public string GetFullyQualifiedVideoURL(string videoBaseUrl)
        {
            if (!String.IsNullOrEmpty(this.VideoUrl))
            {
                //if we're starting with HTTP we have the full URL in the body, just return it
                if (this.VideoUrl.ToLower().StartsWith("http"))
                {
                    return this.VideoUrl;
                }
                else
                {
                    //otherwise we have to work on the URL here
                    string url = videoBaseUrl.Trim();

                    //if both have slashes, we need to remove one
                    if (url.EndsWith("/") && this.VideoUrl.StartsWith("/"))
                    {
                        url = url + this.VideoUrl.Substring(1).Trim();
                    }
                    //if neither have slashes, we need to add one
                    else if (!url.EndsWith("/") && !this.VideoUrl.StartsWith("/"))
                    {
                        url = url + "/" + this.VideoUrl.Trim();
                    }
                    //only one must have a slash so just put them together
                    else
                    {
                        url = url + this.VideoUrl.Trim();
                    }

                    return url;
                }
            }

            return "";
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
                //load the base article if user selected it.
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
            dr.ProcedureName = "Article_Load";
            dr.AddGuid("ArticleId", Id);
        }

        /// <summary>
        /// Sets the properties of this object from the fields in the recordset located in the supplied <see cref="SqlDataReaderWrapper"/>.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> containing the recordset and fields used to set the properties of this object (this layer only).</param>
        protected new void SetPropertiesFromDataReader(SqlDataReaderWrapper dr)
        {
            this.articleCategoryPrimary = (ArticleCategory)this.Registry.CreateInstance(typeof(ArticleCategory), dr.GetGuid("ArticleCategoryIdPrimary"));
            this.urlName = dr.GetString("URLName");

            this.urlNamePrevious = this.urlName;

            this.adminUserCreated = (AdminUser)this.Registry.CreateInstance(typeof(AdminUser), dr.GetGuid("AdminUserIdCreated"));
            this.adminUserUpdated = (AdminUser)this.Registry.CreateInstance(typeof(AdminUser), dr.GetGuid("AdminUserIdUpdated"));
            this.adminUserLastReviewed = (AdminUser)this.Registry.CreateInstance(typeof(AdminUser), dr.GetGuid("AdminUserIdLastReviewed"));
            this.articleType = (ArticleType)dr.GetInt32("ArticleType");
            this.title = dr.GetString("Title");
            this.summary = dr.GetString("Summary");
            this.body = dr.GetString("Body");
            this.keywords = dr.GetString("Keywords");
            this.date = dr.GetDateTime("Date");
            this.author = dr.GetString("Author");
            this.startDate = dr.GetDateTime("StartDate");
            this.endDate = dr.GetNullableDateTime("EndDate");
            this.sortOrder = dr.GetInt32("SortOrder");
            this.videoUrl = dr.GetString("VideoUrl");
            this.videoThumbnailUrl = dr.GetString("VideoThumbnailUrl");
            this.videoDurationSeconds = dr.GetInt32("VideoDurationSeconds");
            this.videoWidth = dr.GetInt32("VideoWidth");
            this.videoHeight = dr.GetInt32("VideoHeight");
            this.isActive = dr.GetBoolean("IsActive");
            this.isCarMD = dr.GetBoolean("IsCarMD");
            this.isCanOBD2 = dr.GetBoolean("IsCanOBD2");
            this.isOBDFix = dr.GetBoolean("IsOBDFix");
            this.isFree = dr.GetBoolean("IsFree");
            this.lastReviewedDateTimeUTC = dr.GetDateTime("LastReviewedDateTimeUTC");
            this.createdDateTimeUTC = dr.GetDateTime("CreatedDateTimeUTC");
            this.updatedDateTimeUTC = dr.GetDateTime("UpdatedDateTimeUTC");

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
                        dr.ProcedureName = "Article_Create";
                        this.CreatedDateTimeUTC = DateTime.UtcNow;
                        this.UpdatedDateTimeUTC = this.CreatedDateTimeUTC;
                    }
                    else
                    {
                        dr.ProcedureName = "Article_Save";
                    }

                    dr.AddGuid("ArticleId", this.Id);
                    dr.AddGuid("ArticleCategoryIdPrimary", this.ArticleCategoryPrimary.Id);

                    if (this.IsActive && String.IsNullOrEmpty(this.URLName))
                    {
                        this.URLName = Article.GetSearchEngineURLName(this.Title);
                    }

                    dr.AddNVarChar("URLName", this.URLName);
                    dr.AddGuid("AdminUserIdCreated", this.AdminUserCreated.Id);
                    dr.AddGuid("AdminUserIdUpdated", this.AdminUserUpdated.Id);
                    dr.AddGuid("AdminUserIdLastReviewed", this.AdminUserCreated.Id);
                    dr.AddInt32("ArticleType", (int)this.ArticleType);
                    dr.AddNVarChar("Title", this.Title);
                    dr.AddNVarChar("Summary", this.Summary);
                    dr.AddNText("Body", this.Body);
                    dr.AddNVarChar("Keywords", this.Keywords);
                    dr.AddDateTime("Date", this.Date);
                    dr.AddNVarChar("Author", this.Author);
                    dr.AddDateTime("StartDate", this.StartDate);
                    if (!this.EndDate.IsNull)
                    {
                        dr.AddDateTime("EndDate", this.EndDate);
                    }
                    dr.AddInt32("SortOrder", this.SortOrder);
                    dr.AddNVarChar("VideoUrl", this.VideoUrl);
                    dr.AddNVarChar("VideoThumbnailUrl", this.VideoThumbnailUrl);
                    dr.AddInt32("VideoDurationSeconds", this.VideoDurationSeconds);
                    dr.AddInt32("VideoWidth", this.VideoWidth);
                    dr.AddInt32("VideoHeight", this.VideoHeight);
                    dr.AddBoolean("IsActive", this.IsActive);
                    dr.AddBoolean("IsCarMD", this.IsCarMD);
                    dr.AddBoolean("IsCanOBD2", this.IsCanOBD2);
                    dr.AddBoolean("IsOBDFix", this.IsOBDFix);
                    dr.AddBoolean("IsFree", this.IsFree);
                    dr.AddDateTime("LastReviewedDateTimeUTC", this.LastReviewedDateTimeUTC);
                    dr.AddDateTime("CreatedDateTimeUTC", this.CreatedDateTimeUTC);
                    dr.AddDateTime("UpdatedDateTimeUTC", this.UpdatedDateTimeUTC);

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
            // save the others object join relationship
            if (this.articleCategories != null && ArticleCategories.IsCollectionDirty)
            {
                transaction = EnsureDatabasePrepared(connection, transaction);

                //for id/joinId (2 column table) relationships
                SqlProcedureCommand deleteArticlesCategoryAssignments = new SqlProcedureCommand();
                deleteArticlesCategoryAssignments.ProcedureName = "ArticleCategoryAssignment_DeleteByArticle";
                deleteArticlesCategoryAssignments.AddGuid("ArticleId", Id);

                // create procedure call to add this Id then call the collection save
                // method, which adds the Id for itself.
                SqlProcedureCommand saveArticlesCategoryAssignments = new SqlProcedureCommand();

                saveArticlesCategoryAssignments.ProcedureName = "ArticleCategoryAssignment_Create";
                saveArticlesCategoryAssignments.AddGuid("ArticleId", Id);

                // perform the save on the related object
                transaction = this.ArticleCategories.Save(connection, transaction, deleteArticlesCategoryAssignments, saveArticlesCategoryAssignments, "ArticleCategoryId");
            }

            return transaction;
        }

        #endregion Required Object Methods (Load, Save, Delete, Etc)
    }
}
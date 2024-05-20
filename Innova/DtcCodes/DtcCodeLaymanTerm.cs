using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using System;
using System.Data.SqlClient;

namespace Innova.DtcCodes
{
    /// <summary>
    /// The DtcCodeLaymanTerm object handles the business logic and data access for the specialized business object, DtcCodeLaymanTerm.
    /// The class inherits from <see cref="BusinessObjectBase"/> class.
    /// </summary>
    /// <example>
    /// See the examples of how to construct the DtcCodeLaymanTerm object.
    ///
    /// To create a new instance of a new of DtcCodeLaymanTerm.
    /// <code>DtcCodeLaymanTerm o = (DtcCodeLaymanTerm)Registry.CreateInstance(typeof(DtcCodeLaymanTerm));</code>
    ///
    /// To create an new instance of an existing DtcCodeLaymanTerm.
    /// <code>DtcCodeLaymanTerm o = (DtcCodeLaymanTerm)Registry.CreateInstance(typeof(DtcCodeLaymanTerm), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of DtcCodeLaymanTerm, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectBase.ConnectionString"/> to the <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable()]
    public class DtcCodeLaymanTerm : InnovaBusinessObjectBase
    {
        // data object variables
        private string errorCode = "";

        private string make = "";
        private string title = "";
        private string title_es = "";
        private string title_fr = "";
        private string title_zh = "";
        private string description = "";
        private string description_es = "";
        private string description_fr = "";
        private string description_zh = "";
        private int severityLevel;
        private string effectOnVehicle = "";
        private string effectOnVehicle_es = "";
        private string effectOnVehicle_fr = "";
        private string effectOnVehicle_zh = "";
        private string responsibleComponentOrSystem = "";
        private string responsibleComponentOrSystem_es = "";
        private string responsibleComponentOrSystem_fr = "";
        private string responsibleComponentOrSystem_zh = "";
        private string whyItsImportant = "";
        private string whyItsImportant_es = "";
        private string whyItsImportant_fr = "";
        private string whyItsImportant_zh = "";

        private string severityDefinition = "";
        private string severityDefinition_es = "";
        private string severityDefinition_fr = "";
        private string severityDefinition_zh = "";

        private string longDescription = "";
        private string longDescription_es = "";
        private string longDescription_fr = "";
        private string longDescription_zh = "";

        #region System Constructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new (one that does not exist, assigning a new <see cref="Guid"/> Id). DtcCodeLaymanTerm object.
        /// In order to create a new DtcCodeLaymanTerm which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <example>
        /// <code>
        /// DtcCodeLaymanTerm o = (DtcCodeLaymanTerm)Registry.CreateInstance(typeof(DtcCodeLaymanTerm));
        /// </code>
        /// </example>
        protected internal DtcCodeLaymanTerm() : base()
        {
            IsObjectCreated = true;
            IsObjectLoaded = true;
            IsObjectDirty = true;
        }

        /// <summary>
        /// Constructor creates an existing (one that exists, and already has a <see cref="Guid"/> Id).  DtcCodeLaymanTerm object.
        /// In order to create an existing DtcCodeLaymanTerm object, which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <param name="id">The <see cref="Guid"/> or SQL uniqueidentifier indentification "Id" of the current object.</param>
        /// <example>
        /// <code>
        /// DtcCodeLaymanTerm o = (DtcCodeLaymanTerm)Registry.CreateInstance(typeof(DtcCodeLaymanTerm), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));
        /// </code>
        /// </example>
        protected internal DtcCodeLaymanTerm(Guid id) : base(id)
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
        /// Gets or sets the <see cref="string"/> error code
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
        /// Gets or sets the <see cref="string"/> vehicle make for this DTC
        /// </summary>
        public string Make
        {
            get
            {
                this.EnsureLoaded();
                return this.make;
            }
            set
            {
                this.EnsureLoaded();
                if (this.make != value)
                {
                    this.IsObjectDirty = true;
                    this.make = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> title of the DTC code.
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
        /// Gets or sets the <see cref="string"/> title of the DTC code in Spanish.
        /// </summary>
        public string Title_es
        {
            get
            {
                this.EnsureLoaded();
                return this.title_es;
            }
            set
            {
                this.EnsureLoaded();
                if (this.title_es != value)
                {
                    this.IsObjectDirty = true;
                    this.title_es = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> title of the DTC code french.
        /// </summary>
        public string Title_fr
        {
            get
            {
                this.EnsureLoaded();
                return this.title_fr;
            }
            set
            {
                this.EnsureLoaded();
                if (this.title_fr != value)
                {
                    this.IsObjectDirty = true;
                    this.title_fr = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> title of the DTC code Mandarin chinese.
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
        /// Gets or sets the <see cref="string"/> title in the language specified in the <see cref="RuntimeInfo"/>.
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
        /// Gets or sets the <see cref="string"/> description of the DTC code
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
        /// Gets or sets the <see cref="string"/> description of the DTC code in Spanish
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
        /// Gets or sets the <see cref="string"/> description of the DTC code in French
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
        /// Gets or sets the <see cref="string"/> description of the DTC code in Mandarin Chinese
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
        /// Gets or sets the <see cref="string"/> description in the language specified in the <see cref="RuntimeInfo"/>.
        /// </summary>
        public string Description_Translated
        {
            get
            {
                this.EnsureLoaded();
                return this.RuntimeInfo.GetTranslatedValue(this.Description, this.Description_es, this.Description_fr, this.Description_zh);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/> severity level.
        /// </summary>
        public int SeverityLevel
        {
            get
            {
                this.EnsureLoaded();
                return this.severityLevel;
            }
            set
            {
                this.EnsureLoaded();
                if (this.severityLevel != value)
                {
                    this.IsObjectDirty = true;
                    this.severityLevel = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> effect on the vehicle.
        /// </summary>
        public string EffectOnVehicle
        {
            get
            {
                this.EnsureLoaded();
                return this.effectOnVehicle;
            }
            set
            {
                this.EnsureLoaded();
                if (this.effectOnVehicle != value)
                {
                    this.IsObjectDirty = true;
                    this.effectOnVehicle = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> effect on the vehicle in Spanish.
        /// </summary>
        public string EffectOnVehicle_es
        {
            get
            {
                this.EnsureLoaded();
                return this.effectOnVehicle_es;
            }
            set
            {
                this.EnsureLoaded();
                if (this.effectOnVehicle_es != value)
                {
                    this.IsObjectDirty = true;
                    this.effectOnVehicle_es = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> effect on the vehicle in French.
        /// </summary>
        public string EffectOnVehicle_fr
        {
            get
            {
                this.EnsureLoaded();
                return this.effectOnVehicle_fr;
            }
            set
            {
                this.EnsureLoaded();
                if (this.effectOnVehicle_fr != value)
                {
                    this.IsObjectDirty = true;
                    this.effectOnVehicle_fr = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> effect on the vehicle in Chinese.
        /// </summary>
        public string EffectOnVehicle_zh
        {
            get
            {
                this.EnsureLoaded();
                return this.effectOnVehicle_zh;
            }
            set
            {
                this.EnsureLoaded();
                if (this.effectOnVehicle_zh != value)
                {
                    this.IsObjectDirty = true;
                    this.effectOnVehicle_zh = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> effect on the vehicle in the language specified in the <see cref="RuntimeInfo"/>.
        /// </summary>
        public string EffectOnVehicle_Translated
        {
            get
            {
                this.EnsureLoaded();
                return this.RuntimeInfo.GetTranslatedValue(this.EffectOnVehicle, this.EffectOnVehicle_es, this.EffectOnVehicle_fr, this.EffectOnVehicle_zh);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> responsible component or system.
        /// </summary>
        public string ResponsibleComponentOrSystem
        {
            get
            {
                this.EnsureLoaded();
                return this.responsibleComponentOrSystem;
            }
            set
            {
                this.EnsureLoaded();
                if (this.responsibleComponentOrSystem != value)
                {
                    this.IsObjectDirty = true;
                    this.responsibleComponentOrSystem = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> responsible component or system in Spanish.
        /// </summary>
        public string ResponsibleComponentOrSystem_es
        {
            get
            {
                this.EnsureLoaded();
                return this.responsibleComponentOrSystem_es;
            }
            set
            {
                this.EnsureLoaded();
                if (this.responsibleComponentOrSystem_es != value)
                {
                    this.IsObjectDirty = true;
                    this.responsibleComponentOrSystem_es = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> responsible component or system in French.
        /// </summary>
        public string ResponsibleComponentOrSystem_fr
        {
            get
            {
                this.EnsureLoaded();
                return this.responsibleComponentOrSystem_fr;
            }
            set
            {
                this.EnsureLoaded();
                if (this.responsibleComponentOrSystem_fr != value)
                {
                    this.IsObjectDirty = true;
                    this.responsibleComponentOrSystem_fr = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> responsible component or system in Chinese.
        /// </summary>
        public string ResponsibleComponentOrSystem_zh
        {
            get
            {
                this.EnsureLoaded();
                return this.responsibleComponentOrSystem_zh;
            }
            set
            {
                this.EnsureLoaded();
                if (this.responsibleComponentOrSystem_zh != value)
                {
                    this.IsObjectDirty = true;
                    this.responsibleComponentOrSystem_zh = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> responsible component or system in the language specified in the <see cref="RuntimeInfo"/>.
        /// </summary>
        public string ResponsibleComponentOrSystem_Translated
        {
            get
            {
                this.EnsureLoaded();
                return this.RuntimeInfo.GetTranslatedValue(this.ResponsibleComponentOrSystem, this.ResponsibleComponentOrSystem_es, this.ResponsibleComponentOrSystem_fr, this.ResponsibleComponentOrSystem_zh);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> describing why the component or system is important.
        /// </summary>
        public string WhyItsImportant
        {
            get
            {
                this.EnsureLoaded();
                return this.whyItsImportant;
            }
            set
            {
                this.EnsureLoaded();
                if (this.whyItsImportant != value)
                {
                    this.IsObjectDirty = true;
                    this.whyItsImportant = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> describing why the component or system is important in Spanish.
        /// </summary>
        public string WhyItsImportant_es
        {
            get
            {
                this.EnsureLoaded();
                return this.whyItsImportant_es;
            }
            set
            {
                this.EnsureLoaded();
                if (this.whyItsImportant_es != value)
                {
                    this.IsObjectDirty = true;
                    this.whyItsImportant_es = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> describing why the component or system is important in French.
        /// </summary>
        public string WhyItsImportant_fr
        {
            get
            {
                this.EnsureLoaded();
                return this.whyItsImportant_fr;
            }
            set
            {
                this.EnsureLoaded();
                if (this.whyItsImportant_fr != value)
                {
                    this.IsObjectDirty = true;
                    this.whyItsImportant_fr = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> describing why the component or system is important in Chinese.
        /// </summary>
        public string WhyItsImportant_zh
        {
            get
            {
                this.EnsureLoaded();
                return this.whyItsImportant_zh;
            }
            set
            {
                this.EnsureLoaded();
                if (this.whyItsImportant_zh != value)
                {
                    this.IsObjectDirty = true;
                    this.whyItsImportant_zh = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> describing why the component or system is important in the language specified in the <see cref="RuntimeInfo"/>.
        /// </summary>
        public string WhyItsImportant_Translated
        {
            get
            {
                this.EnsureLoaded();
                return this.RuntimeInfo.GetTranslatedValue(this.WhyItsImportant, this.WhyItsImportant_es, this.WhyItsImportant_fr, this.WhyItsImportant_zh);
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> description of the severity level in English.
        /// </summary>
        public string SeverityDefinition
        {
            get
            {
                this.EnsureLoaded();
                return this.severityDefinition;
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> description of the severity level in Spanish.
        /// </summary>
        public string SeverityDefinition_es
        {
            get
            {
                this.EnsureLoaded();
                return this.severityDefinition_es;
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> description of the severity level in French.
        /// </summary>
        public string SeverityDefinition_fr
        {
            get
            {
                this.EnsureLoaded();
                return this.severityDefinition_fr;
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> description of the severity level in Chinese.
        /// </summary>
        public string SeverityDefinition_zh
        {
            get
            {
                this.EnsureLoaded();
                return this.severityDefinition_zh;
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> description of the severity level in the language specified in the <see cref="RuntimeInfo"/>.
        /// </summary>
        public string SeverityDefinition_Translated
        {
            get
            {
                this.EnsureLoaded();
                return this.RuntimeInfo.GetTranslatedValue(this.SeverityDefinition, this.SeverityDefinition_es, this.SeverityDefinition_fr, this.SeverityDefinition_zh);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> long description of the DTC code
        /// </summary>
        public string LongDescription
        {
            get
            {
                this.EnsureLoaded();
                return this.longDescription;
            }
            set
            {
                this.EnsureLoaded();
                if (this.longDescription != value)
                {
                    this.IsObjectDirty = true;
                    this.longDescription = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> long description of the DTC code in Spanish
        /// </summary>
        public string LongDescription_es
        {
            get
            {
                this.EnsureLoaded();
                return this.longDescription_es;
            }
            set
            {
                this.EnsureLoaded();
                if (this.longDescription_es != value)
                {
                    this.IsObjectDirty = true;
                    this.longDescription_es = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> long description of the DTC code in French
        /// </summary>
        public string LongDescription_fr
        {
            get
            {
                this.EnsureLoaded();
                return this.longDescription_fr;
            }
            set
            {
                this.EnsureLoaded();
                if (this.longDescription_fr != value)
                {
                    this.IsObjectDirty = true;
                    this.longDescription_fr = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> long description of the DTC code in Mandarin Chinese
        /// </summary>
        public string LongDescription_zh
        {
            get
            {
                this.EnsureLoaded();
                return this.longDescription_zh;
            }
            set
            {
                this.EnsureLoaded();
                if (this.longDescription_zh != value)
                {
                    this.IsObjectDirty = true;
                    this.longDescription_zh = value;
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
                //load the base dtcCode if user selected it.
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
            dr.ProcedureName = "DtcCodeLaymanTerm_Load";
            dr.AddGuid("DtcCodeLaymanTermId", Id);
        }

        /// <summary>
        /// Sets the properties of this object from the fields in the recordset located in the supplied <see cref="SqlDataReaderWrapper"/>.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> containing the recordset and fields used to set the properties of this object (this layer only).</param>
        protected new void SetPropertiesFromDataReader(SqlDataReaderWrapper dr)
        {
            this.errorCode = dr.GetString("ErrorCode");
            this.make = dr.GetString("Make");
            this.title = dr.GetString("Title");
            this.title_es = dr.GetString("Title_es");
            this.title_fr = dr.GetString("Title_fr");
            this.title_zh = dr.GetString("Title_zh");
            this.description = dr.GetString("Description");
            this.description_es = dr.GetString("Description_es");
            this.description_fr = dr.GetString("Description_fr");
            this.description_zh = dr.GetString("Description_zh");
            this.severityLevel = dr.GetInt32("SeverityLevel");
            this.effectOnVehicle = dr.GetString("EffectOnVehicle");
            this.effectOnVehicle_es = dr.GetString("EffectOnVehicle_es");
            this.effectOnVehicle_fr = dr.GetString("EffectOnVehicle_fr");
            this.effectOnVehicle_zh = dr.GetString("EffectOnVehicle_zh");
            this.responsibleComponentOrSystem = dr.GetString("ResponsibleComponentOrSystem");
            this.responsibleComponentOrSystem_es = dr.GetString("ResponsibleComponentOrSystem_es");
            this.responsibleComponentOrSystem_fr = dr.GetString("ResponsibleComponentOrSystem_fr");
            this.responsibleComponentOrSystem_zh = dr.GetString("ResponsibleComponentOrSystem_zh");
            this.whyItsImportant = dr.GetString("WhyItsImportant");
            this.whyItsImportant_es = dr.GetString("WhyItsImportant_es");
            this.whyItsImportant_fr = dr.GetString("WhyItsImportant_fr");
            this.whyItsImportant_zh = dr.GetString("WhyItsImportant_zh");

            this.severityDefinition = dr.GetString("SeverityDefinition");
            this.severityDefinition_es = dr.GetString("SeverityDefinition_es");
            this.severityDefinition_fr = dr.GetString("SeverityDefinition_fr");
            this.severityDefinition_zh = dr.GetString("SeverityDefinition_zh");

            this.longDescription = dr.GetString("LongDescription");
            this.longDescription_es = dr.GetString("LongDescription_es");
            this.longDescription_fr = dr.GetString("LongDescription_fr");
            this.longDescription_zh = dr.GetString("LongDescription_zh");

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
                        dr.ProcedureName = "DtcCodeLaymanTerm_Create";
                    }
                    else
                    {
                        dr.ProcedureName = "DtcCodeLaymanTerm_Save";
                    }

                    dr.AddGuid("DtcCodeLaymanTermId", this.Id);
                    dr.AddNVarChar("ErrorCode", this.ErrorCode);
                    dr.AddNVarChar("Make", this.Make);
                    dr.AddNVarChar("Title", this.Title);
                    dr.AddNVarChar("Title_es", this.Title_es);
                    dr.AddNVarChar("Title_fr", this.Title_fr);
                    dr.AddNVarChar("Title_zh", this.Title_zh);
                    dr.AddNVarChar("Description", this.Description);
                    dr.AddNVarChar("Description_es", this.Description_es);
                    dr.AddNVarChar("Description_fr", this.Description_fr);
                    dr.AddNVarChar("Description_zh", this.Description_zh);
                    dr.AddInt32("SeverityLevel", this.SeverityLevel);
                    dr.AddNVarChar("EffectOnVehicle", this.EffectOnVehicle);
                    dr.AddNVarChar("EffectOnVehicle_es", this.EffectOnVehicle_es);
                    dr.AddNVarChar("EffectOnVehicle_fr", this.EffectOnVehicle_fr);
                    dr.AddNVarChar("EffectOnVehicle_zh", this.EffectOnVehicle_zh);
                    dr.AddNVarChar("ResponsibleComponentOrSystem", this.ResponsibleComponentOrSystem);
                    dr.AddNVarChar("ResponsibleComponentOrSystem_es", this.ResponsibleComponentOrSystem_es);
                    dr.AddNVarChar("ResponsibleComponentOrSystem_fr", this.ResponsibleComponentOrSystem_fr);
                    dr.AddNVarChar("ResponsibleComponentOrSystem_zh", this.ResponsibleComponentOrSystem_zh);
                    dr.AddNVarChar("WhyItsImportant", this.WhyItsImportant);
                    dr.AddNVarChar("WhyItsImportant_es", this.WhyItsImportant_es);
                    dr.AddNVarChar("WhyItsImportant_fr", this.WhyItsImportant_fr);
                    dr.AddNVarChar("WhyItsImportant_zh", this.WhyItsImportant_zh);

                    dr.AddNVarChar("LongDescription", this.LongDescription);
                    dr.AddNVarChar("LongDescription_es", this.LongDescription_es);
                    dr.AddNVarChar("LongDescription_fr", this.LongDescription_fr);
                    dr.AddNVarChar("LongDescription_zh", this.LongDescription_zh);

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

            // delete this current object
            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                //delete the dtcCode
                dr.ProcedureName = "DtcCodeLaymanTerm_Delete";
                dr.AddGuid("DtcCodeLaymanTermId", this.Id);

                dr.ExecuteNonQuery(transaction);
            }

            transaction = base.Delete(connection, transaction);

            return transaction;
        }

        #endregion Required Object Methods (Load, Save, Delete, Etc)
    }
}
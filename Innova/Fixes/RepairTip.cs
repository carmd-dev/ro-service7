using Innova.Markets;
using Innova.Users;
using Metafuse3;
using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using Metafuse3.NullableTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;

namespace Innova.Fixes
{
    /// <summary>
    ///
    /// </summary>
    [Serializable()]
    [ClassDefinition("Repair Tip", "Repair Tip", "RepairTip", "RepairTipId")]
    public class RepairTip : InnovaBusinessObjectBase
    {
        private string description;
        private string description_es = "";
        private string description_fr = "";
        private string description_zh = "";

        private string possibleCause;
        private string possibleCause_es = "";
        private string possibleCause_fr = "";
        private string possibleCause_zh = "";

        private string diagnosticProcedure;
        private string diagnosticProcedure_es = "";
        private string diagnosticProcedure_fr = "";
        private string diagnosticProcedure_zh = "";

        private string repairValidation;
        private string repairValidation_es = "";
        private string repairValidation_fr = "";
        private string repairValidation_zh = "";

        private bool isApproved;

        private AdminUser createdByAdminUser;
        private AdminUser updatedByAdminUser;
        private AdminUser approvedByAdminUser;
        private DateTime createdDateTimeUTC;
        private DateTime updatedDateTimeUTC;
        private NullableDateTime approvedDateTimeUTC;

        private FixNameCollection fixNameCollection;

        private string marketsString = "";
        private bool isMarketsDirty = false;

        private bool hasEngineTypeDefined = false;
        private string engineTypesString = "";
        private bool isEngineTypesDirty = false;

        private bool hasYearDefined = false;
        private string yearsString = "";
        private bool isYearsDirty = false;

        private bool hasMakeDefined = false;
        private string makesString = "";
        private bool isMakesDirty = false;

        private bool hasModelDefined = false;
        private string modelsString = "";
        private bool isModelsDirty = false;

        private List<Market> markets;
        private List<string> engineTypes;
        private List<string> makes;
        private List<string> models;
        private List<int> years;
        private List<string> dtcs;
        private string errorCodesString = "";
        private bool isErrorCodesDirty = false;

        #region IA-236

        public string Status = "";
        public string Notes = "";
        public string FileNames = "";
        public string FamilyId = "";

        #endregion IA-236

        #region System Constructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new (one that does not exist, assigning a new <see cref="Guid"/> Id). RepairTip object.
        /// In order to create a new RepairTip which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <example>
        /// <code>
        /// RepairTip o = (RepairTip)Registry.CreateInstance(typeof(RepairTip));
        /// </code>
        /// </example>
        protected internal RepairTip() : base()
        {
            IsObjectCreated = true;
            IsObjectLoaded = true;
            IsObjectDirty = true;

            this.CreatedDateTimeUTC = DateTime.UtcNow;
            this.UpdatedDateTimeUTC = this.CreatedDateTimeUTC;
        }

        /// <summary>
        /// Constructor creates an existing (one that exists, and already has a <see cref="Guid"/> Id).  FixName object.
        /// In order to create an existing FixName object, which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <param name="id">The <see cref="Guid"/> or SQL uniqueidentifier indentification "Id" of the current object.</param>
        /// <example>
        /// <code>
        /// FixName o = (FixName)Registry.CreateInstance(typeof(FixName), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));
        /// </code>
        /// </example>
        protected internal RepairTip(Guid id) : base(id)
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
        /// Gets the <see cref="bool"/> flag indicating whether or not the engine type is defined.
        /// </summary>
        [PropertyDefinition("Engine Type Defined", "Indicates that engine type was specified.")]
        public bool HasEngineTypeDefined
        {
            get
            {
                EnsureLoaded();
                return hasEngineTypeDefined;
            }
        }

        /// <summary>
        /// Gets the <see cref="bool"/> flag indicating whether or not the model is defined.
        /// </summary>
        [PropertyDefinition("Model Defined", "Indicates that the model is specified.")]
        public bool HasModelDefined
        {
            get
            {
                EnsureLoaded();
                return hasModelDefined;
            }
        }

        /// <summary>
        /// Gets the <see cref="bool"/> flag indicating whether or not the year is defined.
        /// </summary>
        [PropertyDefinition("Year Defined", "Indicates if year is specified.")]
        public bool HasYearDefined
        {
            get
            {
                EnsureLoaded();
                return hasYearDefined;
            }
        }

        /// <summary>
        /// Gets the <see cref="bool"/> flag indicating whether or not the make is defined.
        /// </summary>
        [PropertyDefinition("Make Defined", "Indicates that the make is specified.")]
        public bool HasMakeDefined
        {
            get
            {
                EnsureLoaded();
                return hasMakeDefined;
            }
        }

        /// <summary>
        /// Gets the <see cref="List{T}"/> of vehicle engine types that apply to this fix.
        /// NOTE: DO NOT add to this collection directly. Use the AddEngineType() method.
        /// </summary>
        [PropertyDefinition("Engine Types", "Engine Types that apply to this fix.")]
        public List<string> EngineTypes
        {
            get
            {
                if (this.engineTypes == null)
                {
                    this.EnsureLoaded();

                    this.engineTypes = new List<string>();

                    if (!this.isObjectCreated && this.engineTypesString != "")
                    {
                        foreach (string s in this.engineTypesString.Split("|".ToCharArray()))
                        {
                            if (!string.IsNullOrEmpty(s))
                            {
                                this.engineTypes.Add(s.ToUpper());
                            }
                        }
                    }
                }
                return this.engineTypes;
            }
        }

        /// <summary>
        /// Gets or sets the list of <see cref="Market"/> that this fix applies to.
        /// NOTE: DO NOT add to this collection directly. Use the AddMarket() method.
        /// </summary>
        [PropertyDefinition("Markets", "Markets that this fix covers.")]
        public List<Market> Markets
        {
            get
            {
                if (this.markets == null)
                {
                    this.EnsureLoaded();

                    this.markets = new List<Innova.Markets.Market>();

                    if (!isObjectCreated && this.marketsString != "")
                    {
                        foreach (string s in this.marketsString.Split("|".ToCharArray()))
                        {
                            if (!string.IsNullOrEmpty(s))
                            {
                                markets.Add((Market)int.Parse(s));
                            }
                        }
                    }
                }
                return markets;
            }
        }

        /// <summary>
        /// Get the <see cref="List{T}"/> of vehicle makes that apply to this fix.
        /// NOTE: DO NOT add to this collection directly. Use the AddMake() method.
        /// </summary>
        [PropertyDefinition("Makes", "Makes of vehicles that apply to this fix.")]
        public List<string> Makes
        {
            get
            {
                if (this.makes == null)
                {
                    this.EnsureLoaded();

                    this.makes = new List<string>();

                    //load if not a user created element

                    if (!this.isObjectCreated && this.makesString != "")
                    {
                        foreach (string s in this.makesString.Split("|".ToCharArray()))
                        {
                            if (!string.IsNullOrEmpty(s))
                            {
                                this.makes.Add(s.ToUpper());
                            }
                        }
                    }
                }

                return this.makes;
            }
        }

        /// <summary>
        /// Get the <see cref="List{T}"/> of vehicle models that apply to this fix.
        /// NOTE: DO NOT add to this collection directly. Use the AddModel() method.
        /// </summary>
        [PropertyDefinition("Models", "Models of vehicles that apply to this fix.")]
        public List<string> Models
        {
            get
            {
                if (this.models == null)
                {
                    this.EnsureLoaded();

                    this.models = new List<string>();

                    if (!this.isObjectCreated && this.modelsString != "")
                    {
                        foreach (string s in this.modelsString.Split("|".ToCharArray()))
                        {
                            if (!string.IsNullOrEmpty(s))
                            {
                                //this.models.Add(s.ToUpper()); //Added on 2018-12-10 2:50 PM by INNOVA Dev Team
                                this.models.Add(s.ToUpper());
                            }
                        }
                    }
                }

                return this.models;
            }
        }

        /// <summary>
        /// Get an <see cref="List{T}"/> of <see cref="int"/> vehicle years that apply to this fix.
        /// NOTE: DO NOT add to this collection directly. Use the AddYear() method.
        /// </summary>
        [PropertyDefinition("Years", "Vehicle Years that apply to this fix.")]
        public List<int> Years
        {
            get
            {
                if (this.years == null)
                {
                    this.EnsureLoaded();

                    this.years = new List<int>();

                    if (!this.isObjectCreated && this.yearsString != "")
                    {
                        foreach (string s in this.yearsString.Split("|".ToCharArray()))
                        {
                            if (!string.IsNullOrEmpty(s))
                            {
                                this.years.Add(int.Parse(s));
                            }
                        }
                    }
                }

                return this.years;
            }
        }

        /// <summary>
        /// Get an <see cref="List{T}"/> of vehicle years that apply to this fix.
        /// NOTE: DO NOT add to this collection directly. Use the AddYear() method.
        /// </summary>
        public List<string> YearsAsStrings
        {
            get
            {
                List<string> yearsAsStrings = new List<string>();

                foreach (int y in this.Years)
                {
                    yearsAsStrings.Add(y.ToString());
                }

                return yearsAsStrings;
            }
        }

        #region Description

        /// <summary>
        /// Gets or sets the <see cref="string"/> description of the fix name (or the actual name).
        /// </summary>
        [PropertyDefinition("Initial Inspection", "Initial Inspection of the repair tip")]
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
                    this.UpdatedField("Description");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> description in Spanish
        /// </summary>
        [PropertyDefinition("Description - Spanish", "Description of the repair tip in Spanish")]
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
                    this.UpdatedField("Description_es");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> description in French
        /// </summary>
        [PropertyDefinition("Description - French", "Description of the repair tip in French")]
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
                    this.UpdatedField("Description_fr");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> description in Mandarin
        /// </summary>
        [PropertyDefinition("Description - Mandarin", "Description of the repair tip in Mandarin")]
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
                    this.UpdatedField("description_zh");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> description in the language specified in the <see cref="RuntimeInfo"/>.
        /// </summary>
        [PropertyDefinition("Description", "Description of the repair tip")]
        public string Description_Translated
        {
            get
            {
                this.EnsureLoaded();
                return this.RuntimeInfo.GetTranslatedValue(this.Description, this.Description_es, this.Description_fr, this.Description_zh);
            }
        }

        #endregion Description

        #region Possible Cause

        /// <summary>
        /// Gets or sets the <see cref="string"/> description of the fix name (or the actual name).
        /// </summary>
        [PropertyDefinition(" PossibleCause", " PossibleCause of the repair tip")]
        public string PossibleCause
        {
            get
            {
                this.EnsureLoaded();
                return this.possibleCause;
            }
            set
            {
                this.EnsureLoaded();
                if (this.possibleCause != value)
                {
                    this.IsObjectDirty = true;
                    this.possibleCause = value;
                    this.UpdatedField("PossibleCause");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> description in Spanish
        /// </summary>
        [PropertyDefinition(" PossibleCause - Spanish", " PossibleCause of the repair tip in Spanish")]
        public string PossibleCause_es
        {
            get
            {
                this.EnsureLoaded();
                return this.possibleCause_es;
            }
            set
            {
                this.EnsureLoaded();
                if (this.possibleCause_es != value)
                {
                    this.IsObjectDirty = true;
                    this.possibleCause_es = value;
                    this.UpdatedField("PossibleCause_es");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> description in French
        /// </summary>
        [PropertyDefinition(" PossibleCause - French", " PossibleCause of the repair tip in French")]
        public string PossibleCause_fr
        {
            get
            {
                this.EnsureLoaded();
                return this.possibleCause_fr;
            }
            set
            {
                this.EnsureLoaded();
                if (this.possibleCause_fr != value)
                {
                    this.IsObjectDirty = true;
                    this.possibleCause_fr = value;
                    this.UpdatedField("PossibleCause_fr");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> description in Mandarin
        /// </summary>
        [PropertyDefinition(" PossibleCause - Mandarin", " PossibleCause of the repair tip in Mandarin")]
        public string PossibleCause_zh
        {
            get
            {
                this.EnsureLoaded();
                return this.possibleCause_zh;
            }
            set
            {
                this.EnsureLoaded();
                if (this.possibleCause_zh != value)
                {
                    this.IsObjectDirty = true;
                    this.possibleCause_zh = value;
                    this.UpdatedField("PossibleCause_zh");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> description in the language specified in the <see cref="RuntimeInfo"/>.
        /// </summary>
        [PropertyDefinition(" Possible Cause", "Description of the repair tip")]
        public string PossibleCause_Translated
        {
            get
            {
                this.EnsureLoaded();
                return this.RuntimeInfo.GetTranslatedValue(this.PossibleCause, this.PossibleCause_es, this.PossibleCause_fr, this.PossibleCause_zh);
            }
        }

        #endregion Possible Cause

        #region Diagnostic Procedure

        /// <summary>
        /// Gets or sets the <see cref="string"/> description of the fix name (or the actual name).
        /// </summary>
        [PropertyDefinition(" Diagnostic Procedure", " Diagnostic Procedure of the repair tip")]
        public string DiagnosticProcedure
        {
            get
            {
                this.EnsureLoaded();
                return this.diagnosticProcedure;
            }
            set
            {
                this.EnsureLoaded();
                if (this.diagnosticProcedure != value)
                {
                    this.IsObjectDirty = true;
                    this.diagnosticProcedure = value;
                    this.UpdatedField("DiagnosticProcedure");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> description in Spanish
        /// </summary>
        [PropertyDefinition(" Diagnostic Procedure - Spanish", " Diagnostic Procedure of the repair tip in Spanish")]
        public string DiagnosticProcedure_es
        {
            get
            {
                this.EnsureLoaded();
                return this.diagnosticProcedure_es;
            }
            set
            {
                this.EnsureLoaded();
                if (this.diagnosticProcedure_es != value)
                {
                    this.IsObjectDirty = true;
                    this.diagnosticProcedure_es = value;
                    this.UpdatedField("DiagnosticProcedure_es");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> description in French
        /// </summary>
        [PropertyDefinition(" Diagnostic Procedure - French", " Diagnostic Procedure of the repair tip in French")]
        public string DiagnosticProcedure_fr
        {
            get
            {
                this.EnsureLoaded();
                return this.diagnosticProcedure_fr;
            }
            set
            {
                this.EnsureLoaded();
                if (this.diagnosticProcedure_fr != value)
                {
                    this.IsObjectDirty = true;
                    this.diagnosticProcedure_fr = value;
                    this.UpdatedField("DiagnosticProcedure_fr");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> description in Mandarin
        /// </summary>
        [PropertyDefinition(" Diagnostic Procedure - Mandarin", " Diagnostic Procedure of the repair tip in Mandarin")]
        public string DiagnosticProcedure_zh
        {
            get
            {
                this.EnsureLoaded();
                return this.diagnosticProcedure_zh;
            }
            set
            {
                this.EnsureLoaded();
                if (this.diagnosticProcedure_zh != value)
                {
                    this.IsObjectDirty = true;
                    this.diagnosticProcedure_zh = value;
                    this.UpdatedField("DiagnosticProcedure_zh");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> description in the language specified in the <see cref="RuntimeInfo"/>.
        /// </summary>
        [PropertyDefinition(" Diagnostic Procedure", "Diagnostic Procedure of the repair tip")]
        public string DiagnosticProcedure_Translated
        {
            get
            {
                this.EnsureLoaded();
                return this.RuntimeInfo.GetTranslatedValue(this.DiagnosticProcedure, this.DiagnosticProcedure_es, this.DiagnosticProcedure_fr, this.DiagnosticProcedure_zh);
            }
        }

        #endregion Diagnostic Procedure

        #region Repair Validation

        /// <summary>
        /// Gets or sets the <see cref="string"/> Repair Validation of the repair tip (or the actual name).
        /// </summary>
        [PropertyDefinition(" Repair Validation", " Repair Validation of the repair tip")]
        public string RepairValidation
        {
            get
            {
                this.EnsureLoaded();
                return this.repairValidation;
            }
            set
            {
                this.EnsureLoaded();
                if (this.repairValidation != value)
                {
                    this.IsObjectDirty = true;
                    this.repairValidation = value;
                    this.UpdatedField("RepairValidation");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> description in Spanish
        /// </summary>
        [PropertyDefinition(" Repair Validation - Spanish", " Repair Validation of the repair tip in Spanish")]
        public string RepairValidation_es
        {
            get
            {
                this.EnsureLoaded();
                return this.repairValidation_es;
            }
            set
            {
                this.EnsureLoaded();
                if (this.repairValidation_es != value)
                {
                    this.IsObjectDirty = true;
                    this.repairValidation_es = value;
                    this.UpdatedField("RepairValidation_es");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> description in French
        /// </summary>
        [PropertyDefinition(" Repair Validation - French", " Repair Validation of the repair tip in French")]
        public string RepairValidation_fr
        {
            get
            {
                this.EnsureLoaded();
                return this.repairValidation_fr;
            }
            set
            {
                this.EnsureLoaded();
                if (this.repairValidation_fr != value)
                {
                    this.IsObjectDirty = true;
                    this.repairValidation_fr = value;
                    this.UpdatedField("RepairValidation_fr");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> description in Mandarin
        /// </summary>
        [PropertyDefinition(" Repair Validation - Mandarin", " Repair Validation of the repair tip in Mandarin")]
        public string RepairValidation_zh
        {
            get
            {
                this.EnsureLoaded();
                return this.repairValidation_zh;
            }
            set
            {
                this.EnsureLoaded();
                if (this.repairValidation_zh != value)
                {
                    this.IsObjectDirty = true;
                    this.repairValidation_zh = value;
                    this.UpdatedField("RepairValidation_zh");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> Repair Validation in the language specified in the <see cref="RuntimeInfo"/>.
        /// </summary>
        [PropertyDefinition("Repair Validation", "Description of the repair tip")]
        public string RepairValidation_Translated
        {
            get
            {
                this.EnsureLoaded();
                return this.RuntimeInfo.GetTranslatedValue(this.RepairValidation, this.RepairValidation_es, this.RepairValidation_fr, this.RepairValidation_zh);
            }
        }

        #endregion Repair Validation

        /// <summary>
        /// Gets or sets the <see cref="bool"/> flag indicating whether or not the RepairTip is active or not.
        /// </summary>
        [PropertyDefinition("Is Approved", "Is this RepairTip currently approved.")]
        public bool IsApproved
        {
            get
            {
                this.EnsureLoaded();
                return this.isApproved;
            }
            set
            {
                this.EnsureLoaded();
                if (this.isApproved != value)
                {
                    this.IsObjectDirty = true;
                    this.isApproved = value;
                    this.UpdatedField("IsApproved");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="AdminUser"/> who created the record.
        /// </summary>
        [PropertyDefinition("Created By", "Administrator who created Fix Name")]
        public AdminUser CreatedByAdminUser
        {
            get
            {
                EnsureLoaded();
                return createdByAdminUser;
            }
            set
            {
                EnsureLoaded();
                if (createdByAdminUser != value)
                {
                    IsObjectDirty = true;
                    createdByAdminUser = value;
                    this.UpdatedField("CreatedByAdminUserId");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="AdminUser"/> who last updated the record.
        /// </summary>
        [PropertyDefinition("Updated By", "Administrator who last updated this repair tip.")]
        public AdminUser UpdatedByAdminUser
        {
            get
            {
                EnsureLoaded();
                return updatedByAdminUser;
            }
            set
            {
                EnsureLoaded();
                if (updatedByAdminUser != value)
                {
                    IsObjectDirty = true;
                    updatedByAdminUser = value;
                    this.UpdatedField("UpdatedByAdminUserId");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="AdminUser"/> who last updated the record.
        /// </summary>
        [PropertyDefinition("Updated By", "Administrator who last updated this repair tip.")]
        public AdminUser ApprovedByAdminUser
        {
            get
            {
                EnsureLoaded();
                return approvedByAdminUser;
            }
            set
            {
                EnsureLoaded();
                if (approvedByAdminUser != value)
                {
                    IsObjectDirty = true;
                    approvedByAdminUser = value;
                    this.UpdatedField("ApprovedByAdminUserId");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/> the record was created on in universal time
        /// </summary>
        [PropertyDefinition("Created", "Date and Time this repair tip was created.")]
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
                    this.UpdatedField("CreatedDateTimeUTC");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/> the record was last updated on in universal time
        /// </summary>
        [PropertyDefinition("Updated", "The last time this repair tip was updated.")]
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
                    this.UpdatedField("UpdatedDateTimeUTC");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/> the record was last updated on in universal time
        /// </summary>
        [PropertyDefinition("Approved", "The last time this repair tip was approved.")]
        public NullableDateTime ApprovedDateTimeUTC
        {
            get
            {
                EnsureLoaded();
                return approvedDateTimeUTC;
            }
            set
            {
                EnsureLoaded();
                if (approvedDateTimeUTC != value)
                {
                    IsObjectDirty = true;
                    approvedDateTimeUTC = value;
                    this.UpdatedField("ApprovedDateTimeUTC");
                }
            }
        }

        /// <summary>
        /// FixNameCollection
        /// </summary>
        public FixNameCollection FixNameCollection
        {
            get
            {
                if (this.fixNameCollection == null)
                {
                    this.fixNameCollection = new FixNameCollection(this.Registry);
                    this.fixNameCollection.LoadByRepairTip(this);
                }

                return this.fixNameCollection;
            }
        }

        /// <summary>
        /// Get the <see cref="List{T}"/> of vehicle models that apply to this fix.
        /// NOTE: DO NOT add to this collection directly. Use the AddModel() method.
        /// </summary>
        [PropertyDefinition("ErrorCodes", "ErrorCodes that apply to this RT.")]
        public List<string> ErrorCodes
        {
            get
            {
                if (this.dtcs == null)
                {
                    this.EnsureLoaded();

                    this.dtcs = new List<string>();

                    if (!this.isObjectCreated && this.errorCodesString != "")
                    {
                        foreach (string s in this.errorCodesString.Split("|".ToCharArray()))
                        {
                            if (!string.IsNullOrEmpty(s))
                            {
                                //this.models.Add(s.ToUpper()); //Added on 2018-12-10 2:50 PM by INNOVA Dev Team
                                this.dtcs.Add(s.ToUpper());
                            }
                        }
                    }
                }

                return this.dtcs;
            }
        }

        /// <summary>
        /// Assign to fixnames
        /// </summary>
        /// <param name="registry"></param>
        /// <param name="repairTipId"></param>
        /// <param name="fixNameIds"></param>
        public void AssignToFixNames(Registry registry, Guid repairTipId, List<Guid> fixNameIds)
        {
            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(registry.ConnectionStringDefault))
            {
                dr.ProcedureName = "RepairTip_AssignToFixName";
                dr.AddGuid("FixNameId", fixNameIds[0]);
                dr.AddGuid("RepairTipId", repairTipId);
                dr.ExecuteNonQuery();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="registry"></param>
        /// <param name="fixNameId"></param>
        public static List<RepairTipFixName> LoadAssignmentByFixName(Registry registry, Guid fixNameId)
        {
            List<RepairTipFixName> assignmentList = new List<RepairTipFixName>();
            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(registry.ConnectionStringDefault))
            {
                dr.ProcedureName = "RepairTip_CheckExistedByFixName";
                dr.AddGuid("FixNameId", fixNameId);

                dr.Execute();
                while (dr.Read())
                {
                    assignmentList.Add(new RepairTipFixName
                    {
                        FixNameId = dr.GetGuid("FixNameId"),
                        RepairTipId = dr.GetGuid("RepairTipId")
                    });
                }
            }

            return assignmentList;
        }

        /// <summary>
        /// FixNameIDs
        /// </summary>
        public List<Guid> FixNameIdList
        {
            get
            {
                if (FixNameCollection == null || FixNameCollection.Count == 0)
                    return new List<Guid>();

                List<Guid> ids = new List<Guid>();
                foreach (FixName fn in FixNameCollection)
                {
                    ids.Add(fn.Id);
                }
                return ids;
            }
        }

        /// <summary>
        /// Gets the total number of languages 1, for just english, and another for each additional description field added.
        /// </summary>
        [PropertyDefinition("Language Count", "Number of languages available to be viewed in for this fix name.")]
        public int LanguageCount
        {
            get
            {
                return 1
                    + (!String.IsNullOrEmpty(this.description_es) ? 1 : 0)
                    + (!String.IsNullOrEmpty(this.description_fr) ? 1 : 0)
                    + (!String.IsNullOrEmpty(this.description_zh) ? 1 : 0);
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
        /// <param name="registry"></param>
        /// <param name="year"></param>
        /// <param name="make"></param>
        /// <param name="model"></param>
        /// <param name="engineType"></param>
        /// <param name="dtc"></param>
        /// <returns></returns>
        public static RepairTipCollection SearchTipByYMMEAndDTC(
            Registry registry, int year, string make, string model,
            string engineType, string dtc)
        {
            return Search(registry, null, "", null,
                year, make, model, engineType, "RepairTip.CreatedDateTimeUTC", SortDirection.Ascending,
                1, 100, true, false, dtc);
        }

        /// <summary>
        /// Search Tips
        /// </summary>
        /// <returns></returns>
        public static RepairTipCollection Search(
            Registry registry, Guid? fixNameId, string searchTerm, Market? market, int? year, string make, string model, string engineType,
            string orderBy, SortDirection sortDirection, int currentPage, int pageSize
            , bool includeApproved, bool includeNonApproved, string dtc, string status = "", string familyId = "")
        {
            RepairTipCollection repairTipCollection = new RepairTipCollection(registry);
            SqlProcedureCommand call = new SqlProcedureCommand();
            call.ProcedureName = "RepairTip_LoadBySearch";
            if (fixNameId.HasValue)
                call.AddGuid("FixNameId", fixNameId);
            call.AddNVarChar("SearchTerm", searchTerm);
            call.AddNVarChar("DTC", dtc);
            call.AddNVarChar("OrderBy", orderBy);
            call.AddInt32("SortDirection", (int)sortDirection);
            call.AddInt32("CurrentPage", currentPage);
            call.AddInt32("PageSize", pageSize);
            call.AddBoolean("IncludeApproved", includeApproved);
            call.AddBoolean("IncludeNonApproved", includeNonApproved);
            if (market.HasValue)
            {
                call.AddInt32("Market", (int)market.Value);
            }
            if (year > 0) //if (year >= 0)
            {
                call.AddInt32("Year", year);
            }
            call.AddNVarChar("Make", make);
            call.AddNVarChar("Model", model);
            call.AddNVarChar("EngineType", engineType);
            call.AddNVarChar("Status", status);
            call.AddNVarChar("FamilyId", familyId);

            repairTipCollection.Load(call, "RepairTipId", true, true, true);

            return repairTipCollection;
        }

        /// <summary>
        /// Load fix YMME and compare to YMME in params to get tips
        /// </summary>
        /// <returns></returns>
        public static List<RepairTip> GeTips(Registry registry, Guid fixId, int year, string make, string model, string engineType)
        {
            if (string.IsNullOrWhiteSpace(make)) make = "";
            if (string.IsNullOrWhiteSpace(model)) model = "";
            if (string.IsNullOrWhiteSpace(engineType)) engineType = "";

            List<RepairTip> validTips = new List<RepairTip>();

            Fix fix = (Fix)registry.CreateInstance(typeof(Fix), fixId);
            try
            {
                fix.Load();
            }
            catch
            {
            }

            if (fix == null) return new List<RepairTip>();

            var tips = Search(registry, fix.FixName.Id, "",
                null, null, "", "", "", "Description", SortDirection.Ascending, 1,
                10, true, false, "");

            if (tips == null || tips.Count == 0) return new List<RepairTip>();

            foreach (RepairTip tip in tips)
            {
                if (!tip.hasMakeDefined && !tip.hasModelDefined && !tip.hasYearDefined && !tip.hasEngineTypeDefined)
                {
                    validTips.Add(tip);
                }
                else
                {
                    if (tip.HasMakeDefined)
                    {
                        //check tip's makes to request make
                        if (string.IsNullOrWhiteSpace(make) || !tip.Makes.Contains(make.ToUpper())) continue;
                    }

                    if (tip.HasModelDefined)
                    {
                        if (string.IsNullOrWhiteSpace(model) || !tip.Models.Contains(model.ToUpper())) continue;
                    }

                    if (tip.HasYearDefined)
                    {
                        if (year == 0 || !tip.Years.Contains(year)) continue;
                    }

                    if (tip.HasEngineTypeDefined)
                    {
                        if (string.IsNullOrWhiteSpace(engineType) || !tip.EngineTypes.Contains(engineType.ToUpper())) continue;
                    }

                    validTips.Add(tip);
                }
            }

            return validTips;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="registry"></param>
        /// <param name="sqls"></param>
        public static void ExecuteUpdates(Registry registry, List<string> sqls)
        {
            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(registry.ConnectionStringDefault))
            {
                dr.SqlProcedureCommand.SqlCommand.CommandType = CommandType.Text;
                dr.SqlProcedureCommand.SqlCommand.CommandText = string.Join(Environment.NewLine, sqls);

                dr.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Adds a <see cref="string"/> engine type to the EngineTypes property.
        /// </summary>
        /// <param name="engineType"><see cref="string"/> engine type to add to the list.</param>
        public void AddEngineType(string engineType)
        {
            engineType = engineType.Trim();

            if (!String.IsNullOrEmpty(engineType) && !this.EngineTypes.Contains(engineType))
            {
                this.EngineTypes.Add(engineType);
                this.hasEngineTypeDefined = true;
                this.isEngineTypesDirty = true;
                this.IsObjectDirty = true;
            }
        }

        /// <summary>
        /// Removes a <see cref="string"/> engine type from the EngineTypes property.
        /// </summary>
        /// <param name="engineType">The <see cref="string"/> engine type to be removed.</param>
        public void RemoveEngineType(string engineType)
        {
            if (this.EngineTypes.Contains(engineType))
            {
                this.EngineTypes.Remove(engineType);
                this.isEngineTypesDirty = true;
                this.IsObjectDirty = true;
            }

            this.hasEngineTypeDefined = (this.EngineTypes.Count > 0);
        }

        /// <summary>
        /// Clears all members of the engine type property of values.
        /// </summary>
        public void ClearEngineType()
        {
            //if the engine vin code string is not null then we have some to clear
            if (this.EngineTypes.Count > 0)
            {
                this.EngineTypes.Clear();
                this.isEngineTypesDirty = true;
                this.IsObjectDirty = true;
                this.hasEngineTypeDefined = false;
            }
        }

        /// <summary>
        /// Adds a make to the internal string collection.
        /// </summary>
        /// <param name="make">The <see cref="string"/> make to be added.</param>
        public void AddMake(string make)
        {
            make = make.Trim();

            if (!String.IsNullOrEmpty(make) && !this.Makes.Contains(make))
            {
                this.Makes.Add(make);
                this.hasMakeDefined = true;
                this.isMakesDirty = true;
                this.IsObjectDirty = true;
            }
        }

        /// <summary>
        /// Removes a make from the internal string collection.
        /// </summary>
        /// <param name="make">The <see cref="string"/> make to be removed.</param>
        public void RemoveMake(string make)
        {
            if (this.Makes.Contains(make))
            {
                this.Makes.Remove(make);
                this.isMakesDirty = true;
                this.IsObjectDirty = true;
            }

            this.hasMakeDefined = (this.Makes.Count > 0);
        }

        /// <summary>
        /// Clears all members of the makes property of values.
        /// </summary>
        public void ClearMake()
        {
            if (this.Makes.Count > 0)
            {
                this.Makes.Clear();
                this.isMakesDirty = true;
                this.IsObjectDirty = true;
                this.hasMakeDefined = false;
            }
        }

        /// <summary>
        /// Adds a model to the internal string collection.
        /// </summary>
        /// <param name="model">The <see cref="string"/> model to be added.</param>
        public void AddModel(string model)
        {
            model = model.Trim();

            if (!String.IsNullOrEmpty(model) && !this.Models.Contains(model))
            {
                this.Models.Add(model);
                this.hasModelDefined = true;
                this.isModelsDirty = true;
                this.IsObjectDirty = true;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="errorCode"></param>
        public void AddErrorCode(string errorCode)
        {
            errorCode = errorCode.Trim();

            if (!String.IsNullOrEmpty(errorCode) && !this.Models.Contains(errorCode))
            {
                this.ErrorCodes.Add(errorCode);
                this.isErrorCodesDirty = true;
                this.IsObjectDirty = true;
            }
        }

        /// <summary>
        /// Removes a model from the internal string collection.
        /// </summary>
        /// <param name="model">The <see cref="string"/> model to be removed.</param>
        public void RemoveModel(string model)
        {
            if (this.Models.Contains(model))
            {
                this.Models.Remove(model);
                this.isErrorCodesDirty = true;
                this.IsObjectDirty = true;
            }

            this.hasModelDefined = (this.Models.Count > 0);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="errorCode"></param>
        public void RemoveErrorCode(string errorCode)
        {
            if (this.ErrorCodes.Contains(errorCode))
            {
                this.ErrorCodes.Remove(errorCode);
                this.isErrorCodesDirty = true;
                this.IsObjectDirty = true;
            }
        }

        /// <summary>
        ///
        /// </summary>
        public void ClearErrorCode()
        {
            if (this.ErrorCodes.Count > 0)
            {
                this.ErrorCodes.Clear();
                this.isErrorCodesDirty = true;
                this.IsObjectDirty = true;
            }
        }

        /// <summary>
        /// Clears all members of the Models property of values.
        /// </summary>
        public void ClearModel()
        {
            if (this.Models.Count > 0)
            {
                this.Models.Clear();
                this.isModelsDirty = true;
                this.IsObjectDirty = true;
                this.hasModelDefined = false;
            }
        }

        /// <summary>
        /// Adds a year to the internal collection.
        /// </summary>
        /// <param name="year">The <see cref="int"/> year to be added.</param>
        public void AddYear(int year)
        {
            if (!this.Years.Contains(year))
            {
                this.Years.Add(year);
                this.hasYearDefined = true;
                this.isYearsDirty = true;
                this.IsObjectDirty = true;
            }
        }

        /// <summary>
        /// Removes a year from the internal collection.
        /// </summary>
        /// <param name="year">The <see cref="int"/> year to be removed.</param>
        public void RemoveYear(int year)
        {
            if (this.Years.Contains(year))
            {
                this.Years.Remove(year);
                this.isYearsDirty = true;
                this.IsObjectDirty = true;
            }

            this.hasYearDefined = (this.Years.Count > 0);
        }

        /// <summary>
        /// Clears all members of the Years property of values.
        /// </summary>
        public void ClearYear()
        {
            if (this.Years.Count > 0)
            {
                this.Years.Clear();
                this.isYearsDirty = true;
                this.IsObjectDirty = true;
                this.hasYearDefined = false;
            }
        }

        /// <summary>
        /// Adds a <see cref="Market"/> to the Markets property.
        /// </summary>
        /// <param name="market">The <see cref="Market"/> to add to the list.</param>
        public void AddMarket(Market market)
        {
            if (!this.Markets.Contains(market))
            {
                this.Markets.Add(market);
                this.isMarketsDirty = true;
                this.IsObjectDirty = true;
            }
        }

        /// <summary>
        /// Removes a <see cref="Market"/> from the Markets property.
        /// </summary>
        /// <param name="market">The <see cref="Market"/> to be removed.</param>
        public void RemoveMarket(Market market)
        {
            if (this.Markets.Contains(market))
            {
                this.Markets.Remove(market);
                this.isMarketsDirty = true;
                this.IsObjectDirty = true;
            }
        }

        /// <summary>
        /// Clears all members of the markets property of values.
        /// </summary>
        public void ClearMarket()
        {
            //if the market is not null then we have some to clear
            if (this.Markets.Count > 0)
            {
                this.Markets.Clear();
                this.isMarketsDirty = true;
                this.IsObjectDirty = true;
            }
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

                if (!String.IsNullOrEmpty(os))
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
                //load the base item if user selected it.
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
            dr.ProcedureName = "RepairTip_Load";

            dr.AddGuid("RepairTipId", Id);
        }

        /// <summary>
        /// Sets the properties of this object from the fields in the recordset located in the supplied <see cref="SqlDataReaderWrapper"/>.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> containing the recordset and fields used to set the properties of this object (this layer only).</param>
        protected new void SetPropertiesFromDataReader(SqlDataReaderWrapper dr)
        {
            this.description = dr.GetString("Description");
            this.description_es = dr.GetString("Description_es");
            this.description_fr = dr.GetString("Description_fr");
            this.description_zh = dr.GetString("Description_zh");

            this.possibleCause = dr.GetString("PossibleCause");
            this.possibleCause_es = dr.GetString("PossibleCause_es");
            this.possibleCause_fr = dr.GetString("PossibleCause_fr");
            this.possibleCause_zh = dr.GetString("PossibleCause_zh");

            this.diagnosticProcedure = dr.GetString("DiagnosticProcedure");
            this.diagnosticProcedure_es = dr.GetString("DiagnosticProcedure_es");
            this.diagnosticProcedure_fr = dr.GetString("DiagnosticProcedure_fr");
            this.diagnosticProcedure_zh = dr.GetString("DiagnosticProcedure_zh");

            this.repairValidation = dr.GetString("RepairValidation");
            this.repairValidation_es = dr.GetString("RepairValidation_es");
            this.repairValidation_fr = dr.GetString("RepairValidation_fr");
            this.repairValidation_zh = dr.GetString("RepairValidation_zh");

            this.isApproved = dr.GetBoolean("IsApproved");

            this.createdByAdminUser = //(AdminUser)Registry.CreateInstance(typeof(AdminUser), dr.GetGuid("CreatedByAdminUserId"));
                (AdminUser)dr.GetBusinessObjectBase(this.Registry, typeof(AdminUser), "CreatedByAdminUserId");
            this.updatedByAdminUser = //(AdminUser)Registry.CreateInstance(typeof(AdminUser), dr.GetGuid("UpdatedByAdminUserId"));
                (AdminUser)dr.GetBusinessObjectBase(this.Registry, typeof(AdminUser), "UpdatedByAdminUserId");

            this.approvedByAdminUser =
            //(AdminUser) Registry.CreateInstance(typeof(AdminUser), dr.GetGuid("ApprovedByAdminUserId"));
            (AdminUser)dr.GetBusinessObjectBase(this.Registry, typeof(AdminUser), "ApprovedByAdminUserId");

            this.createdDateTimeUTC = dr.GetDateTime("CreatedDateTimeUTC");
            this.updatedDateTimeUTC = dr.GetDateTime("UpdatedDateTimeUTC");
            this.approvedDateTimeUTC = dr.GetNullableDateTime("ApprovedDateTimeUTC");

            this.marketsString = dr.GetString("MarketsString");

            this.hasEngineTypeDefined = dr.GetBoolean("HasEngineTypeDefined");
            this.engineTypesString = dr.GetString("EngineTypesString");

            this.hasYearDefined = dr.GetBoolean("HasYearDefined");
            this.yearsString = dr.GetString("YearsString");

            this.hasMakeDefined = dr.GetBoolean("HasMakeDefined");
            this.makesString = dr.GetString("MakesString");

            this.hasModelDefined = dr.GetBoolean("HasModelDefined");
            this.modelsString = dr.GetString("ModelsString");

            this.errorCodesString = dr.GetString("ErrorCodesString");

            #region IA-236

            if (dr.ColumnExists("Status"))
            {
                this.Status = dr.GetSysNullableString("Status");
            }

            if (dr.ColumnExists("Notes"))
            {
                this.Notes = dr.GetSysNullableString("Notes");
            }

            if (dr.ColumnExists("FileNames"))
            {
                this.FileNames = dr.GetSysNullableString("FileNames");
            }

            if (dr.ColumnExists("FamilyId"))
            {
                this.FamilyId = dr.GetSysNullableString("FamilyId");
            }

            #endregion IA-236

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
                        dr.ProcedureName = "RepairTip_Create";
                        if (this.CreatedDateTimeUTC == DateTime.MinValue)
                            CreatedDateTimeUTC = DateTime.Now.ToUniversalTime();

                        UpdatedDateTimeUTC = CreatedDateTimeUTC;
                    }
                    else
                    {
                        //dr.UpdateFields("RepairTip", "RepairTipId", this.updatedFields);
                        dr.ProcedureName = "RepairTip_Save";
                        this.UpdatedDateTimeUTC = DateTime.UtcNow;
                    }

                    dr.AddGuid("RepairTipId", this.Id);

                    dr.AddNVarChar("Description", this.Description);
                    dr.AddNVarChar("Description_es", this.Description_es);
                    dr.AddNVarChar("Description_fr", this.Description_fr);
                    dr.AddNVarChar("Description_zh", this.Description_zh);

                    dr.AddNVarChar("PossibleCause", this.PossibleCause);
                    dr.AddNVarChar("PossibleCause_es", this.PossibleCause_es);
                    dr.AddNVarChar("PossibleCause_fr", this.PossibleCause_fr);
                    dr.AddNVarChar("PossibleCause_zh", this.PossibleCause_zh);

                    dr.AddNVarChar("DiagnosticProcedure", this.DiagnosticProcedure);
                    dr.AddNVarChar("DiagnosticProcedure_es", this.DiagnosticProcedure_es);
                    dr.AddNVarChar("DiagnosticProcedure_fr", this.DiagnosticProcedure_fr);
                    dr.AddNVarChar("DiagnosticProcedure_zh", this.DiagnosticProcedure_zh);

                    dr.AddNVarChar("RepairValidation", this.RepairValidation);
                    dr.AddNVarChar("RepairValidation_es", this.RepairValidation_es);
                    dr.AddNVarChar("RepairValidation_fr", this.RepairValidation_fr);
                    dr.AddNVarChar("RepairValidation_zh", this.RepairValidation_zh);

                    dr.AddBoolean("IsApproved", this.IsApproved);

                    dr.AddGuid("CreatedByAdminUserId", this.CreatedByAdminUser.Id);
                    dr.AddGuid("UpdatedByAdminUserId", this.UpdatedByAdminUser.Id);

                    dr.AddDateTime("CreatedDateTimeUTC", this.CreatedDateTimeUTC);
                    dr.AddDateTime("UpdatedDateTimeUTC", this.UpdatedDateTimeUTC);
                    if (this.IsApproved)
                    {
                        dr.AddGuid("ApprovedByAdminUserId", this.ApprovedByAdminUser.Id);
                        dr.AddDateTime("ApprovedDateTimeUTC", this.ApprovedDateTimeUTC);
                    }

                    if (this.isMarketsDirty)
                    {
                        string s = "";

                        foreach (Market m in this.Markets)
                        {
                            string intString = ((int)m).ToString();

                            if (s.Length > 0)
                            {
                                s += "|";
                            }
                            s += intString.Trim();
                        }

                        this.marketsString = s;
                    }
                    dr.AddNVarChar("MarketsString", this.marketsString);
                    dr.AddBoolean("HasEngineTypeDefined", this.HasEngineTypeDefined);
                    if (this.isEngineTypesDirty)
                    {
                        this.engineTypesString = this.BuildStringList(this.EngineTypes);
                    }
                    dr.AddNVarChar("EngineTypesString", this.engineTypesString);
                    dr.AddBoolean("HasYearDefined", HasYearDefined);
                    if (this.isYearsDirty)
                    {
                        this.yearsString = this.BuildStringList(this.Years);
                    }
                    dr.AddNVarChar("YearsString", this.yearsString);

                    dr.AddBoolean("HasMakeDefined", HasMakeDefined);
                    if (this.isMakesDirty)
                    {
                        this.makesString = this.BuildStringList(this.Makes);
                    }
                    dr.AddNVarChar("MakesString", this.makesString);

                    dr.AddBoolean("HasModelDefined", HasModelDefined);
                    if (this.isModelsDirty)
                    {
                        this.modelsString = this.BuildStringList(this.Models);
                    }
                    dr.AddNVarChar("ModelsString", this.modelsString);

                    if (this.isErrorCodesDirty)
                    {
                        this.errorCodesString = this.BuildStringList(this.ErrorCodes);
                    }
                    dr.AddNVarChar("ErrorCodesString", this.errorCodesString);

                    dr.AddNVarChar("Status", Status);

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
            if (this.isEngineTypesDirty)
            {
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    dr.ProcedureName = "RepairTip_SaveEngineType";
                    dr.AddGuid("RepairTipId", Id);
                    dr.AddNText("EngineTypesXMLList", Metafuse3.Xml.XmlList.ToXml(this.EngineTypes));
                    dr.ExecuteNonQuery(transaction);
                }
                this.isEngineTypesDirty = false;
            }

            if (this.isMakesDirty)
            {
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    dr.ProcedureName = "RepairTip_SaveMakes";
                    dr.AddGuid("RepairTipId", Id);
                    dr.AddNText("MakesXmlList", Metafuse3.Xml.XmlList.ToXml(this.Makes));
                    dr.ExecuteNonQuery(transaction);
                }
                this.isMakesDirty = false;
            }
            if (this.isModelsDirty)
            {
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    dr.ProcedureName = "RepairTip_SaveModels";
                    dr.AddGuid("RepairTipId", Id);
                    dr.AddNText("ModelsXmlList", Metafuse3.Xml.XmlList.ToXml(this.Models).Replace("&amp;", "&").Replace("&", "&amp;"));
                    dr.ExecuteNonQuery(transaction);
                }
                this.isModelsDirty = false;
            }

            if (this.isErrorCodesDirty)
            {
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    dr.ProcedureName = "RepairTip_SaveErrorCodes";
                    dr.AddGuid("RepairTipId", Id);
                    dr.AddNText("ErrorCodesXmlList", Metafuse3.Xml.XmlList.ToXml(this.ErrorCodes));
                    dr.ExecuteNonQuery(transaction);
                }
                this.isModelsDirty = false;
            }

            if (this.isYearsDirty)
            {
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    dr.ProcedureName = "RepairTip_SaveYears";
                    dr.AddGuid("RepairTipId", Id);
                    dr.AddNText("YearsXmlList", Metafuse3.Xml.XmlList.ToXml(this.Years));
                    dr.ExecuteNonQuery(transaction);
                }
                this.isYearsDirty = false;
            }

            if (this.isApproved)
            {
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    dr.ProcedureName = "usp_RepairTip_MarkAllRepairTipsInTheSameFamilyId";
                    dr.AddGuid("RepairTipId", Id);
                    dr.ExecuteNonQuery(transaction);
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
            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                dr.ProcedureName = "RepairTipMake_DeleteByRepairTip";
                dr.AddGuid("RepairTipId", Id);
                dr.ExecuteNonQuery(transaction);
            }
            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                dr.ProcedureName = "RepairTipModel_DeleteByRepairTip";
                dr.AddGuid("RepairTipId", Id);
                dr.ExecuteNonQuery(transaction);
            }
            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                dr.ProcedureName = "RepairTipYear_DeleteByRepairTip";
                dr.AddGuid("RepairTipId", Id);
                dr.ExecuteNonQuery(transaction);
            }
            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                dr.ProcedureName = "RepairTipEngineType_DeleteByRepairTip";
                dr.AddGuid("RepairTipId", Id);
                dr.ExecuteNonQuery(transaction);
            }

            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                dr.ProcedureName = "RepairTipDTC_DeleteByRepairTip";
                dr.AddGuid("RepairTipId", Id);
                dr.ExecuteNonQuery(transaction);
            }

            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                //delete RepairTipExtraData
                dr.ProcedureName = "usp_RepairTipExtraData_Delete";
                dr.AddGuid("RepairTipId", Id);

                dr.ExecuteNonQuery(transaction);
            }

            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                //delete the repair tip
                dr.ProcedureName = "RepairTip_Delete";
                dr.AddGuid("RepairTipId", Id);

                dr.ExecuteNonQuery(transaction);
            }
            transaction = base.Delete(connection, transaction);

            return transaction;
        }

        public static List<string> GetCurrentFamilyIds(Registry registry)
        {
            var familyIds = new List<string>();

            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(registry.ConnectionStringDefault))
            {
                dr.ProcedureName = "usp_RepairTip_GetCurrentFamilyIds";
                dr.Execute();

                while (dr.Read())
                {
                    familyIds.Add(dr.GetString("FamilyId"));
                }
            }

            return familyIds;
        }

        #endregion Required Object Methods (Load, Save, Delete, Etc)
    }

    public class RepairTipFixName
    {
        public Guid RepairTipId { get; set; }
        public Guid FixNameId { get; set; }

        //#RepairTipYMME
        public Guid FixId { get; set; }

        public string FixNameDescription { get; set; }
    }
}
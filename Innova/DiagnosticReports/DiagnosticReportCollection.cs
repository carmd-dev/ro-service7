using Innova.Symptoms;
using Innova.Users;
using Innova.Vehicles;
using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using System;

namespace Innova.DiagnosticReports
{
    /// <summary>
    /// Typed collection for the corresponding business object
    /// (Remove the word "Collection" from DiagnosticReportCollection).
    /// Inherits from <see cref="BusinessObjectCollectionBase"/>.
    /// </summary>
    /// <example>
    /// To create the collection:
    ///
    /// <code>DiagnosticReportCollection c = new DiagnosticReportCollection(Registry);</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of DiagnosticReportCollection, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectCollectionBase.ConnectionString"/> to the
    /// <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable()]
    public class DiagnosticReportCollection : BusinessObjectCollectionBase
    {
        #region System Constructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new DiagnosticReportCollection object.
        /// </summary>
        /// <param name="registry"><see cref="Registry"/> currently being used to store <see cref="BusinessObjectBase"/> objects.</param>
        public DiagnosticReportCollection(Registry registry) : base(registry)
        {
            businessObjectBaseType = typeof(DiagnosticReport);
        }

        #endregion System Constructors

        #region Load Methods

        /// <summary>
        /// Loads the collection with reports that have a payload.
        /// </summary>
        public void LoadReportsWithAPayload()
        {
            SqlProcedureCommand command = new SqlProcedureCommand();
            command.ProcedureName = "DiagnosticReport_LoadReportsWithAPayLoad";
            command.SqlCommand.CommandTimeout = 0;

            this.Load(command, "DiagnosticReportId", true, true, false);
        }

        /// <summary>
        /// Loads the collection with reports that have freeze frame values.
        /// </summary>
        public void LoadReportsWithFreezeFrames()
        {
            SqlProcedureCommand command = new SqlProcedureCommand();
            command.ProcedureName = "DiagnosticReport_LoadReportsWithFreezeFrames";
            command.SqlCommand.CommandTimeout = 0;

            this.Load(command, "DiagnosticReportId", true, true, false);
        }

        /// <summary>
        /// Loads the collection with reports that have freeze frame values and multiple DTCs.
        /// </summary>
        public void LoadReportsWithFreezeFramesAndMultipleDTCs()
        {
            SqlProcedureCommand command = new SqlProcedureCommand();
            command.ProcedureName = "DiagnosticReport_LoadReportsWithFreezeFramesAndStoredCodes";
            command.SqlCommand.CommandTimeout = 0;

            this.Load(command, "DiagnosticReportId", true, true, false);
        }

        /// <summary>
        /// Load Symptom report collection
        /// </summary>
	    public void LoadSymptomReportCollection()
        {
            var reportIds = this.GetXmlGuidListFromProperty("Id");
            var symptomReports = SymptomDiagnosticReportItemCollection.Search(Registry, reportIds);
            //symptomReports.LoadRelation(symptomReports.RelationSymptoms);

            foreach (DiagnosticReport report in this)
            {
                foreach (SymptomDiagnosticReportItem symptom in symptomReports)
                {
                    if (report.Id == symptom.DiagnosticReportId)
                    {
                        //if (report.SymptomDiagnosticReportItemCollection.FindByProperty("SymptomId", symptom.Id) == null)
                        //{
                        report.SymptomDiagnosticReportItemCollection.Add(symptom);
                        //}
                    }
                }
            }
        }

        #endregion Load Methods

        #region Relation Properties

        /// <summary>
        /// Gets the <see cref="BusinessObjectCollectionRelationDefinition"/> for loading the <see cref="DiagnosticReport.Vehicle"/> properties.
        /// </summary>
        public BusinessObjectCollectionRelationDefinition RelationVehicles
        {
            get
            {
                SqlProcedureCommand call = new SqlProcedureCommand();
                call.ProcedureName = "Vehicle_LoadByXmlGuidList";
                return new BusinessObjectCollectionRelationDefinition(typeof(Vehicle), "Vehicle.Id", "XmlGuidList", call, "VehicleId", true, true);
            }
        }

        /// <summary>
        /// Gets the <see cref="BusinessObjectCollectionRelationDefinition"/> for loading the <see cref="DiagnosticReport.User"/> properties.
        /// </summary>
        public BusinessObjectCollectionRelationDefinition RelationUsers
        {
            get
            {
                SqlProcedureCommand call = new SqlProcedureCommand();
                call.ProcedureName = "User_LoadByXmlGuidList";
                return new BusinessObjectCollectionRelationDefinition(typeof(User), "User.Id", "XmlGuidList", call, "UserId", true, true);
            }
        }

        /// <summary>
        /// Gets the <see cref="BusinessObjectCollectionRelationDefinition"/> for loading the <see cref="DiagnosticReportResultFixCollection"/> properties.
        /// </summary>
        public BusinessObjectCollectionRelationDefinition RelationDiagnosticReportResults
        {
            get
            {
                SqlProcedureCommand call = new SqlProcedureCommand();
                call.ProcedureName = "DiagnosticReportResult_LoadByXmlGuidList";
                return new BusinessObjectCollectionRelationDefinition(typeof(DiagnosticReportResult), "DiagnosticReportResult.Id", "XmlGuidList", call, "DiagnosticReportResultId", true, true);
            }
        }

        /// <summary>
        /// Gets the <see cref="BusinessObjectCollectionRelationDefinition"/> for loading the <see cref="DiagnosticReport.FixFeedbacks"/> properties.
        /// </summary>
        public BusinessObjectCollectionRelationDefinition RelationDiagnosticReportFixFeedbacks
        {
            get
            {
                SqlProcedureCommand call = new SqlProcedureCommand();
                call.ProcedureName = "DiagnosticReportFixFeedback_LoadByDiagnosticReportXmlGuidList";
                return new BusinessObjectCollectionRelationDefinition(typeof(DiagnosticReportFixFeedback), "Id", "XmlGuidList", call, "DiagnosticReportFixFeedbackId", true, typeof(DiagnosticReportFixFeedbackCollection), "", "fixFeedbacks", "DiagnosticReportId", typeof(DiagnosticReport));
            }
        }

        /// <summary>
        /// Gets the <see cref="BusinessObjectCollectionRelationDefinition"/> for loading the <see cref="DiagnosticReport.DiagnosticReportFeedbacks"/> properties.
        /// </summary>
        public BusinessObjectCollectionRelationDefinition RelationDiagnosticReportFeedbacks
        {
            get
            {
                SqlProcedureCommand call = new SqlProcedureCommand();
                call.ProcedureName = "DiagnosticReportFeedback_LoadByDiagnosticReportXmlGuidList";
                return new BusinessObjectCollectionRelationDefinition(typeof(DiagnosticReports.Feedback.DiagnosticReportFeedback), "Id", "XmlGuidList", call, "DiagnosticReportFeedbackId", true, typeof(DiagnosticReports.Feedback.DiagnosticReportFeedbackCollection), "", "diagnosticReportFeedbacks", "DiagnosticReportId", typeof(DiagnosticReport));
            }
        }

        #endregion Relation Properties

        #region Indexer

        /// <summary>
        /// Indexer, used to return the <see cref="DiagnosticReport"/> located at the index (<see cref="int"/>) position of the list.
        /// </summary>
        public DiagnosticReport this[int index]
        {
            get
            {
                return (DiagnosticReport)List[index];
            }
        }

        #endregion Indexer

        #region Default System Collection Methods

        /*****************************************************************************************
		 *
		 * System Methods
		 *
		 * **************************************************************************************/

        /// <summary>
        /// Adds an <see cref="DiagnosticReport"/> object to the list.
        /// </summary>
        /// <param name="value"><see cref="DiagnosticReport"/> to add.</param>
        public void Add(DiagnosticReport value)
        {
            base.Add(value);
        }

        /// <summary>
        /// Removes an <see cref="DiagnosticReport"/> object from the list.
        /// </summary>
        /// <param name="value"><see cref="DiagnosticReport"/> to remove.</param>
        public void Remove(DiagnosticReport value)
        {
            base.Remove(value);
        }

        /// <summary>
        /// Inserts an <see cref="DiagnosticReport"/> object to the list.
        /// </summary>
        /// <param name="index"><see cref="int"/> index position inserting at</param>
        /// <param name="value"><see cref="DiagnosticReport"/> to add</param>
        public void Insert(int index, DiagnosticReport value)
        {
            base.Insert(index, value);
        }

        /// <summary>
        /// Creates the <see cref="DiagnosticReport"/> object from the specified recordset supplied and optionally sets the properties of the object, then adds it to the list.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> used.</param>
        /// <param name="idField"><see cref="string"/> The field name of the Guid that represents the objectId / primary key for the object.</param>
        /// <param name="isSetProperties"><see cref="bool"/> flag indicating whether or not to set the properties of the object.</param>
        /// <param name="isSetPropertiesOfBase"><see cref="bool"/> flag indicating whether or not to set the properties of bases of the object (if any).</param>
        /// <returns><see cref="BusinessObjectBase"/> created</returns>
        protected override BusinessObjectBase CreateBusinessObjectAndAddToListFromRecordset(SqlDataReaderWrapper dr, string idField, bool isSetProperties, bool isSetPropertiesOfBase)
        {
            DiagnosticReport obj = (DiagnosticReport)Registry.CreateInstance(typeof(DiagnosticReport), dr.GetGuid(idField));

            //if the base properties are set, then set the type of base
            if (isSetProperties)
            {
                obj.LoadPropertiesFromDataReader(dr, true);
            }

            this.Add(obj);

            return obj;
        }

        #endregion Default System Collection Methods
    }
}
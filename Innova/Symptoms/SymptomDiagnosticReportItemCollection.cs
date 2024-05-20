using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using System;

namespace Innova.Symptoms
{
    /// <summary>
    /// Typed collection for the corresponding business object
    /// Inherits from <see cref="BusinessObjectCollectionBase"/>.
    /// </summary>
    /// <example>
    /// To create the collection:
    ///
    /// <code>SymptomCollection c = new SymptomCollection(Registry);</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of SymptomCollection, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectCollectionBase.ConnectionString"/> to the
    /// <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable]
    public class SymptomDiagnosticReportItemCollection : BusinessObjectCollectionBase
    {
        #region System Constructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new Symptom object.
        /// </summary>
        /// <param name="registry"><see cref="Registry"/> currently being used to store <see cref="BusinessObjectBase"/> objects.</param>
        public SymptomDiagnosticReportItemCollection(Registry registry)
            : base(registry)
        {
            businessObjectBaseType = typeof(Symptom);
        }

        #endregion System Constructors



        #region Indexer

        /// <summary>
        /// Indexer, used to return the <see cref="Symptom"/> located at the index (<see cref="int"/>) position of the list.
        /// </summary>
        public SymptomDiagnosticReportItem this[int index]
        {
            get
            {
                return (SymptomDiagnosticReportItem)List[index];
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
        /// Adds an <see cref="SymptomDiagnosticReportItem"/> object to the list.
        /// </summary>
        /// <param name="value"><see cref="SymptomDiagnosticReportItem"/> to add.</param>
        public void Add(SymptomDiagnosticReportItem value)
        {
            base.Add(value);
        }

        /// <summary>
        /// Removes an <see cref="SymptomDiagnosticReportItem"/> object from the list.
        /// </summary>
        /// <param name="value"><see cref="SymptomDiagnosticReportItem"/> to remove.</param>
        public void Remove(SymptomDiagnosticReportItem value)
        {
            base.Remove(value);
        }

        /// <summary>
        /// Inserts an <see cref="Symptom"/> object to the list.
        /// </summary>
        /// <param name="index"><see cref="int"/> index position inserting at</param>
        /// <param name="value"><see cref="Symptom"/> to add</param>
        public void Insert(int index, SymptomDiagnosticReportItem value)
        {
            base.Insert(index, value);
        }

        /// <summary>
        /// Creates the <see cref="SymptomDiagnosticReportItem"/> object from the specified recordset supplied and optionally sets the properties of the object, then adds it to the list.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> used.</param>
        /// <param name="idField"><see cref="string"/> The field name of the Guid that represents the objectId / primary key for the object.</param>
        /// <param name="isSetProperties"><see cref="bool"/> flag indicating whether or not to set the properties of the object.</param>
        /// <param name="isSetPropertiesOfBase"><see cref="bool"/> flag indicating whether or not to set the properties of bases of the object (if any).</param>
        /// <returns><see cref="BusinessObjectBase"/> created</returns>
        protected override BusinessObjectBase CreateBusinessObjectAndAddToListFromRecordset(SqlDataReaderWrapper dr, string idField, bool isSetProperties, bool isSetPropertiesOfBase)
        {
            SymptomDiagnosticReportItem obj = (SymptomDiagnosticReportItem)Registry.CreateInstance(typeof(SymptomDiagnosticReportItem), dr.GetGuid(idField));

            //if the base properties are set, then set the type of base
            if (isSetProperties)
            {
                obj.LoadPropertiesFromDataReader(dr, isSetPropertiesOfBase);
            }

            this.Add(obj);

            return obj;
        }

        /// <summary>
        /// Load items by report GUID list XML
        /// </summary>
        /// <param name="registry"></param>
        /// <param name="diagnosticReportXmlGuidList"></param>
        /// <returns></returns>
        public static SymptomDiagnosticReportItemCollection Search(Registry registry, string diagnosticReportXmlGuidList)
        {
            SymptomDiagnosticReportItemCollection symptomReports = new SymptomDiagnosticReportItemCollection(registry);

            SqlProcedureCommand call = new SqlProcedureCommand();
            call.ProcedureName = "SymptomDiagnosticReportItem_LoadByDiagnosticXmlGuidList";

            call.AddXmlGuidList("DiagnosticReportXmlGuidList", diagnosticReportXmlGuidList);

            symptomReports.Load(call, "SymptomDiagnosticReportItemId", true, false, false);

            return symptomReports;
        }

        /// <summary>
        ///
        /// </summary>
        public BusinessObjectCollectionRelationDefinition RelationSymptoms
        {
            get
            {
                SqlProcedureCommand call = new SqlProcedureCommand();
                call.ProcedureName = "Symptom_LoadByXmlGuidList";
                return new BusinessObjectCollectionRelationDefinition(typeof(Symptom), "Symptom.Id", "XmlGuidList", call, "SymptomId", true, true);
            }
        }

        #endregion Default System Collection Methods
    }
}
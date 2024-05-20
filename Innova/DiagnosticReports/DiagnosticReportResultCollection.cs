using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using System;

namespace Innova.DiagnosticReports
{
    /// <summary>
    /// Typed collection for the corresponding business object
    /// (Remove the word "Collection" from DiagnosticReportResultCollection).
    /// Inherits from <see cref="BusinessObjectCollectionBase"/>.
    /// </summary>
    /// <example>
    /// To create the collection:
    ///
    /// <code>DiagnosticReportResultCollection c = new DiagnosticReportResultCollection(Registry);</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of DiagnosticReportResultCollection, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectCollectionBase.ConnectionString"/> to the
    /// <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable()]
    public class DiagnosticReportResultCollection : BusinessObjectCollectionBase
    {
        #region System Constructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new DiagnosticReportResultCollection object.
        /// </summary>
        /// <param name="registry"><see cref="Registry"/> currently being used to store <see cref="BusinessObjectBase"/> objects.</param>
        public DiagnosticReportResultCollection(Registry registry) : base(registry)
        {
            businessObjectBaseType = typeof(DiagnosticReportResult);
        }

        #endregion System Constructors

        #region Relation Properties

        /// <summary>
        /// Gets the <see cref="BusinessObjectCollectionRelationDefinition"/> for loading the <see cref="DiagnosticReportResultFix"/> properties.
        /// </summary>
        public BusinessObjectCollectionRelationDefinition RelationDiagnosticReportResultFixes
        {
            get
            {
                SqlProcedureCommand call = new SqlProcedureCommand();
                call.SqlCommand.CommandTimeout = 0; //Added on 2018-01-12 1:57 PM by INNOVA Dev Team to increase the timeout wait due to a heavy load of the No-Fix Report data.

                call.ProcedureName = "DiagnosticReportResultFix_LoadByDiagnosticReportResultXmlGuidList";
                return new BusinessObjectCollectionRelationDefinition(typeof(DiagnosticReportResultFix), "Id", "XmlGuidList", call, "DiagnosticReportResultFixId", true, typeof(DiagnosticReportResultFixCollection), "", "diagnosticReportResultFixes", "DiagnosticReportResultId", typeof(DiagnosticReportResult));
            }
        }

        #endregion Relation Properties

        #region Indexer

        /// <summary>
        /// Indexer, used to return the <see cref="DiagnosticReportResult"/> located at the index (<see cref="int"/>) position of the list.
        /// </summary>
        public DiagnosticReportResult this[int index]
        {
            get
            {
                return (DiagnosticReportResult)List[index];
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
        /// Adds an <see cref="DiagnosticReportResult"/> object to the list.
        /// </summary>
        /// <param name="value"><see cref="DiagnosticReportResult"/> to add.</param>
        public void Add(DiagnosticReportResult value)
        {
            base.Add(value);
        }

        /// <summary>
        /// Removes an <see cref="DiagnosticReportResult"/> object from the list.
        /// </summary>
        /// <param name="value"><see cref="DiagnosticReportResult"/> to remove.</param>
        public void Remove(DiagnosticReportResult value)
        {
            base.Remove(value);
        }

        /// <summary>
        /// Inserts an <see cref="DiagnosticReportResult"/> object to the list.
        /// </summary>
        /// <param name="index"><see cref="int"/> index position inserting at</param>
        /// <param name="value"><see cref="DiagnosticReportResult"/> to add</param>
        public void Insert(int index, DiagnosticReportResult value)
        {
            base.Insert(index, value);
        }

        /// <summary>
        /// Creates the <see cref="DiagnosticReportResult"/> object from the specified recordset supplied and optionally sets the properties of the object, then adds it to the list.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> used.</param>
        /// <param name="idField"><see cref="string"/> The field name of the Guid that represents the objectId / primary key for the object.</param>
        /// <param name="isSetProperties"><see cref="bool"/> flag indicating whether or not to set the properties of the object.</param>
        /// <param name="isSetPropertiesOfBase"><see cref="bool"/> flag indicating whether or not to set the properties of bases of the object (if any).</param>
        /// <returns><see cref="BusinessObjectBase"/> created</returns>
        protected override BusinessObjectBase CreateBusinessObjectAndAddToListFromRecordset(SqlDataReaderWrapper dr, string idField, bool isSetProperties, bool isSetPropertiesOfBase)
        {
            DiagnosticReportResult obj = (DiagnosticReportResult)Registry.CreateInstance(typeof(DiagnosticReportResult), dr.GetGuid(idField));

            //if the base properties are set, then set the type of base
            if (isSetProperties)
            {
                obj.LoadPropertiesFromDataReader(dr, isSetPropertiesOfBase);
            }

            this.Add(obj);

            return obj;
        }

        #endregion Default System Collection Methods
    }
}
using Innova.Fixes;
using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using System;

namespace Innova.DiagnosticReports
{
    /// <summary>
    /// Typed collection for the corresponding business object
    /// (Remove the word "Collection" from DiagnosticReportResultFixCollection).
    /// Inherits from <see cref="BusinessObjectCollectionBase"/>.
    /// </summary>
    /// <example>
    /// To create the collection:
    ///
    /// <code>DiagnosticReportResultFixCollection c = new DiagnosticReportResultFixCollection(Registry);</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of DiagnosticReportResultFixCollection, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectCollectionBase.ConnectionString"/> to the
    /// <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable()]
    public class DiagnosticReportResultFixCollection : BusinessObjectCollectionBase
    {
        #region System Constructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new DiagnosticReportResultFixCollection object.
        /// </summary>
        /// <param name="registry"><see cref="Registry"/> currently being used to store <see cref="BusinessObjectBase"/> objects.</param>
        public DiagnosticReportResultFixCollection(Registry registry) : base(registry)
        {
            businessObjectBaseType = typeof(DiagnosticReportResultFix);
        }

        #endregion System Constructors

        #region Relation Properties

        /// <summary>
        /// Gets the <see cref="BusinessObjectCollectionRelationDefinition"/> for loading the <see cref="DiagnosticReportResultFix.DiagnosticReportResultFixParts"/> properties.
        /// </summary>
        public BusinessObjectCollectionRelationDefinition RelationDiagnosticReportResultFixeParts
        {
            get
            {
                SqlProcedureCommand call = new SqlProcedureCommand();
                call.ProcedureName = "DiagnosticReportResultFixPart_LoadByXmlGuidList";
                return new BusinessObjectCollectionRelationDefinition(typeof(DiagnosticReportResultFixPart), "DiagnosticReportResultFixParts.Id", "XmlGuidList", call, "DiagnosticReportResultFixPartId", true, true);
            }
        }

        //ToolDB_
        /// <summary>
        /// Gets the <see cref="BusinessObjectCollectionRelationDefinition"/> for loading the <see cref="DiagnosticReportResultFix.DiagnosticReportResultFixTools"/> properties.
        /// </summary>
        public BusinessObjectCollectionRelationDefinition RelationDiagnosticReportResultFixTools
        {
            get
            {
                SqlProcedureCommand call = new SqlProcedureCommand();
                call.ProcedureName = "DiagnosticReportResultFixTool_LoadByXmlGuidList";
                return new BusinessObjectCollectionRelationDefinition(typeof(DiagnosticReportResultFixTool), "DiagnosticReportResultFixTools.Id", "XmlGuidList", call, "DiagnosticReportResultFixToolId", true, true);
            }
        }

        //ToolDB_

        /// <summary>
        /// Gets the <see cref="BusinessObjectCollectionRelationDefinition"/> for loading the <see cref="DiagnosticReportResultFix.FixName"/> properties.
        /// </summary>
        public BusinessObjectCollectionRelationDefinition RelationFixNames
        {
            get
            {
                SqlProcedureCommand call = new SqlProcedureCommand();
                //##Security - Commented out for temporary on 2020-02-28
                //call.AddNVarChar("EncryptionPassphrase", RuntimeInfo.EncryptionPassphrase);
                //##Security
                call.ProcedureName = "FixName_LoadByXmlGuidList";
                return new BusinessObjectCollectionRelationDefinition(typeof(FixName), "FixName.Id", "XmlGuidList", call, "FixNameId", true, true);
            }
        }

        #endregion Relation Properties

        #region Indexer

        /// <summary>
        /// Indexer, used to return the <see cref="DiagnosticReportResultFix"/> located at the index (<see cref="int"/>) position of the list.
        /// </summary>
        public DiagnosticReportResultFix this[int index]
        {
            get
            {
                return (DiagnosticReportResultFix)List[index];
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
        /// Adds an <see cref="DiagnosticReportResultFix"/> object to the list.
        /// </summary>
        /// <param name="value"><see cref="DiagnosticReportResultFix"/> to add.</param>
        public void Add(DiagnosticReportResultFix value)
        {
            base.Add(value);
        }

        /// <summary>
        /// Removes an <see cref="DiagnosticReportResultFix"/> object from the list.
        /// </summary>
        /// <param name="value"><see cref="DiagnosticReportResultFix"/> to remove.</param>
        public void Remove(DiagnosticReportResultFix value)
        {
            base.Remove(value);
        }

        /// <summary>
        /// Inserts an <see cref="DiagnosticReportResultFix"/> object to the list.
        /// </summary>
        /// <param name="index"><see cref="int"/> index position inserting at</param>
        /// <param name="value"><see cref="DiagnosticReportResultFix"/> to add</param>
        public void Insert(int index, DiagnosticReportResultFix value)
        {
            base.Insert(index, value);
        }

        /// <summary>
        /// Creates the <see cref="DiagnosticReportResultFix"/> object from the specified recordset supplied and optionally sets the properties of the object, then adds it to the list.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> used.</param>
        /// <param name="idField"><see cref="string"/> The field name of the Guid that represents the objectId / primary key for the object.</param>
        /// <param name="isSetProperties"><see cref="bool"/> flag indicating whether or not to set the properties of the object.</param>
        /// <param name="isSetPropertiesOfBase"><see cref="bool"/> flag indicating whether or not to set the properties of bases of the object (if any).</param>
        /// <returns><see cref="BusinessObjectBase"/> created</returns>
        protected override BusinessObjectBase CreateBusinessObjectAndAddToListFromRecordset(SqlDataReaderWrapper dr, string idField, bool isSetProperties, bool isSetPropertiesOfBase)
        {
            DiagnosticReportResultFix obj = (DiagnosticReportResultFix)Registry.CreateInstance(typeof(DiagnosticReportResultFix), dr.GetGuid(idField));

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
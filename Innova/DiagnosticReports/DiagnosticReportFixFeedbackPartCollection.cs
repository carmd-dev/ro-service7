﻿using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using System;

namespace Innova.DiagnosticReports
{
    /// <summary>
    /// Typed collection for the corresponding business object
    /// (Remove the word "Collection" from DiagnosticReportFixFeedbackPartCollection).
    /// Inherits from <see cref="BusinessObjectCollectionBase"/>.
    /// </summary>
    /// <example>
    /// To create the collection:
    ///
    /// <code>DiagnosticReportFixFeedbackPartCollection c = new DiagnosticReportFixFeedbackPartCollection(Registry);</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of DiagnosticReportFixFeedbackPartCollection, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectCollectionBase.ConnectionString"/> to the
    /// <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable()]
    public class DiagnosticReportFixFeedbackPartCollection : BusinessObjectCollectionBase
    {
        #region System Constructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new PartCollection object.
        /// </summary>
        /// <param name="registry"><see cref="Registry"/> currently being used to store <see cref="BusinessObjectBase"/> objects.</param>
        public DiagnosticReportFixFeedbackPartCollection(Registry registry)
            : base(registry)
        {
            businessObjectBaseType = typeof(DiagnosticReportFixFeedbackPart);
        }

        #endregion System Constructors

        #region Relation Properties

        /// <summary>
        /// Gets the <see cref="BusinessObjectCollectionRelationDefinition"/> for loading the <see cref="DiagnosticReportFixFeedbackPart"/> property.
        /// </summary>
        public BusinessObjectCollectionRelationDefinition RelationDiagnosticReportFixFeedbackPart
        {
            get
            {
                SqlProcedureCommand call = new SqlProcedureCommand();
                call.ProcedureName = "DiagnosticReportFixFeedbackPart_LoadByXmlGuidList";
                return new BusinessObjectCollectionRelationDefinition(typeof(DiagnosticReportFixFeedbackPart), "DiagnosticReportFixFeedbackPart.Id", "XmlGuidList", call, "DiagnosticReportFixFeedbackPartId", true, true);
            }
        }

        #endregion Relation Properties

        #region Indexer

        /// <summary>
        /// Indexer, used to return the <see cref="DiagnosticReportFixFeedbackPart"/> located at the index (<see cref="int"/>) position of the list.
        /// </summary>
        public DiagnosticReportFixFeedbackPart this[int index]
        {
            get
            {
                return (DiagnosticReportFixFeedbackPart)List[index];
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
        /// Adds an <see cref="Part"/> object to the list.
        /// </summary>
        /// <param name="value"><see cref="DiagnosticReportFixFeedbackPart"/> to add.</param>
        public void Add(DiagnosticReportFixFeedbackPart value)
        {
            base.Add(value);
        }

        /// <summary>
        /// Removes an <see cref="Part"/> object from the list.
        /// </summary>
        /// <param name="value"><see cref="DiagnosticReportFixFeedbackPart"/> to remove.</param>
        public void Remove(DiagnosticReportFixFeedbackPart value)
        {
            base.Remove(value);
        }

        /// <summary>
        /// Inserts an <see cref="Part"/> object to the list.
        /// </summary>
        /// <param name="index"><see cref="int"/> index position inserting at</param>
        /// <param name="value"><see cref="DiagnosticReportFixFeedbackPart"/> to add</param>
        public void Insert(int index, DiagnosticReportFixFeedbackPart value)
        {
            base.Insert(index, value);
        }

        /// <summary>
        /// Creates the <see cref="DiagnosticReportFixFeedbackPart"/> object from the specified recordset supplied and optionally sets the properties of the object, then adds it to the list.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> used.</param>
        /// <param name="idField"><see cref="string"/> The field name of the Guid that represents the objectId / primary key for the object.</param>
        /// <param name="isSetProperties"><see cref="bool"/> flag indicating whether or not to set the properties of the object.</param>
        /// <param name="isSetPropertiesOfBase"><see cref="bool"/> flag indicating whether or not to set the properties of bases of the object (if any).</param>
        /// <returns><see cref="BusinessObjectBase"/> created</returns>
        protected override BusinessObjectBase CreateBusinessObjectAndAddToListFromRecordset(SqlDataReaderWrapper dr, string idField, bool isSetProperties, bool isSetPropertiesOfBase)
        {
            DiagnosticReportFixFeedbackPart obj = (DiagnosticReportFixFeedbackPart)Registry.CreateInstance(typeof(DiagnosticReportFixFeedbackPart), dr.GetGuid(idField));

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
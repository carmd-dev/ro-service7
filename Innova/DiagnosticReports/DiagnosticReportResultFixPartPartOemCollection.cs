using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace Innova.DiagnosticReports
{
    /// <summary>
    /// Typed collection for the corresponding business object
    /// Inherits from <see cref="BusinessObjectCollectionBase"/>.
    /// </summary>
    /// <example>
    /// To create the collection:
    ///
    /// <code>DiagnosticReportResultFixPartPartOemCollection c = new DiagnosticReportResultFixPartPartOemCollection(Registry);</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of DiagnosticReportResultFixPartPartOemCollection, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectCollectionBase.ConnectionString"/> to the
    /// <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable()]
    public class DiagnosticReportResultFixPartPartOemCollection : BusinessObjectCollectionBase, IEnumerable<DiagnosticReportResultFixPartPartOem>
    {
        #region System Constructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new DiagnosticReportResultFixPartPartOem object.
        /// </summary>
        /// <param name="registry"><see cref="Registry"/> currently being used to store <see cref="BusinessObjectBase"/> objects.</param>
        public DiagnosticReportResultFixPartPartOemCollection(Registry registry) : base(registry)
        {
            businessObjectBaseType = typeof(DiagnosticReportResultFixPartPartOem);
        }

        #endregion System Constructors

        #region Indexer

        /// <summary>
        /// Indexer, used to return the <see cref="DiagnosticReportResultFixPartPartOem"/> located at the index (<see cref="int"/>) position of the list.
        /// </summary>
        public DiagnosticReportResultFixPartPartOem this[int index]
        {
            get
            {
                return (DiagnosticReportResultFixPartPartOem)List[index];
            }
        }

        /// <summary>
        /// Enumerator for the <see cref="DiagnosticReportResultFixPartPartOemCollection"/> collection
        /// </summary>
        /// <returns><see cref="DiagnosticReportResultFixPartPartOem"/></returns>
        public new IEnumerator<DiagnosticReportResultFixPartPartOem> GetEnumerator()
        {
            foreach (DiagnosticReportResultFixPartPartOem o in this.List)
            {
                yield return o;
            }
        }

        /// <summary>
        /// Indexer, used to return the <see cref="DiagnosticReportResultFixPartPartOem"/> located for the <see cref="string"/> Id supplied
        /// </summary>
        public DiagnosticReportResultFixPartPartOem this[string id]
        {
            get
            {
                if (!String.IsNullOrEmpty(id))
                {
                    return this[new Guid(id)];
                }
                return null;
            }
        }

        /// <summary>
        /// Indexer, used to return the <see cref="DiagnosticReportResultFixPartPartOem"/> located for the <see cref="Guid"/> Id supplied
        /// </summary>
        public DiagnosticReportResultFixPartPartOem this[Guid id]
        {
            get
            {
                return (DiagnosticReportResultFixPartPartOem)this.FindByProperty("Id", id);
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
        /// Adds an <see cref="DiagnosticReportResultFixPartPartOem"/> object to the list.
        /// </summary>
        /// <param name="value"><see cref="DiagnosticReportResultFixPartPartOem"/> to add.</param>
        public void Add(DiagnosticReportResultFixPartPartOem value)
        {
            base.Add(value);
        }

        /// <summary>
        /// Removes an <see cref="DiagnosticReportResultFixPartPartOem"/> object from the list.
        /// </summary>
        /// <param name="value"><see cref="DiagnosticReportResultFixPartPartOem"/> to remove.</param>
        public void Remove(DiagnosticReportResultFixPartPartOem value)
        {
            base.Remove(value);
        }

        /// <summary>
        /// Inserts an <see cref="DiagnosticReportResultFixPartPartOem"/> object to the list.
        /// </summary>
        /// <param name="index"><see cref="int"/> index position inserting at</param>
        /// <param name="value"><see cref="DiagnosticReportResultFixPartPartOem"/> to add</param>
        public void Insert(int index, DiagnosticReportResultFixPartPartOem value)
        {
            base.Insert(index, value);
        }

        /// <summary>
        /// Creates the <see cref="DiagnosticReportResultFixPartPartOem"/> object from the specified recordset supplied and optionally sets the properties of the object, then adds it to the list.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> used.</param>
        /// <param name="idField"><see cref="string"/> The field name of the Guid that represents the objectId / primary key for the object.</param>
        /// <param name="isSetProperties"><see cref="bool"/> flag indicating whether or not to set the properties of the object.</param>
        /// <param name="isSetPropertiesOfBase"><see cref="bool"/> flag indicating whether or not to set the properties of bases of the object (if any).</param>
        /// <returns><see cref="BusinessObjectBase"/> created</returns>
        protected override BusinessObjectBase CreateBusinessObjectAndAddToListFromRecordset(SqlDataReaderWrapper dr, string idField, bool isSetProperties, bool isSetPropertiesOfBase)
        {
            DiagnosticReportResultFixPartPartOem obj = (DiagnosticReportResultFixPartPartOem)Registry.CreateInstance(typeof(DiagnosticReportResultFixPartPartOem), dr.GetGuid(idField));

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
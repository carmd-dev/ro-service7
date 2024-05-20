using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace Innova.DtcCodes
{
    /// <summary>
    /// Typed collection for the corresponding business object
    /// Inherits from <see cref="BusinessObjectCollectionBase"/>.
    /// </summary>
    /// <example>
    /// To create the collection:
    ///
    /// <code>DtcCodeLaymanTermCollection c = new DtcCodeLaymanTermCollection(Registry);</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of DtcCodeLaymanTermCollection, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectCollectionBase.ConnectionString"/> to the
    /// <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable()]
    public class DtcCodeLaymanTermCollection : BusinessObjectCollectionBase, IEnumerable<DtcCodeLaymanTerm>
    {
        #region System Constructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new DtcCodeLaymanTerm object.
        /// </summary>
        /// <param name="registry"><see cref="Registry"/> currently being used to store <see cref="BusinessObjectBase"/> objects.</param>
        public DtcCodeLaymanTermCollection(Registry registry)
            : base(registry)
        {
            businessObjectBaseType = typeof(DtcCodeLaymanTerm);
        }

        #endregion System Constructors



        #region Indexer

        /// <summary>
        /// Indexer, used to return the <see cref="DtcCodeLaymanTerm"/> located at the index (<see cref="int"/>) position of the list.
        /// </summary>
        public DtcCodeLaymanTerm this[int index]
        {
            get
            {
                return (DtcCodeLaymanTerm)List[index];
            }
        }

        /// <summary>
        /// Enumerator for the <see cref="DtcCodeLaymanTermCollection"/> collection
        /// </summary>
        /// <returns><see cref="DtcCodeLaymanTerm"/></returns>
        public new IEnumerator<DtcCodeLaymanTerm> GetEnumerator()
        {
            foreach (DtcCodeLaymanTerm o in this.List)
            {
                yield return o;
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
        /// Adds an <see cref="DtcCodeLaymanTerm"/> object to the list.
        /// </summary>
        /// <param name="value"><see cref="DtcCodeLaymanTerm"/> to add.</param>
        public void Add(DtcCodeLaymanTerm value)
        {
            base.Add(value);
        }

        /// <summary>
        /// Removes an <see cref="DtcCodeLaymanTerm"/> object from the list.
        /// </summary>
        /// <param name="value"><see cref="DtcCodeLaymanTerm"/> to remove.</param>
        public void Remove(DtcCodeLaymanTerm value)
        {
            base.Remove(value);
        }

        /// <summary>
        /// Inserts an <see cref="DtcCodeLaymanTerm"/> object to the list.
        /// </summary>
        /// <param name="index"><see cref="int"/> index position inserting at</param>
        /// <param name="value"><see cref="DtcCodeLaymanTerm"/> to add</param>
        public void Insert(int index, DtcCodeLaymanTerm value)
        {
            base.Insert(index, value);
        }

        /// <summary>
        /// Creates the <see cref="DtcCodeLaymanTerm"/> object from the specified recordset supplied and optionally sets the properties of the object, then adds it to the list.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> used.</param>
        /// <param name="idField"><see cref="string"/> The field name of the Guid that represents the objectId / primary key for the object.</param>
        /// <param name="isSetProperties"><see cref="bool"/> flag indicating whether or not to set the properties of the object.</param>
        /// <param name="isSetPropertiesOfBase"><see cref="bool"/> flag indicating whether or not to set the properties of bases of the object (if any).</param>
        /// <returns><see cref="BusinessObjectBase"/> created</returns>
        protected override BusinessObjectBase CreateBusinessObjectAndAddToListFromRecordset(SqlDataReaderWrapper dr, string idField, bool isSetProperties, bool isSetPropertiesOfBase)
        {
            DtcCodeLaymanTerm obj = (DtcCodeLaymanTerm)Registry.CreateInstance(typeof(DtcCodeLaymanTerm), dr.GetGuid(idField));

            //if the base properties are set, then set the type of base
            if (isSetProperties)
            {
                obj.LoadPropertiesFromDataReader(dr, isSetPropertiesOfBase);
            }

            this.Add(obj);

            return obj;
        }

        #endregion Default System Collection Methods

        #region Load By Options

        /// <summary>
        /// Loads the current collection from the supplied comma separated id list or XmlGuidList to load from.
        /// </summary>
        /// <param name="xmlGuidListOrCommaSeparatedList"><see cref="string"/> comma separated id list or XmlGuidList to load from.</param>
        public void LoadByXmlGuidList(string xmlGuidListOrCommaSeparatedList)
        {
            SqlProcedureCommand call = new SqlProcedureCommand();
            call.ProcedureName = "DtcCodeLaymanTerm_LoadByXmlGuidList";
            call.AddXmlGuidList("XmlGuidList", xmlGuidListOrCommaSeparatedList);

            this.Load(call, "DtcCodeLaymanTermId", true, true);
        }

        /// <summary>
        /// Loads the current collection of all records in the system.
        /// </summary>
        public void LoadAll()
        {
            SqlProcedureCommand call = new SqlProcedureCommand();
            call.ProcedureName = "DTCCodeLaymanTerm_LoadAll";

            this.Load(call, "DtcCodeLaymanTermId", true, true);
        }

        #endregion Load By Options
    }
}
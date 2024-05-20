using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace Innova.Parts
{
    /// <summary>
    /// Typed collection for the corresponding business object
    /// Inherits from <see cref="BusinessObjectCollectionBase"/>.
    /// </summary>
    /// <example>
    /// To create the collection:
    ///
    /// <code>PartOEMCollection c = new PartOEMCollection(Registry);</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of PartOEMCollection, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectCollectionBase.ConnectionString"/> to the
    /// <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable()]
    public class PartOEMCollection : BusinessObjectCollectionBase, IEnumerable<PartOEM>
    {
        #region System Constructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new PartOEM object.
        /// </summary>
        /// <param name="registry"><see cref="Registry"/> currently being used to store <see cref="BusinessObjectBase"/> objects.</param>
        public PartOEMCollection(Registry registry) : base(registry)
        {
            businessObjectBaseType = typeof(PartOEM);
        }

        #endregion System Constructors



        #region Indexer

        /// <summary>
        /// Indexer, used to return the <see cref="PartOEM"/> located at the index (<see cref="int"/>) position of the list.
        /// </summary>
        public PartOEM this[int index]
        {
            get
            {
                return (PartOEM)List[index];
            }
        }

        /// <summary>
        /// Enumerator for the <see cref="PartOEMCollection"/> collection
        /// </summary>
        /// <returns><see cref="PartOEM"/></returns>
        public new IEnumerator<PartOEM> GetEnumerator()
        {
            foreach (PartOEM o in this.List)
            {
                yield return o;
            }
        }

        /// <summary>
        /// Indexer, used to return the <see cref="PartOEM"/> located for the <see cref="string"/> Id supplied
        /// </summary>
        public PartOEM this[string id]
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
        /// Indexer, used to return the <see cref="PartOEM"/> located for the <see cref="Guid"/> Id supplied
        /// </summary>
        public PartOEM this[Guid id]
        {
            get
            {
                return (PartOEM)this.FindByProperty("Id", id);
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
        /// Adds an <see cref="PartOEM"/> object to the list.
        /// </summary>
        /// <param name="value"><see cref="PartOEM"/> to add.</param>
        public void Add(PartOEM value)
        {
            base.Add(value);
        }

        /// <summary>
        /// Removes an <see cref="PartOEM"/> object from the list.
        /// </summary>
        /// <param name="value"><see cref="PartOEM"/> to remove.</param>
        public void Remove(PartOEM value)
        {
            base.Remove(value);
        }

        /// <summary>
        /// Inserts an <see cref="PartOEM"/> object to the list.
        /// </summary>
        /// <param name="index"><see cref="int"/> index position inserting at</param>
        /// <param name="value"><see cref="PartOEM"/> to add</param>
        public void Insert(int index, PartOEM value)
        {
            base.Insert(index, value);
        }

        /// <summary>
        /// Creates the <see cref="PartOEM"/> object from the specified recordset supplied and optionally sets the properties of the object, then adds it to the list.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> used.</param>
        /// <param name="idField"><see cref="string"/> The field name of the Guid that represents the objectId / primary key for the object.</param>
        /// <param name="isSetProperties"><see cref="bool"/> flag indicating whether or not to set the properties of the object.</param>
        /// <param name="isSetPropertiesOfBase"><see cref="bool"/> flag indicating whether or not to set the properties of bases of the object (if any).</param>
        /// <returns><see cref="BusinessObjectBase"/> created</returns>
        protected override BusinessObjectBase CreateBusinessObjectAndAddToListFromRecordset(SqlDataReaderWrapper dr, string idField, bool isSetProperties, bool isSetPropertiesOfBase)
        {
            PartOEM obj = (PartOEM)Registry.CreateInstance(typeof(PartOEM), dr.GetGuid(idField));

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
            call.ProcedureName = "PartOEM_LoadByXmlGuidList";
            call.AddXmlGuidList("XmlGuidList", xmlGuidListOrCommaSeparatedList);

            this.Load(call, "PartOEMId", true, true);
        }

        public void LoadByAcesIds(int acesBaseVehicleId, int acesEnginebaseId, int acesPartTypeId)
        {
            SqlProcedureCommand call = new SqlProcedureCommand();

            call.ProcedureName = "PartOEM_LoadByAcesIds";
            call.AddInt32("AcesBaseVehicleId", acesBaseVehicleId);
            call.AddInt32("AcesEngineBaseId", acesEnginebaseId);
            call.AddInt32("AcesPartTypeId", acesPartTypeId);

            this.Load(call, "PartOEMId", true, true);
        }

        /// <summary>
        /// LoadByAcesIdsAndVehicleInfo
        /// </summary>
        /// <param name="acesBaseVehicleId"></param>
        /// <param name="acesEnginebaseId"></param>
        /// <param name="acesPartTypeId"></param>
        /// <param name="year"></param>
        /// <param name="make"></param>
        /// <param name="model"></param>
        /// <param name="engineType"></param>
        public void LoadByAcesIdsAndVehicleInfo(int acesBaseVehicleId, int acesEnginebaseId, int acesPartTypeId
            , int year, string make, string model, string engineType)
        {
            SqlProcedureCommand call = new SqlProcedureCommand();

            call.ProcedureName = "PartOEM_LoadByAcesIdsAndVehicleInfo";
            call.AddInt32("AcesBaseVehicleId", acesBaseVehicleId);
            call.AddInt32("AcesEngineBaseId", acesEnginebaseId);
            call.AddInt32("AcesPartTypeId", acesPartTypeId);
            call.AddInt32("Year", year);
            call.AddNVarChar("Make", make);
            call.AddNVarChar("Model", model);
            call.AddNVarChar("EngineType", engineType);

            this.Load(call, "PartOEMId", true, true);
        }

        #endregion Load By Options
    }
}
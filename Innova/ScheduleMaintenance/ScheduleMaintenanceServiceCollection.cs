using Innova.Fixes;
using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using System;

namespace Innova.ScheduleMaintenance
{
    /// <summary>
    /// Typed collection for the corresponding business object
    /// (Remove the word "Collection" from ScheduleMaintenanceServiceCollection).
    /// Inherits from <see cref="BusinessObjectCollectionBase"/>.
    /// </summary>
    /// <example>
    /// To create the collection:
    ///
    /// <code>ScheduleMaintenanceServiceCollection c = new ScheduleMaintenanceServiceCollection(Registry);</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of ScheduleMaintenanceServiceCollection, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectCollectionBase.ConnectionString"/> to the
    /// <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable()]
    public class ScheduleMaintenanceServiceCollection : BusinessObjectCollectionBase
    {
        #region System Constructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new ScheduleMaintenanceServiceCollection object.
        /// </summary>
        /// <param name="registry"><see cref="Registry"/> currently being used to store <see cref="BusinessObjectBase"/> objects.</param>
        public ScheduleMaintenanceServiceCollection(Registry registry) : base(registry)
        {
            businessObjectBaseType = typeof(ScheduleMaintenanceService);
        }

        #endregion System Constructors

        #region Relation Properties

        /// <summary>
        /// Gets the <see cref="BusinessObjectCollectionRelationDefinition"/> for loading the <see cref="ScheduleMaintenanceService.FixName"/> property.
        /// </summary>
        public BusinessObjectCollectionRelationDefinition RelationFixName
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
        /// Indexer, used to return the <see cref="ScheduleMaintenanceService"/> located at the index (<see cref="int"/>) position of the list.
        /// </summary>
        public ScheduleMaintenanceService this[int index]
        {
            get
            {
                return (ScheduleMaintenanceService)List[index];
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
        /// Adds an <see cref="ScheduleMaintenanceService"/> object to the list.
        /// </summary>
        /// <param name="value"><see cref="ScheduleMaintenanceService"/> to add.</param>
        public void Add(ScheduleMaintenanceService value)
        {
            base.Add(value);
        }

        /// <summary>
        /// Removes an <see cref="ScheduleMaintenanceService"/> object from the list.
        /// </summary>
        /// <param name="value"><see cref="ScheduleMaintenanceService"/> to remove.</param>
        public void Remove(ScheduleMaintenanceService value)
        {
            base.Remove(value);
        }

        /// <summary>
        /// Inserts an <see cref="ScheduleMaintenanceService"/> object to the list.
        /// </summary>
        /// <param name="index"><see cref="int"/> index position inserting at</param>
        /// <param name="value"><see cref="ScheduleMaintenanceService"/> to add</param>
        public void Insert(int index, ScheduleMaintenanceService value)
        {
            base.Insert(index, value);
        }

        /// <summary>
        /// Creates the <see cref="ScheduleMaintenanceService"/> object from the specified recordset supplied and optionally sets the properties of the object, then adds it to the list.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> used.</param>
        /// <param name="idField"><see cref="string"/> The field name of the Guid that represents the objectId / primary key for the object.</param>
        /// <param name="isSetProperties"><see cref="bool"/> flag indicating whether or not to set the properties of the object.</param>
        /// <param name="isSetPropertiesOfBase"><see cref="bool"/> flag indicating whether or not to set the properties of bases of the object (if any).</param>
        /// <returns><see cref="BusinessObjectBase"/> created</returns>
        protected override BusinessObjectBase CreateBusinessObjectAndAddToListFromRecordset(SqlDataReaderWrapper dr, string idField, bool isSetProperties, bool isSetPropertiesOfBase)
        {
            ScheduleMaintenanceService obj = (ScheduleMaintenanceService)Registry.CreateInstance(typeof(ScheduleMaintenanceService), dr.GetGuid(idField));

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
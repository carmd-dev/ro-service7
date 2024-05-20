using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using System;

namespace Innova.Vehicles
{
    /// <summary>
    /// Typed collection for the corresponding business object
    /// (Remove the word "Collection" from VehicleTypeCodeCollection).
    /// Inherits from <see cref="BusinessObjectCollectionBase"/>.
    /// </summary>
    /// <example>
    /// To create the collection:
    ///
    /// <code>VehicleTypeCodeCollection c = new VehicleTypeCodeCollection(Registry);</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of VehicleTypeCodeCollection, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectCollectionBase.ConnectionString"/> to the
    /// <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable()]
    public class VehicleTypeCodeCollection : BusinessObjectCollectionBase
    {
        #region System Constructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new VehicleTypeCodeCollection object.
        /// </summary>
        /// <param name="registry"><see cref="Registry"/> currently being used to store <see cref="BusinessObjectBase"/> objects.</param>
        public VehicleTypeCodeCollection(Registry registry) : base(registry)
        {
            businessObjectBaseType = typeof(VehicleTypeCode);
        }

        #endregion System Constructors

        #region Relation Properties

        /// <summary>
        /// Gets the <see cref="BusinessObjectCollectionRelationDefinition"/> for loading the <see cref="VehicleTypeCode.RelationVehicleTypeCodeAssignments"/> properties.
        /// </summary>
        public BusinessObjectCollectionRelationDefinition RelationVehicleTypeCodeAssignments
        {
            get
            {
                SqlProcedureCommand call = new SqlProcedureCommand();
                call.ProcedureName = "VehicleTypeCodeAssignment_LoadByVehicleTypeCodeXmlGuidList";
                return new BusinessObjectCollectionRelationDefinition(typeof(VehicleTypeCodeAssignment), "Id", "XmlGuidList", call, "VehicleTypeCodeAssignmentId", true, typeof(VehicleTypeCodeAssignmentCollection), "", "vehicleTypeCodeAssignments", "VehicleTypeCodeId", typeof(VehicleTypeCode));
            }
        }

        #endregion Relation Properties

        #region Indexer

        /// <summary>
        /// Indexer, used to return the <see cref="VehicleTypeCode"/> located at the index (<see cref="int"/>) position of the list.
        /// </summary>
        public VehicleTypeCode this[int index]
        {
            get
            {
                return (VehicleTypeCode)List[index];
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
        /// Adds an <see cref="VehicleTypeCode"/> object to the list.
        /// </summary>
        /// <param name="value"><see cref="VehicleTypeCode"/> to add.</param>
        public void Add(VehicleTypeCode value)
        {
            base.Add(value);
        }

        /// <summary>
        /// Removes an <see cref="VehicleTypeCode"/> object from the list.
        /// </summary>
        /// <param name="value"><see cref="VehicleTypeCode"/> to remove.</param>
        public void Remove(VehicleTypeCode value)
        {
            base.Remove(value);
        }

        /// <summary>
        /// Inserts an <see cref="VehicleTypeCode"/> object to the list.
        /// </summary>
        /// <param name="index"><see cref="int"/> index position inserting at</param>
        /// <param name="value"><see cref="VehicleTypeCode"/> to add</param>
        public void Insert(int index, VehicleTypeCode value)
        {
            base.Insert(index, value);
        }

        /// <summary>
        /// Creates the <see cref="VehicleTypeCode"/> object from the specified recordset supplied and optionally sets the properties of the object, then adds it to the list.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> used.</param>
        /// <param name="idField"><see cref="string"/> The field name of the Guid that represents the objectId / primary key for the object.</param>
        /// <param name="isSetProperties"><see cref="bool"/> flag indicating whether or not to set the properties of the object.</param>
        /// <param name="isSetPropertiesOfBase"><see cref="bool"/> flag indicating whether or not to set the properties of bases of the object (if any).</param>
        /// <returns><see cref="BusinessObjectBase"/> created</returns>
        protected override BusinessObjectBase CreateBusinessObjectAndAddToListFromRecordset(SqlDataReaderWrapper dr, string idField, bool isSetProperties, bool isSetPropertiesOfBase)
        {
            VehicleTypeCode obj = (VehicleTypeCode)Registry.CreateInstance(typeof(VehicleTypeCode), dr.GetGuid(idField));

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
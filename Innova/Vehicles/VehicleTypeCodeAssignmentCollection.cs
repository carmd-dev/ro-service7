using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using System;

namespace Innova.Vehicles
{
    /// <summary>
    /// Typed collection for the corresponding business object
    /// (Remove the word "Collection" from VehicleTypeCodeAssignmentCollection).
    /// Inherits from <see cref="BusinessObjectCollectionBase"/>.
    /// </summary>
    /// <example>
    /// To create the collection:
    ///
    /// <code>VehicleTypeCodeAssignmentCollection c = new VehicleTypeCodeAssignmentCollection(Registry);</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of VehicleTypeCodeAssignmentCollection, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectCollectionBase.ConnectionString"/> to the
    /// <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable()]
    public class VehicleTypeCodeAssignmentCollection : BusinessObjectCollectionBase
    {
        #region System Constructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new VehicleTypeCodeAssignmentCollection object.
        /// </summary>
        /// <param name="registry"><see cref="Registry"/> currently being used to store <see cref="BusinessObjectBase"/> objects.</param>
        public VehicleTypeCodeAssignmentCollection(Registry registry) : base(registry)
        {
            businessObjectBaseType = typeof(VehicleTypeCodeAssignment);
        }

        #endregion System Constructors

        #region Relation Properties

        /// <summary>
        /// Gets the <see cref="BusinessObjectCollectionRelationDefinition"/> for loading the <see cref="VehicleTypeCodeAssignment.VehicleTypeCode"/> properties.
        /// </summary>
        public BusinessObjectCollectionRelationDefinition RelationVehicleTypeCodes
        {
            get
            {
                SqlProcedureCommand call = new SqlProcedureCommand();
                call.ProcedureName = "VehicleTypeCode_LoadByXmlGuidList";
                return new BusinessObjectCollectionRelationDefinition(typeof(VehicleTypeCode), "VehicleTypeCode.Id", "XmlGuidList", call, "VehicleTypeCodeId", true, true);
            }
        }

        /// <summary>
        /// Gets the <see cref="BusinessObjectCollectionRelationDefinition"/> for loading the <see cref="VehicleTypeCodeAssignment.VehicleType"/> properties.
        /// </summary>
        public BusinessObjectCollectionRelationDefinition RelationVehicleTypes
        {
            get
            {
                SqlProcedureCommand call = new SqlProcedureCommand();
                call.ProcedureName = "VehicleType_LoadByXmlGuidList";
                return new BusinessObjectCollectionRelationDefinition(typeof(VehicleType), "VehicleType.Id", "XmlGuidList", call, "VehicleTypeId", true, true);
            }
        }

        #endregion Relation Properties

        #region Indexer

        /// <summary>
        /// Indexer, used to return the <see cref="VehicleTypeCodeAssignment"/> located at the index (<see cref="int"/>) position of the list.
        /// </summary>
        public VehicleTypeCodeAssignment this[int index]
        {
            get
            {
                return (VehicleTypeCodeAssignment)List[index];
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
        /// Adds an <see cref="VehicleTypeCodeAssignment"/> object to the list.
        /// </summary>
        /// <param name="value"><see cref="VehicleTypeCodeAssignment"/> to add.</param>
        public void Add(VehicleTypeCodeAssignment value)
        {
            base.Add(value);
        }

        /// <summary>
        /// Removes an <see cref="VehicleTypeCodeAssignment"/> object from the list.
        /// </summary>
        /// <param name="value"><see cref="VehicleTypeCodeAssignment"/> to remove.</param>
        public void Remove(VehicleTypeCodeAssignment value)
        {
            base.Remove(value);
        }

        /// <summary>
        /// Inserts an <see cref="VehicleTypeCodeAssignment"/> object to the list.
        /// </summary>
        /// <param name="index"><see cref="int"/> index position inserting at</param>
        /// <param name="value"><see cref="VehicleTypeCodeAssignment"/> to add</param>
        public void Insert(int index, VehicleTypeCodeAssignment value)
        {
            base.Insert(index, value);
        }

        /// <summary>
        /// Creates the <see cref="VehicleTypeCodeAssignment"/> object from the specified recordset supplied and optionally sets the properties of the object, then adds it to the list.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> used.</param>
        /// <param name="idField"><see cref="string"/> The field name of the Guid that represents the objectId / primary key for the object.</param>
        /// <param name="isSetProperties"><see cref="bool"/> flag indicating whether or not to set the properties of the object.</param>
        /// <param name="isSetPropertiesOfBase"><see cref="bool"/> flag indicating whether or not to set the properties of bases of the object (if any).</param>
        /// <returns><see cref="BusinessObjectBase"/> created</returns>
        protected override BusinessObjectBase CreateBusinessObjectAndAddToListFromRecordset(SqlDataReaderWrapper dr, string idField, bool isSetProperties, bool isSetPropertiesOfBase)
        {
            VehicleTypeCodeAssignment obj = (VehicleTypeCodeAssignment)Registry.CreateInstance(typeof(VehicleTypeCodeAssignment), dr.GetGuid(idField));

            //if the base properties are set, then set the type of base
            if (isSetProperties)
            {
                obj.LoadPropertiesFromDataReader(dr, true);
            }

            this.Add(obj);

            return obj;
        }

        #endregion Default System Collection Methods

        public string GetUniqueErrorCodesDelimittedString()
        {
            string errorCodes = "";

            foreach (VehicleTypeCodeAssignment vtca in this.List)
            {
                if (errorCodes.IndexOf(vtca.ErrorCode) < 0)
                {
                    if (errorCodes != "")
                    {
                        errorCodes += ",";
                    }
                    errorCodes += vtca.ErrorCode;
                }
            }

            return errorCodes;
        }
    }
}
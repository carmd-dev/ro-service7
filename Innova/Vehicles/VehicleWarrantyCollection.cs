using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using System;

namespace Innova.Vehicles
{
    /// <summary>
    /// Typed collection for the corresponding business object
    /// Inherits from <see cref="BusinessObjectCollectionBase"/>.
    /// </summary>
    /// <example>
    /// To create the collection:
    ///
    /// <code>VehicleWarrantyCollection c = new VehicleWarrantyCollection(Registry);</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of VehicleWarrantyCollection, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectCollectionBase.ConnectionString"/> to the
    /// <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable()]
    public class VehicleWarrantyCollection : BusinessObjectCollectionBase
    {
        #region System Constructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new VehicleWarranty object.
        /// </summary>
        /// <param name="registry"><see cref="Registry"/> currently being used to store <see cref="BusinessObjectBase"/> objects.</param>
        public VehicleWarrantyCollection(Registry registry)
            : base(registry)
        {
            businessObjectBaseType = typeof(VehicleWarranty);
        }

        #endregion System Constructors



        #region Indexer

        /// <summary>
        /// Indexer, used to return the <see cref="VehicleWarranty"/> located at the index (<see cref="int"/>) position of the list.
        /// </summary>
        public VehicleWarranty this[int index]
        {
            get
            {
                return (VehicleWarranty)List[index];
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
        /// Adds an <see cref="VehicleWarranty"/> object to the list.
        /// </summary>
        /// <param name="value"><see cref="VehicleWarranty"/> to add.</param>
        public void Add(VehicleWarranty value)
        {
            base.Add(value);
        }

        /// <summary>
        /// Removes an <see cref="VehicleWarranty"/> object from the list.
        /// </summary>
        /// <param name="value"><see cref="VehicleWarranty"/> to remove.</param>
        public void Remove(VehicleWarranty value)
        {
            base.Remove(value);
        }

        /// <summary>
        /// Inserts an <see cref="VehicleWarranty"/> object to the list.
        /// </summary>
        /// <param name="index"><see cref="int"/> index position inserting at</param>
        /// <param name="value"><see cref="VehicleWarranty"/> to add</param>
        public void Insert(int index, VehicleWarranty value)
        {
            base.Insert(index, value);
        }

        /// <summary>
        /// Creates the <see cref="VehicleWarranty"/> object from the specified recordset supplied and optionally sets the properties of the object, then adds it to the list.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> used.</param>
        /// <param name="idField"><see cref="string"/> The field name of the Guid that represents the objectId / primary key for the object.</param>
        /// <param name="isSetProperties"><see cref="bool"/> flag indicating whether or not to set the properties of the object.</param>
        /// <param name="isSetPropertiesOfBase"><see cref="bool"/> flag indicating whether or not to set the properties of bases of the object (if any).</param>
        /// <returns><see cref="BusinessObjectBase"/> created</returns>
        protected override BusinessObjectBase CreateBusinessObjectAndAddToListFromRecordset(SqlDataReaderWrapper dr, string idField, bool isSetProperties, bool isSetPropertiesOfBase)
        {
            VehicleWarranty obj = (VehicleWarranty)Registry.CreateInstance(typeof(VehicleWarranty), dr.GetGuid(idField));

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
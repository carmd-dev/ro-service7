using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using System;

namespace Innova
{
    /// <summary>
    /// Typed collection for the corresponding business object
    /// (Remove the word "Collection" from CountryCollection).
    /// Inherits from <see cref="BusinessObjectCollectionBase"/>.
    /// </summary>
    /// <example>
    /// To create the collection:
    ///
    /// <code>CountryCollection c = new CountryCollection(Registry);</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of CountryCollection, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectCollectionBase.ConnectionString"/> to the
    /// <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable()]
    public class CountryCollection : BusinessObjectCollectionBase
    {
        #region System Constructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new CountryCollection object.
        /// </summary>
        /// <param name="registry"><see cref="Registry"/> currently being used to store <see cref="BusinessObjectBase"/> objects.</param>
        public CountryCollection(Registry registry) : base(registry)
        {
            businessObjectBaseType = typeof(Country);
        }

        #endregion System Constructors



        #region Indexer

        /// <summary>
        /// Indexer, used to return the <see cref="Country"/> located at the index (<see cref="int"/>) position of the list.
        /// </summary>
        public Country this[int index]
        {
            get
            {
                return (Country)List[index];
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
        /// Adds an <see cref="Country"/> object to the list.
        /// </summary>
        /// <param name="value"><see cref="Country"/> to add.</param>
        public void Add(Country value)
        {
            base.Add(value);
        }

        /// <summary>
        /// Removes an <see cref="Country"/> object from the list.
        /// </summary>
        /// <param name="value"><see cref="Country"/> to remove.</param>
        public void Remove(Country value)
        {
            base.Remove(value);
        }

        /// <summary>
        /// Inserts an <see cref="Country"/> object to the list.
        /// </summary>
        /// <param name="index"><see cref="int"/> index position inserting at</param>
        /// <param name="value"><see cref="Country"/> to add</param>
        public void Insert(int index, Country value)
        {
            base.Insert(index, value);
        }

        /// <summary>
        /// Creates the <see cref="Country"/> object from the specified recordset supplied and optionally sets the properties of the object, then adds it to the list.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> used.</param>
        /// <param name="idField"><see cref="string"/> The field name of the Guid that represents the objectId / primary key for the object.</param>
        /// <param name="isSetProperties"><see cref="bool"/> flag indicating whether or not to set the properties of the object.</param>
        /// <param name="isSetPropertiesOfBase"><see cref="bool"/> flag indicating whether or not to set the properties of bases of the object (if any).</param>
        /// <returns><see cref="BusinessObjectBase"/> created</returns>
        protected override BusinessObjectBase CreateBusinessObjectAndAddToListFromRecordset(SqlDataReaderWrapper dr, string idField, bool isSetProperties, bool isSetPropertiesOfBase)
        {
            Country obj = (Country)Registry.CreateInstance(typeof(Country), dr.GetGuid(idField));

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
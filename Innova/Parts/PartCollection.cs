using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using System;

namespace Innova.Parts
{
    /// <summary>
    /// Typed collection for the corresponding business object
    /// (Remove the word "Collection" from PartCollection).
    /// Inherits from <see cref="BusinessObjectCollectionBase"/>.
    /// </summary>
    /// <example>
    /// To create the collection:
    ///
    /// <code>PartCollection c = new PartCollection(Registry);</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of PartCollection, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectCollectionBase.ConnectionString"/> to the
    /// <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable()]
    public class PartCollection : BusinessObjectCollectionBase
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
        public PartCollection(Registry registry) : base(registry)
        {
            businessObjectBaseType = typeof(Part);
        }

        #endregion System Constructors

        #region Relation Properties

        /// <summary>
        /// Gets the <see cref="BusinessObjectCollectionRelationDefinition"/> for loading the <see cref="PartName"/> property.
        /// </summary>
        public BusinessObjectCollectionRelationDefinition RelationPartName
        {
            get
            {
                SqlProcedureCommand call = new SqlProcedureCommand();

                //##Security - Commented out for temporary on 2020-02-28
                //call.AddNVarChar("EncryptionPassphrase", RuntimeInfo.EncryptionPassphrase);
                //##Security

                call.ProcedureName = "PartName_LoadByXmlGuidList";
                return new BusinessObjectCollectionRelationDefinition(typeof(PartName), "PartName.Id", "XmlGuidList", call, "PartNameId", true, true);
            }
        }

        #endregion Relation Properties

        #region Indexer

        /// <summary>
        /// Indexer, used to return the <see cref="Part"/> located at the index (<see cref="int"/>) position of the list.
        /// </summary>
        public Part this[int index]
        {
            get
            {
                return (Part)List[index];
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
        /// <param name="value"><see cref="Part"/> to add.</param>
        public void Add(Part value)
        {
            base.Add(value);
        }

        /// <summary>
        /// Removes an <see cref="Part"/> object from the list.
        /// </summary>
        /// <param name="value"><see cref="Part"/> to remove.</param>
        public void Remove(Part value)
        {
            base.Remove(value);
        }

        /// <summary>
        /// Inserts an <see cref="Part"/> object to the list.
        /// </summary>
        /// <param name="index"><see cref="int"/> index position inserting at</param>
        /// <param name="value"><see cref="Part"/> to add</param>
        public void Insert(int index, Part value)
        {
            base.Insert(index, value);
        }

        /// <summary>
        /// Creates the <see cref="Part"/> object from the specified recordset supplied and optionally sets the properties of the object, then adds it to the list.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> used.</param>
        /// <param name="idField"><see cref="string"/> The field name of the Guid that represents the objectId / primary key for the object.</param>
        /// <param name="isSetProperties"><see cref="bool"/> flag indicating whether or not to set the properties of the object.</param>
        /// <param name="isSetPropertiesOfBase"><see cref="bool"/> flag indicating whether or not to set the properties of bases of the object (if any).</param>
        /// <returns><see cref="BusinessObjectBase"/> created</returns>
        protected override BusinessObjectBase CreateBusinessObjectAndAddToListFromRecordset(SqlDataReaderWrapper dr, string idField, bool isSetProperties, bool isSetPropertiesOfBase)
        {
            Part obj = (Part)Registry.CreateInstance(typeof(Part), dr.GetGuid(idField));

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
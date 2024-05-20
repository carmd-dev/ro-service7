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
    /// <code>PartNameCollection c = new PartNameCollection(Registry);</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of PartNameCollection, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectCollectionBase.ConnectionString"/> to the
    /// <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable()]
    public class PartNameCollection : BusinessObjectCollectionBase
    {
        #region System Constructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new PartName object.
        /// </summary>
        /// <param name="registry"><see cref="Registry"/> currently being used to store <see cref="BusinessObjectBase"/> objects.</param>
        public PartNameCollection(Registry registry)
            : base(registry)
        {
            businessObjectBaseType = typeof(PartName);
        }

        #endregion System Constructors

        #region Indexer

        /// <summary>
        /// Indexer, used to return the <see cref="PartName"/> located at the index (<see cref="int"/>) position of the list.
        /// </summary>
        public PartName this[int index]
        {
            get
            {
                return (PartName)List[index];
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
        /// Adds an <see cref="PartName"/> object to the list.
        /// </summary>
        /// <param name="value"><see cref="PartName"/> to add.</param>
        public void Add(PartName value)
        {
            base.Add(value);
        }

        /// <summary>
        /// Removes an <see cref="PartName"/> object from the list.
        /// </summary>
        /// <param name="value"><see cref="PartName"/> to remove.</param>
        public void Remove(PartName value)
        {
            base.Remove(value);
        }

        /// <summary>
        /// Inserts an <see cref="PartName"/> object to the list.
        /// </summary>
        /// <param name="index"><see cref="int"/> index position inserting at</param>
        /// <param name="value"><see cref="PartName"/> to add</param>
        public void Insert(int index, PartName value)
        {
            base.Insert(index, value);
        }

        /// <summary>
        /// Creates the <see cref="PartName"/> object from the specified recordset supplied and optionally sets the properties of the object, then adds it to the list.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> used.</param>
        /// <param name="idField"><see cref="string"/> The field name of the Guid that represents the objectId / primary key for the object.</param>
        /// <param name="isSetProperties"><see cref="bool"/> flag indicating whether or not to set the properties of the object.</param>
        /// <param name="isSetPropertiesOfBase"><see cref="bool"/> flag indicating whether or not to set the properties of bases of the object (if any).</param>
        /// <returns><see cref="BusinessObjectBase"/> created</returns>
        protected override BusinessObjectBase CreateBusinessObjectAndAddToListFromRecordset(SqlDataReaderWrapper dr, string idField, bool isSetProperties, bool isSetPropertiesOfBase)
        {
            PartName obj = (PartName)Registry.CreateInstance(typeof(PartName), dr.GetGuid(idField));

            //if the base properties are set, then set the type of base
            if (isSetProperties)
            {
                obj.LoadPropertiesFromDataReader(dr, isSetPropertiesOfBase);
            }

            this.Add(obj);

            return obj;
        }

        #endregion Default System Collection Methods

        public bool GetIsValidForMakes(List<string> makes)
        {
            bool isValid = true;

            if (this.Count > 0 && makes.Count > 0)
            {
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(this.Registry.ConnectionStringDefault))
                {
                    dr.ProcedureName = "PartName_GetIsValidForMakes";
                    dr.AddNVarChar("PartNameXmlGuidList", this.ToXmlGuidList());
                    dr.AddNVarChar("MakesXmlList", Metafuse3.Xml.XmlList.ToXml(makes));

                    dr.Execute();
                    dr.Read();
                    isValid = dr.GetBoolean("IsValid");
                }
            }

            return isValid;
        }
    }
}
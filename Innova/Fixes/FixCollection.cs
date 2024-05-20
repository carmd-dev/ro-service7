using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using System;

namespace Innova.Fixes
{
    /// <summary>
    /// Typed collection for the corresponding business object
    /// (Remove the word "Collection" from FixCollection).
    /// Inherits from <see cref="BusinessObjectCollectionBase"/>.
    /// </summary>
    /// <example>
    /// To create the collection:
    ///
    /// <code>FixCollection c = new FixCollection(Registry);</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of FixCollection, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectCollectionBase.ConnectionString"/> to the
    /// <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable()]
    public class FixCollection : BusinessObjectCollectionBase
    {
        #region System Constructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new FixCollection object.
        /// </summary>
        /// <param name="registry"><see cref="Registry"/> currently being used to store <see cref="BusinessObjectBase"/> objects.</param>
        public FixCollection(Registry registry) : base(registry)
        {
            businessObjectBaseType = typeof(Fix);
        }

        #endregion System Constructors

        #region Relation Properties

        /// <summary>
        /// Gets the <see cref="BusinessObjectCollectionRelationDefinition"/> for loading the <see cref="Fix.FixName"/> property.
        /// </summary>
        public BusinessObjectCollectionRelationDefinition RelationFixName
        {
            get
            {
                SqlProcedureCommand call = new SqlProcedureCommand();

                call.ProcedureName = "FixName_LoadByXmlGuidList";

                return new BusinessObjectCollectionRelationDefinition(typeof(FixName), "FixName.Id", "XmlGuidList", call, "FixNameId", true, true);
            }
        }

        /// <summary>
        /// Gets the <see cref="BusinessObjectCollectionRelationDefinition"/> for loading the <see cref="Fix.FixParts"/> property.
        /// </summary>
        public BusinessObjectCollectionRelationDefinition RelationFixParts
        {
            get
            {
                SqlProcedureCommand call = new SqlProcedureCommand();
                call.ProcedureName = "FixPart_LoadByFixXmlGuidList";
                return new BusinessObjectCollectionRelationDefinition(typeof(FixPart), "Id", "XmlGuidList", call, "FixPartId", true, typeof(FixPartCollection), "", "fixParts", "FixId", typeof(Fix));
            }
        }

        /// <summary>
        /// Loads the relation for the Fix DTCs.
        /// </summary>
        public void LoadRelationFixDTCs()
        {
            FixCollection fixes = new FixCollection(this.Registry);
            //lets see the fixes that need to be populated
            foreach (Fix f in this)
            {
                if (f.fixDTCs == null)
                {
                    f.fixDTCs = new SmartCollection();
                    fixes.Add(f);
                }
            }

            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(this.Registry.ConnectionStringDefault))
            {
                dr.ProcedureName = "FixDTC_LoadByFixXmlGuidList";
                dr.AddNVarChar("XmlGuidList", fixes.ToXmlGuidList());

                dr.Execute();

                while (dr.Read())
                {
                    Fix f = (Fix)this.Registry.CreateInstance(typeof(Fix), dr.GetGuid("FixId"));
                    f.FixDTCs.Add(new FixDTC(f, dr.GetString("PrimaryDTC"), dr.GetString("SecondaryDTCList")));
                }
            }
        }

        #endregion Relation Properties

        #region Indexer

        /// <summary>
        /// Indexer, used to return the <see cref="Fix"/> located at the index (<see cref="int"/>) position of the list.
        /// </summary>
        public Fix this[int index]
        {
            get
            {
                return (Fix)List[index];
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
        /// Adds an <see cref="Fix"/> object to the list.
        /// </summary>
        /// <param name="value"><see cref="Fix"/> to add.</param>
        public void Add(Fix value)
        {
            base.Add(value);
        }

        /// <summary>
        /// Removes an <see cref="Fix"/> object from the list.
        /// </summary>
        /// <param name="value"><see cref="Fix"/> to remove.</param>
        public void Remove(Fix value)
        {
            base.Remove(value);
        }

        /// <summary>
        /// Inserts an <see cref="Fix"/> object to the list.
        /// </summary>
        /// <param name="index"><see cref="int"/> index position inserting at</param>
        /// <param name="value"><see cref="Fix"/> to add</param>
        public void Insert(int index, Fix value)
        {
            base.Insert(index, value);
        }

        /// <summary>
        /// Creates the <see cref="Fix"/> object from the specified recordset supplied and optionally sets the properties of the object, then adds it to the list.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> used.</param>
        /// <param name="idField"><see cref="string"/> The field name of the Guid that represents the objectId / primary key for the object.</param>
        /// <param name="isSetProperties"><see cref="bool"/> flag indicating whether or not to set the properties of the object.</param>
        /// <param name="isSetPropertiesOfBase"><see cref="bool"/> flag indicating whether or not to set the properties of bases of the object (if any).</param>
        /// <returns><see cref="BusinessObjectBase"/> created</returns>
        protected override BusinessObjectBase CreateBusinessObjectAndAddToListFromRecordset(SqlDataReaderWrapper dr, string idField, bool isSetProperties, bool isSetPropertiesOfBase)
        {
            Fix obj = (Fix)Registry.CreateInstance(typeof(Fix), dr.GetGuid(idField));

            //if the base properties are set, then set the type of base
            if (isSetProperties)
            {
                obj.LoadPropertiesFromDataReader(dr, isSetPropertiesOfBase);
            }

            this.Add(obj);

            return obj;
        }

        #endregion Default System Collection Methods

        /// <summary>
        /// Sets the IsFromPolkMatch property for all items in the collection.
        /// </summary>
        /// <param name="isFromPolkMatch">The <see cref="bool"/> value to be set.</param>
        public void SetIsFromPolkMatch(bool isFromPolkMatch)
        {
            for (int i = 0; i < this.List.Count; i++)
            {
                Fix f = (Fix)this.List[i];
                f.IsFromPolkMatch = isFromPolkMatch;
            }
        }

        /// <summary>
        /// Sets the IsFromVinPowerMatch property for all items in the collection.
        /// </summary>
        /// <param name="isFromVinPowerMatch">The <see cref="bool"/> value to be set.</param>
        public void SetIsFromVinPowerMatch(bool isFromVinPowerMatch)
        {
            for (int i = 0; i < this.List.Count; i++)
            {
                Fix f = (Fix)this.List[i];
                f.IsFromVinPowerMatch = isFromVinPowerMatch;
            }
        }

        //Added on 09/13/2017 by Nam Lu - INNOVA Dev Team
        /// <summary>
        /// Adds fix items to a collection.
        /// </summary>
        /// <returns></returns>
        public System.Collections.Generic.List<Fix> ToList()
        {
            System.Collections.Generic.List<Fix> list = new System.Collections.Generic.List<Fix>();
            foreach (Fix item in this)
            {
                list.Add(item);
            }
            return list;
        }
    }
}
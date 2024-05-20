using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using System.Collections.Generic;

namespace Innova.DiagnosticReports
{
    /// <summary>
    /// Summary description for ErrorCodeCollection.
    /// </summary>
    public class ProRSFixCollection : BusinessObjectCollectionBase
    {
        /// <summary>
        ///
        /// </summary>
        public ProRSFixCollection(Registry registry) : base(registry)
        {
            businessObjectBaseType = typeof(ProRSFix);
        }

        /// <summary>
        /// Indexer, used to return the <see cref="ProRSFix"/> located at the index (<see cref="int"/>) position of the list.
        /// </summary>
        public new ProRSFix this[int index]
        {
            get
            {
                return (ProRSFix)List[index];
            }
        }

        /// <summary>
        /// Adds an <see cref="ProRSFix"/> object to the list.
        /// </summary>
        /// <param name="value"><see cref="ProRSFix"/> to add.</param>
        public void Add(ProRSFix value)
        {
            base.Add(value);
        }

        /// <summary>
        /// Removes an <see cref="ProRSFix"/> object from the list.
        /// </summary>
        /// <param name="value"><see cref="ProRSFix"/> to remove.</param>
        public void Remove(ProRSFix value)
        {
            base.Remove(value);
        }

        /// <summary>
        /// Inserts an <see cref="ProRSFix"/> object to the list.
        /// </summary>
        /// <param name="index"><see cref="int"/> index position inserting at</param>
        /// <param name="value"><see cref="ProRSFix"/> to add</param>
        public void Insert(int index, ProRSFix value)
        {
            base.Insert(index, value);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        protected override BusinessObjectBase CreateBusinessObjectAndAddToListFromRecordset(SqlDataReaderWrapper dr, string idField, bool isSetProperties, bool isSetPropertiesOfBase)
        {
            ProRSFix obj = (ProRSFix)Registry.CreateInstance(typeof(ProRSFix), dr.GetGuid(idField));

            //if the base properties are set, then set the type of base
            if (isSetProperties)
            {
                obj.LoadPropertiesFromDataReader(dr, isSetPropertiesOfBase);
            }

            this.Add(obj);

            return obj;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
	    public List<ProRSFix> ToList()
        {
            List<ProRSFix> list = new List<ProRSFix>();
            foreach (ProRSFix item in this)
            {
                list.Add(item);
            }
            return list;
        }
    }
}
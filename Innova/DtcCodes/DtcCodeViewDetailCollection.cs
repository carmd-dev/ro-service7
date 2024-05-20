namespace Innova.DtcCodes
{
    /// <summary>
    /// Summary description for DtcCodeViewDetailCollection.
    /// </summary>
    public class DtcCodeViewDetailCollection : Metafuse3.BusinessObjects.SmartCollectionBase
    {
        /// <summary>
        /// Creates a new instance of a DtcCodeViewDetailCollection
        /// </summary>
        public DtcCodeViewDetailCollection()
        {
        }

        /// <summary>
        /// An indexer into the collecton.
        /// </summary>
        public DtcCodeViewDetail this[int index]
        {
            get
            {
                return (DtcCodeViewDetail)List[index];
            }
        }

        /// <summary>
        /// Adds a DtcCodeViewDetail to the collection.
        /// </summary>
        /// <param name="value">The DtcCodeViewDetail to be added.</param>
        public void Add(DtcCodeViewDetail value)
        {
            base.Add(value);
        }

        /// <summary>
        /// Removes a DtcCodeViewDetail from the collection.
        /// </summary>
        /// <param name="value">The DtcCodeViewDetail to be removed.</param>
        public void Remove(DtcCodeViewDetail value)
        {
            base.Remove(value);
        }

        /// <summary>
        /// Inserts a DtcCodeViewDetail into the collection.
        /// </summary>
        /// <param name="index">The location in the collection which to insert the value.</param>
        /// <param name="value">The value to be inserted.</param>
        public void Insert(int index, DtcCodeViewDetail value)
        {
            base.Insert(index, value);
        }

        /// <summary>
        /// Clears the contents of the collection.
        /// </summary>
        public override void Clear()
        {
            base.Clear();
        }
    }
}
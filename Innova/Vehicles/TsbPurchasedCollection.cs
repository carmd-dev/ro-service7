namespace Innova.Vehicles
{
    /// <summary>
    /// Summary description for TsbPurchasedCollection.
    /// </summary>
    public class TsbPurchasedCollection : Metafuse3.BusinessObjects.SmartCollectionBase
    {
        /// <summary>
        /// Public constructor.
        /// </summary>
        public TsbPurchasedCollection()
        {
        }

        /// <summary>
        /// An indexer into the collecton.
        /// </summary>
        public TsbPurchased this[int index]
        {
            get
            {
                return (TsbPurchased)List[index];
            }
        }

        /// <summary>
        /// Adds a TsbPurchased to the collection.
        /// </summary>
        /// <param name="value">The ValidAddress to be added.</param>
        public void Add(TsbPurchased value)
        {
            base.Add(value);
        }

        /// <summary>
        /// Removes a TsbPurchased from the collection.
        /// </summary>
        /// <param name="value">The TsbPurchased to be removed.</param>
        public void Remove(TsbPurchased value)
        {
            base.Remove(value);
        }

        /// <summary>
        /// Inserts a TsbPurchased into the collection.
        /// </summary>
        /// <param name="index">The location in the collection which to insert the value.</param>
        /// <param name="value">The value to be inserted.</param>
        public void Insert(int index, TsbPurchased value)
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
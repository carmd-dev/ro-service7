namespace Innova.Vehicles
{
    /// <summary>
    /// Summary description for RecallCollection.
    /// </summary>
    public class RecallCollection : Metafuse3.BusinessObjects.SmartCollectionBase
    {
        /// <summary>
        /// Creates a new instance of a RecallCollection
        /// </summary>
        public RecallCollection()
        {
        }

        /// <summary>
        /// An indexer into the collecton.
        /// </summary>
        public Recall this[int index]
        {
            get
            {
                return (Recall)List[index];
            }
        }

        /// <summary>
        /// Adds a Recall to the collection.
        /// </summary>
        /// <param name="value">The ValidAddress to be added.</param>
        public void Add(Recall value)
        {
            base.Add(value);
        }

        /// <summary>
        /// Removes a Recall from the collection.
        /// </summary>
        /// <param name="value">The ValidAddress to be removed.</param>
        public void Remove(Recall value)
        {
            base.Remove(value);
        }

        /// <summary>
        /// Inserts a Recall into the collection.
        /// </summary>
        /// <param name="index">The location in the collection which to insert the value.</param>
        /// <param name="value">The value to be inserted.</param>
        public void Insert(int index, Recall value)
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
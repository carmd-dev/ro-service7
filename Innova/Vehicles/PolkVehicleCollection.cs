namespace Innova.Vehicles
{
    /// <summary>
    /// The PolkVehicleCollection class handles the business logic and data access for the PolkVehicleCollection object.
    /// </summary>
    public class PolkVehicleCollection : Metafuse3.BusinessObjects.SmartCollectionBase
    {
        /// <summary>
        /// Creates a new instance of a PolkVehicleCollection
        /// </summary>
        public PolkVehicleCollection()
        {
        }

        /// <summary>
        /// An indexer into the collecton.
        /// </summary>
        public PolkVehicle this[int index]
        {
            get
            {
                return (PolkVehicle)List[index];
            }
        }

        /// <summary>
        /// Adds a PolkVehicle to the collection.
        /// </summary>
        /// <param name="value">The PolkVehicle to be added.</param>
        public void Add(PolkVehicle value)
        {
            base.Add(value);
        }

        /// <summary>
        /// Removes a PolkVehicle from the collection.
        /// </summary>
        /// <param name="value">The PolkVehicle to be removed.</param>
        public void Remove(PolkVehicle value)
        {
            base.Remove(value);
        }

        /// <summary>
        /// Inserts a PolkVehicle into the collection.
        /// </summary>
        /// <param name="index">The location in the collection which to insert the value.</param>
        /// <param name="value">The value to be inserted.</param>
        public void Insert(int index, PolkVehicle value)
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
namespace Innova.Vehicles
{
    /// <summary>
    /// The TsbCategory class handles the business logic and data access for the TsbCategory object.
    /// </summary>
    public class TsbCategory
    {
        private int id;
        private string description;
        private int? count;

        /// <summary>
        /// Constructor used to input the TSB category and the count of TSBs for that category
        /// </summary>
        /// <param name="id"><see cref="int"/> id</param>
        /// <param name="description"><see cref="string"/> description</param>
        /// <param name="count"><see cref="int"/> count of TSBs</param>
        public TsbCategory(int id, string description, int? count)
        {
            this.id = id;
            this.description = description;
            this.count = count;
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/> ID.
        /// </summary>
        public int Id
        { get { return this.id; } set { this.id = value; } }

        /// <summary>
        /// Gets or sets the <see cref="string"/> description.
        /// </summary>
        public string Description
        { get { return this.description; } set { this.description = value; } }

        /// <summary>
        /// Gets or sets the <see cref="int"/> count.
        /// </summary>
        public int? Count
        { get { return this.count; } set { this.count = value; } }
    }
}
namespace Innova.Vehicles
{
    /// <summary>
    /// The TsbType class handles the business logic and data access for the TsbType object.
    /// </summary>
    public class TsbType
    {
        private int id;
        private string description;

        /// <summary>
        /// The public constructor for the <see cref="TsbType"/> class.
        /// </summary>
        /// <param name="id">The <see cref="int"/> id.</param>
        /// <param name="description">The <see cref="string"/> description.</param>
        public TsbType(int id, string description)
        {
            this.id = id;
            this.description = description;
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
        {
            get
            {
                if (this.description.ToLower() == "dealernotification")
                {
                    this.description = "Dealer Notification";
                }
                else if (this.description.ToLower() == "firstresponder")
                {
                    this.description = "First Responder";
                }

                return this.description;
            }
            set
            {
                this.description = value;
            }
        }
    }
}
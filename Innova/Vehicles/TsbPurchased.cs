using System;

namespace Innova.Vehicles
{
    /// <summary>
    /// Summary description for TsbPurchased.
    /// </summary>
    public class TsbPurchased
    {
        private Vehicle vehicle;
        private int tsbId;
        private DateTime purchaseDate;

        /// <summary>
        /// Default constuctor
        /// </summary>
        public TsbPurchased()
        {
        }

        /// <summary>
        /// Gets or sets the <see cref="Vehicle"/>.
        /// </summary>
        public Vehicle Vehicle
        {
            get
            {
                return this.vehicle;
            }
            set
            {
                this.vehicle = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/> TSBID.
        /// </summary>
        public int TsbId
        {
            get
            {
                return this.tsbId;
            }
            set
            {
                this.tsbId = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/>.
        /// </summary>
        public DateTime PurchaseDate
        {
            get
            {
                return this.purchaseDate;
            }
            set
            {
                this.purchaseDate = value;
            }
        }
    }
}
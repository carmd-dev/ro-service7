using Innova.Vehicles;

namespace Innova.DiagnosticReports
{
    /// <summary>
    /// Possible cause associated with the error code parent.
    /// </summary>
    public class PossibleCause
    {
        private string name = "";
        private string description = "";
        private VehicleTypeCollection vehicleTypesAppliedTo = null;

        //		private Metafuse3.BusinessObjects.Registry registry = null;

        /// <summary>
        /// Default constructor for the possible cause to use when adding a Delmar possible cause.
        /// </summary>
        /// <param name="name"><see cref="string"/> name of the error code.</param>
        /// <param name="description"><see cref="string"/> possible cause of the error code.</param>
        /// <param name="vehicleTypeAppliedTo"><see cref="VehicleType"/> Delmar vehicle type the possible cause is applied to. (Null if not a Delmar possible cause).</param>
        public PossibleCause(string name, string description, VehicleType vehicleTypeAppliedTo)
        {
            this.name = name;
            this.description = description;

            if (vehicleTypeAppliedTo != null)
            {
                this.AddVehicleTypesAppliedTo(vehicleTypeAppliedTo);
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> name of the error code.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> description of the possible cause of the error code.
        /// </summary>
        public string Description
        {
            get
            {
                return this.description;
            }
        }

        /// <summary>
        /// Gets the <see cref="VehicleTypeCollection"/> of <see cref="VehicleType"/> objects this possible cause discription is applied to.  Returns null if none.
        /// </summary>
        public VehicleTypeCollection VehicleTypesAppliedTo
        {
            get
            {
                return this.vehicleTypesAppliedTo;
            }
        }

        /// <summary>
        /// Adds a <see cref="VehicleType"/> object to the list of vehicle types to add this object to.
        /// </summary>
        /// <param name="vehicleTypeToAdd"><see cref="VehicleType"/> to add to the list.</param>
        protected internal void AddVehicleTypesAppliedTo(VehicleType vehicleTypeToAdd)
        {
            if (this.vehicleTypesAppliedTo == null)
            {
                this.vehicleTypesAppliedTo = new VehicleTypeCollection(vehicleTypeToAdd.Registry);
            }
            this.vehicleTypesAppliedTo.Add(vehicleTypeToAdd);
        }
    }
}
using System.ComponentModel;

namespace Innova.Vehicles
{
    /// <summary>
    /// The supported vehicle warranty types.
    /// </summary>
    public enum VehicleWarrantyType
    {
        /// <summary>
        /// Body.
        /// </summary>
        [Description("Basic")]
        Basic = 0,

        /// <summary>
        /// Powertrain.
        /// </summary>
        [Description("Powertrain")]
        Powertrain = 1,

        /// <summary>
        /// Federal Emissions.
        /// </summary>
        [Description("Federal Emissions")]
        FederalEmissions = 2,

        //Added on 2017-10-11 by Nam Lu - INNOVA Dev Team: Defined the new WarrantyType for Electric/Hybrid vehicle
        /// <summary>
        /// Electric/Hybrid.
        /// </summary>
        [Description("Electric/Hybrid")]
        ElectricHybrid = 3
    }
}
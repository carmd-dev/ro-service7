using System.ComponentModel;

namespace Innova
{
    /// <summary>
    /// Gets the supported currency enumeration
    /// </summary>
    public enum Currency
    {
        /// <summary>
        /// US Dollars
        /// </summary>
        [Description("US Dollars")]
        USD = 0,

        /// <summary>
        /// Canadian Dollars
        /// </summary>
        [Description("Canadian Dollars")]
        CAD = 1,

        /// <summary>
        /// Euros
        /// </summary>
        [Description("Euros")]
        EUR = 2,

        /// <summary>
        /// Chinese Yuan
        /// </summary>
        [Description("Chinese Yuan")]
        CNY = 3,

        /// <summary>
        /// Mexico Nuevo Peso Yuan
        /// </summary>
        [Description("Mexico Nuevo Peso")]
        MXN = 4
    }
}
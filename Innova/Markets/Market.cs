using System.ComponentModel;

namespace Innova.Markets
{
    /// <summary>
    /// Gets the supported currency enumeration
    /// </summary>
    public enum Market
    {
        /// <summary>
        /// United States
        /// </summary>
        [Description("United States")]
        US = 0,

        /// <summary>
        /// Canada
        /// </summary>
        [Description("Canada")]
        CA = 1,

        /// <summary>
        /// Mexico
        /// </summary>
        [Description("Mexico")]
        MX = 2,

        /// <summary>
        /// China
        /// </summary>
        [Description("China")]
        CN = 3,

        /// <summary>
        /// Japan
        /// </summary>
        [Description("Japan")]
        JP = 4
    }
}
using System.ComponentModel;

namespace Innova
{
    /// <summary>
    /// Gets the supported language enumeration
    /// </summary>
    public enum Language
    {
        /// <summary>
        /// English
        /// </summary>
        [Description("English")]
        English = 0,

        /// <summary>
        /// Spanish (Mexico)
        /// </summary>
        [Description("Spanish")]
        SpanishMX = 1,

        /// <summary>
        /// French
        /// </summary>
        [Description("French")]
        French = 2,

        /// <summary>
        /// Mandarin
        /// </summary>
        [Description("Mandarin")]
        Mandarin = 3
    }
}
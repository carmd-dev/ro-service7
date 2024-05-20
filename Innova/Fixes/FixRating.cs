using System.ComponentModel;

namespace Innova.Fixes
{
    /// <summary>
    /// The enumeration of fix ratings
    /// </summary>
    public enum FixRating
    {
        /// <summary>
        /// Unrated
        /// </summary>
        [Description("Unrated")]
        Unrated = 0,

        /// <summary>
        /// 1 Star
        /// </summary>
        [Description("1 Star")]
        OneStar = 1,

        /// <summary>
        /// 2 Star
        /// </summary>
        [Description("2 Stars")]
        TwoStar = 2,

        /// <summary>
        /// 3 Star
        /// </summary>
        [Description("3 Stars")]
        ThreeStar = 3,

        /// <summary>
        /// 4 Star
        /// </summary>
        [Description("4 Stars")]
        FourStar = 4,

        /// <summary>
        /// 5 Star
        /// </summary>
        [Description("5 Stars")]
        FiveStar = 5,
    }
}